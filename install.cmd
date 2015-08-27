set service_name=%1
if "%service_name%"=="" (set service_name=AspNetWindowsService)

call dnu publish src\AspNetWindowsService --out publish-output --runtime active --no-source
sc create %service_name% binPath= "\"%~dp0publish-output\run.cmd\" --windows-service"
sc start %service_name%
