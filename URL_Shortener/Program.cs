using URL_Shortener.Data;
using URL_Shortener.InterFaces;
using URL_Shortener.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<ExpirationService>();
builder.Services.AddSingleton<IUrlService, UrlService>();
builder.Services.AddSingleton<ICassandraSessionFactory,CassandraSessionFactory>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    // i have to add app.UseSwagger();
    //    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.Run();