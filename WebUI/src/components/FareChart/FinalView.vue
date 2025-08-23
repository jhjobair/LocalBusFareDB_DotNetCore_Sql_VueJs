<template>
  <div class="final-wrap">
    <div class="final-inner">
      <div class="final-card">
        <header class="final-header">
          <h2>Fare Chart Lookup</h2>
        </header>

        <!-- From Station -->
        <section class="controls">
          <label class="label">From station</label>
          <multiselect
            v-model="fromStation"
            :options="stations"
            track-by="id"
            label="stationNameEN"
            placeholder="Search or select From Station"
          >
            <template #option="{ option }">
              {{ option.stationNameEN }} - {{ option.stationNameBN }}
            </template>
            <template #singleLabel="{ option }">
              {{ option.stationNameEN }} - {{ option.stationNameBN }}
            </template>
          </multiselect>
        </section>

        <!-- To Station -->
        <section class="controls" v-if="fromStation">
          <label class="label">To station</label>
          <multiselect
            ref="toSelect"
            v-model="toStation"
            :options="stations"
            track-by="id"
            label="stationNameEN"
            placeholder="Search or select To Station"
          >
            <template #option="{ option }">
              {{ option.stationNameEN }} - {{ option.stationNameBN }}
            </template>
            <template #singleLabel="{ option }">
              {{ option.stationNameEN }} - {{ option.stationNameBN }}
            </template>
          </multiselect>
        </section>

        <!-- Result -->
        <section class="result" v-if="fareChart">
          <div class="route">
            <span class="route-from">{{ fareChart.fromStationName }}</span>
            <span class="arrow">→</span>
            <span class="route-to">{{ fareChart.toStationName }}</span>
          </div>

          <div class="grid">
            <div class="grid-item">
              <small class="muted">Fare</small>
              <div class="value">{{ fareChart.fare }} ৳</div>
            </div>
            <div class="grid-item">
              <small class="muted">Distance</small>
              <div class="value">{{ fareChart.distance }} km</div>
            </div>
            <div class="grid-item full">
              <small class="muted">Chart</small>
              <div class="value">
                {{ fareChart.chartName }}
                <span class="muted">({{ fareChart.chartCode }})</span>
              </div>
            </div>
          </div>
        </section>

        <!-- No data -->
        <section v-else-if="fromStation && toStation && !loading" class="no-data">
          No fare chart found for this route.
        </section>

        <div class="loading" v-if="loading">Loading…</div>
      </div>
    </div>
  </div>
</template>

<script>
import fareChartDataService from "@/service/FareChartDataService";
import axios from "axios";
import Multiselect from "vue-multiselect";
import "vue-multiselect/dist/vue-multiselect.css";

export default {
  name: "FinalView",
  components: { Multiselect },
  data() {
    return {
      stations: [],
      fromStation: null,
      toStation: null,
      fareChart: null,
      loading: false,
    };
  },
  methods: {
    async loadStations() {
      try {
        const res = await axios.get("http://localhost:9080/api/v1/Stations/GetAll");
        this.stations = res.data || [];
      } catch (err) {
        console.error("loadStations:", err);
      }
    },
    async fetchFareChart() {
      if (!this.fromStation || !this.toStation) return;
      this.loading = true;
      this.fareChart = null;
      try {
        const res = await fareChartDataService.finalView(this.fromStation.id, this.toStation.id);
        this.fareChart = res?.data ?? null;
      } catch (err) {
        console.error("fetchFareChart:", err);
        this.fareChart = null;
      } finally {
        this.loading = false;
      }
    },
  },
  watch: {
    fromStation(newVal) {
      if (newVal) {
        this.$nextTick(() => {
          const input = this.$refs.toSelect.$el.querySelector("input");
        if (input) input.focus();
        });
      }
    },
    toStation(newVal) {
      if (newVal) {
        this.fetchFareChart();
      }
    },
  },
  mounted() {
    this.loadStations();
  },
};
</script>


