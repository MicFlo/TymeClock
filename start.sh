#!/bin/bash

echo "Starting TimeClock Application..."
echo "================================"

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo "Error: .NET is not installed. Please install .NET 9.0 or later."
    exit 1
fi

# Check .NET version
DOTNET_VERSION=$(dotnet --version)
echo "Using .NET version: $DOTNET_VERSION"

# Navigate to the API project
cd "$(dirname "$0")/src/TimeClock.API"

# Build the solution
echo "Building solution..."
dotnet build

if [ $? -ne 0 ]; then
    echo "Build failed. Please check the errors above."
    exit 1
fi

echo "Build successful!"

# Start the application
echo "Starting TimeClock API on http://localhost:7000 and https://localhost:7001"
echo "Web UI: http://localhost:7000"
echo "Swagger: http://localhost:7000/swagger"
echo ""
echo "Press Ctrl+C to stop the application"
echo ""

dotnet run --urls "http://localhost:7000;https://localhost:7001"
