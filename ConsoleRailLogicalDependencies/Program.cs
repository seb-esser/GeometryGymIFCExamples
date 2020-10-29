using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryGym.Ifc;


namespace ConsoleRailLogicalDependencies
{
    class Program
    {
        static void Main(string[] args)
        {
            // create database 
            var database = new DatabaseIfc(ModelView.Ifc4X3NotAssigned);

            // create IfcSite instance
            var site = new IfcSite(database, "Station|Area");

            // create top-most spatial structure element IfcProject, set units and assign facility to project
            var project = new IfcProject(site, "Reference Site 'Fahrstrasse'", IfcUnitAssignment.Length.Metre) { Description = "Example representing a 'Fahrstrasse' with corresponding 'Fahrweg' and 'DWeg' referenced on top of an alignment curve" };


            // -- create facility representing the logical unit of road-bridge-road -- 
            var trafficFacility = new IfcFacility(site, "TrackA")
            {
                CompositionType = IfcElementCompositionEnum.COMPLEX
            };

            var fahrstrasse = new IfcFacilityPart(
                trafficFacility,
                "Fahrstrasse", 
                new IfcFacilityPartTypeSelect(IfcRailwayPartTypeEnum.TRACKSTRUCTURE), 
                IfcFacilityUsageEnum.LONGITUDINAL);

            // properties to fahrstrasse
            List<IfcProperty> fahrstraIfcProperties = new List<IfcProperty>();
            fahrstraIfcProperties.Add(new IfcPropertySingleValue(database, "Fahrstrasse_PropertyA", new IfcIdentifier("ABC")));
            fahrstraIfcProperties.Add(new IfcPropertySingleValue(database, "TrainInCorridor", new IfcBoolean(true)));

            new IfcPropertySet(fahrstrasse, "CustomProperties", fahrstraIfcProperties);

            var fahrweg = new IfcSpatialZone(fahrstrasse, "Fahrweg")
            {
                Description = "logical representation of 'fahrweg'"
            };

            var dWeg = new IfcSpatialZone(fahrstrasse, "DWeg")
            {
                Description = "logical representation of 'Durchrutschweg'"
            };

            // system
            var system = new IfcDistributionSystem(site, "SecurityDistribution", IfcDistributionSystemEnum.SECURITY);

            // signals
            var startSignal = new IfcSignal(fahrstrasse, null, null, system)
            {
                Name = "StartSignal",
                Description = "Physical Representation of 'StartSignal' assigned to a 'Fahrstrasse'"
            };

            var endSignal = new IfcSignal(fahrstrasse, null, null, system)
            {
                Name = "EndSignal",
                Description = "Physical Representation of 'EndSignal' assigned to a 'Fahrstrasse'"
            };

            // properties to signal
            List<IfcProperty> startSignalIfcProperties = new List<IfcProperty>();
            startSignalIfcProperties.Add(new IfcPropertySingleValue(database, "SignalType", new IfcIdentifier("Main Signal")));
            startSignalIfcProperties.Add(new IfcPropertySingleValue(database, "InService", new IfcBoolean(true)));
            
            new IfcPropertySet(startSignal, "CustomProperties", startSignalIfcProperties);


            // Fahrweg - equipment
            var equipmentFahrweg01 = new IfcBuiltElement(fahrweg, null, null) { Name = "Fahrweg-Equipment01", Description = "Additional components assigned to logical unit of 'Fahrweg'"};
            var equipmentFahrweg02 = new IfcBuiltElement(fahrweg, null, null) { Name = "Fahrweg-Equipment02", Description = "Additional components assigned to logical unit of 'Fahrweg'"};


           
            

            // --- alignment ---
            var alignmentCurve = new IfcAlignmentCurve(database);
            
            var alignment = new IfcAlignment(site, alignmentCurve)
            {
                Name = "Alignment representation - 2D only",
                Description = "Tweaked geometry but used to represent a topology approach"
            };

            var horizSegment = new IfcAlignment2DHorizontalSegment(
                new IfcLineSegment2D(
                    new IfcCartesianPoint(database, 5, 10),
                    0,
                    400));

            alignmentCurve.Horizontal = new IfcAlignment2DHorizontal(new List<IfcAlignment2DHorizontalSegment>{horizSegment});

            // assigning fahrweg und DWeg to alignment, placing signals
            fahrweg.ObjectPlacement = new IfcLinearSpanPlacement(
                alignmentCurve, 
                new IfcDistanceExpression(database, 0), 250);

            dWeg.ObjectPlacement = new IfcLinearSpanPlacement(
                alignmentCurve, 
                new IfcDistanceExpression(database, 250),100 );

            startSignal.ObjectPlacement = new IfcLinearPlacement(alignmentCurve, new IfcDistanceExpression(database, 0));
            endSignal.ObjectPlacement = new IfcLinearPlacement(alignmentCurve, new IfcDistanceExpression(database, 250));

            database.WriteFile("fahrstrasse.ifc");
        }
    }
}
