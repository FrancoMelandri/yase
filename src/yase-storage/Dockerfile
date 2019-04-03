FROM microsoft/dotnet:sdk
WORKDIR /app

COPY . ./

RUN dotnet restore ./tests/yase-storage-tests.csproj 
RUN dotnet test ./tests/yase-storage-tests.csproj 