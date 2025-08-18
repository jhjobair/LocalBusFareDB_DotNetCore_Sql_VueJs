import axios from 'axios'

const API_URL = 'http://localhost:9080/api/v1'

class FareChartDataService {

    retrieveAllFareChart() {
        return axios.get(`${API_URL}/FareChart/GetAll`);
    }

    // retrieveChartInfo(id) {
    //     return axios.get(`${API_URL}/ChartInfo/GetById/${id}`);
    // }

    // deleteChartInfo(id) {
    //     return axios.delete(`${API_URL}/ChartInfo/Delete/${id}`);
    // }

    // updateChartInfo(id, chartInfo) {
    //     return axios.put(`${API_URL}/ChartInfo/Update/${id}`, chartInfo);
    // }


    // createChartInfo(chartInfo) {
    //     return axios.post(`${API_URL}/ChartInfo/Create`, chartInfo);
    // }
  }
export default new FareChartDataService()