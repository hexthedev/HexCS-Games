using NUnit.Framework;
using HexCS.Core;
using HexCS.Games;

namespace HexCSTests.Games
{
    [TestFixture]
    public class SHexCoordinateTests
    {
        [Test]
        public void Steps()
        {
            // Arrange
            SHexCoordinate t1 = SHexCoordinate.Zero;
            SHexCoordinate t2 = new SHexCoordinate(2, -4);

            // Act
            SHexCoordinate[] test1 = Tests(t1);
            SHexCoordinate[] test2 = Tests(t2);

            // Assert
            AssertCorrect(test1, t1);
            AssertCorrect(test2, t2);

            // FUNCS
            void AssertCorrect(SHexCoordinate[] steps, SHexCoordinate origin)
            {
                Assert.That(steps[0] == origin + new SHexCoordinate(1, 0));
                Assert.That(steps[1] == origin + new SHexCoordinate(0, 1));
                Assert.That(steps[2] == origin + new SHexCoordinate(-1, 1));
                Assert.That(steps[3] == origin + new SHexCoordinate(-1, 0));
                Assert.That(steps[4] == origin + new SHexCoordinate(0, -1));
                Assert.That(steps[5] == origin + new SHexCoordinate(1, -1));

            }

            SHexCoordinate[] Tests(SHexCoordinate c)
            {
                return new SHexCoordinate[]
                {
                    c.Step(SHexCoordinate.EDirection.XNegZ),
                    c.Step(SHexCoordinate.EDirection.YNegZ),
                    c.Step(SHexCoordinate.EDirection.YNegX),
                    c.Step(SHexCoordinate.EDirection.ZNegX),
                    c.Step(SHexCoordinate.EDirection.ZNegY),
                    c.Step(SHexCoordinate.EDirection.XNegY)
                };
            }
        }

        [Test]
        public void SumAbsolute()
        {
            // Arrange
            SHexCoordinate t1 = SHexCoordinate.Zero;
            SHexCoordinate t2 = new SHexCoordinate(2, 1);
            SHexCoordinate t3 = new SHexCoordinate(4, 0);

            // Act
            int ab1 = t1.SumAbsolute;
            int ab2 = t2.SumAbsolute;
            int ab3 = t3.SumAbsolute;

            // Assert
            Assert.That(ab1 == 0);
            Assert.That(ab2 == 6);
            Assert.That(ab3 == 8);
        }

        [Test]
        public void RadialStep()
        {
            // Arrange
            SHexCoordinate t1 = SHexCoordinate.Zero;
            SHexCoordinate t2 = new SHexCoordinate(2, -2);

            // Act
            SHexCoordinate[] radial1 = t1.RadialStep(2);
            SHexCoordinate[] radial2 = t2.RadialStep(1);

            // Assert
            AssertRadial(radial1,
                new SHexCoordinate(2, -2),
                new SHexCoordinate(2, -1),
                new SHexCoordinate(2, 0),
                new SHexCoordinate(1, -2),
                new SHexCoordinate(1, -1),
                new SHexCoordinate(1, 0),
                new SHexCoordinate(1, 1),
                new SHexCoordinate(0, -2),
                new SHexCoordinate(0, -1),
                new SHexCoordinate(0, 0),
                new SHexCoordinate(0, 1),
                new SHexCoordinate(0, 2),
                new SHexCoordinate(-1, -1),
                new SHexCoordinate(-1, 0),
                new SHexCoordinate(-1, 1),
                new SHexCoordinate(-1, 2),
                new SHexCoordinate(-2, 0),
                new SHexCoordinate(-2, 1),
                new SHexCoordinate(-2, 2)
            );

            AssertRadial(radial2,
                new SHexCoordinate(3, -3),
                new SHexCoordinate(3, -2),
                new SHexCoordinate(2, -3),
                new SHexCoordinate(2, -2),
                new SHexCoordinate(2, -1),
                new SHexCoordinate(1, -2),
                new SHexCoordinate(1, -1)
            );

            void AssertRadial(SHexCoordinate[] calculated, params SHexCoordinate[] contains)
            {
                Assert.That(calculated.Length == contains.Length);
                Assert.That(calculated.ContainsElements(contains));
            }
        }

