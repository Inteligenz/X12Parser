ApiKey=$1
Source=$2
Version="5.0.${3}"

# List all packages with nuspecs to be packed and pushed
nuget pack ./src/X12.Hipaa/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Parsing/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Shared/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Specifications/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Sql/package.nuspec -Version $Version -Verbosity detailed
nuget pack ./src/X12.Validation/package.nuspec -Version $Version -Verbosity detailed

nuget setApiKey $ApiKey -Source $Source -Verbosity detailed

nuget push X12.*.nupkg -Source $Source