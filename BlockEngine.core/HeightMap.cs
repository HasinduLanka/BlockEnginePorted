using System;

namespace BlockEngine
{
	// Token: 0x0200000C RID: 12
	public class HeightMap
	{
		// Token: 0x0400001C RID: 28
		public IntVector3 Index;

		// Token: 0x0400001D RID: 29
		public static int Size = 176;

		// Token: 0x0400001E RID: 30
		public byte[][] B;

		// Token: 0x0400001F RID: 31
		public Biome Bm;

		// Token: 0x04000020 RID: 32
		public static long ByteCodeLength = (long)(checked(HeightMap.Size * HeightMap.Size + 4));
	}
}
