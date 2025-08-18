<template>
  <div class="container">
    <h3>Fare Chart</h3>

    <!-- Search Box -->
    <div class="mb-3 d-flex justify-content-end">
      <input 
        type="text" 
        class="form-control" 
        placeholder="Search by Station name" 
        v-model="searchText" 
        style="max-width: 300px;"
      />
    </div>

    <div v-if="message" class="alert alert-success">{{ message }}</div>

    <table class="table">
      <thead>
        <tr>
          <th>From Station</th>
          <th>To station</th>
          <th>Distanse</th>
          <th>Fare</th>
          <th>Chart Name</th>
          <th>Chart Code</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="info in filteredFareChart" :key="info.id">
          <td>{{ info.fromStationName }}</td>
          <td>{{ info.toStationName }}</td>
          <td>{{ info.distance }}</td>
          <td>{{ info.fare }}</td>
          <td>{{ info.chartName }}</td>
          <td>{{ info.chartCode }}</td>
          <!-- <td>
            <button class="btn btn-warning" @click="updateChartInfo(info.id)">
              Update
            </button>
          </td>
          <td>
            <button class="btn btn-danger" @click="deleteChartInfo(info.id)">
              Delete
            </button>
          </td> -->
        </tr>
        <tr v-if="filteredFareChart.length === 0">
          <td colspan="6" class="text-center">No chart info found.</td>
        </tr>
      </tbody>
    </table>

    <div class="row">
      <button class="btn btn-success" @click="addChartInfo()">Add</button>
    </div>
  </div>
</template>

<script>
import FareChartDataService from "../../service/FareChartDataService";
import dayjs from "dayjs";

export default {
  name: "FareChart",
  data() {
    return {
      fareChart: [],
      message: "",
      searchText: "",
    };
  },
  computed: {
filteredFareChart() {
  if (!this.searchText) return this.fareChart;
  const text = this.searchText.toLowerCase();
  return this.fareChart.filter(
    s =>
      (s.fromStationName && s.fromStationName.toLowerCase().includes(text)) ||
      (s.toStationName && s.toStationName.toLowerCase().includes(text)) ||
      (s.distance && s.distance.toString().includes(this.searchText)) ||
      (s.fare && s.fare.toString().includes(this.searchText)) ||
      (s.chartName && s.chartName.toLowerCase().includes(text)) ||
      (s.chartCode && s.chartCode.toLowerCase().includes(text))
  );
},
  },
  methods: {
    formatDate(date) {
      if (!date) return "";
      return dayjs(date).format("DD-MMM-YYYY hh:mm A");
    },
    refreshFareChart() {
      FareChartDataService.retrieveAllFareChart().then((res) => {
        this.fareChart = res.data;
      });
    },
    // addChartInfo() {
    //   this.$router.push(`/SingleChartInfo/-1`);
    // },
    // updateChartInfo(id) {
    //   this.$router.push(`/SingleChartInfo/${id}`);
    // },
    // deleteChartInfo(id) {
    //   ChartInfoDataService.deleteChartInfo(id).then(() => {
    //     this.refreshChartInfo();
    //   });
    // },
  },
  created() {
    this.refreshFareChart();
  },
};
</script>
