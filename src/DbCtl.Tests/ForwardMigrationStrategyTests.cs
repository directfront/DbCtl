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
using Moq;
using NUnit.Framework;

namespace DbCtl.Tests
{
    [TestFixture]
    public class When_calling_get_command_on_forward_migration_strategy
    {
        private IActionStrategy _Strategy;
        private ApplicationOptions _Options;
        private DatabaseConnection _Connection;

        [SetUp]
        public void Setup()
        {
            _Options = new ApplicationOptions
            {
                ConnectionString = "db-connection-string",
                ScriptsPath = ".\\scripts"
            };

            var connector = new Mock<IDbConnector>();
            _Connection = new DatabaseConnection(connector.Object, _Options.ConnectionString);

            _Strategy = new ForwardMigrationStrategy();
        }

        [Test]
        public void It_should_get_a_backward_migration_command()
        {
            var command = _Strategy.GetCommand(_Connection, _Options);

            Assert.IsInstanceOf<MigrateCommand>(command);
            Assert.AreEqual(MigrationType.Forward, ((MigrateCommand)command).Type);
        }
    }
}
