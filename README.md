## Desafio

Criar uma API para gerenciar o ciclo mais básico de uma locadora, cadastrar um filme, cadastrar locador, locar um filme e devolver um filme.

**Regras**

1.	Um locador não pode se repetir
2.	Não é permitido excluir fisicamente um registro
3.	Não permitir alugar um filme que não está disponível
4.	Alertar na devolução se o filme está com atraso

## Solução

A ideia foi separar o projeto em 3 camadas, seguindo a seguinte estrutura: 

+ API
  + Profiles
  + Controllers
+ Domain
  + Entities
  + Services
  + DTO
  + Seedwork
+ Infrastructure
  + Mappings
  + Repositories
  + Seedwork
+ Test
  + Application (TO-DO)
  + Domain (TO-DO)
  
<p align="justify">
  Começando pela camada de domínio, o agregado <i><b>CustomerAgg</b></i> é o mais importante, através dele  os consumidores deste domínio poderá locar ou devolver um filme,
  procurei deixar o domínio o mais rico possível. Uma ressalva são as classes DTO que possuem apenas propriedades.
</p>

<p align="justify">
  Quanto as demais camadas, sem novidades, utilizei os padrões Repository e UnitOfWork na infrastrutura. Na API usei o AutoMapper para converter a entitidade de domínio 
  para objeto DTO e o container de injeção de dependência do próprio aspnet core.
</p>

## Tecnologias utilizadas

- [Talend API Tester](https://chrome.google.com/webstore/detail/talend-api-tester-free-ed/aejoelaoggembcahagimdiliamlcdmfm)
- [Visual Studio 2019](https://visualstudio.microsoft.com/pt-br/downloads/)
- [ASP.NET Core 5](https://dotnet.microsoft.com/download)
- [Microsoft SQL Server 2017 Express](https://www.microsoft.com/en-us/download/details.aspx?id=55994)
- [AutoMapper 10.1.1](https://automapper.org/)
- [Entity Framework Core 5.0.2](https://docs.microsoft.com/pt-br/ef/core/)
- [Swagger](https://swagger.io/)

## Executando o projeto

<p align="justify">
Espero que você tenha instalado as tecnologias citadas acima, caso contrátrio, será necessário a instalação para o 
perfeito funcionamento do projeto.
</p>

<p align="justify">
<strong>1º Passo:</strong> Crie um diretório na sua máquina e clone o projeto do Github com o seguinte comando:

```shell
git clone https://github.com/osouzaelias/videostore.git
```
</p>

> **Observação:** Se preferir usar uma IDE basta importar o projeto.

<p align="justify">
<strong>2º Passo:</strong> Acesse o diretório em que você clonou e restaure as dependencias com o
seguinte comando:

```shell
dotnet restore
```
</p>

<p align="justify">
<strong>3º Passo:</strong> Depois que concluir, execute o build da solução com o seguinte comando:

```shell
dotnet build
```
</p>

<p align="justify">
<strong>4º Passo:</strong> Depois que concluir, acesse o diretorio raiz do projeto VideoStore.API e execute o comando:

```shell
dotnet run
```
</p>

<p align="justify">
  Com a API em execução no terminal, acesse o endereço dela no Chrome e a documentação da API será exibida mas,
  se preferir testar usando uma ferramenta sugiro usar a extensão do Chrome Talend API Tester.
</p>

Até mais. :vulcan_salute:
