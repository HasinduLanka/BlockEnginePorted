using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Xml.Serialization;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;
namespace BlockEngine
{
    // Token: 0x02000024 RID: 36
    public class Loader
    {
        // Token: 0x0600014A RID: 330 RVA: 0x00013544 File Offset: 0x00011744
        static Loader()
        {
            // Note: this type is marked as 'beforefieldinit'.
            checked
            {
                Loader.ThrCachedChunkSavers = new Thread[Loader.nThrCachedChunkSavers + 1];
                Loader.ThrCachedChunkSaversIsCompleted = new bool[Loader.nThrCachedChunkSavers + 1];
                Loader.SaveCashedChunksCompleted = true;
                Loader.ChunkIndices = new IntVector3[3001];
            }
        }

        // Token: 0x0600014C RID: 332 RVA: 0x0001360C File Offset: 0x0001180C
        public static bool CheckIfMapExits(string MMapName)
        {
            bool flag = Directory.Exists("Maps/" + MMapName);
            if (flag)
            {
                bool flag2 = File.Exists("Maps/" + MMapName + "/I.Map");
                if (flag2)
                {
                    return true;
                }
            }
            return false;
        }

        // Token: 0x0600014D RID: 333 RVA: 0x00013668 File Offset: 0x00011868
        public static void CreateMap(string MMapName)
        {
            Loader.MapName = MMapName;
            checked
            {
                Loader.MInfo = new Loader.MapInfo
                {
                    Name = Loader.MapName,
                    HMapSize = HeightMap.Size,
                    PlayerPosition = new Vector3((float)((double)(400 * Stack.Size.X) / 2.0), 1000f, (float)((double)(400 * Stack.Size.Z) / 2.0))
                };
                Loader.MapDir = "Maps/" + Loader.MapName;
                Loader.MapRoot = Loader.MapDir + "/";
                Loader.MapInfoFile = Loader.MapRoot + "I.Map";
                Loader.SavedHMapFile = Loader.MapRoot + "SHM";
                Loader.FileEntity = Loader.MapRoot + "E.xml";
                Directory.CreateDirectory(Loader.MapDir);
                Loader.MInfo.Save(Loader.MapInfoFile);
                XEntity.Save(Loader.FileEntity, new List<XEntity>());
                if (File.Exists(Loader.SavedHMapFile))
                {
                    List<string> Lst = File.ReadAllText(Loader.SavedHMapFile).Split(new char[]
                    {
                        ';'
                    }).ToList<string>();
                    Lst.RemoveAt(Lst.Count - 1);
                    Loader.SavedHMaps = Loader.SavedNamesToIndices(Lst.ToArray()).ToList<IntVector3>();
                    IntVector3 MaxHMID = IntVector3.Zero;

                    foreach (IntVector3 HM in Loader.SavedHMaps)
                    {
                        bool flag2 = MaxHMID.X < HM.X + 1 && MaxHMID.Z < HM.Z + 1;
                        if (flag2)
                        {
                            MaxHMID = new IntVector3(HM.X + 1, 0, HM.Z + 1);
                        }
                    }

                    Loader.MaxWorldBorders = MaxHMID * (int)Math.Round((double)HeightMap.Size / 8.0);
                }
                else
                {
                    Loader.SavedHMaps = new List<IntVector3>();
                    File.WriteAllText(Loader.SavedHMapFile, "");
                    Loader.MaxWorldBorders = new IntVector3((int)Math.Round((double)HeightMap.Size / 8.0), Ground.MaxHeight, (int)Math.Round((double)HeightMap.Size / 8.0));
                }
            }
        }

        // Token: 0x0600014E RID: 334 RVA: 0x00013900 File Offset: 0x00011B00
        public static void LoadWorld(string MMapName)
        {
            Main.Log("Loading your world", true);
            Loader.MapName = MMapName;
            Loader.MapDir = "Maps/" + Loader.MapName;
            Loader.MapRoot = Loader.MapDir + "/";
            Loader.MapInfoFile = Loader.MapRoot + "I.Map";
            Loader.SavedHMapFile = Loader.MapRoot + "SHM";
            Loader.FileEntity = Loader.MapRoot + "E.xml";
            Directory.CreateDirectory("Maps");
            Loader.MInfo = Loader.MapInfo.Load(Loader.MapInfoFile);
            checked
            {
                if (Loader.MInfo == null)
                {
                    Main.Log("Error occured!. World Information file is corrupted. Creating a new Map info file.", true);
                    Loader.MInfo = new Loader.MapInfo
                    {
                        Name = Loader.MapName,
                        HMapSize = HeightMap.Size,
                        PlayerPosition = new Vector3((float)((double)(400 * Stack.Size.X) / 2.0), 1000f, (float)((double)(400 * Stack.Size.Z) / 2.0))
                    };
                    Loader.MInfo.Save(Loader.MapInfoFile);
                    Loader.MInfo = Loader.MapInfo.Load(Loader.MapInfoFile);
                }
                HeightMap.Size = Loader.MInfo.HMapSize;
                bool flag2 = Stack.Size.X >= HeightMap.Size / 8;
                if (flag2)
                {
                    int NewSS = HeightMap.Size / 8 - 1;
                    Stack.Size.X = NewSS;
                    Stack.Size.Z = NewSS;
                    Stack.Volume = Stack.Size * Chunk.Size * 50f;
                }
                Ground.MaxHeight = (Stack.Size * 8).Y;
                Ground.ChunkBarHeight = Stack.Size.Y;
                List<string> LstHM = File.ReadAllText(Loader.SavedHMapFile).Split(new char[]
                {
                    ';'
                }).ToList<string>();
                LstHM.RemoveAt(LstHM.Count - 1);
                Loader.SavedHMaps = Loader.SavedNamesToIndices(LstHM.ToArray()).ToList<IntVector3>();
                IntVector3 MaxHMID = IntVector3.Zero;

                foreach (IntVector3 HM in Loader.SavedHMaps)
                {
                    bool flag3 = MaxHMID.X < HM.X + 1 && MaxHMID.Z < HM.Z + 1;
                    if (flag3)
                    {
                        MaxHMID = new IntVector3(HM.X + 1, 0, HM.Z + 1);
                    }
                }

                Loader.MaxWorldBorders = MaxHMID * (int)Math.Round((double)HeightMap.Size / 8.0);
                Ground.CStack = new Stack();
                Ground.CStack.NewChunkList();
                Ground.FilledSFC = 0;
                Ground.SurfaceChunks = new Chunk[9001];
                Ground.CStack.LoadChunks(Ground.ChunkIndexOfPosition(Loader.MInfo.PlayerPosition));
                Ground.Genarated = true;
                Main.Log("World loaded!", true);
            }
        }

