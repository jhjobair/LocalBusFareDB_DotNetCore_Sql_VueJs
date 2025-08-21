using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Interface;

namespace WebApi.Services
{
    public class StationsService : IStationsService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public StationsService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Stations> GetAll()
        {
            return _context.Stations;
        }

        public Stations GetById(int id)
        {
            return getStations(id);
        }

        public string Create(Stations model)
        {
            // validate
            if (_context.Stations.Any(x => x.Id == model.Id))
            {
                return "Stations with the id '" + model.Id + "' already exists";
            }
            if (getStations(model.StationNameEN, model.StationNameBN) != null)
            {
                return "Stations with the Name '" + model.StationNameEN + "' already exists";
            }
            model.EntryDate = DateTime.Now;
            model.EntryBy = "";
            model.UpdateDate = DateTime.Now;
            model.UpdateBy ="";
            // map model to new Stations object
            var stations = _mapper.Map<Stations>(model);

            // save Stations
            _context.Stations.Add(stations);
            _context.SaveChanges();
            return "";
        }

        public string Update(int id, Stations model)
        {
            var station = getStations(id);

            // validate
            if (model.Id != station.Id)
            {
                return "Stations with the email '" + model.Id + "' already exists";
            }
            if (getStations(model.StationNameEN,model.StationNameBN) != null)
            {
               return "Stations with the Name '" + model.StationNameEN + "' already exists";
            }

            station.StationNameEN = model.StationNameEN;
            station.StationNameBN = model.StationNameBN;
            station.UpdateDate = DateTime.Now;

            // copy model to Stations and save
            //_mapper.Map(model, station);
            _context.Stations.Update(station);
            _context.SaveChanges();
            return "";
        }

        public string Delete(int id)
        {
            var stations = getStations(id);
            _context.Stations.Remove(stations);
            _context.SaveChanges();
            return "Station Deleted Successfully.";
        }

        // helper methods

        private Stations getStations(int id)
        {
            var stations = _context.Stations.Find(id);
            if (stations == null) return null; // throw new KeyNotFoundException("Stations not found");
            return stations;
        }

        private Stations getStations(string name, string namebn)
        {
            var stations = _context.Stations.Where(e=>e.StationNameEN==name || e.StationNameBN == namebn).FirstOrDefault();
            if (stations == null) return null; // throw new KeyNotFoundException("Stations not found");
            return stations;
        }




       
    }
}
