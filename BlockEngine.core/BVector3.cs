using System;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
    // Token: 0x02000029 RID: 41
    public class BVector3 : IEquatable<BVector3>
    {
        // Token: 0x06000199 RID: 409 RVA: 0x000169B1 File Offset: 0x00014BB1
        public BVector3()
        {
        }

        // Token: 0x0600019A RID: 410 RVA: 0x000169BB File Offset: 0x00014BBB
        public BVector3(byte X, byte Y, byte Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        // Token: 0x0600019B RID: 411 RVA: 0x000169DA File Offset: 0x00014BDA
        public BVector3(byte AllXYZAs)
        {
            this.X = AllXYZAs;
            this.Y = AllXYZAs;
            this.Z = AllXYZAs;
        }

        public BVector3(int v1, int v2, int v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        // Token: 0x0600019C RID: 412 RVA: 0x000169FC File Offset: 0x00014BFC
        public static BVector3 operator *(BVector3 L, BVector3 R)
        {
            return new BVector3(L.X * R.X, L.Y * R.Y, L.Z * R.Z);
        }

        // Token: 0x0600019D RID: 413 RVA: 0x00016A40 File Offset: 0x00014C40
        public static BVector3 operator *(BVector3 L, int R)
        {
            return checked(new BVector3((byte)((int)L.X * R), (byte)((int)L.Y * R), (byte)((int)L.Z * R)));
        }

        // Token: 0x0600019E RID: 414 RVA: 0x00016A74 File Offset: 0x00014C74
        public static BVector3 operator +(BVector3 L, BVector3 R)
        {
            return new BVector3(L.X + R.X, L.Y + R.Y, L.Z + R.Z);
        }

        // Token: 0x0600019F RID: 415 RVA: 0x00016AB8 File Offset: 0x00014CB8
        public static BVector3 operator -(BVector3 L, BVector3 R)
        {
            return new BVector3(L.X - R.X, L.Y - R.Y, L.Z - R.Z);
        }

        // Token: 0x060001A0 RID: 416 RVA: 0x00016AFC File Offset: 0x00014CFC
        public static BVector3 operator /(BVector3 L, BVector3 R)
        {
            return checked(new BVector3((byte)Math.Round((double)L.X / (double)R.X), (byte)Math.Round((double)L.Y / (double)R.Y), (byte)Math.Round((double)L.Z / (double)R.Z)));
        }

        // Token: 0x060001A1 RID: 417 RVA: 0x00016B54 File Offset: 0x00014D54
        public static BVector3 operator *(BVector3 L, Vector3 R)
        {
            return checked(new BVector3((byte)Math.Round((double)(unchecked((float)L.X * R.X))), (byte)Math.Round((double)(unchecked((float)L.Y * R.Y))), (byte)Math.Round((double)(unchecked((float)L.Z * R.Z)))));
        }

        // Token: 0x060001A2 RID: 418 RVA: 0x00016BAC File Offset: 0x00014DAC
        public static BVector3 operator +(BVector3 L, Vector3 R)
        {
            return checked(new BVector3((byte)Math.Round((double)(unchecked((float)L.X + R.X))), (byte)Math.Round((double)(unchecked((float)L.Y + R.Y))), (byte)Math.Round((double)(unchecked((float)L.Z + R.Z)))));
        }

        // Token: 0x060001A3 RID: 419 RVA: 0x00016C04 File Offset: 0x00014E04
        public static BVector3 operator -(BVector3 L, Vector3 R)
        {
            return checked(new BVector3((byte)Math.Round((double)(unchecked((float)L.X - R.X))), (byte)Math.Round((double)(unchecked((float)L.Y - R.Y))), (byte)Math.Round((double)(unchecked((float)L.Z - R.Z)))));
        }

        // Token: 0x060001A4 RID: 420 RVA: 0x00016C5C File Offset: 0x00014E5C
        public static BVector3 operator /(BVector3 L, Vector3 R)
        {
            return checked(new BVector3((byte)Math.Round((double)((float)L.X / R.X)), (byte)Math.Round((double)((float)L.Y / R.Y)), (byte)Math.Round((double)((float)L.Z / R.Z))));
        }

        // Token: 0x060001A5 RID: 421 RVA: 0x00016CB4 File Offset: 0x00014EB4
        public static IntVector3 operator *(BVector3 L, IntVector3 R)
        {
            IntVector3 result = checked(new IntVector3((int)L.X * R.X, (int)L.Y * R.Y, (int)L.Z * R.Z));
            return result;
        }

        // Token: 0x060001A6 RID: 422 RVA: 0x00016CF4 File Offset: 0x00014EF4
        public static IntVector3 operator +(BVector3 L, IntVector3 R)
        {
            IntVector3 result = checked(new IntVector3((int)L.X + R.X, (int)L.Y + R.Y, (int)L.Z + R.Z));
            return result;
        }

        // Token: 0x060001A7 RID: 423 RVA: 0x00016D34 File Offset: 0x00014F34
        public static IntVector3 operator -(BVector3 L, IntVector3 R)
        {
            IntVector3 result = checked(new IntVector3((int)L.X - R.X, (int)L.Y - R.Y, (int)L.Z - R.Z));
            return result;
        }

        // Token: 0x060001A8 RID: 424 RVA: 0x00016D74 File Offset: 0x00014F74
        public static IntVector3 operator /(BVector3 L, IntVector3 R)
        {
            IntVector3 result = checked(new IntVector3((int)((byte)Math.Round((double)L.X / (double)R.X)), (int)((byte)Math.Round((double)L.Y / (double)R.Y)), (int)((byte)Math.Round((double)L.Z / (double)R.Z))));
            return result;
        }

        // Token: 0x060001A9 RID: 425 RVA: 0x00016DCC File Offset: 0x00014FCC
        public static implicit operator IntVector3(BVector3 v)
        {
            IntVector3 result = new IntVector3((int)v.X, (int)v.Y, (int)v.Z);
            return result;
        }

        // Token: 0x060001AA RID: 426 RVA: 0x00016DF8 File Offset: 0x00014FF8
        public static implicit operator Vector3(BVector3 v)
        {
            Vector3 result = new Vector3((float)v.X, (float)v.Y, (float)v.Z);
            return result;
        }

        // Token: 0x060001AB RID: 427 RVA: 0x00016E28 File Offset: 0x00015028
        public static implicit operator BVector3(IntVector3 v)
        {
            return checked(new BVector3((byte)v.X, (byte)v.Y, (byte)v.Z));
        }

        // Token: 0x060001AC RID: 428 RVA: 0x00016E54 File Offset: 0x00015054
        public static implicit operator BVector3(Vector3 v)
        {
            return checked(new BVector3((byte)Math.Round((double)v.X), (byte)Math.Round((double)v.Y), (byte)Math.Round((double)v.Z)));
        }

        // Token: 0x060001AD RID: 429 RVA: 0x00016E94 File Offset: 0x00015094
        public static BVector3 FromV3Truncated(Vector3 V3)
        {
            return checked(new BVector3((byte)((double)V3.X), (byte)((double)V3.Y), (byte)((double)V3.Z)));
        }

        // Token: 0x060001AE RID: 430 RVA: 0x00016EC4 File Offset: 0x000150C4
        public static BVector3 FromV3Rounded(Vector3 V3)
        {
            return checked(new BVector3((byte)Math.Round((double)V3.X), (byte)Math.Round((double)V3.Y), (byte)Math.Round((double)V3.Z)));
        }

        // Token: 0x060001AF RID: 431 RVA: 0x00016F04 File Offset: 0x00015104
        public static BVector3 FromIntV3(IntVector3 V3)
        {
            return checked(new BVector3((byte)V3.X, (byte)V3.Y, (byte)V3.Z));
        }

        // Token: 0x060001B0 RID: 432 RVA: 0x00016F30 File Offset: 0x00015130
        public override string ToString()
        {
            return string.Concat(new string[]
            {
                "X=",
                this.X.ToString(),
                ", Y=",
                this.Y.ToString(),
                ", Z=",
                this.Z.ToString()
            });
        }

        // Token: 0x060001B1 RID: 433 RVA: 0x00016F90 File Offset: 0x00015190
        public static bool operator !=(BVector3 left, BVector3 right)
        {
            bool flag = left.X == right.X && left.Y == right.Y && left.Z == right.Z;
            return !flag;
        }

        // Token: 0x060001B2 RID: 434 RVA: 0x00016FDC File Offset: 0x000151DC
        public static bool operator ==(BVector3 left, BVector3 right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        // Token: 0x060001B3 RID: 435 RVA: 0x00017028 File Offset: 0x00015228
        public override bool Equals(object obj)
        {
            BVector3 R = (BVector3)obj;
            return this.X == R.X && this.Y == R.Y && this.Z == R.Z;
        }

        public bool Equals(BVector3 other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y &&
                   Z == other.Z;
        }

        public override int GetHashCode()
        {
            var hashCode = -307843816;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }

        // Token: 0x040001D1 RID: 465
        public byte X;

        // Token: 0x040001D2 RID: 466
        public byte Y;

        // Token: 0x040001D3 RID: 467
        public byte Z;
        private int v1;
        private int v2;
        private int v3;
    }
}
