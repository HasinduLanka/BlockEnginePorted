using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockEngine
{
	// Token: 0x02000021 RID: 33
	public class BlockType
	{
		// Token: 0x06000120 RID: 288 RVA: 0x0000E0EE File Offset: 0x0000C2EE
		public BlockType()
		{
			this.nEffects = false;
			this.IsAir = false;
			this.Transparent = false;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000E10C File Offset: 0x0000C30C
		public Block NewBlock()
		{
			return new Block
			{
				BID = this.ID
			};
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000E134 File Offset: 0x0000C334
		public static BlockType GetByBID(short BID)
		{
			return BlockType.BTList[(int)BID];
		}

		// Token: 0x04000137 RID: 311
		public ModelMesh Mesh;

		// Token: 0x04000138 RID: 312
		public Matrix Transform;

		// Token: 0x04000139 RID: 313
		public string Name;

		// Token: 0x0400013A RID: 314
		public byte ID;

		// Token: 0x0400013B RID: 315
		public byte Varient;

		// Token: 0x0400013C RID: 316
		public bool nEffects;

		// Token: 0x0400013D RID: 317
		public bool IsAir;

		// Token: 0x0400013E RID: 318
		public bool Transparent;

		// Token: 0x0400013F RID: 319
		public static BlockType Air;

		// Token: 0x04000140 RID: 320
		public static BlockType PlaceHolder;

		// Token: 0x04000141 RID: 321
		public static BlockType Sand;

		// Token: 0x04000142 RID: 322
		public static BlockType Dirt;

		// Token: 0x04000143 RID: 323
		public static BlockType GrassBlock;

		// Token: 0x04000144 RID: 324
		public static BlockType WoodPlank;

		// Token: 0x04000145 RID: 325
		public static BlockType Brick;

		// Token: 0x04000146 RID: 326
		public static BlockType Stone;

		// Token: 0x04000147 RID: 327
		public static BlockType StoneWall;

		// Token: 0x04000148 RID: 328
		public static BlockType Tree1;

		// Token: 0x04000149 RID: 329
		public static BlockType Jak;

		// Token: 0x0400014A RID: 330
		public static BlockType Wood;

		/// <summary>
		/// Actual block type count = BTCount + 1
		/// </summary>
		// Token: 0x0400014B RID: 331
		public const int BTCount = 10;

		// Token: 0x0400014C RID: 332
		public static BlockType[] BTList = new BlockType[11];
	}
}
