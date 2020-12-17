using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryGym.Ifc;

namespace ConsoleGettingStarted
{
    class Program
    {
        static void Main(string[] args)
        {
            var database = new DatabaseIfc(ModelView.Ifc4X3NotAssigned);
            database.Factory.ApplicationFullName = "GeometryGymIfc Getting started";

            var element = new IfcBuiltElement(database);
            element.AddComment("some comment");

            database.WriteFile("myIfcFile.ifc");

        }
    }
}
