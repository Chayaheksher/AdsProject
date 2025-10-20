using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class Character
{
    public int CharactersId { get; set; }

    public string? CharactersName { get; set; }

    public virtual ICollection<IshurForAd> IshurForAds { get; set; } = new List<IshurForAd>();

    public virtual ICollection<Ishurim> IshurimCharacters { get; set; } = new List<Ishurim>();

    public virtual ICollection<Ishurim> IshurimCharactersIdinIshurNavigations { get; set; } = new List<Ishurim>();

    public virtual ICollection<Ishurim> IshurimCharactersIdinRejectionNavigations { get; set; } = new List<Ishurim>();

    public virtual ICollection<Ishurim> IshurimCharactersIdinsertRowNavigations { get; set; } = new List<Ishurim>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
