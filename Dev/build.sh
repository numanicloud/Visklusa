echo "configuration=$1"
dotnet build Visklusa.Abstraction/Visklusa.Abstraction.csproj -c $1 --no-restore -o /dockerbuild/$1
dotnet build Visklusa.Archiver.Zip/Visklusa.Archiver.Zip.csproj -c $1 --no-restore -o /dockerbuild/$1
dotnet build Visklusa.IO/Visklusa.IO.csproj -c $1 --no-restore -o /dockerbuild/$1
dotnet build Visklusa.JsonZip/Visklusa.JsonZip.csproj -c $1 --no-restore -o /dockerbuild/$1
dotnet build Visklusa.Notation.Json/Visklusa.Notation.Json.csproj -c $1 --no-restore -o /dockerbuild/$1
dotnet build Visklusa.Preset/Visklusa.Preset.csproj -c $1 --no-restore -o /dockerbuild/$1