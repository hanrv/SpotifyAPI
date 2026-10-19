namespace SpotifyAPI.Models;

public class Cancion
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Artista { get; set; }
    public required string Album { get; set; }
    public int DuracionSegundos { get; set; }
    public long Reproducciones { get; set; }
    public bool EsExplicita { get; set; }
}