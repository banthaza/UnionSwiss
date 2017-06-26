namespace UnionSwiss.Domain.Model.Entity.Interface
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
