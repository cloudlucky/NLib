#REQUIRES -Version 2.0

<#  
.SYNOPSIS  
    Info1

.DESCRIPTION  
    Info2
.NOTES  
    File Name      : Build.ps1  
    Prerequisite   : PowerShell V2 over Vista and upper.
.LINK  
    Script posted over:  
    http://nlib.codeplex.com  

.EXAMPLE    
    build -target {All|Clean|Build|PrepareOutput|StyleCop|FxCop|Tests|Package} -configuration {Debug|Release}

.EXAMPLE
    build -target {All|Clean|Build|PrepareOutput|StyleCop|FxCop|Tests|Package} -configuration {Debug|Release} -property NuGetTool=C:\Nuget.exe
#>


Param(
    [alias("t")]
    [ValidateSet("All","Clean","Build","PrepareOutput","Tests","StyleCop","FxCop","Package")]
    [array] $target = "All",

    [alias("c")]
    [ValidateSet("Debug", "Release")]
    [string] $configuration = "Debug",

    [alias("p")]
    [array] $property
)

$currentScriptDirectory = split-path -parent $MyInvocation.MyCommand.Definition

$msbuildToolPath = "$env:WINDIR\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe"
if ([environment]::Is64BitOperatingSystem)
{
    $msbuildToolPath = "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\MsBuild.exe"
}

$BuildProject = Join-Path $currentScriptDirectory "Build.proj"
$targetParameter = "/target:""" + [String]::Join(";", $target) + """"
$propertyParameter = "/p:Configuration=$configuration /p:Platform=""Any CPU"""
$extraPropertiesParameter = ""
if ($property -ne $NULL) {
    $extraPropertiesParameter = [String]::Join(";", $property)
}
    
$cmd = """{0}"" ""{1}"" {2} {3} {4}" -f $msbuildToolPath, $BuildProject, $targetParameter, $propertyParameter, $extraPropertiesParameter
    
Invoke-Expression "& $cmd"
