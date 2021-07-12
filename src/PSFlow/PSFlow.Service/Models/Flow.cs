using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Models
{
    public class Flow
    {
        public Flow()
        {
            Scripts = new HashSet<FlowScript>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlowId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public int? ActiveScriptId { get; set; }

        public bool Deleted { get; set; }

        public ICollection<FlowScript> Scripts { get; set; }
        
        public FlowScript ActiveScript { get; set; }
    }
}
