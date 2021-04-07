using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake_9000;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_9000.Tests
{
    [TestClass()]
    public class AiTests
    {
        [TestMethod()]
        // Testar så Ain vet åt vilket håll han ska åka beroende på var maten är.
        public void AiFindingFoodTest()
        {
            GameWorld world = new GameWorld(60, 40, true);
            world.Food = new Food('ð', 0, 0);
            world.Ai = new Ai('x', 10, 45);

            world.Food.Position.X = 20;
            world.Food.Position.Y = 45;

            world.Ai.Dir = Direction.None;
            world.Ai.AiFindingFood(world.Food);

            Assert.AreEqual(world.Ai.Dir, Direction.Right);


            world.Ai.Dir = Direction.None;

            world.Food.Position.X = 5;
            world.Food.Position.Y = 45;

            world.Ai.AiFindingFood(world.Food);

            Assert.AreEqual(world.Ai.Dir, Direction.Left);
        }
    }
}

namespace Snake_9000
{
    [TestClass()]
    public class GameWorldTests
    {
        [TestMethod()]
        // Testar om det går att skapa en värld i rätt storlek.
        public void TestGameWorldSize()
        {
            GameWorld world = new GameWorld(60, 40, true);

            Assert.AreEqual(60, world.GameWidth);
            Assert.AreEqual(40, world.GameHeight);
        }

        [TestMethod()]
        // Om Aibool är true ska det finnas en Ai, annars inte.
        public void CreateAiTest()
        {
            GameWorld world = new GameWorld(60, 40, true);
            Assert.IsNotNull(world.Ai);

            GameWorld world2 = new GameWorld(60, 40, false);
            Assert.IsNull(world2.Ai);
        }

        [TestMethod()]
        // Världen ska innehålla 4 väggar till en början.
        public void CreateWallTest()
        {
            GameWorld world = new GameWorld(60, 40, true);
            Assert.AreEqual(4, world.Walls.Count);
        }

        [TestMethod()]
        // Det ska inte gå att skapa en vägg i en spelare och det testar vi här.
        public void CheckWallsPlayerTest()
        {
            GameWorld world = new GameWorld(60, 40, true);

            foreach (var w in world.Walls)
            {
                Assert.AreNotEqual(world.Player.Position, w);
            }

            world.Player.Position.X = 20;
            world.Player.Position.Y = 30;

            world.Walls.Add(new Walls('|', 20, 30));

            foreach (var w in world.Walls)
            {
                Assert.AreNotEqual(world.Player.Position, w);
            }
        }

        [TestMethod()]
        // Kollar Ains alla olika sätt, så Ain faktiskt renderar en slumpvald position
        // och inte en position där ain står/spelare/svans eller liknande.
        public void GameConditionsAiTest()
        {
            GameWorld world = new GameWorld(60, 40, true);

            foreach (var w in world.Walls)
            {
                Assert.AreNotEqual(world.Ai.Position, w);
            }

            world.Ai.Position.X = 30;
            world.Ai.Position.Y = 40;

            world.Walls.Add(new Walls('|', 30, 40));

            foreach (var w in world.Walls)
            {
                Assert.AreNotEqual(world.Ai.Position, w);
            }

            world.Ai.Tail.Add(new Tail('o', 30, 40, ConsoleColor.Cyan));

            foreach (Tail t in world.Ai.Tail)
            {
                Assert.AreNotEqual(world.Ai.Position, t);
            }

            Player player = new Player('x', 20, 40);
            player.Tail.Add(new Tail('x', 20, 40, ConsoleColor.Red));

            foreach (Tail t in player.Tail)
            {
                Assert.AreNotEqual(world.Ai.Position, t);
            }
        }

        [TestMethod()]
        // Detta ska inte gå för du ska inte kunna skapa mat eller en spelare innuti varandra.
        public void EatFoodPlayerTest()
        {
            GameWorld world = new GameWorld(60, 40, false);
            world.Food.Position.X = 30;
            world.Food.Position.X = 40;

            world.Player.Position.X = 20;
            world.Player.Position.Y = 40;
            Assert.AreEqual(0, world.Player.Poäng);

            world.Player.Position.X = 30;
            world.Player.Position.Y = 40;
            Assert.AreNotEqual(1, world.Player.Poäng);

        }

        [TestMethod()]
        // Samma som ovan
        public void EatFoodAiTest()
        {
            GameWorld world = new GameWorld(60, 40, true);
            world.Food.Position.X = 20;
            world.Food.Position.X = 30;

            world.Ai.Position.X = 20;
            world.Ai.Position.Y = 40;
            Assert.AreEqual(0, world.Player.Poäng);

            world.Ai.Position.X = 20;
            world.Ai.Position.Y = 30;
            Assert.AreNotEqual(1, world.Ai.Poäng);
        }

        [TestMethod()]
        // Testar så att spelaren hamnar på andra sidan consolen då den är på kanten av consolen.
        public void PlayerMovingThroughEdgeTest()
        {
            GameWorld world = new GameWorld(60, 40, false);

            world.Player.Position.X = 2;
            world.Player.Position.Y = 30;
            world.Player.Dir = Direction.Left;

            world.Player.Update();
            world.PlayerMovingThroughEdge();

            Assert.AreEqual(world.GameWidth - 1, world.Player.Position.X);
        }

        [TestMethod()]
        // Testar så att ain hamnar på andra sidan consolen då den är på kanten av consolen.
        public void AiMovingThroughWallsTest()
        {
            GameWorld world = new GameWorld(60, 40, false);
            Food Food = new Food('ð', 0, 0);
            List<Walls> Walls = new List<Walls>();

            world.Ai = new Ai('►', 58, 30);
            world.Ai.Dir = Direction.Right;

            world.Ai.FoodUpdate(Food, Walls);
            world.AiMovingThroughWalls();

            Assert.AreEqual(1, world.Ai.Position.X);
        }

        [TestMethod()]
        // Testar så att svårighetsgraden ökar när man tar mat och får över en viss poäng (3).
        public void HarderDifficultyTest()
        {
            GameWorld world = new GameWorld(60, 40, false);
            List<GameObject> GameObj = new List<GameObject>();

            Food Food = new Food('ð', 15, 30);
            GameObj.Add(Food);

            world.Player.Poäng = 3;

            world.Food.Position.X = 15;
            world.Food.Position.Y = 30;

            Assert.AreEqual(15, world.Food.Position.X);
            Assert.AreEqual(30, world.Food.Position.Y);

            world.Player.Poäng++;
            world.HarderDifficultyTEST();

            Assert.AreNotEqual(15, world.Food.Position.X);
            Assert.AreNotEqual(30, world.Food.Position.Y);

            Assert.AreEqual(4, world.Player.Poäng);
        }
    }
}