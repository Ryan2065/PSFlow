using PSFlow.Models;
using System;
using System.Management.Automation;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PSFlow.Job
{
    public class PowerShellJobEngine
    {
        private readonly ChannelWriter<JobStreamData> _channelWriter;
        private readonly PSFlow.Models.Job _job;
        public PowerShellJobEngine(ChannelWriter<JobStreamData> channelWriter, PSFlow.Models.Job job)
        {
            _channelWriter = channelWriter;
            _job = job;
        }
        public void RunAsync(string script)
        {
            var ps = PowerShell.Create();
            ps.Streams.Debug.DataAdded += WriteStreamAsync;
            ps.Streams.Error.DataAdded += WriteStreamAsync;
            ps.Streams.Information.DataAdded += WriteStreamAsync;
            ps.Streams.Progress.DataAdded += WriteStreamAsync;
            ps.Streams.Verbose.DataAdded += WriteStreamAsync;
            ps.Streams.Warning.DataAdded += WriteStreamAsync;
            ps.AddScript(_job.Script.Script);
            var output = ps.Invoke();
            
        }
        public async void WriteStreamAsync(object sender, DataAddedEventArgs eventArgs)
        {
            var newJobStreamData = new JobStreamData();
            newJobStreamData.JobId = _job.JobId;
            newJobStreamData.Recorded = DateTime.UtcNow;
            newJobStreamData.StreamDataId = Guid.NewGuid();
            switch (sender)
            {
                case PSDataCollection<DebugRecord> debugRecords:
                    newJobStreamData.Message = debugRecords[eventArgs.Index].Message;
                    newJobStreamData.JobStreamDataTypeId = (short)JobStreamDataTypeEnum.Debug;
                    break;
                case PSDataCollection<ErrorRecord> errorRecords:
                    newJobStreamData.Message = errorRecords[eventArgs.Index].ErrorDetails.Message;
                    newJobStreamData.ErrorRecord = System.Text.Json.JsonSerializer.Serialize(errorRecords[eventArgs.Index].Exception);
                    newJobStreamData.JobStreamDataTypeId = (short)JobStreamDataTypeEnum.Error;
                    break;
                case PSDataCollection<WarningRecord> warningRecords:
                    newJobStreamData.Message = warningRecords[eventArgs.Index].Message;
                    newJobStreamData.JobStreamDataTypeId = (short)JobStreamDataTypeEnum.Warning;
                    break;
                case PSDataCollection<InformationRecord> infoRecords:
                    newJobStreamData.Message = infoRecords[eventArgs.Index].MessageData.ToString();
                    newJobStreamData.JobStreamDataTypeId = (short)JobStreamDataTypeEnum.Information;
                    break;
                case PSDataCollection<ProgressRecord> projRecords:
                    newJobStreamData.Message = projRecords[eventArgs.Index].StatusDescription;
                    newJobStreamData.JobStreamDataTypeId = (short)JobStreamDataTypeEnum.Progress;
                    break;
                case PSDataCollection<VerboseRecord> verboseRecords:
                    newJobStreamData.Message = verboseRecords[eventArgs.Index].Message;
                    newJobStreamData.JobStreamDataTypeId = (short)JobStreamDataTypeEnum.Verbose;
                    break;
                default:
                    return;
            }
            await _channelWriter.WriteAsync(newJobStreamData);
        }
    }
}
