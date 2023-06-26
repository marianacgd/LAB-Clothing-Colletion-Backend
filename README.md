# LAB-Clothing-Colletion-Backend
![version](https://img.shields.io/static/v1?label=version&message=1.0.0&color=blue)
![release-date](https://img.shields.io/badge/release%20date-06--2023-green)

LAB-Clothing-Collection-Backend  é uma aplicação Back-End audaciosa para gerenciar suas coleções e modelos de vestuário criados por determinados usuários da equipe de moda no setor de vestuário.

# 🗂️**Acesso ao projeto**

Você pode acessar ou baixar o código fonte do projeto no GitHub em: [https://github.com/marianacgd/LAB-Clothing-Colletion-Backend](https://github.com/marianacgd/LAB-Clothing-Colletion-Backend)

Você pode acessar o Trello do projeto em: [https://trello.com/invite/b/I5W0lfR0/ATTIfbab67dc14828a250164d3eb7de5c912732D4FE1/api-lab-clothing-collection](https://trello.com/invite/b/I5W0lfR0/ATTIfbab67dc14828a250164d3eb7de5c912732D4FE1/api-lab-clothing-collection)


## Abrir e rodar o projeto

Após baixar o projeto, você pode abrir com o `Visual Studio 2022` ou com o `VS Code`.
<br>
As tecnologias utilizadas são:
* C#
* .Net 7.0
* SQL Server

## ⚙️**Configurações**
Para execução dessa aplicação é necessário criar a base de dados, conforme definido na [classe de programa](https://github.com/marianacgd/LAB-Clothing-Colletion-Backend/blob/main/LABClothingCollection/LABClothingCollection.API/Program.cs). Devido a utilização do Entity Framework (EF), as tabelas utilizadas foram definidas dentro da aplicação em models, sendo necessário a execução de alguns comandos com a finalidade de criar e popular as tabelas para seu primeiro uso.

## 📜**Comandos utilizados**
### Visual Studio 2022
* Add-Migration InitialCreate
* Update-Database
### VS Code
* dotnet ef migrations add InitialCreate 
* dotnet ef database update

### No `VS Code` pode ser necessário instalar o EF: `dotnet tool install --global dotnet-ef`.


## 📄**Documentação da API**  
Para compreender melhor as funcionalidades existentes na aplicação, utilizamos a interface do swagger.<br>
A API contém 3 seções definidas, sendo elas:

1) Usuários
   <br>
   Serviço responsável por gerenciar o cadastro de usuários.
   <table>
   <tr>
   <td>Método</td>
   <td>EndPoint</td>
   <td>Descrição</td>
   </tr>
   <tr>
   <td>POST</td>
   <td>/api/usuarios</td>
   <td>Inclui um novo usuário no sistema.</td>
   </tr>
   <tr>
   <td>PUT</td>
   <td>/api/usuarios/{identificador}</td>
   <td>Altera o cadastro de um usuário, a partir do identificador fornecido.</td>
   </tr>
   <tr>
   <td>PUT</td>
   <td>/api/usuarios/{identificador}/status</td>
   <td>Altera/Atualiza o status de um usuário, a partir do identificador fornecido.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/usuarios</td>
   <td>Exibe todos os usuários cadastrados no sistema.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/usuarios/{identificador}</td>
   <td>Busca o cadastro de um determinado usuário, a partir do identificador informado.</td>
   </tr>
   </table>
       
2) Coleções
   <br>
   Serviço responsável por gerenciar o cadastro de coleções.
   <table>
   <tr>
   <td>Método</td>
   <td>EndPoint</td>
   <td>Descrição</td>
   </tr>
   <tr>
   <td>POST</td>
   <td>/api/colecoes</td>
   <td>Inclui uma nova coleção no sistema.</td>
   </tr>
   <tr>
   <td>PUT</td>
   <td>/api/colecoes/{identificador}</td>
   <td>Altera o cadastro de uma coleção, a partir do identificador fornecido.</td>
   </tr>
   <tr>
   <td>PUT</td>
   <td>/api/colecoes/{identificador}/status</td>
   <td>Altera/Atualiza o status de uma coleção, a partir do identificador fornecido.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/colecoes</td>
   <td>Exibe todas as coleções cadastradas no sistema.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/colecoes/{identificador}</td>
   <td>Busca o cadastro de uma determinada coleção, a partir do identificador informado.</td>
   </tr>
   <tr>
   <td>DELETE</td>
   <td>/api/colecoes/{identificador}</td>
   <td>Remove do cadastro a coleção informada no identificador da requisição.</td>
   </tr>
   </table>
   
3) Modelos
   <br>
   Serviço responsável por gerenciar o cadastro de modelos.
   <table>
   <tr>
   <td>Método</td>
   <td>EndPoint</td>
   <td>Descrição</td>
   </tr>
   <tr>
   <td>POST</td>
   <td>/api/modelos</td>
   <td>Inclui um novo modelo no sistema.</td>
   </tr>
   <tr>
   <td>PUT</td>
   <td>/api/modelos/{identificador}</td>
   <td>Altera o cadastro de um modelo, a partir do identificador fornecido.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/modelos</td>
   <td>Exibe todos os modelos cadastrados no sistema.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/modelos/{identificador}</td>
   <td>Busca o cadastro de um determinado modelo, a partir do identificador informado.</td>
   </tr>
   <tr>
   <td>DELETE</td>
   <td>/api/modelos/{identificador}</td>
   <td>Remove do cadastro o modelo informado no identificador da requisição.</td>
   </tr>
   </table>

