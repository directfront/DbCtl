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
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Settings.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DbCtl
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                var result = Parser.Default.ParseArguments<ApplicationOptions>(args)
                    .WithNotParsed(errors => HandleParseError(errors))
                    .WithParsedAsync(async options => await ExecuteAsync(options));

                result.Wait();
                    
                Log.Information($"Done (Exit Code={Environment.ExitCode})");
            }
            catch(AggregateException ex)
            {
                foreach (var e in ex.Flatten().InnerExceptions)
                    Log.Error(e.ToString(), ConsoleColor.DarkRed);
            }
            catch(Exception ex)
            {
                Log.Error(ex.ToString(), ConsoleColor.DarkRed);
            }
        }

        private static async Task ExecuteAsync(ApplicationOptions options)
        {
            IActionStrategy strategy = null;

            if (options.Initialize)
                strategy = new InitializationStrategy();

            if (options.Forward)
                strategy = new ForwardMigrationStrategy();

            if (options.Backward)
                strategy = new BackwardMigrationStrategy();

            if (options.List)
                throw new NotImplementedException();

            if (options.Difference)
                throw new NotImplementedException();

            var cancellationToken = new CancellationToken();
            await new Action(strategy, options).ExecuteAsync(cancellationToken);
        }

        static int HandleParseError(IEnumerable<Error> errs)
        {
            var result = ApplicationOptions.DefaultErrorExitCode;
            
            if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
                result = 0;
            
            return result;
        }
    }
}
