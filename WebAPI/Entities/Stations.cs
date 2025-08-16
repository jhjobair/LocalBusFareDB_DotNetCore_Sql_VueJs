namespace WebApi.Entities
{
    public class Stations: BaseEntity
    {
        public string StationNameEN { get; set; }
        public string StationNameBN { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }
}
