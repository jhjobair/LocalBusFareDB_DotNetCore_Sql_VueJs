// import { createWebHistory, createRouter } from "vue-router";
// import { jwtDecode } from "jwt-decode";

// const routes =  [
//     {
//         path: "/",
//         name: "Users",
//         component: () => import("./components/User/Users"),
//     },
//     {
//         path: "/user/:id",
//         name: "User",
//         component: () => import("./components/User/User"),
//     },
//     {
//         path: "/Stations",
//         name: "Stations",
//         component: () => import("./components/Stations/Stations"),
//     },
//     {
//         path: "/station/:id",
//         name: "Station",
//         component: () => import("./components/Stations/Station"),
//     },
//     {
//         path: "/ChartAllinfo",
//         name: "ChartAllinfo",
//         component: () => import("./components/ChartInfo/ChartAllinfo"),
//     },
//     {
//         path: "/SingleChartInfo/:id",
//         name: "SingleChartInfo",
//         component: () => import("./components/ChartInfo/SingleChartInfo"),
//     },
//     {
//         path: "/FareChart",
//         name: "FareChart",
//         component: () => import("./components/FareChart/FareChart"),
//     },
//     {
//         path: "/SingleFareChart/:id",
//         name: "SingleFareChart",
//         component: () => import("./components/FareChart/SingleFareChart"),
//     },
//      {
//         path: "/farechart/final",
//         name: "FinalView",
//         component: () => import("./components/FareChart/FinalView"),
//     },
// ];
 
// const router = createRouter({
//   history: createWebHistory(),
//   routes,
// });

// // Global navigation guard
// router.beforeEach((to, from, next) => {
//   const requiresAuth = to.matched.some(record => record.meta.requiresAuth);
//   const isAuthenticated = localStorage.getItem('userToken');

//   if (requiresAuth && !isAuthenticated) {
//     // If route requires auth and user is not authenticated, redirect to login page
//     next('/login');
//   } else if (to.path === '/login' && isAuthenticated) {
//     // If user is authenticated and tries to go to login page, redirect to home
//     next('/');
//   } else {
//     // Allow access
//     next();
//   }
// });

// router.beforeEach((to, from, next) => {
//   const token = localStorage.getItem('userToken');
  
//   if (to.meta.requiresAdmin) {
//     if (!token) return next('/login');
    
//     const decoded = jwtDecode(token);
//     // Adjust 'role' based on how you named the claim in your C# backend
//     if (decoded.role === 'Admin') { 
//       next();
//     } else {
//       next('/'); // Not an admin, redirect to home
//     }
//   } else {
//     next();
//   }
// });

// export default router;




import { createWebHistory, createRouter } from "vue-router";
import { jwtDecode } from "jwt-decode";

const routes = [
    {
        path: "/login",
        name: "Login",
        // Make sure you create this component
        component: () => import("./components/Authantication/Login.vue"), 
    },
    {
        path: "/",
        name: "Users",
        component: () => import("./components/User/Users"),
        meta: { requiresAuth: true } // Added meta
    },
    {
        path: "/user/:id",
        name: "User",
        component: () => import("./components/User/User"),
        meta: { requiresAuth: true }
    },
    {
        path: "/Stations",
        name: "Stations",
        component: () => import("./components/Stations/Stations"),
        meta: { requiresAuth: true }
    },
    {
        path: "/station/:id",
        name: "Station",
        component: () => import("./components/Stations/Station"),
        meta: { requiresAuth: true }
    },
    {
        path: "/ChartAllinfo",
        name: "ChartAllinfo",
        component: () => import("./components/ChartInfo/ChartAllinfo"),
        meta: { requiresAuth: true,/* requiresAdmin: true */ } // Only Admin can see this
    },
    {
        path: "/SingleChartInfo/:id",
        name: "SingleChartInfo",
        component: () => import("./components/ChartInfo/SingleChartInfo"),
        meta: { requiresAuth: true }
    },
    {
        path: "/FareChart",
        name: "FareChart",
        component: () => import("./components/FareChart/FareChart"),
        meta: { requiresAuth: true }
    },
    {
        path: "/SingleFareChart/:id",
        name: "SingleFareChart",
        component: () => import("./components/FareChart/SingleFareChart"),
        meta: { requiresAuth: true }
    },
    {
        path: "/farechart/final",
        name: "FinalView",
        component: () => import("./components/FareChart/FinalView"),
        meta: { requiresAuth: true }
    },
    {
    path: "/register",
    name: "Register",
    component: () => import("./components/Authantication/Register.vue"),
    },
    {
  path: '/forgotpassword',
  name: 'ForgotPassword',
  component: () => import('@/components/Authantication/ForgotPassword.vue')
    },
    {
    path: '/resetpassword',
    name: 'ResetPassword',
    component: () => import('@/components/Authantication/ResetPassword.vue')
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

// MERGED Navigation Guard
router.beforeEach((to, from, next) => {
    const token = localStorage.getItem('userToken');
    
    // 1. Check if user is trying to access a restricted page without being logged in
    if (to.meta.requiresAuth && !token) {
        return next('/login');
    }

    // 2. If user IS logged in, check role permissions
    if (token) {
        try {
            const decoded = jwtDecode(token);
            
            // Check if token is expired (Optional but recommended)
            const currentTime = Date.now() / 1000;
            if (decoded.exp < currentTime) {
                localStorage.removeItem('userToken');
                return next('/login');
            }

            // Check for Admin requirement
            // Note: 'role' must match the key name in your C# JWT Claim (e.g., ClaimTypes.Role)
            if (to.meta.requiresAdmin && decoded.role !== 'Admin') {
                alert("Access Denied: Admin only");
                return next('/'); 
            }
        } catch (error) {
            console.error("Invalid token", error);
            localStorage.removeItem('userToken');
            return next('/login');
        }
    }

    // 3. Prevent logged-in users from going back to login page
    if (to.path === '/login' && token) {
        return next('/');
    }

    next(); // Allow access if all checks pass
});

export default router;