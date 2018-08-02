/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 29.07.2018
 * Time: 13:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Permutation_Tool
{
	/// <summary>
	/// Description of Permutation.
	/// </summary>
	
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			//TestDebug.TestEnumberAllPermutations,false);
			//TestDebug.TestSumEfficiecy(7,1000000,false);
			TestDebug.TestSubstitutionEfficiency(7,1000000,false);
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		static class TestDebug
		{
			public static void TestEnumberAllPermutations(int size)
			{
				Console.WriteLine("Total num of elems is: {0}", Utils.Factorial(size));
				Permutation test = new Permutation(size);
			
				for(int i=0; i<Utils.Factorial(size)+1;i++)
				{
					Console.WriteLine("{0}) {1} is {2}; And again to permutation {3}",i,test.StringPermutationAccessor,Permutation.ToLecsicNumber(test), Permutation.FromLecsicNumber(size,i).StringPermutationAccessor);
					test=test.GetNextPermutation();
				}
			
				Console.Write("Press any key to continue . . . ");
			}
			
			public static void TestSumProduct(int size)
			{
				Console.WriteLine("Total num of elems is: {0}", Utils.Factorial(size));
				Permutation a = Permutation.FromLecsicNumber(size,144);
				Permutation b = Permutation.FromLecsicNumber(size,6);
				var c=a*b;
				Console.WriteLine("{0} is A lecs: {1}, \n{2} is B lecs: {3}, \n{4} is A*B lecs {5}", a.StringPermutationAccessor, a.LecsicNumber,b.StringPermutationAccessor,b.LecsicNumber, c.StringPermutationAccessor,c.LecsicNumber);
				c=a+b;
				Console.WriteLine("\n{0} is A lecs: {1}, \n{2} is B lecs: {3}, \n{4} is A*B lecs {5}", a.StringPermutationAccessor, a.LecsicNumber,b.StringPermutationAccessor,b.LecsicNumber, c.StringPermutationAccessor,c.LecsicNumber);
			}
			
			public static void TestSumEfficiency(int size, bool enableOutput)
			{
				
				Permutation a,b,c;
				System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
				for (int i=0;i<Utils.Factorial(size);i++)
					for (int j=0;j<Utils.Factorial(size);j++)
						{
							a= Permutation.FromLecsicNumber(size,i);
							b=Permutation.FromLecsicNumber(size,j);
							c=a+b;
							if (enableOutput)
								Console.Write("permutation: {0} lecsic number {3} = {1}+{2} ", c.StringPermutationAccessor, i, j, c.LecsicNumber);
						}
				sw.Stop();
				Console.WriteLine("{0} calculations performed within {1} time", Utils.Factorial(size)*Utils.Factorial(size), sw.Elapsed);
			}
			
			public static void TestSumEfficiecy(int size, int upperBound, bool enableOutput)
			{
				
				Permutation a,b,c;
				int debug=0;
				System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
				for (int i=0;i<Utils.Factorial(size);i++)
					for (int j=0;j<Utils.Factorial(size);j++)
						{
							a= Permutation.FromLecsicNumber(size,i);
							b=Permutation.FromLecsicNumber(size,j);
							c=a+b;
							if (enableOutput)
								Console.WriteLine("permutation: {0} lecsic number {3} = {1}+{2} ", c.StringPermutationAccessor, i, j, c.LecsicNumber);
							debug++;
							if(debug>=upperBound)
							{
								sw.Stop();
								Console.WriteLine("{0} calculations performed within {1} time", debug, sw.Elapsed);
								return;
							}
						}
				sw.Stop();
				Console.WriteLine("{0} calculations performed within {1} time", Utils.Factorial(size)*Utils.Factorial(size), sw.Elapsed);
			}
			
			public static void TestInversePermutation(int size, long permNum)
			{
				Permutation x = Permutation.FromLecsicNumber(size,permNum);
				var y=Permutation.Inverse(x);
				Console.WriteLine("{0} mult {1} is {2} which means {3}*{4}={5}",x.StringPermutationAccessor,y.StringPermutationAccessor,(x*y).StringPermutationAccessor,x.LecsicNumber,y.LecsicNumber,(x*y).LecsicNumber);
			}
			
			public static void TestSubstitutionEfficiency(int size, int upperBound, bool enableOutput)
			{
				Permutation a,b,c;
				int debug=0;
				System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
				for (int i=0;i<Utils.Factorial(size);i++)
					for (int j=0;j<Utils.Factorial(size);j++)
						{
							a= Permutation.FromLecsicNumber(size,i);
							b=Permutation.FromLecsicNumber(size,j);
							c=a-b;
							if (enableOutput)
								Console.WriteLine("permutation: {0} lecsic number {3} = {1}-{2} ", c.StringPermutationAccessor, i, j, c.LecsicNumber);
							debug++;
							if(debug>=upperBound)
							{
								sw.Stop();
								Console.WriteLine("{0} calculations performed within {1} time", debug, sw.Elapsed);
								return;
							}
						}
				sw.Stop();
				Console.WriteLine("{0} calculations performed within {1} time", Utils.Factorial(size)*Utils.Factorial(size), sw.Elapsed);
			}
		}
	}
	
	
	public class Permutation
	{
		protected int numOfElems;
		protected List<int> thisPermutation;
		protected bool usesLongArithemetics;
		
		public BigInteger BigLecsicNumber 
		{ 
			get { return Permutation.ToBigLecsicNumber(this); }
			set { Permutation x = Permutation.FromBigLecsicNumber(this.NumOfElems,(BigInteger)value); this.thisPermutation=x.thisPermutation; }
		}
		public int NumOfElems { get {return numOfElems; } }
		public long LecsicNumber 
		{
			get { return Permutation.ToLecsicNumber(this); }
			set { Permutation x = Permutation.FromLecsicNumber(this.NumOfElems,(long)value); this.thisPermutation=x.thisPermutation; }
		}
		public int[] PermutationAccessor { get {return thisPermutation.ToArray(); } }
		public bool RequiresBigIntegerLexicNumber {get {return usesLongArithemetics; } set {this.usesLongArithemetics=value;} }
		public string StringPermutationAccessor
		{
			get
			{
				string output="";
				for (int i=0; i<numOfElems; i++)
					output+=string.Format("{0}, ",thisPermutation[i]);
				return output;
			}
		}
		
		public Permutation GetNextPermutation()
		{
			int minInd=0;
			var tmp=thisPermutation;
			for (int i=numOfElems-2;i>=0;i--)
			{
				if (tmp[i]<tmp[i+1])
				{
					//Console.WriteLine("{0} at {2} <{1} at {3} ", tmp[i],tmp[i+1],i,i+1);
					minInd=i+1;
					for (int j=i+1;j<=numOfElems-1;j++)
						if ((tmp[j]<thisPermutation[minInd]) && (tmp[j]>tmp[i]))
							minInd=j;
					Utils.Swap(tmp,i,minInd);
					//Program.printIntArray(tmp.ToArray());
					//Console.Write(" => ");	//debug
					tmp.Reverse(i+1,numOfElems-i-1);
					//Program.printIntArray(tmp.ToArray()); //DEBUG
					//Console.WriteLine();
					return new Permutation(tmp.ToArray());					
				}
			}

			return new Permutation(numOfElems);	//if it was the last permutation, then overflow to 0-permutation			
		}
		
		public static long ToLecsicNumber(Permutation x)
		{
			bool[] was=new bool[x.numOfElems];	//wheather elem was used
			for (int i=0; i<x.numOfElems;i++)
				was[i]=false;
			
			long numToPermutation=0;
			for (int i = 0; i<x.numOfElems; i++)
			{
				for (int j = 0; j<x.thisPermutation[i]; j++)
					if (!was[j])
						numToPermutation+=Utils.Factorial(x.numOfElems-i-1);
				was[x.thisPermutation[i]]=true;
			}
			return numToPermutation;
		}
		
		public static BigInteger ToBigLecsicNumber(Permutation x)
		{
			bool[] was=new bool[x.numOfElems];	//wheather elem was used
			for (int i=0; i<x.numOfElems;i++)
				was[i]=false;
			
			BigInteger numToPermutation=0;
			for (int i = 0; i<x.numOfElems; i++)
			{
				for (int j = 0; j<x.thisPermutation[i]; j++)
					if (!was[j])
						numToPermutation+=Utils.BigFactorial(x.numOfElems-i-1);
				was[x.thisPermutation[i]]=true;
			}
			return numToPermutation;
		}
		
		public static Permutation FromLecsicNumber(int size, long LNumber)
		{
			if (LNumber>=Utils.Factorial(size))
				LNumber%=Utils.Factorial(size);
			long alreadyWas;
			int curFree;
			long LN = LNumber;
			int[] intArray = new int[size];
			bool[] was = new bool[size];
			for (int i=0;i<size; i++)
				was[i]=false;
			
			for (int i=0; i<size; i++)
			{
				alreadyWas=(long)(LN/Utils.Factorial(size-i-1));
				LN%=Utils.Factorial(size-i-1);
				curFree=0;
				for (int j=0; j<size; j++)
				{
					if (!was[j])
					{
						curFree++;
						if(curFree==(alreadyWas+1))
						{
							intArray[i]=j;
							was[j]=true;
						}
					}
				}
			}
			
			return new Permutation(intArray);
		}
		
		public static Permutation FromBigLecsicNumber(int size, BigInteger LNumber)
		{
			if (LNumber>=Utils.Factorial(size))
				LNumber%=Utils.Factorial(size);
			BigInteger alreadyWas;
			int curFree;
			BigInteger LN = LNumber;
			int[] intArray = new int[size];
			bool[] was = new bool[size];
			for (int i=0;i<size; i++)
				was[i]=false;
			
			for (int i=0; i<size; i++)
			{
				alreadyWas=BigInteger.Divide(LN,Utils.BigFactorial(size-i-1));
				LN%=Utils.Factorial(size-i-1);
				curFree=0;
				for (int j=0; j<size; j++)
				{
					if (!was[j])
					{
						curFree++;
						if(curFree==(alreadyWas+1))
						{
							intArray[i]=j;
							was[j]=true;
						}
					}
				}
			}
			
			return new Permutation(intArray);
		}
		
		public static Permutation Addition(Permutation A, Permutation B)
		{
			if (A==null || B==null || A.NumOfElems!=B.NumOfElems)
				throw new ArgumentException();
			if (A.RequiresBigIntegerLexicNumber || B.RequiresBigIntegerLexicNumber)
			{
				BigInteger a = A.BigLecsicNumber;
				BigInteger b = B.BigLecsicNumber;
				return Permutation.FromBigLecsicNumber(A.NumOfElems,a+b);
			}
			else
			{
				long a = A.LecsicNumber; long b =B.LecsicNumber;
				try
				{
					long c = a+b;
					return Permutation.FromLecsicNumber(A.NumOfElems,c);
				}
				catch (OverflowException)
				{
					BigInteger c = new BigInteger(a);
					c+=b;
					return Permutation.FromBigLecsicNumber(A.NumOfElems,c);
				}
			}
		}
		
		public static Permutation PermutationProduct(Permutation A, Permutation B)
		{
			if (A==null || B==null || A.NumOfElems!=B.NumOfElems)
				throw new ArgumentException();
			int[] a=A.PermutationAccessor;
			int[] b=B.PermutationAccessor;
			int[] c=new int[a.Length];
			int currentCounterIndex;
			for (int i=0;i<a.Length;i++)
				c[i]=b[a[i]];
			return new Permutation(c);
		}
		
		public static Permutation InverseSign(Permutation x)
		{
			BigInteger a=x.BigLecsicNumber;
			BigInteger result;
			if (x.BigLecsicNumber==0)
				result=0;
			else
				result=Utils.BigFactorial(x.NumOfElems)-a;	//arithmetic modulus Factorial(NumOfElems)
			return Permutation.FromBigLecsicNumber(x.NumOfElems,result);
		}
		
		public static Permutation Inverse(Permutation x)
		{
			int[] tmp = x.PermutationAccessor;
			int[] output = new int[x.NumOfElems];
			for (int i=0; i< x.NumOfElems; i++)
				output[tmp[i]]=i;
			return new Permutation(output);
		}
		
		
		public static Permutation operator ++(Permutation x)
		{
			return x.GetNextPermutation();
		}
		
		public static Permutation operator +(Permutation x, Permutation y)
		{
			return Permutation.Addition(x,y);
		}
		
		public static Permutation operator -(Permutation x, Permutation y)
		{
			return x+Permutation.InverseSign(y);
		}
		
		public static Permutation operator *(Permutation x, Permutation y)
		{
			return Permutation.PermutationProduct(x,y);
		}
		
		public static Permutation operator /(Permutation x, Permutation y)
		{
			return x*Permutation.Inverse(y);
		}
		
		public Permutation()
		{
			usesLongArithemetics=false;
			this.numOfElems=1;
			this.thisPermutation=new List<int>();
			thisPermutation.Add(0);
		}
		
		public Permutation(int permutationSize)
		{
			usesLongArithemetics=false;
			if (permutationSize>20)
				this.usesLongArithemetics=true;
			this.numOfElems=permutationSize;
			this.thisPermutation=new List<int>();
			for (int i=0;i<permutationSize;i++)
				thisPermutation.Add(i);
		}
		
		public Permutation(int[] inputPermutation)
		{
			usesLongArithemetics=false;
			if (inputPermutation.Length>20)
				usesLongArithemetics=true;
			this.thisPermutation=new List<int>(inputPermutation);
			this.numOfElems=inputPermutation.Length;
		}
	}
	
	public static class Utils
	{
		public static long Factorial(int input)
		{
			if (input<2)
				return 1;
			long outp=1;
			try
			{
				for(int i=2;i<=input;i++)
				outp*=i;
			}
			catch(OverflowException ex)	{Console.WriteLine(ex.ToString());}
			return outp;
		}
		
		public static BigInteger BigFactorial(int input)
		{
			if (input<2)
				return 1;
			BigInteger outp=1;
			
			for(int i=2;i<=input;i++)
			outp*=i;
			return outp;
		}
		
		public static void Swap<T>(IList<T> list, int indexA, int indexB)
		{
    		T tmp = list[indexA];
    		list[indexA] = list[indexB];
    		list[indexB] = tmp;
		}
		
		public static void printIntArray(int[] x)
		{
			foreach (int t in x) {
				Console.Write("{0}, ",t);
			}
		}
	}
}
