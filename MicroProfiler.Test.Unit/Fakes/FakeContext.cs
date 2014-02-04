using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace MicroProfiler.Test.Unit.Fakes
{
    public class FakeContext : HttpContextBase
    {
        public Dictionary<string, object> RealItems { get; set; } 

        public FakeContext()
        {
            RealItems = new Dictionary<string, object>();
        }

        public override IDictionary Items
        {
            get { return RealItems; }
        }
    }
}