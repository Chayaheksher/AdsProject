using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class Ad
{
    public int AdId { get; set; }

    public DateTime? DateOrder { get; set; }

    public int? CustomerId { get; set; }

    public string? Content { get; set; }

    public int? AdStatusId { get; set; }

    public int? AdTypeId { get; set; }

    public int? SizeId { get; set; }

    public int? LocationId { get; set; }

    public int? SectionId { get; set; }

    public bool? Bold { get; set; }

    public bool? Along { get; set; }

    public virtual StatusAd? AdStatus { get; set; }

    public virtual AdType? AdType { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<EngagedDetail> EngagedDetails { get; set; } = new List<EngagedDetail>();

    public virtual ICollection<IshurForAd> IshurForAds { get; set; } = new List<IshurForAd>();

    public virtual Location? Location { get; set; }

    public virtual ICollection<PublicationDate> PublicationDates { get; set; } = new List<PublicationDate>();

    public virtual Section? Section { get; set; }

    public virtual SizesDescription? Size { get; set; }
}
