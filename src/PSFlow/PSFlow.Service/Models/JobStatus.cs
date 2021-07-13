using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Models
{
    public class JobStatus
    {
        public JobStatus()
        {
            Jobs = new HashSet<Job>();
        }
        [Key]
        public short JobStatusId { get; set; }
        [MaxLength(15)]
        public string Name { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
    public enum JobStatusEnum
    {
        Created = 1,
        New = 2,
        InProgress = 5,
        Waiting = 6,
        Complete = 10,
        Error = 15
    }
}
