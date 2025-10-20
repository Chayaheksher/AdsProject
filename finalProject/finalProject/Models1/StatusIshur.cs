using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class StatusIshur
{
    public int StatusIshurId { get; set; }

    public string? StatusIshurName { get; set; }

    public virtual ICollection<IshurForAd> IshurForAds { get; set; } = new List<IshurForAd>();

    public virtual ICollection<PublicationDate> PublicationDates { get; set; } = new List<PublicationDate>();
}