<style>
/* Theme via CSS variables — toggled by .dark on root/body */
:root {
    --bg: #f3f6fa;
    --card: #ffffff;
    --text: #1f2937;
    --muted: #6b7280;
    --primary: #0d6efd;
    --success: #198754;
    --shadow: 0 10px 30px rgba(16, 24, 40, 0.06);
}

/* When your app adds .dark on the body or top container, variables change */
.dark {
    --bg: #0b0f12;
    --card: #0f1720;
    --text: #e6eef8;
    --muted: #9aa6b2;
    --primary: #2563eb;
    --success: #16a34a;
    --shadow: 0 10px 30px rgba(0, 0, 0, 0.6);
}

.final-wrap {
    min-height: 100vh;
    background: var(--bg);
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 32px;
}

/* container sizing */
.final-inner {
    width: 100%;
    max-width: 720px;
}

/* card */
.final-card {
    background: var(--card);
    color: var(--text);
    border-radius: 12px;
    box-shadow: var(--shadow);
    padding: 26px;
    transition: background 0.25s ease, color 0.25s ease;
}

/* header */
.final-header {
    display: flex;
    align-items: center;
    gap: 12px;
    margin-bottom: 18px;
}

.final-header h2 {
    font-size: 1.35rem;
    margin: 0;
    font-weight: 700;
}

.final-header .icon {
    width: 30px;
    height: 30px;
    fill: var(--primary);
}

/* label + select area */
.controls {
    margin-bottom: 14px;
}

.label {
    display: block;
    margin-bottom: 8px;
    color: var(--muted);
    font-weight: 600;
}

/* style native selects to look good in both themes */
.select {
    width: 100%;
    padding: 10px 12px;
    border-radius: 8px;
    border: 1px solid rgba(15, 23, 42, 0.06);
    background: linear-gradient(180deg, rgba(255, 255, 255, 0.02), rgba(0, 0, 0, 0.02)), var(--card);
    color: var(--text);
    appearance: none;
    -webkit-appearance: none;
    margin-bottom: 10px;
    transition: background 0.2s, color 0.2s;
}

/* button styling */
.btn {
    display: inline-block;
    padding: 10px 14px;
    border-radius: 8px;
    border: none;
    cursor: pointer;
    font-weight: 600;
    color: #fff;
}

.btn[disabled] {
    opacity: 0.55;
    cursor: not-allowed;
}

.btn-primary {
    background: var(--primary);
}

.btn-success {
    background: var(--success);
}

/* result card */
.result {
    margin-top: 18px;
    padding: 16px;
    border-radius: 10px;
    background: rgba(255, 255, 255, 0.02);
}

/* route row */
.route {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 10px;
    font-weight: 700;
    margin-bottom: 12px;
}

.route .arrow {
    font-size: 1.1rem;
    color: var(--muted);
}

/* grid of values */
.grid {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 12px;
    align-items: center;
}

.grid-item {
    padding: 12px;
    background: rgba(0, 0, 0, 0.03);
    border-radius: 8px;
    text-align: center;
}

.grid-item.full {
    grid-column: 1 / -1;
    background: transparent;
    text-align: center;
}

.muted {
    color: var(--muted);
}

.value {
    font-size: 1.05rem;
    font-weight: 700;
    margin-top: 6px;
}

/* no-data and loading */
.no-data {
    margin-top: 12px;
    color: var(--muted);
    text-align: center;
}

.loading {
    margin-top: 12px;
    text-align: center;
    color: var(--muted);
}

/* make select readable in dark: override option background for some browsers where needed */
.dark .select {
    border-color: rgba(255, 255, 255, 0.06);
    background: linear-gradient(180deg, rgba(255, 255, 255, 0.01), rgba(0, 0, 0, 0.06)), var(--card);
    color: var(--text);
}

/* responsive */
@media (max-width: 520px) {
    .final-inner {
        padding: 0 10px;
    }

    .grid {
        grid-template-columns: 1fr;
    }
}
</style>
