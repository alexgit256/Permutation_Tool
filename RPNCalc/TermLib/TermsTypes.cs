/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 23.07.2018
 * Time: 10:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace RPNCalc
{
	/// <summary>
	/// Description of TermsTypes.
	/// </summary>
	public static partial class TermLib
	{
		public enum TermTypes {ERR,MRK,OPR,INT,FLT,VAR,FNC,NUL,EXP}; //error,markup, operator, integer, float, variable, function, null 
		public enum MRKType {POINT,COMMA,OP_PARENTHESIS,CLS_PARENTHESIS,OP_BRACKET,CLS_BRACKET}; //Types of MRK terms
		public enum OPRType
		{
			EQUAL,PLUS,MINUS,MULT,DIV,MORE,LESS,EXL_MRK,AMPERSANT,VERT_BAR,	//one symbol operarors
			DBL_EQUAL,PLUS_EQUAL,MINUS_EQUAL,MULT_EQUAL,MORE_EQUAL,LESS_EQUAL,DBL_EXL_MRK,DBL_AMPERSANT,DBL_VERT_BAR,RIGHT_SHIFT,
			LEFT_SHIFT,INCREMENT,DECREMENT	//two symbol operators
		};
		// PNTR - pointer, BIN - returns binary code
		public enum VARType {ERR,DSP,UDF,INT,INT32,PTR,PTR48,FLT,CHR,CHR8};
		public enum FNCType {VOID,PNTR,BIN,INT,FLOAT,ERROR};
	}
}
