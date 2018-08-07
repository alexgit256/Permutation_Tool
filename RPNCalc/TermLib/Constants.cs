/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 25.07.2018
 * Time: 14:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;

namespace RPNCalc
{
	/// <summary>
	/// Description of Constants.
	/// </summary>
	public static partial class TermLib
	{
		public const int max_Term_Length = 64;
		public const int max_Term_Count = 128;
		public static readonly char[] Markup = {'.',',','(','(','[',']'};
		public static readonly char[] OperatorSymbols = {'=','+','-','*','/','>','<','!','&','|'}; //Array.BinarySearch() doesn't see * here
		
	}
}
