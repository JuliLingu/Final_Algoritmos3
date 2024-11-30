import { useEffect, useState } from "react";
import { BrowserRouter, Route, Routes, Navigate } from "react-router-dom";

// Layouts
import FrontEnd from "../layouts/FrontEnd";
import BackOffice from "../layouts/BackOffice";

// Pages
import Login from "../pages/Login";
import Inicio from "../pages/Inicio";
import Tareas from "../pages/Tareas";
import AcercaDe from "../pages/AcercaDe";
import Configuraciones from "../pages/Configuraciones";

const AppRouter = () => {
  const [protectedRoutes, setProtectedRoutes] = useState(<></>);

  const baseFrontRoutes = (route, children) => {
    return <Route path={route} element={<FrontEnd>{children}</FrontEnd>} />;
  };

  const isUserAuthorized = () => {
    const userRole = localStorage.getItem("userRole");
    return userRole === "1";
  };

  useEffect(() => {
    if (localStorage.getItem("accessToken")) {
      setProtectedRoutes(
        <>
          {baseFrontRoutes("/inicio", <Inicio />)}
          {baseFrontRoutes("/acercade", <AcercaDe />)}
          {baseFrontRoutes("/tareas", <Tareas />)}
          {isUserAuthorized() && baseFrontRoutes("/configuraciones", <Configuraciones />)}
          {baseFrontRoutes("*", <Inicio />)}
        </>
      );
    } else {
      setProtectedRoutes(<>{baseFrontRoutes("*", <Login />)}</>);
    }
  }, []);

  return (
    <>
      <BrowserRouter>
        <Routes>{protectedRoutes}</Routes>
      </BrowserRouter>
    </>
  );
};

export default AppRouter;
