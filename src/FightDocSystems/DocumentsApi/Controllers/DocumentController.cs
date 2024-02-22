using Core.InterfaceService;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Document;
using Models.DTOs.Fight;

namespace DocumentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
       
        [HttpPost]
        public async Task<IActionResult> PostSingleDocumentAsync([FromForm]DocumentRequest document)
        {
            try
            {
                var response = await _documentService.PostDocumentAsync(document);
                if (response.Succes == true)
                {
                    return Ok(response);
                }
                else return Unauthorized(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
