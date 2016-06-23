using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uBlog.Constants
{
    public class Constants
    {
        #region mvc
        public const string UBlogMVC = "https://localhost:44318";
        public const string UBlogMVCSTSCallback = "https://localhost:44318/stscallback";
        public const string UBlogClientSecret = "123";
        #endregion



        #region auth server
        //只是一个Id，不需要有真实站点
        public const string UBlogIssuerUri = "https://ublogsts/identity";
        //auth server
        public const string UBlogSTSOrigin = "https://localhost:44358";
        //auth server/identity
        public const string UBlogSTS = UBlogSTSOrigin + "/identity";
        //auth server 派发token
        public const string UBlogSTSTokenEndpoint = UBlogSTS + "/connect/token";
        //auth server 用户登录和授权
        public const string UBlogSTSAuthorizationEndpoint = UBlogSTS + "/connect/authorize";
        //auth server 用户个人信息
        public const string UBlogSTSUserInfoEndpoint = UBlogSTS + "/connect/userinfo";
        //auth server 结束会话
        public const string UBlogSTSEndSessionEndpoint = UBlogSTS + "/connect/endsession";
        //auth server 撤销token
        public const string UBlogSTSRevokeTokenEndpoint = UBlogSTS + "/connect/revocation";
        #endregion

        #region api
        public const string UBlogAPI = "https://localhost:44363/";
        #endregion

    }
}
