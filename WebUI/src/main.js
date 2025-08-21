import { createApp } from 'vue'
import App from './App.vue'
import 'bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'
import router from './router'

createApp(App).use(router).mount('#app')

// import { createApp } from 'vue';
// import App from './App.vue';
// import 'bootstrap';
// import 'bootstrap/dist/css/bootstrap.min.css';
// import router from './router';
// import Swal from 'sweetalert2';

// const app = createApp(App);

// // ✅ make $swal available globally
// app.config.globalProperties.$swal = Swal;

// app.use(router).mount('#app');
