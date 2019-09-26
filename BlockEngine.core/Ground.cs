using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
    // Token: 0x02000022 RID: 34
    public class Ground
    {
        // Token: 0x06000125 RID: 293 RVA: 0x0000E24C File Offset: 0x0000C44C
        public static void Generate(string MapName, int MapSize, int IBM, bool SpeedSave)
        {
            Main.Log("Generating a new world..", true);
            HeightMap.Size = checked(MapSize * 8);
            HeightMap.ByteCodeLength = (long)(checked(HeightMap.Size * HeightMap.Size + 4));
            Biome BM = BiomeList.Lst[IBM];
            Main.Log("Generating Biome " + BM.Name, true);
            Ground.MaxHeight = (Stack.Size * 8).Y;
            Ground.ChunkBarHeight = Stack.Size.Y;
            checked
            {
                BM.MaxHeight = Ground.MaxHeight - Ground.BaseHeight - 10;
                Loader.CreateMap(MapName);
                List<XEntity> XeList = new List<XEntity>();
                HeightMap HMG = Loader.GenareteHMap(BM, IntVector3.Zero);
                Loader.SaveHeightMap(HMG);
                HeightMap HM = Loader.LoadHeightMap(IntVector3.Zero);
                Ground.SpeedSaving = SpeedSave;
                Ground.GenAndSaveChunksFromHeightMap(HM, (int)Math.Round((double)HeightMap.Size / 8.0), XeList);
                Ground.CStack = new Stack();
                Ground.CStack.NewChunkList();
                Ground.CStack.LoadChunks(new IntVector3(Stack.Size.X, 0, Stack.Size.Z));
                GC.Collect();
                Ground.Genarated = true;
                Main.Log("Your new world is ready!", true);
            }
        }

        // Token: 0x06000126 RID: 294 RVA: 0x0000E384 File Offset: 0x0000C584
        public static void CheckAndGenerateOrLoad(string MapName)
        {
            bool flag = Loader.CheckIfMapExits(MapName);
            if (flag)
            {
                Loader.LoadWorld(MapName);
            }
            else
            {
                Ground.Generate(MapName, Main.MapVariablePipeline.NewMapSize, Main.MapVariablePipeline.NewMapBiome, Main.MapVariablePipeline.NewMapSpeedSave);
            }
            Main.Log("Started.", true);
        }

        // Token: 0x06000127 RID: 295 RVA: 0x0000E3CC File Offset: 0x0000C5CC
        public static void GenAndSaveChunksFromHeightMap(HeightMap HM, int nChunks, List<XEntity> eList)
        {
            Main.Log("Creating and saving chunks from height map. Length : " + nChunks.ToString(), true);
            Main.Log(Ground.SpeedSaving ? "Speed Saving." : "Progressive Saving.", true);
            Ground.StpWatch.Start();
            Main.Log("Starting...", true);
            int yChunks = Ground.ChunkBarHeight;
            byte[][] BA = HM.B;
            bool IsStructuredBiome = HM.Bm.Structs != null;
            BlockType[][][] SBars = null;
            bool[][] IsStructArray = null;
            byte[][] SHeights = null;
            bool flag = IsStructuredBiome;
            if (flag)
            {
                SBars = Struct.GenerateStructMap(ref HM, ref IsStructArray, ref SHeights, ref eList);
            }
            checked
            {
                Chunk[][] XChunkBars = new Chunk[nChunks - 1 + 1][];
                int num = nChunks - 1;
                for (int xCh = 0; xCh <= num; xCh++)
                {
                    int nXChunkBars = 0;
                    int num2 = nChunks - 1;
                    for (int zCh = 0; zCh <= num2; zCh++)
                    {
                        Chunk[] ChunkBar = new Chunk[yChunks - 1 + 1];
                        int num3 = yChunks - 1;
                        for (int yCh = 0; yCh <= num3; yCh++)
                        {
                            Chunk chunk = new Chunk();
                            chunk.Index = new IntVector3(xCh, yCh, zCh) + HM.Index * nChunks;
                            chunk.Position = chunk.Index * Chunk.Volume;
                            Chunk CH = chunk;
                            CH.BlockList = new Block[8][][];
                            int BX = 0;
                            do
                            {
                                CH.BlockList[BX] = new Block[8][];
                                int BY = 0;
                                do
                                {
                                    CH.BlockList[BX][BY] = new Block[8];
                                    int BZ = 0;
                                    do
                                    {
                                        CH.BlockList[BX][BY][BZ] = new Block
                                        {
                                            BID = 0,
                                            IsAir = true,
                                            CPosition = new BVector3((byte)BX, (byte)BY, (byte)BZ),
                                            SurfaceRelation = false
                                        };
                                        BZ++;
                                    }
                                    while (BZ <= 7);
                                    BY++;
                                }
                                while (BY <= 7);
                                BX++;
                            }
                            while (BX <= 7);
                            ChunkBar[yCh] = CH;
                        }
                        int Bx = 0;
                        do
                        {
                            int Bz = 0;
                            do
                            {
                                int BAX = Bx + xCh * 8;
                                int BAZ = Bz + zCh * 8;
                                byte h = BA[BAX][BAZ];
                                int yCh2 = 0;
                                Chunk Ch = ChunkBar[yCh2];
                                int num4 = (int)(h - 1);
                                Block B;
                                for (int y = 0; y <= num4; y++)
                                {
                                    bool flag2 = (double)yCh2 != Math.Truncate((double)y / 8.0);
                                    if (flag2)
                                    {
                                        yCh2 = (int)((double)y / 8.0);
                                        Ch = ChunkBar[yCh2];
                                    }
                                    int BY2 = y - yCh2 * 8;
                                    B = new Block
                                    {
                                        BID = HM.Bm.InnerBlock.ID,
                                        Varient = HM.Bm.InnerBlock.Varient,
                                        CPosition = new BVector3((byte)Bx, (byte)BY2, (byte)Bz),
                                        IsAir = false
                                    };
                                    Ch.BlockList[Bx][BY2][Bz] = B;
                                    Ch.IsAir = false;
                                    bool flag3 = BAX > 0;
                                    if (flag3)
                                    {
                                        bool flag4 = (int)BA[BAX - 1][BAZ] < y;
                                        if (flag4)
                                        {
                                            Ground.SetBlockVisibleForGenarating(ref B, ref Ch);
                                        }
                                        else
                                        {
                                            bool flag5 = BAX < HeightMap.Size - 1;
                                            if (flag5)
                                            {
                                                bool flag6 = (int)BA[BAX + 1][BAZ] < y;
                                                if (flag6)
                                                {
                                                    Ground.SetBlockVisibleForGenarating(ref B, ref Ch);
                                                }
                                                else
                                                {
                                                    bool flag7 = BAZ > 0;
                                                    if (flag7)
                                                    {
                                                        bool flag8 = (int)BA[BAX][BAZ - 1] < y;
                                                        if (flag8)
                                                        {
                                                            Ground.SetBlockVisibleForGenarating(ref B, ref Ch);
                                                        }
                                                        else
                                                        {
                                                            bool flag9 = BAZ < HeightMap.Size - 1;
                                                            if (flag9)
                                                            {
                                                                bool flag10 = (int)BA[BAX][BAZ + 1] < y;
                                                                if (flag10)
                                                                {
                                                                    Ground.SetBlockVisibleForGenarating(ref B, ref Ch);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Ground.SetBlockVisibleForGenarating(ref B, ref Ch);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Ground.SetBlockVisibleForGenarating(ref B, ref Ch);
                                    }
                                }
                                yCh2 = (int)((double)h / 8.0);
                                Ch = ChunkBar[yCh2];
                                B = new Block
                                {
                                    BID = HM.Bm.SurfaceBlock.ID,
                                    Varient = HM.Bm.SurfaceBlock.Varient,
                                    CPosition = new BVector3((byte)Bx, (byte)((int)h - yCh2 * 8), (byte)Bz),
                                    IsAir = false,
                                    SurfaceRelation = false
                                };
                                Ground.SetBlockVisibleForGenarating(ref B, ref Ch);
                                Ch.BlockList[Bx][(int)B.CPosition.Y][Bz] = B;
                                Ch.IsAir = false;
                                bool flag11 = IsStructuredBiome;
                                if (flag11)
                                {
                                    bool flag12 = IsStructArray[BAX][BAZ];
                                    if (flag12)
                                    {
                                        BlockType[] SBar = SBars[BAX][BAZ];
                                        byte CurSHeight = SHeights[BAX][BAZ];
                                        int StrctHeight = Math.Min((int)(unchecked(h + CurSHeight)), Ground.MaxHeight);
                                        int hStructMin = StrctHeight - (int)CurSHeight;
                                        int Sn = 0;
                                        int num5 = hStructMin;
                                        int num6 = StrctHeight - 1;
                                        for (int SY = num5; SY <= num6; SY++)
                                        {
                                            BlockType StrctBarBlock = SBar[Sn];
                                            bool flag13 = !StrctBarBlock.IsAir;
                                            if (flag13)
                                            {
                                                double SY2 = Math.Truncate((double)SY / 8.0);
                                                Chunk SCh = ChunkBar[(int)Math.Round(SY2)];
                                                Block SB = new Block
                                                {
                                                    BID = StrctBarBlock.ID,
                                                    Varient = StrctBarBlock.Varient,
                                                    CPosition = new BVector3((byte)Bx, (byte)(SY - SCh.Index.Y * 8), (byte)Bz),
                                                    IsAir = StrctBarBlock.IsAir,
                                                    SurfaceRelation = (StrctBarBlock.ID > 0)
                                                };
                                                bool surfaceRelation = SB.SurfaceRelation;
                                                if (surfaceRelation)
                                                {
                                                    Ground.SetBlockVisibleForGenarating(ref SB, ref SCh);
                                                }
                                                SCh.BlockList[Bx][(int)SB.CPosition.Y][Bz] = SB;
                                                SCh.IsAir = false;
                                            }
                                            Sn++;
                                        }
                                    }
                                }
                                Bz++;
                            }
                            while (Bz <= 7);
                            Bx++;
                        }
                        while (Bx <= 7);
                        XChunkBars[nXChunkBars] = ChunkBar;
                        nXChunkBars++;
                    }
                    bool speedSaving = Ground.SpeedSaving;
                    if (speedSaving)
                    {
                        foreach (var ChunkBar2 in XChunkBars)
                        {
                            Loader.SaveChunkBarCachedAdd(ChunkBar2, true);
                        }
                        Loader.CheckAndSaveCachedChunkBars();
                    }
                    else
                    {
                        foreach (var ChunkBar3 in XChunkBars)
                        {
                            Loader.SaveChunkBar(ChunkBar3, true);
                        }
                    }
                    Main.Log(xCh.ToString() + " | ", false);
                }
                XEntity.Save(Loader.FileEntity, eList);
                bool speedSaving2 = Ground.SpeedSaving;
                if (speedSaving2)
                {
                    Main.Log("Chunks genarated.", true);
                    Loader.SaveCashedChunkBars();
                    Main.Log("Chunks Saved.", true);
                }
                else
                {
                    Main.Log("Chunks genarated and saved.", true);
                }
                Main.Log(Ground.StpWatch.ElapsedMilliseconds.ToString() + " ms Elapsed", true);
                Ground.StpWatch.Stop();
            }
        }

        // Token: 0x06000128 RID: 296 RVA: 0x0000EAC8 File Offset: 0x0000CCC8
        public static BVector3 RelCPos(BVector3 CPos, IntVector3 Rel, ref IntVector3 ChunkIndex)
        {
            IntVector3 Out = CPos + Rel;
            ChunkIndex += checked(new IntVector3((int)((double)((float)Out.X / 8f)), (int)((double)((float)Out.Y / 8f)), (int)((double)((float)Out.Z / 8f))));
            return Out % 8;
        }

        // Token: 0x06000129 RID: 297 RVA: 0x0000EB34 File Offset: 0x0000CD34
        public static IntVector3 CPosOfIndex(IntVector3 Index, ref IntVector3 ChunkIndex)
        {
            ChunkIndex = IntVector3.FromV3Truncated(Index / 8);
            return Index % 8;
        }

        // Token: 0x0600012A RID: 298 RVA: 0x0000EB64 File Offset: 0x0000CD64
        public static IntVector3 BlockIndexOfPosition(Vector3 Position)
        {
            return IntVector3.FromV3Rounded(Position / 50f);
        }

        // Token: 0x0600012B RID: 299 RVA: 0x0000EB88 File Offset: 0x0000CD88
        public static IntVector3 BlockIndexOfPosition(Vector3 Position, ref IntVector3 ChunkIndex)
        {
            IntVector3 Index = IntVector3.FromV3Rounded(Position / 50f);
            ChunkIndex = IntVector3.FromV3Truncated(Index / 8);
            return Index;
        }

        // Token: 0x0600012C RID: 300 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
        public static IntVector3 CPosOfPosition(Vector3 Position, ref IntVector3 ChunkIndex)
        {
            IntVector3 Index = IntVector3.FromV3Rounded(Position / 50f);
            ChunkIndex = IntVector3.FromV3Truncated(Index / 8);
            IntVector3 CPosOfPosition = new IntVector3(Index.X % 8, Index.Y % 8, Index.Z % 8);
            return CPosOfPosition;
        }

        // Token: 0x0600012D RID: 301 RVA: 0x0000EC1C File Offset: 0x0000CE1C
        public static IntVector3 ChunkIndexOfBlockIndex(IntVector3 Index)
        {
            return IntVector3.FromV3Truncated(Index / 8);
        }

        // Token: 0x0600012E RID: 302 RVA: 0x0000EC40 File Offset: 0x0000CE40
        public static IntVector3 ChunkIndexOfPosition(Vector3 Pos)
        {
            return IntVector3.FromV3Truncated(IntVector3.RoundV3(Pos / 50f) / 8f);
        }

        // Token: 0x0600012F RID: 303 RVA: 0x0000EC74 File Offset: 0x0000CE74
        public static IntVector3 ChunkIndexOfPositionDownABlock(Vector3 Pos)
        {
            IntVector3 O = IntVector3.FromV3Rounded(Pos / 50f);
            checked
            {
                O.Y--;
                return IntVector3.FromV3Truncated(O / 8);
            }
        }

        // Token: 0x06000130 RID: 304 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
        public static Ground.BlockEnvironment GetBlockEnvironment(Vector3 Position, bool SupressErrors = false)
        {
            if (SupressErrors)
            {
            }
            IntVector3 ChI = default;
            IntVector3 CPos = Ground.CPosOfPosition(Position, ref ChI);
            Ground.BlockEnvironment O = new Ground.BlockEnvironment();
            Chunk Ch = Ground.CStack.GetChunk(ChI);
            bool flag = Ch.BlockList == null;
            if (flag)
            {
                while (Ch.BlockList == null)
                {
                    Thread.Sleep(50);
                    Ch = Ground.CStack.GetChunk(ChI);
                }
            }
            O.CurrentBlock = DBlock.FromBlock(Ch.BlockList[CPos.X][CPos.Y][CPos.Z], Ch);
            IntVector3 LegsChI = ChI;
            BVector3 LegsCPos = Ground.RelCPos(CPos, IntVector3.Up, ref LegsChI);
            bool flag2 = LegsChI == ChI;
            if (flag2)
            {
                O.LegsBlock = Ch.BlockList[(int)LegsCPos.X][(int)LegsCPos.Y][(int)LegsCPos.Z];
            }
            else
            {
                Chunk LegsCh = Ground.CStack.GetChunk(LegsChI);
                O.LegsBlock = LegsCh.BlockList[(int)LegsCPos.X][(int)LegsCPos.Y][(int)LegsCPos.Z];
            }
            IntVector3 BodyChI = LegsChI;
            BVector3 BodyCPos = Ground.RelCPos(LegsCPos, IntVector3.Up, ref BodyChI);
            bool flag3 = BodyChI == ChI;
            if (flag3)
            {
                O.BodyBlock = Ch.BlockList[(int)BodyCPos.X][(int)BodyCPos.Y][(int)BodyCPos.Z];
            }
            else
            {
                Chunk BodyCh = Ground.CStack.GetChunk(BodyChI);
                O.BodyBlock = BodyCh.BlockList[(int)BodyCPos.X][(int)BodyCPos.Y][(int)BodyCPos.Z];
            }
            IntVector3 HeadChI = BodyChI;
            BVector3 HeadCPos = Ground.RelCPos(BodyCPos, IntVector3.Up, ref BodyChI);
            bool flag4 = HeadChI == ChI;
            if (flag4)
            {
                O.HeadBlock = Ch.BlockList[(int)HeadCPos.X][(int)HeadCPos.Y][(int)HeadCPos.Z];
            }
            else
            {
                Chunk HeadCh = Ground.CStack.GetChunk(HeadChI);
                O.HeadBlock = HeadCh.BlockList[(int)HeadCPos.X][(int)HeadCPos.Y][(int)HeadCPos.Z];
            }
            return O;
        }

        /// <summary>
        /// 0 = XNextIsAir , 1 = ZNextIsAir , 2 = XUpNextIsAir , 3 = ZUpNextIsAir
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="FacingDirections"></param>
        /// <returns></returns>
        // Token: 0x06000131 RID: 305 RVA: 0x0000EEDC File Offset: 0x0000D0DC
        public static DBlock[] GetFacingBlocks(Vector3 Position, Direction[] FacingDirections)
        {
            Vector3 CP = Position / Chunk.Volume;
            checked
            {
                int CPX = (int)((double)CP.X);
                int CPY = (int)((double)CP.Y);
                int CPZ = (int)((double)CP.Z);
                Vector3 BP = (Position - new Vector3((float)(CPX * 400), (float)(CPY * 400), (float)(CPZ * 400))) / 50f;
                int BPX;
                for (BPX = (int)Math.Round((double)BP.X); BPX < 0; BPX += 8)
                {
                    CPX--;
                }
                while (BPX > 7)
                {
                    CPX++;
                    BPX -= 8;
                }
                int BPY;
                for (BPY = (int)Math.Round((double)BP.Y); BPY < 0; BPY += 8)
                {
                    CPY--;
                }
                while (BPY > 7)
                {
                    CPY++;
                    BPY -= 8;
                }
                int BPZ;
                for (BPZ = (int)Math.Round((double)BP.Z); BPZ < 0; BPZ += 8)
                {
                    CPZ--;
                }
                while (BPZ > 7)
                {
                    CPZ++;
                    BPZ -= 8;
                }
                DBlock[] O = new DBlock[4];
                bool flag = BPY == 7;
                int BPY2;
                int CPY2;
                if (flag)
                {
                    BPY2 = 0;
                    CPY2 = CPY + 1;
                }
                else
                {
                    BPY2 = BPY + 1;
                    CPY2 = CPY;
                }
                int BPXO = BPX;
                int CPXO = CPX;
                bool flag2 = FacingDirections[1] == Direction.Left;
                if (flag2)
                {
                    bool flag3 = BPX == 0;
                    if (flag3)
                    {
                        CPX--;
                        BPX = 7;
                    }
                    else
                    {
                        BPX--;
                    }
                    Chunk CH = Ground.CStack.GetChunk(CPX, CPY, CPZ);
                    O[0] = DBlock.FromBlock(CH.BlockList[BPX][BPY][BPZ]);
                    O[0].Chunk = CH;
                    O[0].RealBlock.CPosition = new Vector3((float)BPX, (float)BPY, (float)BPZ);
                    Chunk CH2 = Ground.CStack.GetChunk(CPX, CPY2, CPZ);
                    O[2] = DBlock.FromBlock(CH2.BlockList[BPX][BPY2][BPZ]);
                    O[2].Chunk = CH;
                    O[2].RealBlock.CPosition = new Vector3((float)BPX, (float)BPY2, (float)BPZ);
                }
                else
                {
                    bool flag4 = FacingDirections[1] == Direction.Right;
                    if (flag4)
                    {
                        bool flag5 = BPX == 7;
                        if (flag5)
                        {
                            CPX++;
                            BPX = 0;
                        }
                        else
                        {
                            BPX++;
                        }
                        Chunk CH = Ground.CStack.GetChunk(CPX, CPY, CPZ);
                        O[0] = DBlock.FromBlock(CH.BlockList[BPX][BPY][BPZ]);
                        O[0].Chunk = CH;
                        O[0].RealBlock.CPosition = new Vector3((float)BPX, (float)BPY, (float)BPZ);
                        Chunk CH2 = Ground.CStack.GetChunk(CPX, CPY2, CPZ);
                        O[2] = DBlock.FromBlock(CH2.BlockList[BPX][BPY2][BPZ]);
                        O[2].Chunk = CH;
                        O[2].RealBlock.CPosition = new Vector3((float)BPX, (float)BPY2, (float)BPZ);
                    }
                }
                bool flag6 = FacingDirections[0] == Direction.Forward;
                if (flag6)
                {
                    bool flag7 = BPZ == 0;
                    if (flag7)
                    {
                        CPZ--;
                        BPZ = 7;
                    }
                    else
                    {
                        BPZ--;
                    }
                    Chunk CH = Ground.CStack.GetChunk(CPXO, CPY, CPZ);
                    O[1] = DBlock.FromBlock(CH.BlockList[BPXO][BPY][BPZ]);
                    O[1].Chunk = CH;
                    O[1].RealBlock.CPosition = new Vector3((float)BPX, (float)BPY, (float)BPZ);
                    Chunk CH2 = Ground.CStack.GetChunk(CPXO, CPY2, CPZ);
                    O[3] = DBlock.FromBlock(CH2.BlockList[BPXO][BPY2][BPZ]);
                    O[3].Chunk = CH;
                    O[3].RealBlock.CPosition = new Vector3((float)BPX, (float)BPY2, (float)BPZ);
                }
                else
                {
                    bool flag8 = FacingDirections[0] == Direction.Backward;
                    if (flag8)
                    {
                        bool flag9 = BPZ == 7;
                        if (flag9)
                        {
                            CPZ++;
                            BPZ = 0;
                        }
                        else
                        {
                            BPZ++;
                        }
                        Chunk CH = Ground.CStack.GetChunk(CPXO, CPY, CPZ);
                        O[1] = DBlock.FromBlock(CH.BlockList[BPXO][BPY][BPZ]);
                        O[1].Chunk = CH;
                        O[1].RealBlock.CPosition = new Vector3((float)BPX, (float)BPY, (float)BPZ);
                        Chunk CH2 = Ground.CStack.GetChunk(CPXO, CPY2, CPZ);
                        O[3] = DBlock.FromBlock(CH2.BlockList[BPXO][BPY2][BPZ]);
                        O[3].Chunk = CH;
                        O[3].RealBlock.CPosition = new Vector3((float)BPX, (float)BPY2, (float)BPZ);
                    }
                }
                return O;
            }
        }

        /// <summary>
        /// 0= X, 1= Y, 2= UX, 3= UY
        /// </summary>
        // Token: 0x06000132 RID: 306 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
        public static bool[] GetFacingIsAir(Vector3 Position, Direction[] FacingDirections, Vector3 D)
        {
            Vector3 CP = Position / Chunk.Volume;
            checked
            {
                int CPX = (int)((double)CP.X);
                int CPY = (int)((double)CP.Y);
                int CPZ = (int)((double)CP.Z);
                Vector3 BP = (Position - new Vector3((float)(CPX * 400), (float)(CPY * 400), (float)(CPZ * 400))) / 50f;
                int BPX;
                for (BPX = (int)Math.Round((double)BP.X); BPX < 0; BPX += 8)
                {
                    CPX--;
                }
                while (BPX > 7)
                {
                    CPX++;
                    BPX -= 8;
                }
                int BPY;
                for (BPY = (int)Math.Round((double)BP.Y); BPY < 0; BPY += 8)
                {
                    CPY--;
                }
                while (BPY > 7)
                {
                    CPY++;
                    BPY -= 8;
                }
                int BPZ;
                for (BPZ = (int)Math.Round((double)BP.Z); BPZ < 0; BPZ += 8)
                {
                    CPZ--;
                }
                while (BPZ > 7)
                {
                    CPZ++;
                    BPZ -= 8;
                }
                bool[] O = new bool[5];
                bool flag = BPY == 7;
                int BPY2;
                int CPY2;
                if (flag)
                {
                    BPY2 = 0;
                    CPY2 = CPY + 1;
                }
                else
                {
                    BPY2 = BPY + 1;
                    CPY2 = CPY;
                }
                int BPXO = BPX;
                int CPXO = CPX;
                bool flag2 = FacingDirections[1] == Direction.Left;
                if (flag2)
                {
                    bool flag3 = BPX == 0;
                    if (flag3)
                    {
                        CPX--;
                        BPX = 7;
                    }
                    else
                    {
                        BPX--;
                    }
                }
                else
                {
                    bool flag4 = FacingDirections[1] == Direction.Right;
                    if (flag4)
                    {
                        bool flag5 = BPX == 7;
                        if (flag5)
                        {
                            CPX++;
                            BPX = 0;
                        }
                        else
                        {
                            BPX++;
                        }
                    }
                }
                Chunk CH = Ground.CStack.GetChunk(CPX, CPY, CPZ);
                O[0] = CH.AirGrid[BPX][BPY][BPZ];
                Chunk CH2 = Ground.CStack.GetChunk(CPX, CPY2, CPZ);
                O[2] = CH2.AirGrid[BPX][BPY2][BPZ];
                bool flag6 = FacingDirections[0] == Direction.Forward;
                if (flag6)
                {
                    bool flag7 = BPZ == 0;
                    if (flag7)
                    {
                        CPZ--;
                        BPZ = 7;
                    }
                    else
                    {
                        BPZ--;
                    }
                }
                else
                {
                    bool flag8 = FacingDirections[0] == Direction.Backward;
                    if (flag8)
                    {
                        bool flag9 = BPZ == 7;
                        if (flag9)
                        {
                            CPZ++;
                            BPZ = 0;
                        }
                        else
                        {
                            BPZ++;
                        }
                    }
                }
                CH = Ground.CStack.GetChunk(CPXO, CPY, CPZ);
                O[1] = CH.AirGrid[BPXO][BPY][BPZ];
                CH2 = Ground.CStack.GetChunk(CPXO, CPY2, CPZ);
                O[3] = CH2.AirGrid[BPXO][BPY2][BPZ];
                CH2 = Ground.CStack.GetChunk(CPX, CPY2, CPZ);
                O[4] = CH2.AirGrid[BPX][BPY2][BPZ];
                return O;
            }
        }

        // Token: 0x06000133 RID: 307 RVA: 0x0000F684 File Offset: 0x0000D884
        public static DBlock GetBlockInTheDirection(Vector3 Pos, Vector3 Dir, int MaxDistance, bool IsAir)
        {
            checked
            {
                IntVector3 RootI = new IntVector3((int)Math.Round((double)(Pos.X / 50f)), (int)Math.Round((double)(Pos.Y / 50f)), (int)Math.Round((double)(Pos.Z / 50f)));
                Vector3 RootChI = RootI / Chunk.Size;
                RootChI.X = (float)Math.Truncate((double)RootChI.X);
                RootChI.Y = (float)Math.Truncate((double)RootChI.Y);
                RootChI.Z = (float)Math.Truncate((double)RootChI.Z);
                Dir.Normalize();
                DBlock O = null;
                Vector3 NIndex = default;
                IntVector3 NChIndex = default;
                IntVector3 CPos = default;
                for (int i = 1; i <= MaxDistance; i++)
                {
                    Vector3 NPos = Pos + Dir * (float)i;
                    NIndex = new IntVector3((int)Math.Round((double)(NPos.X / 50f)), (int)Math.Round((double)(NPos.Y / 50f)), (int)Math.Round((double)(NPos.Z / 50f)));
                    NChIndex = IntVector3.FromV3Truncated(NIndex / Chunk.Size);
                    CPos = IntVector3.FromV3Truncated(NIndex - NChIndex * Chunk.Size);
                    Chunk CH = Ground.CStack.GetChunk(NChIndex.X, NChIndex.Y, NChIndex.Z);
                    bool flag = CH.AirGrid[CPos.X][CPos.Y][CPos.Z] == IsAir;
                    if (flag)
                    {
                        O = DBlock.FromBlock(CH.BlockList[CPos.X][CPos.Y][CPos.Z]);
                        O.RealBlock.CPosition = CPos;
                        O.Chunk = CH;
                        return O;
                    }
                }
                return O;
            }
        }

        // Token: 0x06000134 RID: 308 RVA: 0x0000F87C File Offset: 0x0000DA7C
        public static DBlock GetBeforeBlockInTheDirection(Vector3 Pos, Vector3 Dir, int MaxDistance, bool IsAir)
        {
            checked
            {
                IntVector3 RootI = new IntVector3((int)Math.Round((double)(Pos.X / 50f)), (int)Math.Round((double)(Pos.Y / 50f)), (int)Math.Round((double)(Pos.Z / 50f)));
                Vector3 RootChI = RootI / Chunk.Size;
                RootChI.X = (float)Math.Truncate((double)RootChI.X);
                RootChI.Y = (float)Math.Truncate((double)RootChI.Y);
                RootChI.Z = (float)Math.Truncate((double)RootChI.Z);
                Dir.Normalize();
                DBlock O = null;
                Vector3 NIndex = default;
                IntVector3 NChIndex = default;
                IntVector3 CPos = default;
                Chunk BChunk = null;
                IntVector3 BCPos = default;
                for (int i = 1; i <= MaxDistance; i++)
                {
                    Vector3 NPos = Pos + Dir * (float)i;
                    NIndex = new IntVector3((int)Math.Round((double)(NPos.X / 50f)), (int)Math.Round((double)(NPos.Y / 50f)), (int)Math.Round((double)(NPos.Z / 50f)));
                    NChIndex = IntVector3.FromV3Truncated(NIndex / Chunk.Size);
                    NChIndex.X = Convert.ToInt32(Math.Truncate(new decimal(NChIndex.X)));
                    NChIndex.Y = Convert.ToInt32(Math.Truncate(new decimal(NChIndex.Y)));
                    NChIndex.Z = Convert.ToInt32(Math.Truncate(new decimal(NChIndex.Z)));
                    CPos = IntVector3.FromV3Rounded(NIndex - NChIndex * Chunk.Size);
                    bool flag = CPos.X >= 8;
                    if (flag)
                    {
                        CPos.X -= 8;
                        NChIndex.X++;
                    }
                    bool flag2 = CPos.Y >= 8;
                    if (flag2)
                    {
                        CPos.Y -= 8;
                        NChIndex.Y++;
                    }
                    bool flag3 = CPos.Z >= 8;
                    if (flag3)
                    {
                        CPos.Z -= 8;
                        NChIndex.Z++;
                    }
                    Chunk CH = Ground.CStack.GetChunk(NChIndex);
                    bool flag4 = CH.AirGrid[CPos.X][CPos.Y][CPos.Z] == IsAir;
                    if (flag4)
                    {
                        bool flag5 = Information.IsNothing(BCPos);
                        DBlock GetBeforeBlockInTheDirection;
                        if (flag5)
                        {
                            GetBeforeBlockInTheDirection = null;
                        }
                        else
                        {
                            O = DBlock.FromBlock(BChunk.BlockList[BCPos.X][BCPos.Y][BCPos.Z]);
                            O.RealBlock.CPosition = BCPos;
                            O.Chunk = BChunk;
                            GetBeforeBlockInTheDirection = O;
                        }
                        return GetBeforeBlockInTheDirection;
                    }
                    BCPos = CPos;
                    BChunk = CH;
                }
                return O;
            }
        }

        // Token: 0x06000135 RID: 309 RVA: 0x0000FB84 File Offset: 0x0000DD84
        public static int GetIsAirDistanceInTheDirection(Vector3 Pos, Vector3 Dir, int MaxDistance, bool IsAir)
        {
            IntVector3 RootI = IntVector3.FromV3Rounded((float)Math.Round((double)(Pos.X / 50f)), (float)Math.Round((double)(Pos.Y / 50f)), (float)Math.Round((double)(Pos.Z / 50f)));
            Vector3 RootChI = RootI / Chunk.Size;
            RootChI.X = (float)Math.Truncate((double)RootChI.X);
            RootChI.Y = (float)Math.Truncate((double)RootChI.Y);
            RootChI.Z = (float)Math.Truncate((double)RootChI.Z);
            Dir.Normalize();
            Vector3 NIndex = default;
            IntVector3 NChIndex = default;
            IntVector3 CPos = default;
            int num = Math.Sign(MaxDistance);
            int i = 1;
            checked
            {
                while ((num >> 31 ^ i) <= (num >> 31 ^ MaxDistance))
                {
                    Vector3 NPos = Pos + Dir * (float)i;
                    NIndex = new IntVector3((int)Math.Round((double)(NPos.X / 50f)), (int)Math.Round((double)(NPos.Y / 50f)), (int)Math.Round((double)(NPos.Z / 50f)));
                    NChIndex = IntVector3.FromV3Truncated(NIndex / Chunk.Size);
                    NChIndex.X = Convert.ToInt32(Math.Truncate(new decimal(NChIndex.X)));
                    NChIndex.Y = Convert.ToInt32(Math.Truncate(new decimal(NChIndex.Y)));
                    NChIndex.Z = Convert.ToInt32(Math.Truncate(new decimal(NChIndex.Z)));
                    CPos = IntVector3.FromV3Rounded(NIndex - NChIndex * Chunk.Size);
                    bool flag = CPos.X >= 8;
                    if (flag)
                    {
                        CPos.X -= 8;
                        NChIndex.X++;
                    }
                    bool flag2 = CPos.Y >= 8;
                    if (flag2)
                    {
                        CPos.Y -= 8;
                        NChIndex.Y++;
                    }
                    bool flag3 = CPos.Z >= 8;
                    if (flag3)
                    {
                        CPos.Z -= 8;
                        NChIndex.Z++;
                    }
                    Chunk CH = Ground.CStack.GetChunk(NChIndex);
                    bool flag4 = CH.AirGrid[CPos.X][CPos.Y][CPos.Z] == IsAir;
                    if (flag4)
                    {
                        return i;
                    }
                    i += num;
                }
                return -1;
            }
        }

        // Token: 0x06000136 RID: 310 RVA: 0x0000FE34 File Offset: 0x0000E034
        public static void SetBlock(ref DBlock B, BlockType BT)
        {
            bool surfaceRelation = B.RealBlock.SurfaceRelation;
            checked
            {
                if (surfaceRelation)
                {
                    List<Block> SBLst = B.Chunk.SurfaceBlocks.ToList<Block>();
                    SBLst.Remove(B.RealBlock);
                    B.Chunk.SurfaceBlocks = SBLst.ToArray();
                    B.Chunk.FilledSB--;
                    int num = (int)(B.Chunk.BIDFilledI - 1);
                    for (int i = 0; i <= num; i++)
                    {
                        byte[] BArr = B.Chunk.BIDList[i];
                        bool flag = BArr[0] == B.RealBlock.CPosition.X && BArr[1] == B.RealBlock.CPosition.Y && BArr[2] == B.RealBlock.CPosition.Z;
                        if (flag)
                        {
                            List<byte[]> BArrLst = B.Chunk.BIDList.ToList<byte[]>();
                            BArrLst.RemoveAt(i);
                            BArrLst.Add(null);
                            B.Chunk.BIDList = BArrLst.ToArray();
                            Chunk chunk = B.Chunk;
                            chunk.BIDFilledI -= 1;
                            List<Vector4> BlockTranslationsLst = B.Chunk.BlockTranslations.ToList<Vector4>();
                            BlockTranslationsLst.RemoveAt(i);
                            B.Chunk.BlockTranslations = BlockTranslationsLst.ToArray();
                            B.Chunk.CountForBlockTypes[(int)B.RealBlock.BID]--;
                            break;
                        }
                    }
                }
                bool flag2 = BT.ID > 0;
                if (flag2)
                {
                    B.RealBlock.SurfaceRelation = true;
                }
                else
                {
                    B.RealBlock.SurfaceRelation = false;
                }
                B.RealBlock.IsAir = BT.IsAir;
                B.RealBlock.BID = BT.ID;
                B.Chunk.AirGrid[(int)B.RealBlock.CPosition.X][(int)B.RealBlock.CPosition.Y][(int)B.RealBlock.CPosition.Z] = BT.IsAir;
                B.Chunk.BlockList[(int)B.RealBlock.CPosition.X][(int)B.RealBlock.CPosition.Y][(int)B.RealBlock.CPosition.Z] = B.RealBlock;
                bool flag3 = !B.Chunk.IsInTheSurface;
                if (flag3)
                {
                    B.Chunk.IsInTheSurface = true;
                    B.Chunk.SurfaceChunkIndex = Ground.FilledSFC;
                    Ground.SurfaceChunks[Ground.FilledSFC] = B.Chunk;
                    Ground.FilledSFC++;
                }
                bool surfaceRelation2 = B.RealBlock.SurfaceRelation;
                if (surfaceRelation2)
                {
                    B.Chunk.SurfaceBlocks[B.Chunk.FilledSB] = B.RealBlock;
                    B.Chunk.FilledSB++;
                }
                B.Chunk.Changed = true;
                Ground.CStack.SavingChunks[Ground.CStack.nSavingChunks] = B.Chunk;
                Ground.CStack.nSavingChunks++;
                B.Chunk.Removed = false;
                bool surfaceRelation3 = B.RealBlock.SurfaceRelation;
                if (surfaceRelation3)
                {
                    byte[] BBlock = new byte[]
                    {
                        B.RealBlock.CPosition.X,
                        B.RealBlock.CPosition.Y,
                        B.RealBlock.CPosition.Z,
                        BT.ID,
                        BT.Varient
                    };
                    B.Chunk.BIDList[(int)B.Chunk.BIDFilledI] = BBlock;
                    bool flag4 = B.Chunk.BIDList.Length < (int)(B.Chunk.BIDFilledI + 10);
                    if (flag4)
                    {
                        Array.Resize<byte[]>(ref B.Chunk.BIDList, B.Chunk.BIDList.Length + 20);
                    }
                    Vector4 BWorld = unchecked(new Vector4((float)(checked(BBlock[0] * 50)) + B.Chunk.Position.X, (float)(checked(BBlock[1] * 50)) + B.Chunk.Position.Y, (float)(checked(BBlock[2] * 50)) + B.Chunk.Position.Z, 1f));
                    Array.Resize<Vector4>(ref B.Chunk.BlockTranslations, (int)(B.Chunk.BIDFilledI + 1));
                    B.Chunk.BlockTranslations[(int)B.Chunk.BIDFilledI] = BWorld;
                    B.Chunk.CountForBlockTypes[(int)BT.ID]++;
                    Chunk chunk2 = B.Chunk;
                    chunk2.BIDFilledI += 1;
                    B.Chunk.GenerateBIDForBTranslations();
                }
                Main.IsGroundChanged = true;
            }
        }

        // Token: 0x06000137 RID: 311 RVA: 0x00010344 File Offset: 0x0000E544
        public static void BreakBlock(ref DBlock B)
        {
            bool surfaceRelation = B.RealBlock.SurfaceRelation;
            checked
            {
                if (surfaceRelation)
                {
                    List<Block> SBLst = B.Chunk.SurfaceBlocks.ToList<Block>();
                    SBLst.Remove(B.RealBlock);
                    B.Chunk.SurfaceBlocks = SBLst.ToArray();
                    B.Chunk.FilledSB--;
                    int num = (int)(B.Chunk.BIDFilledI - 1);
                    for (int i = 0; i <= num; i++)
                    {
                        byte[] BArr = B.Chunk.BIDList[i];
                        bool flag = BArr[0] == B.RealBlock.CPosition.X && BArr[1] == B.RealBlock.CPosition.Y && BArr[2] == B.RealBlock.CPosition.Z;
                        if (flag)
                        {
                            List<byte[]> BArrLst = B.Chunk.BIDList.ToList<byte[]>();
                            BArrLst.RemoveAt(i);
                            BArrLst.Add(null);
                            B.Chunk.BIDList = BArrLst.ToArray();
                            Chunk chunk = B.Chunk;
                            chunk.BIDFilledI -= 1;
                            List<Vector4> BlockTranslationsLst = B.Chunk.BlockTranslations.ToList<Vector4>();
                            BlockTranslationsLst.RemoveAt(i);
                            B.Chunk.BlockTranslations = BlockTranslationsLst.ToArray();
                            B.Chunk.CountForBlockTypes[(int)B.RealBlock.BID]--;
                            break;
                        }
                    }
                    B.Chunk.GenerateBIDForBTranslations();
                }
                B.RealBlock.BID = 0;
                B.RealBlock.IsAir = true;
                B.RealBlock.SurfaceRelation = false;
                B.Chunk.AirGrid[(int)B.RealBlock.CPosition.X][(int)B.RealBlock.CPosition.Y][(int)B.RealBlock.CPosition.Z] = true;
                B.Chunk.BlockList[(int)B.RealBlock.CPosition.X][(int)B.RealBlock.CPosition.Y][(int)B.RealBlock.CPosition.Z] = B.RealBlock;
                B.Chunk.Changed = true;
                Ground.CStack.SavingChunks[Ground.CStack.nSavingChunks] = B.Chunk;
                Ground.CStack.nSavingChunks++;
                B.Chunk.Removed = false;
                Ground.ScanBlockVisibilityChanges(B);
                Main.IsGroundChanged = true;
            }
        }

        // Token: 0x06000138 RID: 312 RVA: 0x000105F0 File Offset: 0x0000E7F0
        public static void SetBlockVisible(DBlock B)
        {
            bool flag = !B.RealBlock.SurfaceRelation;
            checked
            {
                if (flag)
                {
                    bool flag2 = !B.Chunk.IsInTheSurface;
                    if (flag2)
                    {
                        B.Chunk.IsInTheSurface = true;
                        B.Chunk.SurfaceChunkIndex = Ground.FilledSFC;
                        Ground.SurfaceChunks[Ground.FilledSFC] = B.Chunk;
                        Ground.FilledSFC++;
                    }
                    B.RealBlock.SurfaceRelation = (B.RealBlock.BID > 0);
                    B.Chunk.Changed = true;
                    bool surfaceRelation = B.RealBlock.SurfaceRelation;
                    if (surfaceRelation)
                    {
                        B.Chunk.SurfaceBlocks[B.Chunk.FilledSB] = B.RealBlock;
                        B.Chunk.FilledSB++;
                        byte[] BBlock = new byte[]
                        {
                            B.RealBlock.CPosition.X,
                            B.RealBlock.CPosition.Y,
                            B.RealBlock.CPosition.Z,
                            B.RealBlock.BID,
                            B.RealBlock.Varient
                        };
                        B.Chunk.BIDList[(int)B.Chunk.BIDFilledI] = BBlock;
                        bool flag3 = B.Chunk.BIDList.Length < (int)(B.Chunk.BIDFilledI + 10);
                        if (flag3)
                        {
                            Array.Resize<byte[]>(ref B.Chunk.BIDList, B.Chunk.BIDList.Length + 40);
                        }
                        Vector4 BWorld = unchecked(new Vector4((float)(checked(BBlock[0] * 50)) + B.Chunk.Position.X, (float)(checked(BBlock[1] * 50)) + B.Chunk.Position.Y, (float)(checked(BBlock[2] * 50)) + B.Chunk.Position.Z, 1f));
                        Array.Resize<Vector4>(ref B.Chunk.BlockTranslations, (int)(B.Chunk.BIDFilledI + 1));
                        B.Chunk.BlockTranslations[(int)B.Chunk.BIDFilledI] = BWorld;
                        B.Chunk.CountForBlockTypes[(int)B.RealBlock.BID]++;
                        Chunk chunk = B.Chunk;
                        chunk.BIDFilledI += 1;
                        B.Chunk.GenerateBIDForBTranslations();
                    }
                }
                Main.IsGroundChanged = true;
            }
        }

        // Token: 0x06000139 RID: 313 RVA: 0x00010860 File Offset: 0x0000EA60
        public static void SetBlockVisibleForGenarating(ref Block B, ref Chunk Ch)
        {
            Ch.IsInTheSurface = true;
            B.SurfaceRelation = true;
            Ch.SurfaceBlocks[Ch.FilledSB] = B;
            checked
            {
                Ch.FilledSB++;
                byte[] BBlock = new byte[]
                {
                    B.CPosition.X,
                    B.CPosition.Y,
                    B.CPosition.Z,
                    B.BID,
                    B.Varient
                };
                Ch.BIDList[(int)Ch.BIDFilledI] = BBlock;
                Chunk chunk = Ch;
                chunk.BIDFilledI += 1;
                bool flag = Ch.BIDList.Length < (int)(Ch.BIDFilledI + 10);
                if (flag)
                {
                    Array.Resize<byte[]>(ref Ch.BIDList, Ch.BIDList.Length + 40);
                }
            }
        }

        // Token: 0x0600013A RID: 314 RVA: 0x00010938 File Offset: 0x0000EB38
        public static void PutChunkVisible(Chunk Ch)
        {
            bool flag = !Ch.IsInTheSurface;
            checked
            {
                if (flag)
                {
                    Ch.IsInTheSurface = true;
                    Ch.SurfaceChunkIndex = Ground.FilledSFC;
                    Ground.SurfaceChunks[Ground.FilledSFC] = Ch;
                    Ground.FilledSFC++;
                    Ch.Changed = true;
                }
                Main.IsGroundChanged = true;
            }
        }

        // Token: 0x0600013B RID: 315 RVA: 0x0001098C File Offset: 0x0000EB8C
        public static void ScanBlockVisibilityChanges(DBlock B)
        {
            Chunk Ch = B.Chunk;
            bool flag = B.RealBlock.CPosition.X == 7;
            checked
            {
                if (flag)
                {
                    Chunk CH2 = Ground.CStack.GetChunk(B.Chunk.Index.X + 1, B.Chunk.Index.Y, B.Chunk.Index.Z);
                    bool flag2 = !CH2.AirGrid[0][(int)B.RealBlock.CPosition.Y][(int)B.RealBlock.CPosition.Z];
                    if (flag2)
                    {
                        DBlock DB = DBlock.FromBlock(CH2.BlockList[0][(int)B.RealBlock.CPosition.Y][(int)B.RealBlock.CPosition.Z], CH2);
                        DB.RealBlock.CPosition = B.RealBlock.CPosition - Ground.V3_7X;
                        Ground.SetBlockVisible(DB);
                    }
                }
                else
                {
                    bool flag3 = !Ch.AirGrid[(int)(B.RealBlock.CPosition.X + 1)][(int)B.RealBlock.CPosition.Y][(int)B.RealBlock.CPosition.Z];
                    if (flag3)
                    {
                        DBlock DB2 = DBlock.FromBlock(Ch.BlockList[(int)(B.RealBlock.CPosition.X + 1)][(int)B.RealBlock.CPosition.Y][(int)B.RealBlock.CPosition.Z], Ch);
                        DB2.RealBlock.CPosition = B.RealBlock.CPosition + Vector3.Right;
                        Ground.SetBlockVisible(DB2);
                    }
                }
                bool flag4 = B.RealBlock.CPosition.X == 0;
                if (flag4)
                {
                    Chunk CH2 = Ground.CStack.GetChunk(B.Chunk.Index.X - 1, B.Chunk.Index.Y, B.Chunk.Index.Z);
                    bool flag5 = !CH2.AirGrid[7][(int)B.RealBlock.CPosition.Y][(int)B.RealBlock.CPosition.Z];
                    if (flag5)
                    {
                        DBlock DB3 = DBlock.FromBlock(CH2.BlockList[7][(int)B.RealBlock.CPosition.Y][(int)B.RealBlock.CPosition.Z], CH2);
                        DB3.RealBlock.CPosition = B.RealBlock.CPosition + Ground.V3_7X;
                        Ground.SetBlockVisible(DB3);
                    }
                }
                else
                {
                    bool flag6 = !Ch.AirGrid[(int)(B.RealBlock.CPosition.X - 1)][(int)B.RealBlock.CPosition.Y][(int)B.RealBlock.CPosition.Z];
                    if (flag6)
                    {
                        DBlock DB4 = DBlock.FromBlock(Ch.BlockList[(int)(B.RealBlock.CPosition.X - 1)][(int)B.RealBlock.CPosition.Y][(int)B.RealBlock.CPosition.Z], Ch);
                        DB4.RealBlock.CPosition = B.RealBlock.CPosition + Vector3.Left;
                        Ground.SetBlockVisible(DB4);
                    }
                }
                bool flag7 = B.RealBlock.CPosition.Y == 7;
                if (flag7)
                {
                    Chunk CH2 = Ground.CStack.GetChunk(B.Chunk.Index.X, B.Chunk.Index.Y + 1, B.Chunk.Index.Z);
                    bool flag8 = !CH2.AirGrid[(int)B.RealBlock.CPosition.X][0][(int)B.RealBlock.CPosition.Z];
                    if (flag8)
                    {
                        DBlock DB5 = DBlock.FromBlock(CH2.BlockList[(int)B.RealBlock.CPosition.X][0][(int)B.RealBlock.CPosition.Z], CH2);
                        DB5.RealBlock.CPosition = B.RealBlock.CPosition - Ground.V3_7Y;
                        Ground.SetBlockVisible(DB5);
                    }
                }
                else
                {
                    bool flag9 = !Ch.AirGrid[(int)B.RealBlock.CPosition.X][(int)(B.RealBlock.CPosition.Y + 1)][(int)B.RealBlock.CPosition.Z];
                    if (flag9)
                    {
                        DBlock DB6 = DBlock.FromBlock(Ch.BlockList[(int)B.RealBlock.CPosition.X][(int)(B.RealBlock.CPosition.Y + 1)][(int)B.RealBlock.CPosition.Z], Ch);
                        DB6.RealBlock.CPosition = B.RealBlock.CPosition + Vector3.Up;
                        Ground.SetBlockVisible(DB6);
                    }
                }
                bool flag10 = B.RealBlock.CPosition.Y == 0;
                if (flag10)
                {
                    Chunk CH2 = Ground.CStack.GetChunk(B.Chunk.Index.X, B.Chunk.Index.Y - 1, B.Chunk.Index.Z);
                    bool flag11 = !CH2.AirGrid[(int)B.RealBlock.CPosition.X][7][(int)B.RealBlock.CPosition.Z];
                    if (flag11)
                    {
                        DBlock DB7 = DBlock.FromBlock(CH2.BlockList[(int)B.RealBlock.CPosition.X][7][(int)B.RealBlock.CPosition.Z], CH2);
                        DB7.RealBlock.CPosition = B.RealBlock.CPosition + Ground.V3_7Y;
                        Ground.SetBlockVisible(DB7);
                    }
                }
                else
                {
                    bool flag12 = !Ch.AirGrid[(int)B.RealBlock.CPosition.X][(int)(B.RealBlock.CPosition.Y - 1)][(int)B.RealBlock.CPosition.Z];
                    if (flag12)
                    {
                        DBlock DB8 = DBlock.FromBlock(Ch.BlockList[(int)B.RealBlock.CPosition.X][(int)(B.RealBlock.CPosition.Y - 1)][(int)B.RealBlock.CPosition.Z], Ch);
                        DB8.RealBlock.CPosition = B.RealBlock.CPosition + Vector3.Down;
                        Ground.SetBlockVisible(DB8);
                    }
                }
                bool flag13 = B.RealBlock.CPosition.Z == 7;
                if (flag13)
                {
                    Chunk CH2 = Ground.CStack.GetChunk(B.Chunk.Index.X, B.Chunk.Index.Y, B.Chunk.Index.Z + 1);
                    bool flag14 = !CH2.AirGrid[(int)B.RealBlock.CPosition.X][(int)B.RealBlock.CPosition.Y][0];
                    if (flag14)
                    {
                        DBlock DB9 = DBlock.FromBlock(CH2.BlockList[(int)B.RealBlock.CPosition.X][(int)B.RealBlock.CPosition.Y][0], CH2);
                        DB9.RealBlock.CPosition = B.RealBlock.CPosition - Ground.V3_7Z;
                        Ground.SetBlockVisible(DB9);
                    }
                }
                else
                {
                    bool flag15 = !Ch.AirGrid[(int)B.RealBlock.CPosition.X][(int)B.RealBlock.CPosition.Y][(int)(B.RealBlock.CPosition.Z + 1)];
                    if (flag15)
                    {
                        DBlock DB10 = DBlock.FromBlock(Ch.BlockList[(int)B.RealBlock.CPosition.X][(int)B.RealBlock.CPosition.Y][(int)(B.RealBlock.CPosition.Z + 1)], Ch);
                        DB10.RealBlock.CPosition = B.RealBlock.CPosition + Vector3.Backward;
                        Ground.SetBlockVisible(DB10);
                    }
                }
                bool flag16 = B.RealBlock.CPosition.Z == 0;
                if (flag16)
                {
                    Chunk CH2 = Ground.CStack.GetChunk(B.Chunk.Index.X, B.Chunk.Index.Y, B.Chunk.Index.Z - 1);
                    bool flag17 = !CH2.AirGrid[(int)B.RealBlock.CPosition.X][(int)B.RealBlock.CPosition.Y][7];
                    if (flag17)
                    {
                        DBlock DB11 = DBlock.FromBlock(CH2.BlockList[(int)B.RealBlock.CPosition.X][(int)B.RealBlock.CPosition.Y][7], CH2);
                        DB11.RealBlock.CPosition = B.RealBlock.CPosition + Ground.V3_7Z;
                        Ground.SetBlockVisible(DB11);
                    }
                }
                else
                {
                    bool flag18 = !Ch.AirGrid[(int)B.RealBlock.CPosition.X][(int)B.RealBlock.CPosition.Y][(int)(B.RealBlock.CPosition.Z - 1)];
                    if (flag18)
                    {
                        DBlock DB12 = DBlock.FromBlock(Ch.BlockList[(int)B.RealBlock.CPosition.X][(int)B.RealBlock.CPosition.Y][(int)(B.RealBlock.CPosition.Z - 1)], Ch);
                        DB12.RealBlock.CPosition = B.RealBlock.CPosition + Vector3.Forward;
                        Ground.SetBlockVisible(DB12);
                    }
                }
                Main.IsGroundChanged = true;
            }
        }

        // Token: 0x0400014D RID: 333
        public static float GroundLvl = 0f;

        // Token: 0x0400014E RID: 334
        public static bool Genarated = false;

        // Token: 0x0400014F RID: 335
        public static int LastChunkIndex = 0;

        // Token: 0x04000150 RID: 336
        public static Stack CStack;

        // Token: 0x04000151 RID: 337
        public const int BlockSize = 50;

        // Token: 0x04000152 RID: 338
        public static Vector3 BlockSizeV3 = new Vector3(50f);

        // Token: 0x04000153 RID: 339
        public static Vector3 BlockSizeYonlyV3 = new Vector3(0f, 50f, 0f);

        // Token: 0x04000154 RID: 340
        public static Vector3 BlockSizeHalfYonlyV3 = new Vector3(0f, 25f, 0f);

        // Token: 0x04000155 RID: 341
        public static int LastBlockIndex = 0;

        // Token: 0x04000156 RID: 342
        public static int MaxHeight;

        // Token: 0x04000157 RID: 343
        public static int ChunkBarHeight;

        // Token: 0x04000158 RID: 344
        public static int BaseHeight = 10;

        // Token: 0x04000159 RID: 345
        public static int FilledSFC = 0;

        // Token: 0x0400015A RID: 346
        public static Chunk[] SurfaceChunks = new Chunk[5001];

        // Token: 0x0400015B RID: 347
        public static int TmpFilledSFC = 0;

        // Token: 0x0400015C RID: 348
        public static Chunk[] TmpSurfaceChunks = new Chunk[41];

        // Token: 0x0400015D RID: 349
        public static bool SpeedSaving = true;

        // Token: 0x0400015E RID: 350
        public static Stopwatch StpWatch = new Stopwatch();

        // Token: 0x0400015F RID: 351
        public static Vector3 V3_7X = new Vector3(7f, 0f, 0f);

        // Token: 0x04000160 RID: 352
        public static Vector3 V3_7Y = new Vector3(0f, 7f, 0f);

        // Token: 0x04000161 RID: 353
        public static Vector3 V3_7Z = new Vector3(0f, 0f, 7f);

        // Token: 0x0200003F RID: 63
        public class BlockEnvironment
        {
            // Token: 0x04000285 RID: 645
            public DBlock CurrentBlock;

            // Token: 0x04000286 RID: 646
            public Block LegsBlock;

            // Token: 0x04000287 RID: 647
            public Block BodyBlock;

            // Token: 0x04000288 RID: 648
            public Block HeadBlock;
        }
    }
}
