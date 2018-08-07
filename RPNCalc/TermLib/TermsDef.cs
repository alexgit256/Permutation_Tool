/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 23.07.2018
 * Time: 10:46
 * 
 * Here all Terms Types' fields and properties are defined.
 */
using System;
using System.Text.RegularExpressions;

namespace RPNCalc
{
	/// <summary>
	/// Defines classes for terms.
	/// </summary>
	public static partial class TermLib
	{
		public abstract class TermAbsClass
		{
			public abstract Boolean checkMatch(string input);	//check if input is given term
			protected TermTypes TermType;
			protected int[] val;
			public TermTypes GetTermType { get {return TermType; } }
			public int[] GetBitValue { get { return val; } }
			//public static abstract TermAbsClass getFromString(string input);
			//public abstract TermAbsClass GetResult(TermAbsClass TermA, TermAbsClass TermB);
			
			public TermAbsClass()	{ TermType = TermTypes.ERR; val=new int[1]; val[0]=(int)ERRType.ERR_NOT_DEFINED; }
			public TermAbsClass(ERRType err)	{ TermType = TermTypes.ERR; val=new int[1]; val[0]=(int)err; }
			//public TermAbsClass()
		}
		
		/*public class EXPTerm: TermAbsClass
		{
			
		}*/
		
		public class ERRTerm: TermAbsClass
		{
			/*public TermAbsClass GetResult(TermAbsClass TermA, TermAbsClass TermB)
			{
				return new ERRTerm(ERRType.ERR_INVALID_ARGUMENTS);	//problems with arguments
			}*/
			public override bool checkMatch(string input) {return true;}
			
			public ERRTerm(ERRType err)	{ TermType = TermTypes.ERR; val=new int[1]; val[0]=(int)err; }
		}
	
		public class MRKTerm : TermAbsClass
		{
			/*public TermAbsClass GetResult(TermAbsClass TermA, TermAbsClass TermB) 
			{ 
				Console.WriteLine("Markup doesn't perform any operations!");
				return new ERRTerm(ERRType.ERR_GET_RESULT);	//ERR_15 No operations for markup
			}*/
			
			public MRKType MarkupType {get { return (MRKType)val[0];} }
			
			public override bool checkMatch(string input)
			{
				if (input.Length!=1)
					return false;
				
				if (Array.BinarySearch(Markup,input[0])>=0)	//if symbol is markup
					return true;
				return false;
			}
			
			public MRKTerm(string text)
			{
				base.val = new int[1];
				base.TermType=TermTypes.MRK;
				switch (text) 
				{
					case ".":
						val[0]=(int)MRKType.POINT;
						break;
					case ",":
						val[0]=(int)MRKType.COMMA;
						break;
					case "(":
						val[0]=(int)MRKType.OP_PARENTHESIS;
						break;
					case ")":
						val[0]=(int)MRKType.CLS_PARENTHESIS;
						break;
					case "[":
						val[0]=(int)MRKType.OP_BRACKET;
						break;
					case "]":
						val[0]=(int)MRKType.CLS_BRACKET;
						break;
				}
			}
		}
	
		public class OPRTerm : TermAbsClass
		{
			public override bool checkMatch(string input)
			{
				if (input.Length>2)
					return false;
				if (Array.BinarySearch(OperatorSymbols,input[0])>=0)
					if (input.Length==1)
						return true;
					else
						if (Array.BinarySearch(OperatorSymbols,input[1])>=0)
							return true;
				return false;
			}
			
			public TermAbsClass GetResult(params TermAbsClass[] TermA)
			{
				throw (new NotImplementedException());
			}
			
