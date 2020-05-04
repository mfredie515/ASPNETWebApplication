using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETWebApplication.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public byte[] Data { get; set; }
    }
}
