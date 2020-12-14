
# DbCtl
A database migration utility for various database systems. DbCtl is plugin-based which means that it can be extended to support a wide variety of database engines. 

## Usage
### Filename Conventions

The first thing to know is that your migration scripts need to be named according to the following convention `(f|b)-(ver)-(descr).(ddl|dml|dcl)` where:
*  `(f|b)` is either `f` for forward or `b` for a backwards migration;
* `ver` is the version according to the [SemVer](https://semver.org/) versioning scheme;
* `descr` is the change description (it may not contain any hyphens or spaces), and;
* `(ddl|dml|dcl)` is the extension that indicates whether it changes the structure, data or security of the database.

### Execution Order
Scripts will be executed in the following order, after resolving which scripts need to be executed:
1. Scripts are first ordered by `version` number;
2. Then, by file extension where the file extension has the following precedence: `.ddl`, `.dml` and `.dcl`.

<!--stackedit_data:
eyJoaXN0b3J5IjpbLTE4Njc1MTIzMSwtMTg2NzUxMjMxLDQ0Mz
A1NzU3MCwtMTIyOTY2MDYzNCwtMTA2NDYyMjAwNSwxNTg5MzE0
ODUzXX0=
-->