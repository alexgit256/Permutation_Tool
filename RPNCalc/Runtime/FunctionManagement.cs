/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 02.08.2018
 * Time: 15:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace RPNCalc.Runtime
{
	/// <summary>
	/// Description of FunctionManagement.
	/// </summary>
	public static class FunctionManagement
	{
		public abstract class FunctionManager: IDisposable
		{
			protected SingleFunctionManager[] Members;
			
			public void Dispose()
			{
				foreach (var element in this.Members) {
					element.Dispose();
				}
			}
		}
		
		public abstract class SingleFunctionManager: IDisposable //manages behaviour of given function
		{
			protected string functionName;
			protected Int64 functionId;
			protected RPNCalc.TermLib.FNCType functionType;
			
			public void Dispose()
			{
				functionName=null;
				//other actions
			}
		}
	}
}
