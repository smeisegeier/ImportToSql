using NJsonSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models
{
    public abstract class Base
    {
        public abstract string Message { get; }

        public static string PrintList<T>(List<T> list) where T : Base =>
            string.Join(Environment.NewLine, list.Select(x => x.ToString()));
    }
}
