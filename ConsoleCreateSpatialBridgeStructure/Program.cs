using GeometryGym.Ifc;

namespace ConsoleCreateSpatialBridgeStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DatabaseIfc(ModelView.Ifc4X3NotAssigned);
            var site = new IfcSite(db, null)
            {
                Name = "Skärholmen (Delomrade)"
            };
            var project = new IfcProject(site, "BridgeProject")
            {
                Name = "E4 Förbifart Stockholm (Program)"
            };

            var bridge = new IfcBridge(site, null, null);

            new IfcPropertySet(bridge, "Anläggningsdelar");
            new IfcPropertySet(bridge, "K-nummer");
            new IfcPropertySet(bridge, "granskning");
            new IfcPropertySet(bridge, "status");

            var superstructure = new IfcFacilityPart(bridge, "överbyggnad (31--)", new IfcFacilityPartTypeSelect(IfcBridgePartTypeEnum.SUPERSTRUCTURE), IfcFacilityUsageEnum.VERTICAL );
            var substructure = new IfcFacilityPart(bridge, "underbyggnad (12--)", new IfcFacilityPartTypeSelect(IfcBridgePartTypeEnum.SUBSTRUCTURE), IfcFacilityUsageEnum.VERTICAL );
            var foundation = new IfcFacilityPart(bridge, "undergrund (11--)", new IfcFacilityPartTypeSelect(IfcBridgePartTypeEnum.FOUNDATION), IfcFacilityUsageEnum.VERTICAL );
            var underground = new IfcFacilityPart(bridge, "Anläggningskomplettering (..--)", new IfcFacilityPartTypeSelect(IfcFacilityPartCommonTypeEnum.BELOWGROUND), IfcFacilityUsageEnum.VERTICAL );

            db.WriteFile("AM_SpatialBreakdown.ifc");

        }
    }
}
