set hostIp=127.0.0.1
consul agent -server -data-dir=C:\consul\consulServerData -bind=%hostIp% -enable-script-checks=true -bootstrap -ui -client=%hostIp%