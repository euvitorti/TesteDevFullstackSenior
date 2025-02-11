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

---

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

---

## Teste a API

1. **Cadastro de Usuário**

Primeiramente, é necessário cadastrar um usuário. Esse endpoint cria o usuário e retorna uma mensagem de sucesso.

```
  POST http://localhost:5000/api/user/register
```

2. **Requisição com cURL**
 
Utilize o comando curl abaixo (em uma única linha) para registrar um novo usuário. Certifique-se de enviar os campos UserName, Password e Email corretamente:

```
  curl -X POST http://localhost:5000/api/user/register -H "Content-Type: application/json" -d "{\"UserName\": \"adm\", \"Password\": \"adm\", \"Email\": \"adm@dominio.com\"}"    
```

3. **Resposta Esperada**

Em caso de sucesso, a resposta deverá ser similar a:

```
  {
    "message": "Usuário registrado com sucesso!"
  }
```

4. **Realizar Login e Obter o Token**

Para acessar os endpoints protegidos da API, é necessário realizar o login e obter um token de acesso. O login deve ser feito enviando os dados do usuário (UserName e Password) no corpo da requisição.

```
  POST http://localhost:5000/api/authentication/login
```

5. **Exemplo de Requisição (cURL)**

Utilize o comando abaixo para efetuar o login. Certifique-se de substituir os valores seu_usuario e sua_senha pelos dados corretos do usuário cadastrado:

```
  curl -X POST http://localhost:5000/api/authentication/login -H "Content-Type: application/json" -d "{\"UserName\": \"seu_usuario\", \"Password\": \"sua_senha\"}"
```

**Possíveis Respostas**: Login realizado com sucesso, retornando o token de acesso:

```
{"token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3RlIiwiZW1haWwiOiJ0ZXN0ZUBnbWFpbC5jb20iLCJuYmYiOjE3MzkyNzAyNjMsImV4cCI6MTczOTI3Mzg2MywiaWF0IjoxNzM5MjcwMjYzfQ.iKS7u0VlNl85YOW9c7hTCm1im6MxGH2LWgI_DoC16xk"}
```

- Listagem de Reservas: Use o endpoint api/reservas e passe parâmetros de filtro para as datas, se necessário.
- Faturamento Mensal: Acesse o endpoint api/faturamento/mes para obter o faturamento

A API estará disponível em http://localhost:5000.
