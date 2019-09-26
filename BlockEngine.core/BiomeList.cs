using System;
using System.Collections.Generic;

namespace BlockEngine
{
	// Token: 0x0200000B RID: 11
	public class BiomeList
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000037F0 File Offset: 0x000019F0
		public static void Initialize()
		{
			BiomeList.Lst.Clear();
			BiomeList.Lst.AddRange(new Biome[]
			{
				BiomeList.Dessert,
				BiomeList.DessertHills,
				BiomeList.Flat,
				BiomeList.Plain,
				BiomeList.DraftHills,
				BiomeList.Hills,
				BiomeList.SuddenHills,
				BiomeList.DirtLands
			});
			checked
			{
				byte[] O = new byte[Loader.ChunkByteCodeLength + 1];
				int i = 0;
				int X = 0;
				do
				{
					int Y = 0;
					do
					{
						int Z = 0;
						do
						{
							O[i] = 0;
							O[i + 1] = 0;
							O[i + 2] = 2;
							i += 3;
							Z++;
						}
						while (Z <= 7);
						Y++;
					}
					while (Y <= 7);
					X++;
				}
				while (X <= 7);
				O[Loader.ChunkByteCodeLength] = 0;
				Loader.AirChunkByteCode = O;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000038B0 File Offset: 0x00001AB0
		public static Biome Dessert
		{
			get
			{
				return new Biome
				{
					Name = "Dessert",
					Index = 0,
					RowPattern = new int[]
					{
						0,
						0,
						10,
						10,
						0,
						20,
						30,
						20,
						0,
						50
					},
					InnerBlock = BlockType.Sand,
					SurfaceBlock = BlockType.Sand,
					FlagDistance = 20,
					FlagLerpAmont = 4,
					FlagPowerMax = 1,
					FlagPowerMin = 0
				};
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003928 File Offset: 0x00001B28
		public static Biome DessertHills
		{
			get
			{
				return new Biome
				{
					Name = "DessertHills",
					Index = 1,
					RowPattern = new int[]
					{
						0,
						0,
						20,
						40,
						0,
						10
					},
					InnerBlock = BlockType.Sand,
					SurfaceBlock = BlockType.Sand,
					FlagDistance = 40,
					FlagLerpAmont = 2,
					FlagPowerMax = 2,
					FlagPowerMin = 0,
					Structs = new Struct.EnumStructs[]
					{
						Struct.EnumStructs.HouseSmall1,
						Struct.EnumStructs.HouseSmall2,
						Struct.EnumStructs.WatchTower1
					},
					StructsRarity = new double[]
					{
						0.0003,
						0.0003,
						7E-05
					}
				};
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000039D0 File Offset: 0x00001BD0
		public static Biome Flat
		{
			get
			{
				return new Biome
				{
					Name = "Flat",
					Index = 2,
					RowPattern = new int[1],
					InnerBlock = BlockType.Dirt,
					SurfaceBlock = BlockType.GrassBlock,
					FlagDistance = 20
				};
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003A28 File Offset: 0x00001C28
		public static Biome Plain
		{
			get
			{
				return new Biome
				{
					Name = "Plain",
					Index = 3,
					RowPattern = new int[]
					{
						8,
						8,
						8,
						8,
						8,
						8,
						10,
						10,
						12,
						14,
						15,
						15,
						15,
						15,
						16,
						16
					},
					InnerBlock = BlockType.Dirt,
					SurfaceBlock = BlockType.GrassBlock,
					FlagDistance = 20,
					FlagLerpAmont = 4,
					FlagPowerMax = 1,
					FlagPowerMin = 0,
					Structs = new Struct.EnumStructs[]
					{
						Struct.EnumStructs.Tree1,
						Struct.EnumStructs.TreeJak,
						Struct.EnumStructs.HouseSmall1,
						Struct.EnumStructs.HouseSmall2,
						Struct.EnumStructs.WatchTower1,
						Struct.EnumStructs.Hut1
					},
					StructsRarity = new double[]
					{
						0.004,
						0.004,
						0.0003,
						0.0003,
						7E-05,
						0.0003
					}
				};
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public static Biome DraftHills
		{
			get
			{
				return new Biome
				{
					Name = "DraftHills",
					Index = 4,
					RowPattern = new int[]
					{
						0,
						0,
						0,
						10,
						10,
						20,
						30,
						10,
						10,
						20
					},
					InnerBlock = BlockType.Dirt,
					SurfaceBlock = BlockType.GrassBlock,
					FlagLerpAmont = 2,
					FlagDistance = 10,
					FlagPowerMax = 2,
					FlagPowerMin = 0,
					Structs = new Struct.EnumStructs[]
					{
						Struct.EnumStructs.Tree1,
						Struct.EnumStructs.TreeJak,
						Struct.EnumStructs.HouseSmall1,
						Struct.EnumStructs.HouseSmall2,
						Struct.EnumStructs.WatchTower1,
						Struct.EnumStructs.Hut1
					},
					StructsRarity = new double[]
					{
						0.005,
						0.004,
						0.0001,
						0.0001,
						9E-05,
						0.0001
					}
				};
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00003B78 File Offset: 0x00001D78
		public static Biome Hills
		{
			get
			{
				return new Biome
				{
					Name = "Hills",
					Index = 5,
					RowPattern = new int[]
					{
						0,
						200,
						100,
						20,
						50,
						10,
						50,
						200,
						0,
						0
					},
					InnerBlock = BlockType.Dirt,
					SurfaceBlock = BlockType.GrassBlock,
					FlagDistance = 40,
					FlagLerpAmont = 2,
					FlagPowerMax = 2,
					FlagPowerMin = 1,
					Structs = new Struct.EnumStructs[]
					{
						Struct.EnumStructs.Tree1,
						Struct.EnumStructs.TreeJak,
						Struct.EnumStructs.HouseSmall1,
						Struct.EnumStructs.WatchTower1,
						Struct.EnumStructs.Hut1
					},
					StructsRarity = new double[]
					{
						0.005,
						0.004,
						0.0001,
						0.0004,
						0.0003
					}
				};
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00003C20 File Offset: 0x00001E20
		public static Biome SuddenHills
		{
			get
			{
				return new Biome
				{
					Name = "SuddenHills",
					Index = 6,
					RowPattern = new int[]
					{
						100,
						100,
						40,
						40,
						20,
						10,
						0,
						0,
						0,
						100
					},
					InnerBlock = BlockType.Dirt,
					SurfaceBlock = BlockType.GrassBlock,
					FlagDistance = 40,
					FlagLerpAmont = 2,
					FlagPowerMax = 2,
					FlagPowerMin = 1,
					Structs = new Struct.EnumStructs[]
					{
						Struct.EnumStructs.Tree1,
						Struct.EnumStructs.TreeJak,
						Struct.EnumStructs.WatchTower1,
						Struct.EnumStructs.Hut1
					},
					StructsRarity = new double[]
					{
						0.003,
						0.002,
						0.0003,
						0.0003
					}
				};
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public static Biome DirtLands
		{
			get
			{
				return new Biome
				{
					Name = "DirtLands",
					Index = 7,
					RowPattern = new int[]
					{
						10,
						20,
						20,
						0,
						0
					},
					InnerBlock = BlockType.Dirt,
					SurfaceBlock = BlockType.Dirt,
					FlagDistance = 10,
					FlagLerpAmont = 2,
					FlagPowerMax = 1,
					FlagPowerMin = 0,
					Structs = new Struct.EnumStructs[]
					{
						Struct.EnumStructs.Tree1,
						Struct.EnumStructs.HouseSmall1,
						Struct.EnumStructs.WatchTower1,
						Struct.EnumStructs.Hut1
					},
					StructsRarity = new double[]
					{
						0.0005,
						0.0001,
						0.0001,
						0.0001
					}
				};
			}
		}

		// Token: 0x0400001B RID: 27
		public static List<Biome> Lst = new List<Biome>();

		// Token: 0x0200003B RID: 59
		public enum Biomes
		{
			// Token: 0x04000273 RID: 627
			Dessert,
			// Token: 0x04000274 RID: 628
			DessertHills,
			// Token: 0x04000275 RID: 629
			Flat,
			// Token: 0x04000276 RID: 630
			Plain,
			// Token: 0x04000277 RID: 631
			DraftHills,
			// Token: 0x04000278 RID: 632
			Hills,
			// Token: 0x04000279 RID: 633
			SuddenHills,
			// Token: 0x0400027A RID: 634
			DirtLands
		}
	}
}
