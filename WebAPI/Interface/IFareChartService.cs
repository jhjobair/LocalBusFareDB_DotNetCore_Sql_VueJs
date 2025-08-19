namespace WebApi.Interface;

using WebApi.Entities;
using WebApi.Models.Users;

public interface IFareChartService
{
    IEnumerable<FareChartViewModel> GetAll();
    FareChartViewModel GetById(int id);
    //string Create(ChartInfo model);
    //string Update(int id, ChartInfo model);
    //string Delete(int id);
}

