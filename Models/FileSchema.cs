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
        public DropDownItem DropDownItem { get; set; }
        public Type TypeDtoSchema { get; set; }
        public Type TypeDomainSchema { get; set; }
        public JSchema JsonSchema { get; set; }
        public BaseRepo Repository { get; set; }

        public FileSchema(DropDownItem dropDownItem, Type typeDtoSchema, Type typeDomainSchema, JSchema jsonSchema, BaseRepo repository)
        {
            DropDownItem = dropDownItem;
            TypeDtoSchema = typeDtoSchema;
            TypeDomainSchema = typeDomainSchema;
            JsonSchema = jsonSchema;
            Repository = repository;
        }


        /// <summary>
        /// The List registers all relevant items of a schema.
        /// This MUST be initialized only once
        /// </summary>
        public static IEnumerable<FileSchema> ListOfAllFileSchemas { get; } = new List<FileSchema>()
        {
            new FileSchema(new DropDownItem("Test01", "/resources/images/blau.png"),
                null,
                typeof(Test1),
                null,
                new RepoTest1()),
            new FileSchema(new DropDownItem("Test02", "/resources/images/gelb.png"),
                null,
                typeof(Test1),
                null,
                new RepoTest2()),
            new FileSchema(new DropDownItem("Schema03", "/resources/images/gruen.png"),
                typeof(Schema03Dto),
                typeof(Schema03Anmeldungen),
                Schema03Dto.Schema,
                new RepoSchema03())
        };

        /// <summary>
        /// Get encapsuling object of a DropDownItem
        /// </summary>
        /// <param name="dropDownItem"></param>
        /// <returns></returns>
        public static FileSchema GetFileSchemaByDropDownItem(DropDownItem dropDownItem) =>
            ListOfAllFileSchemas.FirstOrDefault(x => x.DropDownItem == dropDownItem);

        /// <summary>
        /// Get encapsuling object of a Domain type
        /// </summary>
        /// <param name="dropDownItem"></param>
        /// <returns></returns>
        public static FileSchema GetFileSchemaByDomainType(Type typeDomainSchema) =>
            ListOfAllFileSchemas.FirstOrDefault(x => x.TypeDomainSchema == typeDomainSchema);

    }
}
