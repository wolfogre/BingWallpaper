using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSScriptControl;

namespace BingWallpaper
{
	class UserStatistics
	{
		private ScriptControl scriptControl;
		public UserStatistics()
		{
			scriptControl = new ScriptControl();
			scriptControl.UseSafeSubset = true;
			scriptControl.Language = "JavaScript";
			((DScriptControlSource_Event)scriptControl).Error += new DScriptControlSource_ErrorEventHandler(ScriptEngine_Error);
			((DScriptControlSource_Event)scriptControl).Timeout += new DScriptControlSource_TimeoutEventHandler(ScriptEngine_Timeout);
		}
	}
}
