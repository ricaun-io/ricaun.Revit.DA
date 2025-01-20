# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.0.0] / 2025-01-20
### Features
- Initial release.
### DesignApplication
- Add `DesignApplicationLoader` to fix [Design Automation for Revit ignores PackageContents.xml configuration.](https://github.com/ricaun-io/RevitAddin.DA.Tester/issues/7)
- Add `ExternalServer` to fix [Design Automation for Revit ActiveAddInId is null](https://github.com/ricaun-io/RevitAddin.DA.Tester/issues/9)
### Application
- Sample to execute `ricaun.Revit.DA` library.
### Tests
- Test `ricaun.Revit.DA` library.
- Update `TestCase` with expected results. (`Reference`, `TargetFramework` and `ActiveAddInId`)

[vNext]: ../../compare/1.0.0...HEAD
[1.0.0]: ../../compare/1.0.0