        // Token: 0x0600014F RID: 335 RVA: 0x00013C2C File Offset: 0x00011E2C
        public static void SaveChunkBar(Chunk[] Ch, bool Dispose = false)
        {
            checked
            {
                byte[] ChunkBarByteCode = new byte[Loader.ChunkByteCodeLength * Ch.Length - 1 + 1];
                int num = Ch.Length - 1;
                for (int i = 0; i <= num; i++)
                {
                    Array.Copy(Loader.GenarateChunkByteCode(Ch[i]), 0, ChunkBarByteCode, Loader.ChunkByteCodeLength * i, Loader.ChunkByteCodeLength);
                    bool flag = Dispose | Ch[i].Removed;
                    if (flag)
                    {
                        Ch[i].Dispose();
                    }
                }
                File.WriteAllBytes(Loader.ChunkBarPathOf(Ch[0]), ChunkBarByteCode);
            }
        }

        // Token: 0x06000150 RID: 336 RVA: 0x00013CB0 File Offset: 0x00011EB0
        public static byte[] GenarateChunkBarByteCode(Chunk[] Ch)
        {
            checked
            {
                byte[] ChunkBarByteCode = new byte[Loader.ChunkByteCodeLength * Ch.Length - 1 + 1];
                int num = Ch.Length - 1;
                for (int i = 0; i <= num; i++)
                {
                    Array.Copy(Loader.GenarateChunkByteCode(Ch[i]), 0, ChunkBarByteCode, Loader.ChunkByteCodeLength * i, Loader.ChunkByteCodeLength);
                }
                return ChunkBarByteCode;
            }
        }

        // Token: 0x06000151 RID: 337 RVA: 0x00013D04 File Offset: 0x00011F04
        public static void SaveChunks(Chunk[] Chunks, int Count, bool DisposeChunks)
        {
            int BarCount = 0;
            Chunk[][] Bars = Loader.CreateChunkBarsFromChunks(Chunks, Count, ref BarCount);
            checked
            {
                int num = BarCount - 1;
                for (int i = 0; i <= num; i++)
                {
                    Loader.SaveChunkBar(Bars[i], DisposeChunks);
                }
            }
        }

        // Token: 0x06000152 RID: 338 RVA: 0x00013D38 File Offset: 0x00011F38
        private static Chunk[][] CreateChunkBarsFromChunks(Chunk[] Chunks, int Count, ref int BarCount)
        {
            int nBars = 0;
            checked
            {
                Chunk[][] Bars = new Chunk[Count - 1 + 1][];
                IntVector3[] BarIndices = new IntVector3[Count - 1 + 1];
                int num = Count - 1;
                for (int i = 0; i <= num; i++)
                {
                    Chunk Ch = Chunks[i];
                    IntVector3 ChBarIndex = new IntVector3(Ch.Index.X, 0, Ch.Index.Z);
                    bool flag = BarIndices.Contains(ChBarIndex);
                    if (flag)
                    {
                        int num2 = nBars - 1;
                        for (int j = 0; j <= num2; j++)
                        {
                            bool flag2 = BarIndices[j] == ChBarIndex;
                            if (flag2)
                            {
                                Bars[j][Ch.Index.Y] = Ch;
                            }
                        }
                    }
                    else
                    {
                        Chunk[] ChBar = Ground.CStack.GetChunkBar(Ch.Index);
                        bool flag3 = ChBar == null;
                        if (flag3)
                        {
                            byte[] array = Loader.LoadFile(Loader.ChunkBarPathOf(ChBarIndex));
                            ChBar = Loader.CompileChunkBarByteCode(ref array, ChBarIndex);
                        }
                        Bars[nBars] = ChBar;
                        Bars[nBars][Ch.Index.Y] = Ch;
                        BarIndices[nBars] = ChBarIndex;
                        nBars++;
                    }
                }
                BarCount = nBars;
                return Bars;
            }
        }

        // Token: 0x06000153 RID: 339 RVA: 0x00013E58 File Offset: 0x00012058
        public static void SaveChunkBarCachedAdd(Chunk[] Chunks, bool DisposeChunks)
        {
            Loader.CachSaveChunksPaths[Loader.nCachSaveChunks] = Loader.ChunkBarPathOf(Chunks[0]);
            Loader.CachSaveChunksContents[Loader.nCachSaveChunks] = Loader.GenarateChunkBarByteCode(Chunks);
            checked
            {
                Loader.nCachSaveChunks++;
                bool flag = Loader.CachSaveChunksPaths.Length < Loader.nCachSaveChunks + 5;
                if (flag)
                {
                    Array.Resize<byte[]>(ref Loader.CachSaveChunksContents, Loader.CachSaveChunksPaths.Length + 100);
                    Array.Resize<string>(ref Loader.CachSaveChunksPaths, Loader.CachSaveChunksPaths.Length + 100);
                }
                if (DisposeChunks)
                {
                    int num = Ground.ChunkBarHeight - 1;
                    for (int x = 0; x <= num; x++)
                    {
                        Chunks[x].Dispose();
                    }
                }
                else
                {
                    int num2 = Ground.ChunkBarHeight - 1;
                    for (int x2 = 0; x2 <= num2; x2++)
                    {
                        bool removed = Chunks[x2].Removed;
                        if (removed)
                        {
                            Chunks[x2].Dispose();
                        }
                    }
                }
            }
        }

        // Token: 0x06000154 RID: 340 RVA: 0x00013F34 File Offset: 0x00012134
        public static void CheckAndSaveCachedChunkBars()
        {
            bool flag = Loader.nCachSaveChunks >= Loader.MaxnCachSaveChunks;
            if (flag)
            {
                Loader.SaveCashedChunkBars();
            }
        }

