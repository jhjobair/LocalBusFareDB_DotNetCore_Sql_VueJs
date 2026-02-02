<template>
<div class="container">
    <h3>Fare Chart</h3>

    <div v-if="message" class="alert alert-success">{{ message }}</div>

    <div class="row">
        <div class="col-md-6">
            <div class="mb-3 d-flex justify-content-start">
                <button class="btn btn-primary" @click="addChartFareInfo()">Add</button>
            </div>

        </div>
        <div class="col-md-6">
            <!-- Search Box -->
            <div class="mb-3 d-flex justify-content-end">
                <input type="text" class="form-control" placeholder="Search by Station name" v-model="searchText" style="max-width: 300px;" />
            </div>
        </div>
    </div>

    <table class="table">
        <thead>
            <tr>
                <th>From Station</th>
                <th>To station</th>
                <th>Distanse</th>
                <th>Fare</th>
                <th>Chart Name</th>
                <th>Chart Code</th>
                <th>Update</th>
                <th>Delete</th>
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
                <td>
                    <button class="btn btn-warning" @click="EditFareDetails(info.id)">
                        Update
                    </button>
                </td>
                <td>
                    <button class="btn btn-danger" @click="confirmDelete(info.id)">
                        Delete
                    </button>
                </td>
                <!-- <td>
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
    <!-- <button class="btn btn-primary" @click="addChartInfo()">Add</button>  -->
<div class="report-actions">
    <button @click="downloadReport('pdf')" class="btn btn-success mr-2">
      <i class="fa fa-file-pdf"></i> Download PDF
    </button>
    
    <button @click="downloadReport('excel')" class="btn btn-success mr-2">
      <i class="fa fa-file-excel"></i> Download Excel
    </button>
  </div>
</div>

</template>

<script>
import FareChartDataService from "../../service/FareChartDataService";
import dayjs from "dayjs";
import Swal from "sweetalert2";
import reportService from "../../service/ReportDataService";

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
        EditFareDetails(id) {
            this.$router.push(`/SingleFareChart/${id}`);
        },
        addChartFareInfo() {
            this.$router.push(`/SingleFareChart/-1`);
        },
        // updateChartInfo(id) {
        //   this.$router.push(`/SingleChartInfo/${id}`);
        // },
        confirmDelete(id) {
            Swal.fire({
                title: "Are you sure?",
                text: "Do you want to delete this FareRow It also Delete reverse Row?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, delete it!",
                cancelButtonText: "Cancel"
            }).then((result) => {
                if (result.isConfirmed) {
                    FareChartDataService.deleteFareChart(id).then(() => {
                        this.refreshFareChart();
                        Swal.fire({
                            icon: "success",
                            title: "Deleted!",
                            text: "FareRow deleted successfully.",
                            timer: 2000,
                            showConfirmButton: false
                        });
                    });
                }
            });
        },
         async downloadReport(type) {
      try {
        // 1. Call the service
        const response = await reportService.getFullFareChartReport(type);

        // 2. Create a Blob from the response data
        const blob = new Blob([response.data], { 
          type: type === 'pdf' ? 'application/pdf' : 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' 
        });

        // 3. Generate a temporary URL for the file
        const url = window.URL.createObjectURL(blob);
        
        // 4. Create a hidden <a> tag to trigger download
        const link = document.createElement('a');
        link.href = url;
        
        const extension = type === 'pdf' ? 'pdf' : 'xlsx';
        link.setAttribute('download', `FareChart_Report_${new Date().getTime()}.${extension}`);
        
        // 5. Append to body, click, and remove
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        
        // Clean up the memory
        window.URL.revokeObjectURL(url);

      } catch (error) {
        console.error("Report Download Error:", error);
        alert("Failed to generate report. Please check if the backend is running.");
      }
    }

    },
    created() {
        this.refreshFareChart();
    },
};
</script>
