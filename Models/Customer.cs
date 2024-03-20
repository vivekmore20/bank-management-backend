using System;
using System.Collections.Generic;

namespace BankApplication.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Address { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string? AadharNumber { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
