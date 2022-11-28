using SchoolPlayground.Models;

namespace SchoolPlayground.DAL
{
	public interface IStudentRepo
	{
		Task Add(Student obj);
		Task Delete(int id);
		Task<bool> EntityExists(int id);
		Task<IEnumerable<Student>> GetAll();
		Task<Student> GetById(int id);
		Task Save();
	}
}
