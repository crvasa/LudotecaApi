// Controller che espone l'API esterna tramite il tuo backend
using LudotecaApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;
using System.Xml;

namespace LudotecaApi.Controllers
{
    [ApiController]
    [Route("giochi-esterni")]
    public class GiochiEsterniController : ControllerBase
    {
        [HttpGet("{nome}")]
        public async Task<IActionResult> SearchGame(string nome)
        {
            using var client = new HttpClient();
            var url = $"https://boardgamegeek.com/xmlapi2/search?query={WebUtility.UrlEncode(nome)}";
            var xml = await client.GetStringAsync(url);

            var doc = new XmlDocument();
            doc.LoadXml(xml);

            var item = doc.SelectSingleNode("//item");
            if (item == null) return NotFound();

            var idAttr = item.Attributes?["id"];
            if (idAttr == null) return NotFound("ID mancante nella risposta XML");
            int id = int.Parse(idAttr.Value);
            return await GetGameDetailsById(id);
        }

        private async Task<IActionResult> GetGameDetailsById(int id)
        {
            using var client = new HttpClient();
            var url = $"https://boardgamegeek.com/xmlapi2/thing?id={id}&versions=0";
            var xml = await client.GetStringAsync(url);

            var doc = new XmlDocument();
            doc.LoadXml(xml);

            var item = doc.SelectSingleNode("//item");
            if (item == null) return NotFound();

            var nameNode = item.SelectSingleNode("name[@type='primary']");
            var yearNode = item.SelectSingleNode("yearpublished");
            var minNode = item.SelectSingleNode("minplayers");
            var maxNode = item.SelectSingleNode("maxplayers");
            var thumbNode = item.SelectSingleNode("thumbnail");

            if (nameNode?.Attributes?["value"] == null) return NotFound();

            var valueAttr = nameNode?.Attributes?["value"];
            if (valueAttr == null)
                return NotFound("Nome non disponibile");

            var name = valueAttr.Value;
            var year = int.TryParse(yearNode?.Attributes?["value"]?.Value, out int y) ? y : 0;
            var minp = int.TryParse(minNode?.Attributes?["value"]?.Value, out int mi) ? mi : 0;
            var maxp = int.TryParse(maxNode?.Attributes?["value"]?.Value, out int ma) ? ma : 0;
            var thumb = thumbNode?.InnerText ?? "";

            return Ok(new GameDetails
            {
                Id = id,
                Name = name,
                YearPublished = year,
                MinPlayers = minp,
                MaxPlayers = maxp,
                Thumbnail = thumb
            });
        }


    }
}
