using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBR_Web_Api.Models
{

    // Модель Валюты для отображения в DataGrid
    internal class XmlValute
    {
        public string? Date { get; set; }
        public int? Nominal { get; set; }
        public double? Value { get; set; }
        public double? AllValue { get; set; }
    }
}