			public static OPRTerm GetOperatorTerm(string inpt)
			{
				OPRType tmp=new OPRType();
			
				switch(inpt)
				{
					case "=":
						tmp=OPRType.EQUAL;
						break;
					case "+":
						tmp=OPRType.PLUS;
						break;
					case "-":
						tmp=OPRType.MINUS;
						break;
					case "*":
						tmp=OPRType.MULT;
						break;
					case "/":
						tmp=OPRType.DIV;
						break;
					case "<":
						tmp=OPRType.LESS;
						break;
					case ">":
						tmp=OPRType.MORE;
						break;
					case "!":
						tmp=OPRType.EXL_MRK;
						break;
					case "&":
						tmp=OPRType.AMPERSANT;
						break;
					case "|":
						tmp=OPRType.VERT_BAR;
						break;
					case "==":
						tmp=OPRType.DBL_EQUAL;
						break;
					case "+=":
						tmp=OPRType.PLUS_EQUAL;
						break;
					case "-=":
						tmp=OPRType.MINUS_EQUAL;
						break;
					case "*=":
						tmp=OPRType.MULT_EQUAL;
						break;
					case ">=":
						tmp=OPRType.MORE_EQUAL;
						break;
					case "<=":
						tmp=OPRType.LESS_EQUAL;
						break;
					case "!!":
						tmp=OPRType.DBL_EXL_MRK;
						break;
					case "&&":
						tmp=OPRType.DBL_AMPERSANT;
						break;
					case "||":
						tmp=OPRType.DBL_VERT_BAR;
						break;
					case ">>":
						tmp=OPRType.RIGHT_SHIFT;
						break;
					case "<<":
						tmp=OPRType.LEFT_SHIFT;
						break;
					case "++":
						tmp=OPRType.INCREMENT;
						break;
					case "--":
						tmp=OPRType.DECREMENT;
						break;
				}
				return new OPRTerm(tmp);
			}
			
			public OPRTerm(OPRType oprT) {base.TermType=TermTypes.OPR; val = new int[1]; val[0]=(int)oprT; }
			
		}
		
		public class INTTerm : TermAbsClass
		{
			/*public TermAbsClass GetResult(TermAbsClass TermA, TermAbsClass TermB)
			{
				return new ERRTerm(ERRType.ERR_INVALID_ARGUMENTS);	//problems with arguments
			}*/
			
			public override bool checkMatch(string input)
			{
				if (input.Length>max_Term_Length)
					return false;
				if (Regex.IsMatch(input,@"\d*"))
					return true;
				return false;
			}
			
			public long Value
			{
				get { long outt=(long)val[1]; outt=outt<<32; outt+=val[0]; return outt; }
				set { val[0]=(int)value; var tmp=value>>32; val[1]=(int)tmp; }
			}
			
			public INTTerm(Int64 num)
			{
				TermType=TermTypes.INT;
				val=new int[2];
				val[0]=(int)num;
				num=num>>32;
				val[1]=(int)num;
			}
		}
		
		public class FLTTerm : TermAbsClass
		{
			public override bool checkMatch(string input)
			{
				if (max_Term_Length<input.Length)
					return false;
				if (Regex.IsMatch(input,@"\d\.\d"))
					return true;
				return false;
			}
			
			public double Value
			{
				get { long tmp=val[0]+(val[1]>>32); return BitConverter.Int64BitsToDouble(tmp); }
				set { long tmp = BitConverter.DoubleToInt64Bits(value); val[0] = (int)tmp; val[1]=(int)(tmp>>32); }
			}
			
			public FLTTerm(double input)
			{
				base.TermType=TermTypes.FLT;
				long tmp = BitConverter.DoubleToInt64Bits(input);
				val = new int[2];
				val[0] = (int)input;
				val[1]=(int)(tmp>>32);
			}
		}
		
		public class VARTerm: TermAbsClass
		{
			public override bool checkMatch(string input)
			{
				if (!Char.IsLetter(input[0]))
					return false;
				if (input.Length==1)
					return true;
				for (int i=1; i<input.Length; i++)
				{
					if (!Char.IsLetter(input[i]))
						if (input[i]!='_')
							return false;
				}
				return true;
			}
			
			public VARTerm(VARType var_type, int[] intVal)
			{
				base.TermType=TermTypes.VAR;
				base.val=null;
				base.val=new int[intVal.Length+1];
				base.val[0]=(int)var_type;
				Array.ConstrainedCopy(intVal,0,base.val,1,intVal.Length);
			}
		}
		
		public class FNCTerm: TermAbsClass
		{
			//private byte[] functionTransform() {};
			
			public override bool checkMatch(string input)
			{
				if (!Char.IsLetter(input[0]))
					return false;
				if (input.Length==1)
					return true;
				for (int i=1; i<input.Length; i++)
				{
					if (!Char.IsLetter(input[i]))
						if (input[i]!='_')
							return false;
				}
				return true;
			}
			
			public FNCTerm(FNCType func_type)
			{
				base.TermType=TermTypes.FNC;
				base.val = new int[2];
				base.val[0] = (int)func_type;
			}
		}
		
		public class NULLTerm : TermAbsClass	//e.g. for comments
		{
			public override bool checkMatch(string input) {return true;}
			public NULLTerm(int[] code)
			{
				base.val=code;
			}
		}
	}
}
