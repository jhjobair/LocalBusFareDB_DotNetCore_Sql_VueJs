using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Interface;

namespace WebApi.Services
{
    public class ChartInfoService : IChartInfoService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public ChartInfoService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ChartInfo> GetAll()
        {
            return _context.ChartInfo;
        }

        public ChartInfo GetById(int id)
        {
            return getChartInfo(id);
        }

        public string Create(ChartInfo model)
        {
            // validate
            if (_context.Stations.Any(x => x.Id == model.Id))
            {
                return "Stations with the id '" + model.Id + "' already exists";
            }
            if (getChartInfo(model.ChartName, model.ChartCode) != null)
            {
                return "Stations with the Name '" + model.ChartName + "' already exists";
            }
            model.EntryDate = DateTime.Now;
            model.EntryBy = "";
            model.UpdateDate = DateTime.Now;
            model.UpdateBy ="";
            // map model to new Stations object
            var chartInfo = _mapper.Map<ChartInfo>(model);

            // save Stations
            _context.ChartInfo.Add(chartInfo);
            _context.SaveChanges();
            return "";
        }

        public string Update(int id, ChartInfo model)
        {
            var chartInfo = getChartInfo(id);

            // validate
            if (model.Id != chartInfo.Id)
            {
                return "Stations with the email '" + model.Id + "' already exists";
            }
            if (getChartInfo(model.ChartName,model.ChartCode) != null)
            {
               return "Stations with the Name '" + model.ChartName + "' already exists";
            }

            chartInfo.ChartName = model.ChartName;
            chartInfo.ChartName = model.ChartCode;
            chartInfo.ChartPath = model.ChartPath;
            chartInfo.UpdateDate = DateTime.Now;

            // copy model to Stations and save
            //_mapper.Map(model, station);
            _context.ChartInfo.Update(chartInfo);
            _context.SaveChanges();
            return "";
        }

        public string Delete(int id)
        {
            var chartInfo = getChartInfo(id);
            _context.ChartInfo.Remove(chartInfo);
            _context.SaveChanges();
            return "";
        }

        // helper methods

        private ChartInfo getChartInfo(int id)
        {
            var chartInfo = _context.ChartInfo.Find(id);
            if (chartInfo == null) return null; // throw new KeyNotFoundException("Stations not found");
            return chartInfo;
        }

        private ChartInfo getChartInfo(string CHname, string CHcode)
        {
            var chartInfo = _context.ChartInfo.Where(e=>e.ChartName== CHname || e.ChartCode == CHcode).FirstOrDefault();
            if (chartInfo == null) return null; // throw new KeyNotFoundException("Stations not found");
            return chartInfo;
        }
    }
}
