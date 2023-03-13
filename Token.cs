namespace SimpleScanner;

public class Token
{
    private readonly TokenType _type;
    private readonly string? _text;

    public Token(TokenType t, string? text)
    {
        _type = t;
        _text = text;
    }

    public override string ToString()
    {
        return _text is null ? new string(_type.ToString()) : new string(_type + " " + _text);
    }
}