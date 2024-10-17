using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectnew.Models
{
    [Table("finance")]
    
        public class finance
        {
            public int Id { get; set; }

            [Required]
            public string category { get; set; }

            [Required]
            public decimal amount { get; set; }

            [Required]
            public DateTime expenseDate { get; set; }

            [Required]
            public string description { get; set; }
        }
    }
