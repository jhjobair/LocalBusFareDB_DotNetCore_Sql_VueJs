namespace WebApi.Entities
{
    public class Stations: BaseEntity
    {
        public string StationNameEN { get; set; } = string.Empty;
        public string StationNameBN { get; set; } = string.Empty;
        public string longitude { get; set; } = string.Empty;
        public string latitude { get; set; } = string.Empty;
    }
}
