using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.InterfaceService;
using Models.DTOs.Document;
using Models.DTOs;
using Models.Entity.Document;
using Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Core.Enum;
using Models.DTOs.Fight;
using Models.Entity.Fight;
using AutoMapper;
namespace Core.ManagerSerice
{
    public class DocumentService:IDocumentService
    {
        private readonly AppDbContext _appDbContext;
        //private readonly IMapper _autoMapper;
        public DocumentService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            //_autoMapper = autoMapper;
        }
        private async Task<Document> IsDocumentSavedAsync(DocumentRequest documentRequest)
        {
            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory() , "Shares" , "Documents");
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }
            var documentName = Guid.NewGuid().ToString() + "-" + documentRequest.DocumentDetail.FileName;
            var documentPath = Path.Combine(uploadDirectory, documentName);
            var document = new Document
            {
                FlightId = documentRequest.FlightId,
                DocumentName = documentName,
                DocumentType = documentRequest.DocumentTypeId,
                FileType = documentRequest.DocumentDetail.ContentType,
                Path = "Shares\\Documents\\"+documentName,
            };
            try
            {
                using (var fileStream = new FileStream(documentPath, FileMode.Create))
                {
                    await documentRequest.DocumentDetail.CopyToAsync(fileStream);
                }
                _appDbContext.Document.Add(document);
                await _appDbContext.SaveChangesAsync();
                return document;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Response<Document>> PostDocumentAsync(DocumentRequest documentRequest)
        {
            var response = new Response<Document>();
            try
            {
                var resultUploadDoc = await IsDocumentSavedAsync(documentRequest);

                if (resultUploadDoc != null)
                {
                    response.Data= resultUploadDoc;
                    response.StatusCode = 201;
                    response.Message = $"Post file '{documentRequest.DocumentDetail.FileName}' sucessfuly";
                    response.Succes = true;
                    return response;
                }
                else
                {
                    response.StatusCode = 401;
                    response.Message = $"Post file ''{documentRequest.DocumentDetail.FileName}'' failed";
                    response.Succes = false;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.Succes= false; 
                return response;
            }
            return response;

        }
        public async Task<IEnumerable<DocumentResponse>> ConvertDocumentsToResponses(IEnumerable<Document> docs)
        {            
            return docs.Select(d => new DocumentResponse
            {
                DocumentName = d.DocumentName,
                DocumentType = d.DocumentType,
                DocumentVersion = d.DocumentVersion,
                Path = d.Path,                
            });
        }
        public async Task<Response<IEnumerable<DocumentResponse>>> GetDocumentResponsesByFlightIdAsync(int flightId)
        {
            var response = new   Response < IEnumerable < DocumentResponse >> ();
            try
            {
                var documents = await _appDbContext.Document.Where(d => d.FlightId == flightId).ToListAsync();
                if (documents != null && documents.Any())
                {
                    var data = await ConvertDocumentsToResponses(documents);
                    if (data != null)
                    {
                        response.Data = data;
                    }
                    response.StatusCode = 200;
                    response.Message = "Documents retrieved successfully";
                    response.Succes = true;
                    
                }
                else
                {
                    response.StatusCode = 401;
                    response.Message = $"No Documents found";
                    response.Succes = false;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.Succes = false;
                
            }
            return response;

        }
    }
}
