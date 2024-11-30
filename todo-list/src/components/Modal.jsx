import React from 'react';

const Modal = ({ nuevaTarea, handleChange, handleAddTask, handleEditTask, isEditMode }) => {
  return (
    <div className="modal fade" id="addTaskModal" tabIndex="-1" aria-labelledby="addTaskModalLabel" aria-hidden="true">
      <div className="modal-dialog">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title" id="addTaskModalLabel">{isEditMode ? "Editar Tarea" : "Agregar Nueva Tarea"}</h5>
            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div className="modal-body">
            <div className="mb-3">
              <label htmlFor="titulo" className="form-label">Título</label>
              <input
                type="text"
                className="form-control"
                id="titulo"
                name="titulo"
                value={nuevaTarea.titulo}
                onChange={handleChange}
                required
              />
            </div>
            <div className="mb-3">
              <label htmlFor="descripcion" className="form-label">Descripción</label>
              <textarea
                className="form-control"
                id="descripcion"
                name="descripcion"
                value={nuevaTarea.descripcion}
                onChange={handleChange}
                required
              ></textarea>
            </div>
            <div className="mb-3">
              <label htmlFor="prioridad" className="form-label">Prioridad</label>
              <select
                className="form-select"
                id="prioridad"
                name="prioridad"
                value={nuevaTarea.prioridad}
                onChange={handleChange}
              >
                <option value="Baja">Baja</option>
                <option value="Media">Media</option>
                <option value="Alta">Alta</option>
              </select>
            </div>
            <div className="mb-3">
              <label htmlFor="estado" className="form-label">Estado</label>
              <select
                className="form-select"
                id="estado"
                name="estado"
                value={nuevaTarea.estado}
                onChange={handleChange}
              >
                <option value="Pendiente">Pendiente</option>
                <option value="Completada">Completada</option>
              </select>
            </div>
          </div>
          <div className="modal-footer">
            <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
            {isEditMode ? (
              <button type="button" className="btn btn-primary" onClick={handleEditTask}>Actualizar Tarea</button>
            ) : (
              <button type="button" className="btn btn-primary" onClick={handleAddTask}>Agregar Tarea</button>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Modal;