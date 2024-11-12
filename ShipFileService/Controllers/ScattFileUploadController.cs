using Microsoft.AspNetCore.Mvc;
using ScattService.Services;

namespace ScattService.Controllers {
  /// <summary>Upload controller for Scatt files</summary>
  [ApiController]
  [Route("api/[controller]")]
  public class ScattFileUploadController : Controller
  {
    private readonly ISaveScattFileService _saveScattFileService;

    public ScattFileUploadController(ISaveScattFileService saveScattFileService)
    {
      _saveScattFileService = saveScattFileService;
    }

    /// <summary>Upload the Scatt file</summary>
    /// <returns>Returns the uri from the uploaded file</returns>
    [HttpPost(Constants.UploadUri)]
    public async Task<IActionResult> UploadScattFile([FromForm] Dto.ResponseFileDto file)
    {
      return Created((await _saveScattFileService.SaveScattFile(file.ResponseFile)).AbsoluteUri, null);
    }
  }
}
