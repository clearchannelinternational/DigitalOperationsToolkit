using Broadsign_DOMS.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Service
{
    public class BroadsignService : Request
    {
        #region Fields
        private static BroadsignService _instance;
        #endregion

        #region Properties
        public static BroadsignService Instance 
        {
            get
            {
                return _instance == null ? _instance = new BroadsignService() : _instance;
            }
             
        }
        
        #endregion

        #region Constructors
        private BroadsignService()
        {
            Url = "https://api.broadsign.com:10889/rest";
        }

        #endregion

        #region Methods
        public async Task<List<T>> GetResources<T>(string path, string key) where T : IBroadsignAPIModel
        {
         
            var rootContent = JsonConvert.DeserializeObject<Dictionary<string, object>>(GetRequest(path).Content);
            return await Task.Run(() => JsonConvert.DeserializeObject<List<T>>(rootContent[key].ToString()));
        }

        #endregion

    }
}
