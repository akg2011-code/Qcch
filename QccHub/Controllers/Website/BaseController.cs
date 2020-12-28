using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace QccHub.Controllers.Website
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]/[action]")]
    public class BaseController : Controller
    {
        protected readonly IConfiguration _iConfig;
        protected readonly IHttpClientFactory _clientFactory;
        private const string errorsSparator = "@@";
        private const string errorKeyValueSparator = "&&";
        protected string APIURL = "";

        public BaseController(IConfiguration iConfig, IHttpClientFactory clientFactory)
        {
            _iConfig = iConfig;
            _clientFactory = clientFactory;
            APIURL = _iConfig.GetValue<string>("AppSettings:APIURL");
        }

        internal void AddModelError(string errorString)
        {
            string[] errorList = errorString.Split(errorsSparator);
            foreach (var error in errorList)
            {
                string[] errorSeparate = error.Split(errorKeyValueSparator);
                if (errorSeparate.Length > 1)
                    ModelState.AddModelError(errorSeparate[0], errorSeparate[1]);
                else
                    ModelState.AddModelError("Error", error);
            }
        }
    }
}