using System;

namespace BlockEngine
{
	// Token: 0x02000020 RID: 32
	public class DBlock
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000E064 File Offset: 0x0000C264
		public IntVector3 Index
		{
			get
			{
				return this.RealBlock.CPosition + this.Chunk.Index * 8;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000E098 File Offset: 0x0000C298
		public static DBlock FromBlock(Block initialData)
		{
			return new DBlock(initialData);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
		public static DBlock FromBlock(Block initialData, Chunk Ch)
		{
			return new DBlock(initialData)
			{
				Chunk = Ch
			};
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000E0CF File Offset: 0x0000C2CF
		public DBlock(Block B)
		{
			this.RealBlock = B;
		}

		// Token: 0x04000135 RID: 309
		public Block RealBlock;

		// Token: 0x04000136 RID: 310
		public Chunk Chunk;
	}
}
