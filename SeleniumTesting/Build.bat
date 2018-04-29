SET OutputDir=%~dp0Output
SET Configuration=Release
SET ILRepackExe=%~dp0packages\ILRepack.2.0.15\tools\ILRepack.exe
SET MSBuildExe="%PROGRAMFILES(X86)%\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MSBuild.exe"
SET NpmExe=npm

pushd %~dp0

rmdir %OutputDir% /s /q
mkdir %OutputDir%

call %NpmExe% install --only-dev
call %NpmExe% run build

%MSBuildExe% SeleniumTesting.sln /t:Rebuild /p:Configuration=%Configuration%

pushd SeleniumTesting\bin\%Configuration%

%ILRepackExe% /xmldocs /v4 /internalize /out:%OutputDir%\SKBKontur.SeleniumTesting.dll^
 SKBKontur.SeleniumTesting.dll^
 Kontur.RetryableAssertions.dll

popd

popd

