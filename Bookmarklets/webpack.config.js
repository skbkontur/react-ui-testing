var path = require('path');
var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');

var NODE_ENV = process.env.NODE_ENV;
var PROD = NODE_ENV === 'production';
var DEV = NODE_ENV === 'development';

const bookmarkletsPageConfig = {
    entry: {
        ['index']: [
            'babel-polyfill',
            './src/bookmarklets-page/index.js',
        ]
    },
    output: 
        PROD 
            ? {
                publicPath: 'http://tech.skbkontur.ru/react-ui-testing/',
                path: path.join(__dirname, 'dist'),
                filename: '[name].js',                
            }
            : {
                publicPath: '/',
                path: path.join(__dirname, 'dist'),
                filename: '[name].js',                
            },
    module: {
        rules: [
            {
                test: /\.jsx?$/,
                include: path.join(__dirname, 'src/bookmarklets-page/'),
                loader: "babel-loader",
            },
            {
                test: /\.less$/,
                include: path.join(__dirname, 'src/bookmarklets-page/'),
                use: [
                    'classnames-loader',
                    'style-loader',
                    'css-loader?modules&localIdentName=[name]-[local]-[hash:base64:4]',
                    'less-loader',
                ],
            },            
        ],
    },
    plugins: [
        new webpack.DefinePlugin({
            'process.env.bookmarkletsRoot': PROD 
                ? JSON.stringify('http://tech.skbkontur.ru/react-ui-testing/bookmarklets')
                : JSON.stringify('http://localhost:8080/bookmarklets')
        }),
        new HtmlWebpackPlugin({
            template: './src/bookmarklets-page/index.html',
        }),
    ]
};

const highlightTidBookmarklet = {
    entry: {
        ['highlight-tid-bookmarklet']: [
            './src/highlight-tid-bookmarklet/index.js'
        ],
    },
    output: 
        PROD
            ? {
                publicPath: '/',
                path: path.join(__dirname, 'dist/bookmarklets/'),
                filename: '[name].js',                
            }
            : {
                publicPath: '/',
                path: path.join(__dirname, 'dist/bookmarklets/'),
                filename: '[name].js',                
            },
    module: {
        rules: [
            {
                test: /\.jsx?$/,
                include: path.join(__dirname, 'src'),
                loader: "babel-loader",
            },
            {
                test: /\.less$/,
                include: path.join(__dirname, 'src'),
                use: [
                    'classnames-loader',
                    'style-loader',
                    'css-loader?localIdentName=[name]-[local]-[hash:base64:4]',
                    'less-loader',
                ],
            },
        ],
    },
}

module.exports = [
    bookmarkletsPageConfig,
    highlightTidBookmarklet,
];