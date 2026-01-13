/**
 * Obsługa podglądu zdjęć z dynamicznym przyciskiem usuwania
 */


function previewImage(input, index) {
    const preview = document.getElementById('preview-' + index);
    const content = document.getElementById('content-' + index);
    const container = input.closest('.card'); // Szuka najbliższego div z klasą card

    if (input.files && input.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            // 1. Ustaw podgląd zdjęcia
            preview.src = e.target.result;
            preview.style.display = 'block';

            // 2. Ukryj ikonę "+" i tekst środkowy
            if (content) content.style.display = 'none';

            // 3. Usuń stary przycisk X jeśli już tam był
            let oldBtn = container.querySelector('.action-btn-remove');
            if (oldBtn) oldBtn.remove();

            // 4. Stwórz nowy przycisk "X"
            const btn = document.createElement('div');
            btn.innerHTML = '×';
            btn.className = 'action-btn-remove';

            // 5. Logika kliknięcia w "X"
            btn.onclick = function (event) {
                event.preventDefault();
                event.stopPropagation();

                input.value = "";               // Czyści pole pliku
                preview.src = "#";              // Resetuje podgląd obrazka
                preview.style.display = 'none'; // Ukrywa obrazek

                if (content) content.style.display = 'block'; // Pokazuje ikonę "+"

                this.remove();                  // Usuwa samo czerwone kółko
            };

            // 6. Dodaj przycisk do kontenera
            container.appendChild(btn);
        };

        reader.readAsDataURL(input.files[0]);
    }
}


  /* CreateImage*/

function previewImage(input, index) {
    const preview = document.getElementById('preview-' + index);
    const content = document.getElementById('content-' + index);
    const container = input.closest('.card');

    if (input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = function (e) {
            preview.src = e.target.result;
            preview.style.display = 'block';
            if (content) content.style.display = 'none';

            let oldBtn = container.querySelector('.action-btn-remove');
            if (oldBtn) oldBtn.remove();

            const btn = document.createElement('div');
            btn.innerHTML = '×';
            btn.className = 'action-btn-remove';

            btn.onclick = function (event) {
                event.preventDefault();
                event.stopPropagation();
                input.value = "";
                preview.src = "#";
                preview.style.display = 'none';
                if (content) content.style.display = 'block';
                this.remove();
            };

            container.appendChild(btn);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

/*Rejestracja uzytkownika */

         document.addEventListener("DOMContentLoaded", function () {
        // 1. Tłumaczenie nagłówków i tekstów na stronie Register/Login
        const translations = {
             "Register": "Zarejestruj się",
         "Create a new account.": "Utwórz nowe konto.",
         "Log in": "Zaloguj się",
         "Use a local account to log in.": "Zaloguj się przez konto lokalne.",
         "Email": "Adres E-mail",
         "Password": "Hasło",
         "Confirm Password": "Potwierdź hasło",
         "Remember me?": "Zapamiętaj mnie?",
         "Forgot your password?": "Zapomniałeś hasła?",
         "Register as a new user": "Zarejestruj się jako nowy użytkownik",
         "Resend email confirmation": "Wyślij ponownie potwierdzenie e-mail"
        };

         // Funkcja zamieniająca tekst w elementach
         function translatePage() {
             // Tłumaczenie przycisków
             document.querySelectorAll("button").forEach(btn => {
                 if (translations[btn.innerText.trim()]) {
                     btn.innerText = translations[btn.innerText.trim()];
                 }
             });

            // Tłumaczenie nagłówków h1 i h2
            document.querySelectorAll("h1, h2").forEach(el => {
                if (translations[el.innerText.trim()]) {
             el.innerText = translations[el.innerText.trim()];
                }
            });

            // Tłumaczenie etykiet (labels)
            document.querySelectorAll("label").forEach(label => {
                if (translations[label.innerText.trim()]) {
             label.innerText = translations[label.innerText.trim()];
                }
            });

             // Tłumaczenie linków
            document.querySelectorAll("a").forEach(link => {
                if (translations[link.innerText.trim()]) {
             link.innerText = translations[link.innerText.trim()];
                }
            });
        }

         translatePage();
    });
    