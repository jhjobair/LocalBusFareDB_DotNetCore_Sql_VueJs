<template>
  <div>
    <h3>Station</h3>
    <div class="container">
      <form @submit="validateAndSubmit">
        <div v-if="errors.length">
          <div
            class="alert alert-danger"
            v-bind:key="index"
            v-for="(error, index) in errors"
          >
            {{ error }}
          </div>
        </div>
        <fieldset class="form-group">
          <label>Station Name EN</label>
          <input type="text" class="form-control" v-model="stationNameEN" />
        </fieldset>
        <fieldset class="form-group">
          <label>Station Name BN</label>
          <input type="text" class="form-control" v-model="stationNameBN" />
        </fieldset>
        <button class="btn btn-success" type="submit">Save</button>
      </form>
    </div>
  </div>
</template>
<script>
import StationsDataService from "../../service/StationsDataService";

export default {
  name: "Station",
  data() {
    return {
      stationNameEN: "",
      stationNameBN: "",
      errors: [],
    };
  },
  computed: {
    id() {
      return this.$route.params.id;
    },
  },
  methods: {
    refreshStationDetails() {
      StationsDataService.retrieveStation(this.id).then((res) => {
        this.stationNameEN = res.data.stationNameEN;
        this.stationNameBN = res.data.stationNameBN;
      });
    },
    validateAndSubmit(e) {
      e.preventDefault();
      this.errors = [];
      if (!this.stationNameEN) {
        this.errors.push("Enter valid values");
      } else if (this.stationNameEN.length < 3) {
        this.errors.push("Enter atleast 3 characters in station Name EN");
      }
      if (!this.stationNameBN) {
        this.errors.push("Enter valid values");
      } else if (this.stationNameBN.length < 3) {
        this.errors.push("Enter atleast 3 characters in station Name BN");
      }
      if (this.errors.length === 0) {
       if (this.id == -1) {
        StationsDataService.createStation({
          stationNameEN: this.stationNameEN,
          stationNameBN: this.stationNameBN,
        }).then(() => {
          this.$router.push("/");
        }, err => {
            console.error("Error Response:", err.response); // full object
            if (err.response && err.response.data && err.response.data.message) {
              this.errors.push(err.response.data.message); // custom message from backend
            } else {
              this.errors.push("Something went wrong");
            }
        });
      } else {
        StationsDataService.updateStation(this.id, {
          id: this.id,
          stationNameEN: this.stationNameEN,
          stationNameBN: this.stationNameBN,
        }).then(() => {
          this.$router.push("/");
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
    this.refreshStationDetails();
  },
};
</script>