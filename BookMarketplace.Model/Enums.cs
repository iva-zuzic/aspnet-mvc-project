namespace BookMarketplace.Model;
public enum UlogaKorisnika
{
    Korisnik,
    Admin
}

public enum StatusOglasa
{
    Aktivan,
    Neaktivan,
    Prodan,
    Izbrisan
}

public enum TipOglasa
{
    Knjiga,
    DrustvenaIgra
}

public enum StanjeArtikla
{
    Novo,
    KaoNovo,
    Dobro,
    Prihvatljivo,
    Losije
}

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

public enum ZanrIgre
{
    Strategija,
    Kooperativna,
    Pustolovinska,
    Apstraktna,
    Zabavna,
    Edukativna
}
