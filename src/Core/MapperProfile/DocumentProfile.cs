using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Models.DTOs.Document;
using Models.Entity.Document;

namespace Core.MapperProfile
{
    public class DocumentProfile:Profile
    {
        public DocumentProfile() 
        {
            CreateMap<Document, DocumentResponse>();
        }
    }
}
