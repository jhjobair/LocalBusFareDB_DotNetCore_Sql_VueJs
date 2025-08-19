<template>
  <div class="card-container">
    <div class="card custom-card shadow-lg">
      <div class="card-header">
        <h3 class="mb-0">Chart Info</h3>
      </div>
      <div class="card-body">
         <form @submit.prevent="validateAndSubmit"> 
          
          <!-- Error Messages -->
          <div v-if="errors.length" class="mb-3">
            <div
              v-for="(error, index) in errors"
              :key="index"
              class="alert alert-danger py-2 mb-2" 
            >
              {{ error }}
            </div>
          </div>

          <!-- Chart Name -->
          <div class="mb-3">
            <label class="form-label fw-bold">From Station</label>
            <input 
              type="text" 
              class="form-control"
              v-model="fromStationName" 
              placeholder="Enter Station"
              readonly
            />
          </div>

          <!-- Chart Code -->
          <div class="mb-3">
            <label class="form-label fw-bold">To Station</label>
            <input 
              type="text" 
              class="form-control"
              v-model="toStationName" 
              placeholder="Enter Station"
              readonly
            />
          </div>

          <!-- Chart Path -->
          <div class="mb-3">
            <label class="form-label fw-bold">Distance</label>
            <input 
              type="text" 
              class="form-control"
              v-model="distance" 
              placeholder="Distance"
              readonly
            />
          </div>

           <!-- Chart Name -->
          <div class="mb-3">
            <label class="form-label fw-bold">Fare</label>
            <input 
              type="text" 
              class="form-control"
              v-model="fare" 
              placeholder="Enter Fare"
              readonly
            />
          </div>

           <!-- Chart Name -->
          <div class="mb-3">
            <label class="form-label fw-bold">Chart Name</label>
            <input 
              type="text" 
              class="form-control"
              v-model="chartName" 
              placeholder="Enter chart name"
              readonly
            />
          </div>

           <!-- Chart Name -->
          <div class="mb-3">
            <label class="form-label fw-bold">Chart Code</label>
            <input 
              type="text" 
              class="form-control"
              v-model="chartCode" 
              placeholder="Enter chartCode"
              readonly
            />
          </div>
        <!-- Save & Cancel Buttons -->
         <div class="row">
        <div class="d-flex justify-content-start col col-2">
          <!-- <button class="btn btn-success px-4" type="submit">
            💾 Save
          </button> -->
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




<script>
import FareChartDataService from "../../service/FareChartDataService";

export default {
  name: "SingleFareChart",
  data() {
    return {
      fromStationName: "",
      toStationName: "",
      distance: "",
      fare: "",
      chartName: "",
      chartCode: "",
      errors: [],
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
  //   validateAndSubmit(e) {
  //     e.preventDefault();
  //     this.errors = [];
  //     if (!this.chartName) {
  //       this.errors.push("Enter valid chartName");
  //     } else if (this.chartName.length < 2) {
  //       this.errors.push("Enter atleast 2 characters in ChartName");
  //     }
  //     if (!this.chartCode) {
  //       this.errors.push("Enter valid chartCode");
  //     } else if (this.chartCode.length < 2) {
  //       this.errors.push("Enter atleast 2 characters in ChartCode");
  //     }
  //     if (!this.chartPath) {
  //       this.errors.push("Enter valid chartPath");
  //     } else if (this.chartPath.length < 2) {
  //       this.errors.push("Enter atleast 2 characters in chartPath");
  //     }
  //     if (this.errors.length === 0) {
  //      if (this.id == -1) {
  //       ChartInfoDataService.createChartInfo({
  //         chartName: this.chartName,
  //         chartCode: this.chartCode,
  //         chartPath: this.chartPath,
  //       }).then(() => {
  //         this.$router.push("/ChartAllinfo");
  //       }, err => {
  //           console.error("Error Response:", err.response); // full object
  //           if (err.response && err.response.data && err.response.data.message) {
  //             this.errors.push(err.response.data.message); // custom message from backend
  //           } else {
  //             this.errors.push("Something went wrong");
  //           }
  //       });
  //     } else {
  //       ChartInfoDataService.updateChartInfo(this.id, {
  //         id: this.id,
  //         chartName: this.chartName,
  //         chartCode: this.chartCode,
  //          chartPath: this.chartPath,
  //       }).then(() => {
  //         this.$router.push("/ChartAllinfo");
  //       }, err => {
  //           console.error("Error Response:", err.response); // full object
  //           if (err.response && err.response.data && err.response.data.message) {
  //             this.errors.push(err.response.data.message); // custom message from backend
  //           } else {
  //             this.errors.push("Something went wrong");
  //         }
  //       });
  //     }
  //     }
  //   },
  },
  created() {
    this.refreshFareInfo();
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