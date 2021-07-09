using PSFlow.DB;
using PSFlow.DB.Models;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PSFlow.Variables
{
    public class VariableManager : IDisposable
    {
        /*private FlowContext _dbContext;
        public string Environment;
        public VariableManager(FlowContext dbContext)
        {
            
            _dbContext = dbContext;
            Environment = settings.PSFlowEnivronment;
        }
        public VariableManager()
        {
            var settings = new PSFlow.Settings();
            _dbContext = settings.GetDbContext();
            Environment = settings.PSFlowEnivronment;
        }
        private string SerializeObject(object obj, int maxDepth)
        {
            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.MaxDepth = 3;
            jsonOptions.IncludeFields = true;
            jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;
            return JsonSerializer.Serialize(obj, jsonOptions);
        }
        public Variable Create(string name, object value, string environment = null, int maxDepth = 3)
        {
            if(_dbContext.Variables.Where( p => p.Name == name && p.Environment == environment).Any())
            {
                throw new ArgumentException($"Variable name {name} for environment {environment} already in  use");
            }
            var newVar = new Variable();
            newVar.Environment = environment;
            newVar.Name = name;
            newVar.Value = SerializeObject(value, maxDepth);
            _dbContext.Add(newVar);
            _dbContext.SaveChanges();
            return newVar;
        }
        public Variable Set(string name, object value, string environment = null, int maxDepth = 3)
        {
            var vari = Get(name, environment);
            if(vari == null)
            {
                throw new ArgumentException($"Variable name {name} for environment {environment} not found!");
            }
            vari.Value = SerializeObject(value, maxDepth);
            _dbContext.SaveChanges();
            return vari;
        }
        public Variable Get(string name, string environment = null)
        {
            if(environment == null)
            {
                environment = Environment;
            }
            return _dbContext.Variables.Where(p => p.Name == name && p.Environment == environment).FirstOrDefault();
        }
        public void Delete(string name, string environment = null)
        {
            var vari = Get(name, environment);
            if (vari == null)
            {
                throw new ArgumentException($"Variable name {name} for environment {environment} not found!");
            }
            _dbContext.Remove(vari);
            _dbContext.SaveChanges();
        }*/
        public void Dispose()
        {
            //_dbContext.Dispose();
        }
    }
}
