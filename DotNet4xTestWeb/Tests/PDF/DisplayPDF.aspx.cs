﻿using System;
using System.IO;

namespace DotNet4xTestWeb.Tests.PDF
{
	public partial class DisplayPDF : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			WritePdf();
		}

		/// <summary>
		/// Write the PDF file to the response stream, but NOT as an attachment, and tell the browser
		/// to display it as a PDF (and to use the appropriate plug-in).
		/// </summary>
		/// <param name="strFileName">The PDF file to write to the response.</param>
		private void WritePdf()
		{
			string fileName = "Generated.pdf";
			Response.ContentType = "application/pdf";
			Response.AppendHeader("Content-Disposition", $"inline; filename={fileName}");

			if (File.Exists(Server.MapPath("/Tests/PDF/example.pdf")))
			{
				Response.TransmitFile(Server.MapPath("/Tests/PDF/example.pdf"));
				//using (var fileStream = File.Open(Server.MapPath("/Tests/PDF/example.xdp"), FileMode.Open))
				//using(var streamContent = new StreamContent(fileStream))
				//{ 
				//	Response.OutputStream.Write(streamContent.ReadAsByteArrayAsync().Result, 0, (int)fileStream.Length);
				//}
			}
			else
			{
				Response.Write("The PDF file did not exist on the server.");
			}

			Response.Flush();

			Response.End();
		}

	}
}