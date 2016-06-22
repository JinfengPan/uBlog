using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uBlog.Constants
{
    public class Constants
    {
        public const string UBlogMVC = "https://localhost:44318";
        public const string UBlogMVCCallback = "https://localhost:44318/stscallback";

        public const string UBlogClientSecret = "123";

        public const string UBlogIssuerUri = "https://tripcompanysts/identity";
        public const string UBlogSTSOrigin = "https://localhost:44317";
        public const string UBlogSTS = UBlogSTSOrigin + "/identity";
        public const string UBlogSTSTokenEndpoint = UBlogSTS + "/connect/token";
        public const string UBlogSTSAuthorizationEndpoint = UBlogSTS + "/connect/authorize";
        public const string UBlogSTSUserInfoEndpoint = UBlogSTS + "/connect/userinfo";
        public const string UBlogSTSEndSessionEndpoint = UBlogSTS + "/connect/endsession";
        public const string UBlogSTSRevokeTokenEndpoint = UBlogSTS + "/connect/revocation";

    }
}
