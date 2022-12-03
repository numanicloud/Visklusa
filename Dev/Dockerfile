FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG config=debug
ENV config=$config
WORKDIR /source

COPY Visklusa.sln .
COPY Visklusa.Abstraction/Visklusa.Abstraction.csproj Visklusa.Abstraction/
COPY Visklusa.Archiver.Zip/Visklusa.Archiver.Zip.csproj Visklusa.Archiver.Zip/
COPY Visklusa.IO/Visklusa.IO.csproj Visklusa.IO/
COPY Visklusa.JsonZip/Visklusa.JsonZip.csproj Visklusa.JsonZip/
COPY Visklusa.Notation.Json/Visklusa.Notation.Json.csproj Visklusa.Notation.Json/
COPY Visklusa.Preset/Visklusa.Preset.csproj Visklusa.Preset/
COPY *.props .
RUN dotnet restore Visklusa.sln

COPY Visklusa.Abstraction/ Visklusa.Abstraction/
COPY Visklusa.Archiver.Zip/ Visklusa.Archiver.Zip/
COPY Visklusa.IO/ Visklusa.IO/
COPY Visklusa.JsonZip/ Visklusa.JsonZip/
COPY Visklusa.Notation.Json/ Visklusa.Notation.Json/
COPY Visklusa.Preset/ Visklusa.Preset/
COPY build.sh .

RUN echo $config
ENTRYPOINT ./build.sh $config