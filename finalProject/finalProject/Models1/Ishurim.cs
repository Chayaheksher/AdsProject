using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class Ishurim
{
    public int KodIshur { get; set; }

    public int? CharactersId { get; set; }

    public string? Description { get; set; }

    public string? RejectionDescribetion { get; set; }

    public string? IshurDescribetion { get; set; }

    public bool? DateDepending { get; set; }

    public int? KodStatusInIshur { get; set; }

    public int? KodStatusInRejection { get; set; }

    public int? StatusAdId { get; set; }

    public int? CharactersIdinIshur { get; set; }

    public int? CharactersIdinRejection { get; set; }

    public int? CharactersIdinsertRow { get; set; }

    public virtual Character? Characters { get; set; }

    public virtual Character? CharactersIdinIshurNavigation { get; set; }

    public virtual Character? CharactersIdinRejectionNavigation { get; set; }

    public virtual Character? CharactersIdinsertRowNavigation { get; set; }

    public virtual ICollection<IshurForAd> IshurForAds { get; set; } = new List<IshurForAd>();

    public virtual StatusAd? KodStatusInIshurNavigation { get; set; }

    public virtual StatusAd? KodStatusInRejectionNavigation { get; set; }

    public virtual StatusAd? StatusAd { get; set; }
}
