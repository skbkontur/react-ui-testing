var path = require('path');
var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var versions = require('./versions');

function createConfig(reactVersion, retailUIVersion, pairs) {
    var targetDir = reactVersion + '_' + retailUIVersion;
    return {
        entry: {
          ["index_" + reactVersion + '_' + retailUIVersion]: [
              "babel-polyfill",
              '../react-selenium-testing.js',
              "./" + targetDir + "/index.js"
            ]
        },
        output: {
          path: './dist',
          publicPath: '/',
          filename: '[name].js',
        },
        module: {
          loaders: [
            {
              test: /\.(css)$/,
              loader: 'style!css',
            },
            {
              test: /\.(less)$/,
              loader: 'style!css!less',
            },
            {
              test: /\.(woff|eot|png|gif|ttf|woff2)$/, 
              loader: "file-loader"
            },
            {
              test: /\.jsx?$/,
              exclude: /node_modules/,
              loader: 'babel',
              query: {
                  presets: [
                      require.resolve('babel-preset-react'),
                      require.resolve('babel-preset-es2015'),
                      require.resolve('babel-preset-stage-0')
                  ],
              }
            },
            {
              test: /\.jsx?$/,
              include: /(retail\-ui)/,
              loader: 'babel',
              exclude: /(react\-input\-mask)/,
              query: {
                  presets: [
                      require.resolve('babel-preset-react'),
                      require.resolve('babel-preset-es2015'),
                      require.resolve('babel-preset-stage-0')
                  ]
              }
            }
          ]
        },
        resolve: {
            extensions: [
                "",
                ".js",
                ".jsx",
            ],
            alias: { 
              'react': path.resolve(targetDir + '/' + 'node_modules/' + 'react'),
              'react-addons-css-transition-group': path.resolve(targetDir + '/' + 'node_modules/' + 'react-addons-css-transition-group'),
              'react-addons-test-utils': path.resolve(targetDir + '/' + 'node_modules/' + 'react-addons-test-utils'),
              'react-dom': path.resolve(targetDir + '/' + 'node_modules/' + 'react-dom'),
              'retail-ui': path.resolve(targetDir + '/' + 'node_modules/' + 'retail-ui'),
            }
        },
        plugins: [
            new webpack.DefinePlugin({
                'process.env.enableReactTesting': JSON.stringify(true),
                'process.env.baseUrl': JSON.stringify('/' + reactVersion + '/' + retailUIVersion),
            }),
            new HtmlWebpackPlugin({
              filename: reactVersion + '/' + retailUIVersion + '/index.html',
              template: './src/index.html',
            })
        ],
        devServer: {
          "port": 8083,
          historyApiFallback: {
            rewrites: pairs.map(x => 
              ({
                from: new RegExp('^\/' + x[0] + '\/' + x[1] + '\/.*'), 
                to: '/' + x[0] + '/' + x[1] + '/index.html',
              })),
          }
        },
    };
}

function bypassTo(pathToHtml) {
    return {
        secure: false,
        //target: '//' + pathToHtml
         bypass: function(req) {
             return '/dist/' + pathToHtml;
         }
    }
}

const z = Object.keys(versions)
    .map(reactVersion => versions[reactVersion].map(retailUIVersion => [reactVersion, retailUIVersion]))
    .reduce((x, y) => x.concat(y), []);

module.exports = Object.keys(versions)
    .map(reactVersion => versions[reactVersion].map(retailUIVersion => [reactVersion, retailUIVersion]))
    .reduce((x, y) => x.concat(y), [])
    .map(x => createConfig(x[0], x[1], z));
