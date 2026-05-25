const cart = [

  {
    id: 1,
    name: "MC IG",
    date: "20/06/2026",
    location: "Marília - SP",
    price: 200,
    quantity: 2,
    imageUrl: "../img/mcig.png"
  },

  {
    id: 2,
    name: "Show Anitta",
    date: "25/06/2026",
    location: "Marília - SP",
    price: 180,
    quantity: 1,
    imageUrl: "../img/anitta.png"
  }

];

const cartItems = document.getElementById("cartItems");

let subtotal = 0;

cart.forEach(item => {

  subtotal += item.price;

  cartItems.innerHTML += `

    <div class="cart-card">

      <div class="card-left">

        <img src="${item.imageUrl}">

        <div class="event-info">

          <h3>${item.name}</h3>

          <p>${item.date}</p>

          <p>${item.location}</p>

        </div>

      </div>

      <div class="card-right">

        <div class="quantity-box">

          <button>-</button>

          <span>${item.quantity}</span>

          <button>+</button>

        </div>

        <div class="card-price">
          R$ ${item.price},00
        </div>

        <i class="fa-regular fa-trash-can remove-btn"></i>

      </div>

    </div>

  `;
});

document.getElementById("subtotal")
.innerText = `R$ ${subtotal},00`;

document.getElementById("totalPrice")
.innerText = `R$ ${subtotal + 19},00`;