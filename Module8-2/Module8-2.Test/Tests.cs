using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            //Arrange
            client = new HttpClient();
        }

        [Test]
        public void TestTextXmlContentTypeGetRequest()
        {
            //Act
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            HttpResponseMessage response = 
                client.GetAsync("http://localhost:53616/Report?customerId=VINET").Result;
            
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestTextXlContentTypeGetRequest()
        {
            //Act
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            HttpResponseMessage response =
                client.GetAsync("http://localhost:53616/Report?customerId=VINET").Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestTextXmlContentTypePostRequest()
        {
            //Act
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                {"customerId", "VINET"}
            };
            FormUrlEncodedContent encodedContent = new FormUrlEncodedContent(parameters);
            HttpResponseMessage response = client.PostAsync("http://localhost:53616/", encodedContent).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestTextXlContentTypePostRequest()
        {
            //Act
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                {"customerId", "VINET"}
            };
            FormUrlEncodedContent encodedContent = new FormUrlEncodedContent(parameters);
            HttpResponseMessage response = client.PostAsync("http://localhost:53616/", encodedContent).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestByCustomerIdAndDateFrom()
        {
            //Act
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            HttpResponseMessage response = 
                client.GetAsync("http://localhost:53616/Report?customerId=VINET&dateFrom=1997").Result;
            HttpStatusCode statusCode = response.StatusCode;
            string content = response.Content.ReadAsStringAsync().Result;
            
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            Assert.AreNotEqual(0,content.Length);
        }

        [Test]
        public void TestByCustomerIdAndTake()
        {
            //Act
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            HttpResponseMessage response = 
                client.GetAsync("http://localhost:53616/Report?customerId=VINET&take=1").Result;
            HttpStatusCode statusCode = response.StatusCode;
            string content = response.Content.ReadAsStringAsync().Result;
            
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            Assert.AreNotEqual(0,content.Length);
        }

        [Test]
        public void TestByCustomerIdAndSkip()
        {
            //Act
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            HttpResponseMessage response = 
                client.GetAsync("http://localhost:53616/Report?customerId=VINET&skip=3").Result;
            HttpStatusCode statusCode = response.StatusCode;
            string content = response.Content.ReadAsStringAsync().Result;
            
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            Assert.AreNotEqual(0,content.Length);
        }
    }
}