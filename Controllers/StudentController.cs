using Microsoft.AspNetCore.Mvc;
using AlunosAPI.Services;
using AlunosAPI.DTOs.Student;
using AlunosAPI.Exceptions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AlunosAPI.Controllers;

[Route("api/student")]
[Produces("application/json")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class StudentController : ControllerBase
{
    private IStudentService _service;
    public StudentController(IStudentService service)
    {
        _service = service;
    }

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IAsyncEnumerable<StudentResponseJson>>> GetAll()
    {
        try
        {
            var response = await _service.GetAllAsync();
            if (!response.Any())
            {
                return NoContent();
            }
            return Ok(response);
        }
        catch
        {
            return BadRequest(new { message = "invalid request!" });
        }
    }

    [HttpPost("search")]
    public async Task<ActionResult<IAsyncEnumerable<StudentResponseJson>>> GetByName(
        GetStudentsRequestJson name)
    {

        try
        {
            var response = await _service.GetByNameAsync(name);
            if (!response.Any() || name is null)
            {
                return NoContent();
            }
            return Ok(response);
        }
        catch
        {
            return BadRequest(new { message = "invalid request" });
        }
    }

    [HttpGet("{id:int}", Name = "GetStudent")]
    public async Task<ActionResult<StudentResponseJson>> GetStudent(int id)
    {
        try
        {
            var response = await _service.GetByIdAsync(id);
            return Ok(response);
        }
        catch (StudentNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch
        {
            return BadRequest(new { message = "invalid request" });
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create(RegisterStudentRequestJson request)
    {
        try
        {
            var response = await _service.RegisterAsync(request);
            return CreatedAtRoute(nameof(GetStudent), new { id = response.Id }, response);
        }
        catch
        {
            return BadRequest(new { message = "invalid request" });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(UpdateStudentRequestJson request, int id)
    {
        try
        {
            await _service.UpdateAsync(request, id);
            return NoContent();
        }
        catch (StudentNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch
        {
            return BadRequest(new { message = "invalid request" });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (StudentNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch
        {
            return BadRequest(new { message = "invalid request" });
        }
    }
}
