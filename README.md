# TimeClock - Employee Time Management System

A modern, professional employee time management system built with .NET Core 8.0, featuring a beautiful web UI and RESTful API for tracking employee time punches.

## Features

- **Clock In/Out**: Record employee clock-in and clock-out times
- **Lunch Tracking**: Track lunch breaks and meal periods
- **Transfer Logging**: Log department or location transfers
- **Real-time Updates**: Live time display and instant punch recording
- **Modern UI**: Beautiful, responsive web interface with excellent UX
- **RESTful API**: Full CRUD operations for time punch management
- **SQLite Database**: Lightweight, file-based database storage
- **Swagger Documentation**: Interactive API documentation

## Technology Stack

- **Backend**: .NET Core 8.0 Web API
- **Database**: SQLite with Entity Framework Core
- **Frontend**: Modern HTML5, CSS3, and JavaScript
- **Architecture**: Clean Architecture with separation of concerns
- **UI Framework**: Custom CSS with modern design principles

## Project Structure

```
TimeClock/
├── src/
│   ├── TimeClock.Core/           # Domain models and interfaces
│   ├── TimeClock.Infrastructure/ # Data access and services
│   └── TimeClock.API/           # Web API and controllers
├── TimeClock.sln                 # Solution file
└── README.md                     # This file
```

## Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022, VS Code, or any .NET-compatible IDE

## Getting Started

### 1. Clone and Build

```bash
git clone <repository-url>
cd TimeClock
dotnet restore
dotnet build
```

### 2. Run the Application

```bash
cd src/TimeClock.API
dotnet run
```

The application will start on `https://localhost:7001` (or `http://localhost:7000`).

### 3. Access the Application

- **Web UI**: Navigate to `http://localhost:7000` in your browser
- **API Documentation**: Visit `http://localhost:7000/swagger` for Swagger UI
- **Database**: SQLite database file will be created automatically at `TimeClock.db`

## API Endpoints

### Time Punch Management

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/timepunch` | Create a new time punch |
| `GET` | `/api/timepunch/user/{userId}` | Get all time punches for a user |
| `GET` | `/api/timepunch/{id}` | Get a specific time punch by ID |
| `PUT` | `/api/timepunch/{id}` | Update an existing time punch |
| `DELETE` | `/api/timepunch/{id}` | Delete a time punch |

### Request/Response Examples

#### Create Time Punch
```http
POST /api/timepunch
Content-Type: application/json

{
  "userId": "EMP001",
  "type": "In",
  "notes": "Starting morning shift"
}
```

#### Get User Time Punches
```http
GET /api/timepunch/user/EMP001
```

#### Update Time Punch
```http
PUT /api/timepunch/1
Content-Type: application/json

{
  "type": "Out",
  "timestamp": "2024-01-15T17:00:00Z",
  "notes": "End of shift"
}
```

## Punch Types

The system supports four types of time punches:

1. **In** - Clock in (start of work)
2. **Out** - Clock out (end of work)
3. **Lunch** - Lunch break or meal period
4. **Transfer** - Department or location transfer

## User Interface Features

### Modern Design
- **Gradient Backgrounds**: Beautiful color schemes and visual appeal
- **Card-based Layout**: Clean, organized information presentation
- **Responsive Design**: Works seamlessly on desktop and mobile devices
- **Smooth Animations**: Hover effects and transitions for better UX

### User Experience
- **Real-time Clock**: Live time and date display
- **Quick Actions**: Large, intuitive punch buttons
- **Visual Feedback**: Loading states and success/error messages
- **Easy Management**: Edit and delete time punches with confirmation

### Key UI Components
- **Employee ID Input**: Simple user identification
- **Punch Buttons**: Large, color-coded action buttons
- **Today's Activity**: Quick view of current day's punches
- **All Time Punches**: Complete history with management options

## Database Schema

The SQLite database contains a single `TimePunches` table with the following structure:

```sql
CREATE TABLE TimePunches (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    Type TEXT NOT NULL,
    Timestamp DATETIME NOT NULL,
    Notes TEXT,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME
);
```

## Configuration

The application uses `appsettings.json` for configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=TimeClock.db"
  }
}
```

## Development

### Adding New Features
1. **Domain Models**: Add new entities in `TimeClock.Core/Models/`
2. **Services**: Implement business logic in `TimeClock.Infrastructure/Services/`
3. **API Controllers**: Add new endpoints in `TimeClock.API/Controllers/`
4. **UI Updates**: Modify the HTML/CSS/JavaScript in `wwwroot/index.html`

### Database Migrations
```bash
cd src/TimeClock.API
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Testing

### Manual Testing
1. Start the application
2. Open the web UI in your browser
3. Test all punch types (In, Out, Lunch, Transfer)
4. Verify CRUD operations work correctly
5. Test error handling and edge cases

### API Testing
1. Use Swagger UI at `/swagger`
2. Test all endpoints with different data
3. Verify response formats and error handling

## Deployment

### Production Considerations
- Update connection string for production database
- Configure proper CORS policies
- Set up logging and monitoring
- Implement authentication and authorization
- Use HTTPS in production

### Docker Support
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TimeClock.API/TimeClock.API.csproj", "TimeClock.API/"]
COPY ["TimeClock.Core/TimeClock.Core.csproj", "TimeClock.Core/"]
COPY ["TimeClock.Infrastructure/TimeClock.Infrastructure.csproj", "TimeClock.Infrastructure/"]
RUN dotnet restore "TimeClock.API/TimeClock.API.csproj"
COPY . .
WORKDIR "/src/TimeClock.API"
RUN dotnet build "TimeClock.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TimeClock.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimeClock.API.dll"]
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support and questions:
- Create an issue in the repository
- Contact the development team
- Check the documentation and examples

## Changelog

### Version 1.0.0
- Initial release
- Basic time punch functionality
- Modern web UI
- RESTful API
- SQLite database support
