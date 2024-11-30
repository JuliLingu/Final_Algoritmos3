import React, { useState, useEffect } from "react";
import { GET, PUT } from "../services/fetch"; 

const Configuraciones = () => {
    const [users, setUsers] = useState([]);
    const [error, setError] = useState("");

    // Obtener la lista de usuarios
    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const data = await GET("api/usuarios");
                if (data) {
                    setUsers(data);
                } else {
                    setError("No se pudo cargar la lista de usuarios.");
                }
            } catch (err) {
                console.error("Error al obtener los usuarios:", err);
                setError("No se pudo cargar la lista de usuarios.");
            }
        };
        fetchUsers();
    }, []);

    // Actualizar el rol del usuario
    const handleUpdateRol = async (userId, newRoleId) => {
      try {
          const response = await PUT(`api/usuarios/updateRole/${userId}`, newRoleId);
  
          if (response && response.id === userId) {
              setUsers(users.map(user =>
                  user.id === userId ? { ...user, id_Roles_FK: newRoleId } : user
              ));
              alert("Rol actualizado correctamente.");
          } else if (response && response.message) {
              alert(response.message);
              
              if (response.message.includes("debe haber al menos un administrador")) {
                  window.location.reload(); 
              }
          } else {
              alert("No se pudo actualizar el rol.");
          }
      } catch (err) {
          console.error("Error al actualizar el rol:", err);
          alert("Ocurrió un error al actualizar el rol.");
      }
  };

    return (
        <div className="container mt-5">
            <h1>Configuración de Usuarios</h1>
            {error && <p className="text-danger">{error}</p>}

            <table className="table mt-4">
                <thead>
                    <tr>
                        <th>Nombre</th>
                        <th>Rol</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    {users.map(user => (
                        <tr key={user.id}>
                            <td>{user.nombre}</td>
                            <td>
                                <select
                                    value={user.id_Roles_FK}
                                    onChange={e => {
                                        const updatedUsers = users.map(u =>
                                            u.id === user.id
                                                ? { ...u, id_Roles_FK: parseInt(e.target.value, 10) }
                                                : u
                                        );
                                        setUsers(updatedUsers);
                                    }}
                                    className="form-select"
                                >
                                    <option value="1">Administrador</option>
                                    <option value="2">Moderador</option>
                                    <option value="3">Usuario</option>
                                </select>
                            </td>
                            <td>
                                <button
                                    className="btn btn-outline-primary"
                                    onClick={() => handleUpdateRol(user.id, user.id_Roles_FK)}
                                >
                                    Guardar cambios
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default Configuraciones;