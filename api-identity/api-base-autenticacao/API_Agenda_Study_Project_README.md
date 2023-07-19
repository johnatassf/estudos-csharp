# API - Estudos .NET Core 7 - Authorizarion and Authentication with identity

[![Swagger](https://img.shields.io/badge/Swagger-Explore%20API-green.svg)](https://localhost:7203/swagger)
[![API Status](https://img.shields.io/badge/status-online-brightgreen.svg)](https://localhost:7203)
[![API Version](https://img.shields.io/badge/API%20Version-v1-blue.svg)](https://localhost:7203)

![.NET Core Version](https://img.shields.io/badge/.NET%20Core-7.0-blueviolet.svg)
![Entity Framework Core Version](https://img.shields.io/badge/Entity%20Framework%20Core-7.0-orange.svg)
![Swagger Version](https://img.shields.io/badge/Swagger-3.0.1-yellow.svg)
![Identity Version](https://img.shields.io/badge/Identity-4.0.0-lightgrey.svg)

## Descrição

Bem-vindo à API Agenda, um projeto de estudos desenvolvido em .NET Core 7 com separação de domínios, infraestrutura e controladores. 
A API utiliza o Identity para autenticação e o Entity Framework Core para interagir com o banco de dados.

## Como Executar

1. Certifique-se de ter o .NET Core 7 instalado em sua máquina.

2. Clone este repositório em sua máquina local.

3. Navegue até o diretório da API no terminal.

4. Execute o seguinte comando para iniciar a API:

```
dotnet run --project api-base-autenticacao.csproj
```

5. Acesse a documentação do Swagger para explorar os endpoints da API:

   - URL do Swagger (HTTPS): [https://localhost:7203/swagger](https://localhost:7203/swagger)
   - URL do Swagger (HTTP): [http://localhost:5164/swagger](http://localhost:5164/swagger)

## Endpoints

A API Agenda oferece os seguintes endpoints principais:

- **POST /authentication/user**: Cria um novo usuário no sistema. Parâmetros: `FirstName`, `LastName`, `Email` e `UserName`.

- **POST /authentication/token**: Gera um token de acesso para autenticação de usuários. Parâmetros: `Username` e `Password`.

 A utilização da classe "WeatherForecast" apenas mantendo a classe criada por padrão ao criar a api. 
 
- **GET /WeatherForecast/summaries**: Obtém a lista de resumos de previsão do tempo.

- **GET /WeatherForecast**: Obtém a lista completa de previsão do tempo.

- **POST /WeatherForecast**: Cria uma nova previsão do tempo. Corpo da requisição: Objeto JSON com as propriedades `date`, `temperatureC` e `summary`.

- **GET /WeatherForecast/{id}**: Obtém os detalhes da previsão do tempo com o ID especificado.

- **DELETE /WeatherForecast/{id}**: Exclui a previsão do tempo com o ID especificado.

## Contribuição

Se você deseja contribuir com este projeto, sinta-se à vontade para enviar pull requests. Toda contribuição é bem-vinda!

---
