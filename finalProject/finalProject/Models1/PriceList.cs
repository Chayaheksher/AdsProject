using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class PriceList
{
    public int PriceId { get; set; }

    public int? AdTypeId { get; set; }

    public int? SizeId { get; set; }

    public int? LocationId { get; set; }

    public int? SectionId { get; set; }

    public bool? Bold { get; set; }

    public int? Price { get; set; }

    public virtual AdType? AdType { get; set; }

    public virtual Location? Location { get; set; }

    public virtual ICollection<PublicationDate> PublicationDates { get; set; } = new List<PublicationDate>();

    public virtual Section? Section { get; set; }

    public virtual SizesDescription? Size { get; set; }
}
