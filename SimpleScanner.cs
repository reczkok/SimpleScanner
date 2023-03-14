namespace SimpleScanner;

public static class SimpleScanner
{
    private static void CheckValidity(TextReader input, char first, int idx)
    {
        switch (first)
        {
            case '+': case '*': case '-': case '/':
                if (input.Peek() == -1 || input.Peek() == 114)
                    throw new Exception("Inavlid end of input");
                if (!(char.IsDigit((char)input.Peek()) || (char)input.Peek() == '('))
                    throw new Exception("Invalid token: " + (char)input.Peek() + " at idx: " + (idx+1));
                break;
            case '(':
                if (input.Peek() == -1 || input.Peek() == 114)
                    throw new Exception("Inavlid end of input");
                if(!(char.IsDigit((char)input.Peek()) || (char)input.Peek() == '-'))
                    throw new Exception("Invalid token: " + (char)input.Peek() + " at idx: " + (idx+1));
                break;
            case ')':
                if (input.Peek() == -1 || input.Peek() == 114)
                    break;
                if(char.IsDigit((char)input.Peek()))
                    throw new Exception("Invalid token: " + (char)input.Peek() + " at idx: " + (idx+1));
                break;
        }
    }
    // Function to handle multiple digit numbers
    private static int MatchNumber(TextReader input, char first, List<Token> output, int idx)
    {
        var wholeNumber = first.ToString();
        while(input.Peek() != -1 && char.IsDigit((char)input.Peek()))
        {
            wholeNumber += (char)input.Read();
        }
        output.Add(new Token(TokenType.Number, wholeNumber));
        if ((char)input.Peek() == '(')
            throw new Exception("Invalid token: " + (char)input.Peek() + " at idx: " + (idx + wholeNumber.Length));
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
                    CheckValidity(input, nextChar, idx);
                    break;
                case '-':
                    output.Add(new Token(TokenType.Minus, null)); idx += 1;
                    CheckValidity(input, nextChar, idx);
                    break;
                case '*':
                    output.Add(new Token(TokenType.Multiplication, null)); idx += 1;
                    CheckValidity(input, nextChar, idx);
                    break;
                case '/':
                    output.Add(new Token(TokenType.Division, null)); idx += 1;
                    CheckValidity(input, nextChar, idx);
                    break;
                case '(':
                    output.Add(new Token(TokenType.BracketOpen, null)); idx += 1;
                    CheckValidity(input, nextChar, idx);
                    break;
                case ')':
                    output.Add(new Token(TokenType.BracketClose, null)); idx += 1;
                    CheckValidity(input, nextChar, idx);
                    break;
                case '0': case '1': case '2': case '3': case '4': case '5': case '6': case '7': case '8': case '9':
                    idx += MatchNumber(input, nextChar, output, idx);
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
