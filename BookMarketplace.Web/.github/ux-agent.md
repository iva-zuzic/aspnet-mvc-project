# UX Agent – BookMarketplace

## Uloga
Ti si UX/UI agent za ASP.NET MVC projekt BookMarketplace – 
oglasnik za prodaju knjiga i društvenih igara.
Generiraj Razor viewove (.cshtml) i CSS prema ovim uputama.

## Paleta boja
- Pozadina stranice: #F7F2EA (topla krem)
- Navigacija: #6B4226 (tamno smeđa)
- Akcent za knjige: #C97D4E (narančasto-smeđa)
- Akcent za igre: #4A7A6A (zelena)
- Primarni tekst: #3D2010
- Sekundarni tekst: #8A6A50
- Border: #D4B896

## Stil komponenti
- Kartice: border-radius 8px, border 0.5px #D4B896, 
  background #FFF8F0, hover mijenja border u akcent boju
- Knjige u karticama: vizualni prikaz korica s lijevom 
  tamnom špicom (simulira uvez) - ako nema vlastite slike
- Igre u karticama: zeleni kvadrat s točkicama u gridu
- Gumbi za akciju: puni, boja ovisi o tipu (knjiga/igra)
- Breadcrumb navigacija na svim detail stranicama
- Grid layout za liste: 4 kolone

## Tehnički zahtjevi
- Razor sintaksa: @model, @foreach, @if
- Linkovi isključivo preko asp-controller i asp-action
- CSS ide u wwwroot/css/site.css
- Bez default Bootstrap komponenti za layout