using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolPlayground.Models
{
	public class Student
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public int Age { get; set; }
		[Required]
		public string Name { get; set; }
	}

	public class StudentDto
	{
		public int Id { get; set; }
		public int Age { get; set; }
		public string Name { get; set; }
	}

	public class StudentUpsertDto
	{
		[Required]
		public int Age { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
