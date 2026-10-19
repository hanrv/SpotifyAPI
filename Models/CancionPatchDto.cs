namespace SpotifyAPI.Models;

public class CancionPatchDto
{
    public string? Titulo { get; set; }
    public string? Artista { get; set; }
    public string? Album { get; set; }
    public int? DuracionSegundos { get; set; }
    public long? Reproducciones { get; set; }
    public bool? EsExplicita { get; set; }
}