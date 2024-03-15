using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace EcoArtisanFinal
{
    public partial class SphereMaker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["sceneSetupCode"] != null)
                {
                    string sceneSetupCode = Request.QueryString["sceneSetupCode"];
                    string decodedData = Server.UrlDecode(sceneSetupCode);

                    // Now you can use decodedData as your configuration JSON
                    // Depending on your requirements, you might directly assign it to a control,
                    // or parse it to use in some other way.
                }
            }
        }
    }
}