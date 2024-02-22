﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entity.Document;

namespace Models.DTOs.Document
{
    public class DocumentResponse
    {
        public string DocumentName { get; set; }
        public int DocumentType { get; set; }
        public float DocumentVersion { get; set; }
        public string Path { get; set; }
        
    }
}
