using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using lusers_game;
using Microsoft.Xna.Framework;

namespace lusers_game.test
{
    [TestClass]
    public class TestCharacters
    {
        [TestMethod]
        public void TestCEO()
        {
            Characters.CEO c = new Characters.CEO(new Vector2(0, 0));
        }
    }
}
