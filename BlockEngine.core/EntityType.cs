using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockEngine
{
	// Token: 0x02000016 RID: 22
	public class EntityType
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00008A7C File Offset: 0x00006C7C
		public EntityType()
		{
			this.IsHuman = false;
			this.IsPlayer = false;
			this.EtEnemies = new List<EntityType>();
			this.EtFriendlies = new List<EntityType>();
			this.TargetSelectingRulesForDamage = new Dictionary<EntityRelationMode, AITargetMode>();
			this.TargetSelectingRulesForDetection = new Dictionary<EntityRelationMode, AITargetMode>();
			this.LookingAtTimeout = 50;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00008AD4 File Offset: 0x00006CD4
		public void Load(Model M, short ID)
		{
			bool flag = M != null;
			if (flag)
			{
				this.Model = new Model(Main.Game.GraphicsDevice, new List<ModelBone>(), new List<ModelMesh>());
				Main.CloneObject<Model>(M, ref this.Model);
				this.Model.Tag = ID;
			}
			this.ID = ID;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00008B31 File Offset: 0x00006D31
		public void SetMethods(Type TType)
		{
			this.T = TType;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00008B3C File Offset: 0x00006D3C
		public void CreateBasicTargetingRules()
		{
			this.TargetSelectingRulesForDamage.Clear();
			this.TargetSelectingRulesForDamage.Add(EntityRelationMode.Enemy, AITargetMode.FollowAndKill);
			this.TargetSelectingRulesForDamage.Add(EntityRelationMode.TypeEnemies, AITargetMode.FollowAndKill);
			this.TargetSelectingRulesForDamage.Add(EntityRelationMode.Friends, AITargetMode.None);
			this.TargetSelectingRulesForDamage.Add(EntityRelationMode.TypeFriends, AITargetMode.None);
			this.TargetSelectingRulesForDamage.Add(EntityRelationMode.Unknowen, AITargetMode.FollowAndKill);
			this.TargetSelectingRulesForDetection.Clear();
			this.TargetSelectingRulesForDetection.Add(EntityRelationMode.Enemy, AITargetMode.FollowAndKill);
			this.TargetSelectingRulesForDetection.Add(EntityRelationMode.TypeEnemies, AITargetMode.FollowAndKill);
			this.TargetSelectingRulesForDetection.Add(EntityRelationMode.Friends, AITargetMode.None);
			this.TargetSelectingRulesForDetection.Add(EntityRelationMode.TypeFriends, AITargetMode.None);
			this.TargetSelectingRulesForDetection.Add(EntityRelationMode.Unknowen, AITargetMode.None);
		}

		// Token: 0x040000D1 RID: 209
		public short ID;

		// Token: 0x040000D2 RID: 210
		public Model Model;

		// Token: 0x040000D3 RID: 211
		public Matrix Transforms;

		// Token: 0x040000D4 RID: 212
		public bool IsHuman;

		// Token: 0x040000D5 RID: 213
		public bool IsPlayer;

		// Token: 0x040000D6 RID: 214
		public Type T;

		// Token: 0x040000D7 RID: 215
		public Action<Entity> DelTargetScan;

		// Token: 0x040000D8 RID: 216
		public Action<Entity> UpdateAI;

		// Token: 0x040000D9 RID: 217
		public List<EntityType> EtEnemies;

		// Token: 0x040000DA RID: 218
		public List<EntityType> EtFriendlies;

		// Token: 0x040000DB RID: 219
		public Dictionary<EntityRelationMode, AITargetMode> TargetSelectingRulesForDamage;

		// Token: 0x040000DC RID: 220
		public Dictionary<EntityRelationMode, AITargetMode> TargetSelectingRulesForDetection;

		// Token: 0x040000DD RID: 221
		public int LookingAtTimeout;

		// Token: 0x040000DE RID: 222
		public float Width;
	}
}
