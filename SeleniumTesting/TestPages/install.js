var path = require('path');
var fs = require('fs');
var versions = require('./versions');
var reactVersions = Object.keys(versions);
var exec = require('child_process').exec;

for (var i = 0; i < reactVersions.length; i++) {
    reactVersion = reactVersions[i];
    retailUiVersions = versions[reactVersions[i]];

    for(var j = 0; j < retailUiVersions.length; j++) {
        var retailUiVersion = retailUiVersions[j];
        var targetDir = reactVersion + '_' + retailUiVersion;
        if (!fs.existsSync(targetDir)){
            fs.mkdirSync(targetDir);
        }
        fs.createReadStream('./test-page-template/index.js')
            .pipe(fs.createWriteStream('./' + targetDir + '/index.js'));
        fs.createReadStream('./test-page-template/package.json')
            .pipe(fs.createWriteStream('./' + targetDir + '/package.json'));
        fs.appendFileSync('./' + targetDir + '/.gitignore', '*');

        var libs = [
            "react@" + reactVersion,
            "react-addons-css-transition-group@" + (reactVersion === "16.0.0" ? "15.6.2" : reactVersion),
            "react-addons-test-utils@" + (reactVersion === "16.0.0" ? "15.6.2" : reactVersion),
            "react-dom@" + reactVersion,
            "retail-ui@" + retailUiVersion
        ];
        
        var child = exec('npm install ' + libs.join(' '), { cwd: path.join(__dirname, targetDir) },
        function (error, stdout, stderr) {
            console.log('stdout: ' + stdout);
            console.log('stderr: ' + stderr);
            if (error !== null) {
                console.log('exec error: ' + error);
            }
        });        
    }
}


