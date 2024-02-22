using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models.DTOs;
using Models.DTOs.Document;
using Models.Entity.Document;

namespace Core.InterfaceService
{
    public interface IDocumentService
    {
        Task<Response<Document>> PostDocumentAsync(DocumentRequest documentRequest);
    }
}
