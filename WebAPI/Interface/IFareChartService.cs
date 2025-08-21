namespace WebApi.Interface;

using WebApi.Entities;
using WebApi.Models.Users;

public interface IFareChartService
{
    IEnumerable<FareChartViewModel> GetAll();
    FareChartViewModel GetById(int id);
    string Create(FareChart model);
    string Update(int id, FareChart model);
    string Delete(int id);
}

