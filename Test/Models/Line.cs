using Test.Contracts;

namespace Test.Models
{
    public class Line
    {
        public Line(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            if(x2!=x1 && y2!=y1)
            {
                K = Convert.ToDouble(y2 - y1) / Convert.ToDouble(x2 - x1);
                B = -(x1 * y2 - x2 * y1) / (x2 - x1);
            }
            Points = new List<CreatePointRequest>();
        }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public double K { get; set; }
        public double B { get; set; }
        public List<CreatePointRequest> Points { get; set; }

        public List<CreatePointRequest> GetPoints(int lastNumberPoint)
        {
            if(X2<X1)
            {
                for(int i = 1;i<X1 - X2;i++)
                {
                    lastNumberPoint++;
                    Points.Add(new CreatePointRequest(X1 - i, Convert.ToInt32(K * (X1 - i) + B),lastNumberPoint));
                }
            }
            else
            {
                for (int i = 1; i < X2 - X1; i++)
                {
                    lastNumberPoint++;
                    Points.Add(new CreatePointRequest(X1 + i, Convert.ToInt32(K * (X1 + i) + B), lastNumberPoint));
                }
            }
            return Points;
        }

    }
}
