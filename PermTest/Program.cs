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
			int size=4;
			//Console.WriteLine(Factorial(5));
			Permutation test = new Permutation(size);
			
			for(int i=0; i<Utils.Factorial(size)+1;i++)
			{
				Console.WriteLine("{0}) {1} is {2}; And again to number {3}",i,test.StringPermutationAccessor,Permutation.ToLecsicNumber(test), Permutation.FromLecsicNumber(size,i).StringPermutationAccessor);
				test=test.GetNextPermutation();
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
	
	
	public class Permutation
	{
		protected int numOfElems;
		protected int numberEquialent;
		protected List<int> thisPermutation;
		
		public BigInteger BigLecsicNumber { get { throw new NotImplementedException(); } }
		public int NumOfElems { get {return numOfElems; } }
		public long LecsicNumber 
		{
			get { return Permutation.ToLecsicNumber(this); }
			set { throw new NotImplementedException(); }
		}
		public int[] PermutationAccessor { get {return thisPermutation.ToArray(); } }
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
		
		public Permutation()
		{
			this.numOfElems=1;
			this.thisPermutation=new List<int>();
			thisPermutation.Add(0);
		}
		
		public Permutation(int permutationSize)
		{
			this.numOfElems=permutationSize;
			this.thisPermutation=new List<int>();
			for (int i=0;i<permutationSize;i++)
				thisPermutation.Add(i);
		}
		
		public Permutation(int[] inputPermutation)
		{
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
			int outp=1;
			try
			{
				for(int i=2;i<=input;i++)
				outp*=i;
			}
			catch(OverflowException ex)	{Console.WriteLine(ex.ToString());}
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
