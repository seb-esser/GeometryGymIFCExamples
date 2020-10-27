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


            database.WriteFile("fahrstrasse.ifc");
        }
    }
}
