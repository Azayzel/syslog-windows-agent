# Jakkl - Syslog Windows Agent ![Jakkl](jakkl_sm.png "Jakkl")

A free Windows Event Collector Agent that forwards Windows Event Logs to a Syslog server (e.g., Syslog-ng, Graylog, Splunk) using the standard [RFC 5424 Syslog](https://tools.ietf.org/html/rfc5424) protocol.

## Requirements

- Windows 10/11 or Windows Server 2016+
- [.NET 10 Runtime](https://dotnet.microsoft.com/download/dotnet/10.0) (Windows Desktop Runtime)

## Building from Source

```bash
# Clone the repo
git clone https://github.com/Azayzel/syslog-windows-agent.git
cd syslog-windows-agent

# Build
dotnet build

# Run
dotnet run
```

## Features

- Monitor multiple Windows Event Logs simultaneously
- Filter by event types (Error, Warning, Information, SuccessAudit, FailureAudit)
- Send events to a remote Syslog server via TCP+TLS, TCP, or UDP
- RFC 5424 and RFC 3164 message serialization
- System tray application with balloon notifications
- Settings persisted to Windows Registry
- Optional run-on-startup configuration

## Configuration

1. Double-click the system tray icon to open the configuration window
2. Select the event log(s) to monitor
3. Choose event type filters
4. Enter your Syslog server address and port
5. Select the transport protocol (TCP+TLS recommended)
6. Click **Apply**

All settings are saved to the Windows Registry under `HKLM\SOFTWARE\Jakkl`.

## v2.0.0 Breaking Changes

- Migrated from .NET Framework 4.5.2 to **.NET 10**
- Replaced MetroFramework UI with standard WinForms controls
- Replaced custom NotifyIconEx with built-in `NotifyIcon`
- Added dependency injection with `Microsoft.Extensions.Logging`
- Supports multi-select for both Event Sources and Event Types
- Transport protocol (TCP+TLS, TCP, UDP) is now configurable from the UI
- Async network operations
- Removed Unix/local syslog code paths (Windows-only agent)
- Uses process ID instead of hardcoded ProcId
- TLS now negotiates the best available protocol version (TLS 1.2/1.3)

## Architecture

```text
Program.cs              - Entry point with DI container
Config.cs               - WinForms UI (system tray + config window)
Services/
  EventMonitorService   - Windows Event Log monitoring
  SyslogService         - Syslog message construction and sending
  SettingsService       - Registry-based settings persistence
Syslog/
  SyslogMessage         - RFC 5424 message model
  Serialization/        - RFC 5424 and RFC 3164 serializers
  Transport/            - TCP, TCP+TLS, and UDP senders
```

## License

Copyright 2019-2026 Josh Lavely
