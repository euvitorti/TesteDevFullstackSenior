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
- Cache
- Ferramentas de Desenvolvimento: Visual Studio Code, Git

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

1. **Cadastro, Login e Uso do Token**

Esta seção explica como registrar um usuário e fazer login para obter um token JWT que será usado para autenticar as próximas requisições. Para verificar os parâmetros e as rotas disponíveis, acesse a documentação completa via Swagger na URL:

```
  http://localhost:5000/swagger/index.html
```

2. Cadastre um Novo Usuário:

- No Swagger, localize a seção Users.
- Clique em POST /api/user/register para expandir a rota de cadastro.
- Clique no botão Try it out (canto superior direito da caixa da requisição).
- No campo de exemplo que aparecer, preencha com os dados do usuário. Exemplo

```
  {
    "userName": "string",
    "password": "string",
    "email": "user@example.com"
  }
```

Depois de preencher, clique em Execute para enviar a requisição. Verifique a resposta para confirmar que o cadastro foi realizado com sucesso.

3. **Login**

- No Swagger, localize a seção Authentication.
- Encontre a rota POST /api/authentication/login.
- Clique em Try it out.
- Preencha os campos com o nome de usuário e senha cadastrados:

```
  {
    "userName": "string",
    "password": "string"
  }
```

Clique em Execute e copie o token JWT que aparecerá na resposta. Ele será algo parecido com:

```
  eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

4. **Adicione o Token para Acessar Rotas Protegidas**

- No topo direito da página do Swagger, clique em Authorize.
- Na janela que abrir, cole o token no seguinte formato:

```
  Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Clique em Authorize e depois em Close.

5. **Testando Outras Rotas Protegidas**

- Agora você pode acessar rotas que exigem autenticação.
- Escolha uma rota, clique em ➤ para expandir.
- Clique em Try it out, preencha os parâmetros necessários e clique em Execute.
- O Swagger enviará a requisição usando o token que você adicionou.

6. **Dicas Extras**

- Parâmetros de Requisição: O Swagger exibe claramente quais parâmetros são obrigatórios para cada rota.
- Token Expirado: Se receber erro de autenticação, faça o login novamente para obter um novo token.
- Sem Instalação: Todo o teste pode ser feito diretamente no Swagger, sem necessidade de ferramentas externas.

A API estará disponível em http://localhost:5000.


## Exemplo Prático de Requisição com Token

Depois de obter o token, siga o passo a passo para fazer uma requisição protegida no Swagger.

Exemplo: Criar uma nova reserva.

1. **Cadastrar um Motel**.

- Primeiro, registre um motel usando a rota POST /api/motels.
- Após cadastrar, anote o motelId retornado.

2. **Cadastrar uma Suíte**

- Após registrar o motel, crie uma suíte utilizando a rota POST /api/suites.
- Anote o suiteTypeId retornado.

3. **Criar uma nova reserva**
   
- No swagger encontre a seção Reservations.
- Encontre a rota POST /api/reservations.
- Clique em ➤ para expandir a rota.
- Clique em Try it out.

Preencha o corpo da requisição com os dados da reserva, conforme o exemplo abaixo:

```
  {
    "startDate": "2025-02-11T12:16:29.804Z",
    "endDate": "2025-02-11T12:16:29.804Z",
    "userId": 1,
    "suiteTypeId": 1,
    "motelId": 1,
    "totalAmount": 100.50
  }
```

### Atenção!

- startDate e endDate devem estar no formato ISO 8601, ou seja, YYYY-MM-DDTHH:MM:SS.MSZS.
- userId, suiteTypeId e motelId devem ser os valores retornados nas rotas de cadastro do usuário, suíte e motel, respectivamente.
- totalAmount é o valor total da reserva.

Clique em Execute e a resposta mostrará os detalhes da reserva criada ou erros, se houver algum problema com os dados enviados.
