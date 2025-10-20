using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class CommunicationUser
{
    public int CommunicationId { get; set; }

    public int? UserId { get; set; }

    public int? CommunicationType { get; set; }

    public string? Contant { get; set; }

    public bool? Main { get; set; }

    public virtual CommunicationType? CommunicationTypeNavigation { get; set; }

    public virtual User? User { get; set; }
}