        // Token: 0x06000155 RID: 341 RVA: 0x00013F60 File Offset: 0x00012160
        public static void SaveCashedChunkBars()
        {
            Loader.SaveCashedChunksCompleted = false;
            Loader.CurrThrCachedChunk = 0;
            System.Timers.Timer TmrRep = new System.Timers.Timer(1000.0);
            TmrRep.Elapsed += (object a0, ElapsedEventArgs a1) => { Loader.SaveCashedChunksReport(); };


            TmrRep.Start();
            int num = Loader.nThrCachedChunkSavers;
            checked
            {
                for (int nThr = 0; nThr <= num; nThr++)
                {
                    Loader.ThrCachedChunkSaversIsCompleted[nThr] = true;
                }
                int LoopMax = Loader.nCachSaveChunks - 2 * Loader.FilesPerThreadSession - 1;
                for (; ; )
                {
                    bool flag = Loader.CurrThrCachedChunk >= LoopMax;
                    if (flag)
                    {
                        break;
                    }
                    int num2 = Loader.nThrCachedChunkSavers;
                    for (int nThr2 = 0; nThr2 <= num2; nThr2++)
                    {
                        bool flag2 = Loader.ThrCachedChunkSaversIsCompleted[nThr2];
                        if (flag2)
                        {
                            Loader.ThrCachedChunkSavers[nThr2] = new Thread(new ParameterizedThreadStart(Loader.ThrCachedChunkSave));
                            Loader.ThrCachedChunkSavers[nThr2].Start(new int[]
                            {
                                Loader.CurrThrCachedChunk,
                                nThr2
                            });
                            Loader.CurrThrCachedChunk += Loader.FilesPerThreadSession;
                            break;
                        }
                    }
                }
                int currThrCachedChunk = Loader.CurrThrCachedChunk;
                int num3 = Loader.nCachSaveChunks - 1;
                for (int i = currThrCachedChunk; i <= num3; i++)
                {
                    File.WriteAllBytes(Loader.CachSaveChunksPaths[i], Loader.CachSaveChunksContents[i]);
                }
                int num4 = Loader.nThrCachedChunkSavers;
                for (int nThr3 = 0; nThr3 <= num4; nThr3++)
                {
                    bool flag3 = !Loader.ThrCachedChunkSaversIsCompleted[nThr3];
                    if (flag3)
                    {
                        for (; ; )
                        {
                            bool flag4 = Loader.ThrCachedChunkSaversIsCompleted[nThr3];
                            if (flag4)
                            {
                                break;
                            }
                            Main.Log(". ", false);
                            Thread.Sleep(1000);
                        }
                    }
                }
                TmrRep.Stop();
                TmrRep.Dispose();
                Loader.nCachSaveChunks = 0;
                Loader.SaveCashedChunksCompleted = true;
            }
        }

        // Token: 0x06000156 RID: 342 RVA: 0x0001413C File Offset: 0x0001233C
        private static void ThrCachedChunkSave(object o)
        {
            int[] n = (int[])o;
            Loader.ThrCachedChunkSaversIsCompleted[n[1]] = false;
            int num = n[0];
            checked
            {
                int num2 = n[0] + Loader.FilesPerThreadSession - 1;
                for (int i = num; i <= num2; i++)
                {
                    File.WriteAllBytes(Loader.CachSaveChunksPaths[i], Loader.CachSaveChunksContents[i]);
                }
                Loader.ThrCachedChunkSaversIsCompleted[n[1]] = true;
            }
        }

        // Token: 0x06000157 RID: 343 RVA: 0x00014199 File Offset: 0x00012399
        public static void SaveCashedChunksReport()
        {
            Main.Log(Loader.CurrThrCachedChunk.ToString() + " of " + Loader.nCachSaveChunks.ToString(), true);
        }

        // Token: 0x06000158 RID: 344 RVA: 0x000141C4 File Offset: 0x000123C4
        public static byte[] LoadFile(string Path)
        {
            Loader.BytesLoadFile = File.ReadAllBytes(Path);
            return Loader.BytesLoadFile;
        }

        // Token: 0x06000159 RID: 345 RVA: 0x000141F0 File Offset: 0x000123F0
        public static byte[] GenarateChunkByteCode(Chunk Ch)
        {
            bool isAir = Ch.IsAir;
            checked
            {
                byte[] GenarateChunkByteCode;
                if (isAir)
                {
                    GenarateChunkByteCode = Loader.AirChunkByteCode;
                }
                else
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
                                O[i] = Ch.BlockList[X][Y][Z].Varient;
                                O[i + 1] = Ch.BlockList[X][Y][Z].BID;
                                bool isAir2 = Ch.BlockList[X][Y][Z].IsAir;
                                if (isAir2)
                                {
                                    bool surfaceRelation = Ch.BlockList[X][Y][Z].SurfaceRelation;
                                    if (surfaceRelation)
                                    {
                                        O[i + 2] = 3;
                                    }
                                    else
                                    {
                                        O[i + 2] = 2;
                                    }
                                }
                                else
                                {
                                    bool surfaceRelation2 = Ch.BlockList[X][Y][Z].SurfaceRelation;
                                    if (surfaceRelation2)
                                    {
                                        O[i + 2] = 1;
                                    }
                                    else
                                    {
                                        O[i + 2] = 0;
                                    }
                                }
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
                    O[Loader.ChunkByteCodeLength - 1] = (byte)((Ch.IsInTheSurface) ? 1 : 0);
                    GenarateChunkByteCode = O;
                }
                return GenarateChunkByteCode;
            }
        }

