using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoReminder.Server.DBContext;
using ToDoReminder.Server.Extensions;
using ToDoReminder.Server.Service;
using ToDoReminder.Server.Service.Proxy;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("MySql");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
builder.Services.AddDbContext<ToDoReminderContext>(options => { options.UseMySql(connectionString, serverVersion); });

builder.Services.AddScoped<DbContext, ToDoReminderContext>();

builder.Services.AddTransient<IToDoReminderService, ToDoReminderService>();
builder.Services.AddTransient<IMemoService, MemoService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IStatisticService, StatisticService>();
var automapperConfog = new MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperProFile());
});

builder.Services.AddSingleton(automapperConfog.CreateMapper());

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
