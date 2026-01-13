# Cottage Management System 🏡

Profesjonalna aplikacja webowa do zarządzania ofertami domków wczasowych, zbudowana w oparciu o najnowsze standardy **.NET 8** oraz architekturę **Onion Architecture**.

## 🏗️ Architektura i Technologie
Projekt został zaprojektowany z myślą o skalowalności i czystości kodu (Clean Code):
* **Onion Architecture**: Podział na warstwy Domain, Application, Infrastructure oraz MVC.
* **CQRS (MediatR)**: Całkowite rozdzielenie operacji odczytu (Queries) od operacji zapisu (Commands).
* **Entity Framework Core**: Zaawansowane mapowanie obiektowo-relacyjne z wykorzystaniem bazy SQL Server.
* **Microsoft Identity**: Pełny system uwierzytelniania i autoryzacji użytkowników.
* **FluentValidation**: Profesjonalna walidacja danych wejściowych.

## ✨ Kluczowe Funkcjonalności
### Zarządzanie Ofertami (CRUD)
* **Dodawanie**: Intuicyjny formularz tworzenia nowej oferty z przypisaniem do zalogowanego użytkownika.
* **Edycja**: Możliwość modyfikacji wszystkich danych domku oraz zarządzanie galerią zdjęć (dodawanie/zmiana obrazów).
* **Usuwanie**: Szybkie usuwanie nieaktualnych ofert z panelu użytkownika.
* **Przeglądanie**: Zaawansowany widok szczegółów (`CottageMore`) z dynamicznym pobieraniem danych kontaktowych właściciela przez CQRS.

### Zarządzanie Profilem Użytkownika
* **Dane Kontaktowe**: Edycja numeru telefonu oraz adresu e-mail.
* **Bezpieczeństwo**: Pełna obsługa zmiany hasła i zarządzania sesją użytkownika przez Identity.

## 📂 Struktura Projektu
Aplikacja składa się z kilkunastu rozbudowanych klas, przekraczając wymogi akademickie:
1. **Cottage**: Główna encja domenowa.
2. **AppDbContext**: Konfiguracja bazy danych i Identity.
3. **CottageRepository**: Warstwa abstrakcji nad dostępem do danych.
4. **Commands & Handlers**: Logika modyfikacji danych (np. `EditCottageCommandHandler`).
5. **Queries & Handlers**: Logika pobierania danych (np. `GetContactQueryHandler`).
6. **Validators**: Reguły biznesowe dla formularzy.

## 🎨 Front-end i UI
* **Zgodność ze standardami MVC**: Wykorzystanie silnika Razor do dynamicznego generowania widoków przy zachowaniu logiki w warstwie Application [cite: 2026-01-13].
* **Bootstrap Framework**: Zastosowanie sprawdzonych klas użytkowych do budowy responsywnego interfejsu (RWD) [cite: 2026-01-13].
* **Separacja CSS**: Wszystkie niestandardowe style są wyniesione do zewnętrznych arkuszy, unikając atrybutów "style" wewnątrz tagów HTML [cite: 2026-01-11].


<img width="1888" height="877" alt="image" src="https://github.com/user-attachments/assets/1967f51b-9e91-4bc8-a0c6-1752100c2957" />

---
*Projekt przygotowany w ramach laboratorium programowania.*
