using System.Text;

namespace Snake.Engine;

public class GameEngine
{
    private readonly (int rows, int cols) _mapSize;
    private readonly List<(int x, int y)> _snake;

    public GameEngine((int rows, int cols) mapSize)
    {
        _mapSize = mapSize;
        _snake = InitSnake();
    }

    public void Run()
    {
        PrintBoard();
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
    }
}