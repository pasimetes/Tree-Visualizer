using System;
using System.Collections.Generic;

namespace TreeVisualizer
{
    public interface ITree<TValue> where TValue : IComparable<TValue>
    {
        void Insert(TValue value);

        void Remove(TValue value);

        IEnumerable<NodeInfo> GetAllNodes();

        TreeConfiguration GetConfiguration();
    }
}
