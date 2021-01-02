namespace TreeVisualizer
{
    public class TreeConfiguration
    {
        public TreeConfiguration(int circleDiameter, int arrowAnchorSize)
        {
            CircleDiameter = circleDiameter;
            ArrowAnchorSize = arrowAnchorSize;
        }

        public int CircleDiameter { get; private set; }

        public int ArrowAnchorSize { get; private set; }
    }
}