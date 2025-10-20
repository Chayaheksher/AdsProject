using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? CustomerId { get; set; }

    public decimal? ChargeAmount { get; set; }

    public int? PaymentTypeId { get; set; }

    public string? Details { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<ChargesPayment> ChargesPayments { get; set; } = new List<ChargesPayment>();

    public virtual Customer? Customer { get; set; }

    public virtual PaymentsType? PaymentType { get; set; }

    public virtual User? User { get; set; }
}
