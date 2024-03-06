namespace Device.Domain.Entities
{
    public class Device : Entity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
