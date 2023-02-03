// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const btn = document.getElementById("greet");
const show = document.getElementById("count");

let count = 0

btn.addEventListener("click", () => {
    count += 1;
    console.log("Hello world")
    show.innerText = count
    play()
})

function play() {
    var audio = new Audio(
'https://media.geeksforgeeks.org/wp-content/uploads/20190531135120/beep.mp3');
    audio.play()
    audio.pause();
}