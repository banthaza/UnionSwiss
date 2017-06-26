using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using UnionSwiss.Domain.Calculators;
using UnionSwiss.Domain.Model.Entity;
using UnionSwiss.Domain.Repository;
using UnionSwiss.Persistence.Context;
using UnionSwiss.Persistence.Factory;
using UnionSwiss.Persistence.Repository;
using UnionSwiss.Service.Calculators;
using UnionSwiss.Service.Services;

namespace UnionSwiss.Service.Tests.Service
{
    [TestFixture()]
    public class EmployeeServiceTestFixture
    {
        private EmployeeService _employeeService;
        private IRepository<long> _repository;

        private IIncomeCalculator _incomeCalculator;

        private IDbContextFactory<IUnionSwissContext> _dbContextFactory;
        private IUnionSwissContext _dbContext;
        private DbSet<Employee> _dbSetEmployees;
        private DbSet<EmployeePayslip> _dbSetEmployeePayslips;
        private DbSet<TaxPeriod> _dbsetTaxPeriod;
        private DbSet<TaxBracket> _dbSetTaxBrackets;

        private readonly IQueryable<Employee> _employees = new List<Employee>
        {
            new Employee() {Id = 1, FirstName = "Andrew" , LastName = "Baker" , AnnualSalary = 60050, PensionContributionPercentage = 9, MicrosoftStartDate= new DateTime(2017, 1, 1),
                 Payslips =new List<EmployeePayslip>() },
            new Employee()
            {
                Id = 2, FirstName = "Chris" , LastName = "Davies" , AnnualSalary = 1200000, PensionContributionPercentage = 10, MicrosoftStartDate= new DateTime(2017, 1, 1),
                Payslips =new List<EmployeePayslip>()
                
            },
                new Employee()
            {
                Id = 3, FirstName = "Some" , LastName = "OtherGuy" , AnnualSalary = 1200000, PensionContributionPercentage = 10, MicrosoftStartDate= new DateTime(2017, 1, 1),
                Payslips = new List<EmployeePayslip>()
                {
                    new EmployeePayslip() {EmployeeId = 2,  FullName = "Some OtherGuy", GrossIncome = 10000, TaxPeriodId = 1, IncomeTax = 2696,  NetIncome =7304, Pension = 1000}
                }
            }
        }.AsQueryable();

        private readonly IQueryable<EmployeePayslip> _employeepayslips = new List<EmployeePayslip>
        {
         new EmployeePayslip() {Id=1, TaxPeriodId = 1,EmployeeId = 1, FullName = "Andrew Baker", GrossIncome = 5004,  IncomeTax = 922,  NetIncome =4082, Pension = 450}
        
        }.AsQueryable();

        private readonly IQueryable<TaxPeriod> _taxPeriods = new List<TaxPeriod>
        {
            new TaxPeriod() {Id = 1, StartDate = new DateTime(2017,04, 01), EndDate = new DateTime(2017,04, 30), TaxYearId = 1}

        }.AsQueryable();

        private readonly IQueryable<TaxBracket> _taxBrackets = new List<TaxBracket>
        {
            new TaxBracket() { Id = 1, TaxYearId = 1, MinQualifyingValue = 0, MaxQualifyingValue = 18200, IncrementMultiplier = 0, BaseTaxValue = 0},
            new TaxBracket() { Id = 2, TaxYearId = 1, MinQualifyingValue = 18201, MaxQualifyingValue = 37000, IncrementMultiplier = 0.19m, BaseTaxValue = 0},
            new TaxBracket() { Id = 3, TaxYearId = 1, MinQualifyingValue = 37001, MaxQualifyingValue = 80000, IncrementMultiplier = 0.325m, BaseTaxValue = 3527},
            new TaxBracket() { Id = 4, TaxYearId = 1, MinQualifyingValue = 80001, MaxQualifyingValue = 180000, IncrementMultiplier = 0.325m, BaseTaxValue = 17547 },
            new TaxBracket() { Id = 5, TaxYearId = 1, MinQualifyingValue = 180001, MaxQualifyingValue = Int32.MaxValue, IncrementMultiplier = 0.45m, BaseTaxValue = 54547 },

        }.AsQueryable();

