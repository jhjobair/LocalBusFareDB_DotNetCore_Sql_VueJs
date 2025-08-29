<template>
<div class="container">
    <h3>All Stations</h3>

    <!-- Search Box -->

    <div v-if="message" class="alert alert-success">{{ message }}</div>

    <div class="row">
        <div class="col-md-6">
            <div class="mb-3 d-flex justify-content-start">
                <button class="btn btn-primary" @click="addStation()">Add</button>
            </div>

        </div>
        <div class="col-md-6">
            <div class="mb-3 d-flex justify-content-end">
                <input type="text" class="form-control" placeholder="Search by station name..." v-model="searchText" style="max-width: 300px;" />
            </div>
        </div>
    </div>

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

</div>
</template>

  
<script>
import StationsDataService from "../../service/StationsDataService";
import dayjs from "dayjs";
import Swal from "sweetalert2";

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
    Swal.fire({
        title: "Are you sure?",
        text: "Do you want to delete this Station?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "Cancel"
    }).then((result) => {
        if (result.isConfirmed) {
            // Call your data service to delete the station
            StationsDataService.deleteStation(id)
                .then(() => {
                    // ✅ SUCCESS PATH: This block only runs if the API returns a 2xx status code.
                    this.refreshStations();
                    Swal.fire({
                        icon: "success",
                        title: "Deleted!",
                        text: "Station deleted successfully.",
                        timer: 2000,
                        showConfirmButton: false
                    });
                })
                .catch(err => {
                    // 🛑 ERROR PATH: This block runs for any 4xx or 5xx status code.
                    
                    // Check if the error response and data exist, and if the status is 400
                    if (err.response && err.response.status === 400 && err.response.data) {
                        // This is our specific business logic error from the backend.
                        Swal.fire({
                            icon: 'error',
                            title: 'Deletion Failed',
                            // Display the specific message sent from the API
                            text: err.response.data.message, 
                        });
                    } else {
                        // This is for all other types of errors (network, server crash, etc.)
                        console.error("An unexpected error occurred during deletion:", err);
                        Swal.fire({
                            icon: 'error',
                            title: 'An Error Occurred',
                            text: 'The station could not be deleted. Please try again later.',
                        });
                    }
                });
        }
    });
}
       
    },
    created() {
        this.refreshStations();
    },
};
</script>
