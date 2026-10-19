using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Models;

namespace SpotifyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CancionesController : ControllerBase
{
    // Simulación de base de datos en memoria
    private static readonly List<Cancion> _catalogo = new()
    {
        new Cancion
        {
            Id = 1,
            Titulo = "Blinding Lights",
            Artista = "The Weeknd",
            Album = "After Hours",
            DuracionSegundos = 200,
            Reproducciones = 4200000000,
            EsExplicita = false
        },
        new Cancion
        {
            Id = 2,
            Titulo = "Monaco",
            Artista = "Bad Bunny",
            Album = "Nadie Sabe Lo Que Va a Pasar Mañana",
            DuracionSegundos = 267,
            Reproducciones = 850000000,
            EsExplicita = true
        }
    };

    // 1. GET: api/canciones (Obtener todas)
    [HttpGet]
    public ActionResult<IEnumerable<Cancion>> ObtenerTodas()
    {
        return Ok(_catalogo);
    }

    // 2. GET: api/canciones/1 (Obtener por ID)
    [HttpGet("{id:int}")]
    public ActionResult<Cancion> ObtenerPorId(int id)
    {
        var cancion = _catalogo.FirstOrDefault(c => c.Id == id);
        if (cancion is null)
            return NotFound(new { Mensaje = $"La canción con ID {id} no fue encontrada." });

        return Ok(cancion);
    }

    // 3. POST: api/canciones (Crear canción)
    [HttpPost]
    public ActionResult<Cancion> Crear([FromBody] Cancion nuevaCancion)
    {
        nuevaCancion.Id = _catalogo.Any() ? _catalogo.Max(c => c.Id) + 1 : 1;
        _catalogo.Add(nuevaCancion);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaCancion.Id }, nuevaCancion);
    }

    // 4. PUT: api/canciones/1 (Reemplazo total)
    [HttpPut("{id:int}")]
    public IActionResult Reemplazar(int id, [FromBody] Cancion cancionActualizada)
    {
        var cancionExistente = _catalogo.FirstOrDefault(c => c.Id == id);
        if (cancionExistente is null)
            return NotFound(new { Mensaje = $"La canción con ID {id} no existe." });

        cancionExistente.Titulo = cancionActualizada.Titulo;
        cancionExistente.Artista = cancionActualizada.Artista;
        cancionExistente.Album = cancionActualizada.Album;
        cancionExistente.DuracionSegundos = cancionActualizada.DuracionSegundos;
        cancionExistente.Reproducciones = cancionActualizada.Reproducciones;
        cancionExistente.EsExplicita = cancionActualizada.EsExplicita;

        return NoContent(); // 204 No Content
    }

    // 5. PATCH: api/canciones/1 (Actualización parcial)
    [HttpPatch("{id:int}")]
    public ActionResult<Cancion> ActualizarParcialmente(int id, [FromBody] CancionPatchDto cambios)
    {
        var cancionExistente = _catalogo.FirstOrDefault(c => c.Id == id);
        if (cancionExistente is null)
            return NotFound(new { Mensaje = $"La canción con ID {id} no existe." });

        if (cambios.Titulo is not null)
            cancionExistente.Titulo = cambios.Titulo;

        if (cambios.Artista is not null)
            cancionExistente.Artista = cambios.Artista;

        if (cambios.Album is not null)
            cancionExistente.Album = cambios.Album;

        if (cambios.DuracionSegundos.HasValue)
            cancionExistente.DuracionSegundos = cambios.DuracionSegundos.Value;

        if (cambios.Reproducciones.HasValue)
            cancionExistente.Reproducciones = cambios.Reproducciones.Value;

        if (cambios.EsExplicita.HasValue)
            cancionExistente.EsExplicita = cambios.EsExplicita.Value;

        return Ok(cancionExistente);
    }

    // 6. DELETE: api/canciones/1 (Eliminar)
    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        var cancion = _catalogo.FirstOrDefault(c => c.Id == id);
        if (cancion is null)
            return NotFound(new { Mensaje = $"La canción con ID {id} no existe." });

        _catalogo.Remove(cancion);
        return NoContent(); // 204 No Content
    }
}