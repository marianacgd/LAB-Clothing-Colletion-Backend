using LABClothingCollection.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add servi�os ao conteiner.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //agregar o servi�o de explora��o de pontos de acesso � API.
builder.Services.AddSwaggerGen(); //configura e adicionar o servi�o de gera��o de documenta��o Swagger � cole��o de servi�os

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true; //URLs s�o convertidas em min�sculas. 
    options.LowercaseQueryStrings = true; //Cadeias de consulta s�o convertidas em min�sculas.
});

//configura��o para obter a cadeia de conex�o de uma base de dados
string connectionstring = builder.Configuration.GetConnectionString("ServerConnection")!;

//configura o servi�o de contexto de base de dados para se conectar e trabalhar com uma base de dados SQL Server atrav�s do contexto.
builder.Services.AddDbContext<LABClothingCollectionDbContext>(options => options.UseSqlServer(connectionstring));

//configura e adicionar o servi�o AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//constroi o host, configura��o e servi�os necess�rios para que o aplicativo funcione.
var app = builder.Build();

// Configura o pipeline de solicita��o HTTP.
if (app.Environment.IsDevelopment()) //verifica se o aplicativo est� no modo de desenvolvimento.
{
    app.UseSwagger(); //configura o Swagger. Q permite que vc possa navegar e testar os endpoints da API por meio de uma interface gr�fica em seu navegador.
    app.UseSwaggerUI(); //configura o SwaggerUI uma interface de usu�rio que facilita a visualiza��o e teste da documenta��o da API gerada pelo Swagger.
}

app.UseHttpsRedirection(); //redireciona automaticamente as solicita��es HTTP para seus equivalentes HTTPS.

app.UseAuthorization(); //Permite q a aplica��o restrinja o acesso a certas partes ou a��es baseadas em pol�ticas de autoriza��o definidas.

app.MapControllers(); //configura o roteamento dos controladores.

app.Run(); //inicia a execu��o do aplicativo.
