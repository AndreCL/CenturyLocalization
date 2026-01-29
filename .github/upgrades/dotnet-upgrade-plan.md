# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade CenturyLocalization\CenturyLocalization.csproj to .NET 10.0
4. Upgrade LocalizationTests\LocalizationTests.csproj to .NET 10.0
5. Run unit tests to validate upgrade in the project: LocalizationTests\LocalizationTests.csproj

## Settings

This section contains settings and data used by execution steps.

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### CenturyLocalization\CenturyLocalization.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`

#### LocalizationTests\LocalizationTests.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net10.0`