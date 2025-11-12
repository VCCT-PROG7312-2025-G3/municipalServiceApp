//NEEDED FOR PART 3 OF POE
using System;
using System.Collections.Generic;

namespace municipalServiceApp.DataStructures
{
    public class BinaryTree<T>
    {
        public BinaryTreeNode<T>? Root { get; set; }

        public IEnumerable<T> InOrder()
        {
            var stack = new Stack<BinaryTreeNode<T>>();
            var current = Root;
            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }
                current = stack.Pop();
                yield return current.Value;
                current = current.Right;
            }
        }
    }
}