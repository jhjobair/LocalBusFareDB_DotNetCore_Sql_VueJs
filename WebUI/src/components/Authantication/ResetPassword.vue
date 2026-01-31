<template>
  <div class="card-container">
    <div class="card">
      <div class="card-header">Reset Password</div>
      <div class="card-body">
        <form @submit.prevent="handleReset">
          <!-- <div class="form-group">
            <label>Email</label>
            <input v-model="resetData.email" class="form-control" readonly />
          </div> -->
          <div class="form-group">
            <label>Verification Code (from Email)</label>
            <input v-model="resetData.resetCode" class="form-control" placeholder="6-digit code" required />
          </div>
          <div class="form-group">
            <label>New Password</label>
            <input v-model="resetData.newPassword" type="password" class="form-control" placeholder="Min 6 characters" required />
          </div>
          <button type="submit" class="btn-success">Update Password</button>
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
      resetData: {
        email: this.$route.query.email || '', // Get email from URL if available
        resetCode: '',
        newPassword: ''
      }
    };
  },
  methods: {
    async handleReset() {
      try {
        await authService.resetPassword(this.resetData);
        alert("Password updated! You can now login.");
        this.$router.push('/login');
      } catch (err) {
        alert(err.response?.data?.message || "Reset failed. Check code.");
      }
    }
  }
}
</script>
