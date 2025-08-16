import { createWebHistory, createRouter } from "vue-router";

const routes =  [
    {
        path: "/Users",
        name: "Users",
        component: () => import("./components/User/Users"),
    },
    {
        path: "/user/:id",
        name: "User",
        component: () => import("./components/User/User"),
    },
    {
        path: "/",
        name: "Stations",
        component: () => import("./components/Stations/Stations"),
    },
    {
        path: "/station/:id",
        name: "Station",
        component: () => import("./components/Stations/Station"),
    },
];
 
const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;