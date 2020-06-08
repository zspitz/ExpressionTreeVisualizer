using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Octokit;

namespace Periscope {
    public class GithubProjectInfo : IProjectInfo {
        public GithubProjectInfo(string owner, string repo) {
            ProjectUrl = $"https://github.com/{owner}/{repo}";
            FeedbackUrl = $"https://github.com/{owner}/{repo}/issues/new/choose";
            ReleaseUrl = $"https://github.com/{owner}/{repo}/releases";
            this.owner = owner;
            this.repo = repo;
        }

        public string FeedbackUrl { get; }
        public string ReleaseUrl { get; }
        public string ProjectUrl { get; }

        private readonly string owner;
        private readonly string repo;

        private Version? latestVersion;
        public async Task<Version?> GetLatestVersionAsync(CancellationToken cancellationToken = default) {
            if (latestVersion is { }) { return latestVersion; }

            try {
                var client = new GitHubClient(new ProductHeaderValue($"periscope-{owner}-{repo}"));
                var rateLimit = await client.Miscellaneous.GetRateLimits();
                if (rateLimit.Rate.Remaining < 10) { return latestVersion; }

                var release = await client.Repository.Release.GetLatest(owner, repo);
                var tagName = release.TagName;
                latestVersion = new Version(tagName);
                return latestVersion;
            } catch {
                return latestVersion;
            }
        }

    }
}
