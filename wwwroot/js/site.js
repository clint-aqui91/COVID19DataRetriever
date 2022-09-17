// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.addEventListener('DOMContentLoaded', (event) => {
    console.log('DOM fully loaded and parsed');
});

// Function which draws the Google Map and displays it on the webpage, once page is loaded (from the API's URL callback which calls this function).
// NOTE: If Google Map displays a message: "This page can't load Google Maps correctly.", the internet browser's console was checked and an error message was found stating that no API key was used.

function drawGoogleMap() {
    const Malta = { lat: 35.9417696, lng: 14.3959322 };
    const Map = new google.maps.Map(document.getElementById("map"), {
        zoom: 10,
        center: Malta,
    });

}

window.drawGoogleMap = drawGoogleMap;


// JavaScript function which calculates the death ratio based on the passed arguemnts (Confirmed Cases and Confirmed Deaths)
function calculateDeathRatio(confirmedCases, confirmedDeaths) {
    //var confirmedCases = confirmedCases;
    //var confirmedDeaths = confirmedDeaths;
    var ratio = confirmedDeaths / confirmedCases;

    // alert('You clicked !' + confirmedCases + confirmedDeaths + ratio);

    // if the fourth decimal place is 0, it will not be displayed (eg. 0.00701 will set value 0.007 and if value is 0.00711, it will set value to 0.0071)
    ratio = Math.round(ratio * 10000) / 10000
    document.getElementById("deathratio1").value = ratio;
    return ratio;
}


