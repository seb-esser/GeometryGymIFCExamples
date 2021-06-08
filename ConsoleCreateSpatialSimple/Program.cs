using System;
using GeometryGym.Ifc;

namespace ConsoleCreateSpatialSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            // create database 
            var database = new DatabaseIfc(ModelView.Ifc4NotAssigned);

            // create IfcSite instance
            var site = new IfcSite(database, "Site entity");

            // create top-most spatial structure element IfcProject, set units and assign facility to project
            var project = new IfcProject(site, "Project entity");

            database.WriteFile("spatial.ifc");

        }
    }
}
