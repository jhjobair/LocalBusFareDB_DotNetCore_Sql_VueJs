<template>
<div class="container">
    <h3>All ChartInfo</h3>
    <div v-if="message" class="alert alert-success">{{ message }}</div>

    <!-- Search Box -->
    <div class="row">
        <div class="col-md-6">
            <div class="mb-3 d-flex justify-content-start">
                <button class="btn btn-primary" @click="addChartInfo()">Add</button>
            </div>

        </div>
        <div class="col-md-6">

            <div class="mb-3 d-flex justify-content-end">
                <input type="text" class="form-control" placeholder="Search by Chart name..." v-model="searchText" style="max-width: 300px;" />
            </div>
        </div>
    </div>

    <table class="table">
        <thead>
            <tr>
                <th>Chart Name</th>
                <th>Chart Code</th>
                <th>File Path</th>
                <th>Update Date</th>
                <th>Update</th>
                <th>Delete</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="info in filteredChartInfo" :key="info.id">
                <td>{{ info.chartName }}</td>
                <td>{{ info.chartCode }}</td>
                <td> <img v-if="info.chartPath" :src="getImageUrl(info.chartPath)" :alt="info.chartName" class="chart-image-thumbnail" />
                    <span v-else class="text-muted">No Image</span></td>
                <td>{{ formatDate(info.updateDate) }}</td>
                <td>
                    <button class="btn btn-warning" @click="updateChartInfo(info.id)">
                        Update
                    </button>
                </td>
                <td>
                    <button class="btn btn-danger" @click="deleteChartInfo(info.id)">
                        Delete
                    </button>
                </td>
            </tr>
            <tr v-if="filteredChartInfo.length === 0">
                <td colspan="6" class="text-center">No chart info found.</td>
            </tr>
        </tbody>
    </table>

</div>
</template>

<script>
import ChartInfoDataService from "../../service/ChartInfoDataService";
import dayjs from "dayjs";
import Swal from "sweetalert2";

export default {
    name: "ChartAllinfo",
    data() {
        return {
            chartInfo: [],
            message: "",
            searchText: "",
            apiBaseUrl: "http://localhost:9080",
        };
    },
    computed: {
        filteredChartInfo() {
            if (!this.searchText) return this.chartInfo;
            const text = this.searchText.toLowerCase();
            return this.chartInfo.filter(
                s =>
                (s.chartName && s.chartName.toLowerCase().includes(text)) ||
                (s.chartCode && s.chartCode.toLowerCase().includes(text)) ||
                (s.chartPath && s.chartPath.toLowerCase().includes(text))
            );
        },
    },
    methods: {
        getImageUrl(relativePath) {
            if (!relativePath) return '';
            // This combines your base URL and the relative path from the database
            // Example: "http://localhost:9080" + "/images/charts/3eab8d45....png"
            return `${this.apiBaseUrl}${relativePath}`;
        },
        formatDate(date) {
            if (!date) return "";
            return dayjs(date).format("DD-MMM-YYYY hh:mm A");
        },
        refreshChartInfo() {
            ChartInfoDataService.retrieveAllChart().then((res) => {
                this.chartInfo = res.data;
            });
        },
        addChartInfo() {
            this.$router.push(`/SingleChartInfo/-1`);
        },
        updateChartInfo(id) {
            this.$router.push(`/SingleChartInfo/${id}`);
        },

        // deleteChartInfo(id) {
        //     Swal.fire({
        //         title: "Are you sure?",
        //         text: "Do you want to delete this ChartInfo?",
        //         icon: "warning",
        //         showCancelButton: true,
        //         confirmButtonColor: "#3085d6",
        //         cancelButtonColor: "#d33",
        //         confirmButtonText: "Yes, delete it!",
        //         cancelButtonText: "Cancel"
        //     }).then((result) => {
        //         if (result.isConfirmed) {
        //             ChartInfoDataService.deleteChartInfo(id).then(() => {
        //                 this.refreshChartInfo();
        //                 Swal.fire({
        //                     icon: "success",
        //                     title: "Deleted!",
        //                     text: "ChartInfo deleted successfully.",
        //                     timer: 2000,
        //                     showConfirmButton: false
        //                 });
        //             });
        //         }
        //     });
        // }
        deleteChartInfo(id) {
            Swal.fire({
                title: "Are you sure?",
                text: "Do you want to delete this ChartInfo?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, delete it!",
                cancelButtonText: "Cancel"
            }).then((result) => {
                if (result.isConfirmed) {
                    // Call your data service to delete the station
                    ChartInfoDataService.deleteChartInfo(id)
                        .then(() => {
                            // ✅ SUCCESS PATH: This block only runs if the API returns a 2xx status code.
                            this.refreshChartInfo();
                            Swal.fire({
                                icon: "success",
                                title: "Deleted!",
                                text: "ChartInfo deleted successfully.",
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
                                    text: 'The ChartInfo could not be deleted. Please try again later.',
                                });
                            }
                        });
                }
            });
        }
    },
    created() {
        this.refreshChartInfo();
    },
};
</script>

<style scoped>
.chart-image-thumbnail {
    width: 70px;
    height: 70px;
    object-fit: cover;
    /* This makes the image cover the area without stretching */
    border-radius: 5px;
    /* Optional: adds rounded corners */
}

.table td,
.table th {
    vertical-align: middle;
    /* Aligns content vertically in the middle of table cells */
}
</style>
