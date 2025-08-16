using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;

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

        public void Create(Stations model)
        {
            // validate
            if (_context.Stations.Any(x => x.Id == model.Id))
                throw new AppException("Stations with the email '" + model.Id + "' already exists");

            model.EntryDate = DateTime.Now;
            model.EntryBy = "";
            model.UpdateDate = DateTime.Now;
            model.UpdateBy ="";
            // map model to new Stations object
            var stations = _mapper.Map<Stations>(model);

            // save Stations
            _context.Stations.Add(stations);
            _context.SaveChanges();
        }

        public void Update(int id, Stations model)
        {
            var station = getStations(id);

            // validate
            if (model.Id != station.Id && _context.Stations.Any(x => x.Id == model.Id))
                throw new AppException("Stations with the email '" + model.Id + "' already exists");

            // copy model to Stations and save
            _mapper.Map(model, station);
            _context.Stations.Update(station);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var stations = getStations(id);
            _context.Stations.Remove(stations);
            _context.SaveChanges();
        }

        // helper methods

        private Stations getStations(int id)
        {
            var stations = _context.Stations.Find(id);
            if (stations == null) return null; // throw new KeyNotFoundException("Stations not found");
            return stations;
        }
    }
}
