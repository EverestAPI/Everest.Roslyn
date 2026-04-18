# Building and Testing `ModLifecycleAttributes`

## Building

Clone the solution and build the `ModLifecycleAttributes` project. Prefer Release mode as it's
more optimized.

Building in Release mode also generates a NuGet package.

> [!IMPORTANT]
> Before publishing to NuGet, remember to bump the package version in the `.csproj` file according to
> [SemVer](https://semver.org/) and update `CHANGELOG.md`.

## Testing the NuGet Package

Packages pushed to https://nuget.org/ are immutable, meaning any mistake you make there will be permanent.  
Fortunately, you can test the NuGet package locally before pushing it for real.

First, open the package and inspect it. `.nupkg` files are `.zip` files in disguise.  
Make sure only the necessary files are present.

Second, create a NuGet package source that points to some folder if you don't already have one.  
You will be able to safely push the package there for testing and have other projects be able to reference it,
so long that source is enabled.
```
dotnet nuget add source --name "Local NuGet Packages" "path/to/package/source/folder"
```

Then, push the package to the test source.
```
dotnet nuget push "path/to/package.nupkg" --source "Local NuGet Packages"
```

Now, open a project you want to test the NuGet package in.  
Reference the package in its `.csproj` like below, replacing the version with the one you just built.
```xml
<ItemGroup>
  <PackageReference Include="CelesteMod.Roslyn.ModLifecycleAttributes" Version="x.y.z" />
</ItemGroup>
```
> [!NOTE]
> Using a version already present in NuGet may use the published package instead of your test package.  
> Remember to bump the version before building.

Restore the solution for the package to be available.  
Now you can test and make sure the package works as expected.

If you make changes to the package and push it again, you will need to clear the NuGet cache.  
Your IDE may interfere here, so close it before clearing the cache.
```
dotnet nuget locals all --clear
```

Once you feel you're ready to publish, you can disable the local source without deleting it.  
You may enable it later by rerunning the command below, replacing `disable` with `enable`.
```
dotnet nuget disable source "Local NuGet Packages"
```
