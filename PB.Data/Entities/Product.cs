using IEntity = PB.Data.Interfaces.IEntity;

namespace PB.Data.Entities
{
    public class Product : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
