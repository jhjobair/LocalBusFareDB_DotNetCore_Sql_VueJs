import axios from 'axios';

const API_URL = 'http://localhost:9080/api/Auth';

export default {
  // POST /api/Auth/register
  async register(userData) {
    return await axios.post(`${API_URL}/register`, userData);
  },

  // POST /api/Auth/login
   async login(credentials) {
    // credentials = { Username: '...', Password: '...' }
    const response = await axios.post(`${API_URL}/login`, credentials);
    
    // We handle the different token structures here once
    const data = response.data;
    const token = data.accessToken || data.token || (typeof data === 'string' ? data : null);

    if (token) {
      localStorage.setItem('userToken', token);
      return { success: true, token };
    }
    
    return { success: false, message: "Token not found in response" };
  },

  // POST /api/Auth/refresh-token
  async refreshToken() {
    const refreshToken = localStorage.getItem('refreshToken');
    const response = await axios.post(`${API_URL}/refresh-token`, { refreshToken });
    if (response.data.token) {
      localStorage.setItem('userToken', response.data.token);
    }
    return response.data;
  },
  async forgotPassword(email) {
    // Backend expects { email: "..." }
    return await axios.post(`${API_URL}/forgot-password`, { email });
  },

  async resetPassword(resetData) {
    // Backend ResetPasswordRequest (Identity Data) expects:
    // { email, resetCode, newPassword }
    return await axios.post(`${API_URL}/reset-password`, resetData);
  },
  logout() {
    localStorage.removeItem('userToken');
    localStorage.removeItem('refreshToken');
  }
};