for /f %%a in ('dir %windir%\Microsoft.Net\Framework64\regasm.exe /s /b') do set currentRegasm="%%a"
%currentRegasm% %CD%\DosBoxMenu.dll /unregister
pause
