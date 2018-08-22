using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Threading.Tasks;

namespace TourManagement.API.Helpers
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
    {
        public string _requestHeaderToMatch;
        public string[] _expectedMediaTypes;

        public RequestHeaderMatchesMediaTypeAttribute(string requestHeader, string[] expectedMediaTypes)
        {
            this._requestHeaderToMatch = requestHeader;
            this._expectedMediaTypes = expectedMediaTypes;
        }

        public bool Accept(ActionConstraintContext context)
        {
            var requestHeaders = context.RouteContext.HttpContext.Request.Headers;
            
            if (!requestHeaders.ContainsKey(_requestHeaderToMatch))
            {
                return false;
            }

            string[] requestHeaderMediaTypes = requestHeaders[_requestHeaderToMatch].ToString().Split(',');

            foreach (string expectedMediaType in _expectedMediaTypes)
            {
                foreach (string requestHeaderMediaTypeValue in requestHeaderMediaTypes)
                {
                    if (expectedMediaType.Equals(requestHeaderMediaTypeValue, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;    
                    }
                }
            }

            return false;
        }

        public int Order
        {
            get { return 0; }
        }
    }
}