        // Token: 0x0600015A RID: 346 RVA: 0x00014318 File Offset: 0x00012518
        private static Chunk CompileChunkByteCode(ref byte[] O, IntVector3 ChunkIndex)
        {
            Chunk Ch = new Chunk();
            Ch.BlockList = new Block[8][][];
            byte X = 0;
            do
            {
                Ch.BlockList[(int)X] = new Block[8][];
                byte Y = 0;
                do
                {
                    Ch.BlockList[(int)X][(int)Y] = new Block[8];
                    Y += 1;
                }
                while (Y <= 7);
                X += 1;
            }
            while (X <= 7);
            bool IsNotAir = true;
            int i = 0;
            X = 0;
            do
            {
                byte Y = 0;
                do
                {
                    byte Z = 0;
                    do
                    {
                        checked
                        {
                            Block B = new Block
                            {
                                BID = O[i + 1],
                                Varient = O[i],
                                CPosition = new IntVector3((int)X, (int)Y, (int)Z),
                                IsAir = (O[i + 2] / 2 > 0),
                                SurfaceRelation = (O[i + 2] % 2 > 0)
                            };
                            i += 3;
                            IsNotAir = (IsNotAir || !B.IsAir);
                            Ch.BlockList[(int)X][(int)Y][(int)Z] = B;
                            bool surfaceRelation = B.SurfaceRelation;
                            if (surfaceRelation)
                            {
                                byte[] BBlock = new byte[]
                                {
                                    X,
                                    Y,
                                    Z,
                                    B.BID,
                                    B.Varient
                                };
                                Ch.BIDList[(int)Ch.BIDFilledI] = BBlock;
                                Chunk chunk = Ch;
                                unchecked
                                {
                                    chunk.BIDFilledI += 1;
                                }
                                bool flag = Ch.BIDList.Length < (int)(Ch.BIDFilledI + 5);
                                if (flag)
                                {
                                    Array.Resize<byte[]>(ref Ch.BIDList, Ch.BIDList.Length + 40);
                                }
                                Ch.SurfaceBlocks[Ch.FilledSB] = B;
                                Ch.FilledSB++;
                            }
                        }
                        Z += 1;
                    }
                    while (Z <= 7);
                    Y += 1;
                }
                while (Y <= 7);
                X += 1;
            }
            while (X <= 7);
            Ch.IsInTheSurface = (O[checked(Loader.ChunkByteCodeLength - 1)] > 0);
            Ch.Index = ChunkIndex;
            Ch.Position = ChunkIndex * Chunk.Volume;
            Ch.IsAir = !IsNotAir;
            Ch.GenarateAirGrid();
            Ch.GenarateBlockTranslations();
            Ch.GenerateBIDForBTranslations();
            return Ch;
        }

        // Token: 0x0600015B RID: 347 RVA: 0x0001452C File Offset: 0x0001272C
        public static Chunk[] CompileChunkBarByteCode(ref byte[] O, IntVector3 ChunkBarIndex)
        {
            checked
            {
                Chunk[] Chs = new Chunk[Ground.ChunkBarHeight - 1 + 1];
                byte[] ChBC = new byte[Loader.ChunkByteCodeLength + 1];
                IntVector3 ChunkIndex = ChunkBarIndex;
                int num = Ground.ChunkBarHeight - 1;
                for (int y = 0; y <= num; y++)
                {
                    Array.Copy(O, y * Loader.ChunkByteCodeLength, ChBC, 0, Loader.ChunkByteCodeLength);
                    ChunkIndex.Y = y;
                    Chs[y] = Loader.CompileChunkByteCode(ref ChBC, ChunkIndex);
                }
                return Chs;
            }
        }

        // Token: 0x0600015C RID: 348 RVA: 0x000145A4 File Offset: 0x000127A4
        public static void LoadAndReplaceChunksRegion(IntVector3 Min, IntVector3 Max)
        {
            Array.Clear(Loader.ChunkIndices, 0, Loader.ChunkIndices.Length);
            checked
            {
                int nChunkIndices = (Max.X - Min.X) * (Max.Z - Min.Z);
                bool flag = nChunkIndices > Loader.ChunkIndices.Length - 10;
                if (flag)
                {
                    Loader.ChunkIndices = new IntVector3[nChunkIndices + 200 + 1];
                }
                int i = 0;
                int x = Min.X;
                int num = Max.X - 1;
                for (int X = x; X <= num; X++)
                {
                    int z = Min.Z;
                    int num2 = Max.Z - 1;
                    for (int Z = z; Z <= num2; Z++)
                    {
                        Loader.ChunkIndices[i] = new IntVector3(X, 0, Z);
                        i++;
                    }
                }
                int num3 = i - 1;
                for (int j = 0; j <= num3; j++)
                {
                    IntVector3 S = Loader.ChunkIndices[j];
                    byte[] array = Loader.LoadFile(Loader.SavedPathOf(S, Loader.ExtChunk));
                    Chunk[] Chs = Loader.CompileChunkBarByteCode(ref array, S);
                    int Y = 0;
                    IntVector3 SIndex = S;
                    foreach (Chunk Ch in Chs)
                    {
                        SIndex.Y = Y;
                        Ground.CStack.SetChunk(SIndex, Ch);
                        bool isInTheSurface = Ch.IsInTheSurface;
                        if (isInTheSurface)
                        {
                            Ch.SurfaceChunkIndex = Ground.FilledSFC;
                            Ground.SurfaceChunks[Ground.FilledSFC] = Ch;
                            Ground.FilledSFC++;
                        }
                        Y++;
                    }
                }
            }
        }

        // Token: 0x0600015D RID: 349 RVA: 0x0001472C File Offset: 0x0001292C
        public static void LoadAndReplaceChunksRegions(IntVector3 Min1, IntVector3 Max1, IntVector3 Min2, IntVector3 Max2)
        {
            Array.Clear(Loader.ChunkIndices, 0, Loader.ChunkIndices.Length);
            checked
            {
                int nChunkIndices = (Max1.X - Min1.X) * (Max1.Z - Min1.Z) + (Max2.X - Min2.X) * (Max2.Z - Min2.Z);
                bool flag = nChunkIndices > Loader.ChunkIndices.Length - 10;
                if (flag)
                {
                    Loader.ChunkIndices = new IntVector3[nChunkIndices + 200 + 1];
                }
                int i = 0;
                int x = Min1.X;
                int num = Max1.X - 1;
                for (int X = x; X <= num; X++)
                {
                    int z = Min1.Z;
                    int num2 = Max1.Z - 1;
                    for (int Z = z; Z <= num2; Z++)
                    {
                        Loader.ChunkIndices[i] = new IntVector3(X, 0, Z);
                        i++;
                    }
                }
                int x2 = Min2.X;
                int num3 = Max2.X - 1;
                for (int X2 = x2; X2 <= num3; X2++)
                {
                    int z2 = Min2.Z;
                    int num4 = Max2.Z - 1;
                    for (int Z2 = z2; Z2 <= num4; Z2++)
                    {
                        Loader.ChunkIndices[i] = new IntVector3(X2, 0, Z2);
                        i++;
                    }
                }
                int num5 = i - 1;
                for (int j = 0; j <= num5; j++)
                {
                    IntVector3 S = Loader.ChunkIndices[j];
                    byte[] array = Loader.LoadFile(Loader.SavedPathOf(S, Loader.ExtChunk));
                    Chunk[] Chs = Loader.CompileChunkBarByteCode(ref array, S);
                    int Y = 0;
                    IntVector3 SIndex = S;
                    foreach (Chunk Ch in Chs)
                    {
                        SIndex.Y = Y;
                        Ground.CStack.SetChunk(SIndex, Ch);
                        bool isInTheSurface = Ch.IsInTheSurface;
                        if (isInTheSurface)
                        {
                            Ch.SurfaceChunkIndex = Ground.FilledSFC;
                            Ground.SurfaceChunks[Ground.FilledSFC] = Ch;
                            Ground.FilledSFC++;
                        }
                        Y++;
                    }
                }
            }
        }

