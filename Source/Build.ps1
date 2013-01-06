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
    build -install

.EXAMPLE    
    build -target {All|Build|StyleCop|FxCop|Tests|Package} -configuration {Debug|Release}
#>


Param(
    [alias("t")]
    [ValidateSet("All","Build","Tests","StyleCop","FxCop","Package")]
    [array] $target = "All",

    [alias("c")]
    [ValidateSet("Debug", "Release")]
    [string] $configuration = "Debug",

    [switch] $install
)

$currentScriptDirectory = split-path -parent $MyInvocation.MyCommand.Definition
$rootDirectory = Split-Path -parent $currentScriptDirectory
$toolsDirectory = Join-Path $rootDirectory "Tools"

if ($install)
{
    Write-Host "Begin Installation"
    
    Write-Host "Installing MsBuild Extensions Pack"
    $installMsBuildExtensions = [IO.Path]::Combine($toolsDirectory,"MsBuild Extension Pack","MSBuild Extension Pack 4.0.msi")
    Invoke-Expression "& ""$installMsBuildExtensions"""

    Write-Host "Installing StyleCop"
    $installStyleCop = [IO.Path]::Combine($toolsDirectory,"StyleCop","StyleCop-4.7.42.0.msi")
    Invoke-Expression "& ""$installStyleCop"""

    Write-Host "Installing FxCop"
    $installFxCop = [IO.Path]::Combine($toolsDirectory,"FxCop","FxCopSetup.exe")
    Invoke-Expression "& ""$installFxCop"""
        
    Write-Host "Copy MSBuild.ExtensionPack.StyleCop.dll to StyleCop Directory"
    $msBuildExtensionsPath = [IO.Path]::Combine(${env:ProgramFiles(x86)},"MSBuild","ExtensionPack","4.0")    
    $itemsToCopy = Join-Path $msBuildExtensionsPath "MSBuild.ExtensionPack.StyleCop.*"
    $styleCopPath = [IO.Path]::Combine(${env:ProgramFiles(x86)},"StyleCop 4.7")
    Copy-Item $itemsToCopy $styleCopPath
    
    Write-Host "End Installation"
}
else
{
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
}
