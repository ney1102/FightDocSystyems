using Core.InterfaceService;
using Core.ManagerSerice;
using Core.MapperProfile;
using Microsoft.EntityFrameworkCore;
using Models.Context;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var AllowOrigins = "_allowOrigins";
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddCustomMapper<DocumentProfile>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDocumentService,DocumentService>();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: AllowOrigins,
//        policy =>
//        {
//            policy.WithOrigins("https://localhost:7171")
//            .AllowAnyHeader().AllowAnyHeader().AllowAnyOrigin();
//        });
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseCors(AllowOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
