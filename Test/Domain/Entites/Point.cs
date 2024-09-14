namespace Test.Domain.Entites
{
    public class Point
    {
        public Point(int x, int y,int number) 
        {
            X = x;
            Y = y;
            Number = number;
        }
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
