using Newtonsoft.Json.Schema;
using Rki.ImportToSql.Models.Domain;
using Rki.ImportToSql.Models.Dto;
using Rki.ImportToSql.Services;
using Rki.ImportToSql.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models
{
    public class FileSchema
    {
        public string Key { get; set; }
        public DropDownItem DropDownItem { get; set; }
        public Type TypeDtoSchema { get; set; }
        public JSchema JsonSchema { get; set; }
        public BaseRepo Repository { get; set; }

        public FileSchema(string key, DropDownItem dropDownItem, Type typeDtoSchema, JSchema jsonSchema, BaseRepo repository)
        {
            Key = key;
            DropDownItem = dropDownItem;
            TypeDtoSchema = typeDtoSchema;
            JsonSchema = jsonSchema;
            Repository = repository;
        }

        /// <summary>
        /// The List registers all relevant items of a schema.
        /// </summary>
        public static IEnumerable<FileSchema> ListOfAllFileSchemas { get; } = new List<FileSchema>()
        {
            new FileSchema("Test1",
                new DropDownItem("Test01", "/resources/images/blau.png"),
                null,
                null,
                new RepoTest1()),
            new FileSchema("Test2",
                new DropDownItem("Test02", "/resources/images/gelb.png"),
                null,
                null,
                new RepoTest2()),
            new FileSchema("Schema03",
                new DropDownItem("Schema03", "/resources/images/gruen.png"),
                typeof(Schema03Dto),
                Schema03Dto.Schema,
                new RepoSchema03())
        };
    }
}
