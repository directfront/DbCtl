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

using DbCtl.Core;
using DbCtl.Core.Gateways;
using DbCtl.Core.Services;
using System;
using System.ComponentModel.Composition.Hosting;
using System.IO.Abstractions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DbCtl.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace DbCtl
{
    internal class Action
    {
        private readonly IActionStrategy _Strategy;
        private readonly ApplicationOptions _Options;
        private readonly IConnectorResolver _ConnectorResolver;

        public Action(IActionStrategy strategy, ApplicationOptions options)
            : this(strategy, new ConnectorResolver(new ConnectorsGateway(new FileSystem(), d => new DirectoryCatalog(d))), options)
        {
        }

        public Action(IActionStrategy strategy, IConnectorResolver connectorResolver, ApplicationOptions options)
        {
            _Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            _ConnectorResolver = connectorResolver ?? throw new ArgumentNullException(nameof(connectorResolver));
            _Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var connection = CreateDatabaseConnection();
            var command = _Strategy.GetCommand(connection, _Options);
            await new Invoker().ExecuteAsync(command, cancellationToken);
        }

        private DatabaseConnection CreateDatabaseConnection()
        {
            var connector = _ConnectorResolver.Resolve(_Options.ConnectorName);
            return new DatabaseConnection(connector, _Options.ConnectionString);
        }
    }
}
