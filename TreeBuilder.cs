using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Complex1.SegmentTree;

namespace Complex1
{
    interface IBuilder
    {
        public Dictionary<SegmentData, Segment> Build(int[] original);
    }


    class TreeBuilder : IBuilder
    {
        private readonly Dictionary<SegmentData, Segment> _buildResult = new();

        public Dictionary<SegmentData, Segment> Build(int[] original)
        {
            BuildFirstLayer(original);
            var currentLevelInfo = new List<SegmentData> { new(1, original.Length) };
            while (!EndOfTree(currentLevelInfo))
                currentLevelInfo = BuildLayer(currentLevelInfo, original);
            
            return _buildResult;
        }

        private bool EndOfTree(List<SegmentData> treeLevelInfo) =>
            treeLevelInfo.All(segmentData => segmentData.Start == segmentData.End);

        private void BuildFirstLayer(int[] original) =>
            _buildResult[new SegmentData(1, original.Length)] = new Segment(original);

        private List<SegmentData> BuildLayer(List<SegmentData> currentLevelInfo, int[] original)
        {
            var newLevelInfo = new List<SegmentData>();
            foreach (var data in currentLevelInfo)
            {
                var segmentLength = (data.End - data.Start + 1) / 2;
                var leftSegment = new SegmentData(data.Start, data.Start + segmentLength - 1);
                var rightSegment = new SegmentData(data.Start + segmentLength, data.End);
                AddSegment (leftSegment, newLevelInfo, original);
                AddSegment (rightSegment, newLevelInfo, original);
            }

            return newLevelInfo;
        }

        private void AddSegment(SegmentData data, List<SegmentData> levelInfo, int[] original)
        {
            if (data.Start > data.End) return;

            _buildResult[data] = new Segment(original[(data.Start - 1)..data.End]);
            levelInfo.Add(data);
        }
    }
}
