//NEEDED FOR PART 3 OF POE
using System;
using System.Collections.Generic;

namespace municipalServiceApp.DataStructures
{
    public class BSTNode<T> where T : IComparable<T>
    {
        public T Value;
        public BSTNode<T>? Left, Right;
        public BSTNode(T val) { Value = val; }
    }

    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public BSTNode<T>? Root;
        public void Insert(T value)
        {
            Root = InsertInternal(Root, value);
        }
        private BSTNode<T> InsertInternal(BSTNode<T>? node, T value)
        {
            if (node == null) return new BSTNode<T>(value);
            if (value.CompareTo(node.Value) < 0) node.Left = InsertInternal(node.Left, value);
            else node.Right = InsertInternal(node.Right, value);
            return node;
        }

        public bool Contains(T value)
        {
            var cur = Root;
            while (cur != null)
            {
                var cmp = value.CompareTo(cur.Value);
                if (cmp == 0) return true;
                cur = cmp < 0 ? cur.Left : cur.Right;
            }
            return false;
        }

        public IEnumerable<T> InOrder()
        {
            var stack = new Stack<BSTNode<T>>();
            var cur = Root;
            while (cur != null || stack.Count > 0)
            {
                while (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.Left;
                }
                cur = stack.Pop();
                yield return cur.Value;
                cur = cur.Right;
            }
        }
    }
}