        // Token: 0x0600015E RID: 350 RVA: 0x00014934 File Offset: 0x00012B34
        public static void LoadAndReplaceChunk(IntVector3 Index)
        {
            byte[] array = Loader.LoadFile(Loader.SavedPathOf(Index, Loader.ExtChunk));
            Chunk[] Chs = Loader.CompileChunkBarByteCode(ref array, Index);
            int Y = 0;
            IntVector3 SIndex = Index;
            checked
            {
                foreach (Chunk Ch in Chs)
                {
                    SIndex.Y = Y;
                    Ground.CStack.SetChunk(SIndex, Ch);
                    bool isInTheSurface = Ch.IsInTheSurface;
                    if (isInTheSurface)
                    {
                        Ch.SurfaceChunkIndex = Ground.FilledSFC;
                        Ground.SurfaceChunks[Ground.FilledSFC] = Ch;
                        Ground.FilledSFC++;
                    }
                    Y++;
                }
            }
        }

        // Token: 0x0600015F RID: 351 RVA: 0x000149D4 File Offset: 0x00012BD4
        public static void LoadAndReplaceChunkTempory(IntVector3 Index)
        {
            byte[] array = Loader.LoadFile(Loader.SavedPathOf(Index, Loader.ExtChunk));
            Chunk[] Chs = Loader.CompileChunkBarByteCode(ref array, Index);
            int Y = 0;
            IntVector3 SIndex = Index;
            checked
            {
                foreach (Chunk Ch in Chs)
                {
                    SIndex.Y = Y;
                    Ground.CStack.SetChunk(SIndex, Ch);
                    bool isInTheSurface = Ch.IsInTheSurface;
                    if (isInTheSurface)
                    {
                        bool flag = !Funcs.Contains(Ground.TmpSurfaceChunks, Ground.TmpFilledSFC, Ch);
                        if (flag)
                        {
                            Ch.SurfaceChunkIndex = Ground.TmpFilledSFC;
                            Ground.TmpSurfaceChunks[Ground.TmpFilledSFC] = Ch;
                            Ground.TmpFilledSFC++;
                        }
                    }
                    Y++;
                }
            }
        }

        // Token: 0x06000160 RID: 352 RVA: 0x00014A94 File Offset: 0x00012C94
        public static IntVector3 SavedNameToIndex(string S)
        {
            string[] A = S.Split(new char[]
            {
                ','
            });
            IntVector3 SavedNameToIndex = new IntVector3(Conversions.ToInteger(A[0]), 0, Conversions.ToInteger(A[1]));
            return SavedNameToIndex;
        }

        /// <summary>
        /// Can be used to any saved file type
        /// </summary>
        // Token: 0x06000161 RID: 353 RVA: 0x00014AD0 File Offset: 0x00012CD0
        public static IntVector3[] SavedNamesToIndices(string[] Names)
        {
            checked
            {
                IntVector3[] O = new IntVector3[Names.Length - 1 + 1];
                int i = 0;
                foreach (string S in Names)
                {
                    string[] A = S.Split(new char[]
                    {
                        ','
                    });
                    O[i] = new IntVector3(Conversions.ToInteger(A[0]), 0, Conversions.ToInteger(A[1]));
                    i++;
                }
                return O;
            }
        }

        // Token: 0x06000162 RID: 354 RVA: 0x00014B4C File Offset: 0x00012D4C
        public static string ChunkBarPathOf(Chunk Ch)
        {
            return string.Concat(new string[]
            {
                Loader.MapRoot,
                Ch.Index.X.ToString(),
                ",",
                Ch.Index.Z.ToString(),
                ".C"
            });
        }

        // Token: 0x06000163 RID: 355 RVA: 0x00014BA8 File Offset: 0x00012DA8
        public static string ChunkBarPathOf(IntVector3 Index)
        {
            return string.Concat(new string[]
            {
                Loader.MapRoot,
                Index.X.ToString(),
                ",",
                Index.Z.ToString(),
                ".C"
            });
        }

        // Token: 0x06000164 RID: 356 RVA: 0x00014BFC File Offset: 0x00012DFC
        public static string ChunkBarNameOf(Chunk Ch)
        {
            return Ch.Index.X.ToString() + "," + Ch.Index.Z.ToString();
        }

        // Token: 0x06000165 RID: 357 RVA: 0x00014C38 File Offset: 0x00012E38
        public static string ChunkBarNameOf(IntVector3 Index)
        {
            return Index.X.ToString() + "," + Index.Z.ToString();
        }

        /// <summary>
        /// Can be used to any saved file type
        /// </summary>
        // Token: 0x06000166 RID: 358 RVA: 0x00014C6C File Offset: 0x00012E6C
        public static string SavedPathOf(IntVector3 Pos, string Ext)
        {
            return string.Concat(new string[]
            {
                Loader.MapRoot,
                Pos.X.ToString(),
                ",",
                Pos.Z.ToString(),
                Ext
            });
        }

        /// <summary>
        /// Can be used to any saved file type
        /// </summary>
        // Token: 0x06000167 RID: 359 RVA: 0x00014CBC File Offset: 0x00012EBC
        public static string SavedNameOf(IntVector3 Index)
        {
            return Index.X.ToString() + "," + Index.Z.ToString();
        }

