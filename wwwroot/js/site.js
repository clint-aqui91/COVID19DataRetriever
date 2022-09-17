// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// This file contains all the JavaScript functions. JavaScript functions were not put in a view, to improve code re-usability, in other words, multiple views/pages can make use of the JavaScript functions contained here.

// Function which draws the Google Map and displays it on the webpage, once page is loaded (from the API's URL callback which calls this function, once the page/view is fully loaded).
// NOTE: If Google Map displays a message: "This page can't load Google Maps correctly.", the internet browser's console was checked and an error message was found stating that no API key was used.
function drawGoogleMap() {

    // Google Maps coordinates for Malta
    const Malta = { lat: 35.9417696, lng: 14.3959322 };

    // Map properties
    const Map = new google.maps.Map(document.getElementById("map"), {
        zoom: 10.5,
        center: Malta,
    });

    const Marker = new google.maps.Marker({
        position: Malta,
        Map: Map,
    });

}

window.drawGoogleMap = drawGoogleMap;


// JavaScript function which calculates the death ratio based on the passed arguemnts (Confirmed Cases and Confirmed Deaths)
function calculateDeathRatio(confirmedCases, confirmedDeaths) {

    var ratio = confirmedDeaths / confirmedCases;

    // alert('You clicked !' + confirmedCases + confirmedDeaths + ratio);

    // if the fourth decimal place is 0, it will not be displayed (eg. 0.00701 will set value 0.007 and if value is 0.00711, it will set value to 0.0071)
    ratio = Math.round(ratio * 10000) / 10000

    // Display the ratio value in the HTML element with ID deathratio1
    document.getElementById("deathratio1").value = ratio;

}


