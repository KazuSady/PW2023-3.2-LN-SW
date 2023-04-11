using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class AbstractDaneAPI
    {
        //public abstract Obszar obszar { get; }
        public static AbstractDaneAPI CreateApi()
        {
            return new DaneAPI();
        }

        internal sealed class DaneAPI : AbstractDaneAPI
        {

        }
    }
}
