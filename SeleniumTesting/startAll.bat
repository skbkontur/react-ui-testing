cd /d %~dp0

pushd .\Assemblies\WebDriver
start "Selenium" cycle.bat
popd

call npm install

pushd TestPages
call npm install
call npm run install-versions
start npm start
popd
