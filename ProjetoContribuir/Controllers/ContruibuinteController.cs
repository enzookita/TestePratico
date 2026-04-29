using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ContribuintesController : ControllerBase
{
    private readonly ContruibuinteService _service;

    public ContribuintesController(ContruibuinteService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult CreateContribuinte([FromBody] CreateContribuinteDto dto)
    {

        var result = _service.CreateContribuinte(dto);

        if (result is ResponseContribuinte response)
        {
            return CreatedAtAction(nameof(GetContribuinte), new { id = response.Id }, response);
        }
        return BadRequest(result);

    }
    [HttpGet("{id}")]
    public IActionResult GetContribuinte(Guid id)
    {
        var contribuinte = _service.GetContribuinte(id);
        if (contribuinte == null) return NotFound(new { Message = "Contribuinte não encontrado" });
        return Ok(contribuinte);
    }


}