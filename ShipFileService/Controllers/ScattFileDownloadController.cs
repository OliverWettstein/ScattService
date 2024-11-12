using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScattService.Services;

namespace ScattService.Controllers {
  [ApiController]
  [Route("api/[controller]")]
  public class ScattFileDownloadController : Controller
  {
    private readonly IGetScattFileService _getScattFileService;

    public ScattFileDownloadController(IGetScattFileService getScattFileService)
    {
      _getScattFileService = getScattFileService;
    }

    /// <param name="fileGuid">The guid/name from the file</param>
    /// <returns>Returns the file as a FileContentResult</returns>
    [HttpGet(Constants.DownloadUri)]
    public async Task<IActionResult> DownloadScattFile(string fileGuid)
    {
      string fileName = fileGuid;
      byte[]? fileBytes = await _getScattFileService.GetScattFile(fileGuid);
      if (fileBytes != null)
      {
        FileContentResult file = File(fileBytes, "application/pdf", fileName);
        return file;
      }
      else
      {
        return StatusCode(StatusCodes.Status410Gone, "The file is no longer available.");
      }
    }
  }
}
