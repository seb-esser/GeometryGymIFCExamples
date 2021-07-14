using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryGym.Ifc;
using GeometryGym.STEP;

namespace ConsoleCreateAlignment
{
    class Program
    {
        static void Main(string[] args)
        {
            var ut_name = "Alignment-TUMAdsk-1";

            var db = new DatabaseIfc(ModelView.Ifc4X3NotAssigned);
            db.Factory.Options.GenerateOwnerHistory = false;

            var site = new IfcSite(db, "site");
           
            var project = new IfcProject(db, ut_name)
            {
                Name = ut_name, 
                Description = "sample for a single horizontal line segment - geometry + semantic part"
            };

            new IfcRelAggregates(project, new List<IfcObjectDefinition>() { site });

            // alignment
            var alignment = new IfcAlignment(site);
            alignment.AddComment("Entry point for alignment container");
            
            var horiz = new IfcAlignmentHorizontal(alignment);
            horiz.AddComment("groups all horizontal segments using an IfcRelNests rel");

            var pt1 = new IfcCartesianPoint(db, 3, 1, 0);

            var semanticPartOfTheSegment = new IfcAlignmentHorizontalSegment(
                pt1,
                0,
                0,
                0, 
                56.4,
                IfcAlignmentHorizontalSegmentTypeEnum.LINE);
            semanticPartOfTheSegment.AddComment("Semantic part of the line segment");
            
            var semanticSegment1 = new IfcAlignmentSegment(horiz, semanticPartOfTheSegment);
            semanticSegment1.AddComment("link semantic segment with horizontal alignment container");

            // build geometric part 
            var line = new IfcLine(pt1, new IfcVector(new IfcDirection(db, 0, 0), 1));
            line.AddComment("line geometry");
            var curveSeg = new IfcCurveSegment(IfcTransitionCode.CONTINUOUS, new IfcAxis1Placement(pt1), new IfcNonNegativeLengthMeasure(0), new IfcNonNegativeLengthMeasure(56.4), line);
            curveSeg.AddComment("Trim the entire line");

            var shapeRep = new IfcShapeRepresentation(curveSeg, ShapeRepresentationType.Curve2D);
            IfcProductDefinitionShape shape1 = new IfcProductDefinitionShape(shapeRep)
            {
                Name = "Horizontal alignment geom rep"
                
            };
            shape1.AddComment("link the geometric rep of a line to the representation of the horizontal segment");

            horiz.Representation = shape1;

            db.WriteFile(ut_name + ".ifc");
            
        }
    }
}
