using Microsoft.AspNetCore.Mvc;
using Moola.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moola.IntegrationsTests
{
    public class UsersControllerTest
    {
        private readonly UsersController _controller;

        public UsersControllerTest()
        {
            var incomes = new List<Income>
                {
                    new Income{Id = 15, IncomeDate = DateTime.Now, Amount = 100, CategoryId = 1, Note = "Test", FinanceId = 1 },
                    new Income{Id = 16, IncomeDate = DateTime.Today, Amount = 200, CategoryId = 1, Note = "Test", FinanceId = 1 }
                };

            var expenses = new List<Expense>
                {
                    new Expense {Id = 15, ExpenseDate = DateTime.Now, Amount = 100, CategoryId = 1, Note = "Test", FinanceId = 1 },
                    new Expense { Id = 15, ExpenseDate = DateTime.Now, Amount = 50, CategoryId = 11, Note = "Test", FinanceId = 2 }
                };

            var mockDbContext = new Mock<MyContext>();
            mockDbContext.Setup(x => x.Incomes).Returns((DbSet<Income>)incomes.AsQueryable());
            mockDbContext.Setup(x => x.Expenses).Returns((DbSet<Expense>)expenses.AsQueryable());

            _controller = new UsersController(mockDbContext.Object);
        }

        [Fact]
        public void BalanceIsCorrect()
        {
            // Arrange
            var expectedBalance = 150;
            // Act
            var actualBalance = _controller.Balance();

            // Assert
            Assert.Equal(expectedBalance.ToString(), actualBalance.ToString());

            //var result = controller.Balance() as ViewResult;
            //var viewData = result.ViewData;

            //// Assert
            //Assert.NotNull(result);
            //Assert.Equal(275, viewData["TotalAmount"]);
            //Assert.Equal(300, viewData["TotalIncomesAmount"]);
            //Assert.Equal(125, viewData["TotalExpensesAmount"]);
            //Assert.Equal(50, viewData["CardBalance"]);
            //Assert.Equal(125, viewData["CashBalance"]);
        }
    }
}
