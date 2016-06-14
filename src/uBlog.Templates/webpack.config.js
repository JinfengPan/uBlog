var path = require('path');
var webpack = require('webpack');

var commonsPlugin = new webpack.optimize.CommonsChunkPlugin('shared.js');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports ={

  //基础路径(绝对路径)，用来解析entry项
  context: path.resolve('js'),


  //字符串会被解析得到一个module
  //数组中的所有module都会被加载，数组中最后一个module会被导出(export)
  //如果你传一个对象，那么将会生成几个bundle

  entry:{
    about:'./about_page.js',
    home:'./home_page.js',
    contact:'./contact_page.js'
  },
  output:{

    //bundle.js存储的真实路径
    path: path.resolve('public/js/'),
    //使用devserver时，浏览器访问bundle.js的虚拟路径
    publicPath:'/public/assets/js/',
    filename: "[name].js"


  },
  plugins:[commonsPlugin, new ExtractTextPlugin("style.css")],
  devServer:{
    contentBase: 'public'
  },
  //watch:true

  module:{
    preloaders:[
      {
        test:/\.js$/,
        exclude:'node_modules',
        loader:'jshint-loader'
      }
    ],
    loaders:[
      {
        test:/\.es6$/,
        exclude:/node_modules/,
        loader:"babel-loader"
      },
      {
        test:/\.css$/,
        loader: ExtractTextPlugin.extract("style-loader","css-loader")
      },
      {
        test:/\.scss$/,
        exclude:/node_modules/,
        loader: ExtractTextPlugin.extract("style-loader","css-loader!sass-loader")
      }
    ]
  },
  resolve:{
    extensions:['','.js','.es6']
  }
};
