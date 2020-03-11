namespace MathExamples
{
    struct Pair
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Pair(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            Pair other = (Pair)obj;

            if (this.X == other.X && this.Y == other.Y)
            {
                return true;
            }
            else if (this.X == other.Y && this.Y == other.X)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
