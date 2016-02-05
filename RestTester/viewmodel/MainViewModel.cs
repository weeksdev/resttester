using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace RestTester.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            RequestCommand = new RelayCommand(Request);
        }

        public RelayCommand RequestCommand { get; set; }

        
        private Dictionary<string,string> _headers = new Dictionary<string, string>()
        {
        };
        public Dictionary<string, string> Headers
        {
            get { return _headers; }
            set { _headers = value; RaisePropertyChanged("Headers"); }
        }
        private string _body = "";
        public string Body
        {
            get { return _body; }
            set {
                _body = value;
                if (ContentType.Contains("json"))
                {
                    try {
                        FormatError = "";
                        var body = Newtonsoft.Json.JsonConvert.DeserializeObject(_body);
                        _body = Newtonsoft.Json.JsonConvert.SerializeObject(body, Newtonsoft.Json.Formatting.Indented);
                    }catch(Exception ex)
                    {
                        FormatError = ex.Message;
                        _body = value;
                    }
                }
                RaisePropertyChanged("Body");
            }
        }
        private string _formatError;
        public string FormatError
        {
            get { return _formatError; }
            set { _formatError = value; RaisePropertyChanged("FormatError"); }
        }

        private string _url = "http://localhost";
        public string Url
        {
            get { return _url; }
            set { _url = value; RaisePropertyChanged("Url"); }
        }
        private List<string> _methods = new List<string>()
        {
            "GET",
            "POST",
            "PUT",
            "DELETE",
            "OPTIONS"
        };

        public List<string> Methods
        {
            get { return _methods; }
            set { _methods = value; RaisePropertyChanged("Methods"); }
        }

        private string _method = "GET";
        public string Method
        {
            get { return _method; }
            set { _method = value; RaisePropertyChanged("Method"); }
        }
        private bool _useDefaultCredentials = true;
        public bool UseDefaultCredentaials
        {
            get { return _useDefaultCredentials; }
            set { _useDefaultCredentials = value; RaisePropertyChanged("UseDefaultCredentaials"); }
        }
        private bool _setCredentials;
        public bool SetCredentials
        {
            get { return _setCredentials; }
            set { _setCredentials = value; RaisePropertyChanged("SetCredentials"); }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; RaisePropertyChanged("Username"); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged("Password"); }
        }
        private string _domain;
        public string Domain
        {
            get { return _domain; }
            set { _domain = value; RaisePropertyChanged("Domain"); }
        }
        private string _response = "";
        public string Response
        {
            get { return _response; }
            set { _response = value; RaisePropertyChanged("Response"); }
        }
        private string _statusCode;
        public string StatusCode
        {
            get { return _statusCode; }
            set { _statusCode = value; RaisePropertyChanged("StatusCode"); }
        }
        private string _contentType = "application/json";
        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; RaisePropertyChanged("ContentType"); }
        }
        private string _userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36";
        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; RaisePropertyChanged("UserAgent"); }
        }
        private Dictionary<string, string> _responseHeaders;
        public Dictionary<string,string> ResponseHeaders
        {
            get { return _responseHeaders; }
            set { _responseHeaders = value;  RaisePropertyChanged("ResponseHeader"); }
        }
        public void Request()
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(Url));
            foreach (var header in Headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            request.Method = Method;
            request.ContentType = ContentType;
            request.UserAgent = UserAgent;
            request.UseDefaultCredentials = UseDefaultCredentaials;
            if (SetCredentials)
            {
                request.Credentials = new NetworkCredential(Username, Password);
                if (Domain != "" && Domain != null)
                {
                    ((NetworkCredential)request.Credentials).Domain = Domain;
                }
            }

            if (Body != "" && Body != null && Method != "GET")
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(Body);
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();


            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Response = reader.ReadToEnd();
                    StatusCode = ((int)response.StatusCode).ToString() + " (" + response.StatusCode.ToString() + ")";
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    for(var i = 0; i < response.Headers.Count; i++)
                    {
                        headers.Add(response.Headers.GetKey(i), response.Headers[i]);
                    }
                    ResponseHeaders = headers;
                }
            }
            catch (WebException ex)
            {
                using (var response = (HttpWebResponse)ex.Response)
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Response = reader.ReadToEnd();
                    StatusCode = ((int)response.StatusCode).ToString() + " (" + response.StatusCode.ToString() + ")";
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    for (var i = 0; i < response.Headers.Count; i++)
                    {
                        headers.Add(response.Headers.GetKey(i), response.Headers[i]);
                    }
                    ResponseHeaders = headers;
                }
            }
            catch(Exception ex)
            {
                Response = ex.Message;
            }
        }
    }
}