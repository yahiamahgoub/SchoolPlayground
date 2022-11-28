using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolPlayground.DAL;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsProduction())
{
	Console.WriteLine("--> Using SqlServer Db");
	builder.Services.AddDbContext<SchoolPlaygroundContext>(opt =>
		opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
	builder.Services.AddScoped<IStudentRepo, StudentRepo>();

}
else if(builder.Environment.IsStaging())
{
	Console.WriteLine("--> Using InMem Db");
	builder.Services.AddDbContext<SchoolPlaygroundContext>(opt =>
		 opt.UseInMemoryDatabase("InMem"));
	builder.Services.AddScoped<IStudentRepo, StudentRepo>();
}
else
{
	Console.WriteLine("--> Using Dummy Db");
	builder.Services.AddSingleton<IStudentRepo, StudentDummyRepo>();
}

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
