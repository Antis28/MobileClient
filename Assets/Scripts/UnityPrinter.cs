using System.Text;
using ConsoleForUnity;
using ThreadViewHelper;

public class UnityPrinter : IThreadPrinter
{
    public void Print(string message, char borderChar = ' ')
    {
        var cb = new StringBuilder();
        cb.Append(new string('*', 10));
        cb.Append("\n" + message + "\n");
        cb.Append(new string('*', 10));

        ConsoleInTextView.LogInText(cb.ToString());
    }
}
