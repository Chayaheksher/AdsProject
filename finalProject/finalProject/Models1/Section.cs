using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class Section
{
    public int SectionId { get; set; }

    public int? ParentSection { get; set; }

    public string? SectionName { get; set; }

    public virtual ICollection<Ad> Ads { get; set; } = new List<Ad>();

    public virtual ICollection<Section> InverseParentSectionNavigation { get; set; } = new List<Section>();

    public virtual Section? ParentSectionNavigation { get; set; }

    public virtual ICollection<PriceList> PriceLists { get; set; } = new List<PriceList>();
}
