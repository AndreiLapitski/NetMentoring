using System.Text;

namespace Module8_2
{
    public class QueryBuilder
    {
        public static string PrepareQuery(RequestParameters requestParameters)
        {
            StringBuilder query = new StringBuilder();

            if (requestParameters.Take != null && requestParameters.Skip == null)
            {
                query.Append($"SELECT TOP {requestParameters.Take} * FROM ORDERS ");
            }
            else
            {
                query.Append("SELECT * FROM Orders ");
            }

            if (requestParameters.CustomerId != null)
            {
                query.Append($"WHERE CustomerID = '{requestParameters.CustomerId}' ");
                if (requestParameters.DateFrom != null)
                {
                    query.Append($"AND OrderDate > '{requestParameters.DateFrom}' ");
                    if (requestParameters.DateTo != null)
                    {
                        query.Append($"AND OrderDate < '{requestParameters.DateTo}' ");
                    }
                }
            }

            if ((requestParameters.CustomerId == null) && (requestParameters.DateFrom != null))
            {
                query.Append($"WHERE OrderDate > '{requestParameters.DateFrom}' ");
                if (requestParameters.DateTo != null)
                {
                    query.Append($"AND OrderDate < '{requestParameters.DateTo}' ");
                }
            }

            if ((requestParameters.CustomerId == null) && (requestParameters.DateFrom == null) && (requestParameters.DateTo != null))
            {
                query.Append($"WHERE OrderDate < '{requestParameters.DateTo}' ");
            }

            query.Append("ORDER BY OrderID ");

            if ((requestParameters.Take != null) && (requestParameters.Skip != null))
            {
                query.Append($"OFFSET {requestParameters.Skip} ROWS FETCH NEXT {requestParameters.Take} ROWS ONLY");
            }

            if ((requestParameters.Take == null) && (requestParameters.Skip != null))
            {
                query.Append($"OFFSET {requestParameters.Skip} ROWS");
            }

            return query.ToString();
        }
    }
}