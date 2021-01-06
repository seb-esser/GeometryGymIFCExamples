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
            var db = new DatabaseIfc(ModelView.Ifc4NotAssigned);
            
            var site = new IfcSite(db,"site");
            var project = new IfcProject(site, "GeomRep", IfcUnitAssignment.Length.Metre);

            IfcAxis2Placement3D placement = new IfcAxis2Placement3D(new IfcCartesianPoint(db, 2, 5, 1));
			IfcLocalPlacement objectPlacement = new IfcLocalPlacement(site.ObjectPlacement, placement);
			var proxy = new IfcBuildingElementProxy(site, objectPlacement, null)
            {
                Name ="Cuboid1"
            };
			
			// var profile = new IfcCircleProfileDef(db, "BoreHole", 4);
			var profile = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);

            IfcExtrudedAreaSolid extrudedAreaSolid = new IfcExtrudedAreaSolid(profile, 1.35);
			IfcProductDefinitionShape shape = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid));

            proxy.Representation = shape;

            db.WriteFile("model1.ifc");



        }
    }
}
