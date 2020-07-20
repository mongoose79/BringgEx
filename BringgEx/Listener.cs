using System;
using System.Net;
using System.Threading.Tasks;

namespace BringgEx
{
    public class Listener
    {
        private const string DefaultUrl = "http://localhost:8080/gens/find/";

        public void CreateAndStartListener()
        {
            var listener = new HttpListener();
            listener.Prefixes.Add(DefaultUrl);
            listener.Start();

            while (true)
            {
                Task.Factory.StartNew(() => { ProcessRequest(listener.GetContext()); });
            }
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            try
            {
                var request = context?.Request;
                var response = context?.Response;
                var rawUrl = request?.RawUrl;
                if (response != null)
                {
                    ProcessResult(rawUrl, response);
                    context.Response.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while processing the request: {ex.Message}");
            }
        }

        private void ProcessResult(string rawUrl, HttpListenerResponse response)
        {
            string sourceGen = GetSourceGen(rawUrl);
            var genSearcher = new GenSearcher();
            if (string.IsNullOrEmpty(sourceGen) || !genSearcher.IsGenVaild(sourceGen))
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                sourceGen = RemovePrefix(sourceGen);
                if (genSearcher.IsGenExist(sourceGen))
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }
            }
        }

        private string GetSourceGen(string rawUrl)
        {
            string gen = "";
            var splitUrl = rawUrl.Split('/');
            if (splitUrl.Length == 4)
            {
                gen = splitUrl[3].ToUpper();
            }
            else
            {
                Console.WriteLine("Invalid source URL");
            }
            return gen;
        }

        private string RemovePrefix(string sourceGen)
        {
            string prefix = "AAAAAAAAAAA";
            sourceGen = sourceGen.Substring(prefix.Length);
            return sourceGen;
        }
    }
}
