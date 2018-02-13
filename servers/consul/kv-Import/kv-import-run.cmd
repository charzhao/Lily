ping /n 30 127.1>nul
for /r %~dp0 %%i in (*.json) do consul  kv import -http-addr=http://%1:8500 @%%i
