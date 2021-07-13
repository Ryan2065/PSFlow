using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Models
{
    public class JobStreamData
    {
        [Key]
        public Guid StreamDataId { get; set; }
        public Guid JobId { get; set; }
        public string Message { get; set; }
        public DateTime Recorded { get; set; }
        public string ErrorRecord { get; set; }
        public short JobStreamDataTypeId { get; set; }

        public Job PSJob { get; set; }
        public JobStreamDataType JobStreamDataType { get; set; }
    }
}
