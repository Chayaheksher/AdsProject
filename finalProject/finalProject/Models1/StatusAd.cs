using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class StatusAd
{
    public int StatusAdId { get; set; }

    public string? StatusAdName { get; set; }

    public virtual ICollection<Ad> Ads { get; set; } = new List<Ad>();

    public virtual ICollection<Ishurim> IshurimKodStatusInIshurNavigations { get; set; } = new List<Ishurim>();

    public virtual ICollection<Ishurim> IshurimKodStatusInRejectionNavigations { get; set; } = new List<Ishurim>();

    public virtual ICollection<Ishurim> IshurimStatusAds { get; set; } = new List<Ishurim>();
}
