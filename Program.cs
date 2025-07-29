using Microsoft.EntityFrameworkCore;
using Quiz_project.Data;
using Quiz_project.Repositories;
using Quiz_project.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// 1. Add DbContext with connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add Controllers
builder.Services.AddControllers();

// 3. Add Swagger (for API documentation/testing)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: Later: Register Repositories, Authentication, etc.
 builder.Services.AddScoped<IUserRepository, UserRepository>();
 builder.Services.AddScoped< IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

var app = builder.Build();

// 4. Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ?? Middleware (authentication/authorization)
app.UseAuthentication(); // ?? ???? Auth ?????
app.UseAuthorization();

// 5. Map routes to controllers
app.MapControllers();

app.Run();
