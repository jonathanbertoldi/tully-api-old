using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tully.Api.Data;
using Tully.Api.Filters;
using Tully.Api.Models;
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

        [HttpGet("{desafioId}", Name = "GetDesafio")]
        public async Task<IActionResult> GetDesafio(int desafioId)
        {
            var desafio = await _context
                .Desafios
                .FirstOrDefaultAsync(a => a.Id == desafioId);

            if (desafio == null) return NotFound();

            var result = Mapper.Map<DesafioViewModel>(desafio);

            return Ok(result);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> PostDesafio([FromBody] DesafioPostViewModel model)
        {
            var desafio = Mapper.Map<Desafio>(model);

            await _context.Desafios.AddAsync(desafio);
            await _context.SaveChangesAsync();

            var result = Mapper.Map<DesafioViewModel>(desafio);

            return CreatedAtRoute("GetDesafio", new { desafioId = desafio.Id }, result);
        }

        [ValidateModel]
        [HttpPatch("{desafioId}")]
        public async Task<IActionResult> PatchDesafio(int desafioId, [FromBody] JsonPatchDocument<DesafioUpdateViewModel> patchDocument)
        {
            var desafio = await _context.Desafios.FindAsync(desafioId);

            return Ok();
        }

        [HttpDelete("{desafioId}")]
        public async Task<IActionResult> DeleteDesafio(int desafioId)
        {
            var desafio = await _context.Desafios.FindAsync(desafioId);

            if (desafio == null) return NotFound();

            desafio.RemovidoEm = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
