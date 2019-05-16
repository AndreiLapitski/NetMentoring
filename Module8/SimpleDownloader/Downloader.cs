using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace SimpleDownloader
{
    public class Downloader
    {
        private const string Href = "href";
        private const string Src = "src";
        private string _destinationPath { get; }
        private string _page { get; set; }
        public int _level { get; set; }
        public bool _otherDomain { get; set; }
        public string _validImageExtensions { get; set; }
        public bool _isLog { get; set; }
        public Downloader(string destinationPath, int level, bool otherDomain, string validImageExtensions, bool isLog)
        {       
            _destinationPath = destinationPath;            
            _level = level;
            _otherDomain = otherDomain;
            _validImageExtensions = validImageExtensions;
            _isLog = isLog;
        }

        public void Download(Uri uri)
        {
            ShowMessage($"Work with {uri.AbsoluteUri} started", _isLog);

            HtmlDocument _htmlDocument = new HtmlDocument();            
            using (HttpClient httpClient = new HttpClient())
            {
                _htmlDocument.LoadHtml(httpClient.GetStringAsync(uri.AbsoluteUri).Result);
                _page = httpClient.GetStringAsync(uri.AbsoluteUri).Result;
            }

            CreateDestinationDirectory(_destinationPath, uri);
            HtmlNodeCollection htmlNodeCollection = _htmlDocument.DocumentNode.SelectNodes("//a|//link|//script|//img");

            foreach (HtmlNode htmlNode in htmlNodeCollection)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string currentHrefOrSrc = null;
                    httpClient.BaseAddress = uri;
                    HttpResponseMessage response = null;
                    MediaTypeHeaderValue contentType = null;
                    if (htmlNode.Attributes[Href] != null && (htmlNode.Name == "link" || htmlNode.Name == "a"))
                    {
                        currentHrefOrSrc = htmlNode.Attributes[Href].Value;
                        try
                        {
                            response = httpClient.GetAsync(htmlNode.Attributes[Href].Value).Result;
                            contentType = response.Content.Headers.ContentType;
                        }
                        catch (Exception e)
                        {
                            ShowMessage($"{currentHrefOrSrc} - {e.Message}", _isLog);
                        }

                    }
                    else if (htmlNode.Attributes[Src] != null)
                    {
                        currentHrefOrSrc = htmlNode.Attributes[Src].Value;
                        response = httpClient.GetAsync(htmlNode.Attributes[Src].Value).Result;
                        contentType = response.Content.Headers.ContentType;
                    }

                    if (response != null)
                    {
                        if (contentType != null && 
                            contentType.MediaType == "text/html")
                        {
                            if (currentHrefOrSrc.Contains("http:/") || currentHrefOrSrc.Contains("https:/"))
                            {
                                if (_otherDomain)
                                {
                                    //Download(new Uri(currentHrefOrSrc));
                                }
                                else
                                {
                                    if (currentHrefOrSrc.Contains(uri.AbsoluteUri))
                                    {
                                        //Download(new Uri(currentHrefOrSrc));
                                    }                                    
                                    
                                    ShowMessage("Other domains are not allowed!", _isLog);
                                }
                            }
                        }

                        if (contentType != null && (contentType.MediaType.Contains("image/") ||
                                                    contentType.MediaType == "text/css" ||
                                                    contentType.MediaType.Contains("javascript")))
                        {
                            ProcessFile(currentHrefOrSrc, uri);
                        }
                    }
                }
            }

            File.AppendAllText(Path.Combine(_destinationPath, uri.Host, "index.html"), _page);

            ShowMessage($"Work with {uri.AbsoluteUri} finished", _isLog);
        }

        private void ProcessFile(string hrefOrSrc, Uri uri)
        {            
            if (hrefOrSrc.Contains("http:/") || hrefOrSrc.Contains("https:/"))
            {
                ShowMessage($"Incorrect path! - {hrefOrSrc}", _isLog);
            }
            else
            {
                string[] parts = hrefOrSrc.Trim('/').Split('/');

                if (parts.Length == 1 && parts[0].Contains('.'))
                {
                    CreateFile(parts[0], uri);
                }
                else
                {
                    CreateDirectories(parts, uri);
                    CreateFile(hrefOrSrc.Trim('/'), uri);
                    
                    _page = _page.Replace(hrefOrSrc, hrefOrSrc.Trim('/'));
                }       
            }           
        }

        private void CreateFile(string absolutePathToFile, Uri uri)
        {
            StringBuilder fullPath = new StringBuilder(Path.Combine(_destinationPath, uri.Host, absolutePathToFile));

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = uri;
                HttpResponseMessage response = httpClient.GetAsync(Path.Combine(uri.AbsoluteUri, absolutePathToFile)).Result;
                MediaTypeHeaderValue contentType = response.Content.Headers.ContentType;                
                FileStream fileStream = null;

                if (response.IsSuccessStatusCode)
                {
                    if (contentType.MediaType.Contains("image/") && 
                        !IsValidImageExtension(absolutePathToFile))
                    {
                        ShowMessage($"Incorrect image extension! - {absolutePathToFile}", _isLog);                    
                    }
                    else
                    {
                        fileStream = new FileStream(fullPath.ToString(), FileMode.Create);
                        response.Content.ReadAsStreamAsync().Result.CopyTo(fileStream);
                        fileStream.Close(); 

                        ShowMessage($"{fullPath} file was created", _isLog);
                    }      
                }                         
            }
        }

        private void CreateDirectories(string[] parts, Uri uri)
        {           
            StringBuilder fullPath = new StringBuilder(Path.Combine(_destinationPath, uri.Host) + "/");
            for (int i = 0; i < parts.Length - 1; i++) // -1 because the last part is always a file name
            {
                fullPath.Append(parts[i] + "/");
            }

            if (!Directory.Exists(fullPath.ToString()))
            {
                Directory.CreateDirectory(fullPath.ToString());
            }
        }
        private void CreateDestinationDirectory(string destinationPath, Uri uri)
        {
            Directory.CreateDirectory(Path.Combine(destinationPath, uri.Host));
        }

        private bool IsValidImageExtension(string fileNameWithExtension)
        {
            string[] validExtensions = _validImageExtensions.Trim(';').Split(';');
            List<Regex> regexes = new List<Regex>();
            foreach (string validExtension in validExtensions)
            {
                regexes.Add(new Regex(string.Format(".*.{0}$", validExtension)));
            }

            foreach (Regex regex in regexes)
            {
                if (regex.IsMatch(fileNameWithExtension))
                {
                    return true;                   
                }
            }

            return false;
        }

        private void ShowMessage(string message, bool isLog)
        {
            if (isLog)
            {
                Console.WriteLine(message);
            }           
        }  
    }
}
