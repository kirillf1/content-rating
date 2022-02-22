using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
        public (bool Verified, bool NeedsUpgrade) Check(string hash, string password);
    }
}
