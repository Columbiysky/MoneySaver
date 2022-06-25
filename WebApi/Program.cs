using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
builder.Services.AddSwaggerGen(c =>
    {
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
//builder.WebHost.UseUrls("http://localhost:5290/");

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
