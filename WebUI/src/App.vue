<template>
  <div :class="theme" class="app-container">
    <!-- Top Navbar -->
    <div class="navbar-header">
      <a class="navbar-brand" href="#">
        Local Bus Fare Chart
      </a>
      <div class="navbar-actions">
        <!-- 🔓 Show Logout only if logged in -->
        <button v-if="isLoggedIn" @click="handleLogout" class="logout-btn">
          Logout 🚪
        </button>

        <button @click="toggleTheme" class="theme-toggle-btn">
          <span v-if="theme === 'light'">🌙</span>
          <span v-else>☀️</span>
        </button>
      </div>
    </div>

    <div class="layout">
      <!-- ⬅️ Sidebar: Only show if logged in -->
      <aside v-if="isLoggedIn" class="sidebar">
        <ul>
          <li><router-link to="/">Dashboard</router-link></li>
          <li><router-link to="/Stations">Stations</router-link></li>
          <li><router-link to="/ChartAllinfo">Chart info</router-link></li>
          <li><router-link to="/farechart">Fare Chart</router-link></li>
          <li><router-link to="/farechart/final">FinalView</router-link></li>
        </ul>
      </aside>

      <!-- ➡️ Main Content -->
      <!-- If logged out, this will take up full width for Login/Register pages -->
      <main class="content" :class="{ 'full-width': !isLoggedIn }">
        <router-view />
      </main>
    </div>
  </div>
</template>

<script>
import { ref, onMounted, watch } from "vue";
import { useRouter, useRoute } from "vue-router";
import "vue-multiselect/dist/vue-multiselect.css";

export default {
  name: "App",
  setup() {
    const theme = ref("light");
    const isLoggedIn = ref(false);
    const router = useRouter();
    const route = useRoute();

    // Function to check if user is logged in
    const checkAuth = () => {
      isLoggedIn.value = !!localStorage.getItem("userToken");
    };

    onMounted(() => {
      theme.value = localStorage.getItem("theme") || "light";
      document.body.className = theme.value;
      checkAuth();
    });

    // Watch the route: every time the page changes, re-check if we are logged in
    // This ensures the sidebar appears/disappears immediately after login/logout
    watch(() => route.path, () => {
      checkAuth();
    });

    const toggleTheme = () => {
      theme.value = theme.value === "light" ? "dark" : "light";
      document.body.className = theme.value;
      localStorage.setItem("theme", theme.value);
    };

    const handleLogout = () => {
      localStorage.removeItem("userToken");
      isLoggedIn.value = false;
      router.push("/login");
    };

    return { 
      theme, 
      toggleTheme, 
      isLoggedIn, 
      handleLogout 
    };
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
  color: rgb(44, 16, 16);
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
.navbar-actions {
  display: flex;
  align-items: center;
  gap: 15px;
}

.logout-btn {
  background: #dc3545; /* Red color for logout */
  font-size: 14px;
}

.logout-btn:hover {
  background: #c82333;
}

/* Make content full width when sidebar is hidden */
.full-width {
  width: 100%;
}

.btn-glass {
  /* Light mode - white glass */
  background: rgba(90, 132, 196, 0.25);
  backdrop-filter: blur(10px);
  border: 1px solid rgba(255, 255, 255, 0.18);
  box-shadow: 
    0 8px 32px rgba(31, 38, 135, 0.37),
    inset 0 1px 0 rgba(255, 255, 255, 0.3);
  color: #000;
  font-weight: 500;
}

.btn-glass:hover {
  background: rgba(255, 255, 255, 0.35);
  transform: translateY(-2px);
  box-shadow: 
    0 12px 40px rgba(31, 38, 135, 0.45),
    inset 0 1px 0 rgba(255, 255, 255, 0.4);
}

/* Dark mode - dark glass with white text */
.dark .btn-glass {
  background: rgba(115, 152, 228, 0.8);
  border: 1px solid rgba(255, 255, 255, 0.1);
  box-shadow: 
    0 8px 32px rgba(0, 0, 0, 0.4),
    inset 0 1px 0 rgba(255, 255, 255, 0.1);
  color: #fff !important;
  text-shadow: 0 1px 2px rgba(0, 0, 0, 0.3);
}

.dark .btn-glass:hover {
  background: rgba(4, 101, 56, 0.6);
  box-shadow: 
    0 12px 40px rgba(0, 0, 0, 0.5),
    inset 0 1px 0 rgba(255, 255, 255, 0.15);
  transform: translateY(-2px);
}
</style>

