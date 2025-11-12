//NEEDED FOR PART 3 OF POE
using System.Collections.Generic;

namespace municipalServiceApp.DataStructures
{
    public class BasicTreeNode<T>
    {
        public T Value { get; set; }
        public List<BasicTreeNode<T>> Children { get; } = new List<BasicTreeNode<T>>();
        public BasicTreeNode(T value) { Value = value; }
        public void AddChild(BasicTreeNode<T> child) => Children.Add(child);
    }
}
