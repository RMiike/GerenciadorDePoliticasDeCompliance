# GerenciadorDePoliticasDeCompliance

A proposta apresentada poderia ser solucionada de forma rápida e dinâmica por meio do Entity FrameWork, que permite ao desenvolvedor a realização de um trabalho sem muita preocupação em relação a tabelas de bancos de dados, uma vez que esse conjunto de tecnologias permite ao programador um trabalho em alto nível de abstração.
Entretanto, por motivos de aprendizado e desafio, optei por buscar uma solução simples, utilizando queries para acessar o banco de dados e processamento.
Para solucionar o case, comecei interpretando a estrutura do modelo de entidade e relacionamento apresentado, para determinar a problemática proposta.

Assim, ficou acertado que o usuário com perfil de  Administrador poderá: 
-	Cadastrar uma política;
- Alterar uma política;
- Visualizar lista de politicas;
- Visualizar detalhes da política e Listar usuários que assinaram essa política.

Aquele com perfil de Usuário comum poderá:
- Visualizar uma lista de políticas e uma lista com políticas assinadas;
- Visualizar detalhes de uma política;
- Assinar a política;
img 1
Em seguida, prossegui criando um banco de dados com o acréscimo de uma tabela de perfil de Usuário, responsável pela realização de login do sistema, em razão das boas práticas de um padrão de projeto, uma vez que existem perfis de usuário, podendo, em uma eventualidade, haver a criação de novos perfis.
Um perfil de usuário se associa a uma determinada entidade do sistema, dessa forma, a criação de novos perfis, como clientes, fornecedores, se faz necessária para que exista uma determina autenticação, fazendo com que o Funcionário não acesse determinada áreas de responsabilidade de um Administrador.
 img 2
 
 
 Get post
http
query – procedure – 57
mvc


# Tecnologias

## Front End
* Html, Css, Bootstrap, MVC
## Back End

* C#, ASP.Net, MVC

## Banco de Dados

* Sql-Server

# Referências 
