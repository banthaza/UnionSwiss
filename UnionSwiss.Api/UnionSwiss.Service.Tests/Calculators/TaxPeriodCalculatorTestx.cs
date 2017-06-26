using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using UnionSwiss.Service.Calculators;
using Assert = NUnit.Framework.Assert;

namespace UnionSwiss.Service.Tests.Calculators
{
    [TestFixture]
    public class TaxPeriodCalculatorTests
    {
        private TaxYearGenerator _taxYearGenerator;
        [SetUp]
        public void SetUp()
        {
            _taxYearGenerator = new TaxYearGenerator();
        }

        [Test()]
        public void TaxYearGenerator_Should_Return_Valid()
        {
            //Arrange 

            //Act 
            var taxYear = _taxYearGenerator.GenerteTaxYear(new DateTime(2017, 3, 1));
            //Assert
          Assert.AreEqual(taxYear.StartDate ,new DateTime(2017, 3, 1));
          Assert.AreEqual(taxYear.EndDate, new DateTime(2018, 2, 28));
        }

        [Test()]
        public void TaxYearGenerator_When_LeapYear_Should_Calculate_29th()
        {
            //Arrange 

            //Act 
            var taxYear = _taxYearGenerator.GenerteTaxYear(new DateTime(2015, 3, 1));
            //Assert
            Assert.AreEqual(taxYear.StartDate, new DateTime(2015, 3, 1));
            Assert.AreEqual(taxYear.EndDate, new DateTime(2016, 2, 29));
        }


    }
}
