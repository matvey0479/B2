using Microsoft.AspNetCore.Mvc;
using Test.Contracts;

namespace Test.Controllers
{
    [Route("api/pointInPolygons")]
    [ApiController]
    public class CheckPointInPolygonController : Controller
    {
        [HttpPost]
        public IActionResult PointIsPolygons([FromBody] CheckPointInPolygonRequst request)
        {
            bool result = false;
            var points = request.Polygon.Points;
            var point = request.Point;
            int size = request.Polygon.Points.Count;
            int j = size - 1;
            for (int i = 0; i < size; i++)
            {
                if ((points[i].Y < point.Y && points[j].Y >= point.Y || points[j].Y < point.Y && points[i].Y >= point.Y) &&
                     (points[i].X + (point.Y - points[i].Y) / (points[j].Y - points[i].Y) * (points[j].X - points[i].X) < point.X))
                    result = !result;
                j = i;
            }
            return Ok(result);
        }
    }
}
