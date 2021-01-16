using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryGym.Ifc;

namespace CreateUnitTest
{
    class Program
    {
        /// <summary>
        /// You can give the name of the TestCase -> to build only them
        /// </summary>
        /// <param name="args">Testcases to expoert</param>
        /// <help> Testcases: Spatial_simple\n
        /// Railway_spatial\n Spatial_01\n
        /// 
        /// </help>
        static void Main(string[] args)
        {
            //Var declaration

            string folder = "Unittest";
            var testcasename = new List<string>();
            testcasename.Add("Spatial_simple");
            testcasename.Add("Railway_spatial");
            testcasename.Add("Spatial_01");
            testcasename.Add("Placement_Local");

            //Help handling + single selection
            if (args.Length != 0)
            {

                //Help handling
                if (args.First().Equals("-?"))
                {
                    foreach (var i in testcasename)
                    {
                        Console.WriteLine(i);
                    }
                    return;
                }

                //Select only selected ones
                testcasename = new List<string>();
                foreach (var i in args)
                {
                    testcasename.Add(i);
                }
            }


            foreach (var proname in testcasename)
            {

                var db = new DatabaseIfc(ModelView.Ifc4NotAssigned);
                db.Factory.Options.GenerateOwnerHistory = false;

                var site = new IfcSite(db, "site")
                {
                    Guid = new Guid("aa4019f8-2584-44a1-a132-c17dfff69c41")
                };
                var project = new IfcProject(db, proname)
                {
                    Guid = new Guid("94d4ddac-9120-4fb9-bea0-7a414ee725d4")
                };

                new IfcRelAggregates(project, new List<IfcObjectDefinition>() { site })
                {
                    Guid = new Guid("5e1fd0e5-b005-fe11-501e-5caff01dc1ad")
                };
                //Some placements
                IfcAxis2Placement3D placement1 = new IfcAxis2Placement3D(new IfcCartesianPoint(db, 2, 5, 1));
                IfcLocalPlacement objectPlacement1 = new IfcLocalPlacement(site.ObjectPlacement, placement1);
                IfcAxis2Placement3D placement2 = new IfcAxis2Placement3D(new IfcCartesianPoint(db, 10, 5, 1));
                IfcLocalPlacement objectPlacement2 = new IfcLocalPlacement(site.ObjectPlacement, placement2);
                IfcAxis2Placement3D placement3 = new IfcAxis2Placement3D(new IfcCartesianPoint(db, 15, 3, 6));
                IfcLocalPlacement objectPlacement3 = new IfcLocalPlacement(site.ObjectPlacement, placement3);
                IfcAxis2Placement3D placement4 = new IfcAxis2Placement3D(new IfcCartesianPoint(db, 5, 8, 3));
                IfcLocalPlacement objectPlacement4 = new IfcLocalPlacement(site.ObjectPlacement, placement4);
                IfcAxis2Placement3D originaxis2place = new IfcAxis2Placement3D(db.Factory.Origin);
                IfcLocalPlacement origionobjplace = new IfcLocalPlacement(site.ObjectPlacement,originaxis2place);


                //Select the Testcases and fill them into the container
                switch (proname)
                {
                    case "Spatial_simple":
                        //initial File
                        IfcBuilding building1 = new IfcBuilding(db, "building1")
                        {
                            Guid = new Guid("fbc7f4b2-177d-4875-88bb-d3b44115bb42")
                        };
                        building1.ObjectPlacement = objectPlacement1;


                        IfcBuilding building2 = new IfcBuilding(db, "building2")
                        {
                            Guid = new Guid("fbc7f4b2-177d-4875-88bb-d3b441151337")
                        };
                        building2.ObjectPlacement = objectPlacement2;

                        //Write inital File
                        db.WriteFile(".\\"+folder + "\\Initial_" + proname + ".ifc");

                        //continue with the updated version


                        IfcBuilding building3 = new IfcBuilding(db, "building3")
                        {
                            Guid = new Guid("fbc7f4b2-177d-4875-88bb-d3b441153531"),
                            ObjectPlacement= objectPlacement3
                        };
                        break;

                    case "Railway_spatial":
                        //initial
                        IfcSpatialZone spazone1 = new IfcSpatialZone(db, "Spatialzone1")
                        {
                            Guid = new Guid("42c7f4b2-177d-4875-88bb-d3b441153531"),
                            ObjectPlacement = objectPlacement1
                        };

                        IfcSpatialZone spazone2 = new IfcSpatialZone(db, "Spatialzone2")
                        {
                            Guid = new Guid("42c7f4b2-1337-4875-88bb-d3b441153531"),
                            ObjectPlacement=objectPlacement2
                        };
                        db.WriteFile(".\\" + folder + "\\Initial_" + proname + ".ifc");

                        //Update 
                        IfcSpatialZone spazone3 = new IfcSpatialZone(db, "Spatialzone3")
                        {
                            Guid = new Guid("42c7f4b2-1337-4422-88bb-d3b441153531"),
                            ObjectPlacement = objectPlacement3
                        };
                        IfcSpatialZone spazone4 = new IfcSpatialZone(db, "Spatialzone4")
                        {
                            Guid = new Guid("42c7f4b2-1337-4875-4242-d3b441153531"),
                            ObjectPlacement = objectPlacement4
                        };

                        break;
                    case "Spatial_01":
                        //creating building and add parts to it
                        IfcBuilding building4 = new IfcBuilding(db, "building4")
                        {
                            Guid = new Guid("fbc7f4b2-177d-4875-88bb-d3b44115bb42"),
                            ObjectPlacement = objectPlacement1
                        };

                        IfcBuildingStorey buildingsto1 = new IfcBuildingStorey(building4, "Level1", 0)
                        {
                            Guid = new Guid("42c74222-1337-4875-4242-d3b441153531"),
                            ObjectPlacement = objectPlacement2
                        };

                        building4.IsDecomposedBy.First().Guid = new Guid("42abcd22-1337-4875-4242-abcd41153531");

                        var profile1 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        IfcExtrudedAreaSolid extrudedAreaSolid1 = new IfcExtrudedAreaSolid(profile1, 1.35);
                        IfcProductDefinitionShape shape1 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid1));

                        IfcWall wall = new IfcWall(buildingsto1, objectPlacement3, shape1)
                        {
                            Guid=new Guid("42aa4222-1337-aaaa-4242-d3aa41153531"),
                            Name="Wall1"
                        };
                        wall.ContainedInStructure.Guid = new Guid("42c74222-1337-4875-4242-d3b44abcd531");


                        db.WriteFile(".\\" + folder + "\\Initial_" + proname + ".ifc");

                        //update
                        IfcBuildingStorey buildingsto2 = new IfcBuildingStorey(building4, "Level2", 0)
                        {
                            Guid = new Guid("42c74222-1337-4875-4242-bbbb41153531"),
                            ObjectPlacement = objectPlacement4
                        };
                        // Reassign Wall to Storey2
                        wall.ContainedInStructure.RelatingStructure = buildingsto2;

                        break;

                    case "Placement_Local":
                        //Initial set
                        var profile2 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        IfcExtrudedAreaSolid extrudedAreaSolid2 = new IfcExtrudedAreaSolid(profile2, 1.35);
                        IfcProductDefinitionShape shape2 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid2));
                            
