using System;

namespace BlockEngine
{
	// Token: 0x02000033 RID: 51
	public class RewardSetMultiplier
	{
		// Token: 0x06000209 RID: 521 RVA: 0x0001C93A File Offset: 0x0001AB3A
		public RewardSetMultiplier(float HHealth, float SStrength, float XXP)
		{
			this.Health = 1f;
			this.Strength = 1f;
			this.XP = 1f;
			this.Health = HHealth;
			this.Strength = SStrength;
			this.XP = XXP;
		}

		// Token: 0x0400022A RID: 554
		public float Health;

		// Token: 0x0400022B RID: 555
		public float Strength;

		// Token: 0x0400022C RID: 556
		public float XP;
	}
}
