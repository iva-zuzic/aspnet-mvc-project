using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarketplace.Model;

public class DrustvenaIgra
{
    [Key]
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public int MinBrojIgraca { get; set; }
    public int MaxBrojIgraca { get; set; }
    public int MinimalnasDob { get; set; }
    public int TrajanjeMins { get; set; }
    public ZanrIgre Zanr { get; set; }

    [NotMapped]
    public string BrojIgracaPrikaz =>
        MinBrojIgraca == MaxBrojIgraca
            ? MinBrojIgraca.ToString()
            : $"{MinBrojIgraca}–{MaxBrojIgraca}";

    [ForeignKey("Oglas")]
    public int OglasId { get; set; }
    public virtual Oglas Oglas { get; set; } = null!;
}
