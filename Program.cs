using Microsoft.EntityFrameworkCore;
using School24.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContextFactory<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbConnectionString"), sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(1);
    }));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
