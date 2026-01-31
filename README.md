🚌 LocalBus Fare-Chart System

[![.NET](https://img.shields.io/badge/.NET-8.0-512bd4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Vue.js](https://img.shields.io/badge/Vue.js-3.x-42b883?logo=vuedotjs)](https://vuejs.org/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/en-us/sql-server/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A robust, full-stack enterprise application designed to manage bus routes, stations, and fare calculations. This system provides secure administrative controls and professional reporting capabilities for public transport management.

---

## ✨ Key Features

### 🔐 Security & Identity
*   **JWT Authentication:** Secure login using JSON Web Tokens with **Refresh Token** logic.
*   **Password Recovery:** Automated "Forgot Password" workflow using **MailKit/SMTP** with 6-digit verification codes and expiration logic.
*   **Role-Based Access:** Protected routes and API endpoints based on user roles.

### 📊 Reporting Engine
*   **RDLC Integration:** Dynamic generation of **PDF and Excel** reports.
*   **Unicode Support:** Full compatibility with **Bangla fonts** for localized reporting.
*   **Parameterized Reports:** Filterable reports for specific stations or full database exports.

### 🚌 Fare Management
*   **Station CRUD:** Full management of bus stations and coordinates.
*   **Dynamic Fare Chart:** Calculate and display fares based on distance and station relations.
*   **Modern UI:** Built with Vue.js, featuring **Select2** for searchable dropdowns and **SweetAlert2** for interactive feedback.

---

## 🛠️ Tech Stack

### Backend
*   **Framework:** ASP.NET Core 8.0 Web API
*   **ORM:** Entity Framework Core (Code-First)
*   **Database:** Microsoft SQL Server
*   **Services:** Automapper, MailKit, MimeKit, ReportViewerCore.NETCore

### Frontend
*   **Framework:** Vue.js 3
*   **State Management:** LocalStorage / Reactive state
*   **Styling:** Bootstrap 5 / Custom CSS
*   **HTTP Client:** Axios

---

## 🚀 Getting Started

### Prerequisites
*   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [Node.js](https://nodejs.org/) (v16+)
*   SQL Server Express

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/LocalBus-Fare-Chart.git
   ```

2. **Backend Setup**
   * Navigate to `WebAPI` folder.
   * Update `appsettings.json` with your **Connection String** and **Gmail App Password**.
   * Run migrations:
     ```bash
     dotnet ef database update
     ```
   * Start the API:
     ```bash
     dotnet run
     ```

3. **Frontend Setup**
   * Navigate to the Vue project folder.
   * Install dependencies:
     ```bash
     npm install
     ```
   * Start the development server:
     ```bash
     npm run serve
     ```

---

## 📖 API Documentation
Once the backend is running, you can access the interactive **Swagger UI** at:
`http://localhost:9080/swagger`

---
<img width="1869" height="899" alt="Screenshot_10" src="https://github.com/user-attachments/assets/2f43eb02-20e0-4678-bc97-ee3bd9ab3e9e" />

<img width="1918" height="918" alt="Screenshot_2" src="https://github.com/user-attachments/assets/7e77c4ca-fb7a-4e2e-9797-5b9fc52dd45e" />

<img width="1905" height="920" alt="Screenshot_1" src="https://github.com/user-attachments/assets/82a8b1e4-ec31-4304-b528-7715ad9589a8" />
<img width="1908" height="908" alt="Screenshot_3" src="https://github.com/user-attachments/assets/e01a8d85-8ac0-450d-8ed3-37cf20688756" />

<img width="1891" height="905" alt="Screenshot_4" src="https://github.com/user-attachments/assets/16039e64-d21d-4bcf-9aca-0d62cce7e74f" />

<img width="1880" height="920" alt="Screenshot_9" src="https://github.com/user-attachments/assets/fedcd817-4605-48d0-824a-b73ee5c2aec2" />
<img width="888" height="628" alt="Screenshot_8" src="https://github.com/user-attachments/assets/afca1a49-e133-4f42-8d1d-a2c6651d3a4e" />

<img width="1894" height="916" alt="Screenshot_7" src="https://github.com/user-attachments/assets/20f134d2-ed7c-4773-829d-2c8c45dbee5e" />



## 📝 License
Distributed under the MIT License. See `LICENSE` for more information.

## ✉️ Contact
**Your Name** - [jhjobair2878@gmail.com](mailto:jhjobair2878@gmail.com)  
**Project Link:** [https://github.com/yourusername/LocalBus-Fare-Chart](https://github.com/yourusername/LocalBus-Fare-Chart)

---
*Developed with ❤️ as a Full Stack Portfolio Project.*
