using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using School24.DTOs;
using School24.Models;
using System.Data;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public StudentController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> GetTotalAbsencesBySchool([FromQuery] string schoolName)
    {
        if (string.IsNullOrWhiteSpace(schoolName))
        {
            return BadRequest("School name must be provided.");
        }

        var connectionString = _configuration.GetConnectionString("SchoolDbConnectionString");

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("[dbo].[GetTotalAbsencesBySchool]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SchoolName", schoolName);

                    await connection.OpenAsync();
                    var result = await command.ExecuteReaderAsync();

                    if (result.HasRows)
                    {
                        while (await result.ReadAsync())
                        {
                            return Ok(new StudentAbsenceDto
                            {
                                SchoolName = (string)result["Schoolname"],
                                StudentId = (int)result["StudentId"],
                                StudentName = (string)result["StudentName"],
                                AbsenceLength = (int)result["AbsenceLength"]
                            });
                        }
                    }

                    return NotFound("No data found for the provided school name.");
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception (omitted for brevity)
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpGet]
    [Route("students")]
    public async Task<IActionResult> GetStudents()
    {
        var connectionString = _configuration.GetConnectionString("SchoolDbConnectionString");

        if (string.IsNullOrEmpty(connectionString))
        {
            return StatusCode(500, "Database connection string is not configured.");
        }

        try
        {
            await using var connection = new SqlConnection(connectionString);
            await using var command = new SqlCommand("[dbo].[GetStudents]", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            await using var result = await command.ExecuteReaderAsync();

            var students = new List<StudentAbsenceDto>();
            while (await result.ReadAsync())
            {
                students.Add(new StudentAbsenceDto
                {
                    SchoolName = result["SchoolName"].ToString(),
                    StudentId = Convert.ToInt32(result["StudentId"]),
                    StudentName = result["StudentName"].ToString(),
                    AbsenceLength = Convert.ToInt32(result["AbsenceLength"])
                });
            }

            return Ok(students);
        }
        catch (SqlException sqlEx)
        {
            // Log the exception (not shown here)
            return StatusCode(500, $"Database error: {sqlEx.Message}");
        }
        catch (Exception ex)
        {
            // Log the exception (not shown here)
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

}
