<template>
  <div class="card-container">
    <div class="card">
      <div class="card-header text-center">
        <h3>Welcome Back</h3>
        <p class="text-muted mb-0">Sign in to your account</p>
      </div>
      <div class="card-body">
        <form @submit.prevent="handleLogin">
          <div class="form-group mb-4">
            <label class="form-label">Username</label>
            <input 
              v-model="loginData.username" 
              class="form-control form-control-lg" 
              placeholder="Enter your username"
              required 
            />
          </div>
          
          <div class="form-group mb-4">
            <label class="form-label">Password</label>
            <input 
              v-model="loginData.password" 
              type="password" 
              class="form-control form-control-lg" 
              placeholder="Enter your password"
              required 
            />
          </div>

      <button type="submit" class="btn btn-glass btn-lg w-100 mb-3">
  Sign In
</button>


          <!-- Links Section -->
          <div class="text-center">
            <router-link to="/forgotpassword" class="link-primary text-decoration-none mb-3 d-block">
              Forgot Password?
            </router-link>
            
            <p class="text-muted mb-0">
              Don't have an account? 
              <router-link to="/register" class="link-info fw-bold text-decoration-none">
                Sign up here
              </router-link>
            </p>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import authService from '@/service/authService';

export default {
  data() {
    return {
      loginData: { username: '', password: '' }
    };
  },
  methods: {
    async handleLogin() {
      try {
         const credentials = {
          Username: this.loginData.username,
          Password: this.loginData.password
        };

        const result  = await authService.login(credentials);

       
        
        if (result.success) {
          // Success! Redirection logic
          this.$router.push('/Stations');
        } else {
          alert(result.message);
        }
      } catch (err) {
        const errorMessage = err.response?.data || err.message;
        console.error("Detailed Error:", err.response);
        alert("Login failed: " + errorMessage);
      }
    }
  }
};
</script>
