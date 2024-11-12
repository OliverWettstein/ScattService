using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScattService.Services;

namespace ScattService.Controllers {
  [ApiController]
  [Route("api")]
  public class ScattServiceTestController : Controller
  {

    public ScattServiceTestController()
    {
      
    }

    [HttpGet(Constants.TestUri)]
    public async Task<IActionResult> TestScattService()
    {
      return StatusCode(200);
    }
  }
}
