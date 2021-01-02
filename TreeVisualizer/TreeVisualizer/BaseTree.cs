using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeVisualizer
{
    public abstract class BaseTree<TValue> : ITree<TValue> where TValue : IComparable<TValue>
    {
        protected TreeConfiguration _configuration;
        protected Node<TValue> _root;

        public BaseTree(TreeConfiguration configuration)
        {
            _configuration = configuration;
        }

        public abstract void Insert(TValue value);

        public abstract void Remove(TValue value);

        public abstract IEnumerable<NodeInfo> GetAllNodes();

        protected int CalculateNodePositions(Node<TValue> root, IDictionary<Node<TValue>, NodeInfo> nodeInfos, int offset, int depth)
        {
            if (root == null)
            {
                return 0;
            }

            int circleDiameterOffset = _configuration.CircleDiameter - (int)(_configuration.CircleDiameter / Math.PI);

            int left = CalculateNodePositions(root.Left, nodeInfos, offset, depth + 1);
            int right = CalculateNodePositions(root.Right, nodeInfos, offset + left + circleDiameterOffset, depth + 1);

            nodeInfos[root].Position =
                new Position
                {
                    Y = depth * _configuration.CircleDiameter,
                    X = left + offset
                };
            return left + right + circleDiameterOffset;
        }

        protected void AggregateChildNotePositions(Node<TValue> root, Node<TValue> parent, IDictionary<Node<TValue>, NodeInfo> nodeInfos)
        {
            if (root == null)
            {
                return;
            }

            AggregateChildNotePositions(root.Left, root, nodeInfos);
            AggregateChildNotePositions(root.Right, root, nodeInfos);

            if (parent != null)
            {
                nodeInfos[root].IsRightChild = parent.Right == root;
                nodeInfos[root].IsLeftChild = parent.Left == root;
            }

            if (root.Left != null)
                nodeInfos[root].LeftChildPosition =
                    new Position
                    {
                        X = nodeInfos[root.Left].Position.X,
                        Y = nodeInfos[root.Left].Position.Y
                    };
            if (root.Right != null)
                nodeInfos[root].RightChildPosition =
                    new Position
                    {
                        X = nodeInfos[root.Right].Position.X,
                        Y = nodeInfos[root.Right].Position.Y
                    };
        }

        protected void GetAllNodes(Node<TValue> root, ICollection<Node<TValue>> collection)
        {
            if (root == null)
            {
                return;
            }
            collection.Add(root);
            GetAllNodes(root.Left, collection);
            GetAllNodes(root.Right, collection);
        }

        protected Node<TValue> GetMinValueNode(Node<TValue> root)
        {
            Node<TValue> currentNode = root;
            while (currentNode != null && currentNode.Left != null)
            {
                currentNode = currentNode.Left;
            }
            return currentNode;
        }

        protected Node<TValue> GetMaxValueNode(Node<TValue> root)
        {
            Node<TValue> currentNode = root;
            while (currentNode != null && currentNode.Right != null)
            {
                currentNode = currentNode.Right;
            }
            return currentNode;
        }

        public TreeConfiguration GetConfiguration()
        {
            return _configuration;
        }
    }
}
