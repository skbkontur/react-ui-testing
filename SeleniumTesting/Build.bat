SET OutputDir=%~dp0Output
SET Configuration=Release
SET ILMergeExe=%~dp0Assemblies\ILMerge\ILMerge.exe
SET MSBuildExe=%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
SET NpmExe=npm

pushd %~dp0

rmdir %OutputDir% /s /q
mkdir %OutputDir%

call %NpmExe% install --only-dev
call %NpmExe% run build

%MSBuildExe% SeleniumTesting.sln /t:Rebuild /p:Configuration=%Configuration%

pushd SeleniumTesting\bin\%Configuration%

%ILMergeExe% /xmldocs /v4 /internalize /out:%OutputDir%\SKBKontur.SeleniumTesting.dll^
 SKBKontur.SeleniumTesting.dll^
 Humanizer.dll^
 Humanizer.resources.dll^
 ru\Humanizer.resources.dll^
 MoreLinq.dll^
 Newtonsoft.Json.dll

popd

popd
