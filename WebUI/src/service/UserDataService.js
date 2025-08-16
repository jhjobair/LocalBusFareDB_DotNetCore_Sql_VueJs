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
  }
export default new UserDataService()