using CodingIdeas.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace CodingIdeas.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static DataTransferManager _managerInstance;
        public static DataTransferManager Manager
        {
            get
            {
                if (_managerInstance == null)
                {
                    var conf = System.Web.Configuration.WebConfigurationManager.AppSettings;
                    int postsPerPage = int.Parse(conf["PostsPerPage"]);
                    int commentsPerPage = int.Parse(conf["CommentsPerPage"]);
                    int savedPostsPerPage = int.Parse(conf["SavedPostsPerPage"]);
                    _managerInstance = new DataTransferManager(postsPerPage, commentsPerPage, savedPostsPerPage);
                }
                return _managerInstance;
            }
        }
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
