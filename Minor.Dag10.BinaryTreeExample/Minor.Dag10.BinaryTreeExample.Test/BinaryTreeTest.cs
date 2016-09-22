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

        [TestMethod]
        public void BinaryTreeCountIs3()
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

        [TestMethod]
        public void BinaryTreeContains()
        {
            //Arrange
            var target = BinaryTree.Empty;
            target = target.Add(10);
            target = target.Add(1);
            target = target.Add(12);

            //Act
            var result = target.Contains(12);


            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BinaryTreeContainsItemNotInTree()
        {
            //Arrange
            var target = BinaryTree.Empty;
            target = target.Add(10);
            target = target.Add(1);
            target = target.Add(12);

            //Act
            var result = target.Contains(11);


            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ForEachThroughBinaryTreeWith1Item()
        {
            //Arrange
            var target = BinaryTree.Empty;
            target = target.Add(1);
            int sum = 0;

            //Act
            foreach(var item in target)
            {
                sum += item;
            }

            //Assert
            Assert.AreEqual(1, sum);
        }

        [TestMethod]
        public void ForEachThroughBinaryTreeWith3Item()
        {
            //Arrange
            var target = BinaryTree.Empty;
            target = target.Add(5);
            target = target.Add(10);
            target = target.Add(15);
            int sum = 0;
            int count = 0;

            //Act
            foreach (var item in target)
            {
                count++; 
                sum += item;
            }

            //Assert
            Assert.AreEqual(30, sum);
            Assert.AreEqual(3, count);
        }
    }
}
