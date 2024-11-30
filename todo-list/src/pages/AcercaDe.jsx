import React from 'react';

const AcercaDe = () => {
  return (
    <div className="container mt-5">
      <h1 className="mb-4">Sobre Esta Aplicación de Tareas</h1>
      <p>
        Esta aplicación te ayuda a gestionar y hacer un seguimiento de tus tareas de manera eficiente.
      </p>
      <h2 className="mt-4">Características</h2>
      <ul className="list-unstyled">
        <li><i className="bi bi-check-circle"></i> Añadir, editar y eliminar tareas</li>
        <li><i className="bi bi-check-circle"></i> Marcar tareas como completadas</li>
        <li><i className="bi bi-check-circle"></i> Ver un resumen de las tareas</li>
      </ul>
      <h2 className="mt-4">Sobre el Desarrollador</h2>
      <p>
        Desarrollado por Máximo Schmidt. Apasionado por construir aplicaciones útiles e intuitivas.
      </p>
    </div>
  );
};

export default AcercaDe;
