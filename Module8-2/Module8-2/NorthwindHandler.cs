using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.Http;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Module8_2
{
    public class NorthwindHandler : IHttpHandler
    {
        private string connectionStr = ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString;
        private RequestParameters requestParameters;
        public void ProcessRequest(HttpContext context)
        {
            ParseRequest(context);
            PrepareResponse(context);         
        }

        public bool IsReusable => true;
        private void ParseRequest(HttpContext context)
        {
            string httpMethod = context.Request.HttpMethod;

            switch (httpMethod)
            {
                case "GET":
                    NameValueCollection parametersFromURL = context.Request.Url.ParseQueryString();
                    requestParameters = new RequestParameters()
                    {
                        CustomerId = parametersFromURL["customerId"],
                        DateFrom = parametersFromURL["dateFrom"],
                        DateTo = parametersFromURL["dateTo"],
                        Take = parametersFromURL["take"],
                        Skip = parametersFromURL["skip"]
                    };
                    break;

                case "POST":
                    NameValueCollection parametersFromBody = context.Request.Form;
                    requestParameters = new RequestParameters()
                    {
                        CustomerId = parametersFromBody["customerId"],
                        DateFrom = parametersFromBody["dateFrom"],
                        DateTo = parametersFromBody["dateTo"],
                        Take = parametersFromBody["take"],
                        Skip = parametersFromBody["skip"]
                    };
                    break;
            }           
        }

        private void PrepareResponse(HttpContext context)
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
            {
                orders = sqlConnection.Query<Order>(QueryBuilder.PrepareQuery(requestParameters)).ToList();
            }        

            List<string> acceptTypes = new List<string>(context.Request.AcceptTypes);

            if (acceptTypes.Contains("text/xml") || acceptTypes.Contains("application/xml"))
            {
                ReportCreator.PrepareXMLResponse<Order>(context, orders);
            }
            else
            {
                ReportCreator.PrepareXLResponse<Order>(context, orders);
            }
        }
    }   
}