using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class CustomersType
{
    public int CustomerType { get; set; }

    public string? Desciption { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
