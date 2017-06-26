using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using UnionSwiss.Persistence.Factory;
using Assert = NUnit.Framework.Assert;
using CollectionAssert = NUnit.Framework.CollectionAssert;


namespace UnionSwiss.PersistenceTests.Repository
{
    [TestFixture()]
    public class BaseRepositoryTestFixture
    {
        private TestBaseRepository _baseRepository;
        private IDbContextFactory<TestDbContext> _dbContextFactory;
        private TestDbContext _dbContext;
        private DbSet<TestEntity> _testEntityDbSet;

        private readonly IQueryable<TestEntity> _testEntities = new List<TestEntity>
        {
            new TestEntity {Id = 1, Name = "TE 1.0"},
            new TestEntity {Id = 2, Name = "TE 2"},
            new TestEntity {Id = 3, Name = "TE 3"},
            new TestEntity {Id = 4, Name = "TE 4"},
            new TestEntity {Id = 1, Name = "TE 1.1"}
        }.AsQueryable();

        [SetUp]
        public void Setup()
        {
            _dbContextFactory = A.Fake<IDbContextFactory<TestDbContext>>();
            _dbContext = A.Fake<TestDbContext>();
            _testEntityDbSet = A.Fake<DbSet<TestEntity>>(builder => builder.Implements(typeof (IQueryable<TestEntity>)));

            A.CallTo(() => _dbContextFactory.CreateContext()).Returns(_dbContext);

            A.CallTo(() => ((IQueryable<TestEntity>) _testEntityDbSet).Provider).Returns(_testEntities.Provider);
            A.CallTo(() => ((IQueryable<TestEntity>) _testEntityDbSet).Expression).Returns(_testEntities.Expression);
            A.CallTo(() => ((IQueryable<TestEntity>) _testEntityDbSet).ElementType).Returns(_testEntities.ElementType);
            A.CallTo(() => ((IQueryable<TestEntity>) _testEntityDbSet).GetEnumerator())
                .Returns(_testEntities.GetEnumerator());

            A.CallTo(() => _dbContext.GetDbSet<TestEntity>()).Returns(_testEntityDbSet);
            A.CallTo(() => _dbContext.GetDbSet<TestEntity>().AsNoTracking()).Returns(_testEntityDbSet);
            A.CallTo(() => _dbContext.GetDbSet<TestEntity>().Local)
                .Returns(new ObservableCollection<TestEntity>(_testEntities));

            _baseRepository = new TestBaseRepository(_dbContextFactory);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException), ExpectedMessage = "dbContextFactory",
            MatchType = MessageMatch.Contains)]
        public void Constructor_GivenNullDbContextFactory_ThrowsArgumentNullException()
        {
            new TestBaseRepository(null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException), ExpectedMessage = "predicate",
            MatchType = MessageMatch.Contains)]
        public void Get_GivenNullPredicate_ThrowsArgumentNullException()
        {
            _baseRepository.Get<TestEntity>(null);
        }

        [Test]
        public void Get_GivenPredicateMatch_ReturnsFirst()
        {
            var testEntity = _baseRepository.Get<TestEntity>(x => x.Id == 1);

            Assert.AreEqual(1, testEntity.Id);
            Assert.AreEqual("TE 1.0", testEntity.Name);
        }

        [Test]
        public void Get_GivenPredicateDoesNotMatch_ReturnsDefaultEntity()
        {
            var entity = _baseRepository.Get<TestEntity>(x => x.Id == 11);

            Assert.IsNull(entity);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException), ExpectedMessage = "predicate",
            MatchType = MessageMatch.Contains)]
        public void Query_GivenNullPredicate_ThrowsArgumentNullException()
        {
            _baseRepository.Query<TestEntity>(null);
        }

        [Test]
        public void Query_GivenPredicateMatch_ReturnsCollection()
        {
            var entities = _baseRepository.Query<TestEntity>(x => x.Id == 1);

            Assert.AreEqual(2, entities.Count());
            Assert.IsFalse(entities.Any(x => x.Id != 1));
        }

       


   

        [Test]
        public void All_GivenCollection_ReturnsEntireCollection()
        {
            var repository = new TestBaseRepository(_dbContextFactory);

            var entities = repository.All<TestEntity>();

            Assert.AreEqual(5, entities.Count());
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException), ExpectedMessage = "predicate",
            MatchType = MessageMatch.Contains)]
        public void Delete_GivenNullPredicate_ThrowsArgumentNullException()
        {
            _baseRepository.Delete<TestEntity>(null);
        }

        [Test]
        public void Delete_GivenPredicateMatches_RemovesEntity()
        {
            // Arrange
            const int entityId = 1;

            // Act
            _baseRepository.Delete<TestEntity>(x => x.Id == entityId);

            // Assert
            A.CallTo(() => _dbContext.GetDbSet<TestEntity>().RemoveRange(null))
                .WhenArgumentsMatch(collection =>
                {
                    var entities = collection[0] as List<TestEntity>;

                    return entities != null && entities.All(x => x.Id == entityId);
                })
                .MustHaveHappened();
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException), ExpectedMessage = "entity", MatchType = MessageMatch.Contains
            )]
        public void Save_GivenNullEntity_ThrowsArgumentNullException()
        {
            _baseRepository.Save<TestEntity>(null, () => true);
        }

        [Test]
        public void Save_GivenValidArgument_SavesToContext()
        {
            var testEntity = new TestEntity {Name = "New"};
            _baseRepository.Save(testEntity, () => testEntity.Id == 0);

            A.CallTo(() => _dbContext.Save())
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Save_GivenEntityWithIdZero_AddsEntityToContext()
        {
            var testEntity = new TestEntity {Name = "New"};

            _baseRepository.Save(testEntity, () => testEntity.Id == 0);

            A.CallTo(() => _dbContext.GetDbSet<TestEntity>().Add(testEntity))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Save_GivenEntityWithIdGreaterThanZero_AttachesEntityAndUpdatesStateToModified()
        {
            // Arrange
            var updatedEntity = new TestEntity
            {
                Id = 4,
                Name = "Entity 4 Updated"
            };
            A.CallTo(() => _dbContext.GetDbSet<TestEntity>().Attach(updatedEntity)).Returns(updatedEntity);

            // Act
            _baseRepository.Save(updatedEntity, () => updatedEntity.Id == 0);

            // Assert
            A.CallTo(() => _dbContext.GetDbSet<TestEntity>().Attach(updatedEntity))
                .MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _dbContext.SetModified(updatedEntity))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

       

        [Test]
        public void Exists_GivenEntityDoesNotExistInRepository_ReturnsFalse()
        {
            // Arrange
            var entity = new TestEntity();

            // Act
            var exists = _baseRepository.Exists(entity);

            // Assert
            Assert.IsFalse(exists);
        }
    }
}
