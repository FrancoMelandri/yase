FROM microsoft/dotnet:sdk
WORKDIR /app

COPY . ./

RUN dotnet restore ./tests/yase-core-tests.csproj 
RUN dotnet test ./tests/yase-core-tests.csproj 