namespace LudotecaApi.Models
{
    public class GiocoDaTavolo
    {
        public int Id { get; set; }
        public string? Titolo { get; set; }
        public string? Editore { get; set; }
        public int NumeroGiocatoriMin { get; set; }
        public int NumeroGiocatoriMax { get; set; }
        public int DurataMedia { get; set; } // in minuti
        public string? Genere { get; set; }
        public int EtaMin { get; set; }
        public int EtaMax { get; set; }
    }
}
