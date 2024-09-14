using Test.Domain.Entites;
using Test.Models;

namespace Test.Contracts
{
    public class CheckPointInPolygonRequst
    {
        public CheckPointInPolygonRequst() { }
        public CheckPointInPolygonRequst(PointSansNumber point, CreatePolygonRequest polygon)
        {
            Point = point;
            Polygon = polygon;
        }
        public PointSansNumber Point { get; set; }
        public CreatePolygonRequest Polygon { get; set; }
    }
}
