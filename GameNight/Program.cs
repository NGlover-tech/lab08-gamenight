/*
* Name: Nicholas Glover
* Course: CSCI 1250, Section 002
* Assignment: Lab 08, Game Night
* Date: November 18, 2026
* Description: Keeps a game night leaderboard in three text files. Shows the
* season so far, records tonight's points, saves the new totals,
* and adds tonight's result to a history that grows every week.
*/



//Constant for the players, points, and history files to be related back to.
const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20; 

// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

//Variables for figuring out the winner with their points.
int tonightWinner = 0;
int tonightWinnerPoints = -1; 

//Variable that reads the players.txt and points.txt and puts the information in two arrays.
string[] line_players = File.ReadAllLines(PLAYERS_FILE);
string[] line_points = File.ReadAllLines(POINTS_FILE);

//PART 3 of telling which night it is.
Console.WriteLine("=== GAME NIGHT LEADERBOARD ===");

//Reads all information in history.txt and puts it into an array. 
string[] history = File.ReadAllLines(HISTORY_FILE);

//Variable for telling which night it is.

int tonightNumber = history.Length + 1;
if (history.Length == 0)
{
    Console.WriteLine("No nights played yet.");
}
else
{
    Console.WriteLine($"Nights played: {history.Length}");

    // Foreach: Every night finishes equals a finished line that needs to be printed,
    // which means there is no need for an index. 
    foreach ( string night in history)
    {
        Console.WriteLine($"  {night}");
    }
}
    Console.WriteLine($"Tonight is Night {tonightNumber}.");

// Start of PART 1
System.Console.WriteLine("PLAYER     POINTS");

//for: Needed to compile the arrays of points and players in the exact order given to pair the player to the respective points,
// so an index is needed.
for (int i = 0; i < line_players.Length; i++)
{
    string players = (line_players[i]);
    int points = Convert.ToInt32(line_points[i]);
    Console.WriteLine($"{players.PadRight(11)}{points}");
}

// Method calls for TotalPoints and Leader Index
int seasonTotal = TotalPoints(line_points);
int leaderPosition = LeaderIndex(line_points);

//Output of part 1 that prints the leader with their points and the season's total points.
Console.WriteLine($"Leader: {line_players[leaderPosition]} with {line_points[leaderPosition]}");
Console.WriteLine($"Season total: {seasonTotal}");



// Start of PART 2
// For: Need to compile all of tonight's scores in a specific order to be with their respective player.
for (int i = 0; i < line_players.Length; i++)
{
    int nightScore = 0;
    Console.Write($"Points tonight for {line_players[i]}: ");
    nightScore = Convert.ToInt32(Console.ReadLine());

    //While: We can't know how many times a person will input a number outside of the range so it keeps asking until the input is in the set range.
    while (nightScore < 0 || nightScore > MAX_POINTS)
    {
        Console.WriteLine($"Points must be from 0 to {MAX_POINTS}.");
        Console.Write($"Points tonight for {line_players[i]}: ");
        nightScore = Convert.ToInt32(Console.ReadLine());
    }
    int season = Convert.ToInt32(line_points[i]) + nightScore;
    line_points[i] = season.ToString();
    if(nightScore > tonightWinnerPoints)
    {
        tonightWinnerPoints = nightScore;
        tonightWinner = i;
    }
}

// Output for part 2 that prints tonight's winner with their points and tells that the points and history in their respective files have been updated for this night.
Console.WriteLine($"Tonight's winner: {line_players[tonightWinner]} with {tonightWinnerPoints}");
// Second half of PART 3: Method call to show the winner for tonight with their points.
string [] entry = {HistoryEntry(tonightNumber, line_players[tonightWinner], tonightWinnerPoints)};
// Adds the information from entry into history.txt as an array.
File.AppendAllLines(HISTORY_FILE, entry);
// Writes the new points into the points.txt.
File.WriteAllLines(POINTS_FILE, line_points);
Console.WriteLine("Saved points.txt.");
// Print telling the user that the information from entry is being saved on history.txt.
Console.WriteLine($"Added to history.txt: {entry[0]}");

// Method that tracks points from each player and updates the leader if a higher score is read,
// and returns the position associated of the array with the person that is the leader.
static int LeaderIndex(string[] points)
{
    int indexLeader = 0;
    int bestScore = Convert.ToInt32(points[0]);
    //for: Needed to compile the arrays of points and players in the exact order given to pair the player to the respective points to determine the position of the leader in the array,
    // so an index is needed. 
     for (int i = 1; i < points.Length; i++)
     {
            int  current = Convert.ToInt32(points[i]);
            if (current > bestScore)
        {
            bestScore = current;
            indexLeader = i;
        }
     }
     return indexLeader;
}

// Method that adds all the points from each player and returns the sum of points.
static int TotalPoints(string[] points)
{
    int sum = 0;
    // Foreach: Every score is a line that needs to be taken into account so all points are added to the sum,
    // so there is no need for an index
    foreach(string score in points)
    {
        int amount = Convert.ToInt32(score);
        sum = sum + amount;
    }

    return sum;
}

// Returns one finished line tonights history.
static string HistoryEntry(int night, string winner, int points)
{
   return $"Night {night}: {winner} won with {points} points";
}
