module.exports = function () {
  var instanceRoot = "C:\\inetpub\\wwwroot\\sitecorecoffee.local";
  var config = {
    websiteRoot: instanceRoot + "\\Website",
    sitecoreLibraries: instanceRoot + "\\Website\\bin",
    licensePath: instanceRoot + "\\Data\\license.xml",
    solutionName: "SitecoreCoffee",
    buildConfiguration: "Debug",
    buildPlatform: "Any CPU",
    publishPlatform: "AnyCpu",
    runCleanBuilds: false
  };
  return config;
}
