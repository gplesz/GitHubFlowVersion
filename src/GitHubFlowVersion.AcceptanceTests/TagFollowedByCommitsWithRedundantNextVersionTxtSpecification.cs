﻿using GitHubFlowVersion.AcceptanceTests.Helpers;
using TestStack.BDDfy;
using Xunit.Extensions;

namespace GitHubFlowVersion.AcceptanceTests
{
    public class TagFollowedByCommitsWithRedundantNextVersionTxtSpecification : RepositorySpecification
    {
        private ExecutionResults _result;
        private const string TaggedVersion = "1.0.3";
        private int _numCommitsToMake;
        private const string NextVersionTxtVersion = "1.0.0";
        private const string ExpectedNextVersion = "1.0.4";

        public void GivenARepositoryWithASingleTag()
        {
            Repository.MakeATaggedCommit(TaggedVersion);
        }

        public void AndGivenRepositoryHasAnotherXCommits()
        {
            Repository.MakeCommits(_numCommitsToMake);
        }

        public void AndGivenRepositoryHasARedundantNextVersionTxtFile()
        {
            Repository.AddNextVersionTxtFile(NextVersionTxtVersion);
        }
        
        public void WhenGitHubFlowVersionIsExecuted()
        {
            _result = GitHubFlowVersionHelper.ExecuteIn(RepositoryPath);
        }

        public void ThenAZeroExitCodeShouldOccur()
        {
            _result.AssertExitedSuccessfully();
        }

        public void AndTheCorrectVersionShouldBeOutput()
        {
            _result.Output.ShouldContainCorrectBuildVersion(ExpectedNextVersion, _numCommitsToMake);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        public void Run(int noCommits)
        {
            _numCommitsToMake = noCommits;
            this.BDDfy();
        }

        protected new void RunSpecification() { }
    }
}
