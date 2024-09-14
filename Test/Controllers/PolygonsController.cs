using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Test.Contracts;
using Test.Domain;
using Test.Domain.Entites;

namespace Test.Controllers
{
    [Route ("api/polygons")]
    [ApiController]
    public class PolygonsController : Controller
    {
        private readonly PolygonsDbContext _context;

        public PolygonsController(PolygonsDbContext context)
        {
            _context = context;
        }   

        [HttpGet]
        public async Task<IActionResult> GetPolygons()
        {
            var polygons = await _context.Polygons.Include(p => p.Points).ToListAsync();
            var points = await _context.Points.ToListAsync();
            var json = JsonConvert.SerializeObject(polygons,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
            return Ok(new GetPolygonsResponse(polygons));
        }

        [HttpPost]
        public async Task<IActionResult> SavePolygons([FromBody] CreatePolygonRequest request)
        {
            var polygon = new Polygon(request.Name);
            polygon.Points = new List<Domain.Entites.Point>();
            await _context.Polygons.AddAsync(polygon);
            foreach (var point in request.Points)
            {
                var createPoint = new Domain.Entites.Point(point.X, point.Y,point.Number);
                await _context.Points.AddAsync(createPoint);
                polygon.Points.Add(createPoint);  
            }
            await _context.SaveChangesAsync();
            return Ok(request);
        }
    }
}
