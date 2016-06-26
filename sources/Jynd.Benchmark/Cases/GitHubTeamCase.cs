using System;

namespace Jynd.Benchmark.Cases
{
    public static class GitHubTeamCase
    {
        public const string Data = @"{'id':1,'url':'https://api.github.com/teams/1','name':'Justice League','slug':'justice-league','description':'A great team.','privacy':'closed','permission':'admin','members_url':'https://api.github.com/teams/1/members{/member}','repositories_url':'https://api.github.com/teams/1/repos','members_count':3,'repos_count':10,'organization':{'login':'github','id':1,'url':'https://api.github.com/orgs/github','repos_url':'https://api.github.com/orgs/github/repos','events_url':'https://api.github.com/orgs/github/events','hooks_url':'https://api.github.com/orgs/github/hooks','issues_url':'https://api.github.com/orgs/github/issues','members_url':'https://api.github.com/orgs/github/members{/member}','public_members_url':'https://api.github.com/orgs/github/public_members{/member}','avatar_url':'https://github.com/images/error/octocat_happy.gif','description':'A great organization'}}";

        public static BenchmarkCase<Team> Instance
        {
            get
            {
                return new BenchmarkCase<Team>
                {
                    Name = "github-team",
                    Iterations = 1000000,
                    Source = "https://developer.github.com/v3/orgs/teams/",
                    Data = Data.Replace('\'', '\"'),
                    OnDynamic = CheckDynamic,
                    OnStatic = CheckStatic
                };
            }
        }

        private static void CheckDynamic(dynamic instance)
        {
            string url = instance.url;
            int members = instance.members_count;
            int repos = instance.repos_count;
            string avatar = instance.organization.avatar_url;

            if (url != "https://api.github.com/teams/1")
                throw new Exception();

            if (members != 3)
                throw new Exception();

            if (repos != 10)
                throw new Exception();

            if (avatar != "https://github.com/images/error/octocat_happy.gif")
                throw new Exception();
        }

        private static void CheckStatic(Team instance)
        {
            string url = instance.url;
            int members = instance.members_count;
            int repos = instance.repos_count;
            string avatar = instance.organization.avatar_url;

            if (url != "https://api.github.com/teams/1")
                throw new Exception();

            if (members != 3)
                throw new Exception();

            if (repos != 10)
                throw new Exception();

            if (avatar != "https://github.com/images/error/octocat_happy.gif")
                throw new Exception();
        }

        public class Team
        {
            public int id { get; set; }
            public string url { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public string description { get; set; }
            public string privacy { get; set; }
            public string permission { get; set; }
            public string members_url { get; set; }
            public string repositories_url { get; set; }
            public int members_count { get; set; }
            public int repos_count { get; set; }
            public Organization organization { get; set; }
        }

        public class Organization
        {
            public string login { get; set; }
            public int id { get; set; }
            public string url { get; set; }
            public string repos_url { get; set; }
            public string events_url { get; set; }
            public string hooks_url { get; set; }
            public string issues_url { get; set; }
            public string members_url { get; set; }
            public string public_members_url { get; set; }
            public string avatar_url { get; set; }
            public string description { get; set; }
        }
    }
}