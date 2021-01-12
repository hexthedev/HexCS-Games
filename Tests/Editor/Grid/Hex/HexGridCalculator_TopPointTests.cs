using NUnit.Framework;
using HexCS.Core;
using HexCS.Games;
using static HexCS.Games.HexGridCalculator_TopPoint;

namespace HexCSTests.Games
{
    [TestFixture]
    public class HexGridCalculator_TopPointTests
    {
        // inner raidus = 1, distance between hexes = 2
        private HexGridCalculator_TopPoint _calculator 
            = new HexGridCalculator_TopPoint(1); 

        [Test]
        public void HexPosition_GridCoordinate()
        {
            // Act
            Vector2 origin = _calculator.EuclidianPosition(new DiscreteVector2(0, 0));
            Vector2 up1 = _calculator.EuclidianPosition(new DiscreteVector2(0, 1));
            Vector2 up2 = _calculator.EuclidianPosition(new DiscreteVector2(0, 2));
            Vector2 right1 = _calculator.EuclidianPosition(new DiscreteVector2(1, 0));
            Vector2 diag1 = _calculator.EuclidianPosition(new DiscreteVector2(1, 1));

            // Assert
            AssertVector(origin, new Vector2(0,0));
            AssertVector(up1, new Vector2(1 * UTHexGrid.cInnerRadiusModifier, 1.5f));
            AssertVector(up2, new Vector2(0, 3f));
            AssertVector(right1, new Vector2( 2 * UTHexGrid.cInnerRadiusModifier, 0));
            AssertVector(diag1, new Vector2( 3f * UTHexGrid.cInnerRadiusModifier, 1.5f));
        }

        [Test]
        public void HexPosition_HexCoordinate()
        {
            // Act
            Vector2 origin = _calculator.EuclidianPosition(SHexCoordinate.Zero);
            Vector2 upright = _calculator.EuclidianPosition(new SHexCoordinate(1, 0));
            Vector2 right = _calculator.EuclidianPosition(new SHexCoordinate(0, 1));
            Vector2 downright = _calculator.EuclidianPosition(new SHexCoordinate(-1, 1));
            Vector2 downleft = _calculator.EuclidianPosition(new SHexCoordinate(-1, 0));
            Vector2 left = _calculator.EuclidianPosition(new SHexCoordinate(0, -1));
            Vector2 upleft = _calculator.EuclidianPosition(new SHexCoordinate(1, -1));

            // Assert
            AssertVector(origin, new Vector2(0, 0));
            AssertVector(upright, new Vector2(1 * UTHexGrid.cInnerRadiusModifier, 1.5f));
            AssertVector(right, new Vector2(2 * UTHexGrid.cInnerRadiusModifier, 0));
            AssertVector(downright, new Vector2(1 * UTHexGrid.cInnerRadiusModifier, -1.5f));
            AssertVector(downleft, new Vector2(-1 * UTHexGrid.cInnerRadiusModifier, -1.5f));
            AssertVector(left, new Vector2(-2 * UTHexGrid.cInnerRadiusModifier, 0));
            AssertVector(upleft, new Vector2(-1 * UTHexGrid.cInnerRadiusModifier, 1.5f));
        }

        [Test]
        public void OuterRadius()
        {
            Assert.That(_calculator.OuterRadius == 1f);
        }

        [Test]
        public void InnerRadius()
        {
            Assert.That(_calculator.InnerRadius == UTHexGrid.cInnerRadiusModifier);
        }

        [Test]
        public void HexCenterDistance()
        {
            Assert.That(_calculator.HexCenterDistance == UTHexGrid.cInnerRadiusModifier * 2);
        }

        [Test]
        public void HexHorizontalDistance()
        {
            Assert.That(_calculator.HexHorizontalDistance == UTHexGrid.cInnerRadiusModifier * 2);
        }

        [Test]
        public void HexVerticalDistance()
        {
            Assert.That(_calculator.HexVerticalDistance == 1.5f);
        }

        [Test]
        public void Points()
        {
            Vector2[] points = _calculator.Points;

            Assert.That(points[0] == new Vector2(0, 1));
            Assert.That(points[1] == new Vector2(UTHexGrid.cInnerRadiusModifier, 0.5f));
            Assert.That(points[2] == new Vector2(UTHexGrid.cInnerRadiusModifier, -0.5f));
            Assert.That(points[3] == new Vector2(0, -1));
            Assert.That(points[4] == new Vector2(-UTHexGrid.cInnerRadiusModifier, 0.5f));
            Assert.That(points[5] == new Vector2(-UTHexGrid.cInnerRadiusModifier, -0.5f));
        }

        [Test]
        public void EuclidianPosition_NormalCoords()
        {
            Vector2 pos1 = _calculator.EuclidianPosition(new DiscreteVector2(0,0));
            Vector2 pos2 = _calculator.EuclidianPosition(new DiscreteVector2(1, 1));

            Assert.That(pos1 == Vector2.Zero);
            Assert.That(pos2 == new Vector2(UTHexGrid.cInnerRadiusModifier * 3f, 1.5f));
        }

        [Test]
        public void EuclidianPosition_HexCoords()
        {
            Vector2 pos1 = _calculator.EuclidianPosition(SHexCoordinate.Zero);
            Vector2 pos2 = _calculator.EuclidianPosition(new SHexCoordinate(2, 1));

            Assert.That(pos1 == Vector2.Zero);
            Assert.That(pos2 == new Vector2(UTHexGrid.cInnerRadiusModifier * 4, 3f));
        }

        [Test]
        public void GetNeighbour()
        {
            HexGridCalculator_TopPoint cal = _calculator;

            SHexCoordinate coord = new SHexCoordinate(2, 1);

            Assert.That(cal.GetNeighbour(coord, EHexDirection.UP_RIGHT) == new SHexCoordinate(3, 1));
            Assert.That(cal.GetNeighbour(coord, EHexDirection.RIGHT) == new SHexCoordinate(2, 2));
            Assert.That(cal.GetNeighbour(coord, EHexDirection.DOWN_RIGHT) == new SHexCoordinate(1, 2));
            Assert.That(cal.GetNeighbour(coord, EHexDirection.DOWN_LEFT) == new SHexCoordinate(1, 1));
            Assert.That(cal.GetNeighbour(coord, EHexDirection.LEFT) == new SHexCoordinate(2, 0));
            Assert.That(cal.GetNeighbour(coord, EHexDirection.UP_LEFT) == new SHexCoordinate(3, 0));
        }

        [Test]
        public void GetNeighbours()
        {
            HexGridCalculator_TopPoint cal = _calculator;

            SHexCoordinate coord = new SHexCoordinate(2, 1);
            SHexCoordinate[] coords = cal.GetNeighbours(coord);

            Assert.That(coords[0] == new SHexCoordinate(3, 1));
            Assert.That(coords[1] == new SHexCoordinate(2, 2));
            Assert.That(coords[2] == new SHexCoordinate(1, 2));
            Assert.That(coords[3] == new SHexCoordinate(1, 1));
            Assert.That(coords[4] == new SHexCoordinate(2, 0));
            Assert.That(coords[5] == new SHexCoordinate(3, 0));
        }

        private void AssertVector(Vector2 test, Vector2 against)
        {
            Vector2 diff = test - against;
            Assert.That(diff.X < 0.1f);
            Assert.That(diff.Y < 0.1f);
        }
    }
}