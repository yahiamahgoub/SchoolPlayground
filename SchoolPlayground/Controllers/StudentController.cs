using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolPlayground.DAL;
using SchoolPlayground.Models;

namespace SchoolPlayground.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class StudentController : ControllerBase
	{
		private readonly ILogger<StudentController> logger;
		private readonly IStudentRepo repo;
		private readonly IMapper mapper;
		public StudentController(
			IStudentRepo repo,
			IMapper mapper,
			ILogger<StudentController> logger
			)
		{
			this.logger = logger;
			this.repo = repo;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudents()
		{
			try
			{
				var students = await repo.GetAll();
				return Ok(mapper.Map<IEnumerable<StudentDto>>(students));
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				return StatusCode(500, "Request failed, try again later!");
			}
		}

		[HttpGet("{id}", Name = "GetStudentById")]
		public async Task<ActionResult<StudentDto>> GetStudent(int id)
		{
			try
			{
				var studentDto = mapper.Map<StudentDto>(await repo.GetById(id));
				return studentDto is null ? NotFound() : Ok(studentDto);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				return StatusCode(500, "Request failed, try again later!");
			}
		}

		[HttpPost]
		public async Task<ActionResult> AddStudent(StudentUpsertDto studentUpsertDto)
		{
			try
			{
				var student = mapper.Map<Student>(studentUpsertDto);
				await repo.Add(student);
				await repo.Save();
				var studentDto = mapper.Map<StudentDto>(student);
				
				logger.LogInformation($"New student with id: {studentDto.Id} was created");

				return CreatedAtRoute("GetStudentById", new { id = studentDto.Id }, studentDto);
			}
			catch (Exception ex)
			{
				logger.LogError($"Adding student with values: {studentUpsertDto}, failed with message: {ex.Message}");
				return StatusCode(500, "Request failed, try again later!");
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateStudent(int id, StudentUpsertDto studentUpsertDto)
		{
			try
			{
				if (!await repo.EntityExists(id))
					return NotFound();
				var student = await repo.GetById(id);
				mapper.Map(studentUpsertDto, student);
				await repo.Save();

				logger.LogInformation($"Student with id: {id} was updated");

				return NoContent();
			}
			catch (Exception ex)
			{
				logger.LogError($"Updating student with id: {id}, and values: {studentUpsertDto}, failed with message: {ex.Message}");
				return StatusCode(500, "Request failed, try again later!");
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> RemoveStudent(int id)
		{
			try
			{
				if (!await repo.EntityExists(id))
					return NotFound();
				await repo.Delete(id);
				await repo.Save();
				
				logger.LogInformation($"Student with id: {id} was removed");
				
				return NoContent();
			}
			catch (Exception ex)
			{
				logger.LogError($"Removing student with id: {id}, failed with message: {ex.Message}");
				return StatusCode(500, "Request failed, try again later!");
			}
		}
	}
}
