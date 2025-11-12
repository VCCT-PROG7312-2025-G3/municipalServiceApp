//NEEDED FOR PART 3 OF POE
using System;

namespace municipalServiceApp.DataStructures
{
    public enum NodeColor
    {
        Red,
        Black
    }

    public class RedBlackTreeNode<T> where T : IComparable
    {
        public T Value { get; set; }
        public NodeColor Color { get; set; }
        public RedBlackTreeNode<T>? Left { get; set; }
        public RedBlackTreeNode<T>? Right { get; set; }
        public RedBlackTreeNode<T>? Parent { get; set; }

        public RedBlackTreeNode(T value)
        {
            Value = value;
            Color = NodeColor.Red; 
        }
    }

    public class RedBlackTree<T> where T : IComparable
    {
        public RedBlackTreeNode<T>? Root { get; private set; }

        #region Public Methods

        public void Insert(T value)
        {
            var newNode = new RedBlackTreeNode<T>(value);
            Root = BSTInsert(Root, newNode);
            FixInsert(newNode);
        }

        public void InOrderTraversal(RedBlackTreeNode<T>? node, Action<T> action)
        {
            if (node == null) return;

            InOrderTraversal(node.Left, action);
            action(node.Value);
            InOrderTraversal(node.Right, action);
        }

        #endregion

        #region Private Helpers

        private RedBlackTreeNode<T> BSTInsert(RedBlackTreeNode<T>? root, RedBlackTreeNode<T> node)
        {
            if (root == null) return node;

            if (node.Value.CompareTo(root.Value) < 0)
            {
                root.Left = BSTInsert(root.Left, node);
                root.Left.Parent = root;
            }
            else if (node.Value.CompareTo(root.Value) > 0)
            {
                root.Right = BSTInsert(root.Right, node);
                root.Right.Parent = root;
            }

            return root;
        }

        private void RotateLeft(RedBlackTreeNode<T> node)
        {
            var right = node.Right!;
            node.Right = right.Left;
            if (right.Left != null)
                right.Left.Parent = node;

            right.Parent = node.Parent;

            if (node.Parent == null)
                Root = right;
            else if (node == node.Parent.Left)
                node.Parent.Left = right;
            else
                node.Parent.Right = right;

            right.Left = node;
            node.Parent = right;
        }

        private void RotateRight(RedBlackTreeNode<T> node)
        {
            var left = node.Left!;
            node.Left = left.Right;
            if (left.Right != null)
                left.Right.Parent = node;

            left.Parent = node.Parent;

            if (node.Parent == null)
                Root = left;
            else if (node == node.Parent.Left)
                node.Parent.Left = left;
            else
                node.Parent.Right = left;

            left.Right = node;
            node.Parent = left;
        }

        private void FixInsert(RedBlackTreeNode<T> node)
        {
            while (node.Parent != null && node.Parent.Color == NodeColor.Red)
            {
                var parent = node.Parent;
                var grandparent = parent.Parent;

                if (parent == grandparent?.Left)
                {
                    var uncle = grandparent.Right;

                    if (uncle?.Color == NodeColor.Red)
                    {
                        parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        grandparent.Color = NodeColor.Red;
                        node = grandparent;
                    }
                    else
                    {
                        if (node == parent.Right)
                        {
                            node = parent;
                            RotateLeft(node);
                        }
                        parent.Color = NodeColor.Black;
                        grandparent!.Color = NodeColor.Red;
                        RotateRight(grandparent);
                    }
                }
                else
                {
                    var uncle = grandparent?.Left;

                    if (uncle?.Color == NodeColor.Red)
                    {
                        parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        grandparent!.Color = NodeColor.Red;
                        node = grandparent;
                    }
                    else
                    {
                        if (node == parent.Left)
                        {
                            node = parent;
                            RotateRight(node);
                        }
                        parent.Color = NodeColor.Black;
                        grandparent!.Color = NodeColor.Red;
                        RotateLeft(grandparent);
                    }
                }
            }

            Root!.Color = NodeColor.Black;
        }

        #endregion
    }
}
