# Backup Manager

This project is a suite of tools meant to ease the storage and management of
customer and operational backups for Virtual Chemistry.

## Project structure

Everything lives inside one main solution named `BackupManager`.
The main way of communicating with the code inside all of the modules is through
the `CommandLineInterface`.

Each module has a programatic way of calling it allowing you to skip the cli.

## Projects

### FindDate

This project parses an incoming csv file for keys and dates and then gives the
first and last date a backup was made for said customer

### FolderBackup

This project seperates folders within categorical folders easily and interfaces
with Amazon S3 to automatically move folders around.

##### Notes & Useful resources

In S3 there is no concept of folders meaning that it is a flat filesystem which
allow super long filenames that include slashes. This makes my work much easier
since I just need to insert slashes in file names instead of creating folders
and moving objects (files)
source: https://stackoverflow.com/a/20674074/3453207

https://docs.aws.amazon.com/AmazonS3/latest/dev/CopyingObjectUsingNetSDK.html