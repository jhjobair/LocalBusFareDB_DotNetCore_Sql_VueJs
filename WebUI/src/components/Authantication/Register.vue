<template>
  <div class="card-container">
    <div class="card">
      <div class="card-header text-center">
        <h3>Create Account</h3>
        <p class="text-muted mb-0">Join us today</p>
      </div>
      <div class="card-body">
        <form @submit.prevent="handleRegister">
          <div class="form-group mb-4">
            <label class="form-label">Username</label>
            <input 
              v-model="form.username" 
              class="form-control form-control-lg" 
              placeholder="Choose a username"
              required 
            />
          </div>

          <div class="form-group mb-4">
            <label class="form-label">Email</label>
            <input 
              v-model="form.email" 
              type="email" 
              class="form-control form-control-lg" 
              placeholder="Enter your email"
              required 
            />
          </div>

          <div class="form-group mb-4">
            <label class="form-label">Password</label>
            <input 
              v-model="form.password" 
              type="password" 
              class="form-control form-control-lg" 
              placeholder="Create a password"
              required 
            />
          </div>

          <button type="submit" class="btn btn-glass btn-lg w-100 mb-3">
  Sign Up
</button>


          <!-- Links Section -->
          <div class="text-center">
            <p class="text-muted mb-0">
              Already have an account? 
              <router-link to="/login" class="link-info fw-bold text-decoration-none">
                Sign in here
              </router-link>
            </p>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import authService from '../../service/authService'; 

export default {
  data() {
    return {
      form: {
        username: '',
        email: '',
        password: ''
      }
    };
  },
  methods: {
    async handleRegister() {
        console.log("AuthService Object:", authService); // Add this line to debug
      try {
          const userData = {
          Username: this.form.username,
          Email: this.form.email,
          Password: this.form.password
        };
        await authService.register(userData);
        alert("Registration Successful!");
        this.$router.push('/login');
      } catch (err) {
         console.error("Full Error Object:", err);
        alert("Registration failed: " + (err.response?.data || "Error"));
      }
    }
  }
};
</script>
