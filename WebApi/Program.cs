using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(10, 5, 15))));
builder.Services.AddDbContext<CategoryContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(10, 5, 15))));
builder.Services.AddDbContext<SubCategoryContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(10, 5, 15))));
builder.Services.AddDbContext<DataConext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(10, 5, 15))));
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

app.UseRouting();
app.MapControllers();

app.Run();