        // Token: 0x06000168 RID: 360 RVA: 0x00014CF0 File Offset: 0x00012EF0
        public static HeightMap GenareteHMap(Biome Bm, IntVector3 Index)
        {
            HeightMap HM = new HeightMap();
            Funcs.RND = new Random(100);
            int XLength = HeightMap.Size;
            int YLength = HeightMap.Size;
            int FlagDistance = Bm.FlagDistance;
            int[] RowPattern = Bm.RowPattern;
            int FlagLerpAmont = Bm.FlagLerpAmont;
            int FlagPowerMin = Bm.FlagPowerMin;
            int FlagPowerMax = Bm.FlagPowerMax;
            int MaxHeight = Bm.MaxHeight;
            checked
            {
                byte[][] A = new byte[XLength - 1 + 1][];
                int num = YLength - 1;
                for (int x = 0; x <= num; x++)
                {
                    A[x] = new byte[YLength - 1 + 1];
                }
                int nFlagsX = (int)((double)XLength / (double)FlagDistance);
                int nFlagsY = (int)((double)YLength / (double)FlagDistance);
                byte[][] Flags = new byte[nFlagsX + 1][];
                int num2 = nFlagsX;
                for (int x2 = 0; x2 <= num2; x2++)
                {
                    Flags[x2] = new byte[nFlagsY + 1];
                }
                Main.Log("Creating flags", true);
                int num3 = nFlagsX;
                for (int X = 0; X <= num3; X++)
                {
                    int num4 = nFlagsY;
                    for (int Y = 0; Y <= num4; Y++)
                    {
                        Flags[X][Y] = Convert.ToByte(Math.Round(new decimal(Math.Min(Math.Max(RowPattern[Funcs.RND.Next(0, RowPattern.Length)], 0), MaxHeight))));
                    }
                }
                int nFLAF = FlagLerpAmont * 2 * (FlagLerpAmont * 2);
                Main.Log("Lerping flags", true);
                bool flag = FlagLerpAmont > 0;
                if (flag)
                {
                    int num5 = FlagLerpAmont;
                    int num6 = nFlagsX - 1 - FlagLerpAmont;
                    for (int X2 = num5; X2 <= num6; X2++)
                    {
                        int num7 = FlagLerpAmont;
                        int num8 = nFlagsY - 1 - FlagLerpAmont;
                        for (int Y2 = num7; Y2 <= num8; Y2++)
                        {
                            int Total = 0;
                            int num9 = 0 - FlagLerpAmont;
                            int num10 = FlagLerpAmont;
                            for (int FLAX = num9; FLAX <= num10; FLAX++)
                            {
                                int num11 = 0 - FlagLerpAmont;
                                int num12 = FlagLerpAmont;
                                for (int FLAY = num11; FLAY <= num12; FLAY++)
                                {
                                    Total += (int)Flags[X2 + FLAX][Y2 + FLAY];
                                }
                            }
                            double Average = (double)Total / (double)nFLAF;
                            byte F = Flags[X2][Y2];
                            int RndFlagPower = Funcs.RND.Next(FlagPowerMin, FlagPowerMax + 1);
                            Flags[X2][Y2] = (byte)Math.Round(Math.Min(Math.Max(Math.Round(unchecked((double)(checked((int)F * RndFlagPower)) + Average) / (double)(RndFlagPower + 1)), 0.0), (double)MaxHeight));
                        }
                    }
                    int BnFLAF = FlagLerpAmont * FlagLerpAmont;
                    int num13 = FlagLerpAmont;
                    for (int X3 = 0; X3 <= num13; X3++)
                    {
                        int num14 = nFlagsY - 1 - FlagLerpAmont;
                        for (int Y3 = 0; Y3 <= num14; Y3++)
                        {
                            int Total2 = 0;
                            int num15 = FlagLerpAmont;
                            for (int FLAX2 = 0; FLAX2 <= num15; FLAX2++)
                            {
                                int num16 = FlagLerpAmont;
                                for (int FLAY2 = 0; FLAY2 <= num16; FLAY2++)
                                {
                                    Total2 += (int)Flags[X3 + FLAX2][Y3 + FLAY2];
                                }
                            }
                            double Average2 = (double)Total2 / (double)BnFLAF;
                            byte F2 = Flags[X3][Y3];
                            int RndFlagPower2 = Funcs.RND.Next(FlagPowerMin, FlagPowerMax + 1);
                            Flags[X3][Y3] = (byte)Math.Round(Math.Min(Math.Max(Math.Round(unchecked((double)(checked((int)F2 * RndFlagPower2)) + Average2) / (double)(RndFlagPower2 + 1)), 0.0), (double)MaxHeight));
                        }
                    }
                    int num17 = nFlagsX - 1 - FlagLerpAmont;
                    int num18 = nFlagsX - 1;
                    for (int X4 = num17; X4 <= num18; X4++)
                    {
                        int num19 = nFlagsY - 1 - FlagLerpAmont;
                        for (int Y4 = 0; Y4 <= num19; Y4++)
                        {
                            int Total3 = 0;
                            int num20 = FlagLerpAmont;
                            for (int FLAX3 = num20; FLAX3 >= 0; FLAX3 += -1)
                            {
                                int num21 = FlagLerpAmont;
                                for (int FLAY3 = num21; FLAY3 >= 0; FLAY3 += -1)
                                {
                                    Total3 += (int)Flags[Math.Min(X4 + FLAX3, nFlagsX - 1)][Math.Min(Y4 + FLAY3, nFlagsY - 1)];
                                }
                            }
                            double Average3 = (double)Total3 / (double)BnFLAF;
                            byte F3 = Flags[X4][Y4];
                            int RndFlagPower3 = Funcs.RND.Next(FlagPowerMin, FlagPowerMax + 1);
                            Flags[X4][Y4] = (byte)Math.Round(Math.Min(Math.Max(Math.Round(unchecked((double)(checked((int)F3 * RndFlagPower3)) + Average3) / (double)(RndFlagPower3 + 1)), 0.0), (double)MaxHeight));
                        }
                    }
                    int num22 = nFlagsX - 1 - FlagLerpAmont;
                    for (int X5 = 0; X5 <= num22; X5++)
                    {
                        int num23 = FlagLerpAmont;
                        for (int Y5 = 0; Y5 <= num23; Y5++)
                        {
                            int Total4 = 0;
                            int num24 = FlagLerpAmont;
                            for (int FLAX4 = 0; FLAX4 <= num24; FLAX4++)
                            {
                                int num25 = FlagLerpAmont;
                                for (int FLAY4 = 0; FLAY4 <= num25; FLAY4++)
                                {
                                    Total4 += (int)Flags[X5 + FLAX4][Y5 + FLAY4];
                                }
                            }
                            double Average4 = (double)Total4 / (double)BnFLAF;
                            byte F4 = Flags[X5][Y5];
                            int RndFlagPower4 = Funcs.RND.Next(FlagPowerMin, FlagPowerMax + 1);
                            Flags[X5][Y5] = (byte)Math.Round(Math.Min(Math.Max(Math.Round(unchecked((double)(checked((int)F4 * RndFlagPower4)) + Average4) / (double)(RndFlagPower4 + 1)), 0.0), (double)MaxHeight));
                        }
                    }
                    int num26 = nFlagsX - 1 - FlagLerpAmont;
                    for (int X6 = 0; X6 <= num26; X6++)
                    {
                        int num27 = nFlagsY - 1 - FlagLerpAmont;
                        int num28 = nFlagsY - 1;
                        for (int Y6 = num27; Y6 <= num28; Y6++)
                        {
                            int Total5 = 0;
                            int num29 = FlagLerpAmont;
                            for (int FLAX5 = num29; FLAX5 >= 0; FLAX5 += -1)
                            {
                                int num30 = FlagLerpAmont;
                                for (int FLAY5 = num30; FLAY5 >= 0; FLAY5 += -1)
                                {
                                    Total5 += (int)Flags[Math.Min(X6 + FLAX5, nFlagsX - 1)][Math.Min(Y6 + FLAY5, nFlagsY - 1)];
                                }
                            }
                            double Average5 = (double)Total5 / (double)BnFLAF;
                            byte F5 = Flags[X6][Y6];
                            int RndFlagPower5 = Funcs.RND.Next(FlagPowerMin, FlagPowerMax + 1);
                            Flags[X6][Y6] = (byte)Math.Round(Math.Min(Math.Max(Math.Round(unchecked((double)(checked((int)F5 * RndFlagPower5)) + Average5) / (double)(RndFlagPower5 + 1)), 0.0), (double)MaxHeight));
                        }
                    }
                }
                Main.Log("Creating Block heights", true);
                int num31 = nFlagsX;
                for (int X7 = 1; X7 <= num31; X7++)
                {
                    int num32 = nFlagsX;
                    for (int Y7 = 1; Y7 <= num32; Y7++)
                    {
                        byte ThisFlag = Flags[X7][Y7];
                        byte PXFlag = Flags[X7 - 1][Y7];
                        byte PYFlag = Flags[X7][Y7 - 1];
                        byte PXPYFlag = Flags[X7 - 1][Y7 - 1];
                        int[] DifPX = Funcs.ListDif((int)PXFlag, (int)ThisFlag, FlagDistance);
                        int[] DifPY = Funcs.ListDif((int)PYFlag, (int)ThisFlag, FlagDistance);
                        int[] DifParrelelPX = Funcs.ListDif((int)PXPYFlag, (int)PYFlag, FlagDistance);
                        int[] DifParrelelPY = Funcs.ListDif((int)PXPYFlag, (int)PXFlag, FlagDistance);
                        int PosX = X7 * FlagDistance;
                        int PosY = Y7 * FlagDistance;
                        int PosPX = (X7 - 1) * FlagDistance;
                        int PosPY = (Y7 - 1) * FlagDistance;
                        int[][] DDifX = new int[FlagDistance - 1 + 1][];
                        int num33 = FlagDistance - 1;
                        for (int i = 0; i <= num33; i++)
                        {
                            DDifX[i] = Funcs.ListDif(DifParrelelPY[i], DifPY[i], FlagDistance);
                        }
                        int j = 0;
                        int num34 = PosPX;
                        int num35 = PosX - 1;
                        for (int BX = num34; BX <= num35; BX++)
                        {
                            int k = 0;
                            int[] DDifY = Funcs.ListDif(DifParrelelPX[j], DifPX[j], FlagDistance);
                            int num36 = PosPY;
                            int num37 = PosY - 1;
                            for (int BY = num36; BY <= num37; BY++)
                            {
                                A[BX][BY] = (byte)Math.Round((double)(DDifY[k] + DDifX[k][j]) / 2.0);
                                k++;
                            }
                            j++;
                        }
                    }
                }
                Main.Log("Generating Height maps succeed", true);
                HM.B = A;
                HM.Bm = Bm;
                HM.Index = Index;
                return HM;
            }
        }

