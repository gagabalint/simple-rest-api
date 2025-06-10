Focicsapat Felállítás Generátor
Full-stack alkalmazás különböző focicsapat felállítások generálásához és vizualizálásához. A rendszer REST API architektúrán alapul, C# ASP.NET backend-del és JavaScript/HTML/CSS frontend-del.
Funkciók

Kezdő 11 generálás - Különböző értelmes felállások létrehozása
Felállás értékelés - Minden felálláshoz jósági érték társítása
Színkódolt megjelenítés - Bootstrap card-ok színes kategorizálással
Többféle formáció - 4-4-2, 4-3-3, 3-5-2 
Reszponzív design - Bootstrap keretrendszerrel

Technológiák
Backend

C# ASP.NET Core - REST API szerver
Controller & Model architektúra
Memóriában történő adattárolás

Frontend

JavaScript - Dinamikus funkciók
HTML5 & CSS3 - Felhasználói interfész
Bootstrap 5 - Reszponzív design és komponensek

Követelmények

.NET 8.0
Modern webböngésző (Chrome, Firefox, Edge)
Visual Studio 2022 és VS Code

Telepítés és futtatás
1. Repository klónozása
````bash
git clone  https://github.com/gagabalint/simple-rest-api.git
````
3. Backend indítása
A Backend mappán belül a KEZDOCSAPATXI mappában:
Dupla klikk a `.sln` fájlra és `http` protokoll kiválasztása után futtatás.

A szerver alapértelmezetten a https://localhost:5209 címen indul.

5. Frontend megnyitása
# VS Codeban Live Server segítségével futtatás

🎯 Használat
1. Játékos adatok megadása

Add meg a focicsapat játékosainak nevét és posztját
Legalább 11 játékos szükséges a felállításokhoz
Csereplayers is megadhatók

2. Felállítások generálása

Kattints a "Felállítások generálása" gombra
A rendszer automatikusan létrehoz különböző formációkat

3. Eredmények megtekintése

Zöld kártyák - Legjobb felállások
Sárga kártyák - Közepes minőségű felállások
Piros kártyák - Gyengébb felállások
