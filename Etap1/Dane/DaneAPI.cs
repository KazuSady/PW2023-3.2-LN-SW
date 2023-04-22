using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class AbstractDaneAPI
    {
        
        public abstract void CreateObszar(int height, int width, int kulaAmount ,int kulaRadius);

        
        public static AbstractDaneAPI CreateApi()
        {
            return new DaneAPI();
        }

        internal sealed class DaneAPI : AbstractDaneAPI
        {
            public override void CreateObszar(int height, int width, int kulaAmount, int kulaRadius)
            {
                
            }
        }
    }
}
