using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class AdType
{
    public int AdTypeId { get; set; }

    public string? DescriptionAdTypes { get; set; }

    public decimal? PriceAdType { get; set; }

    public virtual ICollection<Ad> Ads { get; set; } = new List<Ad>();

    public virtual ICollection<PriceList> PriceLists { get; set; } = new List<PriceList>();
}
