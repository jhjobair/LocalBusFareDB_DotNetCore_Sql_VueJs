namespace WebApi.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; } 
        public DateTime EntryDate { get; set; }
        public string EntryBy { get; set; }

        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string Status { get; set; }
    }
}