        // Token: 0x06000169 RID: 361 RVA: 0x00015494 File Offset: 0x00013694
        public static void SaveHeightMap(HeightMap HM)
        {
            bool Exits = false;

            foreach (IntVector3 I in Loader.SavedHMaps)
            {
                bool flag = I == HM.Index;
                if (flag)
                {
                    Exits = true;
                }
            }
            bool flag2 = !Exits;
            if (flag2)
            {
                Loader.SavedHMaps.Add(HM.Index);
                File.WriteAllText(Loader.SavedHMapFile, Loader.SavedNameOf(HM.Index) + ";", Encoding.ASCII);
            }
            File.WriteAllBytes(Loader.SavedPathOf(HM.Index, Loader.ExtHeightMap), Loader.GenarateHMByteCode(HM));
            Main.Log("Height map saved", true);
        }

        // Token: 0x0600016A RID: 362 RVA: 0x0001557C File Offset: 0x0001377C
        public static byte[] GenarateHMByteCode(HeightMap HM)
        {
            checked
            {
                byte[] B = new byte[(int)HeightMap.ByteCodeLength + 1];
                int num = HeightMap.Size - 1;
                for (int X = 0; X <= num; X++)
                {
                    int num2 = HeightMap.Size - 1;
                    for (int Y = 0; Y <= num2; Y++)
                    {
                        B[X * HeightMap.Size + Y] = HM.B[X][Y];
                    }
                }
                int SizePow = HeightMap.Size * HeightMap.Size;
                B[SizePow + 1] = (byte)HM.Index.X;
                B[SizePow + 2] = (byte)HM.Index.Y;
                B[SizePow + 3] = (byte)HM.Index.Z;
                B[SizePow + 4] = (byte)HM.Bm.Index;
                return B;
            }
        }

