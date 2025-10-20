using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class CommunicationType
{
    public int CommunicationType1 { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<CommunicationCustomer> CommunicationCustomers { get; set; } = new List<CommunicationCustomer>();

    public virtual ICollection<CommunicationUser> CommunicationUsers { get; set; } = new List<CommunicationUser>();
}
