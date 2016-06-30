var WebpackStrip = require('strip-loader');
var devConfig = require('./webpack.config.js');

const merge = require('webpack-merge');


const buildConfig = {

  module:{
    loaders:[
      {
        test:[/\.js$/, /\.es6$/],
        exclude:/node_modules/,
        loader: WebpackStrip.loader('console.log', 'perfLog') //剥离两个函数
      }
    ]
  }

};



module.exports = merge(buildConfig, devConfig);
