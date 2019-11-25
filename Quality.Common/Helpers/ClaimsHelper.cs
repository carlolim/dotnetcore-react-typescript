using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Quality.Common.Helpers
{
    public class ClaimsHelper : IClaimsHelper
    {
        private readonly ClaimsPrincipal _claimsPrincipal;

        public ClaimsHelper(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }

        public int ClientId
        {
            get
            {
                return Convert.ToInt32(_claimsPrincipal.Claims.FirstOrDefault(m => m.Type == "ClientId")?.Value);
            }
        }

        public int UserId
        {
            get
            {
                return Convert.ToInt32(_claimsPrincipal.Claims.FirstOrDefault(m => m.Type == "UserId")?.Value);
            }
        }

        public string FirstName
        {
            get
            {
                return _claimsPrincipal.Claims.FirstOrDefault(m => m.Type == "FirstName")?.Value;
            }
        }

        public string LastName
        {
            get
            {
                return _claimsPrincipal.Claims.FirstOrDefault(m => m.Type == "LastName")?.Value;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return Convert.ToBoolean(_claimsPrincipal.Claims.FirstOrDefault(m => m.Type == "IsAdmin")?.Value);
            }
        }
    }
}
