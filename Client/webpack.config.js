const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');

const sourcePath = __dirname;
const buildPath = path.resolve(__dirname, '../wwwroot');

module.exports = {
     entry: [
       path.join(sourcePath, 'index.js')
     ],
     output: {
         path: buildPath,
         filename: 'bundle.js',
         publicPath: "/"
     },
     module: {
       rules: [
        {
            test: /\.css$/,
            use: [ 'style-loader', 'css-loader' ]
        },
        {
            test: /\.png$/,
            use: [ 'file-loader' ]
        },
        {
            test: /\.(js|jsx)$/,
            exclude: /node_modules/,
            use: {
                loader: 'babel-loader'
            }
        },
        {
            test: /\.(ttf|otf|eot|svg|woff(2)?)(\?[a-z0-9]+)?$/,
            use: ['file-loader?name=fonts/[name].[ext]' ]
        }
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
