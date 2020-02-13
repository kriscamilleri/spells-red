// Inside vue.config.js
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
