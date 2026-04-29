using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DebitosController : ControllerBase
{
    private readonly DebitoService _service;
    private readonly ContruibuinteService _contribuinteService;

    public DebitosController(DebitoService service, ContruibuinteService contribuinteService)
    {
        _service = service;
        _contribuinteService = contribuinteService;
    }

    [HttpPost]
    public IActionResult CreateDebito([FromBody] CreateDebito dto)
    {
        var contribuinte = _contribuinteService.ContribuinteExists(dto.ContribuinteId);
        if (contribuinte == false)
        {
            return NotFound(new ErrorResponse { Message = "Contribuinte não encontrado", Errors = ["ID do contribuinte inválido"] });
        }
        var result = _service.CriarDebito(dto);
        if (result is ResponseDebito response)
        {
            return CreatedAtAction(nameof(GetDebito), new { id = response.Id }, response);
        }
        return BadRequest(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetDebito(Guid id)
    {
        var debito = _service.GetDebito(id);
        if (debito == null) return NotFound(new { Message = "Débito não encontrado" });
        return Ok(debito);
    }
}