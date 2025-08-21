import axios from 'axios'

const API_URL = 'http://localhost:9080/api/v1'

class FareChartDataService {

    retrieveAllFareChart() {
        return axios.get(`${API_URL}/FareChart/GetAll`);
    }

    retrieveFareChart(id) {
        return axios.get(`${API_URL}/FareChart/GetById/${id}`);
    }

    deleteFareChart(id) {
        return axios.delete(`${API_URL}/FareChart/Delete/${id}`);
    }

    updateFareChart(id, chartInfo) {
        return axios.put(`${API_URL}/FareChart/Update/${id}`, chartInfo);
    }


    createFareChartInfo(chartInfo) {
        return axios.post(`${API_URL}/FareChart/Create`, chartInfo);
    }
  }
export default new FareChartDataService()