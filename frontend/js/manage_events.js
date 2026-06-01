const eventForm = document.getElementById("eventForm");
const formTitle = document.getElementById("formTitle");
const submitButton = document.getElementById("submitButton");
const cancelEditButton = document.getElementById("cancelEditButton");
const eventsTableBody = document.getElementById("eventsTableBody");
const feedbackMessage = document.getElementById("feedbackMessage");

const fields = {
  id: document.getElementById("eventId"),
  name: document.getElementById("name"),
  description: document.getElementById("description"),
  pictureUrl: document.getElementById("pictureUrl"),
  price: document.getElementById("price"),
  capacity: document.getElementById("capacity"),
  date: document.getElementById("date"),
  location: document.getElementById("location")
};

let currentEvents = [];

function setFeedback(message, type = "") {
  feedbackMessage.className = `feedback-message ${type}`.trim();
  feedbackMessage.textContent = message;
}

function resetForm() {
  eventForm.reset();
  fields.id.value = "";
  fields.pictureUrl.value = EventerApi.DEFAULT_EVENT_IMAGE;
  formTitle.textContent = "Criar evento";
  submitButton.textContent = "Salvar evento";
  cancelEditButton.hidden = true;
}

function populateForm(event) {
  fields.id.value = event.id;
  fields.name.value = event.name;
  fields.description.value = event.description;
  fields.pictureUrl.value = event.pictureUrl;
  fields.price.value = event.price;
  fields.capacity.value = event.capacity;
  fields.date.value = EventerApi.toDateTimeLocalValue(event.date);
  fields.location.value = event.location;
  formTitle.textContent = `Editar evento #${event.id}`;
  submitButton.textContent = "Atualizar evento";
  cancelEditButton.hidden = false;
  window.scrollTo({ top: 0, behavior: "smooth" });
}

function renderEvents(events) {
  if (!events.length) {
    eventsTableBody.innerHTML = `
      <tr>
        <td colspan="7" class="empty-state">Nenhum evento cadastrado.</td>
      </tr>
    `;
    return;
  }

  eventsTableBody.innerHTML = events.map(event => `
    <tr>
      <td>${event.id}</td>
      <td>${event.name}</td>
      <td>${event.location || "-"}</td>
      <td>${EventerApi.formatDate(event.date)}</td>
      <td>${EventerApi.formatCurrency(event.price)}</td>
      <td>${event.capacity}</td>
      <td class="actions-cell">
        <button type="button" class="secondary-button" data-action="edit" data-event-id="${event.id}">Editar</button>
        <button type="button" class="danger-button" data-action="delete" data-event-id="${event.id}">Excluir</button>
      </td>
    </tr>
  `).join("");

  eventsTableBody.querySelectorAll("[data-action='edit']").forEach(button => {
    button.addEventListener("click", () => {
      const event = currentEvents.find(item => item.id === Number(button.dataset.eventId));
      if (event) {
        populateForm(event);
      }
    });
  });

  eventsTableBody.querySelectorAll("[data-action='delete']").forEach(button => {
    button.addEventListener("click", async () => {
      const eventId = Number(button.dataset.eventId);
      if (!window.confirm(`Excluir o evento #${eventId}?`)) {
        return;
      }

      try {
        setFeedback("Excluindo evento...");
        await EventerApi.deleteEvent(eventId);
        setFeedback("Evento excluido com sucesso.", "success");
        await loadEvents();
        if (Number(fields.id.value) === eventId) {
          resetForm();
        }
      } catch (error) {
        setFeedback(`Falha ao excluir evento: ${error.message}`, "error");
      }
    });
  });
}

function getPayload() {
  return {
    id: fields.id.value ? Number(fields.id.value) : undefined,
    name: fields.name.value.trim(),
    description: fields.description.value.trim(),
    pictureUrl: fields.pictureUrl.value.trim(),
    price: Number(fields.price.value),
    capacity: Number(fields.capacity.value),
    date: new Date(fields.date.value).toISOString(),
    location: fields.location.value.trim()
  };
}

async function loadEvents() {
  try {
    currentEvents = await EventerApi.fetchEvents();
    renderEvents(currentEvents);
  } catch (error) {
    renderEvents([]);
    setFeedback(`Falha ao carregar eventos: ${error.message}`, "error");
  }
}

eventForm.addEventListener("submit", async event => {
  event.preventDefault();

  try {
    const payload = getPayload();
    setFeedback(fields.id.value ? "Atualizando evento..." : "Criando evento...");

    if (fields.id.value) {
      await EventerApi.updateEvent(payload);
      setFeedback("Evento atualizado com sucesso.", "success");
    } else {
      delete payload.id;
      await EventerApi.createEvent(payload);
      setFeedback("Evento criado com sucesso.", "success");
    }

    resetForm();
    await loadEvents();
  } catch (error) {
    setFeedback(`Falha ao salvar evento: ${error.message}`, "error");
  }
});

cancelEditButton.addEventListener("click", resetForm);

resetForm();
loadEvents();
