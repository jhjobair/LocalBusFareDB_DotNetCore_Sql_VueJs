using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Interface;

namespace WebApi.Services
{
    public class FareChartService : IFareChartService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public FareChartService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<FareChartViewModel> GetAll()
        {
            var result = (from fc in _context.FareChart
                          join fs in _context.Stations on fc.FromStationId equals fs.Id
                          join ts in _context.Stations on fc.ToStationId equals ts.Id
                          join ci in _context.ChartInfo on fc.ChartId equals ci.Id
                          select new FareChartViewModel
                          {
                              Id = fc.Id,
                              FromStationName = fs.StationNameEN + " - " + fs.StationNameBN,
                              ToStationName = ts.StationNameEN + " - " + ts.StationNameBN,
                              Distance = fc.Distance,
                              Fare = fc.Fare,
                              ChartName = ci.ChartName,
                              ChartCode = ci.ChartCode,
                              FromStationId=fc.FromStationId,
                              ToStationId=fc.ToStationId,
                              ChartId = fc.ChartId
                          }).ToList();

            return result;
        }

    }
}
