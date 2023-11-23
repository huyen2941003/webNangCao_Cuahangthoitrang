var map = L.map('map').setView([21.0642985, 105.7801634], 12);
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors',
    maxZoom: 18,
}).addTo(map);
L.marker([21.0642985, 105.7801634]).addTo(map);