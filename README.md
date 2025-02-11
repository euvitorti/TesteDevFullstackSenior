# API RESTful Guia de Motéis!

## Objetivos e Requisitos do Teste

O objetivo do teste foi desenvolver uma API que atendesse aos seguintes critérios:

- Cadastro e login de usuários (com JWT).
- Endpoint para listar reservas filtradas por data.
- Endpoint para obter faturamento mensal de forma otimizada.
- Criação de modelo relacional no banco de dados SQL para entidades como tipos de suíte, motéis, clientes e reservas.
- Cache: Implementação desejável para otimização da consulta de reservas.
- Proteção contra SQL Injection e otimização de queries.

---

## Sobre o Desenvolvimento

Sou um desenvolvedor júnior e, embora a posição para a qual fui considerado seja para Desenvolvedor Fullstack Sênior, fiz o meu melhor para entregar a solução dentro dos padrões exigidos, incluindo aspectos como segurança, eficiência e organização do código. Caso minha candidatura para a vaga de Desenvolvedor Fullstack Sênior não seja a mais indicada para este momento, eu ficaria extremamente grato por uma oportunidade de entrevista para uma vaga de Estágio ou Desenvolvedor Júnior, onde acredito que posso contribuir com minha dedicação, vontade de aprender e habilidades crescentes.

Durante o desenvolvimento, fui capaz de:

- Entender e aplicar JWT: Implementei a autenticação JWT para o cadastro e login de usuários, garantindo a segurança na troca de informações entre cliente e servidor.
- Modelagem de Banco de Dados: Criei um modelo relacional utilizando SQL para as entidades essenciais do sistema, como tipos de suíte, motéis, clientes e reservas.
- Performance e Segurança: Otimizei queries para garantir a performance da API e implementei medidas de segurança para proteção contra SQL Injection.
- Uso de Cache: Implementei um mecanismo de cache simples para otimizar a consulta de reservas.

---

## Tecnologias Utilizadas

- Backend: .NET Core
- Banco de Dados: PostgreSQL (Modelo Relacional)
- Autenticação: JWT
- Cache: (Descrever a estratégia de cache implementada, se aplicável)
- Ferramentas de Desenvolvimento: Visual Studio, Git

## Como Rodar o Projeto

1. Clone este repositório:

```
  git clone https://github.com/euvitorti/TesteDevFullstackSenior.git
```

2. Navegue até a pasta do projeto:

```
  cd TesteDevFullstackSenior/GuiaMotel
```

3. Restaure as dependências:

```
  dotnet restore
```

4. Banco de Dados:

O banco de dados PostgreSQL foi configurado no servidor Render. Utilizei migrations para criar a estrutura relacional e inicializar o banco de dados. Portanto, não é necessário se preocupar com a conexão, pois a string de conexão já está configurada nas configurações do projeto. Basta rodar a aplicação e testar a API.

6. Inicie a aplicação:

```
  dotnet run
```

7. Teste a API
   
- Cadastro de Usuário: Envie uma requisição POST para api/auth/register com o corpo contendo o nome, email e senha.
- Login de Usuário: Envie uma requisição POST para api/auth/login com as credenciais. Receba o token JWT para usar nos endpoints protegidos.
- Listagem de Reservas: Use o endpoint api/reservas e passe parâmetros de filtro para as datas, se necessário.
- Faturamento Mensal: Acesse o endpoint api/faturamento/mes para obter o faturamento

A API estará disponível em http://localhost:5000.
