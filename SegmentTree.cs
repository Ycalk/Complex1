using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex1
{
    internal interface ISegment
    {
        public bool Contains(int el);
    }

    internal interface ITree
    {
        public bool Contains(int start, int end, int el);
    }

    internal class SegmentTree : ITree
    {
        private readonly Dictionary<SegmentData, Segment> _tree;

        public SegmentTree(int[] mas)
        {
            var treeBuilder = new TreeBuilder();
            _tree = treeBuilder.Build(mas);
        }

        public readonly struct SegmentData
        {
            public readonly int Start;
            public readonly int End;

            public SegmentData(int start, int end)
            {
                Start = start;
                End = end;
            }

            public override string ToString()
            {
                return "Start: " + Start + " End: " + End;
            }
        }

        public class Segment : ISegment
        {
            private readonly HashSet<int> _elements = new();
            public Segment(int[] elements)
            {
                foreach (var el in elements)
                    _elements.Add(el);
            }
            
            public bool Contains(int el) => _elements.Contains(el);

            public override string ToString()
            {
                var result = new StringBuilder();
                result.Append("Elements: ");
                foreach (var el in _elements)
                    result.Append(el + " ");
                return result.ToString();
            }
        }

        public bool Contains(int start, int end, int el)
        {
            foreach (var segmentData in _tree.Keys)
            {
                if (segmentData.Start < start || segmentData.End > end) continue;

                if (_tree[segmentData].Contains(el))
                    return true;
            }
            return false;
        }
    }
}
