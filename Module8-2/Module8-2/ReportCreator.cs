using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml.Serialization;
using ClosedXML.Excel;

namespace Module8_2
{
    public class ReportCreator
    {
        public static void PrepareXMLResponse<T>(HttpContext context, List<T> data)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            context.Response.Clear();
            context.Response.ContentType = "text/xml";
            context.Response.AddHeader("content-disposition", "attachment;filename=\"XMLReport.xml\"");

            using (MemoryStream stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, data);
                stream.WriteTo(context.Response.OutputStream);
            }

            context.Response.End();
        }

        public static void PrepareXLResponse<T>(HttpContext context, List<T> data)
        {
            using (XLWorkbook book = new XLWorkbook())
            {
                IXLWorksheet sheet = book.Worksheets.Add("Report");
                sheet.Cell(1, 1).InsertData(data);
                context.Response.Clear();
                context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                context.Response.AddHeader("content-disposition", "attachment;filename=\"XLReport.xlsx\"");

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    book.SaveAs(memoryStream);
                    memoryStream.WriteTo(context.Response.OutputStream);
                }

                context.Response.End();
            }
        }
    }
}