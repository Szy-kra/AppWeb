// CottageMore.js

function openModal(src) {
    const modal = document.getElementById("imageModal");
    const fullImg = document.getElementById("fullImage");

    if (modal && fullImg) {
        modal.style.display = "block";
        fullImg.src = src;
        document.body.style.overflow = "hidden"; // Blokuje przewijanie strony pod zdjęciem
    }
}

function closeModal() {
    const modal = document.getElementById("imageModal");
    if (modal) {
        modal.style.display = "none";
        document.body.style.overflow = "auto"; // Przywraca przewijanie
    }
}

// Zamykanie klawiszem ESC
document.addEventListener('keydown', function (event) {
    if (event.key === "Escape") {
        closeModal();
    }
});