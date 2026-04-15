namespace BookMarketplace.Models;

// Uloga korisnika u sustavu
public enum UlogaKorisnika
{
    Korisnik,
    Admin
}

// Trenutni status oglasa
public enum StatusOglasa
{
    Aktivan,
    Neaktivan,
    Prodan,
    Izbrisan
}

// Tip artikla koji se prodaje
public enum TipOglasa
{
    Knjiga,
    DrustvenaIgra
}

// Fizičko stanje artikla
public enum StanjeArtikla
{
    Novo,
    KaoNovo,
    Dobro,
    Prihvatljivo,
    Losije
}

// Književni žanr knjige
public enum ZanrKnjige
{
    Fantastika,
    Triler,
    Romansa,
    ZnanstvenaFantastika,
    Krimi,
    Drama,
    Biografija,
    Strucna
}

// Žanr društvene igre
public enum ZanrIgre
{
    Strategija,
    Kooperativna,
    Pustolovinska,
    Apstraktna,
    Zabavna,
    Edukativna
}
