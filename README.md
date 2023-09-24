# Resumo
Esse projeto inicialmente tem como objetivo comparar e testa as opções de pessistência e gerar metricas dos frameworks de banco de dados e em qual tem melhor performace em cada cenário. 

## Requesitos
- [ ] DOTNET CORE 7.0
- [ ] SQL SERVER

## Configuração
o arquivo config.json tem os parametros basico para entrada. você vai precisar informa a connection string do banco SQL Server e a quantidade de registro que deseja trabalhar.

### Banco de dados.
O script para criar a tabela do banco está na pasta /SqlTest/script/table.sql só escutar o script no banco Sql Server
Lembre-se a connectionString do banco deve ser informado no arquivo config.json


### Massa de dados
O proprio algoritimo gerar massa de dados aleatorio para pessistencia. porem a quantidade da massa é você que descide informando no arquivo config.json



## Cenario de Teste

Aqui temos uma breve metrica de alguns cenarios de teste rodando no mesmo ambiente, com uma massa de dados de 10 mil.


| Cenario | ADO NET |  Dapper | Entity | 
| --- | --- | --- | --- |
| Insert Individual | 2min 53 sec | 2min 59 sec | N/A
| Insert Seguro | 3 min 2 sec | 2 min 56 sec | 5 min 12 sec
| insert Multiplo | 14 sec | 14 sec | N/A
| Insert Multiplo reflection | 14 sec | 14 sec | N/A
| Insert Multiplo seguro | N/A | 2 min 59 sec | N/A
| Bulk Insert | 1 sec | N/A |  6 sec


Analise.

Dapper. embora o framework parece ter suporte a lista com segurança, mas teve performace semalhante a individual seguro.

Entity não tem suporte a batch ou multiplo o seu conceito é adicione a entidade e quando tiver pronto para enviar ao banco vc aciona o savechange e sua performace em multiplos dados foram surpreendente perdendo apenas para bulk insert. porem o entity não tem como abster-se da segurança para aumentar performace como dapper e adonet.

ADO.NET
Embora o dapper não tenha suporte nativo a bulk insert. o ado.net é a unica biblioteca das testada com esse suporte e a velocidade de escrita foi a melhor de todos.


