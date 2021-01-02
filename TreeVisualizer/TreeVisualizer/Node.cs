using System;

namespace TreeVisualizer
{
    public class Node<TValue> where TValue : IComparable<TValue>
    {
        public Node(TValue value)
        {
            Value = value;
        }

        public Node<TValue> Left { get; set; }

        public Node<TValue> Right { get; set; }

        public TValue Value { get; set; }
    }
}
