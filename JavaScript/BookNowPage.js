
    const currentDateElement = document.getElementById('current-date');
    const today = new Date();
    const formattedDate = today.toLocaleDateString('en-IE', {weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' });
    currentDateElement.textContent = formattedDate;


    const tourPrices = {
    1: 60,
    2: 70,
    3: 80, 
    4: 40,
    5: 50
    };

   
function updateTotalPrice() {
   
    const tourRadio = document.querySelector('input[name="TourID"]:checked');
    const tourId = tourRadio ? tourRadio.value : null;
    const participants = parseInt(document.getElementById('NumberOfParticipants').value) || 0;
    const price = tourPrices[tourId] || 0;
    const total = participants * price;

    document.getElementById('displayTotalPrice').textContent = `EUR ${total.toFixed(2)}`;
    document.getElementById('TotalPrice').value = total;
}


document.querySelectorAll('input[name="TourID"]').forEach(radio => {
    radio.addEventListener('change', updateTotalPrice);
});
document.getElementById('NumberOfParticipants').addEventListener('input', updateTotalPrice);


updateTotalPrice();
