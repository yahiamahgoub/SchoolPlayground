using Microsoft.EntityFrameworkCore;
using SchoolPlayground.Models;
using System.Collections.Generic;

namespace SchoolPlayground.DAL
{
	public class SchoolPlaygroundContext : DbContext
	{
		public SchoolPlaygroundContext(DbContextOptions options) : base(options)
		{ }
		public DbSet<Student> Students { get; set; }		
	}
}
