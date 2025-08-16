<template>
    <div class="container">
    <h3>All Stations</h3>

    <!-- Search Box -->
    <div class="mb-3 d-flex justify-content-end">
      <input 
        type="text" 
        class="form-control" 
        placeholder="Search by station name..." 
        v-model="searchText" 
        style="max-width: 300px;"
      />
    </div>

    <div v-if="message" class="alert alert-success">{{ message }}</div>

    <table class="table">
      <thead>
        <tr>
          <th>Stations Name English</th>
          <th>Stations Name Bangla</th>
          <th>Entry Date</th>
          <th>Update Date</th>
          <th>Update</th>
          <th>Delete</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="station in filteredStations" :key="station.id">
          <td>{{ station.stationNameEN }}</td>
          <td>{{ station.stationNameBN }}</td>
          <td>{{ formatDate(station.entryDate) }}</td>
          <td>{{ formatDate(station.updateDate) }}</td>
          <td>
            <button class="btn btn-warning" @click="updateStation(station.id)">
              Update
            </button>
          </td>
          <td>
            <button class="btn btn-danger" @click="deleteStation(station.id)">
              Delete
            </button>
          </td>
        </tr>
        <tr v-if="filteredStations.length === 0">
          <td colspan="6" class="text-center">No stations found.</td>
        </tr>
      </tbody>
    </table>

    <div class="row">
      <button class="btn btn-success" @click="addStation()">Add</button>
    </div>
</div>

  </template>

  <script>

  import StationsDataService from "../../service/StationsDataService";
  import dayjs from "dayjs";

  export default {
  name: "Stations",
  data() {
    return {
      stations: [],
      message: "",
      searchText: "", // <-- search text
    };
  },
  computed: {
    filteredStations() {
      if (!this.searchText) return this.stations;
      const text = this.searchText.toLowerCase();
      return this.stations.filter(
        s =>
          (s.stationNameEN && s.stationNameEN.toLowerCase().includes(text)) ||
          (s.stationNameBN && s.stationNameBN.toLowerCase().includes(text))
      );
    },
  },
  methods: {
    formatDate(date) {
      if (!date) return "";
      return dayjs(date).format("DD-MMM-YYYY hh:mm A");
    },
    refreshStations() {
      StationsDataService.retrieveAllStations().then((res) => {
        this.stations = res.data;
      });
    },
    addStation() {
      this.$router.push(`/station/-1`);
    },
    updateStation(id) {
      this.$router.push(`/station/${id}`);
    },
    deleteStation(id) {
      StationsDataService.deleteStation(id).then(() => {
        this.refreshStations();
      });
    },
  },
  created() {
    this.refreshStations();
  },
};

  </script>