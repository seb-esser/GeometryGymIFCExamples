using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryGym.Ifc;

namespace testConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			DatabaseIfc db = new DatabaseIfc(Console.In);
			IfcProject project = db.Project;
			IfcSpatialElement rootElement = project.RootElement();
			List<IfcBuiltElement> elements = project.Extract<IfcBuiltElement>();
			List<IfcFacetedBrep> breps = new List<IfcFacetedBrep>();
			foreach(var element in elements)
			{
				var representation = element.Representation;
				if (representation != null)
				{
					foreach (var rep in representation.Representations)
					{
						IfcShapeRepresentation sr = rep as IfcShapeRepresentation;
						if (sr != null)
						{
							foreach (IfcRepresentationItem item in sr.Items)
							{
								IfcFacetedBrep fb = item as IfcFacetedBrep;
								if (fb != null)
									breps.Add(fb);
							}

						}
					}
				}
			}
		}
	}
}