        [SetUp()]
        public void Setup()
        {

            _dbContextFactory = A.Fake<IDbContextFactory<IUnionSwissContext>>();
            _dbContext = A.Fake<IUnionSwissContext>();
            _dbSetEmployees = A.Fake<DbSet<Employee>>(builder => builder.Implements(typeof(IQueryable<Employee>)));
            _dbSetEmployeePayslips = A.Fake<DbSet<EmployeePayslip>>(builder => builder.Implements(typeof(IQueryable<EmployeePayslip>)));
            _dbsetTaxPeriod = A.Fake<DbSet<TaxPeriod>>(builder => builder.Implements(typeof(IQueryable<TaxPeriod>)));
            _dbSetTaxBrackets = A.Fake<DbSet<TaxBracket>>(builder => builder.Implements(typeof(IQueryable<TaxBracket>)));

            A.CallTo(() => _dbContextFactory.CreateContext()).Returns(_dbContext);

            A.CallTo(() => ((IQueryable<Employee>)_dbSetEmployees).Provider).Returns(_employees.Provider);
            A.CallTo(() => ((IQueryable<Employee>)_dbSetEmployees).Expression).Returns(_employees.Expression);
            A.CallTo(() => ((IQueryable<Employee>)_dbSetEmployees).ElementType).Returns(_employees.ElementType);
            A.CallTo(() => ((IQueryable<Employee>)_dbSetEmployees).GetEnumerator()).Returns(_employees.GetEnumerator());
            A.CallTo(() => _dbContext.GetDbSet<Employee>()).Returns(_dbSetEmployees);

            A.CallTo(() => ((IQueryable<EmployeePayslip>)_dbSetEmployeePayslips).Provider).Returns(_employeepayslips.Provider);
            A.CallTo(() => ((IQueryable<EmployeePayslip>)_dbSetEmployeePayslips).Expression).Returns(_employeepayslips.Expression);
            A.CallTo(() => ((IQueryable<EmployeePayslip>)_dbSetEmployeePayslips).ElementType).Returns(_employeepayslips.ElementType);
            A.CallTo(() => ((IQueryable<EmployeePayslip>)_dbSetEmployeePayslips).GetEnumerator()).Returns(_employeepayslips.GetEnumerator());
            A.CallTo(() => _dbContext.GetDbSet<EmployeePayslip>()).Returns(_dbSetEmployeePayslips);

            A.CallTo(() => ((IQueryable<TaxPeriod>)_dbsetTaxPeriod).Provider).Returns(_taxPeriods.Provider);
            A.CallTo(() => ((IQueryable<TaxPeriod>)_dbsetTaxPeriod).Expression).Returns(_taxPeriods.Expression);
            A.CallTo(() => ((IQueryable<TaxPeriod>)_dbsetTaxPeriod).ElementType).Returns(_taxPeriods.ElementType);
            A.CallTo(() => ((IQueryable<TaxPeriod>)_dbsetTaxPeriod).GetEnumerator()).Returns(_taxPeriods.GetEnumerator());
            A.CallTo(() => _dbContext.GetDbSet<TaxPeriod>()).Returns(_dbsetTaxPeriod);

            A.CallTo(() => ((IQueryable<TaxBracket>)_dbSetTaxBrackets).Provider).Returns(_taxBrackets.Provider);
            A.CallTo(() => ((IQueryable<TaxBracket>)_dbSetTaxBrackets).Expression).Returns(_taxBrackets.Expression);
            A.CallTo(() => ((IQueryable<TaxBracket>)_dbSetTaxBrackets).ElementType).Returns(_taxBrackets.ElementType);
            A.CallTo(() => ((IQueryable<TaxBracket>)_dbSetTaxBrackets).GetEnumerator()).Returns(_taxBrackets.GetEnumerator());
            A.CallTo(() => _dbContext.GetDbSet<TaxBracket>()).Returns(_dbSetTaxBrackets);

            _repository  =new LongRepositroy(_dbContextFactory);
            _incomeCalculator  = new IncomeCalculator();
            _employeeService = new EmployeeService(_repository, _incomeCalculator);
        }

        [Test()]
        public void Cstor_Given_Null_EmployeeRepository_Throws_Exception()
        {
            //Arrange 

            //Act  

            //Assert
            Assert.Throws<ArgumentNullException>(() => new EmployeeService(null, null));
        }

        [Test()]
        public void Cstor_Given_Null_IncomeCulculator_Throws_Exception()
        {
            //Arrange 

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => new EmployeeService(_repository, null));

        }

        [Test()]
        public void Cstor_Given_Valid_Passes()
        {
            //Arrange 

            //Act
            var service = new EmployeeService(_repository, _incomeCalculator);
            //Assert
            Assert.NotNull(service);

        }

