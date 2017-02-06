using System;

namespace AssemblyCSharp
{
	public class Exercise
	{
		private String displayName;
		private int animation;
		private String [] muscles;

		public Exercise (string d, int a, String [] m)
		{
			this.displayName = d;
			this.animation = a;
			this.muscles = m;
		}
		public string getName(){
			return this.displayName;
		}

		public int getAnimation(){
			return this.animation;
		}		

		public String [] getMuscles(){
			return this.muscles;
		}
	}
}

