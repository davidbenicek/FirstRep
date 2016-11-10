using System;

namespace AssemblyCSharp
{
	public class Exercise
	{
		private String displayName;
		private int animation;

		public Exercise (string name, int a)
		{
			this.displayName = name;
			this.animation = a;
		}
		public string getName(){
			return this.displayName;
		}

		public int getAnimation(){
			return this.animation;
		}
	}
}

