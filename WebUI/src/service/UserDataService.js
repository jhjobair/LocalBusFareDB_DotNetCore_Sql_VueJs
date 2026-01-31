import axios from 'axios'

const API_URL = 'http://localhost:9080/api/v1'

class UserDataService {

    retrieveAllUsers() {
        return axios.get(`${API_URL}/users`);
    }

    retrieveUser(id) {
        return axios.get(`${API_URL}/users/${id}`);
    }

    deleteUser(id) {
        return axios.delete(`${API_URL}/users/${id}`);
    }

    updateUser(id, user) {
        return axios.put(`${API_URL}/users/${id}`, user);
    }
    createUser(user) {
        return axios.post(`${API_URL}/users`, user);
    }
    reportUser() {
             return axios.get("http://localhost:9080/api/v1/report/RDLCReport", {
        params: {
          reportType: "pdf",
          fromStationId: 10,
          toStationId: 13
        }
      })
      .then(response => {
        console.log(response.data);
      })
      .catch(error => {
        console.error(error);
      });
    }
  }
export default new UserDataService()