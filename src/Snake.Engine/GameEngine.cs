using System.Text;

namespace Snake.Engine;

public class GameEngine
{
    private readonly (int rows, int cols) _mapSize;
    private readonly List<(int x, int y)> _snake;
    private bool _gameEnded;

    public GameEngine((int rows, int cols) mapSize)
    {
        _mapSize = mapSize;
        _snake = InitSnake();
        _gameEnded = false;
    }

    public void Run()
    {
        PrintBoard();
        while (!_gameEnded)
        {
            Console.Write("Move: ");
            var move = GetMove();
            ApplyMove(move);

            _gameEnded = IsTouchingBoundary();
            PrintBoard();
        }
    }

    private void ApplyMove((int x, int y) move)
    {
        var oldHead = _snake.Last();
        var newHead = (oldHead.x + move.x, oldHead.y + move.y);

        _snake.Add(newHead);
        _snake.RemoveAt(0);
    }

    private (int x, int y) GetMove()
    {
        return Console.ReadLine()!.Trim().ToLower()[0] switch
        {
            'w' => (0, -1),
            's' => (0, 1),
            'a' => (-1, 0),
            'd' => (1, 0),
            _ => (0, 0)
        };
    }

    private bool IsTouchingBoundary()
    {
        var head = _snake.First();

        return head.x < 0
               || head.x >= _mapSize.cols
               || head.y < 0
               || head.x >= _mapSize.rows
               || _snake.GroupBy(tuple => tuple).Any(group => group.Count() > 1);
    }

    private List<(int x, int y)> InitSnake()
    {
        return new List<(int x, int y)>
        {
            (0, 0),
            (1, 0)
        };
    }

    private void PrintBoard()
    {
        var map = Enumerable.Range(0, _mapSize.rows)
            .Select(i => new string('.', _mapSize.cols))
            .Select(s => new StringBuilder(s))
            .ToList();

        foreach (var segment in _snake)
            map[segment.y][segment.x] = '#';

        Console.WriteLine(string.Join("\n", map));
        Console.WriteLine("\n");
    }
}