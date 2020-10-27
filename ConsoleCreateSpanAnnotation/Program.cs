﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryGym.Ifc;

namespace ConsoleCreateSpanAnnotation
{
    class Program
    {
        static void Main(string[] args)
        {
            var database = new DatabaseIfc(ModelView.Ifc4X3NotAssigned);
           

            // basic setup
            var site = new IfcSite(database, "SiteA");
            var project = new IfcProject(
                site,
                "SampleProject with a span annotation",
                IfcUnitAssignment.Length.Metre
                );

            // add a simple alignment
            var horizSegment = new IfcAlignment2DHorizontalSegment(
                new IfcCircularArcSegment2D(
                    new IfcCartesianPoint(
                        database,
                        2,
                        3),
                    20/180 * Math.PI,
                    580.0776, 
                    400, 
                    true));

            var verticalSegment = new IfcAlignment2DVerSegLine(
                     database,
                     0,
                     580.0776,
                     1.25,
                     0
                     );

            var alignmentCurve = new IfcAlignmentCurve(
                new IfcAlignment2DHorizontal(new List<IfcAlignment2DHorizontalSegment>
                {
                    horizSegment
                }),
                new IfcAlignment2DVertical(new List<IfcAlignment2DVerticalSegment>
                {
                    verticalSegment
                })
                );

            var alignment = new IfcAlignment(site, alignmentCurve)
            {
                Name = "sampleAlignment",
                Description = "some basic alignment data to demonstrate an IfcAnnotation with some SpanPlacement",
                ObjectPlacement = new IfcLocalPlacement(
                    new IfcAxis2Placement3D(
                        new IfcCartesianPoint(database, 0, 0, 0)
                        )
                    ),
                PredefinedType = IfcAlignmentTypeEnum.NOTDEFINED
            };

            // create an annotation
            var annotation = new IfcAnnotation(alignment)
            {
                Name = "DesignSpeed",
                Description = "annotate the given alignment curve with some speed values",
                ObjectType = "Magic"
            };

            var spanPlacement = new IfcLinearSpanPlacement(
                alignmentCurve,
                new IfcDistanceExpression(database, 35),
                285)
            {
                
            };
            annotation.ObjectPlacement = spanPlacement;


            var pSet = new IfcPropertySet(annotation, "PSET_SpeedData", new List<IfcProperty>
            {
                new IfcPropertySingleValue(database, "CargoSpeed", 80.0),
                new IfcPropertySingleValue(database, "DesignSpeed", 160.0)
            });

            // set some copyright things for DEPL
            database.OfType<IfcOrganization>().First().Name =
                "Chair of Computational Modeling and Simulation, Technical University of Munich";
            
            database.WriteFile("AlignmentWithSpanAnnotation.ifc");
        }
    }
}
