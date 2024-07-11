using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetKubernetes.Models;

namespace NetKubernetes.Token {
    public interface IJwtGenerator {
      string createToken(User user);
    }
}