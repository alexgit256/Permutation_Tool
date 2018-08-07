/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 23.07.2018
 * Time: 9:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;

namespace RPNCalc
{
	class Program
	{
		public static void Main(string[] args)
		{
			//Console.WriteLine("Hello World!");
			
			// TODO: Implement Functionality Here
			string x = "2*Variable+1";

			TermLib.TermStack TStack = new TermLib.TermStack();
			
			/*NumberFormatInfo provider = new NumberFormatInfo();
			provider.CurrencyDecimalSeparator=".";
			double l = Convert.ToDouble("69.55",provider);
			Console.WriteLine(l);*/
			
			TermLib.ScanExpression(x,ref TStack);
			while (!TStack.isEmpty) 
			{
				try 
				{
					Console.Write("{0}, ", TStack.Pop().GetTermType);
				} 
				catch (IndexOutOfRangeException e) 
				{
					break;
				}
			}
			//Console.Write("My debug {0}, {1}, {2}, {3}, {4}",t0,t1,t2,t3,t4);
			Console.ReadKey(true);
		}
	}
	
	public static partial class TermLib {}
	
}