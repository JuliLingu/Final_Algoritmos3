import React from 'react';

const Tabla = ({ tareas, onEdit, onDelete }) => {
  return (
    <table className="table table-striped">
      <thead>
        <tr>
          <th scope="col">Título</th>
          <th scope="col">Descripción</th>
          <th scope="col">Prioridad</th>
          <th scope="col">Estado</th>
          <th scope="col">Acciones</th>
        </tr>
      </thead>
      <tbody>
  {tareas.map((tarea) => (
    tarea ? ( // Check if tarea is defined
      <tr key={tarea.id}>
        <td>{tarea.titulo}</td>
        <td>{tarea.descripcion}</td>
        <td>{tarea.prioridad}</td>
        <td>{tarea.estado}</td>
        <td>
          <button className="btn btn-warning btn-sm me-2" onClick={() => onEdit(tarea)}>Editar</button>
          <button className="btn btn-danger btn-sm" onClick={() => onDelete(tarea.id)}>Borrar</button>
        </td>
      </tr>
    ) : null
  ))}
</tbody>
    </table>
  );
};

export default Tabla;