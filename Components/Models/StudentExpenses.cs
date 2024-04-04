using VoyageVault.Components.Controllers;
namespace VoyageVault.Components.Models
{
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

