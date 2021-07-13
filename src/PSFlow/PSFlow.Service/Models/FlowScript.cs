using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Models
{
    public class FlowScript
    {
        public FlowScript()
        {
            Jobs = new HashSet<Job>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlowScriptId { get; set; }
        public int FlowId { get; set; }
        public string Script { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool Deleted { get; set; }

        public Flow Flow { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
