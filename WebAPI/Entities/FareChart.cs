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
}
