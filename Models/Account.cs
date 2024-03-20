using System;
using System.Collections.Generic;

namespace BankApplication.Models;

public partial class Account
{
    public int AccountNo { get; set; }

    public int? CustomerId { get; set; }

    public string? AccountType { get; set; }

    public decimal? Balance { get; set; }

    public DateTime? AccountOpenDate { get; set; }

    public bool Status { get; set; }

    public virtual Interest? AccountTypeNavigation { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
