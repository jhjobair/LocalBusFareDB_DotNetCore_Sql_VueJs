<template>
  <div class="card-container">
    <div class="card">
      <div class="card-header">Forgot Password</div>
      <div class="card-body">
        <p>Enter your email and we'll send you a 6-digit code.</p>
        <form @submit.prevent="sendCode">
          <div class="form-group">
            <label>Email Address</label>
            <input v-model="email" type="email" class="form-control" required placeholder="Enter your email" />
          </div>
          <button type="submit" class="btn-success" :disabled="loading">
            {{ loading ? 'Sending...' : 'Send Verification Code' }}
          </button>
        </form>
        <router-link to="/login" style="display:block; margin-top:10px;">Back to Login</router-link>
      </div>
    </div>
  </div>
</template>

<script>
import authService from '@/service/authService'; // Adjust path if needed

export default {
  data() {
    return {
      email: '',
      loading: false
    }
  },
  methods: {
    async sendCode() {
      this.loading = true;
      try {
        await authService.forgotPassword(this.email);
        alert("Code sent! Check your Gmail.");
        // Redirect to reset page and pass the email via query params
        this.$router.push({ name: 'ResetPassword', query: { email: this.email } });
      } catch (err) {
        alert(err.response?.data?.message || "Error sending code");
      } finally {
        this.loading = false;
      }
    }
  }
}
</script>
