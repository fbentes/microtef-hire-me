## O diagrama de componentes StonePaymentsSolutionPackageDiagram.jpeg na pasta raíz contém a arquitetura da solução, e o arquivo StonePaymentsEntityRelationshipModel.jpg contém o DER.

# Seguir os passos inicias de configuração 

Baixar o arquivo "SQLServerDataBase/Backup/StonePaymentsDataBase" e restaurá-lo numa instância do SQL Server 2017/2018.
Além do SQL Server, foram usados o Visual Studio 2019 Community e o IIS 10 no Windows 10.

## No arquivo "Utils\CryptographyTest.cs" do projeto StonePayments.UnitTests:

* Caso seja alterado o parâmetro cryptography.GenerateAPassKey diferente de "Teste para a Stone" 
como já está codificado, executar o CryptographyTest.cs para gerar a nova KeyString criptografada 
via debug (linha 17) e setá-la para StonePayments.Util.KeyStringCryptography.VALUE (no projeto StonePayments.Util);

* Na linha 45, gerar e copiar via debug a StringConnection criptografada para o seu SQL Server e colá-la no campo
"ConnectionString" do arquivo "DataBaseConnection.json" (que está no mesmo diretório do arquivo StonePayments.sln).


## No projeto StonePayments.Server:

* Fazer o Publish do projeto numa pasta qualquer e publicá-la no IIS com Binding 192.168.0.11:9090 (se o IP:PORTA forem 
diferentes de 192.168.0.11:9090, favor alterar o valor da chave "Server" no arquivo de recurso 
StonePayments.Business.StonePaymentResource no projeto StonePayments.Business para o novo IP:PORTA).

* Para executar os endpoints no IP:PORTA = http://192.168.0.11:9090 (ex.: http://192.168.0.11:9090/stone/transactions) 
pelo Postman ou pelo projeto StonePayments.Client em execução, copiar "DataBaseConnection.json" para 
"C:\Windows\System32\inetsrv".

* Para executar os endpoints no IP:PORTA = https://localhost:porta (ex.: https://localhost:44328/stone/transactions) 
pelo Postman quando estiver rodando o projeto StonePayments.Server, copiar "DataBaseConnection.json" para 
"C:\Program Files (x86)\IIS Express".

# Sobre a Solution

* Foram utilizados as blibliotecas: LightInject (para injeção de dependências), o Newtonsoft.Json (para comunicações via json) 
e o EntityFramework para a camada de persistência. O Syncfusion.Shared.WPF foi utilizado para input de dados da transação.

