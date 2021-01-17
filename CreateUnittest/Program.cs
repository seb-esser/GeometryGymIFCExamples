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
        /// Railway_spatial\n Spatial_01\nPlacement_Local"\nGeomRepresentation_01\n
        /// \nGeomRepresentation_02\nGeomRepresentation_03\nGeomRepresentation_04
        /// \nLocalPlacement_01\nLinearPlacement_02
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
            testcasename.Add("GeomRepresentation_01");
            testcasename.Add("GeomRepresentation_02");
            testcasename.Add("GeomRepresentation_03");
            testcasename.Add("GeomRepresentation_04");
            //testcasename.Add("GeomRepresentation_05");
            testcasename.Add("LocalPlacement_01");
            testcasename.Add("LinearPlacement_02");
            System.IO.Directory.CreateDirectory(".\\" + folder);


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
                        building1 = new IfcBuilding(db, "building1")
                        {
                            Guid = new Guid("fbc7f4b2-177d-4875-88bb-d3b44115bb42"),
                            ObjectPlacement = objectPlacement1
                        };

                        IfcBuildingStorey buildingsto1 = new IfcBuildingStorey(building1, "Level1", 0)
                        {
                            Guid = new Guid("42c74222-1337-4875-4242-d3b441153531"),
                            ObjectPlacement = objectPlacement2
                        };

                        building1.IsDecomposedBy.First().Guid = new Guid("42abcd22-1337-4875-4242-abcd41153531");

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
                        IfcBuildingStorey buildingsto2 = new IfcBuildingStorey(building1, "Level2", 0)
                        {
                            Guid = new Guid("42c74222-1337-4875-4242-bbbb41153531"),
                            ObjectPlacement = objectPlacement4
                        };
                        // Reassign Wall to Storey2
                        wall.ContainedInStructure.RelatingStructure = buildingsto2;

                        break;

                    case "Placement_Local":
                        //Initial set
                        profile1 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        extrudedAreaSolid1 = new IfcExtrudedAreaSolid(profile1, 1.35);
                        shape1 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid1));
                            
                        //Wall at origin
                        IfcWall wall2 = new IfcWall(site, origionobjplace, shape1)
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

                    case "GeomRepresentation_01":
                        //init
                        profile1 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        extrudedAreaSolid1 = new IfcExtrudedAreaSolid(profile1, 1.35);
                        shape1 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid1));

                        IfcRectangleProfileDef profile2 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        IfcExtrudedAreaSolid extrudedAreaSolid2 = new IfcExtrudedAreaSolid(profile2, 1.35);
                        IfcProductDefinitionShape shape2 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid2));


                        var proxy1 = new IfcBuildingElementProxy(site, objectPlacement1, shape1)
                        {
                            Name = "Cuboid1",
                            Guid = new Guid("fbc7f4b2-177d-4875-88bb-d3abed15bbaa")
                        };
                        proxy1.ContainedInStructure.Guid = new Guid("000ab222-1337-4875-4242-d3b44abcd531");

                        var proxy2 = new IfcBuildingElementProxy(site, objectPlacement2, shape2)
                        {
                            Name = "Cuboid2",
                            Guid = new Guid("f8e196cb-c7d9-4d53-9885-0f687706abcd")
                        };
                        proxy2.ContainedInStructure.Guid = new Guid("42cab222-1337-4875-4242-00004abcd531");


                        db.WriteFile(".\\" + folder + "\\Initial_" + proname + ".ifc");
                        //Update
                        proxy2.Representation = shape1;
                        break;

                    case "GeomRepresentation_02":
                        //init
                        profile1 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        extrudedAreaSolid1 = new IfcExtrudedAreaSolid(profile1, 1.35);
                        shape1 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid1));

                        profile2 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        extrudedAreaSolid2 = new IfcExtrudedAreaSolid(profile2, 1.35);
                        shape2 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid2));

                        proxy1 = new IfcBuildingElementProxy(site, objectPlacement1, shape1)
                        {
                            Name = "Cuboid1",
                            Guid = new Guid("fbcba4b2-1c7d-4875-88bb-d3abed15bbaa")
                        };
                        proxy1.ContainedInStructure.Guid = new Guid("000ab222-1337-4875-4242-d3b00abcd531");
                        proxy2 = new IfcBuildingElementProxy(site, objectPlacement2, shape2)
                        {
                            Name = "Cuboid2",
                            Guid = new Guid("f821963b-c7d9-4d53-9885-0f87cd6abcda")
                        };
                        proxy1.ContainedInStructure.Guid = new Guid("000ab222-1337-4875-4242-d3414abcd531");
                        db.WriteFile(".\\" + folder + "\\Initial_" + proname + ".ifc");

                        //Update
                        extrudedAreaSolid2.Depth = 3;
                        break;

                    case "GeomRepresentation_03":
                        //init
                        profile1 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        extrudedAreaSolid1 = new IfcExtrudedAreaSolid(profile1, 1.35);
                        shape1 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid1));

                        proxy1 = new IfcBuildingElementProxy(site, objectPlacement1, shape1)
                        {
                            Name = "Cuboid1",
                            Guid = new Guid("fbcba4b2-1c7d-0000-88bb-d3abed15bbaa")
                        };
                        proxy1.ContainedInStructure.Guid = new Guid("00011222-1337-4875-4242-d1114abcd531");
                        db.WriteFile(".\\" + folder + "\\Initial_" + proname + ".ifc");

                        //Update
                        var profile3 = new IfcCircleProfileDef(db, "CylinderProfileDef", 4);
                        var extrudedAreaSolid3 = new IfcExtrudedAreaSolid(profile3, 1.35);
                        var shape3 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid3));
                        proxy1.Representation = shape3;

                        break;

                    case "GeomRepresentation_04":
                        //init
                        profile1 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        extrudedAreaSolid1 = new IfcExtrudedAreaSolid(profile1, 1.35);
                        shape1 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid1));

                        proxy1 = new IfcBuildingElementProxy(site, objectPlacement1, shape1)
                        {
                            Name = "Cuboid1",
                            Guid = new Guid("fbcba4b2-1c7d-0000-88bb-d3abed15bbaa")
                        };
                        proxy1.ContainedInStructure.Guid = new Guid("00011222-1337-4875-4242-d1114abcd531");

                        db.WriteFile(".\\" + folder + "\\Initial_" + proname + ".ifc");

                        //Update

                        var points = new List<IfcCartesianPoint>();
                        points.Add(new IfcCartesianPoint(db, -2, -3, 0));
                        points.Add(new IfcCartesianPoint(db, -2, 3, 0));
                        points.Add(new IfcCartesianPoint(db, 2, 3, 0));
                        points.Add(new IfcCartesianPoint(db, 2, -3, 0));
                        points.Add(new IfcCartesianPoint(db, -2, -3, 1.35));
                        points.Add(new IfcCartesianPoint(db, -2, 3, 1.35));
                        points.Add(new IfcCartesianPoint(db, 2, 3, 1.35));
                        points.Add(new IfcCartesianPoint(db, 2, -3, 1.35));

                        var polyloop = new List<IfcPolyLoop>();
                        polyloop.Add(new IfcPolyLoop(points[0], points[1], points[2], points[3]));
                        polyloop.Add(new IfcPolyLoop(points[0], points[4], points[5], points[1]));
                        polyloop.Add(new IfcPolyLoop(points[0], points[4], points[7], points[3]));
                        polyloop.Add(new IfcPolyLoop(points[4], points[5], points[6], points[7]));
                        polyloop.Add(new IfcPolyLoop(points[1], points[5], points[6], points[2]));
                        polyloop.Add(new IfcPolyLoop(points[3], points[7], points[6], points[2]));

                        var faceouter = new List<IfcFaceOuterBound>();
                        foreach (var i in polyloop)
                        {
                            faceouter.Add(new IfcFaceOuterBound(i, true));
                        }

                        var face = new List<IfcFace>();
                        foreach (var j in faceouter)
                        {
                            face.Add(new IfcFace(j));
                        }

                        var closedshell = new IfcClosedShell(face);
                        var brep = new IfcFacetedBrep(closedshell);

                        shape2 = new IfcProductDefinitionShape(new IfcShapeRepresentation(brep));
                        proxy1.Representation = shape2;
                        break;

                    case "GeomRepresentation_05":
                        var points1 = new List<IfcCartesianPoint>();
                        points1.Add(new IfcCartesianPoint(db, -2, -3, 0));
                        points1.Add(new IfcCartesianPoint(db, -2, 3, 0));
                        points1.Add(new IfcCartesianPoint(db, 2, 3, 0));
                        points1.Add(new IfcCartesianPoint(db, 2, -3, 0));
                        var points2 = new List<IfcCartesianPoint>();
                        points2.Add(new IfcCartesianPoint(db, -2, -3, 1.35));
                        points2.Add(new IfcCartesianPoint(db, -2, 3, 1.35));
                        points2.Add(new IfcCartesianPoint(db, 2, 3, 1.35));
                        points2.Add(new IfcCartesianPoint(db, 2, -3, 1.35));

                        polyloop = new List<IfcPolyLoop>();
                        polyloop.Add(new IfcPolyLoop(points1));
                        polyloop.Add(new IfcPolyLoop(points2));


                        break;


                    case "LocalPlacement_01":
                        //init
                        profile1 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        extrudedAreaSolid1 = new IfcExtrudedAreaSolid(profile1, 1.35);
                        shape1 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid1));

                        profile2 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        extrudedAreaSolid2 = new IfcExtrudedAreaSolid(profile2, 1.35);
                        shape2 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid2));

                        proxy1 = new IfcBuildingElementProxy(site, objectPlacement1, shape1)
                        {
                            Name = "Cuboid1",
                            Guid = new Guid("fbcba4b2-1c7d-4815-88bb-d3abed15bbaa")
                        };
                        proxy1.ContainedInStructure.Guid = new Guid("000ab222-1337-4875-4242-d3b00abcd531");
                        proxy2 = new IfcBuildingElementProxy(site, objectPlacement2, shape2)
                        {
                            Name = "Cuboid2",
                            Guid = new Guid("f821963b-c7d9-4d53-9825-0f87cd6abcda")
                        };
                        proxy1.ContainedInStructure.Guid = new Guid("000ab242-1337-4875-4242-d3414abcd531");
                        db.WriteFile(".\\" + folder + "\\Initial_" + proname + ".ifc");
                        //update
                        var point1 = new IfcCartesianPoint(db, 2, 2, 2);
                        placement1.Location = point1;


                        break;

                    case "LinearPlacement_02":

                        profile1 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        extrudedAreaSolid1 = new IfcExtrudedAreaSolid(profile1, 1.35);
                        shape1 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid1));

                        profile2 = new IfcRectangleProfileDef(db, "rectangleProfileDef", 4, 6);
                        extrudedAreaSolid2 = new IfcExtrudedAreaSolid(profile2, 1.35);
                        shape2 = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrudedAreaSolid2));


                        IfcLine line1 = new IfcLine(new IfcCartesianPoint(db, 1, 1, 1)
                            , new IfcVector(new IfcDirection(db, 1, 1), 20));

                        IfcLine line2 = new IfcLine(new IfcCartesianPoint(db, 1, -1, -1)
                            , new IfcVector(new IfcDirection(db, -1, 1), 20));

                        var alignment1 = new IfcAlignment(site, line1)
                        {
                            Name = "Alignment1",
                            Guid = new Guid("fbcba4b2-1cdd-48d5-88bb-d3aced15bbaa")
                        };
                        var alignment2 = new IfcAlignment(site, line2) 
                        {
                            Name = "Alignment2",
                            Guid = new Guid("f111a4b2-1c7d-4815-88bb-d3aced15bbaa")
                        };

                        var distn1 = new IfcPointByDistanceExpression(4 ,line1);
                        var distn2 = new IfcPointByDistanceExpression(2 ,line2);

                        //Deprecated but should be right for IFC4
                        var linearplace1 = new IfcLinearPlacement(line1, distn1)
                        {
                            Orientation = new IfcOrientationExpression(new IfcDirection(db, 0.5, 0.24),
                            new IfcDirection(db, -0.24, 0.5))
                        };
                        var linearplace2 = new IfcLinearPlacement(line2, distn2)
                        {
                            Orientation = new IfcOrientationExpression(new IfcDirection(db, 0.75, 0.24),
                            new IfcDirection(db, -0.24, 0.66))
                        };

                        proxy1 = new IfcBuildingElementProxy(site, linearplace1, shape1)
                        {
                            Name = "Cuboid1",
                            Guid = new Guid("fbcba4b2-1c7d-4815-88bb-d3aced15bbaa")
                        };
                        proxy1.ContainedInStructure.Guid = new Guid("000cc222-1337-4875-4242-d3b00abcd531");
                        proxy2 = new IfcBuildingElementProxy(site, linearplace2, shape2)
                        {
                            Name = "Cuboid2",
                            Guid = new Guid("f821963b-c7d9-4d53-98a5-0f8ccd6abcda")
                        };
                        proxy1.ContainedInStructure.Guid = new Guid("000ccc42-1337-4875-4242-d3414abcd531");
                        db.WriteFile(".\\" + folder + "\\Initial_" + proname + ".ifc");

                        //Update

                        IfcLine line3 = new IfcLine(new IfcCartesianPoint(db, 0, 1, 0)
                             , new IfcVector(new IfcDirection(db, 0,5, 2), 15));
                        var distn3 = new IfcPointByDistanceExpression(4, line3);

                        linearplace1.PlacementMeasuredAlong = line3;
                        linearplace1.Distance = distn3;


                        break;


                    default:
                        //exit
                        return;
                        
                }

                db.WriteFile(".\\" + folder + "\\Update_" + proname + ".ifc");
            }
        }

    }
}











