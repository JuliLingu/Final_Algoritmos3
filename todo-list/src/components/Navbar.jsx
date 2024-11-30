import { Link } from "react-router-dom";
import FunctionButton from "./Buttons/FunctionButton";
import { useState, useEffect } from "react";

const Navbar = ({ accessToken, logOut }) => {
  const [username, setUsername] = useState(null);
  const [currentDate, setCurrentDate] = useState(new Date().toLocaleDateString("es-ES"));
  const [currentTime, setCurrentTime] = useState(new Date().toLocaleTimeString());
  const [userRole, setUserRole] = useState(null);

  useEffect(() => {
    const timer = setInterval(() => {
      setCurrentTime(new Date().toLocaleTimeString());
      setCurrentDate(new Date().toLocaleDateString("es-ES"));
    }, 1000);
    return () => clearInterval(timer);
  }, []);

  useEffect(() => {
    const storedUsername = localStorage.getItem("username");
    setUsername(storedUsername);
    
    const role = localStorage.getItem("userRole"); 
    setUserRole(role);
  }, []);

  if (!accessToken) {
    return null;
  }

  return (
    <nav className="navbar navbar-expand-lg bg-body-tertiary">
      <div className="container-fluid">
        <Link className="navbar-brand" to="/inicio">
          Lista de tareas
        </Link>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <Link className="nav-link" to="/inicio" aria-current="page">
                Inicio
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/tareas">
                Tareas
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/acercade">
                Acerca
              </Link>
            </li>
            {userRole === "1" && (
              <li className="nav-item">
                <Link className="nav-link" to="/configuraciones">
                  Configuraciones
                </Link>
              </li>
            )}
          </ul>
          <div className="d-flex align-items-center gap-3">
            <span className="text-black">
              <i className="bi bi-person"></i> {username}
            </span>
            <span className="text-black">
              <i className="bi bi-calendar4-week"></i> {currentDate}
            </span>
            <span className="text-black">
              <i className="bi bi-clock"></i> {currentTime}
            </span>
            <FunctionButton
              text={"Salir"}
              tipo={"outline-danger"}
              callback={logOut}
            />
          </div>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
