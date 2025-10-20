using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class ChargesPayment
{
    public int ChargesPaymentsId { get; set; }

    public int? ChargeId { get; set; }

    public int? PaymentId { get; set; }

    public decimal? Amount { get; set; }

    public virtual CustomerCharge? Charge { get; set; }

    public virtual Payment? Payment { get; set; }
}
