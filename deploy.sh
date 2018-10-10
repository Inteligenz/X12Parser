ApiKey=$1
Source=$2

# List all packages with nuspecs to be packed and pushed
nuget pack ./src/X12.Hipaa/package.nuspec
nuget pack ./src/X12.Parsing/package.nuspec
nuget pack ./src/X12.Shared/package.nuspec
nuget pack ./src/X12.Specifications/package.nuspec
nuget pack ./src/X12.Sql/package.nuspec
nuget pack ./src/X12.Validation/package.nuspec

nuget setApiKey $ApiKey -Source $Source -Verbosity detailed

nuget push X12.*.nupkg -Source $Source