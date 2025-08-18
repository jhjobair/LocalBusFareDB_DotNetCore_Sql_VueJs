<template>
  <div :class="theme" class="app-container">
    <!-- Top Navbar -->
    <div class="navbar-header">
      <a class="navbar-brand" href="#">
        Local Bus Fare Chart
      </a>
     <button @click="toggleTheme" class="theme-toggle-btn">
      <span v-if="theme === 'light'">🌙</span>
      <span v-else>☀️</span>
    </button>

    </div>

    <div class="layout">
      <!-- Sidebar -->
      <aside class="sidebar">
        <ul>
          <li><router-link to="/">Dashboard</router-link></li>
          <li><router-link to="/Stations">Stations</router-link></li>
          <li><router-link to="/ChartAllinfo">Chart info</router-link></li>
          <!-- <li><router-link to="/settings">Settings</router-link></li> -->
        </ul>
      </aside>

      <!-- Main Content -->
      <main class="content">
        <router-view />
      </main>
    </div>
  </div>
</template>

<script>
import { ref, onMounted } from "vue";

export default {
  name: "App",
  setup() {
    const theme = ref("light");

    onMounted(() => {
      theme.value = localStorage.getItem("theme") || "light";
      document.body.className = theme.value;
    });

    const toggleTheme = () => {
      theme.value = theme.value === "light" ? "dark" : "light";
      document.body.className = theme.value;
      localStorage.setItem("theme", theme.value);
    };

    return { theme, toggleTheme };
  },
};
</script>

<style>
/* General app container */
.app-container {
  min-height: 100vh;
  transition: all 0.3s ease-in-out;
}

/* Layout structure */
.layout {
  display: flex;
}

/* Sidebar */
.sidebar {
  width: 220px;
  min-height: calc(100vh - 60px); /* minus navbar height */
  padding: 15px;
  transition: all 0.3s;
}

.sidebar ul {
  list-style: none;
  padding: 0;
}

.sidebar li {
  margin: 12px 0;
}

.sidebar a {
  text-decoration: none;
  font-weight: bold;
  display: block;
  padding: 8px 12px;
  border-radius: 6px;
}

.sidebar a:hover {
  background: #007bff;
  color: #fff;
}

/* Main content */
.content {
  flex-grow: 1;
  padding: 20px;
}

/* 🔆 Light mode */
.light {
  background-color: #ffffff;
  color: #000000;
}

.light table {
  background-color: #ffffff;
  color: #000000;
  border: 1px solid #ccc;
}
.light th, .light td {
  border: 1px solid #ccc;
  padding: 8px;
}

.light .sidebar {
  background-color: #f9f9f9;
}

.light .sidebar a {
  color: #000;
}

/* 🌙 Dark mode */
.dark {
  background-color: #121212;
  color: #f5f5f5;
}
.dark table {
  background-color: #1e1e1e;
  color: #f5f5f5;
  border: 1px solid #444;
}

.dark th, .dark td {
  border: 1px solid #444;
  padding: 8px;
}
.dark .sidebar {
  background-color: #1e1e1e;
}

.dark .sidebar a {
  color: #f5f5f5;
}

.dark .sidebar a:hover {
  background: #0056b3;
}

/* Navbar */
.navbar-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px 20px;
}

/* Button */
button {
  background: #007bff;
  color: white;
  padding: 8px 16px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  transition: 0.2s;
}
button:hover {
  background: #0056b3;
}


/* Inputs and buttons */
.light input, .light select, .light textarea {
  background-color: #fff;
  color: #000;
  border: 1px solid #ccc;
}

.dark input, .dark select, .dark textarea {
  background-color: #2c2c2c;
  color: #f5f5f5;
  border: 1px solid #555;
}

/* Light mode active link */
.light .sidebar a.router-link-active {
  background-color: #007bff;
  color: white;
}

/* Dark mode active link */
.dark .sidebar a.router-link-active {
  background-color: #0056b3;
  color: white;
}

.theme-toggle-btn {
  background: transparent;
  border: none;
  cursor: pointer;
  font-size: 20px;
  transition: transform 0.2s;
  color: inherit; /* matches text color of navbar */
}

.theme-toggle-btn:hover {
  background: transparent;
  transform: scale(1.2);
}

</style>

