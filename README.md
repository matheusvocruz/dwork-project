## Instalação
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
````

## Usuários
```
1 - Usuário normal
2 - Usuário gerente
````

Existe um campo em todo request chamado x-user-id, ele está como obrigatório caso for usar no swagger

## Refinamento
- Entender a necessidade do cliente.
- Entidades estão muito anêmicas, gostaria de entender melhor o propósito do sistema, para entender se preciso de um banco relacional.
- Saber o prazo e também se o cliente está disposto a pagar por features/serviços de terceiros para serem utilizados, como o NewRelic ou DataDog para visualizar os Logs, tirando do banco.
- Entender qual seria essa autenticação externa e porque não implementada no início, pega segurança.

## Final
- Utilizar autenticação como JWT para melhor segurança e remover o user-id do header
- Separar o serviço entre 3 microsserviços
- Utilizar CRQS, com bancos e conexões separadas, para uma melhor escalabilidade.
- Poder utilizar um serviço, como AWS, para CI/CD, junto com uma EC2 ou Amazon Elastic Kubernetes, para a melhor latência do cliente.
- Utilizar serviços com Lambda para separar em funções menos utilizadas, como relatórios, e deixar ela 100% rodando por ela, com sua necessidade
- Utilização do Dapper para os relatórios.