if os.ishost("windows") then

    -- Windows
    newaction
    {
        trigger     = "solution",
        description = "Open the GameEstate solution",
        execute = function ()
            os.execute "start src/GameEstate.sln"
        end
    }

    newaction
    {
        trigger     = "native",
        description = "Build the native libraries",
        onStart = function()
            -- install vcpkg
            if not os.isdir("_vcpkg") then
                print("vcpkg does not exist. installing...")
                os.execute "git.exe clone https://github.com/Microsoft/vcpkg.git _vcpkg"
                os.execute "cd _vcpkg && bootstrap-vcpkg.bat -disableMetrics"
            end
            -- install quickbms:x86-windows
            if not os.isdir("_vcpkg/packages/quickbms_x86-windows") then
                os.execute "cd _vcpkg && vcpkg install quickbms:x86-windows --overlay-ports=../custom-ports"
            end
            -- build quickbms:x86-windows
            if true or not os.isfile("_vcpkg/packages/quickbms_x86-windows/bin/quickbms.exe") then
                os.execute "cd _vcpkg/buildtrees/quickbms/src/0.10.1-f19b127b22/ && build.cmd"
            end
        end,
        execute = function ()
        end
    }

else

     -- MacOSX and Linux.
    
     newaction
     {
         trigger     = "solution",
         description = "Open the GameEstate solution",
         execute = function ()
         end
     }
 
     newaction
     {
         trigger     = "loc",
         description = "Count lines of code",
         execute = function ()
             os.execute "wc -l *.cs"
         end
     }
     
end

newaction
{
    trigger     = "build",
    description = "Build GameEstate",
    execute = function ()
        os.execute "dotnet build src/xyz.csproj"
    end
}

newaction
{
    trigger     = "test",
    description = "Build and run all unit tests",
    execute = function ()
        -- os.execute "premake5 build"
        os.execute "dotnet test src/xyz.tests/xyz.tests.csproj"
    end
}

newaction
{
    trigger     = "pack",
    description = "Package and run all unit tests",
    execute = function ()
        os.execute "dotnet pack src/xyz/xyz.csproj --output ../nupkgs --include-source"
    end
}

newaction
{
    trigger     = "publish",
    description = "Package and publish nuget package",
    execute = function ()
        apikey = os.getenv('NUGET_APIKEY')
        os.execute "premake5 pack"
        os.execute( "dotnet nuget push nupkgs/**/xyz.*.nupkg --api-key " .. apikey .. " --source https://api.nuget.org/v3/index.json" )
    end
}

newaction
{
    trigger     = "clean",
    description = "Clean all build files and output",
    execute = function ()
        files_to_delete = 
        {
            "*.make",
            "*.zip",
            "*.tar.gz",
            "*.db",
            "*.opendb"
        }
        directories_to_delete = 
        {
            "obj",
            "ipch",
            "bin",
            "nupkgs",
            ".vs",
            "Debug",
            "Release",
            "release",
            "_vcpkg"
        }
        for i,v in ipairs( directories_to_delete ) do
          os.rmdir( v )
        end
        if not os.ishost "windows" then
            os.execute "find . -name .DS_Store -delete"
            for i,v in ipairs( files_to_delete ) do
              os.execute( "rm -f " .. v )
            end
        else
            for i,v in ipairs( files_to_delete ) do
              os.execute( "del /F /Q  " .. v )
            end
        end

    end
}
