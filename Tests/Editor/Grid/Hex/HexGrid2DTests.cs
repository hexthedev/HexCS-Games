using NUnit.Framework;
using HexCS.Core;
using HexCS.Games;

namespace HexCSTests.Games
{
    [TestFixture]
    public class HexGrid2DTest
    {
        [Test]
        public void Works()
        {
            // Arrange
            HexGrid2D grid = new HexGrid2D(new DiscreteVector2(2, 3));

            // Act
            SHexCoordinate[,] expected = new SHexCoordinate[2, 3]
            {
                { new SHexCoordinate(0,0), new SHexCoordinate(1, 0), new SHexCoordinate(2, -1) },
                { new SHexCoordinate(0,1), new SHexCoordinate(1, 1), new SHexCoordinate(2, 0) }
            };

            // Assert
            for(int x = 0; x < 2; x++)
            {
                for(int y = 0; y < 3; y++)
                {
                    Assert.That(grid.Grid[x,y] == expected[x, y]);
                }
            }
        }
    }
}
