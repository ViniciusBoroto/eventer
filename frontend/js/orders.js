const ordersTableBody = document.getElementById("ordersTableBody");
const orderFilter = document.getElementById("orderFilter");
const ordersFeedback = document.getElementById("ordersFeedback");

let currentOrders = [];
let eventsById = new Map();

function setOrdersFeedback(message, type = "") {
  ordersFeedback.className = `feedback-message ${type}`.trim();
  ordersFeedback.textContent = message;
}

function getVisibleOrders() {
  const filter = orderFilter.value;
  if (!filter) {
    return currentOrders;
  }

  return currentOrders.filter(order => EventerApi.getOrderStatus(order) === filter);
}

function renderOrders() {
  const visibleOrders = getVisibleOrders();

  if (!visibleOrders.length) {
    ordersTableBody.innerHTML = `
      <tr>
        <td colspan="8" class="empty-state">Nenhum pedido encontrado.</td>
      </tr>
    `;
    return;
  }

  ordersTableBody.innerHTML = visibleOrders.map(order => {
    const linkedEvent = eventsById.get(order.eventId);
    const status = EventerApi.getOrderStatus(order);
    const canPay = status === "Pendente";
    const canCancel = status !== "Cancelado";

    return `
      <tr>
        <td>${order.id}</td>
        <td>${linkedEvent?.name || `Evento #${order.eventId}`}</td>
        <td>${EventerApi.formatDate(order.createdAt)}</td>
        <td>${status}</td>
        <td>${order.ticketId ?? "-"}</td>
        <td>${order.confirmedAt ? EventerApi.formatDate(order.confirmedAt) : "-"}</td>
        <td>${order.canceledAt ? EventerApi.formatDate(order.canceledAt) : "-"}</td>
        <td class="actions-cell">
          <button type="button" class="secondary-button" data-action="pay" data-order-id="${order.id}" ${canPay ? "" : "disabled"}>Pagar</button>
          <button type="button" class="warning-button" data-action="cancel" data-order-id="${order.id}" ${canCancel ? "" : "disabled"}>Cancelar</button>
          <button type="button" class="danger-button" data-action="delete" data-order-id="${order.id}">Excluir</button>
        </td>
      </tr>
    `;
  }).join("");

  ordersTableBody.querySelectorAll("[data-action]").forEach(button => {
    button.addEventListener("click", async () => {
      const orderId = Number(button.dataset.orderId);
      const action = button.dataset.action;

      try {
        if (action === "pay") {
          setOrdersFeedback(`Confirmando pagamento do pedido #${orderId}...`);
          await EventerApi.payOrder(orderId);
          setOrdersFeedback("Pedido pago com sucesso.", "success");
        }

        if (action === "cancel") {
          setOrdersFeedback(`Cancelando pedido #${orderId}...`);
          await EventerApi.cancelOrder(orderId);
          setOrdersFeedback("Pedido cancelado com sucesso.", "success");
        }

        if (action === "delete") {
          if (!window.confirm(`Excluir o pedido #${orderId}?`)) {
            return;
          }

          setOrdersFeedback(`Excluindo pedido #${orderId}...`);
          await EventerApi.deleteOrder(orderId);
          setOrdersFeedback("Pedido excluido com sucesso.", "success");
        }

        await loadOrders();
      } catch (error) {
        setOrdersFeedback(`Falha na operacao: ${error.message}`, "error");
      }
    });
  });
}

async function loadOrders() {
  try {
    const [orders, events] = await Promise.all([
      EventerApi.fetchOrders(),
      EventerApi.fetchEvents()
    ]);

    currentOrders = orders.sort((left, right) => right.id - left.id);
    eventsById = new Map(events.map(event => [event.id, event]));
    renderOrders();
  } catch (error) {
    currentOrders = [];
    renderOrders();
    setOrdersFeedback(`Falha ao carregar pedidos: ${error.message}`, "error");
  }
}

orderFilter.addEventListener("change", renderOrders);

loadOrders();
