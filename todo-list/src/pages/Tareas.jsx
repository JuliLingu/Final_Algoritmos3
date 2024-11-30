import React, { useEffect, useState } from "react";
import Modal from "../components/Modal";
import Tabla from "../components/Tabla";
import { GET, POST, DELETE, PUT } from "../services/fetch"; // Asegúrate de tener estas funciones para manejar la API

const Tareas = () => {
  const [tareas, setTasks] = useState([]);
  const [nuevaTarea, setNuevaTarea] = useState({
    titulo: "",
    descripcion: "",
    prioridad: "Baja",
    estado: "Pendiente",
  });
  const [isEditMode, setIsEditMode] = useState(false);
  const [currentTaskId, setCurrentTaskId] = useState(null); // Para almacenar el ID de la tarea actual

  useEffect(() => {
    const fetchTareas = async () => {
      try {
        const data = await GET("api/tareas"); // Cambia la URL según tu API
        setTasks(data);
      } catch (error) {
        console.error("Error al cargar las tareas:", error);
        alert("No se pudieron cargar las tareas.");
      }
    };

    fetchTareas();
  }, []);

  const handleChange = (e) => {
    setNuevaTarea({ ...nuevaTarea, [e.target.name]: e.target.value });
  };

  const handleAddTask = async () => {
    try {
      const createdTask = await POST("api/tareas", nuevaTarea); // Envía la nueva tarea al backend
      setTasks([...tareas, createdTask]); // Agrega la nueva tarea a la lista

      // Restablecer el estado de nuevaTarea
      resetForm();
      
      const modalElement = document.getElementById("addTaskModal");
      const modal = bootstrap.Modal.getInstance(modalElement);
      modal.hide();
    } catch (error) {
      console.error("Error al agregar tarea:", error);
      alert("Ocurrió un error al agregar la tarea.");
    }
  };

  const handleEditTask = async () => {
    try {
      const updatedTask = await PUT(`api/tareas/${currentTaskId}`, nuevaTarea); // Envía la tarea actualizada al backend
  
      setTasks(tareas.map(tarea => (tarea.id === currentTaskId ? updatedTask : tarea))); // Actualiza la lista de tareas
  
      resetForm(); // Restablecer el formulario
  
      const modalElement = document.getElementById("addTaskModal");
      const modal = bootstrap.Modal.getInstance(modalElement);
      modal.hide(); // Ocultar modal
    } catch (error) {
      console.error("Error al actualizar tarea:", error);
      alert("Ocurrió un error al actualizar la tarea.");
    }
  };

  const handleDeleteTask = async (id) => {
    if (window.confirm("¿Estás seguro de que deseas eliminar esta tarea?")) {
      try {
        await DELETE(`api/tareas/${id}`); // Envía la solicitud DELETE al backend
        setTasks(tareas.filter(tarea => tarea.id !== id)); // Actualiza el estado eliminando la tarea
      } catch (error) {
        console.error("Error al eliminar tarea:", error);
        alert("Ocurrió un error al eliminar la tarea.");
      }
    }
  };

  const handleEditButtonClick = (tarea) => {
    setNuevaTarea(tarea); // Cargar los datos de la tarea en el formulario
    setIsEditMode(true); // Activar modo edición
    setCurrentTaskId(tarea.id); // Guardar el ID de la tarea actual

    const modalElement = document.getElementById("addTaskModal");
    const modal = new bootstrap.Modal(modalElement);
    modal.show(); // Mostrar el modal para editar
  };

  const resetForm = () => {
    setNuevaTarea({
      titulo: "",
      descripcion: "",
      prioridad: "Baja",
      estado: "Pendiente",
    });
    setIsEditMode(false); // Desactivar modo edición
    setCurrentTaskId(null); // Limpiar el ID actual
  };

  return (
    <div className="container mt-5">
      <h1 className="mb-4">Lista de Tareas</h1>

      <button
        className="btn btn-primary mb-3"
        data-bs-toggle="modal"
        data-bs-target="#addTaskModal"
        onClick={resetForm} // Restablecer el formulario al abrir el modal
      >
        Agregar Tarea
      </button>

      <Modal
        nuevaTarea={nuevaTarea}
        handleChange={handleChange}
        handleAddTask={handleAddTask}
        handleEditTask={handleEditTask}
        isEditMode={isEditMode} // Pasar si está en modo edición o no
      />

      <Tabla tareas={tareas} onEdit={handleEditButtonClick} onDelete={handleDeleteTask} />
    </div>
  );
};

export default Tareas;