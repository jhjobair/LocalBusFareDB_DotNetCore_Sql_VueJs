import { createWebHistory, createRouter } from "vue-router";

const routes =  [
    {
        path: "/",
        name: "Users",
        component: () => import("./components/User/Users"),
    },
    {
        path: "/user/:id",
        name: "User",
        component: () => import("./components/User/User"),
    },
    {
        path: "/Stations",
        name: "Stations",
        component: () => import("./components/Stations/Stations"),
    },
    {
        path: "/station/:id",
        name: "Station",
        component: () => import("./components/Stations/Station"),
    },
    {
        path: "/ChartAllinfo",
        name: "ChartAllinfo",
        component: () => import("./components/ChartInfo/ChartAllinfo"),
    },
    {
        path: "/SingleChartInfo/:id",
        name: "SingleChartInfo",
        component: () => import("./components/ChartInfo/SingleChartInfo"),
    },
];
 
const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;