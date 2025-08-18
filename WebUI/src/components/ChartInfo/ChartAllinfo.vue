<template>
  <div class="container">
    <h3>All ChartInfo</h3>

    <!-- Search Box -->
    <div class="mb-3 d-flex justify-content-end">
      <input 
        type="text" 
        class="form-control" 
        placeholder="Search by Chart name..." 
        v-model="searchText" 
        style="max-width: 300px;"
      />
    </div>

    <div v-if="message" class="alert alert-success">{{ message }}</div>

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
          <td>{{ info.chartPath }}</td>
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

    <div class="row">
      <button class="btn btn-success" @click="addChartInfo()">Add</button>
    </div>
  </div>
</template>

<script>
import ChartInfoDataService from "../../service/ChartInfoDataService";
import dayjs from "dayjs";

export default {
  name: "ChartAllinfo",
  data() {
    return {
      chartInfo: [],
      message: "",
      searchText: "",
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
    deleteChartInfo(id) {
      ChartInfoDataService.deleteChartInfo(id).then(() => {
        this.refreshChartInfo();
      });
    },
  },
  created() {
    this.refreshChartInfo();
  },
};
</script>
