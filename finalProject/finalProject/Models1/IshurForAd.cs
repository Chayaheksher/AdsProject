using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class IshurForAd
{
    public int? AdId { get; set; }

    public int? PublicationDateId { get; set; }

    public int? KodIshur { get; set; }

    public DateTime? ExecutionDate { get; set; }

    public string? Note { get; set; }

    public int? UserId { get; set; }

    public int? KodStatusIshur { get; set; }

    public int? CharactersIdinsertRow { get; set; }

    public int IshurForAdId { get; set; }

    public virtual Ad? Ad { get; set; }

    public virtual Character? CharactersIdinsertRowNavigation { get; set; }

    public virtual Ishurim? KodIshurNavigation { get; set; }

    public virtual StatusIshur? KodStatusIshurNavigation { get; set; }

    public virtual PublicationDate? PublicationDate { get; set; }

    public virtual User? User { get; set; }
}
