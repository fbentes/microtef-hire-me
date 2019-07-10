Seguir os passos:


No projeto StonePayments.Tests (arquivo CryptographyTest.cs):

Caso seja alterado o parâmetro cryptography.GenerateAPassKey diferente de "Teste para a Stone", executar o CryptographyTest.cs para gerar o novo KeyString para gerar a StringConnection criptografada;

Copiar essa KeyString criptografada e setar para Library.Util.KeyStringConnection.VALUE (projeto Library.Util);

Copiar a StringConnection criptografada e colar no campo "ConnectionString" do arquivo DataBaseConnection.json (no projeto StonePayments.Tests).



No projeto StonePaymentsServer:

Fazer a publicação numa pasta (Ex.: "..\bin\Release\Publish") e publicá-la no IIS com Binding 192.168.0.11:9090 do StonePaymentsServer e inicar no IIS.

Copiar o arquivo DataBaseConnection.json (do projeto StonePayments.Tests) para "C:\Program Files (x86)\IIS Express" para quando for executar os endpoints pelo Postman ou outro concorrente,
pois ele contem a StringConnection criptografada para o EF !

