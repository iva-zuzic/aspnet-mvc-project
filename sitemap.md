# Sitemap

## Pregled
Aplikacija koristi kombinaciju konvencionalnog MVC routinga i atributnog routinga.

Konvencionalna (default) ruta:
- /{controller=Home}/{action=Index}/{id?}

Atributne rute:
- Knjige: /oglasi/knjige i /oglasi/knjige/{id}
- Drustvene igre: /oglasi/igre i /oglasi/igre/{id}
- Gradovi: /gradovi i /gradovi/{id}

## Stranice po kontroleru
- HomeController
- /, /Home, /Home/Index
- /Home/Privacy
- /Home/Error

- KnjigaController
- /oglasi/knjige
- /oglasi/knjige/{id}

- DrustvenaIgraController
- /oglasi/igre
- /oglasi/igre/{id}

- GradController
- /gradovi
- /gradovi/{id}

- OglasController
- /Oglas, /Oglas/Index
- /Oglas/Details/{id?}

- KorisnikController
- /Korisnik, /Korisnik/Index
- /Korisnik/Details/{id?}

- PorukaController
- /Poruka, /Poruka/Index
- /Poruka/Details/{id?}

## Kljucne putanje
- / (pocetna stranica)
- /oglasi/knjige (popis knjiga)
- /oglasi/knjige/1 (detalji knjige/oglasa)
- /oglasi/igre (popis drustvenih igara)
- /oglasi/igre/1 (detalji igre/oglasa)
- /gradovi (popis gradova)
- /gradovi/1 (detalji grada)
- /Poruka (inbox poruke)
- /Home/Privacy (privatnost)

## Napomene
- U glavnoj navigaciji su istaknute putanje: Pocetna, Gradovi, Knjige, Igre, Inbox i Privatnost.
- Za akcije s id parametrom vrijedi:
- Kod atributnih ruta id je dio putanje (npr. /gradovi/{id}).
- Kod konvencionalnih ruta id je opcionalni segment u obrascu, ali bez valjanog id-a detalji najcesce vracaju NotFound.
- Nisu definirane HttpPost/HttpPut/HttpDelete akcije u analiziranim kontrolerima; efektivno su izlozeni GET endpointi.
