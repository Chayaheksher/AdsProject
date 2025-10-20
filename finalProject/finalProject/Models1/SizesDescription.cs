using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class SizesDescription
{
    public int SizeDescriptionId { get; set; }

    public string? DescriptionSizes { get; set; }

    public decimal? PriceSize { get; set; }

    public virtual ICollection<Ad> Ads { get; set; } = new List<Ad>();

    public virtual ICollection<PriceList> PriceLists { get; set; } = new List<PriceList>();

    public virtual ICollection<Size> Sizes { get; set; } = new List<Size>();
}
