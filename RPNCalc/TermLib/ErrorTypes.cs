/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 23.07.2018
 * Time: 11:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace RPNCalc
{
	/// <summary>
	/// Description of ErrorTypes.
	/// </summary>
	public static partial class TermLib
	{
		public enum ERRType
		{
			ERR_NOT_DEFINED, ERR_INVALID_VALUE, ERR_GET_RESULT, ERR_INVALID_ARGUMENTS
		}
		
		public class ParsingException: Exception
		{
			private char problematicChar=(char)0;
			public char ProblematicChar { get { return problematicChar; } }
			
			public override string ToString()
			{
				return String.Format("Problem with symbol ({0})",problematicChar);
			}
			
			public ParsingException() : base() {}
			public ParsingException(char err)
			{
				this.problematicChar=err;
			}
		}
	}
}
