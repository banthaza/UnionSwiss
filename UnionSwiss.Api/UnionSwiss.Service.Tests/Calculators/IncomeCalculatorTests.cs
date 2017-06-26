using NUnit.Framework;
using UnionSwiss.Domain.Model.Entity;
using UnionSwiss.Service.Calculators;

namespace UnionSwiss.Service.Tests.Calculators
{
    [TestFixture]
    public class IncomeCalculatorTests
    {
        private IncomeCalculator _incomeCalculator;

        [SetUp]
        public void SetUp()
        {

            _incomeCalculator = new IncomeCalculator();
        }

        [Test()]
        public void CalculateGrossIncome_Should_Return_Valid()
        {
            //Arrange 
            var expected = 5004m;   
            //Act 
            var actual = _incomeCalculator.CalculateMonthlyGross(60050);

            //Assery
            Assert.AreEqual(expected, actual, $"Should be {expected} but was {actual}");

        }

        [Test()]
        public void CalculatePension1_Should_Return_Valid()
        {
            //Arrange 
            var monthly = 5004;
            var pension = 9;
            var expected = 450;
            //Act 
            var actual = _incomeCalculator.CalculatePension(monthly, pension);

            //Assery
            Assert.AreEqual(expected, actual, $"Should be {expected} but was {actual}");

        }

        [Test()]
        public void CalculatePension2_Should_Return_Valid()
        {
            //Arrange 
            var monthly = 10000;
            var pension = 10;
            var expected = 1000;
            //Act 
            var actual = _incomeCalculator.CalculatePension(monthly, pension);

            //Assery
            Assert.AreEqual(expected, actual, $"Should be {expected} but was {actual}");

        }

        [Test()]
        public void CalculateNet_Should_Return_Valid()
        {
            //Arrange 
            var monthly = 5004;
            var tax = 922;
            var expected = 4082;
            //Act 
            var actual = _incomeCalculator.CalculateNetIncome(monthly, tax);

            //Assery
            Assert.AreEqual(expected, actual, $"Should be {expected} but was {actual}");
        }

        [Test()]
        public void CalculateMonthly_Should_Return_Valid()
        {
            //Arrange 
            var annualSalary = 60050; 
            var minQualifyingValue = 37001;
            var baseTaxValue = 3572;
            var multipier = 0.325m;
            var expected = 922;
            //Act 
            var actual = _incomeCalculator.CalculateMonthlyTax(annualSalary, minQualifyingValue, baseTaxValue, multipier);

            //Assery
            Assert.AreEqual(expected, actual, $"Should be {expected} but was {actual}");
        }


    }
}
