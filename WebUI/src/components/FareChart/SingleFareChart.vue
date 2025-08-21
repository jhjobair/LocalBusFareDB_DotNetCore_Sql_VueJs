<template>
  <div class="card-container">
    <div class="card custom-card shadow-lg">
      <div class="card-header">
        <h3 class="mb-0">Fare Chart</h3>
      </div>
      <div class="card-body">
        <form @submit="validateAndSubmit">

          <!-- Error Messages -->
          <div v-if="errors.length" class="mb-3">
            <div v-for="(error, index) in errors" :key="index" class="alert alert-danger py-2 mb-2">
              {{ error }}
            </div>
          </div>

          <!-- Chart Name -->
          <div class="mb-3">
            <!-- <label class="form-label fw-bold">From Station</label>
            <input 
              type="text" 
              class="form-control"
              v-model="fromStationName" 
              placeholder="Enter Station"
              readonly
            /> -->
            <div><label class="typo__label">From Station</label>
              <multiselect id="single-select-search" v-model="fStation" :options="stationList" placeholder="Select one"
                label="name" track-by="name" aria-label="pick a value"></multiselect>
            </div>
          </div>

          <!-- Chart Code -->
          <div class="mb-3">
            <!-- <label class="form-label fw-bold">To Station</label>
            <input 
              type="text" 
              class="form-control"
              v-model="toStationName" 
              placeholder="Enter Station"
              readonly
            /> -->
            <div><label class="typo__label">To Station</label>
              <multiselect id="single-select-search" v-model="tStation" :options="stationList" placeholder="Select one"
                label="name" track-by="name" aria-label="pick a value"></multiselect>
            </div>
          </div>

          <!-- Chart Path -->
          <div class="mb-3">
            <label class="form-label fw-bold">Distance</label>
            <input type="text" class="form-control" v-model="distance" placeholder="Distance"  />
          </div>

          <!-- Chart Name -->
          <div class="mb-3">
            <label class="form-label fw-bold">Fare</label>
            <input type="text" class="form-control" v-model="fare" placeholder="Enter Fare"  />
          </div>

          <!-- Chart Name -->
          <div class="mb-3">
            <!-- <label class="form-label fw-bold">Chart Name</label>
            <input type="text" class="form-control" v-model="chartName" placeholder="Enter chart name" readonly /> -->

            <div><label class="typo__label">Chart</label>
              <multiselect id="single-select-search" v-model="chartInfo" :options="chartInfoList" placeholder="Select one"
                label="name" track-by="name" aria-label="pick a value"></multiselect>
            </div>
          </div>

          <!-- Chart Name -->
          <div class="mb-3">
            <label class="form-label fw-bold">Chart Code</label>
            <input type="text" class="form-control" v-model="chartInfo.id" placeholder="Enter chartCode" readonly />
          </div>
          <!-- Save & Cancel Buttons -->
          <div class="row">
            <div class="d-flex justify-content-start col col-2">
              <button class="btn btn-success px-4" type="submit">
                💾 Save
              </button>
            </div>
            <div class="d-flex justify-content-end col col-10">
              <button class="btn btn-success px-4" type="button" @click="cancelForm">
                ❌Back
              </button>
            </div>
          </div>


        </form>
      </div>
    </div>
  </div>
</template>



<!-- <script type="text/javascript" src="node_modules/vuejs/dist/vue.min.js"></script>
<script type="text/javascript" src="node_modules/vue-simple-search-dropdown/dist/vue-simple-search-dropdown.min.js"></script>
<script type="text/javascript">
  Vue.use(Dropdown);
</script> -->
<script>
import FareChartDataService from "../../service/FareChartDataService";
import StationsDataService from "../../service/StationsDataService";
import ChartInfoDataService from "../../service/ChartInfoDataService";

import Multiselect from 'vue-multiselect'

