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

using DbCtl.Connectors;
using DbCtl.Core;
using DbCtl.Core.Commands;
using DbCtl.Core.Services;

namespace DbCtl
{
    internal class ForwardMigrationStrategy : IActionStrategy
    {
        public Command GetCommand(DatabaseConnection connection, ApplicationOptions options)
        {
            var migrationType = MigrationType.Forward;

            var migrationScriptService = new MigrationScriptService(options.ScriptsPath, migrationType);
            var databaseVersionService = new DatabaseVersionService(connection);

            return new MigrateCommand(connection, migrationScriptService, databaseVersionService)
            {
                Type = migrationType
            };
        }
    }
}
