using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class PaymentStatus
{
    public int PaymentStatus1 { get; set; }

    public string? Desciption { get; set; }

    public virtual ICollection<CustomerCharge> CustomerCharges { get; set; } = new List<CustomerCharge>();
}
