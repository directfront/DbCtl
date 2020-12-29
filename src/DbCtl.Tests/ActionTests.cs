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
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace DbCtl.Tests
{
    public class When_calling_execute_async_on_action
    {
        private CancellationToken _CancellationToken;
        private Mock<Command> _Command;
        private ApplicationOptions _Options;
        private Mock<IActionStrategy> _Strategy;
        private Mock<IConnectorResolver> _ConnectorResolver;

        [SetUp]
        public void Setup()
        {
            _Options = new ApplicationOptions
            {
                ConnectionString = "db-connection-string"
            };

            var connector = new Mock<IDbConnector>();
            var connection = new DatabaseConnection(connector.Object, _Options.ConnectionString);
            _Command = new Mock<Command>(connection);

            _ConnectorResolver = new Mock<IConnectorResolver>();
            _ConnectorResolver.Setup(cr => cr.Resolve(It.IsAny<string>())).Returns(connector.Object);
            
            _Strategy = new Mock<IActionStrategy>();
            _CancellationToken = new CancellationToken();
        }

        [Test]
        public async Task It_should_execute_the_command()
        {
            _Strategy.Setup(s => s.GetCommand(It.IsAny<DatabaseConnection>(), _Options)).Returns(_Command.Object);
            
            var action = new Action(_Strategy.Object, _ConnectorResolver.Object, _Options);
            await action.ExecuteAsync(_CancellationToken);

            _Command.Verify(command => command.ExecuteAsync(_CancellationToken));
        }
    }
}