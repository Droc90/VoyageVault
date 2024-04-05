using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VoyageVault.Components.Models;

namespace VoyageVault.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoyageController : ControllerBase
    {
        [HttpPost]
        public IActionResult CalculateOwedAmounts([FromBody] List<StudentExpense> studentExpenses)
        {
            if (studentExpenses == null || studentExpenses.Count == 0)
                return BadRequest("No student expenses provided.");

            // Calculate total trip cost
            decimal totalTripCost = studentExpenses.SelectMany(se => se.Expenses).Sum(e => e.Amount);

            // Calculate even share
            decimal evenShare = Math.Round(totalTripCost / studentExpenses.Count, 2);

            // Dictionary to store owed amounts per person
            var owedAmounts = new Dictionary<string, Dictionary<string, decimal>>();

            // Calculate amount owed by each student to others
            foreach (var studentExpense in studentExpenses)
            {
                // Dictionary to store owed amounts for the current student
                var owedToCurrentStudent = new Dictionary<string, decimal>();

                decimal totalPaid = studentExpense.Expenses.Sum(e => e.Amount);
                decimal amountOwed = totalPaid - evenShare;

                if(amountOwed < 0)
                {
                    foreach (var otherStudentExpense in studentExpenses.Where(se => se.StudentName != studentExpense.StudentName))
                    {
                        // Calculate the amount owed to other students
                        decimal otherStudentTotal = otherStudentExpense.Expenses.Sum(e => e.Amount);
                        //does this person owe money
                        decimal amountToOther = evenShare - otherStudentExpense.Expenses.Sum(e => e.Amount);
                        if (amountToOther < 0) 
                        {
                            decimal amountToExchange = Math.Min(Math.Abs(amountOwed), Math.Abs(amountToOther));

                            if (amountToExchange > 0)
                            {
                                // Add owed amount to the dictionary
                                owedToCurrentStudent.Add(otherStudentExpense.StudentName, Math.Round(amountToExchange, 2));
                            }
                        }

                    }

                    // Add total paid by the current student to the dictionary
                    owedToCurrentStudent.Add("TotalPaid", totalPaid);
                }
                else 
                {
                    owedToCurrentStudent.Add("TotalPaid", totalPaid);
                }

                // Add the owed amounts for the current student to the outer dictionary
                owedAmounts.Add(studentExpense.StudentName, owedToCurrentStudent);
            }

            // Prepare response object
            var response = new
            {
                OwedAmounts = owedAmounts,
                TotalTripCost = totalTripCost,
                EvenShare = evenShare
            };

            return Ok(response);
        }
    }
}
