<template>
  <div :class="$root.theme" class="card-container">
    <div class="card">
      <div class="card-header">
        <h3>🚉 Station</h3>
      </div>
      <div class="card-body">
        <form @submit="validateAndSubmit">
          <!-- Error Messages -->
          <div v-if="errors.length">
            <div
              class="alert alert-danger"
              v-for="(error, index) in errors"
              :key="index"
            >
              {{ error }}
            </div>
          </div>

          <!-- Station Name EN -->
          <div class="form-group">
            <label>Station Name (EN)</label>
            <input
              type="text"
              class="form-control"
              v-model="stationNameEN"
              placeholder="Enter English Name"
            />
          </div>

          <!-- Station Name BN -->
          <div class="form-group">
            <label>Station Name (BN)</label>
            <input
              type="text"
              class="form-control"
              v-model="stationNameBN"
              placeholder="বাংলা নাম লিখুন"
            />
          </div>

            <div class="row">
              <div class="d-flex justify-content-start col col-2">
                <button class="btn btn-success px-4" type="submit">
                  💾 Save
                </button>
                </div>
                <div class="d-flex justify-content-end col col-10">
                <button class="btn btn-success px-4" type="button" @click="cancelForm">
                  ❌ Cancel
                </button>
                </div>
            </div>
        </form>
      </div>
    </div>
  </div>
</template>



<script>
import StationsDataService from "../../service/StationsDataService";
import Swal from "sweetalert2";

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
        cancelForm() {
    this.$router.push("/Stations"); 
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
          this.$router.push("/Stations");
        }, err => {
            console.error("Error Response:", err.response); // full object
            if (err.response && err.response.data && err.response.data.message) {
              this.errors.push(err.response.data.message); // custom message from backend
            } else {
              this.errors.push("Something went wrong");
            }
        });
      } else {
        // StationsDataService.updateStation(this.id, {
        //   id: this.id,
        //   stationNameEN: this.stationNameEN,
        //   stationNameBN: this.stationNameBN,
        // }).then(() => {
        //   this.$router.push("/Stations");
        // }, err => {
        //     console.error("Error Response:", err.response); // full object
        //     if (err.response && err.response.data && err.response.data.message) {
        //       this.errors.push(err.response.data.message); // custom message from backend
        //     } else {
        //       this.errors.push("Something went wrong");
        //   }
        // });

        // 🟢 First ask confirmation
      Swal.fire({
        title: "Are you sure?",
        text: "Do you want to update this Station?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, update it!"
      }).then((result) => {
        if (result.isConfirmed) {
          // 🟢 Call API if user confirmed
          StationsDataService.updateStation(this.id, {
            id: this.id,
            stationNameEN: this.stationNameEN,
            stationNameBN: this.stationNameBN,
          })
            .then(() => {
              Swal.fire({
                icon: "success",
                title: "Updated!",
                text: "Station updated successfully.",
                timer: 2000,
                showConfirmButton: false,
              }).then(() => {
                this.$router.push("/Stations"); // redirect
              });
            })
            .catch((err) => {
              console.error("Error Response:", err.response);
              let message =
                err.response && err.response.data && err.response.data.message
                  ? err.response.data.message
                  : "Something went wrong";
              Swal.fire({
                icon: "error",
                title: "Update Failed",
                text: message,
              });
            });
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

