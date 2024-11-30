import { backendurl } from "../env.jsx";

export async function POST(url, data) {
  return await fetch(backendurl + url, {
    method: "POST",
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
    },
    body: JSON.stringify(data),
  })
    .then((res) => res.json())
    .then((res) => res)
    .catch((err) => console.log(err));
}

export async function PUT(url, data) {
  return await fetch(backendurl + url, {
    method: "PUT",
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
    },
    body: JSON.stringify(data),
  })
    .then((res) => res.json())
    .then((res) => res)
    .catch((err) => console.log(err));
}

export async function GET(url, data) {
  let objString = "?";
  if (Array.isArray(data)) {
    data.forEach((el, index) => {
      objString = objString + `array[${index}][id]=${el.id}&`;
    });
  } else {
    objString = objString + new URLSearchParams(data).toString();
  }

  return await fetch(backendurl + url + objString, {
    method: "GET",
    mode: "cors",
    headers: {
      Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
    },
  })
    .then((res) => res.json())
    .then((res) => res);
}

export async function PATCH(url, data) {
  return await fetch(backendurl + url, {
    method: "PATCH",
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
    },
    body: JSON.stringify(data),
  })
    .then((res) => res.json())
    .then((res) => res)
    .catch((err) => console.log(err));
}

// export async function DELETE(url, data) {
//   const objString = "?" + new URLSearchParams(data).toString();

//   return await fetch(backendurl + url + objString, {
//     method: "DELETE",
//     mode: "cors",
//     headers: {
//       Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
//     },
//   })
//     .then((res) => res.json())
//     .then((res) => res)
//     .catch((err) => console.log(err));
// }

export async function DELETE(url, data) {
  const objString = "?" + new URLSearchParams(data).toString();

  try {
      const response = await fetch(backendurl + url + objString, {
          method: "DELETE",
          mode: "cors",
          headers: {
              Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
          },
      });

      if (!response.ok) {
          const errorDetails = await response.json();
          throw new Error(`HTTP error! status: ${response.status}, details: ${JSON.stringify(errorDetails)}`);
      }

      // Si no hay contenido en la respuesta, simplemente retorna un valor vacío o null
      return response.status === 204 ? null : await response.json();
  } catch (err) {
      console.error("Error en DELETE:", err);
      throw err;
  }
}

export async function POSTU(url, file) {
  let data = new FormData();
  data.append("file", file);

  return await fetch(backendurl + url, {
    method: "POST",
    mode: "cors",
    headers: {
      Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
    },
    body: data,
  })
    .then((res) => res.json())
    .then((res) => res)
    .catch((err) => {
      console.log(err);
    });
}
