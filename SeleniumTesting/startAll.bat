
pushd .\Assemblies\WebDriver
start "Selenium" cycle.bat
popd

pushd TestPages
call npm install
call npm run install-versions
start npm start
popd
