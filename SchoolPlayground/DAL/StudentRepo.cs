using Microsoft.EntityFrameworkCore;
using SchoolPlayground.Models;

namespace SchoolPlayground.DAL
{
	public class StudentRepo : IStudentRepo
	{
		private readonly SchoolPlaygroundContext context;

		public StudentRepo(SchoolPlaygroundContext playgroundContext)
		{
			this.context = playgroundContext;
		}

		public async Task<IEnumerable<Student>> GetAll()
		{
			return await context.Students.ToListAsync();
		}

		public async Task<Student> GetById(int id)
		{
			return await context.Students.FindAsync(id);
		}

		public async Task<bool> EntityExists(int id)
		{
			return await context.Students.FindAsync(id) is not null ? true : false;
		}

		public async Task Add(Student obj)
		{
			await context.Students.AddAsync(obj);
		}
		public async Task Delete(int id)
		{
			var stdClass = await GetById(id);
			context.Students.Remove(stdClass);
		}
		public async Task Save()
		{
			await context.SaveChangesAsync();
		}
	}
}
