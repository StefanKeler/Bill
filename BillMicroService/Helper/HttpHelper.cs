using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace BillMicroService.Helper
{
    public class HttpHelper
    {
        static HttpHelper instance = null;

        private HttpHelper()
        {

        }

        public static HttpHelper Instance()
        {
            if (instance == null)
            {
                instance = new HttpHelper();
            }
            return instance;

        }


        // get objekta preko httpa po idu 
        public string GetById(string uri, int port, int id)
        {




            string url = String.Format("http://localhost:{0}/{1}/{2}", port, uri, id);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            var getResponse = (HttpWebResponse)request.GetResponse();
            Stream newStream = getResponse.GetResponseStream();
            StreamReader sr = new StreamReader(newStream);
            var result = sr.ReadToEnd();
            return result;

        }

        // get objekta preko httpa po idu 
        public string GetAll(string uri, int port)
        {

            string url = String.Format("http://localhost:{0}/{1}", port, uri);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            var getResponse = (HttpWebResponse)request.GetResponse();
            Stream newStream = getResponse.GetResponseStream();
            StreamReader sr = new StreamReader(newStream);
            var result = sr.ReadToEnd();
            return result;

        }
    }
}