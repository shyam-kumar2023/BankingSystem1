using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Domain.Entity
{
    [Table("Account", Schema = "public")]
    public class AccountEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
    }
}
