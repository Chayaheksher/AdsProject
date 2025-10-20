using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class PaymentsType
{
    public int PaymentTypeId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
