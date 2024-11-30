import { useState } from "react";
import { POST } from "../services/fetch";

const Login = () => {
  const [formData, setFormData] = useState({
    username: "",
    password: "",
  });
  const [error, setError] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const submitLogin = async (e) => {
    e.preventDefault();
    setError(""); 

    if (!formData.username || !formData.password) {
      window.alert("Complete los campos para continuar.");
    } else {
      try {
        const rsp = await POST("api/Auth/login", formData);
        
        if (rsp && rsp.token) {      
          localStorage.setItem("accessToken", rsp.token);
          localStorage.setItem("username", rsp.user.nombre); 
          localStorage.setItem("userRole", rsp.user.id_Roles_FK);
          localStorage.setItem("userId", rsp.user.id); 
          window.location.replace("/inicio");  
        } else {
          setError("Credenciales inválidas. Por favor, inténtelo de nuevo.");
        }
      } catch (error) {
        console.error("Error en el login:", error);
        setError(error.response?.data?.message || "Ocurrió un error al iniciar sesión. Intente más tarde.");
      }
    }
  };

  return (
    <div className="container d-flex flex-column align-items-center justify-content-center min-vh-100">
      <div className="w-100" style={{ maxWidth: "400px" }}>
        <div className="card">
          <div className="card-body">
            <h2 className="mb-4 text-center">Iniciar Sesión</h2>
            {error && <p className="text-danger text-center">{error}</p>}
            <form onSubmit={submitLogin}>
              <div className="mb-3">
                <label htmlFor="username" className="form-label">
                  Usuario
                </label>
                <input
                  type="text"
                  name="username"
                  id="username"
                  className="form-control"
                  placeholder="Ingrese el usuario"
                  value={formData.username}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="mb-3">
                <label htmlFor="password" className="form-label">
                  Contraseña
                </label>
                <input
                  type="password"
                  name="password"
                  id="password"
                  className="form-control"
                  placeholder="Ingrese la clave"
                  aria-label="Password"
                  value={formData.password}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="d-flex justify-content-center">
                <button type="submit" className="btn btn-outline-success">
                  Iniciar sesión
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
