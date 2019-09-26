using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
    // Token: 0x0200002B RID: 43
    [StandardModule]
    public sealed class Funcs
    {
        // Token: 0x060001B5 RID: 437 RVA: 0x00017084 File Offset: 0x00015284
        public static Vector2 RndPosition(Vector2 min, Vector2 max)
        {
            Vector2 RndPosition = checked(new Vector2((float)Funcs.RND.Next((int)Math.Round((double)min.X), (int)Math.Round((double)max.X)), (float)Funcs.RND.Next((int)Math.Round((double)min.Y), (int)Math.Round((double)max.Y))));
            return RndPosition;
        }

        // Token: 0x060001B6 RID: 438 RVA: 0x000170E8 File Offset: 0x000152E8
        public static Vector2 VLerp(Vector2 A, Vector2 B, float Amount)
        {
            return A + (B - A) * Amount;
        }

        // Token: 0x060001B7 RID: 439 RVA: 0x00017110 File Offset: 0x00015310
        public static Vector2 TruncateV2(Vector2 V2)
        {
            Vector2 TruncateV2 = new Vector2((float)Math.Truncate((double)V2.X), (float)Math.Truncate((double)V2.Y));
            return TruncateV2;
        }

        // Token: 0x060001B8 RID: 440 RVA: 0x00017144 File Offset: 0x00015344
        public static Vector2 RoundV2(Vector2 V2)
        {
            Vector2 RoundV2 = new Vector2((float)Math.Round((double)V2.X), (float)Math.Round((double)V2.Y));
            return RoundV2;
        }

        // Token: 0x060001B9 RID: 441 RVA: 0x00017178 File Offset: 0x00015378
        public static int[] ListDif(int F, int L, int nOfVal)
        {
            checked
            {
                int[] Out = new int[nOfVal - 1 + 1];
                int FLDif = L - F;
                double Dif = (double)FLDif / (double)nOfVal;
                for (int X = 1; X <= nOfVal; X++)
                {
                    Out[X - 1] = F + (int)Math.Round(unchecked((double)X * Dif));
                }
                return Out;
            }
        }

        // Token: 0x060001BA RID: 442 RVA: 0x000171C8 File Offset: 0x000153C8
        public static int[] ListDifSqured(int F, int L, int nOfVal)
        {
            checked
            {
                int[] Out = new int[nOfVal - 1 + 1];
                int FLDif = L - F;
                double Dif = (double)FLDif / (double)nOfVal;
                for (int X = 1; X <= nOfVal; X++)
                {
                    Out[X - 1] = (int)Math.Round(Math.Sqrt(unchecked((double)(checked(L * L)) * ((double)X / (double)nOfVal) + (double)(checked(F * F)) * (1.0 - (double)X / (double)nOfVal))));
                }
                return Out;
            }
        }

        // Token: 0x060001BB RID: 443 RVA: 0x00017238 File Offset: 0x00015438
        public static bool Contains(IntVector3[] A, int Count, IntVector3 Item)
        {
            checked
            {
                int num = Count - 1;
                for (int i = 0; i <= num; i++)
                {
                    bool flag = A[i] == Item;
                    if (flag)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        // Token: 0x060001BC RID: 444 RVA: 0x00017274 File Offset: 0x00015474
        public static bool Contains(List<IntVector3> A, IntVector3 Item)
        {
            checked
            {
                int num = A.Count - 1;
                for (int i = 0; i <= num; i++)
                {
                    bool flag = A[i] == Item;
                    if (flag)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        // Token: 0x060001BD RID: 445 RVA: 0x000172B4 File Offset: 0x000154B4
        public static bool Contains(Chunk[] A, int Count, Chunk Item)
        {
            checked
            {
                int num = Count - 1;
                for (int i = 0; i <= num; i++)
                {
                    bool flag = A[i] == Item;
                    if (flag)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        // Token: 0x040001D7 RID: 471
        public static Random RND = new Random();
    }
}
