using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class CustomerCharge
{
    public int ChargeId { get; set; }

    public int? CustomerId { get; set; }

    public decimal? ChargeAmount { get; set; }

    public int? PublicationDatesId { get; set; }

    public DateTime? ChargeDate { get; set; }

    public int? PaymentStatus { get; set; }

    public virtual ICollection<ChargesPayment> ChargesPayments { get; set; } = new List<ChargesPayment>();

    public virtual Customer? Customer { get; set; }

    public virtual PaymentStatus? PaymentStatusNavigation { get; set; }

    public virtual PublicationDate? PublicationDates { get; set; }
}
