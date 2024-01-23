const path = require("path");

module.exports = {
    devtool: 'source-map',
    module: {
        rules: [{
            test: /\.(js|jsx)$/,
            exclude: /node_modules/,
            use: {
                loader: "babel-loader"
            }
        }]
    },
    resolve: {
        extensions: ["*", ".js", ".jsx"]
    },
    experiments: {
        outputModule: true,
    },
    output: {
        path: path.resolve(__dirname, '../wwwroot/js'),
        filename: "bundle.js",
        clean: true,
        library: {
            type: "module",
        },
    },
};