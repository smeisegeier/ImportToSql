using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Helper
{
    public static class Extensions
    {


        // TODO ->package

        /// <summary>
        /// Parses a json string into an object of given Type. 
        /// Examle: if (json.TryParseJson(out JsonClass result))
        /// https://stackoverflow.com/questions/23906220/deserialize-json-in-a-tryparse-way
        /// </summary>
        /// <typeparam name="T">target type, object is created inside</typeparam>
        /// <param name="this">json string</param>
        /// <param name="result">object to be used outside</param>
        /// <returns>success | fail</returns>
        public static bool ToJsonTryParse<T>(this string @this, out T result)
        {
            bool success = true;
            var settings = new JsonSerializerSettings
            {
                // suppress error handling / exceptions
                Error = (sender, args) => { success = false; args.ErrorContext.Handled = true; },
                MissingMemberHandling = MissingMemberHandling.Error
            };
            result = JsonConvert.DeserializeObject<T>(@this, settings);
            return success;
        }
    }
}
