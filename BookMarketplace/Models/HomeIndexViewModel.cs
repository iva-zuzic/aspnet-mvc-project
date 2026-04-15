namespace BookMarketplace.Models;

public class HomeIndexViewModel
{
    public List<Knjiga> Knjige { get; set; } = [];
    public List<DrustvenaIgra> Igre { get; set; } = [];
    public List<Oglas> NajnovijiOglasi { get; set; } = [];
}
