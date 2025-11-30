using ECommerceAPI.Services;
using ECommerceAPI.Services.Files;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class ReportController(IReportService report, IBlobService blobs) : ControllerBase
{
    [HttpGet("product")]
    public ActionResult GetProducts()
    {
        var stream = GetProductReport();
        
        return File(stream, "application/pdf");
    }
    
    [HttpGet("blob")]
    public async Task<ActionResult> GetBlob()
    {
        var stream = GetProductReport();

        try
        {
            await blobs.UploadFile("productReport.pdf", stream);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        
        return Ok();
    }

    private Stream GetProductReport()
    {
        var stream = new MemoryStream();
        
        report.GenerateProductReport(stream);
        stream.Position = 0;
        
        return stream;
    }
}