namespace WebApi.Services;

using WebApi.Entities;
using WebApi.Models.Users;

public interface IStationsService
{
    IEnumerable<Stations> GetAll();
    Stations GetById(int id);
    void Create(Stations model);
    void Update(int id, Stations model);
    void Delete(int id);
}

