//============================================================================== 

// SOURCE CODE BY TREEBHOPH THOOMSAN / treebhoph@yahoo.co.th 
// This library is free software; you can redistribute it and/or 
// modify it under the terms of the GNU Lesser General Public 
// License as published by the Free Software Foundation; either 
// version 2.1 of the License, or (at your option) any later version. 
// 
// This library is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// Lesser General Public License for more details. 
// 
// You should have received a copy of the GNU Lesser General Public 
// License along with this library; if not, write to the Free Software 
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 
//============================================================================== 


using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Net.Mail;
using System.Web.Security;
using System.Text.RegularExpressions;

namespace SmartLib.Database.Web
{
	public class OpenConnectionInView : IHttpModule, IDisposable
	{
		~OpenConnectionInView() {

			this.DisposeMemory();
		}

		public void Init(HttpApplication context) {
			context.EndRequest += this.EndRequest;
		}

		public void EndRequest(object sender, EventArgs e) {
			Db.CloseAllConnections();
		}

		public void Dispose() {
			this.DisposeMemory();
		}

		private void DisposeMemory() {
			GC.SuppressFinalize(this);
		}
	}


}
