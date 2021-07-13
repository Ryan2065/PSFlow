using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.Models
{
    public class JobStreamDataType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short JobStreamDataTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<JobStreamData> JobStreamData { get; set; }
    }

    public enum JobStreamDataTypeEnum
    {
        Output = 1,
        Error = 2,
        Warning = 3,
        Verbose = 4,
        Debug = 5,
        Information = 6,
        Exception = 7,
        Progress = 8
    }
}
