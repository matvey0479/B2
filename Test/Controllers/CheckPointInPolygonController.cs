using Microsoft.AspNetCore.Mvc;
using Test.Contracts;
using Test.Models;

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
            int size = points.Count;


            if (points[0].X != points[size - 1].X && points[0].Y != points[size - 1].Y)
            {
                // создаем замыкающую линию

                Line line = new Line(points[size - 1].X, points[size - 1].Y, points[0].X, points[0].Y);

                // получаем точки линии по уравнению прямой

                var pointsLine = line.GetPoints(points[size - 1].Number);

                // добавляем точки замыкающей линии к точкам многоугольника

                if (pointsLine != null)
                {
                    foreach (var pointLine in pointsLine)
                    {
                        points.Add(pointLine);
                    }
                }
                size = points.Count;
            }
            int j = size - 1;

            // проверяем принадлежность точки

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
