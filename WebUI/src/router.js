import { createWebHistory, createRouter } from "vue-router";

const routes =  [
    {
        path: "/Users",
        name: "Users",
        component: () => import("./components/Users"),
    },
    {
        path: "/user/:id",
        name: "User",
        component: () => import("./components/User"),
    },
    {
        path: "/",
        name: "Stations",
        component: () => import("./components/Stations"),
    },
    {
        path: "/station/:id",
        name: "Station",
        component: () => import("./components/Station"),
    },
];
 
const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;