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
    trigger     = "quickbms",
    description = "Download quickbms native libraries",
    execute = function()
        if os.ishost("windows") then
            downloadPackage("https://aluigi.altervista.org/papers/quickbms.zip", ".quickbms")
        else
            if os.ishost("macosx") then
                downloadPackage("https://aluigi.altervista.org/papers/quickbms_macosx.zip", ".quickbms")
            else
                downloadPackage("https://aluigi.altervista.org/papers/quickbms_linux.zip", ".quickbms")
            end
        end
        downloadPackage("https://aluigi.altervista.org/quickbms_scripts.php", ".quickbms/scripts")
    end
}

newaction
{
    trigger     = "fakebms",
    description = "Fake quickbms native libraries",
    execute = function()
        os.mkdir(".quickbms")
        if os.ishost("windows") then
            os.execute "type NUL > .quickbms/quickbms_4gb_files.exe"
            os.execute "type NUL > .quickbms/quickbms.exe"
        else
            if os.ishost("macosx") then
                os.execute "type NUL > .quickbms/quickbms_4gb_files.exe"
                os.execute "type NUL > .quickbms/quickbms.exe"
            else
                os.execute "type NUL > .quickbms/quickbms_4gb_files.exe"
                os.execute "type NUL > .quickbms/quickbms.exe"
            end
        end
    end
}

newaction
{
    trigger     = "build",
    description = "Build GameEstate",
    execute = function ()
        -- install quickbms
        if not os.isdir(".quickbms") then
            print(".quickbms does not exist. installing...")
            os.execute "premake5 quickbms"
        end
        -- os.execute "dotnet build src/xyz.csproj"
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

function downloadPackage(url, location)
    if http == nil then
        return false
    end

    -- Download the module.
    local destination = location .. '/temp.zip'

    os.mkdir(location)
    local result_str, response_code = http.download(url, destination, {
        progress = http.reportProgress
    })

    if result_str ~= 'OK' then
        premake.error('Download of %s failed (%d)\n%s', url, response_code, result_str)
    end

    -- Unzip the module, and delete the temporary zip file.
    verbosef(' UNZIP   : %s', destination)
    zip.extract(destination, location)
    os.remove(destination)
    return true;
end
