const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');

const sourcePath = __dirname;
const buildPath = path.resolve(__dirname, '../wwwroot');

module.exports = {
     entry: [
       path.join(sourcePath, 'app.js')
     ],
     output: {
         path: buildPath,
         filename: 'bundle.js',
         publicPath: "/"
     },
     module: {
       loaders: [
           {test: /\.js$/, loader: 'babel-loader', query: { presets: ['es2015'] }},
           {test: /(\.css)$/, loaders: ['style', 'css']},
           {test: /\.eot(\?v=\d+\.\d+\.\d+)?$/, loader: 'file'},
           {test: /\.(woff|woff2)$/, loader: 'url?prefix=font/&limit=5000'},
           {test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/, loader: 'url?limit=10000&mimetype=application/octet-stream'},
           {test: /\.svg(\?v=\d+\.\d+\.\d+)?$/, loader: 'url?limit=10000&mimetype=image/svg+xml'}
       ]
     },
     stats: {
         colors: true
     },
     plugins: [
       new webpack.HotModuleReplacementPlugin(),
       new webpack.NoEmitOnErrorsPlugin(),
       new HtmlWebpackPlugin({
          template: path.join(sourcePath, 'index.html'),
          path: buildPath,
          filename: 'index.html'
        })
     ],
     devtool: 'source-map'
 };
