namespace Interpreter;

public static class Parser
{
    public static IEnumerable<(Tokens, object?)> ParseTokens(string input)
    {
        int position = 0;
        while (position < input.Length)
        {
            char c = input[position];

            if (char.IsWhiteSpace(c))
            {
                position++;
                continue;
            }

            if (char.IsLetter(c))
            {
                var start = position;
                while (position < input.Length && (char.IsLetterOrDigit(input[position]) || input[position] == '_'))
                {
                    position++;
                }

                var value = input[start..position];
                var token = value switch
                {
                    "let" => Tokens.Let,
                    "func" => Tokens.Function,
                    _ => Tokens.Identifier,
                };

                yield return (token, value);
                continue;
            }

            if (char.IsDigit(c))
            {
                var start = position;
                while (position < input.Length && char.IsDigit(input[position]))
                {
                    position++;
                }

                var value = input[start..position];
                yield return (Tokens.Integer, int.Parse(value));
                continue;
            }

            if (c == '\"')
            {
                position++; // skip opening
                var start = position;

                while (position < input.Length && input[position] != '\"')
                {
                    position++;
                }

                if (position >= input.Length)
                {
                    throw new Exception($"Unterminated string");
                }

                var value = input[start..position];
                position++; // skip closing "
                yield return (Tokens.String, value);
                continue;
            }

            yield return c switch
            {
                '=' => (Tokens.Equal, '='),
                '+' => (Tokens.Plus, '+'),
                ',' => (Tokens.Comma, ','),
                ';' => (Tokens.Semicolon, ';'),
                '(' => (Tokens.LParentheses, '('),
                ')' => (Tokens.RParentheses, ')'),
                '{' => (Tokens.LCurlyBraces, '{'),
                '}' => (Tokens.RCurlyBraces, '}'),
                _ => (Tokens.Illegal, c),
            };

            position++;
        }

        yield return (Tokens.Eof, null);
    }

    public static void ColorMyPencils(string input)
    {
        try
        {
            int position = 0;
            while (position < input.Length)
            {
                char c = input[position];

                // check for whitespace
                if (char.IsWhiteSpace(c))
                {
                    position++;
                    Console.Write(c);
                    continue;
                }

                // check for letters (keywords, identifiers)
                if (char.IsLetter(c))
                {
                    var start = position;
                    while (position < input.Length && (char.IsLetterOrDigit(input[position]) || input[position] == '_'))
                    {
                        position++;
                    }

                    var value = input[start..position];
                    var token = value switch
                    {
                        "let" => Tokens.Let,
                        "func" => Tokens.Function,
                        "return" => Tokens.Return,
                        _ => Tokens.Identifier,
                    };

                    Console.ForegroundColor = token switch
                    {
                        Tokens.Let => ConsoleColor.Green,
                        Tokens.Function => ConsoleColor.Blue,
                        Tokens.Identifier => ConsoleColor.Yellow,
                        Tokens.Return => ConsoleColor.Blue,
                        _ => ConsoleColor.White,
                    };

                    Console.Write(value);
                    Console.ResetColor();
                    continue;
                }

                // check for digits
                if (char.IsDigit(c))
                {

                    var start = position;
                    while (position < input.Length && char.IsDigit(input[position]))
                    {
                        position++;
                    }

                    var value = input[start..position];
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(value);
                    Console.ResetColor();

                    continue;
                }

                // check for strings
                if (c == '\"')
                {
                    var start = position;
                    position++; // skip the opening "
                    while (position < input.Length && input[position] != '\"')
                    {
                        position++;
                    }

                    if (position >= input.Length)
                    {
                        throw new Exception($"Unterminated string");
                    }

                    var value = input[start..position];
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(value + input[position]); // include the closing "
                    Console.ResetColor();

                    position++; // skip the closing "
                    continue;
                }

                Console.ForegroundColor = c switch
                {
                    '=' => ConsoleColor.Magenta,
                    '+' => ConsoleColor.Magenta,
                    ',' => ConsoleColor.Magenta,
                    ';' => ConsoleColor.Magenta,
                    '(' => ConsoleColor.Magenta,
                    ')' => ConsoleColor.Magenta,
                    '{' => ConsoleColor.Magenta,
                    '}' => ConsoleColor.Magenta,
                    _ => ConsoleColor.Red,
                };

                Console.Write(c);
                Console.ResetColor();
                position++;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{ex.Message}");
            Console.ResetColor();
        }
    }
}
