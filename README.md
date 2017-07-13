# Backup Manager

This project is a suite of tools meant to ease the storage and management of customer and operational backups for Virtual Chemistry.

## Project structure

Everything lives inside one main solution named `BackupManager`.
The main way of communicating with the code inside all of the modules is through the `CommandLineInterface`.

Each module has a programatic way of calling it allowing you to skip the cli.