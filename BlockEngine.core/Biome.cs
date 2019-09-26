using System;

namespace BlockEngine
{
	// Token: 0x0200000A RID: 10
	public class Biome
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002FC0 File Offset: 0x000011C0
		public Biome()
		{
			this.FlagDistance = 10;
			this.FlagLerpAmont = 0;
			this.MaxHeight = 80;
			this.FlagPowerMin = 1;
			this.FlagPowerMax = 3;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002FF0 File Offset: 0x000011F0
		public int[][] MakeRows(int XLength, int YLength)
		{
			checked
			{
				int[][] A = new int[XLength - 1 + 1][];
				int num = YLength - 1;
				for (int x = 0; x <= num; x++)
				{
					A[x] = new int[YLength - 1 + 1];
				}
				int nFlagsX = (int)((double)XLength / (double)this.FlagDistance);
				int nFlagsY = (int)((double)YLength / (double)this.FlagDistance);
				int[][] Flags = new int[nFlagsX + 1][];
				int num2 = nFlagsX;
				for (int x2 = 0; x2 <= num2; x2++)
				{
					Flags[x2] = new int[nFlagsY + 1];
				}
				int num3 = nFlagsX;
				for (int X = 0; X <= num3; X++)
				{
					int num4 = nFlagsY;
					for (int Y = 0; Y <= num4; Y++)
					{
						Flags[X][Y] = Convert.ToInt32(Math.Round(new decimal(Math.Min(Math.Max(this.RowPattern[Funcs.RND.Next(0, this.RowPattern.Length)], 0), this.MaxHeight))));
					}
				}
				int nFLAF = this.FlagLerpAmont * 2 * (this.FlagLerpAmont * 2);
				bool flag = this.FlagLerpAmont > 0;
				if (flag)
				{
					int flagLerpAmont = this.FlagLerpAmont;
					int num5 = nFlagsX - 1 - this.FlagLerpAmont;
					for (int X2 = flagLerpAmont; X2 <= num5; X2++)
					{
						int flagLerpAmont2 = this.FlagLerpAmont;
						int num6 = nFlagsY - 1 - this.FlagLerpAmont;
						for (int Y2 = flagLerpAmont2; Y2 <= num6; Y2++)
						{
							int Total = 0;
							int num7 = 0 - this.FlagLerpAmont;
							int flagLerpAmont3 = this.FlagLerpAmont;
							for (int FLAX = num7; FLAX <= flagLerpAmont3; FLAX++)
							{
								int num8 = 0 - this.FlagLerpAmont;
								int flagLerpAmont4 = this.FlagLerpAmont;
								for (int FLAY = num8; FLAY <= flagLerpAmont4; FLAY++)
								{
									Total += Flags[X2 + FLAX][Y2 + FLAY];
								}
							}
							double Average = (double)Total / (double)nFLAF;
							int F = Flags[X2][Y2];
							int RndFlagPower = Funcs.RND.Next(this.FlagPowerMin, this.FlagPowerMax + 1);
							Flags[X2][Y2] = (int)Math.Round(Math.Min(Math.Max(Math.Round(unchecked((double)(checked(F * RndFlagPower)) + Average) / (double)(RndFlagPower + 1)), 0.0), (double)this.MaxHeight));
						}
					}
					int BnFLAF = this.FlagLerpAmont * this.FlagLerpAmont;
					int flagLerpAmont5 = this.FlagLerpAmont;
					for (int X3 = 0; X3 <= flagLerpAmont5; X3++)
					{
						int num9 = nFlagsY - 1 - this.FlagLerpAmont;
						for (int Y3 = 0; Y3 <= num9; Y3++)
						{
							int Total2 = 0;
							int flagLerpAmont6 = this.FlagLerpAmont;
							for (int FLAX2 = 0; FLAX2 <= flagLerpAmont6; FLAX2++)
							{
								int flagLerpAmont7 = this.FlagLerpAmont;
								for (int FLAY2 = 0; FLAY2 <= flagLerpAmont7; FLAY2++)
								{
									Total2 += Flags[X3 + FLAX2][Y3 + FLAY2];
								}
							}
							double Average2 = (double)Total2 / (double)BnFLAF;
							int F2 = Flags[X3][Y3];
							int RndFlagPower2 = Funcs.RND.Next(this.FlagPowerMin, this.FlagPowerMax + 1);
							Flags[X3][Y3] = (int)Math.Round(Math.Min(Math.Max(Math.Round(unchecked((double)(checked(F2 * RndFlagPower2)) + Average2) / (double)(RndFlagPower2 + 1)), 0.0), (double)this.MaxHeight));
						}
					}
					int num10 = nFlagsX - 1 - this.FlagLerpAmont;
					int num11 = nFlagsX - 1;
					for (int X4 = num10; X4 <= num11; X4++)
					{
						int num12 = nFlagsY - 1 - this.FlagLerpAmont;
						for (int Y4 = 0; Y4 <= num12; Y4++)
						{
							int Total3 = 0;
							int flagLerpAmont8 = this.FlagLerpAmont;
							for (int FLAX3 = flagLerpAmont8; FLAX3 >= 0; FLAX3 += -1)
							{
								int flagLerpAmont9 = this.FlagLerpAmont;
								for (int FLAY3 = flagLerpAmont9; FLAY3 >= 0; FLAY3 += -1)
								{
									Total3 += Flags[Math.Min(X4 + FLAX3, nFlagsX - 1)][Math.Min(Y4 + FLAY3, nFlagsY - 1)];
								}
							}
							double Average3 = (double)Total3 / (double)BnFLAF;
							int F3 = Flags[X4][Y4];
							int RndFlagPower3 = Funcs.RND.Next(this.FlagPowerMin, this.FlagPowerMax + 1);
							Flags[X4][Y4] = (int)Math.Round(Math.Min(Math.Max(Math.Round(unchecked((double)(checked(F3 * RndFlagPower3)) + Average3) / (double)(RndFlagPower3 + 1)), 0.0), (double)this.MaxHeight));
						}
					}
					int num13 = nFlagsX - 1 - this.FlagLerpAmont;
					for (int X5 = 0; X5 <= num13; X5++)
					{
						int flagLerpAmont10 = this.FlagLerpAmont;
						for (int Y5 = 0; Y5 <= flagLerpAmont10; Y5++)
						{
							int Total4 = 0;
							int flagLerpAmont11 = this.FlagLerpAmont;
							for (int FLAX4 = 0; FLAX4 <= flagLerpAmont11; FLAX4++)
							{
								int flagLerpAmont12 = this.FlagLerpAmont;
								for (int FLAY4 = 0; FLAY4 <= flagLerpAmont12; FLAY4++)
								{
									Total4 += Flags[X5 + FLAX4][Y5 + FLAY4];
								}
							}
							double Average4 = (double)Total4 / (double)BnFLAF;
							int F4 = Flags[X5][Y5];
							int RndFlagPower4 = Funcs.RND.Next(this.FlagPowerMin, this.FlagPowerMax + 1);
							Flags[X5][Y5] = (int)Math.Round(Math.Min(Math.Max(Math.Round(unchecked((double)(checked(F4 * RndFlagPower4)) + Average4) / (double)(RndFlagPower4 + 1)), 0.0), (double)this.MaxHeight));
						}
					}
					int num14 = nFlagsX - 1 - this.FlagLerpAmont;
					for (int X6 = 0; X6 <= num14; X6++)
					{
						int num15 = nFlagsY - 1 - this.FlagLerpAmont;
						int num16 = nFlagsY - 1;
						for (int Y6 = num15; Y6 <= num16; Y6++)
						{
							int Total5 = 0;
							int flagLerpAmont13 = this.FlagLerpAmont;
							for (int FLAX5 = flagLerpAmont13; FLAX5 >= 0; FLAX5 += -1)
							{
								int flagLerpAmont14 = this.FlagLerpAmont;
								for (int FLAY5 = flagLerpAmont14; FLAY5 >= 0; FLAY5 += -1)
								{
									Total5 += Flags[Math.Min(X6 + FLAX5, nFlagsX - 1)][Math.Min(Y6 + FLAY5, nFlagsY - 1)];
								}
							}
							double Average5 = (double)Total5 / (double)BnFLAF;
							int F5 = Flags[X6][Y6];
							int RndFlagPower5 = Funcs.RND.Next(this.FlagPowerMin, this.FlagPowerMax + 1);
							Flags[X6][Y6] = (int)Math.Round(Math.Min(Math.Max(Math.Round(unchecked((double)(checked(F5 * RndFlagPower5)) + Average5) / (double)(RndFlagPower5 + 1)), 0.0), (double)this.MaxHeight));
						}
					}
				}
				int num17 = nFlagsX;
				for (int X7 = 1; X7 <= num17; X7++)
				{
					int num18 = nFlagsX;
					for (int Y7 = 1; Y7 <= num18; Y7++)
					{
						int ThisFlag = Flags[X7][Y7];
						int PXFlag = Flags[X7 - 1][Y7];
						int PYFlag = Flags[X7][Y7 - 1];
						int PXPYFlag = Flags[X7 - 1][Y7 - 1];
						int[] DifPX = Funcs.ListDif(PXFlag, ThisFlag, this.FlagDistance);
						int[] DifPY = Funcs.ListDif(PYFlag, ThisFlag, this.FlagDistance);
						int[] DifParrelelPX = Funcs.ListDif(PXPYFlag, PYFlag, this.FlagDistance);
						int[] DifParrelelPY = Funcs.ListDif(PXPYFlag, PXFlag, this.FlagDistance);
						int PosX = X7 * this.FlagDistance;
						int PosY = Y7 * this.FlagDistance;
						int PosPX = (X7 - 1) * this.FlagDistance;
						int PosPY = (Y7 - 1) * this.FlagDistance;
						int[][] DDifX = new int[this.FlagDistance - 1 + 1][];
						int num19 = this.FlagDistance - 1;
						for (int i = 0; i <= num19; i++)
						{
							DDifX[i] = Funcs.ListDif(DifParrelelPY[i], DifPY[i], this.FlagDistance);
						}
						int j = 0;
						int num20 = PosPX;
						int num21 = PosX - 1;
						for (int BX = num20; BX <= num21; BX++)
						{
							int k = 0;
							int[] DDifY = Funcs.ListDif(DifParrelelPX[j], DifPX[j], this.FlagDistance);
							int num22 = PosPY;
							int num23 = PosY - 1;
							for (int BY = num22; BY <= num23; BY++)
							{
								A[BX][BY] = (int)Math.Round((double)(DDifY[k] + DDifX[k][j]) / 2.0);
								k++;
							}
							j++;
						}
					}
				}
				return A;
			}
		}

		// Token: 0x0400000F RID: 15
		public string Name;

		// Token: 0x04000010 RID: 16
		public int Index;

		// Token: 0x04000011 RID: 17
		public BlockType SurfaceBlock;

		// Token: 0x04000012 RID: 18
		public BlockType InnerBlock;

		// Token: 0x04000013 RID: 19
		public int[] RowPattern;

		// Token: 0x04000014 RID: 20
		public int FlagDistance;

		// Token: 0x04000015 RID: 21
		public int FlagLerpAmont;

		// Token: 0x04000016 RID: 22
		public int MaxHeight;

		// Token: 0x04000017 RID: 23
		public int FlagPowerMin;

		// Token: 0x04000018 RID: 24
		public int FlagPowerMax;

		// Token: 0x04000019 RID: 25
		public Struct.EnumStructs[] Structs;

		// Token: 0x0400001A RID: 26
		public double[] StructsRarity;
	}
}
