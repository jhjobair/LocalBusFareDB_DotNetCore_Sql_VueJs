namespace WebApi.Services;

using WebApi.Entities;
using WebApi.Models.Users;

public interface IStationsService
{
    IEnumerable<Stations> GetAll();
    Stations GetById(int id);
    string Create(Stations model);
    string Update(int id, Stations model);
    string Delete(int id);
}

