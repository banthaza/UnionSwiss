using UnionSwiss.Domain.Model.Entity.Interface;

namespace UnionSwiss.PersistenceTests
{
    public class TestEntity : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}