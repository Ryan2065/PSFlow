using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Models
{
    public class Variable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VariableId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Value { get; set; }
        [MaxLength(100)]
        public string Environment { get; set; }
    }
}
