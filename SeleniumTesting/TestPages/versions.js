var shell = require("shelljs");
var semver = require("semver");

var versionsOutput = shell.exec("npm show retail-ui versions", { silent: true });
var retailUIVersions = eval(versionsOutput.stdout);

const resultVersions = {
    '16.0.0': '0.9.9 || >=0.10.8',
    '15.3.0': '0.6.18 || 0.8.15 || 0.9.9 || >=0.10.8',
    '15.4.2': '0.6.18 || 0.8.15 || 0.9.9 || >=0.10.8',
    '0.14.3': '0.6.18 || 0.8.15 || 0.9.9 || >=0.10.8',
};

var result = {};

if (process.env["TEAMCITY_VERSION"]) {
    var reactVersions = Object.keys(resultVersions);
    for(var i = 0; i < reactVersions.length; i++) {
        var reactVersion = reactVersions[i];
        result[reactVersion] = retailUIVersions.filter(x => semver.satisfies(x, resultVersions[reactVersion]))
    }
} else {
    result = {
        '16.0.0': ['0.9.7'],
        "15.4.2": ["0.9.0"],
    };
}

module.exports = result;

