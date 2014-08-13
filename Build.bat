@ECHO OFF

ECHO Cleaning Build directory...
rmdir /s /q Build

ECHO Building Samurai.Windows.sln...
msbuild ".\Source\Samurai.Windows.sln" /nologo /verbosity:quiet /target:clean
msbuild ".\Source\Samurai.Windows.sln" /nologo /verbosity:quiet /property:Configuration=Release /property:Platform="Any CPU" /property:OutputPath=.\..\..\Build /property:WarningLevel=2
del Build\*.pdb