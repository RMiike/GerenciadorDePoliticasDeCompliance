# GerenciadorDePoliticasDeCompliance

A proposta apresentada poderia ser solucionada de forma rápida e dinâmica por meio do Entity FrameWork, que permite ao desenvolvedor a realização de um trabalho sem muita preocupação em relação a tabelas de bancos de dados, uma vez que esse conjunto de tecnologias permite ao programador um trabalho em alto nível de abstração.

Entretanto, por motivos de aprendizado e desafio, optei por buscar uma solução simples, utilizando queries para acessar o banco de dados e processamento.

Para solucionar o case, comecei interpretando a estrutura do modelo de entidade e relacionamento apresentado, para determinar a problemática proposta.

Assim, ficou acertado que o usuário com perfil de  Administrador poderá: 
- Cadastrar uma política;
- Alterar uma política;
- Visualizar lista de politicas;
- Visualizar detalhes da política e Listar usuários que assinaram essa política.

Aquele com perfil de Usuário comum poderá:
- Visualizar uma lista de políticas e uma lista com políticas assinadas;
- Visualizar detalhes de uma política;
- Assinar a política;

<img src="https://raw.githubusercontent.com/RMiike/GerenciadorDePoliticasDeCompliance/master/assets/img.001.JPG">

Em seguida, prossegui criando um banco de dados com o acréscimo de uma tabela de perfil de Usuário, responsável pela realização de login do sistema, em razão das boas práticas de um padrão de projeto, uma vez que existem perfis de usuário, podendo, em uma eventualidade, haver a criação de novos perfis.
Um perfil de usuário se associa a uma determinada entidade do sistema, dessa forma, a criação de novos perfis, como clientes, fornecedores, se faz necessária para que exista uma determina autenticação, fazendo com que o Funcionário não acesse determinada áreas de responsabilidade de um Administrador.

<img src="https://raw.githubusercontent.com/RMiike/GerenciadorDePoliticasDeCompliance/master/assets/img.002.JPG">

Para realizar a aplicação, irei utilizar o MVC para modular a minha relação de cliente servidor, de forma que será possível um funcionário realizar a requisição pela sua máquina de uma lista de políticas.
Esse request http irá passar pelo servidor web para servidores Internet Information Services (ISS),  que já se encontra a aplicação, os negócios e o banco de dados.

Dessa forma o request será encaminhado para a controler, responsável por controlar o comando de pesquisa das políticas. Após isso, a controler irá encaminhar para o model, que irá fazer a requisição no banco.
Os dados pesquisados irão voltar para a controler que irão ser encaminhados para a view, que irá mandar de volta para o servidor ISS e encaminhar para a tela do usuário a lista das políticas. 

<img src="https://raw.githubusercontent.com/RMiike/GerenciadorDePoliticasDeCompliance/master/assets/img.003.JPG">

Para realizar as pesquisas no banco de dados, eu optei mais uma vez realizar de uma forma que incitasse o meu aprendizado e o meu esforço, escolhendo as pesquisas sem procedure, de forma que eu mesmo irei digitar as queries para treinar e entender melhor o sistema. 

Dessa forma, criei uma class "Conexao" com diversos métodos  responsáveis por realizar todos os procedimentos de conexão, pesquisa, desconectar, e execução de queries.

Por fim, para organizar o front-end, utilizarei tags de html combinadas com bootstrap nas views.

# Tecnologias

## Front End
* Html, Css, Bootstrap, MVC
## Back End

* C#, ASP.Net, MVC

## Banco de Dados

* Sql-Server
