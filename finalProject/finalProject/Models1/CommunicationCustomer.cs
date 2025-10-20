using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class CommunicationCustomer
{
    public int CommunicationId { get; set; }

    public int? CustomerId { get; set; }

    public int? CommunicationType { get; set; }

    public string? Contant { get; set; }

    public bool? Main { get; set; }

    public virtual CommunicationType? CommunicationTypeNavigation { get; set; }

    public virtual Customer? Customer { get; set; }
}
