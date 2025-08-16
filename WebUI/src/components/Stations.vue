<template>
    <div class="container">
      <h3>All Stations</h3>
      <div v-if="message" class="alert alert-success">{{ this.message }}</div>
      <div class="container">
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
            <tr v-for="station in stations" v-bind:key="station.id">
            
              <td>{{ station.stationNameEN }}</td>
              <td>{{ station.stationNameBN }}</td>
              <td>{{ formatDate(station.entryDate) }}</td>
              <td>{{ formatDate(station.updateDate) }}</td>
              <td>
              <button class="btn btn-warning" v-on:click="updateStation(station.id)">
                  Update
                </button>
              </td>
              <td>
               <button class="btn btn-danger" v-on:click="deleteStation(station.id)">
                  Delete
                </button>
              </td>
            </tr>
          </tbody>
        </table>
        <div class="row">
          <button class="btn btn-success" v-on:click="addStation()">Add</button>
        </div>
      </div>
    </div>
  </template>

  <script>

  import StationsDataService from "../service/StationsDataService";
  import dayjs from "dayjs";

  export default {
    name: "Stations",
    data() {
      return {
        stations: [],
        message: "",
      };
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