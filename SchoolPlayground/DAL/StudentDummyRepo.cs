using SchoolPlayground.Models;

namespace SchoolPlayground.DAL
{
	public class StudentDummyRepo : IStudentRepo
	{
		private readonly List<Student> students;

		public StudentDummyRepo()
		{
			students = new List<Student>()
					{
						new Student()
						{
							 Id = 1,
							 Name = "Björn Mark",
							 Age = 17
						},
						new Student()
						{
							Id = 2,
							Name = "Sara Miller",
							Age = 23
						},
						new Student()
						{
							Id= 3,
							Name = "Ran out of names :P",
							Age = 26
						}
					};
		}
		public Task Add(Student obj)
		{
			obj.Id = students.Count + 1; 
			return Task.Run(() => students.Add(obj));
		}

		public Task Delete(int id)
		{
			var student = students.FirstOrDefault(std => std.Id == id);
			return Task.Run(() => students.Remove(student));
		}

		public Task<bool> EntityExists(int id)
		{
			return Task.Run(() =>  students.FirstOrDefault(std => std.Id == id) is not null? true: false);
		}

		public Task<IEnumerable<Student>> GetAll()
		{
			return Task.Run<IEnumerable<Student>>(() => students );
		}

		public Task<Student> GetById(int id)
		{
			return Task.Run(() => students.FirstOrDefault(std => std.Id == id));
		}

		public Task Save()
		{
			return Task.CompletedTask;
		}
	}
}
