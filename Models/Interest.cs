using System;
using System.Collections.Generic;

namespace BankApplication.Models;

public partial class Interest
{
    public string AccountType { get; set; } = null!;

    public decimal? InterestRate { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
