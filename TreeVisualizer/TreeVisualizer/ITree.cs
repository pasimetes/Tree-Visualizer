using System;
using System.Collections.Generic;

namespace TreeVisualizer
{
    public interface ITree
    {
        IEnumerable<NodeInfo> GetAllNodes();

        TreeConfiguration GetConfiguration();
    }

    public interface ITree<TValue> : ITree where TValue : IComparable<TValue>
    {
        void Insert(TValue value);

        void Remove(TValue value);
    }
}
