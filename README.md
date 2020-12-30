![Travis (.com)](https://img.shields.io/travis/com/directfront/DbCtl?style=plastic)

# DbCtl

A database migration utility for various database systems. DbCtl is plugin-based which means that it can be extended to support a wide variety of database engines.

## Features

* Plugin model based on [MEF](https://docs.microsoft.com/en-us/dotnet/framework/mef/) to support *almost* any database;
* Forward migrations;
* Backward migrations in single step or undo;
* List applied database migrations;
* Core functionality available as a NuGet package;
* Semantic version support for migration scripts.
* Currently supported databases:

  * Microsoft SQL Server
  * SQLite
  * more coming soon...

## Overview

![architecture](https://github.com/directfront/DbCtl/blob/main/images/dbctl-v0.1-Overview.png?raw=true)

## Installation

Extract the file from the Release page into a folder and execute the `DbCtl.exe` console application as described below.

## Usage

### Example

`.\DbCtl.exe -f -c "Server=localhost;Database=mydb;User Id=sa;Password=******;" -n SqlServer -p .\scripts`

### Filename Conventions

The first thing to know is that your migration scripts need to be named according to the following convention `(f|b)-(ver)-(descr).(ddl|dml|dcl)` where:

* `(F|B|f|b)` is either `f` for forward or `b` for a backwards migration;

* `ver` is the version according to the [SemVer]([https://semver.org/](https://semver.org/)) versioning scheme;

* `descr` is the change description (it may not contain any hyphens or spaces), and;

* `(ddl|dml|dcl)` is the extension that indicates whether it changes the structure, data or security of the database. That is, data definition language, data manipulation language and data control language, respectively.

Migration scripts can be located in any folder and does not necessarily need to be in the same or sub-folder of the application.

### Command line options

`--forward` or `-f`

Migrates the database structure, data or security to the next version. This options will apply all of the database scripts that have not been applied to the database. That is, any file, in the scripts directory, ending with `.dll`, `.dml` or `.dcl` that is not found in the change log table.

`--backward` or `-b`

Migrates the database structure, data or security to the previous version. In other words, the command will execute a single backward script matching the current database version. For example, if the current database version is `1.1.1`, the backward migration will execute a backward script matching `b-1.1.1-...`

`--init` or `-i`

Initializes, or creates, a change log table, in the database, according to the connector's implementation. Once the database has been initialized, it is referred to as a managed database. It is upto the connector to decide the name of the change log table. However, we suggest the standard of `DbCtlChangeLog`.

`--list` or `-l`

Lists the migrations to the database structure, data or security that have been applied. This functionality is on the roadmap and not currently implemented.

`--diff` or `-~`

Lists the migrations to the database structure, data or security that have been applied. This functionality is on the roadmap and not currently implemented.

`--connectionString` or `-c`

Required. The connection string used to connect to the database. This connection string must match the expectation of the connector corresponding to the database technology used.

`--connectorName` or `-n`

Required. The name of the connector to use for connecting to the database. For example, to use the SQL Server connector, specify `SqlServer` as the connector name. All connectors are dynamically loaded from the connectors directory which is a sub-directory of the `DbCtl` application.

`--scriptsPath` or `-p`

Required. Path to the directory where all the database scripts are located and searched. This directory does not need to be in the same folder as the `DbCtl` application.

`--errorExitCode` or `-x`

(Default: -2) Exit code to use when an error occurs. Defaults to -2.

`--help`

Display the help screen.

`--version`

Display version information of the application.

## Roadmap

The following items still need to be implemented and constitutes the roadmap of the application (any code contributions are welcome):

1. Implement `--list` command.
2. Implement `--diff` command.
3. Add support for, at least, the following databases, viz.,

* Oracle
* DB2
* MySQL
* MariaDB
* PostgreSQL
* CockroachDB
* Informix
* Firebird

## License

Copyright 2020 Direct Front Systems (Pty) Ltd.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
