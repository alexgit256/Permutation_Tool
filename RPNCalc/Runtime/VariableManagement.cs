/*
 * Created by SharpDevelop.
 * User: Alex
 * Date: 02.08.2018
 * Time: 15:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace RPNCalc.Runtime
{
	/// <summary>
	/// Description of VariableManagement.
	/// </summary>
	public static class VariableManagement
	{
		public class VariableManager: IDisposable	//manages behaviour of given variable
		{
			protected SingleVariableManager[] Members;
			
			public void Dispose()
			{
				foreach (var element in Members) {
					element.Dispose();
				}
			}
		}
		
		public class SingleVariableManager: IDisposable	//manages behaviour of given variable
		{
			protected string variableName;
			protected Int64 variableId;
			protected RPNCalc.TermLib.VARType variableType;
			
			public void Dispose()
			{
				variableName=null;
				//other actions
			}
		}
		
	}
}
