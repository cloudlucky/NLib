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
    build -target {All|Clean|Build|StyleCop|FxCop|Tests|Package} -configuration {Debug|Release}
#>


Param(
    [alias("t")]
    [ValidateSet("All","Clean","Build","Tests","StyleCop","FxCop","Package")]
    [array] $target = "All",

    [alias("c")]
    [ValidateSet("Debug", "Release")]
    [string] $configuration = "Debug"
)

$currentScriptDirectory = split-path -parent $MyInvocation.MyCommand.Definition

$msbuildToolPath = "$env:WINDIR\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe"
if ([environment]::Is64BitOperatingSystem)
{
    $msbuildToolPath = "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\MsBuild.exe"
}

$BuildProject = Join-Path $currentScriptDirectory "Build.proj"
$targetParameter = " /target:""" + [String]::Join(";", $target) + """"
$propertyParameter = " /p:Configuration=$configuration /p:Platform=""Any CPU"""
    
$cmd = """{0}"" ""{1}"" {2} {3}" -f $msbuildToolPath, $BuildProject, $targetParameter, $propertyParameter
    
Invoke-Expression "& $cmd"
