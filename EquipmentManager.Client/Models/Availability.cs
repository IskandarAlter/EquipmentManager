namespace EquipmentManager.Client.Models
{
    public class Availability
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; }

        public int EquipmentId { get; set; }
    }
}
