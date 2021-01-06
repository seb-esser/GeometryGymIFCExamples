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
            
            var site = new IfcSite(dbInitial,"site");
            var project = new IfcProject(site, "GeomRep", IfcUnitAssignment.Length.Metre);
          
            IfcAxis2Placement3D placement1 = new IfcAxis2Placement3D(new IfcCartesianPoint(dbInitial, 2, 5, 1));
			IfcLocalPlacement objectPlacement1 = new IfcLocalPlacement(site.ObjectPlacement, placement1);            
            
            IfcAxis2Placement3D placement2 = new IfcAxis2Placement3D(new IfcCartesianPoint(dbInitial, 10, 5, 1));
			IfcLocalPlacement objectPlacement2 = new IfcLocalPlacement(site.ObjectPlacement, placement2);

            
            var profile1 = new IfcRectangleProfileDef(dbInitial, "rectangleProfileDef", 4, 6);
            IfcExtrudedAreaSolid extrudedAreaSolid1 = new IfcExtrudedAreaSolid(profile1, 1.35);
            IfcProductDefinitionShape shape1 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid1));

            var profile2 = new IfcRectangleProfileDef(dbInitial, "rectangleProfileDef", 4, 6);
            IfcExtrudedAreaSolid extrudedAreaSolid2 = new IfcExtrudedAreaSolid(profile2, 1.35);
            IfcProductDefinitionShape shape2 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid2));


            var proxy1 = new IfcBuildingElementProxy(site, objectPlacement1, shape1)
            {
                Name ="Cuboid1"
            };
            var proxy2 = new IfcBuildingElementProxy(site, objectPlacement2, shape2)
            {
                Name = "Cuboid2"
            };


            dbInitial.WriteFile("model2.ifc");



        }
    }
}
