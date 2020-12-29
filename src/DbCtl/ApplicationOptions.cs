// Copyright 2020 Direct Front Systems (Pty) Ltd.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using CommandLine;

namespace DbCtl
{
    [Verb("migrate", HelpText = "Effects changes to the database structure (DDL), data (DML) or security (DCL) based " +
            "on the scripts that have not been run against the configured database.")]
    internal class ApplicationOptions
    {
        public const int DefaultErrorExitCode = -2;

        [Option('f', "forward", SetName = "migrate", HelpText = "Migrates the database structure, data or security to the next version.")]
        public bool Forward { get; set; }
        [Option('b', "backward", SetName = "migrate", HelpText = "Migrates the database structure, data or security to the previous version.")]
        public bool Backward { get; set; }
        [Option('l', "list", SetName = "migrate", HelpText = "Lists the migrations to the database structure, data or security that have been applied.")]
        public bool List { get; set; }
        [Option('i', "init", SetName = "migrate", HelpText = "Lists the migrations to the database structure, data or security that have been applied.")]
        public bool Initialize { get; set; }
        [Option('~', "diff", SetName = "migrate", HelpText = "Lists the migrations to the database structure, data or security that have been applied.")]
        public bool Difference { get; set; }
        [Option('c', "connectionString", Required = true, HelpText = "Connection string to connect to the database.")]
        public string ConnectionString { get; set; }
        [Option('n', "connectorName", Required = true, HelpText = "Name of the connector to use for connecting to the database.")]
        public string ConnectorName { get; set; }
        [Option('p', "scriptsPath", Required = true, HelpText = "Path to the scripts to execute.")]
        public string ScriptsPath { get; set; }
        [Option('x', "errorExitCode", Default = DefaultErrorExitCode, HelpText = "Exit code to use when an error occurs. Defaults to -2.")]
        public int ErrorExitCode { get; set; }
    }
}
