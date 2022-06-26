using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBR_Web_Api.Models
{

    // Модель для получаения CharCode и преобразования его имени в нужный формат отображения
    internal class XmlCharCode
    {
        public string? CharCode { get; set; }
        public string? Name { get; set; }
        public string? CharCodeName { get; set; }
    }
}
