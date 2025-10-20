using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class User
{
    public int UserId { get; set; }

    public int? CharactersId { get; set; }

    public DateTime? LastEnterDate { get; set; }

    public string? FullName { get; set; }

    public string? UserName { get; set; }

    public int? Passwords { get; set; }

    public virtual Character? Characters { get; set; }

    public virtual ICollection<CommunicationUser> CommunicationUsers { get; set; } = new List<CommunicationUser>();

    public virtual ICollection<IshurForAd> IshurForAds { get; set; } = new List<IshurForAd>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
