using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public int? CustomerType { get; set; }

    public string? Identification { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public int? HouseNumber { get; set; }

    public decimal? CustomerCreditBalance { get; set; }

    public virtual ICollection<Ad> Ads { get; set; } = new List<Ad>();

    public virtual ICollection<CommunicationCustomer> CommunicationCustomers { get; set; } = new List<CommunicationCustomer>();

    public virtual ICollection<CustomerCharge> CustomerCharges { get; set; } = new List<CustomerCharge>();

    public virtual CustomersType? CustomerTypeNavigation { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
