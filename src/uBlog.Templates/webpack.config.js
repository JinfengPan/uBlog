module.exports ={
  //字符串会被解析得到一个module
  //数组中的所有module都会被加载，数组中最后一个module会被导出(export)
  entry:["./utils", "./app.js"],
  output:{
    filename: "bundle.js",
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
      }
    ]
  },
  resolve:{
    extensions:['','.js','.es6']
  }
}
