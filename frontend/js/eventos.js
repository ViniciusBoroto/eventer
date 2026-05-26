const grid = document.getElementById("eventsGrid");
const searchInput = document.getElementById("searchInput");
const cityFilter = document.getElementById("cityFilter");
const resultsTitle = document.getElementById("resultsTitle");
const feedback = document.getElementById("feedbackMessage");

let allEvents = [];

function goToDetails(id) {
  window.location.href = `details_event.html?id=${id}`;
}

function buildCityOptions(events) {
  const cities = [...new Set(events.map(event => event.location).filter(Boolean))].sort();
  cityFilter.innerHTML = [
    '<option value="">Todas as cidades</option>',
    ...cities.map(city => `<option value="${city}">${city}</option>`)
  ].join("");
}

function renderEvents(events) {
  if (!events.length) {
    grid.innerHTML = "";
    feedback.textContent = "Nenhum evento encontrado com os filtros atuais.";
    return;
  }

  feedback.textContent = "";
  grid.innerHTML = events.map(event => `
    <article class="event-card">
      <img src="${event.pictureUrl}" alt="Imagem do evento ${event.name}">
      <div class="event-info">
        <h3>${event.name}</h3>
        <p>${EventerApi.formatDate(event.date)}</p>
        <p>${event.location || "Local a definir"}</p>
        <h4>${EventerApi.formatCurrency(event.price)}</h4>
        <button type="button" data-event-id="${event.id}">Ver detalhes</button>
      </div>
    </article>
  `).join("");

  grid.querySelectorAll("[data-event-id]").forEach(button => {
    button.addEventListener("click", () => goToDetails(button.dataset.eventId));
  });
}

function applyFilters() {
  const search = searchInput.value.trim().toLowerCase();
  const city = cityFilter.value;

  const filteredEvents = allEvents.filter(event => {
    const matchesSearch = !search
      || event.name.toLowerCase().includes(search)
      || event.location.toLowerCase().includes(search)
      || event.description.toLowerCase().includes(search);

    const matchesCity = !city || event.location === city;

    return matchesSearch && matchesCity;
  });

  resultsTitle.textContent = city || "todos os locais";
  renderEvents(filteredEvents);
}

async function loadEvents() {
  try {
    feedback.textContent = "Carregando eventos...";
    allEvents = await EventerApi.fetchEvents();
    buildCityOptions(allEvents);
    applyFilters();
  } catch (error) {
    grid.innerHTML = "";
    feedback.textContent = `Falha ao carregar eventos da API: ${error.message}`;
  }
}

searchInput.addEventListener("input", applyFilters);
cityFilter.addEventListener("change", applyFilters);

loadEvents();
