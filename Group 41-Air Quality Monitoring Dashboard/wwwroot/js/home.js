// JavaScript specific to Home view
document.addEventListener("DOMContentLoaded", function () {
    console.log("Homepage script loaded successfully.");

    const carouselElement = document.getElementById("homeCarousel");
    if (carouselElement) {
        const carousel = new bootstrap.Carousel(carouselElement, {
            interval: 5000,
            ride: "carousel"
        });
    }
});
