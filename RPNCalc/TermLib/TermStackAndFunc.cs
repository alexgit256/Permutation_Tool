/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 25.07.2018
 * Time: 15:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;


namespace RPNCalc
{
	/// <summary>
	/// Description of TermStackAndFunc.
	/// </summary>
	public static partial class TermLib
	{
		public class TermStack
		{
			protected List<TermAbsClass> Terms;
			protected int pointer;	//points the top of the stack
			public bool isEmpty { get { return pointer<0; } }
			
			public TermStack()
			{
				pointer =-1;
				Terms=new List<TermAbsClass>();
			}
			
			public TermAbsClass Pop()
			{
				if (pointer<0)
					throw new IndexOutOfRangeException();
				return Terms[pointer--];
			}
			
			public void Push(TermAbsClass x)
			{
				if (pointer<0)
				{
					Terms.Add(x);
					pointer=0;
				}
				else
				{
					if (++pointer<Terms.Count)
						try 
						{
							//Console.Write("pointer is {0} {1}", pointer,Terms.Count);
							Terms[pointer]=x;	
						} 
						catch (IndexOutOfRangeException ex) 
						{						
							Console.Write("{0}, pointer is {1}",ex, pointer);
							throw ex;
						}
					else
						Terms.Add(x);
				}
			}		
		}
		public class StringSplitter
		{
			protected int currentPosition;
			protected int currentTermStartPosition;
			protected TermTypes currentTermType;
			
			public StringSplitter()
			{
				currentPosition=0; currentTermStartPosition=0;
				currentTermType = TermTypes.NUL;
			}
			
			public void getTermFromString(string input, ref TermStack TStack, ref int startPositionPointer)
			{
				//Console.Write("start term {0}, ", input[startPositionPointer]);
				TermTypes tt;
				TermAbsClass term=null;
				bool isOK_Flag=false;
				int tmp=startPositionPointer;
				//string txt=TakePieceFromText(input,startPosition,input.Length-startPosition-1);
				
				if (startPositionPointer<input.Length && OperatorSymbols.Contains(input[startPositionPointer]) && !isOK_Flag)	//if is operator
				{
					//Console.WriteLine("operator {0}", input[startPositionPointer]);
					
					tt=TermTypes.OPR;
					startPositionPointer++;
					if (Array.BinarySearch(OperatorSymbols,input[startPositionPointer])>=0)
						startPositionPointer++;
					string txt=TakePieceFromText(input,tmp,startPositionPointer-tmp);
					term = OPRTerm.GetOperatorTerm(txt);
					isOK_Flag=true;
				}
				
				if (startPositionPointer<input.Length && char.IsDigit(input[startPositionPointer]) && !isOK_Flag)	//if a number
				{
					int len=0;
					bool isFloat=false;
					while (startPositionPointer+len<input.Length && char.IsDigit(input[startPositionPointer+len]))
						len++;
					if (startPositionPointer+len<input.Length && len>0 && input[startPositionPointer+len]=='.')
					{
						isFloat=true; len++;
						while (startPositionPointer+len<input.Length && char.IsDigit(input[startPositionPointer+len]))
						len++;
					}
					
					
					if (isFloat)
					{
						NumberFormatInfo provider = new NumberFormatInfo();
						provider.CurrencyDecimalSeparator=".";
						double x = Convert.ToDouble(TakePieceFromText(input,startPositionPointer,len),provider);
						term = new FLTTerm(x);
						
						//Console.Write("FLOAT {0}, ", x);
					}
					else
					{
						long x=Convert.ToInt64(TakePieceFromText(input,startPositionPointer,len));
						term = new INTTerm(x);
						
						//Console.Write("INTEGER {0}, ", x);
					}
					isOK_Flag=true;
					startPositionPointer+=len;
				}
				
				if (startPositionPointer<input.Length && char.IsLetter(input[startPositionPointer]) && !isOK_Flag)	//FNC and VAR starts with letter
				{
					int len=1;
					while (startPositionPointer+len<input.Length && char.IsLetter(input[startPositionPointer+len]))
						len++;
					if (startPositionPointer+len<input.Length && input[startPositionPointer+len]=='(')	//if name is interupted with bracket - it's func
					{
						term=new FNCTerm(FNCType.ERROR);	//todo catch function
					}					
					else
					{
						int[] debugPush = new int[1];
						//string txt=TakePieceFromText(input,startPosition,input.Length-startPosition-1);
						term=new VARTerm(VARType.ERR,debugPush);	//todo catch variable
					}
					startPositionPointer+=len;
					isOK_Flag=true;
				}
				if (!isOK_Flag)
					throw new ParsingException(input[startPositionPointer]);
				TStack.Push(term);
			}
		}
		
		public static void ScanExpression(string input,ref TermStack TStack)
		{
			int startPositionPointer=0;
			StringSplitter sSplitter=new StringSplitter();
			while (startPositionPointer<input.Length)
				try { sSplitter.getTermFromString(input,ref TStack,ref startPositionPointer); }
			catch (ParsingException ex) { Console.Write("{0}, ", ex.ToString()); }
		}
		
		private static string TakePieceFromText(string text, int startIndex, int length)
		{
	 		if (string.IsNullOrEmpty(text))
        		throw new ArgumentNullException("no text givin");

    		string result = string.Empty;
    		try
    		{
        		result = text.Substring(startIndex, length);
    		}
    		catch (Exception ex)
    		{ 
    			Console.WriteLine(ex.ToString());
    			// log exception
    		}
    		return result;
		}
	}
}
