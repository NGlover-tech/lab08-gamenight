
const string PLAYERS_FILE = "player.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20; 

// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

double total = 0;
int leader = 0;
double leaderIndex = 0;
string[] line_players = File.ReadAllLines(PLAYERS_FILE);
string[] line_points = File.ReadAllLines(POINTS_FILE);

System.Console.WriteLine("PLAYER     POINTS");

//for: Needed to compile the arrays of points and players in the exact order given to pair the player to the respective points.
for (int i = 0; i < line_players.Length; i++)
{
    string players = (line_players[i]);
    int points = Convert.ToInt32(line_points[i]);
    total = total + points;
    if(points > leader)
    {
        leader = (int)points;
        leaderIndex = i;
    }



Console.WriteLine($"{players.PadRight(11)} {points}");

}
Console.WriteLine($"Leader: {line_players[(int)leaderIndex]} with {leader}");
Console.WriteLine($"Season total: {total}");
/*
int finalLeader = LeaderIndex(line_points);


static int LeaderIndex(string[] points)
{
    int indexLeader = 0;

     for (int i = 1; i < points.Length; i++)
     {
            int  current_High_Score = points[i];
            if (points > leader)
        {
            leader = points;
            indexLeader = i;
        }
     }
\
}
/*
static int TotalPoints(string[] points)
{
    foreach(string[] points)
}
static string HistoryEntry(int night, string winner, int points)
{

}
*/