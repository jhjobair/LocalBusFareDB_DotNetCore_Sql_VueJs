import axios from 'axios'

const API_URL = 'http://localhost:9080/api/v1'

class StationsDataService {

    retrieveAllStations() {
        return axios.get(`${API_URL}/Stations/GetAll`);
    }

    retrieveStation(id) {
        return axios.get(`${API_URL}/Stations/GetById/${id}`);
    }

    deleteStation(id) {
        return axios.delete(`${API_URL}/Stations/Delete/${id}`);
    }

    updateStation(id, station) {
        return axios.put(`${API_URL}/Stations/Update/${id}`, station);
    }


    createStation(station) {
        return axios.post(`${API_URL}/Stations/Create`, station);
    }
  }
export default new StationsDataService()