﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLib.Extention
{
    /// <summary>
    /// Summary description for StringExtention
    /// </summary>
    class StringExtention
    {
       /// <summary>
       /// Returns a list of strings no larger than the max length sent in.
       /// </summary>
       /// <remarks>useful function used to wrap string text for reporting.</remarks>
       /// <param name="text">Text to be wrapped into of List of Strings</param>
       /// <param name="maxLength">Max length you want each line to be.</param>
       /// <returns>List of Strings</returns>
        public static List<String> Wrap(string text, int maxLength)
        {

            // Return empty list of strings if the text was empty
            if (text.Length == 0) return new List<string>();

            var words = text.Split(' ');
            var lines = new List<string>();
            var currentLine = "";

            foreach (var currentWord in words)
            {

                if ((currentLine.Length > maxLength) ||
                    ((currentLine.Length + currentWord.Length) > maxLength))
                {
                    lines.Add(currentLine);
                    currentLine = "";
                }

                if (currentLine.Length > 0)
                    currentLine += " " + currentWord;
                else
                    currentLine += currentWord;

            }

            if (currentLine.Length > 0)
                lines.Add(currentLine);


            return lines;
        }

        public static string WrapStringHtml(string text, int maxLength)
        {
            string returnString = "";

            foreach (string inputString in Wrap(text, maxLength))
            {
                if(inputString!="")
                returnString += inputString + "<br />";
            }

            return returnString;
        }
    }
}
