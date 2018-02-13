set hostIp=127.0.0.1
if exist %~dp0kv-Import/kv-import-run.cmd (
start "uploadConfig" %~dp0kv-Import/kv-import-run.cmd  %hostIp%
)
consul agent -server -data-dir=C:\consul\consulServerData -bind=%hostIp% -enable-script-checks=true -bootstrap -ui -client=%hostIp%