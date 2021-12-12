using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.Scoop
{
    internal class BucketList
    {
        List<Bucket> buckets = new List<Bucket>();

        internal void Add(Bucket bucket)
        {
            buckets.Add(bucket);
        }

        internal Bucket Default()
        {
            if (buckets.Any())
                return buckets.First();

            throw new BucketNotFoundException("default");
        }

        internal bool Contains(Bucket bucket)
        {
            return buckets.Contains(bucket);
        }
    }
}
