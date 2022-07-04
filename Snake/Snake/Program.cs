using Snake;

Position[] directions = new Position[]
{
    new Position(0, 1),
    new Position(0, -1),
    new Position(1, 0),
    new Position(-1, 0)
};

Position direction = new Position(0, 1);

Queue<Position> snakeElements = new Queue<Position>();

for (int i = 0; i < 4; i++)
{
    snakeElements.Enqueue(new Position(0, i));
}

foreach (var position in snakeElements)
{
    Console.SetCursorPosition(position.Col, position.Row);
    Console.Write("*");
}

Random randomGenerator = new Random();

Position food = new Position(randomGenerator.Next(0, Console.WindowHeight), randomGenerator.Next(0, Console.BufferWidth));
Console.SetCursorPosition(food.Col, food.Row);
Console.Write("@");

while (true)
{
    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo userInput = Console.ReadKey();

        if (userInput.Key == ConsoleKey.UpArrow)
        {
            if (direction != directions[2]) { direction = directions[3]; }
        }
        else if (userInput.Key == ConsoleKey.DownArrow)
        {
            if (direction != directions[3]) { direction = directions[2]; }
        }
        else if (userInput.Key == ConsoleKey.LeftArrow)
        {
            if (direction != directions[0]) { direction = directions[1]; }
        }
        else if (userInput.Key == ConsoleKey.RightArrow)
        {
            if (direction != directions[1]) { direction = directions[0]; }
        }
    }

    Position snakeHead = snakeElements.Last();
    Position snakeNewHead = new Position(snakeHead.Row + direction.Row, snakeHead.Col + direction.Col);

    if (snakeNewHead.Row < 0) snakeNewHead.Row = Console.WindowHeight - 1;
    if (snakeNewHead.Row > Console.WindowHeight - 1) snakeNewHead.Row = 0;
    if (snakeNewHead.Col < 0) snakeNewHead.Col = Console.WindowWidth - 1;
    if (snakeNewHead.Col > Console.WindowWidth - 1) snakeNewHead.Col = 0;


    if (snakeElements.Contains(snakeNewHead))
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Game over!");
        Console.WriteLine("Press any key for end!");
        Console.ReadLine();
        return;
    }

    snakeElements.Enqueue(snakeNewHead);
    Console.SetCursorPosition(snakeNewHead.Col, snakeNewHead.Row);
    Console.Write("*");

    if (snakeNewHead.Col == food.Col && snakeNewHead.Row == food.Row)
    {
        do
        {
            food = new Position(randomGenerator.Next(0, Console.WindowHeight), randomGenerator.Next(0, Console.WindowWidth));
        } while (snakeElements.Contains(food));
    }
    else
    {
        Position last = snakeElements.Dequeue();
        Console.SetCursorPosition(last.Col, last.Row);
        Console.Write(" ");
    }

    Console.SetCursorPosition(food.Col, food.Row);
    Console.Write("@");

    Thread.Sleep(100);
}