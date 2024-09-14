using Test.Domain.Entites;

namespace Test.Contracts
{
    public record CreatePolygonRequest(string Name,List<CreatePointRequest> Points);

    
}
