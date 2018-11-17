# core
ASP.NET Core 2.1 test application


### ASP.NET Core Development in Linux:
- https://www.blinkingcaret.com/2018/03/20/net-core-linux/

### MySql:
- https://dev.mysql.com/doc/connectors/en/connector-net-entityframework-core-example.html


### Install Dotnet Runtime
- Download dotnet-runtime-2.1.6-linux-arm.tar.gz from https://www.microsoft.com/net/download
- Copy to Android device and extract it for example in ~/bin
- Add dotnet command to path to execute from everywhere: `nano ~/bashrc`, add  in the end `export PATH=~/bin/aspnetcore-runtime-2.1.6-linux-arm:$PATH`

### Publish
- ~/git/jeroen256/core$ dotnet publish -c Release
- scp -r ~/git/jeroen256/core/Server/bin/Release/netcoreapp2.1/publish android@192.168.1.3:~/core
- ssh android@192.168.1.3
- android@localhost:~/core$ `mv publish v1`
- android@localhost:~/core$ `dotnet v1/Server.dll`
- Goto: `http://192.168.1.3:5000/api/insert/1`

### Secure Hosting
- https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-2.1