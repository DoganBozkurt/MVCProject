using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace MVCProject.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        private readonly TransactionManager _transactionManager;
        readonly private UserManager<User> _userManager;
        private const string IncomeCategoryType = "Income";
        private const string ExpenseCategoryType = "Expense";

        public DashboardController(UserManager<User> userManager)
        {
            _userManager = userManager;
            _transactionManager = new TransactionManager(new EFTransactionDal());
        }




        public async Task<ActionResult> Index()
        {
            //last 7 days
            DateTime StartDate = DateTime.Today.AddDays(-6);

            //Active user by transtaction
            var currentUser = await _userManager.GetUserAsync(User);
            List<Transaction> selectedTransactions = _transactionManager.TSelectedTransactions(currentUser.Id);

            //Total Income
            int TotalIncome = CalculateTotalAmount(selectedTransactions, IncomeCategoryType);
            ViewBag.TotalIncome = TotalIncome.ToString("C0");

            //Total Expense
            int TotalExpense = CalculateTotalAmount(selectedTransactions, ExpenseCategoryType);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");

            //Balance
            int Balance = TotalIncome - TotalExpense;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = String.Format(culture, "{0:C0}", Balance);

            //Doughnut Chart - Expense By Category
            ViewBag.DoughnutChartData = GetDoughnutChartDate(selectedTransactions);

            //Income
            List<SplineChartData> IncomeSummary = GenerateIncomeSummary(selectedTransactions, IncomeCategoryType, StartDate);

            //Expense
            List<SplineChartData> ExpenseSummary = GenerateIncomeSummary(selectedTransactions, ExpenseCategoryType, StartDate);

            //Spline Chart
            var splineChartData = CombineChartData(StartDate, IncomeSummary, ExpenseSummary);
            ViewBag.SplineChartData = splineChartData;

            //Last Transaction
            ViewBag.RecentTransactions = selectedTransactions.TakeLast(3).ToList();
            return View();
        }

        private int CalculateTotalAmount(List<Transaction> transactions, string categoryType)
        {
            return transactions
                .Where(t => t.Category.Type == categoryType)
                .Sum(t => t.Amount);
        }

        private List<Object> GetDoughnutChartDate(IEnumerable<Transaction> selectedTransactions)
        {
            return selectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Category.CategoryID)
                .Select(k => new
                {
                   // categoryTitleWithIcon = k.First().Category.IconData.Title + " " + k.First().Category.Title,
                    categoryTitleWithIcon = k.First().Category.Title,
                    amount = k.Sum(j => j.Amount),
                    formattedAmount = k.Sum(j => j.Amount).ToString("C0"),
                })
                .OrderByDescending(l => l.amount)
                .ToList<object>();
        }

        private List<SplineChartData> GenerateIncomeSummary(List<Transaction> selectedTransactions, string categoryType, DateTime startDate)
        {
            string[] last7Days = Enumerable.Range(0, 7)
            .Select(i => startDate.AddDays(i).ToString("dd-MMM"))
            .ToArray();

            var result = Enumerable.Range(0, 7)
                    .Select(i => startDate.AddDays(i))
                    .GroupJoin(
                        selectedTransactions.Where(i => i.Category.Type == categoryType),
                        date => date,
                        transaction => transaction.Date,
                        (date, transactions) => new SplineChartData()
                        {
                            day = date.ToString("dd-MMM"),
                            amount = transactions.Any() ? transactions.Sum(l => l.Amount) : 0
                        })
                    .ToList();

            int lastItem = 0;
            foreach (var data in result)
            {
                if (data.amount == 0)
                {
                    data.amount = lastItem;
                }
                else
                {
                    lastItem = data.amount;
                }
            }

            return result;
        }
        private IEnumerable<object> CombineChartData(DateTime startDate, List<SplineChartData> incomeSummary, List<SplineChartData> expenseSummary)
        {
            string[] last7Days = Enumerable.Range(0, 7)
                .Select(i => startDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            return from day in last7Days
                   join income in incomeSummary on day equals income.day into dayIncomeJoined
                   from income in dayIncomeJoined.DefaultIfEmpty()
                   join expense in expenseSummary on day equals expense.day into expenseJoined
                   from expense in expenseJoined.DefaultIfEmpty()
                   select new
                   {
                       day = day,
                       income = income?.amount ?? 0,
                       expense = expense?.amount ?? 0,
                   };
        }

    }
}
