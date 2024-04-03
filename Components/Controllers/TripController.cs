using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using VoyageVault.Components.Models;

namespace VoyageVault.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        [HttpPost]
        public IActionResult CalculateOwedAmounts([FromBody] TripRequest tripRequest)
        {
            if (tripRequest == null || tripRequest.Expenses == null || tripRequest.Expenses.Count == 0)
                return BadRequest("Invalid request or no expenses provided.");

            // Calculate total expenses
            decimal totalExpense = tripRequest.Expenses.Sum(e => e.Amount);

            // Calculate even share
            decimal evenShare = totalExpense / tripRequest.Expenses.Count;

            // Calculate owed amounts
            var owedAmounts = new Dictionary<string, decimal>();
            foreach (var expense in tripRequest.Expenses)
            {
                decimal owedAmount = evenShare - expense.Amount;
                owedAmounts.Add(expense.Name, owedAmount);
            }

            return Ok(JsonConvert.SerializeObject(owedAmounts));
        }
    }

}
