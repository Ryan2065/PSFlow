using Microsoft.EntityFrameworkCore;
using PSFlow.Interfaces;
using PSFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlow.DB.Interactions
{
    public class FlowManagerDb : IFlowManager
    {
        private ICurrentUser _currentUser;
        private FlowContext _dbContext;
        public FlowManagerDb(ICurrentUser currentUser)
        {
            _dbContext = FlowDbManager.GetDbContext();
            _currentUser = currentUser;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Flow Get(string name)
        {
            return _dbContext.Flows.Where(p => p.Name == name && p.Deleted == false).FirstOrDefault();
        }

        public Flow Get(int id)
        {
            return _dbContext.Flows.Find(id);
        }

        public List<Flow> Get()
        {
            return _dbContext.Flows.Where(p => p.Deleted == false).ToList();
        }

        public List<string> GetFlowNames(string nameContains)
        {
            return _dbContext.Flows.Where(p => p.Name.Contains(nameContains) && p.Deleted == false).Select(p => p.Name).ToList();
        }

        public Flow New(string name, string script, string description, bool publish = false)
        {
            if (_dbContext.Flows.Where(p => p.Name == name && p.Deleted == false).Any())
            {
                throw new ArgumentException($"Flow name {name} already in use!");
            }
            var newFlow = new PSFlow.Models.Flow
            {
                Name = name,
                Description = description,
                Modified = DateTime.UtcNow,
                Created = DateTime.UtcNow,
                CreatedBy = _currentUser.UserName(),
                ModifiedBy = _currentUser.UserName()
            };
            _dbContext.Add(newFlow);
            _dbContext.SaveChanges();
            var newFlowVersion = new PSFlow.Models.FlowScript
            {
                Deleted = false,
                Created = DateTime.UtcNow,
                CreatedBy = _currentUser.UserName(),
                FlowId = newFlow.FlowId,
                Script = script
            };
            _dbContext.Add(newFlowVersion);
            _dbContext.SaveChanges();

            if (publish)
            {
                newFlow.ActiveScriptId = newFlowVersion.FlowScriptId;
                _dbContext.SaveChanges();
            }
            return newFlow;
        }

        public void Remove(int id)
        {
            var obj = Get(id);
            obj.Deleted = true;
            obj.Modified = DateTime.UtcNow;
            obj.ModifiedBy = _currentUser.UserName();
            _dbContext.SaveChanges();
        }

        public void Remove(string name)
        {
            var obj = Get(name);
            obj.Deleted = true;
            obj.Modified = DateTime.UtcNow;
            obj.ModifiedBy = _currentUser.UserName();
            _dbContext.SaveChanges();
        }

        public Flow Set(int id, Flow newObject)
        {
            var oldObject = Get(id);
            oldObject.Description = newObject.Description;
            oldObject.Name = newObject.Name;
            oldObject.ActiveScriptId = newObject.ActiveScriptId;
            oldObject.Modified = DateTime.UtcNow;
            oldObject.ModifiedBy = _currentUser.UserName();
            _dbContext.SaveChanges();
            return oldObject;
        }
    }
}
