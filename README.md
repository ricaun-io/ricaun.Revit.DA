# ricaun.Revit.DA

[![Revit 2019](https://img.shields.io/badge/Revit-2019+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](../../actions/workflows/Build.yml/badge.svg)](../../actions)

This project was generated by the [ricaun.AppLoader](https://ricaun.com/AppLoader/) Revit plugin.

## PackageReference

Install the `ricaun.Revit.DA` library from NuGet.

```xml
<PackageReference Include="ricaun.Revit.DA" Version="*" />
```

## DesignApplication

The `DesignApplication` is a base class with some abstractions to help you to execute Design Automation for Revit.

```C#
public class App : DesignApplication
{
    public override bool Execute(Application application, string filePath, Document document)
    {
        return true;
    }

    public override void OnStartup()
    {

    }

    public override void OnShutdown()
    {

    }
}
```

### Properties

#### ControlledApplication

The `ControlledApplication` stores the internal `ControlledApplication` from the `IExternalDBApplication`.
```C#
public override void OnStartup()
{
    ControlledApplication.ApplicationInitialized += (s, e) => { };
}
```

## License

This project is [licensed](LICENSE) under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!