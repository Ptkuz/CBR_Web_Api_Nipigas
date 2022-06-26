using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xaml;
using System.Xml.Linq;
using CBR_Web_Api.Models;

namespace CBR_Web_Api.Services.Intrerfaces
{
    // Интерфейс работы с XML. Нужен для реализации внедрения зависимостей
    internal interface IGetXML
    {
        Task<XDocument> ReadyXMLAsync(string valuta, IProgress<double> progress = null, CancellationToken cancel = default);

        IEnumerable<XmlValute> ReadXmlValutes(XDocument document);
        IAsyncEnumerable<XmlCharCode> ReadAllCharCodeAsync();
    }
}
