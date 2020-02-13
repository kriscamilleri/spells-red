// // Inside vue.config.js
// const PurgecssPlugin = require("purgecss-webpack-plugin");
// const glob = require("glob-all");
// const path = require("path");

// ,
//   configureWebpack: {
//     plugins: [
//       new PurgecssPlugin({
//         paths: glob.sync([
//           path.join(__dirname, "./public/index.html"),
//           path.join(__dirname, "./**/*.vue"),
//           path.join(__dirname, "./src/**/*.js")
//         ])
//       })
//     ]
//   }

module.exports = {
  // ...other vue-cli plugin options...
  pwa: {
    name: "Spells Red: Dungeons and Dragons 5e Spell Search",
    shortName: "Spells Red",
    themeColor: "#bb212b",
    msTileColor: "#a51d26",
    appleMobileWebAppCapable: "yes",
    appleMobileWebAppStatusBarStyle: "black"
    // configure the workbox plugin
  }
};
