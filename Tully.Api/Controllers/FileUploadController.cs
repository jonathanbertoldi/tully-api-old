using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Tully.Api.Controllers
{
    [Route("api/files")]
    public class FileUploadController : Controller
    {
        private IHostingEnvironment _env;

        public FileUploadController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpPost("foto_perfil")]
        public async Task<IActionResult> UploadFotoPerfil(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}.jpg";
                var uploadPath = Path.Combine(_env.WebRootPath, "fotos_perfil");

                using (var stream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var apiPath = Path.Combine("fotos_perfil", fileName);

                return Ok(apiPath);
            }

            return BadRequest();
        }
    }
}
