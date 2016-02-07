using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Security;
using System.Runtime.InteropServices;

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
    [JsonObject(MemberSerialization.OptIn)]
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            RequestCommand = new RelayCommand(Request);
            AddNewHeaderCommand = new RelayCommand(AddNewHeader);
            DeleteHeaderCommand = new RelayCommand(DeleteSelectedHeader);
        }

        public RelayCommand RequestCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        public List<string> HeaderKeys
        {
            get
            {
                var headerKeys = new List<string>();
                foreach (var headerKey in Enum.GetNames(typeof(HttpRequestHeader)))
                {
                    headerKeys.Add(headerKey);
                }
                return headerKeys;
            }
        }

        public class KeyVal
        {
            public string Key { get; set; }
            public string Value { get; set; }
            public KeyVal(string key, string value)
            {
                Key = key;
                Value = value;
            }
        }
        private System.Collections.ObjectModel.ObservableCollection<KeyVal> _headers = new System.Collections.ObjectModel.ObservableCollection<KeyVal>()
        {
        };
        [JsonProperty]
        public System.Collections.ObjectModel.ObservableCollection<KeyVal> Headers
        {
            get { return _headers; }
            set { _headers = value; RaisePropertyChanged("Headers"); }
        }
        private KeyVal _selectedHeader;

        public KeyVal SelectedHeader
        {
            get { return _selectedHeader; }
            set { _selectedHeader = value; RaisePropertyChanged("SelectedHeader"); }
        }
        public RelayCommand DeleteHeaderCommand { get; set; }
        public void DeleteSelectedHeader()
        {
            if(SelectedHeader != null)
            {
                Headers.Remove(SelectedHeader);
            }
        }
        public RelayCommand AddNewHeaderCommand { get; set; }
        public void AddNewHeader()
        {
            Headers.Add(new KeyVal("Header-Key", "Header-Value"));
            RaisePropertyChanged("Headers");
        }
        private string _body = "";
        [JsonProperty]
        public string Body
        {
            get { return _body; }
            set
            {
                _body = value;
                if (ContentType.Contains("json"))
                {
                    try
                    {
                        FormatError = "";
                        var body = Newtonsoft.Json.JsonConvert.DeserializeObject(_body);
                        _body = Newtonsoft.Json.JsonConvert.SerializeObject(body, Newtonsoft.Json.Formatting.Indented);
                    }
                    catch (Exception ex)
                    {
                        FormatError = ex.Message;
                        _body = value;
                    }
                }
                RaisePropertyChanged("Body");
            }
        }
        private string _formatError;
        [JsonProperty]
        public string FormatError
        {
            get { return _formatError; }
            set { _formatError = value; RaisePropertyChanged("FormatError"); }
        }

        private string _url = "http://localhost";
        [JsonProperty]
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
        [JsonProperty]
        public List<string> Methods
        {
            get { return _methods; }
            set { _methods = value; RaisePropertyChanged("Methods"); }
        }

        private string _method = "GET";
        [JsonProperty]
        public string Method
        {
            get { return _method; }
            set { _method = value; RaisePropertyChanged("Method"); }
        }
        private bool _useDefaultCredentials = true;
        [JsonProperty]
        public bool UseDefaultCredentials
        {
            get { return _useDefaultCredentials; }
            set { _useDefaultCredentials = value; RaisePropertyChanged("UseDefaultCredentials"); }
        }
        //private bool _setCredentials;
        //[JsonProperty]
        //public bool SetCredentials
        //{
        //    get { return _setCredentials; }
        //    set { _setCredentials = value; RaisePropertyChanged("SetCredentials"); }
        //}
        private bool useBasicAuthorization;
        [JsonProperty]
        public bool UseBasicAuthorization
        {
            get { return useBasicAuthorization; }
            set { useBasicAuthorization = value; RaisePropertyChanged("UseBasicAuthorization"); }
        }
        private bool _preAuthenticate;
        [JsonProperty]
        public bool PreAuthenticate
        {
            get { return _preAuthenticate; }
            set { _preAuthenticate = value; RaisePropertyChanged("PreAuthenticate"); }
        }

        private string _username;
        [JsonProperty]
        public string Username
        {
            get { return _username; }
            set { _username = value; RaisePropertyChanged("Username"); }
        }
        private SecureString _password;
        [JsonProperty]
        public SecureString Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged("Password"); }
        }
        private string _domain;
        [JsonProperty]
        public string Domain
        {
            get { return _domain; }
            set { _domain = value; RaisePropertyChanged("Domain"); }
        }
        private string _response = "";
        [JsonProperty]
        public string Response
        {
            get { return _response; }
            set { _response = value; RaisePropertyChanged("Response"); }
        }
        private string _statusCode;
        [JsonProperty]
        public string StatusCode
        {
            get { return _statusCode; }
            set { _statusCode = value; RaisePropertyChanged("StatusCode"); }
        }
        private string _contentType = "application/json";
        [JsonProperty]
        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; RaisePropertyChanged("ContentType"); }
        }

        private System.Collections.ObjectModel.ObservableCollection<string> _contentTypes = new System.Collections.ObjectModel.ObservableCollection<string>()
        {
            "application/json",
            "application/x-javascript",
            "text/javascript",
            "text/x-javascript",
            "text/x-json",
            "text/xml",
            "application/xml",
            "text/plain",
            "application/soap+xml; charset=utf-8",
            "application/x-www-form-urlencoded",
            "multipart/form-data"
        };

        public System.Collections.ObjectModel.ObservableCollection<string> ContentTypes
        {
            get { return _contentTypes; }
            set { _contentTypes = value; RaisePropertyChanged("ContentTypes"); }
        }


        private string _userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36";
        [JsonProperty]
        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; RaisePropertyChanged("UserAgent"); }
        }

        private System.Collections.ObjectModel.ObservableCollection<string> _userAgents = new System.Collections.ObjectModel.ObservableCollection<string>()
        {
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; AS; rv:11.0) like Gecko",
            "Mozilla/5.0 (Linux; U; Android 4.0.3; ko-kr; LG-L160L Build/IML74K) AppleWebkit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30",
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A",
            "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)"
        };

        public System.Collections.ObjectModel.ObservableCollection<string> UserAgents
        {
            get { return _userAgents; }
            set { _userAgents = value; RaisePropertyChanged("UserAgents"); }
        }


        private Dictionary<string, string> _responseHeaders;
        [JsonProperty]
        public Dictionary<string, string> ResponseHeaders
        {
            get { return _responseHeaders; }
            set { _responseHeaders = value; RaisePropertyChanged("ResponseHeader"); }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; RaisePropertyChanged("IsLoading"); }
        }
        public event EventHandler RequestPosted;
        public void Request()
        {
            IsLoading = true;
            Response = "";
            StatusCode = "";
            System.Threading.Thread requestThread = new System.Threading.Thread(delegate ()
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(Url));
                    
                    request.Method = Method;
                    request.ContentType = ContentType;
                    request.UserAgent = UserAgent;
                    request.UseDefaultCredentials = UseDefaultCredentials;
                    request.PreAuthenticate = PreAuthenticate;

                    if (!UseDefaultCredentials)
                    {
                        if (!UseBasicAuthorization)
                        {
                            request.Credentials = new NetworkCredential(Username, Password);
                            if (Domain != "" && Domain != null)
                            {
                                ((NetworkCredential)request.Credentials).Domain = Domain;
                            }
                        }
                        else
                        {
                            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Username + ":" + SecureStringToString(Password)));
                            request.Headers.Add("Authorization", "Basic " + encoded);
                        }
                    }

                    if (Headers.Distinct().Count() > 0)
                    {
                        Headers.Distinct().ToList().ForEach(a => request.Headers.Add(a.Key, a.Value));
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
                            for (var i = 0; i < response.Headers.Count; i++)
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
                    catch (Exception ex)
                    {
                        Response = ex.Message;
                    }

                }
                catch (Exception ex)
                {
                    Response = "Sorry an unknown error occured in requesting. \r\n" + ex.Message;
                }
                finally
                {
                    IsLoading = false;
                    var handler = RequestPosted;
                    if(handler != null)
                    {
                        handler(this, EventArgs.Empty);
                    }
                }
            });
            requestThread.Start();
        }
        String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; RaisePropertyChanged("FilePath"); }
        }

        public void Save()
        {
            try
            {
                var requestData = Newtonsoft.Json.JsonConvert.SerializeObject(this);
                System.IO.File.WriteAllText(FilePath, requestData);
            }
            catch { }
        }

        public void Open()
        {
            try
            {
                var requestData = System.IO.File.ReadAllText(FilePath);
                var previousModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewModel.MainViewModel>(requestData);
                foreach (var property in previousModel.GetType().GetProperties())
                {
                    //Check if the property is one that we've serialized
                    if (Attribute.IsDefined(property, typeof(JsonPropertyAttribute)))
                    {
                        var oldValue = previousModel.GetType().GetProperty(property.Name).GetValue(previousModel);
                        if (oldValue != null && oldValue.ToString() != "null")
                        {
                            this.GetType().GetProperty(property.Name).SetValue(this, oldValue);
                        }
                        else
                        {
                            this.GetType().GetProperty(property.Name).SetValue(this, null);
                        }
                    }
                }
            }
            catch { }
        }
    }
}