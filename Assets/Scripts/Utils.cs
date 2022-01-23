using System.Collections.Generic;

public static class Utils
{
    public static IEnumerable<(int, int)> GetNeighbors(int r, int c)
    {
        foreach ((int, int) delta in new List<(int, int)> { (0, 1), (0, -1), (1, 0), (-1, 0) })
        {
            int newR = r + delta.Item1;
            int newC = c + delta.Item2;
            if (newR >= 0 && newR < GameController.gridSize && newC >= 0 && newC < GameController.gridSize)
            {
                yield return (newR, newC);
            }
        }
    }
}