export default {
  name: "SingleFareChart",
  components: { Multiselect },
  data() {
    return {
      fromStationName: "",
      toStationName: "",
      distance: "",
      fare: "",
      chartName: "",
      chartCode: "",
      errors: [],

      fStation: { name: 'Select From Station', language: '' },
      tStation: { name: 'Select To Station', language: '' },
      stationList: [],

      chartInfo: { name: 'Select Chart', language: '', id: '' },
      chartInfoList: []

    };
  },
  computed: {
    id() {
      return this.$route.params.id;
    },
  },
  methods: {
    
    refreshFareInfo() {
      FareChartDataService.retrieveFareChart(this.id).then((res) => {
        this.fromStationName = res.data.fromStationName;
        this.toStationName = res.data.toStationName;
        this.distance = res.data.distance;
        this.fare = res.data.fare;
        this.chartName = res.data.chartName;
        this.chartCode = res.data.chartCode;
      });
    },
    cancelForm() {
      this.$router.push("/FareChart");
    },
    refreshChartInfo() {
      ChartInfoDataService.retrieveAllChart().then((res) => {
        //this.chartInfo = res.data;
        this.chartInfoList = res.data.map(item => ({
          name: item.chartName,
          language: item.id,
          id: item.chartCode
        }));
      });
    },
    refreshStations() {
      StationsDataService.retrieveAllStations().then((res) => {
        // this.stations = res.data;
        this.stationList = res.data.map(item => ({
          name: item.stationNameEN,
          language: item.id
        }));
      });
    },
    // nameWithLang ({name, language}) {
    //   return `${name} — [${language}]`
    // }
      validateAndSubmit(e) {
        e.preventDefault();
        this.errors = [];

        if (!this.fStation.language) {
          this.errors.push("Enter Select From Station.");
        } 
        if (!this.tStation.language) {
          this.errors.push("Enter Select To Station.");
        } 
        if (!this.chartInfo.language) {
          this.errors.push("Enter Select Chart Info.");
        } 
        if (!this.distance) {
          this.errors.push("Enter Distance.");
        } else if (this.distance < 0.1) {
          this.errors.push("Enter valid distance.");
        }
        if (!this.fare) {
          this.errors.push("Enter fare.");
        } else if (this.fare < 0.1) {
          this.errors.push("Enter valid fare.");
        }
        if (this.errors.length === 0) {
         if (this.id == -1) {
          FareChartDataService.createFareChartInfo({
            fromStationId:this.fStation.language,
            toStationId :this.tStation.language,
            distance: this.distance,
            fare: this.fare,
            chartId: this.chartInfo.language
          }).then(() => {
            this.$router.push("/FareChart");
          }, err => {
              console.error("Error Response:", err.response); // full object
              if (err.response && err.response.data && err.response.data.message) {
                this.errors.push(err.response.data.message); // custom message from backend
              } else {
                this.errors.push("Something went wrong");
              }
          });
        } else {
          ChartInfoDataService.updateChartInfo(this.id, {
            id: this.id,
            chartName: this.chartName,
            chartCode: this.chartCode,
             chartPath: this.chartPath,
          }).then(() => {
            this.$router.push("/FareChart");
          }, err => {
              console.error("Error Response:", err.response); // full object
              if (err.response && err.response.data && err.response.data.message) {
                this.errors.push(err.response.data.message); // custom message from backend
              } else {
                this.errors.push("Something went wrong");
            }
          });
        }
        }
      },
  },
  created() {
    this.refreshFareInfo();
    this.refreshStations();
    this.refreshChartInfo();
  },
};
</script>

<style scoped>
/* Light Theme Card */
.light .custom-card {
  background: #ffffff;
  color: #000;
  border: 1px solid #ddd;
}

.light .custom-card .card-header {
  background: #007bff;
  color: white;
}

/* Dark Theme Card */
.dark .custom-card {
  background: #1e1e1e;
  color: #f5f5f5;
  border: 1px solid #444;
}

.dark .custom-card .card-header {
  background: #333;
  color: #f5f5f5;
}

/* Input fields theme aware */
.light .form-control {
  background: #fff;
  color: #000;
  border: 1px solid #ccc;
}

.dark .form-control {
  background: #2c2c2c;
  color: #f5f5f5;
  border: 1px solid #555;
}
</style>
