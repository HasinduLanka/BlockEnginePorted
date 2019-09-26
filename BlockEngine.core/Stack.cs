using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
    // Token: 0x02000030 RID: 48
    public class Stack
    {
        // Token: 0x060001EB RID: 491 RVA: 0x00019054 File Offset: 0x00017254
        public Stack()
        {
            this.ChunkList = new Chunk[20][][];
            this.RootX = 0;
            this.RootZ = 0;
            this.MinX = 0;
            this.MinZ = 0;
            this.SavingChunks = new Chunk[101];
            this.nSavingChunks = 0;
            this.eList = new List<Entity>();
            this.Scrolling = false;
        }

        // Token: 0x060001EC RID: 492 RVA: 0x000190B8 File Offset: 0x000172B8
        public IntVector3 ChunkIndexToArrayIndex(IntVector3 Index)
        {
            IntVector3 O = Index - this.ChunkRangeMin;
            checked
            {
                O.X += this.RootX;
                O.Z += this.RootZ;
                bool flag = O.X >= Stack.Size.X;
                if (flag)
                {
                    O.X -= Stack.Size.X;
                }
                bool flag2 = O.Z >= Stack.Size.Z;
                if (flag2)
                {
                    O.Z -= Stack.Size.Z;
                }
                return O;
            }
        }

        // Token: 0x060001ED RID: 493 RVA: 0x00019164 File Offset: 0x00017364
        public int[] ChunkIndexToArrayIndex(int X, int Y, int Z)
        {
            checked
            {
                int OX = X - this.MinX;
                int OZ = Z - this.MinZ;
                OX += this.RootX;
                OZ += this.RootZ;
                bool flag = OX >= Stack.Size.X;
                if (flag)
                {
                    OX -= Stack.Size.X;
                }
                bool flag2 = OZ >= Stack.Size.Z;
                if (flag2)
                {
                    OZ -= Stack.Size.Z;
                }
                return new int[]
                {
                    OX,
                    Y,
                    OZ
                };
            }
        }

        // Token: 0x060001EE RID: 494 RVA: 0x000191F4 File Offset: 0x000173F4
        public Chunk GetChunk(IntVector3 Index)
        {
            Index = this.ChunkIndexToArrayIndex(Index);
            return this.ChunkList[Index.X][Index.Y][Index.Z];
        }

        // Token: 0x060001EF RID: 495 RVA: 0x0001922C File Offset: 0x0001742C
        public Chunk GetChunk(int X, int Y, int Z)
        {
            int[] I = this.ChunkIndexToArrayIndex(X, Y, Z);
            return this.ChunkList[I[0]][I[1]][I[2]];
        }

        // Token: 0x060001F0 RID: 496 RVA: 0x0001925C File Offset: 0x0001745C
        public Chunk[] GetChunkBar(IntVector3 Index)
        {
            Index = this.ChunkIndexToArrayIndex(Index);
            checked
            {
                Chunk[] ChunkBar = new Chunk[Ground.ChunkBarHeight - 1 + 1];
                int num = Ground.ChunkBarHeight - 1;
                for (int y = 0; y <= num; y++)
                {
                    ChunkBar[y] = this.ChunkList[Index.X][y][Index.Z];
                }
                return ChunkBar;
            }
        }

        // Token: 0x060001F1 RID: 497 RVA: 0x000192B4 File Offset: 0x000174B4
        public void SetChunk(IntVector3 Index, Chunk Ch)
        {
            Index = this.ChunkIndexToArrayIndex(Index);
            this.ChunkList[Index.X][Index.Y][Index.Z] = Ch;
        }

        // Token: 0x060001F2 RID: 498 RVA: 0x000192DC File Offset: 0x000174DC
        public void SetChunk(int X, int Y, int Z, Chunk Ch)
        {
            int[] I = this.ChunkIndexToArrayIndex(X, Y, Z);
            this.ChunkList[I[0]][I[1]][I[2]] = Ch;
        }

        // Token: 0x060001F3 RID: 499 RVA: 0x00019308 File Offset: 0x00017508
        public void CreateChunkRangeBounds(IntVector3 Middle)
        {
            this.ChunkRangeAddingVal = IntVector3.FromV3Truncated(new Vector3((float)((double)Stack.Size.X / 2.0), 0f, (float)((double)Stack.Size.Z / 2.0)));
            this.ChunkRangeMiddle = Middle;
            this.ChunkRangeMax = Middle + this.ChunkRangeAddingVal + new IntVector3(0, Stack.Size.Y, 0);
            this.ChunkRangeMin = Middle - this.ChunkRangeAddingVal;
            this.ChunkRangeMin.Y = 0;
            this.MinX = this.ChunkRangeMin.X;
            this.MinZ = this.ChunkRangeMin.Z;
        }

        // Token: 0x060001F4 RID: 500 RVA: 0x000193C8 File Offset: 0x000175C8
        public void NewChunkList()
        {
            checked
            {
                this.ChunkList = new Chunk[Stack.Size.X - 1 + 1][][];
                int num = Stack.Size.X - 1;
                for (int X = 0; X <= num; X++)
                {
                    this.ChunkList[X] = new Chunk[Stack.Size.Y - 1 + 1][];
                    int num2 = Stack.Size.Y - 1;
                    for (int Y = 0; Y <= num2; Y++)
                    {
                        this.ChunkList[X][Y] = new Chunk[Stack.Size.Z - 1 + 1];
                    }
                }
            }
        }

        // Token: 0x060001F5 RID: 501 RVA: 0x0001945C File Offset: 0x0001765C
        public void LoadChunks(IntVector3 MiddleIndex)
        {
            Main.Log("Loading Stack...", true);
            checked
            {
                int num = Ground.FilledSFC - 1;
                for (int i = 0; i <= num; i++)
                {
                    Ground.SurfaceChunks[i].SurfaceChunkIndex = -1;
                    Ground.SurfaceChunks[i] = null;
                }
                Ground.FilledSFC = 0;
                this.CreateChunkRangeBounds(MiddleIndex);
                bool OutOfRange = false;
                IntVector3 NewRangeMiddle = this.ChunkRangeMiddle;
                bool flag = this.ChunkRangeMin.X < 0;
                if (flag)
                {
                    OutOfRange = true;
                    NewRangeMiddle.X = this.ChunkRangeAddingVal.X;
                }
                else
                {
                    bool flag2 = this.ChunkRangeMax.X >= Loader.MaxWorldBorders.X;
                    if (flag2)
                    {
                        OutOfRange = true;
                        NewRangeMiddle.X = Loader.MaxWorldBorders.X - this.ChunkRangeAddingVal.X - 1;
                    }
                }
                bool flag3 = this.ChunkRangeMin.Z < 0;
                if (flag3)
                {
                    OutOfRange = true;
                    NewRangeMiddle.Z = this.ChunkRangeAddingVal.Z;
                }
                else
                {
                    bool flag4 = this.ChunkRangeMax.Z >= Loader.MaxWorldBorders.Z;
                    if (flag4)
                    {
                        OutOfRange = true;
                        NewRangeMiddle.Z = Loader.MaxWorldBorders.Z - this.ChunkRangeAddingVal.Z - 1;
                    }
                }
                bool flag5 = OutOfRange;
                if (flag5)
                {
                    this.CreateChunkRangeBounds(NewRangeMiddle);
                }
                Loader.LoadAndReplaceChunksRegion(this.ChunkRangeMin, this.ChunkRangeMax);
                Main.Log("Stack loaded succeed", true);
            }
        }

        // Token: 0x060001F6 RID: 502 RVA: 0x000195C0 File Offset: 0x000177C0
        public void Scroll(IntVector3 MiddleIndex)
        {
            bool flag = this.Scrolling || MiddleIndex == this.ChunkRangeMiddle;
            checked
            {
                if (!flag)
                {
                    this.Scrolling = true;
                    IntVector3 OldChunkRangeMin = this.ChunkRangeMin;
                    IntVector3 OldChunkRangeMax = this.ChunkRangeMax;
                    this.CreateChunkRangeBounds(MiddleIndex);
                    bool OutOfRange = false;
                    IntVector3 NewRangeMiddle = this.ChunkRangeMiddle;
                    bool flag2 = this.ChunkRangeMin.X < 0;
                    if (flag2)
                    {
                        OutOfRange = true;
                        NewRangeMiddle.X = this.ChunkRangeAddingVal.X;
                    }
                    else
                    {
                        bool flag3 = this.ChunkRangeMax.X >= Loader.MaxWorldBorders.X;
                        if (flag3)
                        {
                            OutOfRange = true;
                            NewRangeMiddle.X = Loader.MaxWorldBorders.X - this.ChunkRangeAddingVal.X - 1;
                        }
                    }
                    bool flag4 = this.ChunkRangeMin.Z < 0;
                    if (flag4)
                    {
                        OutOfRange = true;
                        NewRangeMiddle.Z = this.ChunkRangeAddingVal.Z;
                    }
                    else
                    {
                        bool flag5 = this.ChunkRangeMax.Z >= Loader.MaxWorldBorders.Z;
                        if (flag5)
                        {
                            OutOfRange = true;
                            NewRangeMiddle.Z = Loader.MaxWorldBorders.Z - this.ChunkRangeAddingVal.Z - 1;
                        }
                    }
                    bool flag6 = OutOfRange;
                    if (flag6)
                    {
                        this.CreateChunkRangeBounds(NewRangeMiddle);
                    }
                    bool flag7 = this.ChunkRangeMax.X == OldChunkRangeMax.X && this.ChunkRangeMax.Z == OldChunkRangeMax.Z;
                    if (flag7)
                    {
                        this.Scrolling = false;
                    }
                    else
                    {
                        int TmpFilledSFC = Ground.FilledSFC;
                        Ground.FilledSFC = 0;
                        int nFSC = 0;
                        int num = TmpFilledSFC - 1;
                        for (int i = 0; i <= num; i++)
                        {
                            Chunk Ch = Ground.SurfaceChunks[i];
                            Ground.SurfaceChunks[i] = null;
                            bool flag8 = Ch.Index.X >= this.ChunkRangeMin.X && Ch.Index.Z >= this.ChunkRangeMin.Z && Ch.Index.X <= this.ChunkRangeMax.X && Ch.Index.Z <= this.ChunkRangeMax.Z;
                            if (flag8)
                            {
                                Ch.SurfaceChunkIndex = nFSC;
                                Ground.SurfaceChunks[nFSC] = Ch;
                                nFSC++;
                            }
                            else
                            {
                                Ch.SurfaceChunkIndex = -1;
                                bool changed = Ch.Changed;
                                if (changed)
                                {
                                    this.SavingChunks[this.nSavingChunks] = Ch;
                                    Ch.Changed = false;
                                    this.nSavingChunks++;
                                    Ch.Removed = true;
                                }
                                else
                                {
                                    Ch.Dispose();
                                }
                            }
                        }
                        Ground.FilledSFC = nFSC;
                        bool flag9 = this.ChunkRangeMax.X > OldChunkRangeMax.X;
                        if (flag9)
                        {
                            bool flag10 = this.ChunkRangeMax.Z > OldChunkRangeMax.Z;
                            if (flag10)
                            {
                                this.RootX += this.ChunkRangeMax.X - OldChunkRangeMax.X;
                                bool flag11 = this.RootX >= Stack.Size.X;
                                if (flag11)
                                {
                                    this.RootX -= Stack.Size.X;
                                }
                                this.RootZ += this.ChunkRangeMax.Z - OldChunkRangeMax.Z;
                                bool flag12 = this.RootZ >= Stack.Size.Z;
                                if (flag12)
                                {
                                    this.RootZ -= Stack.Size.Z;
                                }
                                IntVector3 MinPX = new IntVector3(OldChunkRangeMax.X, 0, this.ChunkRangeMin.Z);
                                IntVector3 MaxPX = new IntVector3(this.ChunkRangeMax.X, 0, this.ChunkRangeMax.Z);
                                IntVector3 MinPZ = new IntVector3(this.ChunkRangeMin.X, 0, OldChunkRangeMax.Z);
                                IntVector3 MaxPZ = new IntVector3(OldChunkRangeMax.X, 0, this.ChunkRangeMax.Z);
                                Loader.LoadAndReplaceChunksRegions(MinPX, MaxPX, MinPZ, MaxPZ);
                            }
                            else
                            {
                                bool flag13 = this.ChunkRangeMax.Z == OldChunkRangeMax.Z;
                                if (flag13)
                                {
                                    this.RootX += this.ChunkRangeMax.X - OldChunkRangeMax.X;
                                    bool flag14 = this.RootX >= Stack.Size.X;
                                    if (flag14)
                                    {
                                        this.RootX -= Stack.Size.X;
                                    }
                                    IntVector3 MinPX2 = new IntVector3(OldChunkRangeMax.X, 0, OldChunkRangeMin.Z);
                                    IntVector3 MaxPX2 = new IntVector3(this.ChunkRangeMax.X, 0, OldChunkRangeMax.Z);
                                    Loader.LoadAndReplaceChunksRegion(MinPX2, MaxPX2);
                                }
                                else
                                {
                                    bool flag15 = this.ChunkRangeMax.Z < OldChunkRangeMax.Z;
                                    if (flag15)
                                    {
                                        this.RootX += this.ChunkRangeMax.X - OldChunkRangeMax.X;
                                        bool flag16 = this.RootX >= Stack.Size.X;
                                        if (flag16)
                                        {
                                            this.RootX -= Stack.Size.X;
                                        }
                                        this.RootZ += this.ChunkRangeMax.Z - OldChunkRangeMax.Z;
                                        bool flag17 = this.RootZ < 0;
                                        if (flag17)
                                        {
                                            this.RootZ += Stack.Size.Z;
                                        }
                                        IntVector3 MinPX3 = new IntVector3(OldChunkRangeMax.X, 0, this.ChunkRangeMin.Z);
                                        IntVector3 MaxPX3 = new IntVector3(this.ChunkRangeMax.X, 0, this.ChunkRangeMax.Z);
                                        IntVector3 MinPZ2 = new IntVector3(this.ChunkRangeMin.X, 0, this.ChunkRangeMin.Z);
                                        IntVector3 MaxPZ2 = new IntVector3(OldChunkRangeMax.X, 0, OldChunkRangeMin.Z);
                                        Loader.LoadAndReplaceChunksRegions(MinPX3, MaxPX3, MinPZ2, MaxPZ2);
                                    }
                                }
                            }
                        }
                        else
                        {
                            bool flag18 = this.ChunkRangeMax.X < OldChunkRangeMax.X;
                            if (flag18)
                            {
                                bool flag19 = this.ChunkRangeMax.Z > OldChunkRangeMax.Z;
                                if (flag19)
                                {
                                    this.RootX += this.ChunkRangeMax.X - OldChunkRangeMax.X;
                                    bool flag20 = this.RootX < 0;
                                    if (flag20)
                                    {
                                        this.RootX += Stack.Size.X;
                                    }
                                    this.RootZ += this.ChunkRangeMax.Z - OldChunkRangeMax.Z;
                                    bool flag21 = this.RootZ >= Stack.Size.Z;
                                    if (flag21)
                                    {
                                        this.RootZ -= Stack.Size.Z;
                                    }
                                    IntVector3 MinPX4 = new IntVector3(this.ChunkRangeMin.X, 0, this.ChunkRangeMin.Z);
                                    IntVector3 MaxPX4 = new IntVector3(OldChunkRangeMin.X, 0, this.ChunkRangeMax.Z);
                                    IntVector3 MinPZ3 = new IntVector3(OldChunkRangeMin.X, 0, OldChunkRangeMax.Z);
                                    IntVector3 MaxPZ3 = new IntVector3(this.ChunkRangeMax.X, 0, this.ChunkRangeMax.Z);
                                    Loader.LoadAndReplaceChunksRegions(MinPX4, MaxPX4, MinPZ3, MaxPZ3);
                                }
                                else
                                {
                                    bool flag22 = this.ChunkRangeMax.Z == OldChunkRangeMax.Z;
                                    if (flag22)
                                    {
                                        this.RootX += this.ChunkRangeMax.X - OldChunkRangeMax.X;
                                        bool flag23 = this.RootX < 0;
                                        if (flag23)
                                        {
                                            this.RootX += Stack.Size.X;
                                        }
                                        IntVector3 MinPX5 = new IntVector3(this.ChunkRangeMin.X, 0, this.ChunkRangeMin.Z);
                                        IntVector3 MaxPX5 = new IntVector3(OldChunkRangeMin.X, 0, this.ChunkRangeMax.Z);
                                        Loader.LoadAndReplaceChunksRegion(MinPX5, MaxPX5);
                                    }
                                    else
                                    {
                                        bool flag24 = this.ChunkRangeMax.Z < OldChunkRangeMax.Z;
                                        if (flag24)
                                        {
                                            this.RootX += this.ChunkRangeMax.X - OldChunkRangeMax.X;
                                            bool flag25 = this.RootX < 0;
                                            if (flag25)
                                            {
                                                this.RootX += Stack.Size.X;
                                            }
                                            this.RootZ += this.ChunkRangeMax.Z - OldChunkRangeMax.Z;
                                            bool flag26 = this.RootZ < 0;
                                            if (flag26)
                                            {
                                                this.RootZ += Stack.Size.Z;
                                            }
                                            IntVector3 MinPX6 = new IntVector3(this.ChunkRangeMin.X, 0, this.ChunkRangeMin.Z);
                                            IntVector3 MaxPX6 = new IntVector3(OldChunkRangeMin.X, 0, this.ChunkRangeMax.Z);
                                            IntVector3 MinPZ4 = new IntVector3(OldChunkRangeMin.X, 0, this.ChunkRangeMin.Z);
                                            IntVector3 MaxPZ4 = new IntVector3(this.ChunkRangeMax.X, 0, OldChunkRangeMin.Z);
                                            Loader.LoadAndReplaceChunksRegions(MinPX6, MaxPX6, MinPZ4, MaxPZ4);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                bool flag27 = this.ChunkRangeMax.X == OldChunkRangeMax.X;
                                if (flag27)
                                {
                                    bool flag28 = this.ChunkRangeMax.Z > OldChunkRangeMax.Z;
                                    if (flag28)
                                    {
                                        this.RootZ += this.ChunkRangeMax.Z - OldChunkRangeMax.Z;
                                        bool flag29 = this.RootZ >= Stack.Size.Z;
                                        if (flag29)
                                        {
                                            this.RootZ -= Stack.Size.Z;
                                        }
                                        IntVector3 MinPZ5 = new IntVector3(OldChunkRangeMin.X, 0, OldChunkRangeMax.Z);
                                        IntVector3 MaxPZ5 = new IntVector3(OldChunkRangeMax.X, 0, this.ChunkRangeMax.Z);
                                        Loader.LoadAndReplaceChunksRegion(MinPZ5, MaxPZ5);
                                    }
                                    else
                                    {
                                        bool flag30 = this.ChunkRangeMax.Z < OldChunkRangeMax.Z;
                                        if (flag30)
                                        {
                                            this.RootZ += this.ChunkRangeMax.Z - OldChunkRangeMax.Z;
                                            bool flag31 = this.RootZ < 0;
                                            if (flag31)
                                            {
                                                this.RootZ += Stack.Size.Z;
                                            }
                                            IntVector3 MinPZ6 = new IntVector3(this.ChunkRangeMin.X, 0, this.ChunkRangeMin.Z);
                                            IntVector3 MaxPZ6 = new IntVector3(OldChunkRangeMax.X, 0, OldChunkRangeMin.Z);
                                            Loader.LoadAndReplaceChunksRegion(MinPZ6, MaxPZ6);
                                        }
                                    }
                                }
                            }
                        }
                        Array.Clear(Ground.TmpSurfaceChunks, 0, Ground.TmpFilledSFC + 1);
                        Ground.TmpFilledSFC = 0;
                        this.Scrolling = false;
                    }
                }
            }
        }

        /// <summary> 
        /// <Must>X = Z</Must>
        /// </summary>
        // Token: 0x040001FE RID: 510
        public static IntVector3 Size = new IntVector3(30, 15, 30);

        // Token: 0x040001FF RID: 511
        public static Vector3 Volume = Stack.Size * Chunk.Size * 50f;

        /// <summary>
        /// X - Y - Z
        /// </summary>
        // Token: 0x04000200 RID: 512
        public Chunk[][][] ChunkList;

        // Token: 0x04000201 RID: 513
        public int RootX;

        // Token: 0x04000202 RID: 514
        public int RootZ;

        // Token: 0x04000203 RID: 515
        public int MinX;

        // Token: 0x04000204 RID: 516
        public int MinZ;

        // Token: 0x04000205 RID: 517
        public IntVector3 ChunkRangeMin;

        // Token: 0x04000206 RID: 518
        public IntVector3 ChunkRangeMax;

        // Token: 0x04000207 RID: 519
        public IntVector3 ChunkRangeMiddle;

        // Token: 0x04000208 RID: 520
        public IntVector3 ChunkRangeAddingVal;

        // Token: 0x04000209 RID: 521
        public Vector3 Position;

        // Token: 0x0400020A RID: 522
        public int Index;

        // Token: 0x0400020B RID: 523
        public Chunk[] SavingChunks;

        // Token: 0x0400020C RID: 524
        public int nSavingChunks;

        // Token: 0x0400020D RID: 525
        public List<Entity> eList;

        // Token: 0x0400020E RID: 526
        public bool Scrolling;
    }
}
