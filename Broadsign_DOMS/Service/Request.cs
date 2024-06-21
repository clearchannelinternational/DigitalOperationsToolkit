using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Broadsign_DOMS.Service
{
    //change this to abstract class !
    public class Request : IRequest
    {

        #region fields
     

        #endregion

        #region properties
     
        public IRestClient cli { get; set; }
        public IRestRequest req { get; set; }
        public string Url {get; set;}
        public string Token { get; set; }
        #endregion

        public Request(){}

        private void _addHeaders()
        {
            req.AddHeader("authorization", $"Bearer {Token}");
            req.AddHeader("accept", $"application/json");
            req.AddHeader("content-type", $"application/json");
        }

        private void _addBody(object body)
        {
            req.AddJsonBody(body);
        }

        public IRestResponse GetRequest(string path)
        {
            cli = new RestClient();
            cli.Timeout = -1;
            cli.BaseUrl = new Uri(Url + path);

            req = new RestRequest(Method.GET);

            _addHeaders();

            return cli.Execute(req);
        }

        public IRestResponse UpdateRequest(string path, object body)
        {
            cli = new RestClient();
            cli.Timeout = -1;
            cli.BaseUrl = new Uri(Url + path);

            req = new RestRequest(Method.GET);

            _addHeaders();
            _addBody(body);

            return cli.Execute(req);
        }
    }
}
