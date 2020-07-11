//using System.Collections.Generic;
//using System.Linq;

//namespace GameEstate.Toy.Models
//{
//    public class ParticleSystem : Dictionary<string, object>
//    {
//        public IEnumerable<IDictionary<string, object>> GetRenderers()
//            => this.GetArray("m_Renderers") ?? Enumerable.Empty<IDictionary<string, object>>();

//        public IEnumerable<IDictionary<string, object>> GetOperators()
//            => this.GetArray("m_Operators") ?? Enumerable.Empty<IDictionary<string, object>>();

//        public IEnumerable<IDictionary<string, object>> GetInitializers()
//            => this.GetArray("m_Initializers") ?? Enumerable.Empty<IDictionary<string, object>>();

//        public IEnumerable<IDictionary<string, object>> GetEmitters()
//            => this.GetArray("m_Emitters") ?? Enumerable.Empty<IDictionary<string, object>>();

//        public IEnumerable<string> GetChildParticleNames(bool enabledOnly = false)
//        {
//            IEnumerable<IDictionary<string, object>> children = this.GetArray("m_Children");

//            if (children == null)
//                return Enumerable.Empty<string>();

//            if (enabledOnly)
//                children = children.Where(c => !c.ContainsKey("m_bDisableChild") || !c.Get<bool>("m_bDisableChild"));

//            return children.Select(c => c.Get<string>("m_ChildRef")).ToList();
//        }
//    }
//}
