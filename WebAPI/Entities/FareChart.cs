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
        public string FromStationName { get; set; }
        public string ToStationName { get; set; }
        public decimal Distance { get; set; }
        public decimal Fare { get; set; }
        public string ChartName { get; set; }
        public string ChartCode { get; set; }

        public int FromStationId { get; set; }
        public int ToStationId { get; set; }
        public int ChartId { get; set; }
    }
}
