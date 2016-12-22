using System.IO;
using System.Web.UI.WebControls;
using SobekCM.Core.BriefItem;
using SobekCM.Core.FileSystems;
using SobekCM.Core.Navigation;
using SobekCM.Core.Users;
using SobekCM.Engine_Library.Configuration;
using SobekCM.Library.ItemViewer.Viewers;
using SobekCM.Library.UI;
using SobekCM.Tools;

namespace RTI
{
    public class RTI_ItemViewer : abstractNoPaginationItemViewer
    {
        /// <summary> Constructor for a new instance of the RTI_ItemViewer class, used to display a RTI file from a digital resource </summary>
        /// <param name="BriefItem"> Digital resource object </param>
        /// <param name="CurrentUser"> Current user, who may or may not be logged on </param>
        /// <param name="CurrentRequest"> Information about the current request </param>
        /// <param name="Tracer"> Trace object keeps a list of each method executed and important milestones in rendering </param>
        public RTI_ItemViewer(BriefItemInfo BriefItem, User_Object CurrentUser, Navigation_Object CurrentRequest, Custom_Tracer Tracer)
        {
            this.BriefItem = BriefItem;
            this.CurrentRequest = CurrentRequest;
            this.CurrentUser = CurrentUser;
        }

        /// <summary> Write the item viewer main section as HTML directly to the HTTP output stream </summary>
        /// <param name="Output"> Response stream for the item viewer to write directly to </param>
        /// <param name="Tracer"> Trace object keeps a list of each method executed and important milestones in rendering </param>
        public override void Write_Main_Viewer_Section(TextWriter Output, Custom_Tracer Tracer)
        {
            Output.WriteLine("<script type=\"text/javascript\">");
            Output.WriteLine("  $('#itemNavForm').prop('action','').submit(function(event){ event.preventDefault(); });");
            Output.WriteLine("</script>");

            // Is there a rti subfolder with an info.xml file in it?  Otherwise, use the sample currently
            string source_url = CurrentRequest.Base_URL + "plugins/RTI/sample";
            string network_location = SobekFileSystem.Resource_Network_Uri(BriefItem);
            if (Directory.Exists(Path.Combine(network_location, "rti")))
            {
                // Does an info.xml file exist?
                if (File.Exists(Path.Combine(network_location, "rti", "info.xml")))
                {
                    source_url = UI_ApplicationCache_Gateway.Settings.Servers.Image_URL + SobekFileSystem.AssociFilePath(BriefItem).Replace("\\","/") + "rti";
                }
            }

            Output.WriteLine("    <td>");
            Output.WriteLine("		<div id=\"viewerCont\">");
			Output.WriteLine("        <script  type=\"text/javascript\">");
			Output.WriteLine("        	createRtiViewer(\"viewerCont\", \"" + source_url + "\", 900, 600); ");
            Output.WriteLine("        </script>");
		    Output.WriteLine("		</div>");
            Output.WriteLine("    </td>");
        }

        /// <summary> Allows controls to be added directory to a place holder, rather than just writing to the output HTML stream </summary>
        /// <param name="MainPlaceHolder"> Main place holder ( &quot;mainPlaceHolder&quot; ) in the itemNavForm form into which the bulk of the item viewer's output is displayed</param>
        /// <param name="Tracer"> Trace object keeps a list of each method executed and important milestones in rendering </param>
        /// <remarks> This method does nothing, since nothing is added to the place holder as a control for this item viewer </remarks>
        public override void Add_Main_Viewer_Section(PlaceHolder MainPlaceHolder, Custom_Tracer Tracer)
        {
            // Do nothing
        }

        /// <summary> Write any additional values within the HTML Head of the final served page </summary>
        /// <param name="Output"> Output stream currently within the HTML head tags </param>
        /// <param name="Tracer"> Trace object keeps a list of each method executed and important milestones in rendering </param>
        /// <remarks> By default this does nothing, but can be overwritten by all the individual item viewers </remarks>
        public override void Write_Within_HTML_Head(TextWriter Output, Custom_Tracer Tracer)
        {
            Output.WriteLine("  <link rel=\"stylesheet\" type=\"text/css\" href=\"" + CurrentRequest.Base_URL + "plugins/RTI/css/ui-lightness/jquery-ui-1.10.3.custom.css\" />");
            Output.WriteLine("  <link rel=\"stylesheet\" type=\"text/css\" href=\"" + CurrentRequest.Base_URL + "plugins/RTI/css/webrtiviewer.css\"/>");

            Output.WriteLine("  <script type=\"text/javascript\" src=\"" + CurrentRequest.Base_URL + "plugins/RTI/js/jquery-ui.js\"></script>");
            Output.WriteLine("  <script type=\"text/javascript\" src=\"" + CurrentRequest.Base_URL + "plugins/RTI/spidergl/spidergl.js\"></script>");
            Output.WriteLine("  <script type=\"text/javascript\" src=\"" + CurrentRequest.Base_URL + "plugins/RTI/spidergl/multires.js\"></script>");
        }
    }
}