        // Token: 0x0600016B RID: 363 RVA: 0x00015638 File Offset: 0x00013838
        public static HeightMap LoadHeightMap(IntVector3 Index)
        {
            bool Exits = false;

            foreach (IntVector3 I in Loader.SavedHMaps)
            {
                bool flag = I == Index;
                if (flag)
                {
                    Exits = true;
                }
            }
            bool flag2 = Exits;
            HeightMap LoadHeightMap;
            if (flag2)
            {
                HeightMap HM = Loader.CompileHMByteCode(Loader.LoadFile(Loader.SavedPathOf(Index, Loader.ExtHeightMap)));
                Loader.LoadedHMaps.Add(HM);
                Main.Log("Height map loaded", true);
                LoadHeightMap = HM;
            }
            else
            {
                LoadHeightMap = null;
            }
            return LoadHeightMap;
        }

        // Token: 0x0600016C RID: 364 RVA: 0x000156E0 File Offset: 0x000138E0
        public static HeightMap CompileHMByteCode(byte[] B)
        {
            checked
            {
                HeightMap HM = new HeightMap
                {
                    B = new byte[HeightMap.Size - 1 + 1][]
                };
                int num = HeightMap.Size - 1;
                for (int X = 0; X <= num; X++)
                {
                    HM.B[X] = new byte[HeightMap.Size - 1 + 1];
                    int num2 = HeightMap.Size - 1;
                    for (int Y = 0; Y <= num2; Y++)
                    {
                        HM.B[X][Y] = B[X * HeightMap.Size + Y];
                    }
                }
                int SizePow = HeightMap.Size * HeightMap.Size;
                HM.Index = new IntVector3
                {
                    X = (int)B[SizePow + 1],
                    Y = (int)B[SizePow + 2],
                    Z = (int)B[SizePow + 3]
                };
                HM.Bm = BiomeList.Lst[(int)B[SizePow + 4]];
                Loader.LoadedHMaps.Add(HM);
                return HM;
            }
        }

        // Token: 0x04000174 RID: 372
        public static string MapName;

        // Token: 0x04000175 RID: 373
        public static string MapDir;

        // Token: 0x04000176 RID: 374
        public static string MapRoot;

        // Token: 0x04000177 RID: 375
        public static string FileEntity;

        // Token: 0x04000178 RID: 376
        public static string MapInfoFile;

        // Token: 0x04000179 RID: 377
        public static Loader.MapInfo MInfo;

        // Token: 0x0400017A RID: 378
        public static IntVector3[] LoadedChunks;

        // Token: 0x0400017B RID: 379
        public static int nLoadedChunks = 0;

        // Token: 0x0400017C RID: 380
        public static string SavedHMapFile;

        // Token: 0x0400017D RID: 381
        public static List<IntVector3> SavedHMaps = new List<IntVector3>();

        // Token: 0x0400017E RID: 382
        public static List<HeightMap> LoadedHMaps = new List<HeightMap>();

        // Token: 0x0400017F RID: 383
        public static IntVector3 MaxWorldBorders;

        // Token: 0x04000180 RID: 384
        public static string ExtChunk = ".C";

        // Token: 0x04000181 RID: 385
        public static string ExtHeightMap = ".HM";

        // Token: 0x04000182 RID: 386
        public static StreamWriter SW;

        // Token: 0x04000183 RID: 387
        public static StreamReader SR;

        // Token: 0x04000184 RID: 388
        public static XmlSerializer XS;

        // Token: 0x04000185 RID: 389
        public static int ChunkByteCodeLength = 1538;

        // Token: 0x04000186 RID: 390
        private static int nCachSaveChunks = 0;

        // Token: 0x04000187 RID: 391
        private static byte[][] CachSaveChunksContents = new byte[501][];

        // Token: 0x04000188 RID: 392
        private static string[] CachSaveChunksPaths = new string[501];

        // Token: 0x04000189 RID: 393
        public static int MaxnCachSaveChunks = 3000;

        // Token: 0x0400018A RID: 394
        private static readonly int FilesPerThreadSession = 30;

        // Token: 0x0400018B RID: 395
        private static int CurrThrCachedChunk = 0;

        // Token: 0x0400018C RID: 396
        private static readonly int nThrCachedChunkSavers = 16;

        // Token: 0x0400018D RID: 397
        private static readonly Thread[] ThrCachedChunkSavers;

        // Token: 0x0400018E RID: 398
        private static readonly bool[] ThrCachedChunkSaversIsCompleted;

        // Token: 0x0400018F RID: 399
        private static bool SaveCashedChunksCompleted;

        // Token: 0x04000190 RID: 400
        private static byte[] BytesLoadFile;

        // Token: 0x04000191 RID: 401
        public static byte[] AirChunkByteCode;

        // Token: 0x04000192 RID: 402
        private static IntVector3[] ChunkIndices;

        // Token: 0x02000040 RID: 64
        [Serializable]
        public class MapInfo
        {
            // Token: 0x06000238 RID: 568 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
            public void Save(string Path)
            {
                this.EntityLICode = (int)Entity.LICode;
                Loader.XS = new XmlSerializer(typeof(Loader.MapInfo));
                Loader.SW = new StreamWriter(Path);
                Loader.XS.Serialize(Loader.SW, this);
                Loader.SW.Close();
                File.Copy(Path, Path + "Backup", true);
            }

            // Token: 0x06000239 RID: 569 RVA: 0x0001DB44 File Offset: 0x0001BD44
            public static Loader.MapInfo Load(string Path)
            {
                Loader.MapInfo Out;
                try
                {

                    Loader.XS = new XmlSerializer(typeof(Loader.MapInfo));
                    Loader.SR = new StreamReader(Path);
                    Out = (Loader.MapInfo)Loader.XS.Deserialize(Loader.SR);


                    Entity.LICode = checked((short)Out.EntityLICode);
                }
                catch (Exception)
                {
                    Out = null;
                }

                try
                {
                    Loader.SR.Close();
                }
                catch (Exception)
                {

                }

                try
                {
                    bool flag = Out == null;
                    if (flag)
                    {
                        bool flag2 = File.Exists(Path + "Backup");
                        if (flag2)
                        {
                            File.Copy(Path + "Backup", Path, true);
                            Out = Loader.MapInfo.Load(Path);
                        }
                    }
                }
                catch (Exception)
                {
                    Out = null;
                }
                return Out;
            }

            // Token: 0x04000289 RID: 649
            public string Name;

            // Token: 0x0400028A RID: 650
            public int HMapSize;

            // Token: 0x0400028B RID: 651
            public int WorldHeight;

            // Token: 0x0400028C RID: 652
            public Vector3 PlayerPosition;

            // Token: 0x0400028D RID: 653
            public int EntityLICode;
        }
    }
}
