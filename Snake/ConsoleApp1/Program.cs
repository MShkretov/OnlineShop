using System.Text;

Dictionary<string, string> contests = new Dictionary<string, string>();

while (true)
{
    string contestInput = Console.ReadLine();

    if (contestInput.ToLower() == "end of contests")
    {
        break;
    }

    string[] contestParts = contestInput.Split(":");
    string contestName = contestParts[0];
    string contestPassword = contestParts[1];


    if (!contests.ContainsKey(contestName))
    {
        contests.Add(contestName, contestPassword);
    }
}

Dictionary<string, Dictionary<string, int>> students = new Dictionary<string, Dictionary<string, int>>();

while (true)
{
    string submissions = Console.ReadLine();

    if (submissions.ToLower() == "end of submissions")
    {
        break;
    }

    string[] submissionParts = submissions.Split("=>");
    string contestName = submissionParts[0];
    string contestPassword = submissionParts[1];
    string username = submissionParts[2];
    int points = int.Parse(submissionParts[3]);

    if (contests.ContainsKey(contestName) && contests[contestName].Contains(contestPassword))
    {
        if (!students.ContainsKey(username))
        {
            students.Add(username, new Dictionary<string, int>());
            students[username].Add(contestName, points);
        }
        else if (!students[username].ContainsKey(contestName))
        {
            students[username].Add(contestName, points);
            students[username][contestName] = points;
        }
        else if (students[username].ContainsKey(contestName))
        {
            if (points > students[username][contestName])
            {
                students[username][contestName] = points;
            }
        }
    }
}

int maxSum = 0;
string bestCandidate = string.Empty;

foreach (var student in students)
{
    int sum = 0;

    foreach (var studentContest in student.Value)
    {
        sum += studentContest.Value;
    }

    if (sum > maxSum)
    {
        bestCandidate = student.Key;
        maxSum = sum;
    }
}

StringBuilder stringBuilder = new StringBuilder();

stringBuilder.AppendLine($"Best candidate is {bestCandidate} with total {maxSum} points.");
stringBuilder.AppendLine("Ranking: ");

foreach (var student in students.OrderBy(x => x.Key))
{
    stringBuilder.AppendLine(student.Key);

    foreach (var studentContest in student.Value.OrderByDescending(x => x.Value))
    {
        stringBuilder.AppendLine($"#  {studentContest.Key} -> {studentContest.Value}");
    }
}

Console.WriteLine(stringBuilder.ToString().Trim());