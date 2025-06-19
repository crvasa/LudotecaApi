using LudotecaApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace LudotecaApi.Services
{
    public class GiocoService
    {
        private static List<GiocoDaTavolo> _giochi = new List<GiocoDaTavolo>
        {
            new GiocoDaTavolo { Id = 1, Titolo = "Monopoly", Editore = "Hasbro", NumeroGiocatoriMin = 2, NumeroGiocatoriMax = 6, DurataMedia = 120, Genere = "Strategia", EtaMin = 8, EtaMax = 99 },
            new GiocoDaTavolo { Id = 2, Titolo = "Risiko!", Editore = "Editrice Giochi", NumeroGiocatoriMin = 2, NumeroGiocatoriMax = 6, DurataMedia = 120, Genere = "Strategia", EtaMin = 10, EtaMax = 99 },
            new GiocoDaTavolo { Id = 3, Titolo = "Cluedo", Editore = "Hasbro", NumeroGiocatoriMin = 3, NumeroGiocatoriMax = 6, DurataMedia = 50, Genere = "Mistero", EtaMin = 8, EtaMax = 99 },
            new GiocoDaTavolo { Id = 4, Titolo = "7 Wonders", Editore = "Repos Production", NumeroGiocatoriMin = 3, NumeroGiocatoriMax = 7, DurataMedia = 30, Genere = "Strategia", EtaMin = 10, EtaMax = 99 },
            new GiocoDaTavolo { Id = 5, Titolo = "Nemesis", Editore = "Cranio Creations", NumeroGiocatoriMin = 1, NumeroGiocatoriMax = 5, DurataMedia = 90, Genere = "Fantascienza", EtaMin = 14, EtaMax = 99 }
        };

        public List<GiocoDaTavolo> GetAll() => _giochi;

        public GiocoDaTavolo? GetById(int id) => _giochi.FirstOrDefault(g => g.Id == id);

        public void Aggiungi(GiocoDaTavolo gioco)
        {
            gioco.Id = _giochi.Any() ? _giochi.Max(g => g.Id) + 1 : 1;
            _giochi.Add(gioco);
        }

        public bool Aggiorna(int id, GiocoDaTavolo giocoAggiornato)
        {
            var esistente = _giochi.FirstOrDefault(g => g.Id == id);
            if (esistente == null) return false;

            esistente.Titolo = giocoAggiornato.Titolo;
            esistente.Editore = giocoAggiornato.Editore;
            esistente.NumeroGiocatoriMin = giocoAggiornato.NumeroGiocatoriMin;
            esistente.NumeroGiocatoriMax = giocoAggiornato.NumeroGiocatoriMax;
            esistente.DurataMedia = giocoAggiornato.DurataMedia;
            esistente.Genere = giocoAggiornato.Genere;
            esistente.EtaMin = giocoAggiornato.EtaMin;
            esistente.EtaMax = giocoAggiornato.EtaMax;

            return true;
        }

        public bool Elimina(int id)
        {
            var gioco = _giochi.FirstOrDefault(g => g.Id == id);
            if (gioco == null) return false;

            _giochi.Remove(gioco);
            return true;
        }

        public List<GiocoDaTavolo> Filtra(string? nome, string? categoria, int? numeroGiocatori)
        {
            var query = _giochi.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(g => g.Titolo?.Contains(nome, System.StringComparison.OrdinalIgnoreCase) == true);

            if (!string.IsNullOrWhiteSpace(categoria))
                query = query.Where(g => g.Genere?.Equals(categoria, System.StringComparison.OrdinalIgnoreCase) == true);

            if (numeroGiocatori.HasValue)
                query = query.Where(g => g.NumeroGiocatoriMin <= numeroGiocatori &&
                                         g.NumeroGiocatoriMax >= numeroGiocatori);

            return query.ToList();
        }
    }
}
