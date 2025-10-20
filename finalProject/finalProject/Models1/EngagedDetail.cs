using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class EngagedDetail
{
    public int EngagedId { get; set; }

    public int? AdId { get; set; }

    public string? ChatanFirstName { get; set; }

    public string? ChatanLastName { get; set; }

    public string? ChatanFatherName { get; set; }

    public string? YeshivaName { get; set; }

    public string? ChatanCity { get; set; }

    public string? CalaFirstName { get; set; }

    public string? CalaLastName { get; set; }

    public string? CalaFatherName { get; set; }

    public string? SeminarName { get; set; }

    public string? CalaCity { get; set; }

    public DateTime? DateEngagement { get; set; }

    public virtual Ad? Ad { get; set; }
}
