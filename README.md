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

## 📝 License
Distributed under the MIT License. See `LICENSE` for more information.

## ✉️ Contact
**Your Name** - [jhjobair2878@gmail.com](mailto:jhjobair2878@gmail.com)  
**Project Link:** [https://github.com/yourusername/LocalBus-Fare-Chart](https://github.com/yourusername/LocalBus-Fare-Chart)

---
*Developed with ❤️ as a Full Stack Portfolio Project.*
