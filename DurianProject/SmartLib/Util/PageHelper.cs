using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace SmartLib.Util
{
    class PageHelper
    {
        public static void FocusControlOnPageLoad(string ClientID, Page page)
        {
            
            page.RegisterClientScriptBlock("CtrlFocus",
                                            @"<script> 

                                              function ScrollView()
                                              {
                                                 var el = document.getElementById('" + ClientID + @"')
                                                 if (el != null)
                                                 {        
                                                    el.scrollIntoView();
                                                    el.focus();
                                                 }
                                              }

                                              window.onload = ScrollView;
                                              </script>");

        }
    }
}
