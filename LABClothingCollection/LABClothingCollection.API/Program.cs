using LABClothingCollection.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add serviços ao conteiner.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //agregar o serviço de exploração de pontos de acesso à API.
builder.Services.AddSwaggerGen(); //configura e adicionar o serviço de geração de documentação Swagger à coleção de serviços

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true; //URLs são convertidas em minúsculas. 
    options.LowercaseQueryStrings = true; //Cadeias de consulta são convertidas em minúsculas.
});

//configuração para obter a cadeia de conexão de uma base de dados
string connectionstring = builder.Configuration.GetConnectionString("ServerConnection")!;

//configura o serviço de contexto de base de dados para se conectar e trabalhar com uma base de dados SQL Server através do contexto.
builder.Services.AddDbContext<LABClothingCollectionDbContext>(options => options.UseSqlServer(connectionstring));

//configura e adicionar o serviço AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//constroi o host, configuração e serviços necessários para que o aplicativo funcione.
var app = builder.Build();

// Configura o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment()) //verifica se o aplicativo está no modo de desenvolvimento.
{
    app.UseSwagger(); //configura o Swagger. Q permite que vc possa navegar e testar os endpoints da API por meio de uma interface gráfica em seu navegador.
    app.UseSwaggerUI(); //configura o SwaggerUI uma interface de usuário que facilita a visualização e teste da documentação da API gerada pelo Swagger.
}

app.UseHttpsRedirection(); //redireciona automaticamente as solicitações HTTP para seus equivalentes HTTPS.

app.UseAuthorization(); //Permite q a aplicação restrinja o acesso a certas partes ou ações baseadas em políticas de autorização definidas.

app.MapControllers(); //configura o roteamento dos controladores.

app.Run(); //inicia a execução do aplicativo.
