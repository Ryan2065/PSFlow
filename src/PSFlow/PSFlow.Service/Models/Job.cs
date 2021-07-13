using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Models
{
    public class Job
    {
        public Job()
        {
            StreamData = new HashSet<JobStreamData>();
        }
        [Key]
        public Guid JobId { get; set; }
        public int FlowScriptId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public short StatusId { get; set; }
        public string CreatedBy { get; set; }

        public JobStatus Status { get; set; }
        public FlowScript Script { get; set; }
        public ICollection<JobStreamData> StreamData { get; set; }

    }
}
