call build.bat
if %errorlevel% neq 0 exit /b %errorlevel%

.nuget\nuget pack "MicroProfiler\MicroProfiler.csproj" -Properties Configuration=Release
if %errorlevel% neq 0 exit /b %errorlevel%