using System.Collections.Generic;

namespace GameEstate.Toy.Models
{
    public class World : Dictionary<string, object>
    {
        //public IEnumerable<string> GetEntityLumpNames()
        //    => Data.GetArray<string>("m_entityLumps");

        //public IEnumerable<string> GetWorldNodeNames()
        //    => Data.GetArray("m_worldNodes")
        //        .Select(nodeData => nodeData.GetProperty<string>("m_worldNodePrefix"))
        //        .ToList();
    }
}
