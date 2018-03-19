using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AcceptanceTests
{
    /// <summary>
    /// Made to run tests from diffrent classes in non parallel mode when it is needed.
    /// </summary>
    [CollectionDefinition("Non-parallel", DisableParallelization = true)]
    public class NonParallelCollection { }
}
