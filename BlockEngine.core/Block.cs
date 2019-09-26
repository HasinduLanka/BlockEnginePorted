using System;

namespace BlockEngine
{
	// Token: 0x0200001F RID: 31
	public class Block
	{
		// Token: 0x06000119 RID: 281 RVA: 0x0000E032 File Offset: 0x0000C232
		public Block()
		{
			this.IsAir = true;
			this.SurfaceRelation = false;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000E04C File Offset: 0x0000C24C
		public static implicit operator Block(DBlock initialData)
		{
			return initialData.RealBlock;
		}

		// Token: 0x04000130 RID: 304
		public byte BID;

		// Token: 0x04000131 RID: 305
		public byte Varient;

		// Token: 0x04000132 RID: 306
		public bool IsAir;

		// Token: 0x04000133 RID: 307
		public bool SurfaceRelation;

		// Token: 0x04000134 RID: 308
		public BVector3 CPosition;
	}
}
