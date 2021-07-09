using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.DB.Models
{
    public class FlowVersion
    {
        public int FlowVersionId { get; set; }
        public int FlowId { get; set; }
        public string Script { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool CanRun { get; set; }

        public Flow Flow { get; set; }
    }
}
