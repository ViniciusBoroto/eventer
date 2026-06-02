const DEFAULT_EVENT_IMAGE =
  "data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 1200 630'%3E%3Crect width='1200' height='630' fill='%230f172a'/%3E%3Crect x='60' y='60' width='1080' height='510' rx='32' fill='%231e293b'/%3E%3Ctext x='600' y='290' text-anchor='middle' fill='%23f8fafc' font-family='Arial, Helvetica, sans-serif' font-size='72' font-weight='700'%3EEventer%3C/text%3E%3Ctext x='600' y='370' text-anchor='middle' fill='%2394a3b8' font-family='Arial, Helvetica, sans-serif' font-size='30'%3EImagem do evento indisponivel%3C/text%3E%3C/svg%3E";

const API_BASE_URL = window.EVENTER_API_BASE_URL
  || (window.location.protocol.startsWith("http")
    ? `${window.location.protocol}//${window.location.hostname}:5091/api`
    : "http://localhost:5091/api");

async function request(path, options = {}) {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    headers: {
      "Content-Type": "application/json",
      ...(options.headers || {})
    },
    ...options
  });

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || "Nao foi possivel concluir a requisicao.");
  }

  if (response.status === 204) {
    return null;
  }

  const contentType = response.headers.get("content-type") || "";
  if (!contentType.includes("application/json")) {
    return null;
  }

  return response.json();
}

function normalizeEvent(event) {
  return {
    id: event.id,
    name: event.name,
    description: event.description,
    pictureUrl: event.pictureUrl || DEFAULT_EVENT_IMAGE,
    price: Number(event.price || 0),
    capacity: Number(event.capacity || 0),
    date: event.date,
    location: event.location || ""
  };
}

function formatCurrency(value) {
  return new Intl.NumberFormat("pt-BR", {
    style: "currency",
    currency: "BRL"
  }).format(value);
}

function formatDate(dateValue) {
  const date = new Date(dateValue);
  if (Number.isNaN(date.getTime())) {
    return "Data indisponivel";
  }

  return new Intl.DateTimeFormat("pt-BR", {
    dateStyle: "long",
    timeStyle: "short"
  }).format(date);
}

function splitDateTime(dateValue) {
  const date = new Date(dateValue);
  if (Number.isNaN(date.getTime())) {
    return {
      dateLabel: "Data indisponivel",
      timeLabel: "Horario indisponivel"
    };
  }

  return {
    dateLabel: new Intl.DateTimeFormat("pt-BR", {
      dateStyle: "long"
    }).format(date),
    timeLabel: new Intl.DateTimeFormat("pt-BR", {
      timeStyle: "short"
    }).format(date)
  };
}

async function fetchEvents() {
  const events = await request("/event");
  return Array.isArray(events) ? events.map(normalizeEvent) : [];
}

async function fetchEventById(id) {
  const event = await request(`/event/${id}`);
  return normalizeEvent(event);
}

async function fetchOrders() {
  const orders = await request("/order");
  return Array.isArray(orders) ? orders : [];
}

async function fetchOrderById(id) {
  const order = await request(`/order/${id}`);
  return order || null;
}

function countActiveOrdersForEvent(orders, eventId) {
  return orders.filter(order => order.eventId === eventId && !order.canceledAt).length;
}

async function createOrder(eventId) {
  return request("/order", {
    method: "POST",
    body: JSON.stringify({ eventId })
  });
}

async function payOrder(orderId, eventId) {
  return request(`/order/${orderId}/pay`, {
    method: "POST",
    body: JSON.stringify({ orderId, eventId })
  });
}

async function cancelOrder(orderId) {
  return request(`/order/${orderId}/cancel`, {
    method: "POST"
  });
}

async function deleteOrder(orderId) {
  return request(`/order?id=${orderId}`, {
    method: "DELETE"
  });
}

async function createEvent(payload) {
  return request("/event", {
    method: "POST",
    body: JSON.stringify(payload)
  });
}

async function updateEvent(payload) {
  return request("/event", {
    method: "PUT",
    body: JSON.stringify(payload)
  });
}

async function deleteEvent(eventId) {
  return request(`/event?id=${eventId}`, {
    method: "DELETE"
  });
}

function toDateTimeLocalValue(dateValue) {
  const date = new Date(dateValue);
  if (Number.isNaN(date.getTime())) {
    return "";
  }

  const timezoneOffset = date.getTimezoneOffset() * 60000;
  return new Date(date.getTime() - timezoneOffset).toISOString().slice(0, 16);
}

function getOrderStatus(order) {
  if (order.canceledAt) {
    return "Cancelado";
  }

  if (order.confirmedAt) {
    return "Pago";
  }

  return "Pendente";
}

window.EventerApi = {
  API_BASE_URL,
  DEFAULT_EVENT_IMAGE,
  formatCurrency,
  formatDate,
  splitDateTime,
  toDateTimeLocalValue,
  getOrderStatus,
  fetchEvents,
  fetchEventById,
  fetchOrders,
  fetchOrderById,
  countActiveOrdersForEvent,
  createOrder,
  payOrder,
  cancelOrder,
  deleteOrder,
  createEvent,
  updateEvent,
  deleteEvent
};
