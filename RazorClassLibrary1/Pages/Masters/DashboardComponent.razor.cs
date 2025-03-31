using Radzen.Blazor.Rendering;
using RazorClassLibrary1.Dashboard_Models;
using RazorClassLibrary1.Services;
using System.Text.RegularExpressions;

namespace RazorClassLibrary1.Pages.Masters
{
    public partial class DashboardComponent
    {
        IEnumerable<Issue> issues;
        IEnumerable<Issue> openIssues;
        IEnumerable<Issue> closedIssues;

        class IssueGroup
        {
            public int Count { get; set; }
            public DateTime Week { get; set; }
        }

        class LabelGroup
        {
            public int Count { get; set; }
            public string Label { get; set; }
            public string Color { get; set; }
        }

        class UserGroup
        {
            public int Count { get; set; }
            public string Name { get; set; }
        }

        IEnumerable<IssueGroup> openIssuesByDate;
        IEnumerable<IssueGroup> closedIssuesByDate;
        IEnumerable<LabelGroup> labelGroups;
        IEnumerable<UserGroup> openByGroups;
        IEnumerable<User> users;
        IEnumerable<Issue> filteredIssues;
        IEnumerable<string> labels;
        IEnumerable<string> selectedLabels;
        IEnumerable<IssueState> issueStates = Enum.GetValues(typeof(IssueState)).Cast<IssueState>();
        IEnumerable<string> labelColors;
        IssueState selectedState = IssueState.All;
        User mostActiveMember;
        User selectedUser;
        double closeRatio = 0;
        double closeRatioPercentage = 0;
        int currentPage = 0;
        int totalPages = 0;
        bool fetchingData = false;
        string error = null!;

        class UserComparer : IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                return x.Login.Equals(y.Login);
            }

            public int GetHashCode(User user)
            {
                return user.Login.GetHashCode();
            }
        }

        class LabelComparer : IEqualityComparer<Label>
        {
            public bool Equals(Label x, Label y)
            {
                return x.Name!.Equals(y.Name);
            }

            public int GetHashCode(Label user)
            {
                return user.Name!.GetHashCode();
            }
        }


        void FilterIssues()
        {
            filteredIssues = issues.OrderByDescending(issue => issue.CreatedAt);

            if (selectedUser != null)
            {
                filteredIssues = issues.Where(issue => issue.User.Login == selectedUser.Login);
            }

            if (selectedLabels != null)
            {
                foreach (var selectedLabel in selectedLabels)
                {
                    filteredIssues = filteredIssues.Where(issue => issue.Labels.Any(label => label.Name == selectedLabel));
                }
            }

            if (selectedState != IssueState.All)
            {
                filteredIssues = filteredIssues.Where(issue => issue.State == selectedState);
            }
        }

        void OnProgress(FetchProgressEventArgs args)
        {
            currentPage = args.Current;
            totalPages = args.Total;

            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //GitHub.OnProgress += OnProgress;
                fetchingData = true;
                try
                {
                    issues = await GitHub.GetIssues(DateTime.Now);
                    filteredIssues = issues.OrderByDescending(issue => issue.CreatedAt);
                    openIssues = issues.Where(issue => issue.State == IssueState.Open);
                    closedIssues = issues.Where(issue => issue.State == IssueState.Closed);

                    closeRatio = closedIssues.Count() / (double)issues.Count();
                    closeRatioPercentage = closeRatio * 100;

                    openIssuesByDate = openIssues.GroupBy(issue => issue.CreatedAt.EndOfWeek())
                            .Select(group => new IssueGroup
                            {
                                Week = group.Key,
                                Count = group.Count()
                            });

                    closedIssuesByDate = closedIssues.GroupBy(issue => issue.ClosedAt!.Value.EndOfWeek())
                            .Select(group => new IssueGroup
                            {
                                Week = group.Key,
                                Count = group.Count()
                            });

                    labels = issues.SelectMany(issue => issue.Labels).Select(label => label.Name!).Distinct();

                    labelGroups = issues.SelectMany(issue => issue.Labels)
                                        .GroupBy(label => label, new LabelComparer())
                                        .Select(group => new LabelGroup { Label = Regex.Replace(group.Key.Name!, ":\\w+:", ""), Color = $"#{group.Key.Color}", Count = group.Count() })
                                        .Where(group => group.Label != "area-blazor")
                                        .OrderByDescending(group => group.Count)
                                        .Take(5);

                    labelColors = labelGroups.Select(label => label.Color);

                    openByGroups = issues.GroupBy(issue => issue.User.Login)
                                        .Select(group => new UserGroup { Name = group.Key, Count = group.Count() })
                                        .OrderByDescending(group => group.Count)
                                        .Take(7);

                    mostActiveMember = issues.SelectMany(issue => issue.Assignees)
                                        .GroupBy(assignee => assignee, new UserComparer())
                                        .Select(group => new { User = group.Key, Count = group.Count() })
                                        .OrderByDescending(group => group.Count)
                                        .Select(group => group.User)
                                        .FirstOrDefault()!;

                    users = issues.Select(issue => issue.User)
                                .Distinct(new UserComparer());

                    error = null!;
                    fetchingData = false;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }

                StateHasChanged();
            }
        }
        }
}
