using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.ViewModels.DesafioViewModels;

namespace Tully.Api.Controllers
{
    [Authorize]
    [Route("api/desafios")]
    public class DesafioController : Controller
    {
        private TullyContext _context;

        public DesafioController(TullyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDesafios()
        {
            var desafios = await _context
                .Desafios
                .Where(d => d.RemovidoEm == null)
                .ToListAsync();

            var result = Mapper.Map<IEnumerable<DesafioViewModel>>(desafios);

            return Ok(result);
        }
    }
}
