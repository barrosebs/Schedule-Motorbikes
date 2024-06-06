# Respondendo Desafio
Passos para rodar o projeto
gerar imagem no docker:
	- Postgress (User Id=postgres;Password=SMtest;Database=DBScheduleMotorbikes)
	- RabbitMQ (login e senha: admin e 123Aa@) Pasta Rabbit existe um docker compose
	
Após gerar as imagens,será necessário rodar o commando de migration com visual studio, acessando Tools>> Nuget Packege Manager >>  Packege Manager Console.
No console digite os seguintes comandos:
- Add-migration initial
- update-database
Com isso vai gerar as tabelas
Após, pode rodar o sistema e ele automatica vai gerar algumas configurações básicas, como Usuario Admin e suas permissões

Acesse a URL 
- https://localhost:7257/account/Login
- User admin
- Senha 123Aa@

Para criar um usuário entregador é preciso criá-lo pelo link
https://localhost:7257/Account/create
- São duas etapas 
 - 1 preencher os campos 
 - 2 cadastro de senha	

*****************************************



