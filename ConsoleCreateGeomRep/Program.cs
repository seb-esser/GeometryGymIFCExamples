using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryGym.Ifc;

namespace ConsoleCreateGeomRep
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbInitial = new DatabaseIfc(ModelView.Ifc4NotAssigned);
            dbInitial.Factory.Options.GenerateOwnerHistory = false;

            var site = new IfcSite(dbInitial,"site")
            {
                Guid = new Guid("aa4019f8-2584-44a1-a132-c17dfff69c41")
            };
            var project = new IfcProject(dbInitial, "GeomRep")
            {
                Guid = new Guid("94d4ddac-9120-4fb9-bea0-7a414ee725d4")
            };

            new IfcRelAggregates(project, new List<IfcObjectDefinition>() {site})
            {
                Guid = new Guid("5e1fd0e5-b005-fe11-501e-5caff01dc1ad")
            };
          
            IfcAxis2Placement3D placement1 = new IfcAxis2Placement3D(new IfcCartesianPoint(dbInitial, 2, 5, 1));
			IfcLocalPlacement objectPlacement1 = new IfcLocalPlacement(site.ObjectPlacement, placement1);

            IfcAxis2Placement3D placement2 = new IfcAxis2Placement3D(new IfcCartesianPoint(dbInitial, 14, 5, 1));
            IfcLocalPlacement objectPlacement2 = new IfcLocalPlacement(site.ObjectPlacement, placement2);


            var profile1 = new IfcRectangleProfileDef(dbInitial, "rectangleProfileDef", 4, 6);
            IfcExtrudedAreaSolid extrudedAreaSolid1 = new IfcExtrudedAreaSolid(profile1, 1.35);
            IfcProductDefinitionShape shape1 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid1));

            var profile2 = new IfcRectangleProfileDef(dbInitial, "rectangleProfileDef", 5, 8);
            IfcExtrudedAreaSolid extrudedAreaSolid2 = new IfcExtrudedAreaSolid(profile2, 4.1);
            IfcProductDefinitionShape shape2 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid2));


            var proxy1 = new IfcBuildingElementProxy(site, objectPlacement1, shape1)
            {
                Name ="Cuboid1",
                Guid = new Guid("fbc7f4b2-177d-4875-88bb-d3b44115bbaa")
            };
            var proxy2 = new IfcBuildingElementProxy(site, objectPlacement2, shape2)
            {
                Name = "Cuboid2",
                Guid = new Guid("f8e196cb-c7d9-4d53-9885-0f687706727a")
            };


            dbInitial.WriteFile("cube_double.ifc");



        }
    }
}
