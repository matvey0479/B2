namespace Test.Domain.Entites
{
    public class Polygon
    {
        public Polygon(string name) 
        {
            Name = name;
            CreatedDate = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Point> Points {  get; set; } 
    }
}
