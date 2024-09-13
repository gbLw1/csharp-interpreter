using Interpreter;

const string input = """
let five = 5;
let ten = 10;
let str = "Hello, World!";

let add = func(x, y) {
  return x + y;

let result = add(five, ten);
""";

var tokens = Parser.ParseTokens(input);

foreach (var token in tokens)
{
    Console.WriteLine(token);
}

Parser.ColorMyPencils(input);