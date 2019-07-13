using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore_facebook.ClassController.log
{
    public interface ILogRepository
    {
        void Add(Logitem item);
        IEnumerable<Logitem> GetAll();
        Logitem Find(string key);
        Logitem Remove(string key);
        void Update(Logitem item);
    }
    public class LogRepository : ILogRepository
    {
        static ConcurrentDictionary<string, Logitem> _todos = new ConcurrentDictionary<string, Logitem>();

        public IEnumerable<Logitem> GetAll()
        {
            return _todos.Values;
        }

        public void Add(Logitem item)
        {
            item.Key = Guid.NewGuid().ToString();
            _todos[item.Key] = item;
        }

        public Logitem Find(string key)
        {
            Logitem item;
            _todos.TryGetValue(key, out item);
            return item;
        }

        public Logitem Remove(string key)
        {
            Logitem item;
            _todos.TryGetValue(key, out item);
            _todos.TryRemove(key, out item);
            return item;
        }

        public void Update(Logitem item)
        {
            _todos[item.Key] = item;
        }
    }
}
