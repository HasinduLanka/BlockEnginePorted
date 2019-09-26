using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
    // Token: 0x0200002F RID: 47
    public class Chunk : IDisposable, IEquatable<Chunk>
    {
        // Token: 0x060001E1 RID: 481 RVA: 0x00018C00 File Offset: 0x00016E00
        public void GenarateAirGrid()
        {
            bool[][][] XSet = new bool[8][][];
            int nX = 0;
            checked
            {
                do
                {
                    bool[][] YSet = new bool[8][];
                    int nY = 0;
                    do
                    {
                        bool[] ZSet = new bool[8];
                        int nZ = 0;
                        do
                        {
                            ZSet[nZ] = this.BlockList[nX][nY][nZ].IsAir;
                            nZ++;
                        }
                        while (nZ <= 7);
                        YSet[nY] = ZSet;
                        nY++;
                    }
                    while (nY <= 7);
                    XSet[nX] = YSet;
                    nX++;
                }
                while (nX <= 7);
                this.AirGrid = XSet;
            }
        }

        // Token: 0x060001E2 RID: 482 RVA: 0x00018C70 File Offset: 0x00016E70
        public void GenarateBlockTranslations()
        {
            checked
            {
                this.BlockTranslations = new Vector4[(int)(this.BIDFilledI - 1 + 1)];
                int num = (int)(this.BIDFilledI - 1);
                for (int CI = 0; CI <= num; CI++)
                {
                    byte[] B = this.BIDList[CI];
                    this.BlockTranslations[CI] = unchecked(new Vector4((float)(checked(B[0] * 50)) + this.Position.X, (float)(checked(B[1] * 50)) + this.Position.Y, (float)(checked(B[2] * 50)) + this.Position.Z, 1f));
                }
                int num2 = this.FilledSB - 1;
                for (int i = 0; i <= num2; i++)
                {
                    this.CountForBlockTypes[(int)this.SurfaceBlocks[i].BID]++;
                }
            }
        }

        // Token: 0x060001E3 RID: 483 RVA: 0x00018D3C File Offset: 0x00016F3C
        public void GenerateBIDForBTranslations()
        {
            bool flag = this.BIDList.Length != this.BIDForBTranslations.Length;
            checked
            {
                if (flag)
                {
                    this.BIDForBTranslations = new byte[this.BIDList.Length + 1];
                }
                int num = (int)(this.BIDFilledI - 1);
                for (int i = 0; i <= num; i++)
                {
                    this.BIDForBTranslations[i] = this.BIDList[i][3];
                }
            }
        }

        // Token: 0x060001E4 RID: 484 RVA: 0x00018DA4 File Offset: 0x00016FA4
        public Chunk()
        {
            this.BIDFilledI = 0;
            this.IsAir = true;
            this.Index = new IntVector3(-1, -1, -1);
            this.IsInTheSurface = false;
            this.SurfaceChunkIndex = -1;
            this.FilledSB = 0;
            this.Changed = false;
            this.Removed = false;
            this.CountForBlockTypes = new int[11];
            this.SurfaceBlocks = new Block[513];
            bool[][][] XSet = new bool[8][][];
            int nX = 0;
            checked
            {
                do
                {
                    bool[][] YSet = new bool[8][];
                    int nY = 0;
                    do
                    {
                        bool[] ZSet = new bool[8];
                        int nZ = 0;
                        do
                        {
                            ZSet[nZ] = true;
                            nZ++;
                        }
                        while (nZ <= 7);
                        YSet[nY] = ZSet;
                        nY++;
                    }
                    while (nY <= 7);
                    XSet[nX] = YSet;
                    nX++;
                }
                while (nX <= 7);
                this.AirGrid = XSet;
                this.BIDList = new byte[257][];
                this.BIDForBTranslations = new byte[257];
            }
        }



        // Token: 0x060001E8 RID: 488 RVA: 0x00018F54 File Offset: 0x00017154
        protected virtual void Dispose(bool disposing)
        {
            bool flag = !this.disposedValue;
            if (flag)
            {
                this.disposedValue = true;
                this.BIDFilledI = 0;
                this.SurfaceBlocks = null;
                this.BlockList = null;
                this.BIDList = null;
                this.BIDForBTranslations = null;
                this.AirGrid = null;
                this.BlockTranslations = null;
                this.CountForBlockTypes = null;
                this.Position = default;
                this.BIDFilledI = 0;
                this.IsAir = false;
                this.Position = default;
                this.Index = default;
                this.IsInTheSurface = false;
                this.SurfaceChunkIndex = 0;
                this.FilledSB = 0;
                this.Changed = false;
                this.Removed = false;
            }
        }

        /// <summary>
        /// Do not call unless Ground.SurfaceChunks doesn't contain this Chunk. Remove it first
        /// </summary>
        // Token: 0x060001E9 RID: 489 RVA: 0x0001900A File Offset: 0x0001720A
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Chunk);
        }

        public bool Equals(Chunk other)
        {
            return other != null &&
                   EqualityComparer<IntVector3>.Default.Equals(Index, other.Index);
        }

        public override int GetHashCode()
        {
            return -2134847229 + EqualityComparer<IntVector3>.Default.GetHashCode(Index);
        }

        // Token: 0x040001E8 RID: 488
        public const int IntSize = 8;

        // Token: 0x040001E9 RID: 489
        public static Vector3 Size = new Vector3(8f, 8f, 8f);

        // Token: 0x040001EA RID: 490
        public static IntVector3 SizeInt = new IntVector3(8, 8, 8);

        // Token: 0x040001EB RID: 491
        public static Vector3 Volume = Chunk.Size * 50f;

        // Token: 0x040001EC RID: 492
        public const int VolumeI = 400;

        /// <summary>
        /// X - Y - Z
        /// </summary>
        // Token: 0x040001ED RID: 493
        public Block[][][] BlockList;

        /// <summary>
        /// (#) = Index, (#)(0)= CPosX ,  (#)(1)= CPosY , (#)(2)= CPosZ , (#)(3)= BID
        /// </summary>
        // Token: 0x040001EE RID: 494
        public byte[][] BIDList;

        // Token: 0x040001EF RID: 495
        public short BIDFilledI;

        // Token: 0x040001F0 RID: 496
        public byte[] BIDForBTranslations;

        // Token: 0x040001F1 RID: 497
        public Vector4[] BlockTranslations;

        // Token: 0x040001F2 RID: 498
        public bool IsAir;

        // Token: 0x040001F3 RID: 499
        public Vector3 Position;

        // Token: 0x040001F4 RID: 500
        public IntVector3 Index;

        // Token: 0x040001F5 RID: 501
        public bool IsInTheSurface;

        // Token: 0x040001F6 RID: 502
        public int SurfaceChunkIndex;

        // Token: 0x040001F7 RID: 503
        public Block[] SurfaceBlocks;

        // Token: 0x040001F8 RID: 504
        public int FilledSB;

        // Token: 0x040001F9 RID: 505
        public bool Changed;

        // Token: 0x040001FA RID: 506
        public bool Removed;

        /// <summary>
        /// # = BT ID , (#) = count
        /// </summary>
        // Token: 0x040001FB RID: 507
        public int[] CountForBlockTypes;

        /// <summary>
        /// X - Y - Z
        /// </summary>
        // Token: 0x040001FC RID: 508
        public bool[][][] AirGrid;

        // Token: 0x040001FD RID: 509
        public bool disposedValue;

        public static bool operator ==(Chunk left, Chunk right)
        {
            return EqualityComparer<Chunk>.Default.Equals(left, right);
        }

        public static bool operator !=(Chunk left, Chunk right)
        {
            return !(left == right);
        }
    }
}
