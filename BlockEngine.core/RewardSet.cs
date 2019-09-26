using System;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
	// Token: 0x02000034 RID: 52
	public class RewardSet
	{
		// Token: 0x0600020A RID: 522 RVA: 0x0001C97C File Offset: 0x0001AB7C
		public void Reward(Entity e)
		{
			e.Health = Math.Min(checked(e.Health + this.Health), e.MaxHealth);
			e.Strength += this.Strength;
			e.Accelaration += this.Accelaration;
			e.Weight += this.Weight;
			checked
			{
				e.XP += this.XP;
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0001C9FC File Offset: 0x0001ABFC
		public void Reward(Entity e, RewardSetMultiplier Multiplier)
		{
			checked
			{
				e.Health += (int)Math.Round((double)(unchecked((float)this.Health * Multiplier.Health)));
				e.Health = Math.Min(e.Health, e.MaxHealth);
			}
			e.Strength += this.Strength * Multiplier.Strength;
			e.Accelaration += this.Accelaration;
			e.Weight += this.Weight;
			checked
			{
				e.XP += this.XP * (int)Math.Round((double)Multiplier.XP);
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0001CAAB File Offset: 0x0001ACAB
		public RewardSet(int HHealth)
		{
			this.Health = 0;
			this.Strength = 0f;
			this.Weight = 0f;
			this.Accelaration = Vector3.Zero;
			this.XP = 0;
			this.Health = HHealth;
		}

		// Token: 0x0400022D RID: 557
		public int Health;

		// Token: 0x0400022E RID: 558
		public float Strength;

		// Token: 0x0400022F RID: 559
		public float Weight;

		// Token: 0x04000230 RID: 560
		public Vector3 Accelaration;

		// Token: 0x04000231 RID: 561
		public int XP;
	}
}
