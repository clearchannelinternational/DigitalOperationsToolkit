using RestSharp;

namespace Broadsign_DOMS.Service
{
    public interface IRequest
    {
        string Url { get; set; }
        string Token { get; set; }
        IRestClient cli { get; set; }
        IRestRequest req { get; set; }
        IRestResponse GetRequest(string path);
        IRestResponse UpdateRequest(string path, object body);

    }
}