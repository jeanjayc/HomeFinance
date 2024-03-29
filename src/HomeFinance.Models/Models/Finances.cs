﻿using System.ComponentModel.DataAnnotations.Schema;

namespace HomeFinance.Domain.Models
{
    public class Finances
    {
        [Column("id_finances")]
        public Guid FinancesId { get; set; }
        public string? FinanceName { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Price { get; set; }
        public bool Paid { get; set; }
        public int? InstallmentsPaid { get; set; }
        public int? QtdInstallments { get; set; }
    }
}
