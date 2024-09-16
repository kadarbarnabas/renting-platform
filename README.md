# 🚗🏠 Jármű és Ingatlan Bérbeadási Platform

Ez a projekt egy webalapú platform, amely lehetővé teszi felhasználók számára, hogy járműveket és ingatlanokat adjanak bérbe, illetve béreljenek. A felhasználók hirdetéseket tehetnek közzé, foglalásokat kezelhetnek, és értékeléseket adhatnak a bérbeadókról és bérleményekről.

## 🌟 Főbb Funkciók

- **🔐 Felhasználói regisztráció és bejelentkezés**: Hozz létre fiókot, vagy jelentkezz be meglévő e-mail fiókoddal. Szerepkör alapú hozzáférés (bérlő, bérbeadó, admin).
- **📋 Járművek és ingatlanok listázása**: Bérbeadók létrehozhatják és menedzselhetik hirdetéseiket képekkel, leírással és bérleti feltételekkel.
- **📅 Foglalási rendszer**: A bérlők időpontot foglalhatnak, és kapcsolatba léphetnek a bérbeadókkal.
- **⭐ Értékelési rendszer**: A bérlők értékelhetik a bérleményeket és a bérbeadókat, visszajelzést hagyhatnak.
- **⚙️ Adminisztrátori felület**: Az adminisztrátorok kezelhetik a felhasználói fiókokat, hirdetéseket és moderálhatják a platformot.
- **💳 Fizetési rendszer**: Online fizetési lehetőségek és automatikus számlázás.
- **📧 Email értesítések**: Foglalási értesítések és emlékeztetők.

## 🛠️ Technológiai Stack

- **Backend**: ASP.NET Core MVC
- **Frontend**: Razor Pages, Bootstrap, JavaScript
- **Adatbázis**: MS SQL Server
- **Külső API-k**: 
  - **📅 Google Calendar API**: Foglalások szinkronizálására a felhasználók Google Naptárával. (Ingyenes terv elérhető kisebb projektekhez.)
  - **💳 Stripe/PayPal**: Online fizetésekhez. (Ingyenes fejlesztéshez, de tranzakciós díjak vonatkoznak az éles használat során.)
  - **📧 SendGrid**: Email értesítések küldéséhez. (Havi 100 ingyenes email az ingyenes tervben.)

## 📖 Felhasználási Útmutató
 - **Felhasználói regisztráció**: A felhasználók új fiókot hozhatnak létre, vagy bejelentkezhetnek meglévő fiókjukkal.
 - **Hirdetések létrehozása**: Bérbeadók új hirdetéseket adhatnak fel képekkel, árakkal és bérleti feltételekkel.
 - **Foglalás kezelése**: A bérlők foglalást kezdeményezhetnek a hirdetéseknél megadott időpontokra.
 - **Értékelés**: A bérlet végén a bérlők értékelhetik a bérbeadót és a bérleményt.

## 👥 Közreműködők
 - **Angyal Sándor**
 - **Tomán Péter**
 - **Kádár Barnabás**