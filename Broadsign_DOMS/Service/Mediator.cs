using Broadsign_DOMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Service
{
    public class Mediator
    {
        static IDictionary<string, List<Action<object>>> _listDictionary = new Dictionary<string, List<Action<object>>>();

        public static void Subscribe(string token, Action<object> callback)
        {
            if (!_listDictionary.ContainsKey(token))
            {
                List<Action<object>> list = new List<Action<object>> { callback };
      
                _listDictionary.Add(token, list);
            }
            else
            {
                bool found = false;
                foreach(var item in _listDictionary[token])
                {
                    if(item.Method == callback.Method)
                        found = true;
                }
                if(!found)
                    _listDictionary[token].Add(callback);
            }
        }
        public static void UnSubscribe(string token, Action<object> callback)
        {
            if(_listDictionary.ContainsKey(token))
                _listDictionary[token].Remove(callback);
        }
        public static void Notify(string token,string args = null)
        {
            if (_listDictionary.ContainsKey(token))
            {
                foreach (var callback in _listDictionary[token])
                {
                    callback(args);
                }
            }
        }
    }
}
