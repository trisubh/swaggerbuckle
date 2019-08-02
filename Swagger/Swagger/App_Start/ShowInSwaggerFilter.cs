using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace Swagger.App_Start
{
    public class ShowInSwaggerFilter : IDocumentFilter
    {
        
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            //swaggerDoc.basePath = "C:/Users/Saurabh.Tripathi/Desktop/dapps/v1.json";
        }
    }

    public class AddDefaultResponse : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {

        }
    }

    public class ApplySchemaVendorExtensions : ISchemaFilter
    { 
        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            //throw new NotImplementedException();
        }
    }
}