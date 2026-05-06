# Semantic Model

## Overview
Model pokriva marketplace domenu za oglase knjiga i drustvenih igara. Sredisnji entitet je Oglas, koji povezuje korisnika, grad, sadrzaj artikla (Knjiga ili DrustvenaIgra), slike, favorite i poruke.

## Entiteti

### DrustvenaIgra
- Id: int (required)
- Naziv: string (required)
- MinBrojIgraca: int (required)
- MaxBrojIgraca: int (required)
- MinimalnasDob: int (required)
- TrajanjeMins: int (required)
- Zanr: ZanrIgre (required)
- OglasId: int (required, FK)
- Oglas: Oglas (required navigacija)

### Grad
- Id: int (required)
- Naziv: string (required)
- PostanskiBroj: string (required)
- Oglasi: ICollection<Oglas> (inicijalizirano)

### Favorit
- KorisnikId: int (required, FK)
- Korisnik: Korisnik (required navigacija)
- OglasId: int (required, FK)
- Oglas: Oglas (required navigacija)
- DatumDodavanja: DateTime (required)

### HomeIndexViewModel (view model)
- Knjige: List<Knjiga>
- Igre: List<DrustvenaIgra>
- NajnovijiOglasi: List<Oglas>

### Knjiga
- Id: int (required)
- Naziv: string (required)
- Autor: string (required)
- ISBN: string (required)
- Izdavac: string (required)
- GodinaIzdanja: int (required)
- Jezik: string (required)
- Zanr: ZanrKnjige (required)
- OglasId: int (required, FK)
- Oglas: Oglas (required navigacija)

### Korisnik
- Id: int (required)
- ImeIPrezime: string (required)
- Email: string (required)
- Lozinka: string (required)
- Telefon: string (required)
- DatumRegistracije: DateTime (required)
- Uloga: UlogaKorisnika (required)
- Oglasi: ICollection<Oglas>
- Favoriti: ICollection<Favorit>
- PoslanePoruke: ICollection<Poruka>
- PrimljenePoruke: ICollection<Poruka>

### Oglas
- Id: int (required)
- Naslov: string (required)
- Opis: string (required)
- Cijena: decimal (required)
- DatumObjave: DateTime (required)
- DatumIzmjene: DateTime? (optional)
- Status: StatusOglasa (required)
- TipOglasa: TipOglasa (required)
- StanjeArtikla: StanjeArtikla (required)
- KorisnikId: int (required, FK)
- Korisnik: Korisnik (required navigacija)
- GradId: int (required, FK)
- Grad: Grad (required navigacija)
- Knjiga: Knjiga? (optional navigacija)
- DrustvenaIgra: DrustvenaIgra? (optional navigacija)
- Slike: ICollection<Slika>
- Favoriti: ICollection<Favorit>
- Poruke: ICollection<Poruka>

### Poruka
- Id: int (required)
- Sadrzaj: string (required)
- Procitano: bool (required)
- DatumSlanja: DateTime (required)
- PosiljateljId: int (required, FK na Korisnik)
- Posiljatelj: Korisnik (required navigacija)
- PrimateljId: int (required, FK na Korisnik)
- Primatelj: Korisnik (required navigacija)
- OglasId: int (required, FK)
- Oglas: Oglas (required navigacija)

### Slika
- Id: int (required)
- Putanja: string (required)
- RedoslijedPrikaza: int (required)
- OglasId: int (required, FK)
- Oglas: Oglas (required navigacija)

### ErrorViewModel (tehnicki/view model)
- RequestId: string? (optional)
- ShowRequestId: bool (izracunato)

## Enumeracije

### UlogaKorisnika
- Korisnik
- Admin

### StatusOglasa
- Aktivan
- Neaktivan
- Prodan
- Izbrisan

### TipOglasa
- Knjiga
- DrustvenaIgra

### StanjeArtikla
- Novo
- KaoNovo
- Dobro
- Prihvatljivo
- Losije

### ZanrKnjige
- Fantastika
- Triler
- Romansa
- ZnanstvenaFantastika
- Krimi
- Drama
- Biografija
- Strucna

### ZanrIgre
- Strategija
- Kooperativna
- Pustolovinska
- Apstraktna
- Zabavna
- Edukativna

## Relacije
- Korisnik (1) - (N) Oglas
- Grad (1) - (N) Oglas
- Oglas (1) - (N) Slika
- Oglas (1) - (N) Poruka
- Korisnik (1) - (N) Poruka kao posiljatelj
- Korisnik (1) - (N) Poruka kao primatelj
- Korisnik (N) - (M) Oglas preko Favorit, s atributom DatumDodavanja
- Oglas (1) - (0..1) Knjiga
- Oglas (1) - (0..1) DrustvenaIgra

## Poslovna pravila (inferirano)
- Oglas predstavlja tocno jednu vrstu artikla: Knjiga ili DrustvenaIgra.
- TipOglasa treba biti uskladjen s prisutnom detaljnom navigacijom.
- Favorit treba biti jedinstven po paru KorisnikId + OglasId.
- Poruka je uvijek vezana uz konkretan oglas i oba sudionika.
- DatumIzmjene je prisutan samo nakon izmjene oglasa.

## Napomene
- Nullability i tipovi sugeriraju required/optional semantiku.
- U modelima nema detaljnih validacijskih anotacija poput MaxLength/Range.
- Primarni kljuc za Favorit nije vidljiv u samoj klasi; ocekuje se konfiguracija u EF Core.
- Svojstvo MinimalnasDob moguc je nazivni tipfeler (vjerojatno MinimalnaDob).
