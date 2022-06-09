using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProgrammingCompetitionService.Models;
using ProgrammingCompetitionService.Services;
using ProgrammingCompetitionService.Intrerfaces;
using ProgrammingCompetitionService.Repositories;
using ProgrammingCompetitionService.Middleware;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<PCContext>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<ITasksRepository, TasksRepository>();
builder.Services.AddTransient<ITaskDetailsRepository, TaskDetailsRepository>();
builder.Services.AddTransient<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<ICompletedTasksRepository, CompletedTasksRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:JWT:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Programming Competition", Version = "v0.2.0" });
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standart Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddHttpClient<JdoodleService>(httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetSection("AppSettings:Jdoodle:BaseAdress").Value);
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Programming Competition v1");
        c.RoutePrefix = String.Empty;
    }
    );

}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(builder=>builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();

