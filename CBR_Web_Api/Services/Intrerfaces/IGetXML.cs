using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xaml;
using System.Xml.Linq;
using CBR_Web_Api.Models;

namespace CBR_Web_Api.Services.Intrerfaces
{
    internal interface IGetXML
    {
        Task<XDocument> ReadyXMLAsync(string valuta);

        IEnumerable<XmlValute> ReadXmlValutes(XDocument document);
        IAsyncEnumerable<XmlCharCode> ReadAllCharCodeAsync();
    }
}
