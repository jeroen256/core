# core
ASP.NET Core 2.1 test application


### ASP.NET Core Development in Linux:
- https://www.blinkingcaret.com/2018/03/20/net-core-linux/

### MySql:
- https://dev.mysql.com/doc/connectors/en/connector-net-entityframework-core-example.html


### Publish
- ~/git/jeroen256/core$ dotnet publish -c Release -r linux-arm
- scp -r ~/git/jeroen256/core/Server/bin/Release/netcoreapp2.1/linux-arm/publish android@192.168.1.3:/var/www/core
- ssh android@192.168.1.3
- android@localhost:/var/www/core$ mv publish v1; ./v2/Server 
- http://192.168.1.3:5000/api/insert/1