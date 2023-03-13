namespace SimpleScanner;

// TODO: Handle more exceptions
// Currently only handles unexpected characters
// Should probably handle invalid tokens as well (?)


public static class SimpleScanner
{
    // Function to handle multiple digit numbers
    private static int MatchNumber(TextReader input, char first, List<Token> output)
    {
        var wholeNumber = first.ToString();
        while(input.Peek() != -1 && char.IsDigit((char)input.Peek()))
        {
            wholeNumber += (char)input.Read();
        }
        output.Add(new Token(TokenType.Number, wholeNumber));
        return wholeNumber.Length;
    }

    // The main function of the scanner
    private static List<Token> Scan(TextReader input)
    {
        var output = new List<Token>();
        var idx = 0;
        int nextInput;
        while ((nextInput = input.Read()) != -1)
        {
            var nextChar = (char)nextInput;
            // Skip whitespace
            if(nextChar == ' ') continue;
            // The end of the input character
            if (nextChar == 'r') return output;
            switch (nextChar)
            {
                case '+':
                    output.Add(new Token(TokenType.Plus, null)); idx += 1;
                    break;
                case '-':
                    output.Add(new Token(TokenType.Minus, null)); idx += 1;
                    break;
                case '*':
                    output.Add(new Token(TokenType.Multiplication, null)); idx += 1;
                    break;
                case '/':
                    output.Add(new Token(TokenType.Division, null)); idx += 1;
                    break;
                case '(':
                    output.Add(new Token(TokenType.BracketOpen, null)); idx += 1;
                    break;
                case ')':
                    output.Add(new Token(TokenType.BracketClose, null)); idx += 1;
                    break;
                case '0': case '1': case '2': case '3': case '4': case '5': case '6': case '7': case '8': case '9':
                    idx += MatchNumber(input, nextChar, output);
                    break;
                default:
                    throw new Exception("Unexpected character: " + nextChar + " at position " + idx + ".");
            }
        }

        return output;
    }

    public static int Main(string[] args)
    {
        var input = Console.In;
        var output = Scan(input);
        foreach (var token in output)
        {
            Console.WriteLine(token);
        }
        return 1;
    }
}