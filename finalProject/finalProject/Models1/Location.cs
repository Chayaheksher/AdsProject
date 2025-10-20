using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class Location
{
    public int LocationId { get; set; }

    public string? DescriptionLocations { get; set; }

    public decimal? PriceLocations { get; set; }

    public virtual ICollection<Ad> Ads { get; set; } = new List<Ad>();

    public virtual ICollection<PriceList> PriceLists { get; set; } = new List<PriceList>();
}
