using Rki.ImportToSql.Models.Domain;
using Rki.ImportToSql.Services;
using Rki.ImportToSql.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models
{
    // TODO versioning
    public class FileSchema
    {
        public DropDownItem DropDownItem { get; set; }
        
        public Type TypeDomainSchema { get; set; }
        
        /// <summary>
        /// Singleton object, n domain classes can share the same db / repo / context
        /// </summary>
        public BaseRepo Repository { get; set; }

        public FileSchema(DropDownItem dropDownItem, Type typeDomainSchema, BaseRepo repository)
        {
            DropDownItem = dropDownItem;
            TypeDomainSchema = typeDomainSchema;
            Repository = repository;
        }

        /// <summary>
        /// The List registers all relevant items of a schema.
        /// This MUST be initialized only once
        /// enums flags are used
        /// </summary>
        public static IEnumerable<FileSchema> ListOfAvailableFileSchemas => _listOfAllFileSchemas
            .Where(x => Globals.ApplicationNetworkMode.HasFlag(x.Repository._ApplicationNetworkModeType));

        private static IEnumerable<FileSchema> _listOfAllFileSchemas = new List<FileSchema>()
        {
            new FileSchema(new DropDownItem("Test01", "/resources/images/grau.png"),
                typeof(Test1),
                RepoTest1.SingletonRepo
                ),
            new FileSchema(new DropDownItem("Test02", "/resources/images/gelb.png"),
                typeof(Test2),
                RepoTest2.SingletonRepo
                ),
            new FileSchema(new DropDownItem("COALA_Prozessdaten", "/resources/images/rot.png"),
                typeof(GsProzessdaten),
                InterfaceDbContext.SingletonRepo
                ),
            new FileSchema(new DropDownItem("IMIRA", "/resources/images/Icon_freigegeben.jpg"),
                typeof(Imira),
                InterfaceDbContext.SingletonRepo
                )
        };

        /// <summary>
        /// Get encapsuling object of a DropDownItem
        /// </summary>
        /// <param name="dropDownItem"></param>
        /// <returns></returns>
        public static FileSchema GetFileSchemaByDropDownItem(DropDownItem dropDownItem) =>
            ListOfAvailableFileSchemas.FirstOrDefault(x => x.DropDownItem == dropDownItem);

        /// <summary>
        /// Get encapsuling object of a Domain type
        /// </summary>
        /// <param name="dropDownItem"></param>
        /// <returns></returns>
        public static FileSchema GetFileSchemaByDomainType(Type typeDomainSchema) =>
            ListOfAvailableFileSchemas.FirstOrDefault(x => x.TypeDomainSchema == typeDomainSchema);

    }
}
