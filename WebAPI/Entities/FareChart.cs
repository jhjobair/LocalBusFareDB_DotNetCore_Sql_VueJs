using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class FareChart:BaseEntity
    {
        public int FromStationId { get; set; }


        public int ToStationId { get; set; }

        public decimal Distance { get; set; }
        public decimal Fare { get; set; }

        public int ChartId { get; set; }

    }


    public class FareChartViewModel
    {
        public int Id { get; set; }
        public string FromStationName { get; set; } = string.Empty;
        public string ToStationName { get; set; } = string.Empty;
        public decimal Distance { get; set; }
        public decimal Fare { get; set; }
        public string ChartName { get; set; } = string.Empty;
        public string ChartCode { get; set; } = string.Empty;

        public int FromStationId { get; set; }
        public int ToStationId { get; set; }
        public int ChartId { get; set; }
    }
}
