using Municipal.Api.Extensions;
using Municipal.DataAccess.Extensions;
using Municipal.Utils.Extensions;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerView();

builder.Services.AddDbContext(builder.Configuration); 
builder.Services.AddDependency();
builder.AddMassTransitConsumerServices();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
