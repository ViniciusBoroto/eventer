const params = new URLSearchParams(window.location.search);
const eventId = Number(params.get("id"));

const eventName = document.getElementById("eventName");
const eventDate = document.getElementById("eventDate");
const eventTime = document.getElementById("eventTime");
const eventLocation = document.getElementById("eventLocation");
const eventDescription = document.getElementById("eventDescription");
const eventPrice = document.getElementById("eventPrice");
const eventTickets = document.getElementById("eventTickets");
const eventImage = document.getElementById("eventImage");
const statusMessage = document.getElementById("statusMessage");
const buyButton = document.getElementById("buyButton");

let currentEvent = null;
let currentAvailability = 0;

function setStatus(message, type = "") {
  statusMessage.className = `status-message ${type}`.trim();
  statusMessage.textContent = message;
}

function updatePurchaseState() {
  const soldOut = currentAvailability <= 0;
  buyButton.disabled = soldOut || !currentEvent;
  buyButton.textContent = soldOut ? "Ingressos esgotados" : "Comprar ingresso";
}

async function loadEventDetails() {
  if (!Number.isInteger(eventId) || eventId <= 0) {
    setStatus("Evento invalido.");
    buyButton.disabled = true;
    return;
  }

  try {
    setStatus("Carregando detalhes do evento...");

    const [event, orders] = await Promise.all([
      EventerApi.fetchEventById(eventId),
      EventerApi.fetchOrders()
    ]);

    currentEvent = event;
    const activeOrders = EventerApi.countActiveOrdersForEvent(orders, event.id);
    currentAvailability = Math.max(event.capacity - activeOrders, 0);

    const labels = EventerApi.splitDateTime(event.date);

    eventName.textContent = event.name;
    eventDate.textContent = labels.dateLabel;
    eventTime.textContent = labels.timeLabel;
    eventLocation.textContent = event.location || "Local a definir";
    eventDescription.textContent = event.description || "Descricao indisponivel.";
    eventPrice.textContent = EventerApi.formatCurrency(event.price);
    eventTickets.textContent = `${currentAvailability} de ${event.capacity} ingressos disponiveis`;
    eventImage.src = event.pictureUrl;
    eventImage.alt = `Imagem do evento ${event.name}`;

    updatePurchaseState();
    setStatus("");
  } catch (error) {
    setStatus(`Falha ao carregar o evento: ${error.message}`, "error");
    buyButton.disabled = true;
  }
}

buyButton.addEventListener("click", async () => {
  if (!currentEvent || currentAvailability <= 0) {
    return;
  }

  try {
    buyButton.disabled = true;
    setStatus("Criando pedido...");
    await EventerApi.createOrder(currentEvent.id);
    await loadEventDetails();
    setStatus("Pedido criado com sucesso. Seu ingresso foi reservado.", "success");
  } catch (error) {
    setStatus(`Nao foi possivel criar o pedido: ${error.message}`, "error");
    updatePurchaseState();
  }
});

loadEventDetails();
