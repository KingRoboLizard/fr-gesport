string line;
string rightAnswer = "";
string answer;


start:
int score = 0;
int maxScore = 0;
Console.WriteLine("Skriv 'help' för kommand eller tryck 'Enter' för att starta frågesporten.");
var contents = new StreamReader("questions.csv");
input();
answer = "";

//Läser csv filen och skriver ut frågor och svar
line = contents.ReadLine();
while (!contents.EndOfStream)
{
    line = contents.ReadLine();
    string[] lines = line.Split(",");
    while (answer == "")
    {
        Console.Clear();
        for (var i = 0; i < 5; i++)
        {
            if (i == 4)
            {
                rightAnswer = lines[i].Trim().ToLower();
            }
            else if (i == 0)
            {
                Console.WriteLine(lines[i].Trim());
            }
            else
            {
                Console.WriteLine($"({i}) " + lines[i].Trim());
            }
        }

        Console.WriteLine("Vad är ditt svar?");
        input();
    }

    if (answer == rightAnswer)
    {
        score++;
        Console.WriteLine("Rätt svar!");
    }
    else
    {
        Console.WriteLine("Fel.");
    }
    maxScore++;
    input();
    Console.Clear();
}

//Slutet av spelet, kollar ens score.
if (score == maxScore)
{
    Console.WriteLine($"Du fick rätt på alla frågor! ({maxScore}st)");
}
else
{
    Console.WriteLine($"Du fick {score} utav {maxScore} frågor rätt!");
}
Console.WriteLine("Vill du spela igen? y/n");

answer = Console.ReadLine().ToLower();
if (answer == "y")
{
    Console.Clear();
    contents.Close();
    goto start;
}

//Läser ens input och bestämmer vad som ska hända 
void input()
{
    answer = Console.ReadLine().ToLower().Trim();
    if (answer == "help")
    {
        Console.WriteLine("'Help' lista av alla kommand\n'exit' avslutar programmet\n'score' visar hur många poäng du har\n'edit' för att redigera frågorna\n\nTryck enter för att forsätta");
        answer = "";
        input();
    }
    else if (answer == "exit")
    {
        Environment.Exit(0);
    }
    else if (answer == "score")
    {
        Console.WriteLine($"Du har {score} utav {maxScore} poäng hittills.\n\nTryck enter för att forsätta");
        answer = "";
        input();
    }
    else if (answer == "edit")
    {
        contents.Close();
        editMode();
        contents = new StreamReader("questions.csv");
    }
}

//Läser hela filen till en string
void editMode()
{
    var contents = new StreamReader("questions.csv");
    line = contents.ReadToEnd() + "\n";
    contents.Close();
    var editor = new StreamWriter("questions.csv");
    while (answer != "")
    {
        Console.Clear();
        Console.WriteLine("entered edit mode\nWrite the question and answers separated by a comma(,) and then which anwer is correct.\nWhen done writing the question press enter to submit it.\n'removeline' to remove the last question, and press 'enter' with nothing written to exit edit mode.\nExiting without saving will delete all questions.\n");
        Console.WriteLine(line);
        answer = Console.ReadLine();
        if (answer == "removeline")
        {
            if (!string.IsNullOrWhiteSpace(line))   //stoppar dig från att krasha programmet genom att ta bort mer än det finns
            {
                line = line.Remove(line.TrimEnd().LastIndexOf("\n"));
                line += "\n";
            }
        }
        else if (answer != "")
        {
            line += answer.Trim();
        }
    }
    editor.Write(line.TrimEnd());
    editor.Close();
}