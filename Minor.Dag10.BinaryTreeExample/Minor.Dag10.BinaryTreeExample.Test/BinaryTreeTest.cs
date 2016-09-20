using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minor.Dag10.BinaryTreeExample.Test
{
    [TestClass]
    public class BinaryTreeTest
    {    
        [TestMethod]
        public void BinaryTreeDepthIs0()
        {
            //Act
            var t1 = BinaryTree.Empty;
            //Assert
            Assert.AreEqual(0, t1.Depth);
        }

        [TestMethod]
        public void BinaryTreeDepthIs1()
        {
            //Arrange
            var target = BinaryTree.Empty;

            //Act
            target = target.Add(10);

            //Assert
            Assert.AreEqual(1, target.Depth);
        }

        [TestMethod]
        public void BinaryTreeDepthIs2()
        {
            //Arrange
            var target = BinaryTree.Empty;

            //Act
            target = target.Add(10);
            target = target.Add(1);

            //Assert
            Assert.AreEqual(2, target.Depth);
        }

        [TestMethod]
        public void BinaryTreeDepthIs2With2Branches()
        {
            //Arrange
            var target = BinaryTree.Empty;

            //Act
            target = target.Add(10);
            target = target.Add(1);
            target = target.Add(12);


            //Assert
            Assert.AreEqual(2, target.Depth);
        }

        [TestMethod]
        public void BinaryTreeCountIs0()
        {
            //Act
            var t1 = BinaryTree.Empty;
            //Assert
            Assert.AreEqual(0, t1.Count);
        }

        [TestMethod]
        public void BinaryTreeCountIs1()
        {
            //Arrange
            var target = BinaryTree.Empty;

            //Act
            target = target.Add(10);

            //Assert
            Assert.AreEqual(1, target.Count);
        }

        public void BinaryTreeDepthIs3()
        {
            //Arrange
            var target = BinaryTree.Empty;

            //Act
            target = target.Add(10);
            target = target.Add(1);
            target = target.Add(12);


            //Assert
            Assert.AreEqual(3, target.Count);
        }
    }
}
