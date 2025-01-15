using Library_Management_API.BLL.Services.IServices;
using Library_Management_API.BLL.Services.ServicesImpl;
using Library_Management_API.DAL;
using Library_Management_API.DAL.Repositories.IRepositories;
using Library_Management_API.DAL.Repositories.RepositoriesImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()//logs to console
    .Enrich.FromLogContext()//add cotext data
    .MinimumLevel.Debug()//minimum log level
    .CreateLogger();
builder.Host.UseSerilog();//use serilog as the logging provider

//services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();
builder.Services.AddScoped<IBorrowingService, BorrowingService>();
builder.Services.AddScoped<IReportingService, ReportingService>();
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
