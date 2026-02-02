import axios from 'axios';

const API_URL = 'http://localhost:9080/api/v1/Report';

export default {
  /**
   * Fetches the generated RDLC report from the backend
   * @param {string} reportType - 'pdf' or 'excel'
   */
  async getFullFareChartReport(reportType = 'pdf') {
    return await axios.get(`${API_URL}/FullFareChartReport`, {
      params: { reportType: reportType },
      responseType: 'blob', // CRITICAL: This allows handling binary file data
    });
  },
  async getFullStationReport(reportType = 'pdf') {
    return await axios.get(`${API_URL}/StationsReport`, {
      params: { reportType: reportType },
      responseType: 'blob', // CRITICAL: This allows handling binary file data
    });
  }
};