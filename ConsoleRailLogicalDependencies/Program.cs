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
            var site = new IfcSite(database, "sampleSite");

            // create top-most spatial structure element IfcProject, set units and assign facility to project
            var project = new IfcProject(site, "myProject", IfcUnitAssignment.Length.Metre);


            // -- create facility representing the logical unit of road-bridge-road -- 
            var trafficFacility = new IfcFacility(site, "RailwaySystem")
            {
                CompositionType = IfcElementCompositionEnum.COMPLEX
            };

            var fahrstrasse = new IfcFacilityPart(
                trafficFacility,
                "Fahrstrasse", 
                new IfcFacilityPartTypeSelect(IfcRailwayPartTypeEnum.TRACKSTRUCTURE), 
                IfcFacilityUsageEnum.LONGITUDINAL);

            var fahrweg = new IfcSpatialZone(fahrstrasse, "Fahrweg")
            {
                Description = "logical representation of 'fahrweg'"
            };

            var dWeg = new IfcSpatialZone(fahrstrasse, "DWeg")
            {
                Description = "logical representation of 'Durchrutschweg'"
            };


            // signals
            var startSignal = new IfcBuiltElement(fahrstrasse, null, null)
            {
                Name = "StartSignal",
                Description = "logical Representation of 'StartSignal' assigned to a 'Fahrstrasse'"
            };

            var endSignal = new IfcBuiltElement(fahrstrasse, null, null)
            {
                Name = "EndSignal",
                Description = "logical Representation of 'EndSignal' assigned to a 'Fahrstrasse'"
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




            database.WriteFile("fahrstrasse.ifc");
        }
    }
}
