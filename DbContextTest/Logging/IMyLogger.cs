using System;

namespace DbContextTest.Logging
{
	public interface IMyLogger
	{
		void Log(string msg, Exception ex = null);
	}
}