                        //Wall at origin
                        IfcWall wall2 = new IfcWall(site, origionobjplace, shape2)
                        {
                            Guid = new Guid("42aa4222-1337-aaaa-4242-d3aa41153531"),
                            Name = "Wall1"
                        };
                        wall2.ContainedInStructure.Guid = new Guid("42cab222-1337-4875-4242-d3b44abcd531");

                        db.WriteFile(".\\" + folder + "\\Initial_" + proname + ".ifc");

                        //Update
                        IfcAxis2Placement3D wallaxisplace = new IfcAxis2Placement3D(new IfcCartesianPoint(db, 2, 3, 0));
                        wall2.ObjectPlacement= new IfcLocalPlacement(site.ObjectPlacement, wallaxisplace);


                        break;

                    default:
                        return;
                        //here add new test cases
                }

                db.WriteFile(".\\" + folder + "\\Update_" + proname + ".ifc");
            }
        }

    }
}




/* Backup files may needed later
 
     
     
            IfcAxis2Placement3D placement1 = new IfcAxis2Placement3D(new IfcCartesianPoint(dbInitial, 2, 5, 1));
			IfcLocalPlacement objectPlacement1 = new IfcLocalPlacement(site.ObjectPlacement, placement1);            
            
            IfcAxis2Placement3D placement2 = new IfcAxis2Placement3D(new IfcCartesianPoint(dbInitial, 10, 5, 1));
			IfcLocalPlacement objectPlacement2 = new IfcLocalPlacement(site.ObjectPlacement, placement2);

            
            var profile1 = new IfcRectangleProfileDef(dbInitial, "rectangleProfileDef", 4, 6);
            IfcExtrudedAreaSolid extrudedAreaSolid1 = new IfcExtrudedAreaSolid(profile1, 1.35);
            IfcProductDefinitionShape shape1 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid1));

            //var profile2 = new IfcRectangleProfileDef(dbInitial, "rectangleProfileDef", 4, 6);
            //IfcExtrudedAreaSolid extrudedAreaSolid2 = new IfcExtrudedAreaSolid(profile2, 1.35);
            //IfcProductDefinitionShape shape2 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid2));


            var proxy1 = new IfcBuildingElementProxy(site, objectPlacement1, shape1)
            {
                Name ="Cuboid1",
                Guid = new Guid("fbc7f4b2-177d-4875-88bb-d3b44115bbaa")
            };
            var proxy2 = new IfcBuildingElementProxy(site, objectPlacement2, shape1)
            {
                Name = "Cuboid2",
                Guid = new Guid("f8e196cb-c7d9-4d53-9885-0f687706727a")
            };

     
     
     
     
     
     
     
     
     
     
     
     
     
     */











