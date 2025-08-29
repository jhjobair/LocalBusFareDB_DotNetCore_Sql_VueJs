<template>
<div class="card-container">
    <div class="card custom-card shadow-lg">
        <div class="card-header">
            <h3 class="mb-0">Chart Info</h3>
        </div>
        <div class="card-body">
            <form @submit.prevent="validateAndSubmit">

                <!-- Error Messages -->
                <div v-if="errors.length" class="mb-3">
                    <div v-for="(error, index) in errors" :key="index" class="alert alert-danger py-2 mb-2">
                        {{ error }}
                    </div>
                </div>

                <!-- Chart Name -->
                <div class="mb-3">
                    <label class="form-label fw-bold">Chart Name</label>
                    <input type="text" class="form-control" v-model="chartName" placeholder="Enter chart name" />
                </div>

                <!-- Chart Code -->
                <div class="mb-3">
                    <label class="form-label fw-bold">Chart Code</label>
                    <input type="text" class="form-control" v-model="chartCode" placeholder="Enter chart code" />
                </div>

                <!-- Chart Path -->
                 <div class="mb-3">
                    <label class="form-label fw-bold">Chart Image</label>
                    <input type="file" class="form-control" @change="onFileSelected" accept="image/*" />
                </div>
                 <div v-if="imagePreviewUrl" class="mb-3">
                    <label class="form-label fw-bold">Preview</label>
                    <div>
                        <img :src="imagePreviewUrl" class="img-thumbnail" alt="Preview" style="max-width: 200px;" />
                    </div>
                </div>
                <!-- Save & Cancel Buttons -->
                <div class="row">
                    <div class="d-flex justify-content-start col col-2">
                        <button class="btn btn-success px-4" type="submit">
                            💾 Save
                        </button>
                    </div>
                    <div class="d-flex justify-content-end col col-10">
                        <button class="btn btn-success px-4" type="button" @click="cancelForm">
                            ❌ Cancel
                        </button>
                    </div>
                </div>

            </form>
        </div>
    </div>
</div>
</template>

<script>
import ChartInfoDataService from "../../service/ChartInfoDataService";
import Swal from "sweetalert2";

export default {
    name: "SingleChartInfo",
    data() {
        return {
            chartName: "",
            chartCode: "",
            chartPath: "",
                chartImageBase64: null,
            // 🟢 NEW: To display the existing image from server or the new preview
            imagePreviewUrl: null,
            errors: [],
               apiBaseUrl: "http://localhost:9080", 
        };
    },
    computed: {
        id() {
            return this.$route.params.id;
        },
    },
    methods: {
         getImageUrl(relativePath) {
            if (!relativePath) return '';
            // This combines your base URL and the relative path from the database
            // Example: "http://localhost:9080" + "/images/charts/3eab8d45....png"
            return `${this.apiBaseUrl}${relativePath}`;
        },
        refreshChartDetails() {
            ChartInfoDataService.retrieveChartInfo(this.id).then((res) => {
                this.chartName = res.data.chartName;
                this.chartCode = res.data.chartCode;
                 this.imagePreviewUrl = this.getImageUrl(res.data.chartPath);
            });
        },
         onFileSelected(event) {
            const file = event.target.files[0];
            if (!file) {
                this.chartImageBase64 = null;
                this.imagePreviewUrl = null; // Or revert to existing image if editing
                return;
            }

            // Use FileReader to convert image to a Base64 string
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => {
                // The result is the Base64 Data URL (e.g., "data:image/png;base64,iVBORw...")
                this.chartImageBase64 = reader.result;
                this.imagePreviewUrl = reader.result;
            };
            reader.onerror = (error) => {
                console.error("Error converting file to Base64:", error);
                this.errors.push("Failed to read the image file.");
            };
        },

        cancelForm() {
            this.$router.push("/ChartAllinfo");
        },
        validateAndSubmit(e) {
            e.preventDefault();
            this.errors = [];
            if (!this.chartName) {
                this.errors.push("Enter valid chartName");
            } else if (this.chartName.length < 2) {
                this.errors.push("Enter atleast 2 characters in ChartName");
            }
            if (!this.chartCode) {
                this.errors.push("Enter valid chartCode");
            } else if (this.chartCode.length < 2) {
                this.errors.push("Enter atleast 2 characters in ChartCode");
            }
             // 🟢 Validate that an image was selected when creating
            if (this.id == -1 && !this.chartImageBase64) {
                this.errors.push("Please select an image file.");
            }
            if (this.errors.length === 0) {
                if (this.id == -1) {
                    ChartInfoDataService.createChartInfo({
                        chartName: this.chartName,
                        chartCode: this.chartCode,
                       chartPath: this.chartImageBase64
                    }).then(() => {
                        this.$router.push("/ChartAllinfo");
                    }, err => {
                        console.error("Error Response:", err.response); // full object
                        if (err.response && err.response.data && err.response.data.message) {
                            this.errors.push(err.response.data.message); // custom message from backend
                        } else {
                            this.errors.push("Something went wrong");
                        }
                    });
                } else {
                    // 🟢 First ask confirmation
                    Swal.fire({
                        title: "Are you sure?",
                        text: "Do you want to update this chart info?",
                        icon: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#3085d6",
                        cancelButtonColor: "#d33",
                        confirmButtonText: "Yes, update it!"
                    }).then((result) => {
                        if (result.isConfirmed) {
                            // 🟢 Call API if user confirmed
                            ChartInfoDataService.updateChartInfo(this.id, {
                                    id: this.id,
                                    chartName: this.chartName,
                                    chartCode: this.chartCode,
                                    chartPath: this.chartImageBase64,
                                })
                                .then(() => {
                                    Swal.fire({
                                        icon: "success",
                                        title: "Updated!",
                                        text: "Chart info updated successfully.",
                                        timer: 2000,
                                        showConfirmButton: false,
                                    }).then(() => {
                                        this.$router.push("/ChartAllinfo"); // redirect
                                    });
                                })
                                .catch((err) => {
                                    console.error("Error Response:", err.response);
                                    let message =
                                        err.response && err.response.data && err.response.data.message ?
                                        err.response.data.message :
                                        "Something went wrong";
                                    Swal.fire({
                                        icon: "error",
                                        title: "Update Failed",
                                        text: message,
                                    });
                                });
                        }
                    });
                }
            }
        },
    },
    created() {
        this.refreshChartDetails();
    },
};
</script>

<style scoped>
/* Light Theme Card */
.light .custom-card {
    background: #ffffff;
    color: #000;
    border: 1px solid #ddd;
}

.light .custom-card .card-header {
    background: #007bff;
    color: white;
}

/* Dark Theme Card */
.dark .custom-card {
    background: #1e1e1e;
    color: #f5f5f5;
    border: 1px solid #444;
}

.dark .custom-card .card-header {
    background: #333;
    color: #f5f5f5;
}

/* Input fields theme aware */
.light .form-control {
    background: #fff;
    color: #000;
    border: 1px solid #ccc;
}

.dark .form-control {
    background: #2c2c2c;
    color: #f5f5f5;
    border: 1px solid #555;
}
</style>
