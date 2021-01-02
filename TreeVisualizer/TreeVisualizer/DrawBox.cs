using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace TreeVisualizer
{
    public class DrawBox : PictureBox
    {
        private IEnumerable<NodeInfo> _treeNodes;
        private TreeConfiguration _configuration;

        public DrawBox()
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.DoubleBuffer,
                true);
        }

        public void Print<TTree>(ITree tree)
            where TTree : ITree
        {
            _treeNodes = tree.GetAllNodes();
            _configuration = tree.GetConfiguration();

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (_treeNodes == null)
            {
                return;
            }

            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            base.OnPaint(pe);

            int baseOffset = Width / 2 - _configuration.CircleDiameter / 2 - _treeNodes.FirstOrDefault()?.Position.X ?? default;

            foreach (var node in _treeNodes)
            {
                if (node.LeftChildPosition != null)
                    DrawConnectionArrow(node.Position, node.LeftChildPosition, baseOffset, pe.Graphics);
                if (node.RightChildPosition != null)
                    DrawConnectionArrow(node.Position, node.RightChildPosition, baseOffset, pe.Graphics);

                DrawNode(node, baseOffset, pe.Graphics);
            }
        }

        private void DrawConnectionArrow(Position fromNodePosition, Position toNodePosition, int offset, Graphics grapics)
        {
            GraphicsPath capPath = new GraphicsPath();
            capPath.AddLine(_configuration.ArrowAnchorSize * -1, 0, _configuration.ArrowAnchorSize, 0);
            capPath.AddLine(_configuration.ArrowAnchorSize * -1, 0, 0, _configuration.ArrowAnchorSize);
            capPath.AddLine(0, _configuration.ArrowAnchorSize, _configuration.ArrowAnchorSize, 0);
            Pen linePen = new Pen(Color.Black, 1)
            {
                CustomEndCap = new CustomLineCap(null, capPath, LineCap.ArrowAnchor)
            };

            var startPoint = new Point
            {
                X = fromNodePosition.X + _configuration.CircleDiameter / 2 + offset,
                Y = fromNodePosition.Y + _configuration.CircleDiameter / 2
            };
            var endPoint = new Point
            {
                X = toNodePosition.X + _configuration.CircleDiameter / 2 + offset,
                Y = toNodePosition.Y + _configuration.CircleDiameter / 2
            };

            grapics.DrawLine(
                linePen,
                startPoint,
                GeometryHelper.CalculatePoint(startPoint, endPoint, GeometryHelper.GetDistance(startPoint, endPoint) - _configuration.ArrowAnchorSize - _configuration.CircleDiameter / 2)
            );
        }

        private void DrawNode(NodeInfo node, int offset, Graphics grapics)
        {
            grapics.FillEllipse(
                PensAndStuff.CircleBrush,
                node.Position.X + offset,
                node.Position.Y,
                _configuration.CircleDiameter,
                _configuration.CircleDiameter
                );

            grapics.DrawEllipse(
                PensAndStuff.CirclePen,
                node.Position.X + offset,
                node.Position.Y,
                _configuration.CircleDiameter,
                _configuration.CircleDiameter
                );

            var stringSize = grapics.MeasureString(node.Value, DefaultFont);

            grapics.DrawString(
                node.Value,
                DefaultFont,
                PensAndStuff.TextBrush,
                node.Position.X + (_configuration.CircleDiameter / 2) - (stringSize.Width / 2) + 1 + offset,
                node.Position.Y + (_configuration.CircleDiameter / 2) - (stringSize.Height / 2) + 1
                );

            if (node.IsAvlNode)
            {
                grapics.DrawString(
                    node.Height.ToString(),
                    new Font(DefaultFont.FontFamily, 7f),
                    PensAndStuff.TextBrush,
                    node.IsLeftChild ? node.Position.X - 8f + offset : node.Position.X + _configuration.CircleDiameter + offset,
                    node.Position.Y
                    );
            }
        }
    }
}
 