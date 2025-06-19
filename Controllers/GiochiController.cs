using Microsoft.AspNetCore.Mvc;
using LudotecaApi.Models;
using LudotecaApi.Services;
using System.Collections.Generic;


namespace LudotecaApi.Controllers
{
    [ApiController]
    [Route("giochidatavolo")]
    public class GiochiDaTavoloController : ControllerBase
    {
        private readonly GiocoService _giocoService;

        public GiochiDaTavoloController(GiocoService giocoService)
        {
            _giocoService = giocoService;
        }

        [HttpGet("filtra")]
        public ActionResult<IEnumerable<GiocoDaTavolo>> Filtra(
            [FromQuery] int? numeroGiocatori,
            [FromQuery] string? nome,
            [FromQuery] string? categoria)
        {
            var risultati = _giocoService.Filtra(nome, categoria, numeroGiocatori);
            return Ok(risultati);
        }

        [HttpGet]
        public ActionResult<IEnumerable<GiocoDaTavolo>> Get()
        {
            return Ok(_giocoService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<GiocoDaTavolo> Get(int id)
        {
            var gioco = _giocoService.GetById(id);
            if (gioco == null) return NotFound();
            return Ok(gioco);
        }

        [HttpPost]
        public ActionResult<GiocoDaTavolo> Post(GiocoDaTavolo nuovoGioco)
        {
            if (nuovoGioco == null) return BadRequest("Gioco non valido");
            _giocoService.Aggiungi(nuovoGioco);
            return CreatedAtAction(nameof(Get), new { id = nuovoGioco.Id }, nuovoGioco);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, GiocoDaTavolo giocoAggiornato)
        {
            var successo = _giocoService.Aggiorna(id, giocoAggiornato);
            if (!successo) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var successo = _giocoService.Elimina(id);
            if (!successo) return NotFound();
            return NoContent();
        }

       

    }
}
