//Giam so luong
function decrementValue() {
  let quantityInput = document.getElementById("quantity");
  let currentValue = parseInt(quantityInput.value);
  if (currentValue > 1) {
    quantityInput.value = currentValue - 1;
  }
}

//Tang so luong
function incrementValue() {
  let quantityInput = document.getElementById("quantity");
  let currentValue = parseInt(quantityInput.value);
  quantityInput.value = currentValue + 1;
}

//click-image
const activeImage = document.querySelector(".img-main img");
const productImages = document.querySelectorAll(".img-extra img");
function changeImage(e) {
  activeImage.src = e.target.src;
}
productImages.forEach((image) => image.addEventListener("click", changeImage));

//size-chart
const sizeChartBtn = document.querySelector(".size-chart");
const showSizeChart = document.querySelector(".show-size-chart"); 
const closeSizeChart1 = document.querySelector(".close-size-chart1");
const closeSizeChart2 = document.querySelector(".close-size-chart2"); 
function showUpdateInforDT() {
  showSizeChart.classList.add("open");
}
function hideUpdateInforDT() {
  showSizeChart.classList.remove("open");
}
sizeChartBtn.addEventListener("click", showUpdateInforDT);
closeSizeChart1.addEventListener("click", hideUpdateInforDT);
closeSizeChart2.addEventListener("click", hideUpdateInforDT);


/* click button */
function changeColor(button, group) {
  const buttons = document.querySelectorAll(`.${group}-product button`);
  buttons.forEach(btn => {
      btn.classList.remove('active');
      btn.style.backgroundColor = 'white';
      btn.style.color = '#182853';
  });
  button.classList.add('active');
  button.style.backgroundColor = '#182853';
  button.style.color = 'white';
}