* OBS.:
O CqrsLite iria ser aplicado como demonstração como solucionador de problemas de 
concorrência/disponibilidade no banco de dados (read/write) pela segregação do banco de leitura (SGBD desnormalizado) e 
escrita (DocumentDB armazenando json), mas devido a falta de tempo não foi possível implementá-lo. Pensando na utilização 
do CQRS, os IDs das entidades persistentes estão com o tipo no SQL Server uniqueidentifier (System.Guid no C#) e não 
bigint com autoincrement (long no C#).

O CRUD para a entidade Card não foi propositalmente feito para a dedicação de mais tempo para questões criativas. Para inserir dados nessa entidade, é preciso inserir diretamente na tabela no SQL Server, gerando seu id pelo site https://www.guidgenerator.com/online-guid-generator.aspx. A tabela Transaction tem seu frontend no WPF conforme solicitado, e a tabela Customer tem seu frontend Web num projeto separado em WebForms.

__________________________________________________________________________________________________________________________

# Hire me

Desafio para avaliar as suas habilidades como desenvolvedor.

## Instruções

Você deve:

1. Clonar esse repositório
2. Em seu _fork_, atenda os requisitos descritos abaixo:
  * Para **aprendizes de padawan**: tópicos que tiverem :hatching_chick:
  * Para **padawans**: tópicos que tiverem :hatching_chick: e :hatched_chick:
  * Para **jedis**: tópicos que tiverem :hatching_chick:, :hatched_chick: e :baby_chick: 
  * Para **mestres jedi**: tópicos que tiverem :hatching_chick:, :hatched_chick:, :baby_chick: e :chicken:
3. [Nos envie um e-mail](mailto:devmicrotef@stone.com.br) com seu nome e link do repositório

## O desafio

O projeto consiste na implementação de duas aplicações (cliente-servidor) que **simulem o processo de uma transação financeira**.

### :hatching_chick: O cliente (dê um nome legal pra ele)

Deve ser uma aplicação desktop **simples** utilizando WPF. O layout **não será avaliado**, mas a escolha de componentes e estrutura do XAML serão.

Deve haver duas telas principais:

* **Tela de transação**: input dos dados da transação e envio da transação para o servidor.
* **Tela de consulta das transações efetuadas**: lista das transações efetuadas.

**:warning: Apenas para :hatching_chick:: como você não precisa fazer o servidor, faça mock dos dados da transação.**

#### :chicken: Catálogo de cartões virtuais

Criar um catálogo de cartões virtuais (o número que você achar razoável para testar diferentes cenários), que estarão disponíveis quando o cliente for fazer uma compra. Esses cartões deverão ter propriedades a mais do que o cartão "básico" descrito abaixo.

* A senha de cada cartão do catálogo deve estar criptografada de algum jeito
* Com esse catálogo, a verificação da senha do cartão deve ser feita apenas pelo servidor

### :hatched_chick: O servidor (dê um nome legal pra ele)

O servidor irá simular justamente o que a Stone é, uma **adquirente**. 

    No mundo real, uma adquirente se comunica com o banco (emissor do cartão de crédito/débito) e com a bandeira (Visa, MasterCard, American Express, etc). O servidor não precisará se preocupar com isso, apenas irá simular uma transação financeira de acordo com alguns parâmetros.

Sinta-se livre para utilizar a tecnologia que quiser, desde que cumpra os requisitos abaixo. õ/

O servidor deve esperar por uma transação. Assim que o cliente enviar uma requisição (como demonstrado no esquema a seguir), o servidor deve usar **parâmetros de validação dos dados da transação** e **parâmetros randômicos** _(você pode usar outro parâmetro, mas documente em algum canto!)_ para retornar sucesso ou erro na transação. 

**Códigos de retorno**

Código | Explicação
--- | ---
Aprovado | Transação aprovada
Transação negada | Transação negada
Saldo insuficiente | Portador do cartão não possui saldo
Valor inválido | Mínimo de 10 centavos
Cartão bloqueado | Quando o cartão está bloqueado (_dãa!_)
Erro no tamanho da senha | Senha deve ter entre 4 e 6 dítigos
:baby_chick: Senha inválida | A senha enviada é inválida

:baby_chick: Você pode (_e deve!_) adicionar novos códigos de retorno.

#### :baby_chick: Cadastro dos clientes

Deverá ser capaz de cadastrar clientes que possam passar transações no seu servidor. O limite de crédito de cada cliente deve ser considerado.

### Comunicação entre cliente e servidor

A comunicação pode acontecer em **JSON ou XML**.

#### Autorização

![Relação cliente-servidor](image/client-server.png)

#### :baby_chick: Sondagem das transações

![Relação cliente-servidor com sonda](image/client-server-advanced.png)

## O modelo de dados

:baby_chick: O modelo descrito aqui pode (_e deve!_) ser incrementado.

### Card

Propriedade | Descrição
--- | ---
CardholderName | Nome do portador do cartão
Number | Os números que são impressos no cartão, podendo variar entre 12 à 19 dígitos
ExpirationDate | Data de expiração do cartão
CardBrand | Bandeira do cartão
Password | Senha do cartão
Type | Chip ou tarja magnética 
HasPassword | Se o cartão possui senha. Apenas cartões de tarja magnética podem ter essa propriedade como `True`

### Transaction

Propriedade | Descrição
--- | ---
Amount | Valor da transação
Type | Tipo da transação
Card | Propriedades do cartão
Number | Número de parcelas, **se for uma transação de crédito parcelado**
 
### O que será avaliado?

1. Desenho e arquitetura da solução
2. Padrões do projeto utilizados
3. Organização, documentação e legibilidade do código
4. :baby_chick: Mapeamento ORM
5. Uso de biblioteca de terceiros
6. Criatividade
7. :baby_chick: Testes de unidade e _mocking_

### Inspiração

Esse desafio foi inspirado no layout maravilhoso do projeto [hire.me feito pela Bemobi](https://github.com/bemobi/hire.me).
