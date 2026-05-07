using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMarketplace.Model;

public class Poruka
{
    [Key]
    public int Id { get; set; }
    public string Sadrzaj { get; set; } = string.Empty;
    public bool Procitano { get; set; }
    public DateTime DatumSlanja { get; set; }

    [ForeignKey("Posiljatelj")]
    public int PosiljateljId { get; set; }
    public virtual Korisnik Posiljatelj { get; set; } = null!;

    [ForeignKey("Primatelj")]   
    public int PrimateljId { get; set; }
    public virtual Korisnik Primatelj { get; set; } = null!;

    [ForeignKey("Oglas")]
    public int OglasId { get; set; }
    public virtual Oglas Oglas { get; set; } = null!;
}
