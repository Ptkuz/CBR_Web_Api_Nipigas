using CBR_Web_Api.Models;
using CBR_Web_Api.Services.Intrerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CBR_Web_Api.Services
{
    internal class GetXML : IGetXML
    {
       

        public async Task<XDocument> ReadyXMLAsync(string charCode)
        {
            XDocument xdoc = null;
            XDocument newXdoc = new XDocument(); // Новый документ
            XElement newRootElement = new XElement("ValCurs");

            DateTime dateStart = DateTime.Now.Subtract(new TimeSpan(14, 0, 0, 0));
            for (var day = dateStart; day < DateTime.Now; day = day.AddDays(1))
            {
                string date = day.ToShortDateString();
                var url = $"http://www.cbr.ru/scripts/XML_daily_eng.asp?date_req={date}";

                using (var client = new HttpClient())
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.StatusCode == HttpStatusCode.OK)
                        xdoc = XDocument.Parse(await response.Content.ReadAsStringAsync());
                    else
                        return null;
                    if (charCode == null)
                        return null;
                    var valute = xdoc.Element("ValCurs")?
                        .Elements("Valute")
                        .FirstOrDefault(v => v.Element("CharCode")?.Value == charCode.ToString());

                    List<string> concteteValuta = new List<string>();
                    if (valute != null)
                    {
                        XElement xmlValute = new XElement("Valute");

                        XAttribute nameValute = new XAttribute("name", charCode);
                        XElement dateElement = new XElement("Date", date);
                        XElement nominalElement = new XElement("Nominal", valute?.Element("Nominal")?.Value);
                        XElement valueElement = new XElement("Value", valute?.Element("Value")?.Value);

                        xmlValute.Add(nameValute);
                        xmlValute.Add(dateElement);
                        xmlValute.Add(nominalElement);
                        xmlValute.Add(valueElement);

                        newRootElement.Add(xmlValute);
                    }
                }
            }
            newXdoc.Add(newRootElement);
            return newXdoc;
        }


        public IEnumerable<XmlValute> ReadXmlValutes(XDocument document)
        {
            if (document == null)
                yield break;
            foreach (var xe in document.Root.Elements("Valute"))
            {
                var date = xe.Element("Date").Value;
                var nominal = xe.Element("Nominal")?.Value;
                var value = xe.Element("Value")?.Value;



                yield return new XmlValute
                {
                    Date = DateTime.Parse(date).ToShortDateString(),
                    Nominal = Int32.Parse(nominal),
                    Value = Double.Parse(value),
                    AllValue = (nominal.ToDouble() * value.ToDouble())

                };

            }
        }
       
        // Записываем в коллекцию все CharCode
        public async IAsyncEnumerable<XmlCharCode> ReadAllCharCodeAsync()
        {
            XDocument document = await GetXDocAsync();

            foreach (var xe in document.Root.Elements("Valute")) 
            {
                var charCode = xe.Element("CharCode").Value;
                var name = xe.Element("Name")?.Value;

                yield return new XmlCharCode
                {
                    CharCode = charCode,
                    Name = name,
                    CharCodeName = $"{charCode} | {name}"
                    
                };
            }

        }


        // Считываем документ для поиска всех CharCode
        private async Task<XDocument> GetXDocAsync() 
        {

            XDocument xDocument = new XDocument();
            string date = DateTime.Now.ToShortDateString();
            var url = $"http://www.cbr.ru/scripts/XML_daily_eng.asp?date_req={date}";
            using (var client = new HttpClient())
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.OK)
                    xDocument = XDocument.Parse(await response.Content.ReadAsStringAsync());
                else
                    return null;
            }

            return xDocument;


        }




    }
}
