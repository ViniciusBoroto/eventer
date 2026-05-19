// PEGAR ID DA URL

const params =
new URLSearchParams(window.location.search);

const id = params.get("id");


// LISTA DE EVENTOS

const eventos = [

  {
    Id: 1,

    Name: "MC IG",

    EventDate: "20 de Junho de 2026",

    EventTime: "22:00",

    Location: "Marília - SP",

    Description: "Venha curtir o melhor do funk com MC IG, o fenômeno do momento! Com seus hits contagiantes e energia única, MC IG promete uma noite inesquecível de muita música e diversão. Não perca a chance de dançar ao som dos maiores sucessos do funk e se divertir com os amigos. Garanta já seu ingresso e prepare-se para uma experiência incrível com MC IG!",

    Price: 100,

    Capacity: 500,

    AvailableTickets: 15,

    Image: "../img/mcig.png"
  },

  {
    Id: 2,

    Name: "Show Anitta",

    EventDate: "25 de Junho de 2026",

    EventTime: "20:00",

    Location: "Marília - SP",

    Price: 180,

    Capacity: 300,

    AvailableTickets: 50,

    Image: "../img/anitta.jpg"
  },

  {
    Id: 3,

    Name: "Show Marília Mendonça",

    EventDate: "30 de Junho de 2026",

    EventTime: "21:00",

    Location: "Marília - SP",

    Price: 150,

    Capacity: 400,

    AvailableTickets: 80,

    Image: "../img/marilia.jpg"
  }

];


// BUSCAR EVENTO PELO ID

const evento = eventos.find(
  e => e.Id == id
);


// PREENCHER TELA

document.getElementById("eventName")
.innerText = evento.Name;

document.getElementById("eventDate")
.innerText = evento.EventDate;

document.getElementById("eventTime")
.innerText = evento.EventTime;

document.getElementById("eventLocation")
.innerText = evento.Location;

document.getElementById("eventDescription")
.innerText = evento.Description;

document.getElementById("eventPrice")
.innerText = `R$ ${evento.Price},00`;

document.getElementById("eventTickets")
.innerText =
`${evento.AvailableTickets} / ${evento.Capacity}`;

document.getElementById("eventImage")
.src = evento.Image;


// QUANTIDADE DE INGRESSOS

let quantity = 1;

const quantityText =
document.getElementById("quantity");

const plusBtn =
document.getElementById("plusBtn");

const minusBtn =
document.getElementById("minusBtn");


// AUMENTAR

plusBtn.addEventListener("click", () => {

  if(quantity < evento.AvailableTickets){

    quantity++;

    quantityText.innerText = quantity;
  }

});


// DIMINUIR

minusBtn.addEventListener("click", () => {

  if(quantity > 1){

    quantity--;

    quantityText.innerText = quantity;
  }

});