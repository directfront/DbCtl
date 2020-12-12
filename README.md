
# DbCtl
A database migration utility for various database systems. DbCtl is plugin-based which means that it can be extended to support a wide variety of database engines. 

## Usage
### Filename Conventions

The first thing to know is that your migration scripts need to be named according to the following convention `(f|b)-(ver)-(descr).(ddl|dml|dcl)` where:
*  `(f|b)` is either `f` for forward or `b` for a backwards migration;
* `ver` is the version according to the [SemVer](https://semver.org/) versioning scheme;
* `descr` is the change description (it may not contain any hyphens or spaces), and;
* `(ddl|dml|dcl)` is the extension that indicates whether it changes the structure, data or security of the database.

## Connector Implementation Guide

### Schema ChangeLog Table
Below is the `DbCtlChangeLog` table schema for SQL Server:

```
CREATE TABLE DbCtlChangeLog (
	MigrationType VARCHAR(15) NOT NULL,
    Version VARCHAR(10),
	Description VARCHAR(255),
	Filename VARCHAR(255) NOT NULL,
	Hash VARCHAR(64) NOT NULL CONSTRAINT UQ_DbCtlChangeLog_Hash UNIQUE,
    AppliedBy VARCHAR(50) NOT NULL,
	ChangeDateTime DATETIME CONSTRAINT DF_DbCtlChangeLog_ChangeDateTime DEFAULT GETDATE(),
    CONSTRAINT PK_DbCtlChangeLog PRIMARY KEY (Version, Filename)
)
```

<!--stackedit_data:
eyJoaXN0b3J5IjpbLTEwNjQ2MjIwMDUsMTU4OTMxNDg1M119
-->