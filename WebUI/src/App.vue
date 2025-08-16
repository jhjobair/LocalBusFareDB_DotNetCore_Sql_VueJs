<!-- <template>
  <div class="container">
    <div class="navbar-header">
      <a class="navbar-brand" href="#"
        >.Net 6 + Vue.js 3 CRUD Application</a
      ><br /><br />
    </div>
    <router-view />
  </div>
</template>

<script>
export default {
  name: "app",
};
</script> -->

<template>
  <div :class="theme" class="app-container">
    <div class="navbar-header">
      <a class="navbar-brand" href="#">
        .NET 6 + Vue.js 3 CRUD Application
      </a>
      <button @click="toggleTheme">
        Switch to {{ theme === "dark" ? "Light" : "Dark" }} Mode
      </button>
    </div>

    <router-view />
  </div>
</template>

<script>
import { ref, onMounted } from "vue";

export default {
  name: "App",
  setup() {
    const theme = ref("light");

    onMounted(() => {
      // load saved theme
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
  padding: 20px;
  transition: all 0.3s ease-in-out;
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


/* Navbar style */
.navbar-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

/* Button style */
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
</style>
