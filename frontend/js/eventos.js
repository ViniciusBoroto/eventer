const eventos = [

  {
    Id: 1,

    Name: "MC IG",

    Date: "20/06/2026",

    Location: "Marília - SP",

    Price: 100,

    Image: "../img/mcig.png"
  },

  {
    Id: 2,

    Name: "Show Anitta",

    Date: "25/06/2026",

    Location: "Marília - SP",

    Price: 180,

    Image: "img/anitta.jpg"
  }

];

const grid = document.getElementById("eventsGrid");

eventos.forEach(evento => {

  grid.innerHTML += `

    <div class="event-card">

      <img src="${evento.Image}">

      <div class="event-info">

        <h3>${evento.Name}</h3>

        <p>${evento.Date}</p>

        <p>${evento.Location}</p>

        <h4>R$ ${evento.Price}</h4>

        <button onclick="goToDetails(${evento.Id})">
        Ver detalhes 
        </button>

      </div>

    </div>

  `;

});

function abrirEvento(id){

  window.location.href =
  `details_event.html?id=${id}`;

}

function goToDetails(id){

  window.location.href =
  `details_event.html?id=${id}`;

}