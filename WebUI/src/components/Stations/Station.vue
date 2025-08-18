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

          <!-- Save Button -->
          <div class="form-actions">
            <button class="btn btn-success" type="submit">
              💾 Save
            </button>
          </div>
        </form>
      </div>
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
        StationsDataService.updateStation(this.id, {
          id: this.id,
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
      }
      }
    },
  },
  created() {
    this.refreshStationDetails();
  },
};
</script>

<style>
/* Card Container */
.card-container {
  display: flex;
  justify-content: center;
  margin-top: 30px;
}

/* Card Style */
.light .card {
  width: 450px;
  background: #ffffff;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  overflow: hidden;
  transition: all 0.3s ease-in-out;
  color: #000;
}

.dark .card {
  width: 450px;
  background: #1e1e1e;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(255, 255, 255, 0.1);
  overflow: hidden;
  transition: all 0.3s ease-in-out;
  color: #f5f5f5;
}

.card:hover {
  transform: translateY(-3px);
}

/* Card Header */
.light .card-header {
  background: #007bff;
  color: white;
  padding: 15px;
  text-align: center;
  font-weight: bold;
}

.dark .card-header {
  background: #333;
  color: #f5f5f5;
  padding: 15px;
  text-align: center;
  font-weight: bold;
}

/* Card Body */
.card-body {
  padding: 20px;
}

/* Form Groups */
.form-group {
  margin-bottom: 15px;
}

.form-group label {
  font-weight: 600;
  margin-bottom: 6px;
  display: block;
}

/* Inputs */
.light .form-control {
  width: 100%;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 6px;
  background: #fff;
  color: #000;
}

.dark .form-control {
  width: 100%;
  padding: 10px;
  border: 1px solid #555;
  border-radius: 6px;
  background: #2c2c2c;
  color: #f5f5f5;
}

/* Button */
.form-actions {
  text-align: right;
}

.light .btn-success {
  background: #28a745;
  border: none;
  padding: 10px 20px;
  border-radius: 6px;
  color: white;
  font-weight: bold;
  cursor: pointer;
  transition: background 0.2s;
}

.light .btn-success:hover {
  background: #218838;
}

.dark .btn-success {
  background: #0d6efd;
  border: none;
  padding: 10px 20px;
  border-radius: 6px;
  color: white;
  font-weight: bold;
  cursor: pointer;
  transition: background 0.2s;
}

.dark .btn-success:hover {
  background: #0b5ed7;
}

/* Alerts */
.light .alert-danger {
  background-color: #f8d7da;
  color: #842029;
  padding: 10px;
  border-radius: 6px;
  margin-bottom: 10px;
}

.dark .alert-danger {
  background-color: #842029;
  color: #f8d7da;
  padding: 10px;
  border-radius: 6px;
  margin-bottom: 10px;
}

</style>