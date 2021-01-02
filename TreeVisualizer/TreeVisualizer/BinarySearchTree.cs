using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeVisualizer
{
    public class BinarySearchTree<TValue> : BaseTree<TValue> where TValue : IComparable<TValue>
    {
        public BinarySearchTree(TreeConfiguration configuration)
            : base(configuration)
        {
        }

        public override void Insert(TValue value)
        {
            _root = Insert(_root, value);
        }

        public override void Remove(TValue value)
        {
            _root = Remove(_root, value);
        }

        private Node<TValue> Insert(Node<TValue> root, TValue value)
        {
            if (root == null)
                root = new Node<TValue>(value);
            else if (root.Value.CompareTo(value) > 0)
                root.Left = Insert(root.Left, value);
            else
                root.Right = Insert(root.Right, value);
            return root;
        }

        private Node<TValue> Remove(Node<TValue> root, TValue value)
        {
            if (root == null)
                return root;

            if (root.Value.CompareTo(value) > 0)
                root.Left = Remove(root.Left, value);
            else if (root.Value.CompareTo(value) < 0)
                root.Right = Remove(root.Right, value);
            else
            {
                if (root.Left == null && root.Right == null)
                    root = null;
                else if (root.Left == null && root.Right != null)
                    root = root.Right;
                else if (root.Left != null && root.Right == null)
                    root = root.Left;
                else
                {
                    Node<TValue> minValueNode = GetMinValueNode(root.Right);
                    root.Value = minValueNode.Value;
                    root.Right = Remove(root.Right, minValueNode.Value);
                }
            }
            return root;
        }

        public override IEnumerable<NodeInfo> GetAllNodes()
        {
            var nodeCollection = new List<Node<TValue>>();

            GetAllNodes(_root, nodeCollection);

            var nodeInfos = nodeCollection.ToDictionary(
                x => x,
                y => new NodeInfo
                {
                    IsBstNode = true,
                    IsAvlNode = false,
                    Value = y.Value.ToString(),
                    IsLeaf = y.Left == null && y.Right == null
                }
            );

            CalculateNodePositions(_root, nodeInfos, offset: 0, depth: 0);
            AggregateChildNotePositions(_root, null, nodeInfos);

            return nodeInfos.Values;
        }
    }
}