        [Test()]
        public void GetEmployee_Given_Zero_employeeId_Throws_ArgumentOutofRangeException()
        {
            //Arrange 

            //Act
                
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _employeeService.GetEmployee(0));

        }

        [Test()]
        public void GetEmployee_Given_NonExising_employeeId_Throws_InvalidDataExceptionn()
        {
            //Arrange 

            //Act

            //Assert
            Assert.Throws<InvalidDataException>(() => _employeeService.GetEmployee(10));

        }

        [Test()]
        public void GetEmployee_Given_Valid_Passes()
        {
            //Arrange 
        
            //Act
            var employee = _employeeService.GetEmployee(3);
            //Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.Id==3);
            Assert.IsTrue(employee.Payslips.Count()==1);

        }

        [Test()]
        public void GetEmployee_When_Valid_Should_Call_Repository()
        {
            //Arrange 
       
            //Act
           var employee= _employeeService.GetEmployee(3);
            //Assert
           
            Assert.NotNull(employee);
            Assert.IsAssignableFrom(typeof(Employee), employee,"Should retun an employee" );
            Assert.IsTrue(employee.Id==3);
            Assert.IsTrue(employee.Payslips.Count == 1);
        }


        [Test()]
        public void GetPayslip_Given_Zero_EmployeeId_Throws_ArgumentOutOfRange()
        {
            //Arrange 

            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _employeeService.GetPayslip(0, 0));
        }

        [Test()]
        public void GetPayslip_Given_Zero_TaxPeriod_Throws_ArgumentOutOfRange()
        {
            //Arrange 

            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _employeeService.GetPayslip(1, 0));
        }


        [Test()]
        public void GetPayslip_Given_NonExisting_Throws_InvalidDataException()
        {
            //Arrange 

            //Act

            //Assert
            Assert.Throws<InvalidDataException>(() => _employeeService.GetPayslip(6, 1));
        }


        [Test()]
        public void GetPayslip_Given_Valid_Throws_Returns()
        {
            //Arrange 

            //Act
            var payslip = _employeeService.GetPayslip(1, 1);
            //Assert
            Assert.NotNull(payslip);
            Assert.IsTrue(payslip.Id ==1);
            Assert.IsTrue(payslip.EmployeeId == 1);
        }

        [Test()]
        public void ListEmployees_Should_Retun_EmployeeList()
        {
            //Arrange 

            //Act
            var employeeList = _employeeService.ListEmployees();
            //Assert
            Assert.IsTrue(employeeList.Count ==3);
        }


        [Test()]
        public void SaveEmployee_Given_Null_Employee_Should_Throw_ArgumentNullException()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => _employeeService.SaveEmployee(null));
        }

        [Test()]
        public void SaveEmployee_Given_NullOrEmptyFirstName_Should_Throw_ArgumentNullException()
        {
            //Arrange
               var employee = new Employee();
            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => _employeeService.SaveEmployee(employee));
        }

        [Test()]
        public void SaveEmployee_Given_NullOrEmptyLastName_Should_Throw_ArgumentNullException()
        {
            //Arrange
            var employee = new Employee() {FirstName = "FirstName"};
            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => _employeeService.SaveEmployee(employee));
        }

        [Test()]
        public void SaveEmployee_Given_ZeroAnualSalary_Should_Throw_ArgumentOutOfRange()
        {
            //Arrange
            var employee = new Employee() { FirstName = "FirstName" , LastName = "Lastname", AnnualSalary = -1};
            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _employeeService.SaveEmployee(employee));
        }

        [Test()]
        public void SaveEmployee_Given_NegativeAnualSalar_Should_Throw_ArgumentOutOfRange()
        {
            //Arrange
            var employee = new Employee() { FirstName = "FirstName", LastName = "Lastname", AnnualSalary = -1 };
            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _employeeService.SaveEmployee(employee));
        }


        [Test()]
        public void SaveEmployee_Given_NegativePension_Should_Throw_ArgumentOutOfRange()
        {
            //Arrange
            var employee = new Employee() { FirstName = "FirstName", LastName = "Lastname", AnnualSalary = 1, PensionContributionPercentage = -1};
            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _employeeService.SaveEmployee(employee));
        }


        [Test()]
        public void SaveEmployee_Given_Pension_GreaterThan_50_Should_Throw_ArgumentOutOfRange()
        {
            //Arrange
            var employee = new Employee() { FirstName = "FirstName", LastName = "Lastname", AnnualSalary = 1, PensionContributionPercentage = 51 };
            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _employeeService.SaveEmployee(employee));
        }

        
        [Test()]
        [Ignore("I cant set the invalid cast cause a failure before the test runs")]
        public void SaveEmployee_InvalidDate_String_Throws_InvalidCastException()
        {
            //Arrange
            var employee = new Employee() {  FirstName = "FirstName", LastName = "Lastname", AnnualSalary = 1, PensionContributionPercentage = 51, StartDate = "2015"};
            //Act

            //Assert
            Assert.Throws<InvalidCastException>(() => _employeeService.SaveEmployee(employee));
        }

        [Test()]
        public void SaveEmployee_GivenUpdate_Passes()
        {
            //Arrange
            
            var employee = new Employee() {Id = 2, FirstName = "FirstName", LastName = "Lastname", AnnualSalary = 1, PensionContributionPercentage = 50, StartDate = "2017-04-01" };
            //Act
            var returnedEmployee = _employeeService.SaveEmployee(employee);
            //Assert
            Assert.NotNull(returnedEmployee);
            Assert.NotNull(returnedEmployee.Id=2);
            Assert.NotNull(returnedEmployee.FirstName = "FirstName");
            Assert.NotNull(returnedEmployee.Payslips.Count()==1);
        }

        [Test()]
        [Ignore("This will fail because the mocked repository will not add a new Id")]
        public void SaveEmployee_Given_New_Passes()
        {
            //Arrange

            var employee = new Employee() {  FirstName = "FirstName", LastName = "Lastname", AnnualSalary = 1, PensionContributionPercentage = 51, StartDate = "2017-04-01" };
            //Act
            var returnedEmployee = _employeeService.SaveEmployee(employee);
            //Assert
            Assert.NotNull(returnedEmployee);
            Assert.NotNull(returnedEmployee.Id = 2);
            Assert.NotNull(returnedEmployee.FirstName = "FirstName");
            Assert.NotNull(returnedEmployee.Payslips.Count() == 1);
        }


        [Test()]
        public void Delete_Given_0_Throws_ArgumentOutOfRangeException()
        {
            //Arange 

            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _employeeService.Delete(0));
        }

        [Test()]
        [Ignore("Not able to test this due to the structure of entity framework")]
        public void Delete_Given_Valid_Should_RemoveItem()
        {
            //Arange 

            //Act
            _employeeService.Delete(1);
            //Assert
            Assert.False(_repository.Query<Employee>(x=>x.Id==1).Any());
        }

     

        [Test()]
        public void CreatePayslip_Given_0_EmployeeId_Should_Throw_ArgumentOutOfRangeException()
        {
            //Arange 

            //Act
     
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _employeeService.CreatePaySlip(0, new DateTime() ));
        }


        [Test()]   
        public void CreatePayslip_Given_EmployeeId_Should_Throw_ArgumentOutOfRangeException()
        {
            //Arange 

            //Act
       
            //Assert
            Assert.Throws<InvalidCastException>(() => _employeeService.CreatePaySlip(1, new DateTime()));
        }

        [Test()]
        public void CreatePayslip_When_Employee_Not_Found_Throws_InvalidDataException()
        {
            //Arange 

            //Act
          
            //Assert
            Assert.Throws<InvalidDataException>(() => _employeeService.CreatePaySlip(7, DateTime.Now));
        }

        [Test()]
        public void CreatePayslip_When_TaxPeriod_Not_Found_Throws_InvalidDataException()
        {
            //Arange 

            //Act

            //Assert
            Assert.Throws<InvalidDataException>(() => _employeeService.CreatePaySlip(1,new DateTime(2017, 05, 01)));
        }


        [Test()]
        public void CreatePayslip_When_Payslip_Found_Throws_InvalidDataException()
        {
            //Arange 

            //Act

            //Assert
            Assert.Throws<InvalidDataException>(() => _employeeService.CreatePaySlip(1, new DateTime(2017, 04, 01)));
        }

        [Test()]
        public void CreatePayslip_When_TaxBracket_Not_Found_Throws_InvalidDataException()
        {
            //Arange 

            //Act

            //Assert
            Assert.Throws<InvalidDataException>(() => _employeeService.CreatePaySlip(3, new DateTime(2016, 04, 01)));
        }

        [Test()]
        public void CreatePayslip_When_TaxBracket_37001_8000_Found_Throws_InvalidDataException()
        {
            //Arange 

            //Act
            var payslip = _employeeService.CreatePaySlip(1, new DateTime(2017, 04, 01));
            //Assert
           Assert.True(payslip.GrossIncome==5004);
           Assert.True(payslip.IncomeTax == 922);
           Assert.True(payslip.NetIncome == 4082);
           Assert.True(payslip.Pension == 450);
           Assert.True(payslip.FullName == "Andrew Baker");
           Assert.True(payslip.FullName == "01 March - 31 March");
        }

    }
}