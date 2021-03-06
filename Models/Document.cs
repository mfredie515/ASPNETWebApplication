﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETWebApplication.Models
{
    public class Document
    {
        public Document()
        {
            this.Id = -1;
            this.Filename = "Nothing";
            this.Data = null;
        }

        public Document(int id, string filename, byte[] data)
        {
            this.Id = id;
            this.Filename = filename;
            this.Data = data;
        }

        public int Id { get; set; }
        public string Filename { get; set; }
        public byte[] Data { get; set; }
    }
}
