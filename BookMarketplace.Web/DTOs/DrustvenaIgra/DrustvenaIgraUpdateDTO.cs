using System.ComponentModel.DataAnnotations;
using BookMarketplace.Model;

namespace BookMarketplace.Web.DTOs.DrustvenaIgra;

public class DrustvenaIgraUpdateDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Naziv { get; set; } = string.Empty;

    [Range(1, 100)]
    public int MinBrojIgraca { get; set; }

    [Range(1, 100)]
    public int MaxBrojIgraca { get; set; }

    [Range(0, 100)]
    public int MinimalnaDob { get; set; }

    [Range(1, 1000)]
    public int TrajanjeMins { get; set; }

    [Required]
    public ZanrIgre Zanr { get; set; }
}