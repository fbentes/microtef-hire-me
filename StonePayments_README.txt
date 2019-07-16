**********  Seguir os passos inicias de configuração para executar os testes e os projetos Client e Server ***********

Baixar o arquivo "SQLServerDataBase/Backup/StonePaymentsDataBase" e restaurá-lo numa instância do SQL Server.


No arquivo "Utils\CryptographyTest.cs" do projeto StonePayments.Tests:

Caso seja alterado o parâmetro cryptography.GenerateAPassKey diferente de "Teste para a Stone" 
como já está codificado, executar o CryptographyTest.cs para gerar a nova KeyString criptografada 
via debug (linha 18) e setá-la para StonePayments.Util.KeyStringCryptography.VALUE (no projeto StonePayments.Util);

Na linha 46, gerar e copiar via debug a StringConnection criptografada para o seu SQL Server e colá-la no campo
"ConnectionString" do arquivo "DataBaseConnection.json" (que está no mesmo diretório do arquivo StonePayments.sln).


No projeto StonePayments.Server:

Fazer o Publish do projeto numa pasta qualquer e publicá-la no IIS com Binding 192.168.0.11:9090 (se o IP:PORTA forem 
diferentes de 192.168.0.11:9090, favor alterar o valor da chave "Server" no arquivo de recurso 
StonePayments.Client.StonePaymentServerResource no projeto StonePayments.Client para o novo IP:PORTA).

Para executar os endpoints no IP:PORTA = http://192.168.0.11:9090 (ex.: http://192.168.0.11:9090/stone/transactions) 
pelo Postman ou pelo projeto StonePayments.Client em execução, copiar "DataBaseConnection.json" para 
"C:\Windows\System32\inetsrv".

Para executar os endpoints no IP:PORTA = https://localhost:porta (ex.: https://localhost:44328/stone/transactions) 
pelo Postman quando estiver rodando o projeto StonePayments.Server, copiar "DataBaseConnection.json" para 
"C:\Program Files (x86)\IIS Express".




*******************   Sobre a Solution   *****************

Foram utilizados as blibliotecas: LightInject (para injeção de dependências), o Newtonsoft.Json (para comunicações via json) 
e o EntityFramework para a camada de persistência. 

OBS.:
O CqrsLite iria ser aplicado como demonstração como solucionador de problemas de 
concorrência/disponibilidade no banco de dados (read/write) pela segregação do banco de leitura (SGBD desnormalizado) e 
escrita (DocumentDB armazenando json), mas devido a falta de tempo não foi possível implementá-lo. Pensando na utilização 
do CQRS, os IDs das entidades persistentes estão com o tipo no SQL Server uniqueidentifier (System.Guid no C#) e não 
bigint com autoincrement (long no C#).
