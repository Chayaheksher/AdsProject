using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class PublicationDate
{
    public int PublicationDateId { get; set; }

    public int? AdId { get; set; }

    public DateTime? PublicationDate1 { get; set; }

    public int? PriceId { get; set; }

    public int? StatusIshurId { get; set; }

    public virtual Ad? Ad { get; set; }

    public virtual ICollection<CustomerCharge> CustomerCharges { get; set; } = new List<CustomerCharge>();

    public virtual ICollection<IshurForAd> IshurForAds { get; set; } = new List<IshurForAd>();

    public virtual PriceList? Price { get; set; }

    public virtual StatusIshur? StatusIshur { get; set; }
}
