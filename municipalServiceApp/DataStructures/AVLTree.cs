//NEEDED FOR PART 3 OF POE
using System;
using System.Collections.Generic;

namespace municipalServiceApp.DataStructures
{
    public class AVLNode<T> where T : IComparable<T>
    {
        public T Value;
        public AVLNode<T>? Left, Right;
        public int Height;
        public AVLNode(T value) { Value = value; Height = 1; }
    }

    public class AVLTree<T> where T : IComparable<T>
    {
        public AVLNode<T>? Root;

        private int Height(AVLNode<T>? n) => n?.Height ?? 0;
        private int BalanceFactor(AVLNode<T>? n) => n == null ? 0 : Height(n.Left) - Height(n.Right);
        private void UpdateHeight(AVLNode<T> n) => n.Height = 1 + Math.Max(Height(n.Left), Height(n.Right));

        private AVLNode<T> RightRotate(AVLNode<T> y)
        {
            var x = y.Left!;
            var T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            UpdateHeight(y); UpdateHeight(x);
            return x;
        }

        private AVLNode<T> LeftRotate(AVLNode<T> x)
        {
            var y = x.Right!;
            var T2 = y.Left;
            y.Left = x;
            x.Right = T2;
            UpdateHeight(x); UpdateHeight(y);
            return y;
        }

        private AVLNode<T> InsertInternal(AVLNode<T>? node, T value)
        {
            if (node == null) return new AVLNode<T>(value);
            if (value.CompareTo(node.Value) < 0) node.Left = InsertInternal(node.Left, value);
            else node.Right = InsertInternal(node.Right, value);

            UpdateHeight(node);
            var balance = BalanceFactor(node);

            if (balance > 1 && value.CompareTo(node.Left!.Value) < 0) return RightRotate(node);
            if (balance < -1 && value.CompareTo(node.Right!.Value) > 0) return LeftRotate(node);
            if (balance > 1 && value.CompareTo(node.Left!.Value) > 0)
            {
                node.Left = LeftRotate(node.Left!);
                return RightRotate(node);
            }
            if (balance < -1 && value.CompareTo(node.Right!.Value) < 0)
            {
                node.Right = RightRotate(node.Right!);
                return LeftRotate(node);
            }

            return node;
        }

        public void Insert(T val) => Root = InsertInternal(Root, val);

        public IEnumerable<T> InOrder()
        {
            var stack = new Stack<AVLNode<T>>();
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
