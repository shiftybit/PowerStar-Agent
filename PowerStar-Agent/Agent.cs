using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace PowerStar_Agent
{
	static class Agent
	{
		private static PowerShell powershell;

		static Action<string> WriteLine;

		public static void Initialize(Action<String> writeLine = null)
		{
			WriteLine = writeLine;
			InitialSessionState state = InitialSessionState.CreateDefault();
			SessionStateVariableEntry agentEntry = new SessionStateVariableEntry("Agent", typeof(Agent), "The Agent");
			state.Variables.Add(agentEntry);
			powershell = PowerShell.Create(state);
		}

		public static string RunString(string text)
		{
			powershell.AddScript(text);
			string returnString = "";
			foreach (dynamic item in powershell.Invoke().ToList())
			{
				returnString += item.ToString();
			}
			return returnString;
		}
	}
}