        [Test]
        public void Line()
        {
            // Arrange
            SHexCoordinate h1 = SHexCoordinate.Zero;
            SHexCoordinate h2 = new SHexCoordinate(2, -2);
            SHexCoordinate h3 = new SHexCoordinate(-1, 2);

            // Act
            SHexCoordinate[] line1 = SHexCoordinate.Line(h1, h2);
            SHexCoordinate[] expectedLine1 = new SHexCoordinate[]
            {
                new SHexCoordinate(0, 0),
                new SHexCoordinate(1, -1),
                new SHexCoordinate(2, -2)
            };

            SHexCoordinate[] line2 = SHexCoordinate.Line(h2, h3);
            SHexCoordinate[] expectedLine2 = new SHexCoordinate[]
            {
                new SHexCoordinate(2, -2),
                new SHexCoordinate(1, -1),
                new SHexCoordinate(0, 0),
                new SHexCoordinate(0, 1),
                new SHexCoordinate(-1, 2)
            };

            // Assert
            Assert.That(line1.Length == expectedLine1.Length);
            for(int i = 0; i<expectedLine1.Length; i++)
            {
                Assert.That(line1[i] == expectedLine1[i]);
            }

            Assert.That(line2.Length == expectedLine2.Length);
            for (int i = 0; i < expectedLine2.Length; i++)
            {
                Assert.That(line2[i] == expectedLine2[i]);
            }
        }

        [Test]
        public void XZConstruct()
        {
            // Arrange
            SHexCoordinate coordTest = SHexCoordinate.XZConstruct(6, 9);

            // Assert
            Assert.That(coordTest.X == 6 && coordTest.Y == -15 && coordTest.Z == 9);
        }

        [Test]
        public void YZConstruct()
        {
            // Arrange
            SHexCoordinate coordTest = SHexCoordinate.YZConstruct(6, 9);

            // Assert
            Assert.That(coordTest.X == -15 && coordTest.Y == 6 && coordTest.Z == 9);
        }

        [Test]
        public void Zero()
        {
            // Arrange
            SHexCoordinate coordTest = SHexCoordinate.Zero;

            // Assert
            Assert.That(coordTest.X == 0 && coordTest.Y == 0 && coordTest.Z == 0);
        }

        [Test]
        public void Plus()
        {
            // Arrange
            SHexCoordinate c1 = new SHexCoordinate(2, 3);
            SHexCoordinate c2 = new SHexCoordinate(-2, 17);

            // Act
            SHexCoordinate test = c1 + c2;

            // Assert
            Assert.That(test.X == 0 && test.Y == 20 && test.Z == -20);
        }

        [Test]
        public void Minus()
        {
            // Arrange
            SHexCoordinate c1 = new SHexCoordinate(2, 3);
            SHexCoordinate c2 = new SHexCoordinate(-2, 17);

            // Act
            SHexCoordinate test = c1 - c2;

            // Assert
            Assert.That(test.X == 4 && test.Y == -14 && test.Z == 10);
        }

        [Test]
        public void Distance()
        {
            // Arrange
            SHexCoordinate c1 = new SHexCoordinate(-1, 3);
            SHexCoordinate c2 = new SHexCoordinate(2, 0);

            // Act
            int dist = SHexCoordinate.Distance(c1, c2);
            int distOp = SHexCoordinate.Distance(c2, c1);

            // Assert
            Assert.That(dist == distOp && dist == 3);
        }

        [Test]
        public void Equals()
        {
            Assert.That(SHexCoordinate.Zero == SHexCoordinate.Zero);
            Assert.That(new SHexCoordinate(82, -40) == new SHexCoordinate(82, -40));
            Assert.That(!(new SHexCoordinate(82, -40) == SHexCoordinate.Zero));
        }

        [Test]
        public void NotEquals()
        {
            Assert.That(!(new SHexCoordinate(82, -40) != new SHexCoordinate(82, -40)));
            Assert.That(!(SHexCoordinate.Zero != SHexCoordinate.Zero));
            Assert.That(SHexCoordinate.Zero != new SHexCoordinate(82, -40));
        }
    }
}   