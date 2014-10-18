using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLib.Printer
{
	public enum TextAlign
	{
		Left,
		Center,
		Right
	}

	public class PrintSlipTextBuilder
	{
		private const Int32 MAX_TEXT_LEGTH = 40;
		private StringBuilder strText;
		private Int32 lineLength = 0;

		public PrintSlipTextBuilder()
		{
			this.strText = new StringBuilder();
		}

		public override string ToString()
		{
			return this.strText.ToString();
		}

		public void Append(string strText)
		{
			this.Append(strText, TextAlign.Left);
		}

		public void Append(string strText, TextAlign align)
		{

			string userDelimeter = "\n";
			char programDelimeter = Convert.ToChar(693);
			string allTxt = strText.Replace(userDelimeter, programDelimeter.ToString());
			string[] txts = allTxt.Split(programDelimeter);

			if (txts.Length > 0)
			{
				Int32 i = 1;
				foreach (string x in txts)
				{

					if ((x.Trim().Length > 0))
					{

						if ((i > 1))
						{
							this.InsertNewLine();
						}

						string txt = this.GetTextFormatted(x, align, 0);
						this.strText.Append(txt);
						if (this.strText.Length > 0)
						{
							lineLength = lineLength + this.CountChar(txt);
						}

					}
					i = i + 1;
				}
			}
			else
			{
				string txt = this.GetTextFormatted(strText, align, 0);
				this.strText.Append(txt);

				if (this.strText.Length > 0)
				{
					lineLength = lineLength + this.CountChar(txt);
				}
			}

		}


		public void Append(string strText, TextAlign align, Int32 paddingSpace)
		{

			string userDelimeter = "\n";
			char programDelimeter = Convert.ToChar(693);
			string allTxt = strText.Replace(userDelimeter, programDelimeter.ToString());
			string[] txts = allTxt.Split(programDelimeter);
				
			if (txts.Length > 0)
			{
				Int32 i = 1;
				foreach (string x in txts)
				{
					if ((x.Trim().Length > 0))
					{

						if ((i > 1))
						{
							this.InsertNewLine();
						}

						string txt = this.GetTextFormatted(x.Trim(), align, paddingSpace);
						this.strText.Append(txt);

						if (this.strText.Length > 0)
						{
							lineLength = lineLength + this.CountChar(txt) + paddingSpace;
						}

					}
					i = i + 1;
				}
			}
			else
			{
				string txt = this.GetTextFormatted(strText, align, paddingSpace);
				this.strText.Append(txt);

				if (this.strText.Length > 0)
				{
					lineLength = lineLength + this.CountChar(txt) + paddingSpace;
				}
			}

		}

		public void AppendLine(string strText)
		{
			this.AppendLine(strText, TextAlign.Left);
		}

		public void AppendLine(string strText, TextAlign align)
		{
			this.Append(strText, align);
			this.InsertNewLine();
		}

		public void AppendLine(string strText, TextAlign align, Int32 paddingSpace)
		{
			this.Append(strText, align, paddingSpace);
			this.InsertNewLine();
		}

		public void Clear()
		{
			this.strText.Remove(0, this.strText.Length);
		}

		public void Finish()
		{
			this.InsertNewLine(7);
			this.InsertPaperCut();
		}


		public void InsertNewLine()
		{
			this.strText.Append("\r\n");
			this.lineLength = 0;
		}

		public void InsertNewLine(Int32 line)
		{
			for (Int32 i = 0; i <= line; i++)
			{
				this.strText.Append("\r\n");
			}

			this.lineLength = 0;
		}


		private int CountChar(string strText)
		{
			Int32 chLength = strText.Length - 1;
			Int32 count = 0;
			Int32 ascii = -1;

			char[] arrStrText = strText.ToCharArray();

			for (Int16 i = 0; i <= chLength; i++)
			{
				ascii = Convert.ToInt32(arrStrText[i]);
				if (!(ascii == 209 | (ascii > 211 & ascii < 218) | (ascii > 230 & ascii < 239) | (ascii == 3633) | (ascii > 3635 & ascii < 3642) | (ascii > 3654 & ascii < 3663)))
				{
					count = count + 1;
				}
			}

			return count;
		}


		private string GetTextFormatted(string strText, TextAlign align, Int32 padingSpace)
		{
			Int32 chrLength = this.CountChar(strText);
			string txt = strText;

			if (align == TextAlign.Left)
			{
				txt = txt.PadLeft(txt.Length + padingSpace);
			}
			else if (align == TextAlign.Center)
			{
				Int32 padLeft = ((MAX_TEXT_LEGTH - chrLength) / 2) - 1;
				padLeft -= this.lineLength;
				txt = txt.PadLeft(txt.Length + padLeft);
			}
			else if (align == TextAlign.Right)
			{
				Int32 padLeft = (MAX_TEXT_LEGTH - chrLength - this.lineLength) - padingSpace;
				txt = txt.PadLeft(txt.Length + padLeft);

			}

			return txt;
		}


		public void InsertPaperCut()
		{
			this.strText.Append(Convert.ToChar(27).ToString() + Convert.ToChar(105).ToString());
		}

		private string HtmlToEscCode(string strText)
		{
			return "";
		}
	}

}
