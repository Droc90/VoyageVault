

namespace VoyageVault.Components.Controllers
{


using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

                decimal totalTripExpense = studentExpenses.Sum(se => se.Expenses.Sum(e => e.Amount));

                decimal evenShare = totalTripExpense / studentExpenses.Count;

                var owedAmounts = new Dictionary<string, decimal>();

                foreach (var studentExpense in studentExpenses)
                {
                    decimal totalStudentExpense = studentExpense.Expenses.Sum(e => e.Amount);
                    decimal amountOwed = evenShare - totalStudentExpense;
                    owedAmounts.Add(studentExpense.StudentName, amountOwed);
                }

                return Ok(owedAmounts);
            }
        }

        public class StudentExpense
        {
            public string StudentName { get; set; }
            public List<Expense> Expenses { get; set; }
        }

        public class Expense
        {
            public decimal Amount { get; set; }
        }
    }
}