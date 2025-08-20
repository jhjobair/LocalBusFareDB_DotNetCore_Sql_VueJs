using AutoMapper;
using System.Runtime.InteropServices;
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
        public FareChartViewModel GetById(int id)
        {
            var result = (from fc in _context.FareChart
                          join fs in _context.Stations on fc.FromStationId equals fs.Id
                          join ts in _context.Stations on fc.ToStationId equals ts.Id
                          join ci in _context.ChartInfo on fc.ChartId equals ci.Id
                          where fc.Id == id
                          select new FareChartViewModel
                          {
                              Id = fc.Id,
                              FromStationName = fs.StationNameEN + " - " + fs.StationNameBN,
                              ToStationName = ts.StationNameEN + " - " + ts.StationNameBN,
                              Distance = fc.Distance,
                              Fare = fc.Fare,
                              ChartName = ci.ChartName,
                              ChartCode = ci.ChartCode,
                              FromStationId = fc.FromStationId,
                              ToStationId = fc.ToStationId,
                              ChartId = fc.ChartId
                          }).FirstOrDefault();

            return result;
        }

        //private FareChart getFareChart(int id)
        //{
        //    var data = _context.FareChart.Find(id);
        //    if (data == null) return null; // throw new KeyNotFoundException("Stations not found");
        //    return data;
        //}



        public string Create(FareChart model)
        {
            // validate (check if either direction already exists)
            if (_context.FareChart.Any(x =>
                (x.FromStationId == model.FromStationId && x.ToStationId == model.ToStationId) ||
                (x.FromStationId == model.ToStationId && x.ToStationId == model.FromStationId)))
            {
                return $"Stations with the id '{model.Id}' already exists";
            }

            var fareCharts = new List<FareChart>();

            // prepare common info
            model.EntryDate = DateTime.Now;
            model.EntryBy = "";
            model.UpdateDate = DateTime.Now;
            model.UpdateBy = "";

            // add the original record
            fareCharts.Add(model);

            // add the reverse record
            var reverseModel = new FareChart
            {
                FromStationId = model.ToStationId,
                ToStationId = model.FromStationId,
                Fare = model.Fare, // if you have a fare column
                Distance = model.Distance, // if you have a fare column
                ChartId= model.ChartId,
                EntryDate = DateTime.Now,
                EntryBy = "",
                UpdateDate = DateTime.Now,
                UpdateBy = ""
            };

            fareCharts.Add(reverseModel);

            // save both FareChart records
            _context.FareChart.AddRange(fareCharts);
            _context.SaveChanges();

            return "";
        }
    }
}
