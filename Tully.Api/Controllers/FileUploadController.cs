using Firebase.Storage;
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

                var fileUrl = await new FirebaseStorage("tullyteste.appspot.com")
                    .Child("fotos_perfil")
                    .Child(fileName)
                    .PutAsync(image.OpenReadStream());

                return Ok(new { fileUrl = fileUrl});
            }

            return BadRequest();
        }
    }
}
