using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDapper.Models;

namespace TestDapper.Helpers
{
    public interface IAESHelper
    {
        string Encrypt(string value);
        ResultModel Decrypt(string CipherText);
    }
}
