using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mywebform
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int statecode = int.Parse(Request.QueryString.Get("code"));
            throw new HttpException(statecode, "not found!");

        }
    }
}