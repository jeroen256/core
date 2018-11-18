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
- android@localhost:~/core$ `cp -r publish v5` (make a copy in case we need to go back to this version later, the service runs from publish folder)
- android@localhost:~/core$ `dotnet publish/Server.dll`
- Goto: `http://192.168.1.3:5000/api/insert/1`

### Secure Hosting
- https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-2.1
- Problem: we use Android app "Linux Deploy (Debian 9)" for hosting. But services are not supported in chroot.

Other option:
- `dotnet core/publish/Server.dll &> "core/publish/logs/log_$(date +%F_%R).log" &` run in background, redirect stderr and stdout into log file (does not work very well, stdout tends to be printed all over terminal instead)

Other option, use screen: (screen always seems to be dead, doesn't work)
- help: https://linuxize.com/post/how-to-use-linux-screen/
- `screen -dR jeroen /home/android/bin/aspnetcore-runtime-2.1.6-linux-arm/dotnet /home/android/core/publish/Server.dll` 
- Reattach to the screen session by typing `screen -r`, Use the key sequence `Ctrl-a + Ctrl-d` to detach from the screen session.

Current option: Startup script: `/etc/rc.local/core.sh`, runas user android, works but no logging
```sh
#!/bin/sh
echo "starting jeroen.sh $(date +%F_%R)"
echo "whoami: $(whoami)"
echo "pwd: $(pwd)"
sudo service nginx start 
#/home/android/bin/aspnetcore-runtime-2.1.6-linux-arm/dotnet /home/android/core/publish/Server.dll &> "/home/android/core/publish/logs/log_$(date +%F_%R).log" &
/home/android/bin/aspnetcore-runtime-2.1.6-linux-arm/dotnet /home/android/core/publish/Server.dll
#screen -dR jeroen /home/android/bin/aspnetcore-runtime-2.1.6-linux-arm/dotnet /home/android/core/publish/Server.dll # screen will be dead somehow
echo "finished jeroen.sh $(date +%F_%R)"
```
