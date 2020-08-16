using System;
using System.Collections.Generic;
using System.Text;

namespace ClassApp.Database
{
	public class DatabaseAccessor
	{
		static DatabaseAccessor()
		{
			Instance = new minicstructorContext();
		}

		public static minicstructorContext Instance { get; private set; }
	}
}

