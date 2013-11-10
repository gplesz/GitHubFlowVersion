﻿using System;
using GitHubFlowVersion.AcceptanceTests.Helpers;
using Xunit;

namespace GitHubFlowVersion.AcceptanceTests
{
    public class NoTagsInRepositorySpecification : RepositorySpecification
    {
        private ExecutionResults _result;

        public void GivenARepositoryWithCommitsButNoTags()
        {
            Repository.MakeACommit();
            Repository.MakeACommit();
            Repository.MakeACommit();
        }
        
        public void WhenGitHubFlowVersionIsExecuted()
        {
            _result = GitHubFlowVersionHelper.ExecuteIn(RepositoryPath);
        }

        public void AndGivenRunningInTeamCity()
        {
            Environment.SetEnvironmentVariable("TEAMCITY_VERSION", "8.0.4");
        }

        public void ThenAZeroExitCodeShouldOccur()
        {
            Assert.Equal(0, _result.ExitCode);
        }

        public void AndTheCorrectVersionShouldBeOutput()
        {
            _result.Output.ShouldContainCorrectBuildVersion("0.1.0", 2);
        }
    }
}
