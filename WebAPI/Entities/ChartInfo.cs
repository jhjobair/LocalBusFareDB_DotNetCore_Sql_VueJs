namespace WebApi.Entities
{
    public class ChartInfo:BaseEntity
    {
        public string  ChartName { get; set; } = string.Empty;
        public string ChartCode { get; set; } = string.Empty;
        public string ChartPath { get; set; } = string.Empty;
    }
}
