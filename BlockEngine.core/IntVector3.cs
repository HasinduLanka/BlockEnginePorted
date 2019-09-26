using System;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
	// Token: 0x02000028 RID: 40
	public struct IntVector3 : IEquatable<IntVector3>
    {
		// Token: 0x06000184 RID: 388 RVA: 0x00016472 File Offset: 0x00014672
		public IntVector3(int X, int Y, int Z)
		{
			this = default;
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00016491 File Offset: 0x00014691
		public IntVector3(int AllXYZAs)
		{
			this = default;
			this.X = AllXYZAs;
			this.Y = AllXYZAs;
			this.Z = AllXYZAs;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000164B0 File Offset: 0x000146B0
		public static IntVector3 operator *(IntVector3 L, IntVector3 R)
		{
			IntVector3 result = checked(new IntVector3(L.X * R.X, L.Y * R.Y, L.Z * R.Z));
			return result;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000164F0 File Offset: 0x000146F0
		public static IntVector3 operator *(IntVector3 L, int R)
		{
			IntVector3 result = checked(new IntVector3(Convert.ToInt32(Math.Truncate(new decimal(L.X * R))), Convert.ToInt32(Math.Truncate(new decimal(L.Y * R))), Convert.ToInt32(Math.Truncate(new decimal(L.Z * R)))));
			return result;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00016550 File Offset: 0x00014750
		public static IntVector3 operator /(IntVector3 L, int R)
		{
			IntVector3 result = checked(new IntVector3((int)((double)L.X / (double)R), (int)((double)L.Y / (double)R), (int)((double)L.Z / (double)R)));
			return result;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0001658C File Offset: 0x0001478C
		public static IntVector3 operator +(IntVector3 L, IntVector3 R)
		{
			IntVector3 result = checked(new IntVector3(L.X + R.X, L.Y + R.Y, L.Z + R.Z));
			return result;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000165CC File Offset: 0x000147CC
		public static IntVector3 operator -(IntVector3 L, IntVector3 R)
		{
			IntVector3 result = checked(new IntVector3(L.X - R.X, L.Y - R.Y, L.Z - R.Z));
			return result;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0001660C File Offset: 0x0001480C
		public static IntVector3 operator /(IntVector3 L, IntVector3 R)
		{
			IntVector3 result = checked(new IntVector3((int)((double)L.X / (double)R.X), (int)((double)L.Y / (double)R.Y), (int)((double)L.Z / (double)R.Z)));
			return result;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00016654 File Offset: 0x00014854
		public static IntVector3 operator %(IntVector3 L, int R)
		{
			IntVector3 result = new IntVector3(L.X % R, L.Y % R, L.Z % R);
			return result;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00016684 File Offset: 0x00014884
		public static implicit operator Vector3(IntVector3 v)
		{
			Vector3 result = new Vector3((float)v.X, (float)v.Y, (float)v.Z);
			return result;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000166B4 File Offset: 0x000148B4
		public static IntVector3 FromV3Truncated(Vector3 V3)
		{
			IntVector3 FromV3Truncated = checked(new IntVector3((int)((double)V3.X), (int)((double)V3.Y), (int)((double)V3.Z)));
			return FromV3Truncated;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000166E4 File Offset: 0x000148E4
		public static Vector3 TruncateV3(Vector3 V3)
		{
			Vector3 TruncateV3 = new Vector3((float)Math.Truncate((double)V3.X), (float)Math.Truncate((double)V3.Y), (float)Math.Truncate((double)V3.Z));
			return TruncateV3;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00016724 File Offset: 0x00014924
		public static Vector3 RoundV3(Vector3 V3)
		{
			Vector3 RoundV3 = new Vector3((float)Math.Round((double)V3.X), (float)Math.Round((double)V3.Y), (float)Math.Round((double)V3.Z));
			return RoundV3;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00016764 File Offset: 0x00014964
		public static IntVector3 FromV3Rounded(Vector3 V3)
		{
			IntVector3 FromV3Rounded = checked(new IntVector3((int)Math.Round((double)V3.X), (int)Math.Round((double)V3.Y), (int)Math.Round((double)V3.Z)));
			return FromV3Rounded;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000167A4 File Offset: 0x000149A4
		public static IntVector3 FromV3Rounded(float X, float Y, float Z)
		{
			IntVector3 FromV3Rounded = checked(new IntVector3((int)Math.Round((double)X), (int)Math.Round((double)Y), (int)Math.Round((double)Z)));
			return FromV3Rounded;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000167D4 File Offset: 0x000149D4
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

		// Token: 0x06000194 RID: 404 RVA: 0x00016834 File Offset: 0x00014A34
		public static bool operator !=(IntVector3 left, IntVector3 right)
		{
			bool flag = left.X == right.X && left.Y == right.Y && left.Z == right.Z;
			return !flag;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00016880 File Offset: 0x00014A80
		public static bool operator ==(IntVector3 left, IntVector3 right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000168CC File Offset: 0x00014ACC
		public override bool Equals(object obj)
		{
			IntVector3 R = (IntVector3)obj;
			return this.X == R.X && this.Y == R.Y && this.Z == R.Z;
		}

        public bool Equals(IntVector3 other)
        {
            return X == other.X &&
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

        // Token: 0x06000197 RID: 407 RVA: 0x0001691C File Offset: 0x00014B1C
        public static bool operator !=(IntVector3 left, BVector3 right)
		{
			bool flag = left.X == (int)right.X && left.Y == (int)right.Y && left.Z == (int)right.Z;
			return !flag;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00016968 File Offset: 0x00014B68
		public static bool operator ==(IntVector3 left, BVector3 right)
		{
			return left.X == (int)right.X && left.Y == (int)right.Y && left.Z == (int)right.Z;
		}

		// Token: 0x040001C5 RID: 453
		public int X;

		// Token: 0x040001C6 RID: 454
		public int Y;

		// Token: 0x040001C7 RID: 455
		public int Z;

		// Token: 0x040001C8 RID: 456
		public static IntVector3 Up = new IntVector3(0, 1, 0);

		// Token: 0x040001C9 RID: 457
		public static IntVector3 Down = new IntVector3(0, -1, 0);

		// Token: 0x040001CA RID: 458
		public static IntVector3 Right = new IntVector3(1, 0, 0);

		// Token: 0x040001CB RID: 459
		public static IntVector3 Left = new IntVector3(-1, 0, 0);

		// Token: 0x040001CC RID: 460
		public static IntVector3 Back = new IntVector3(0, 0, 1);

		// Token: 0x040001CD RID: 461
		public static IntVector3 Foward = new IntVector3(0, 0, -1);

		// Token: 0x040001CE RID: 462
		public static IntVector3 Zero = new IntVector3(0, 0, 0);

		// Token: 0x040001CF RID: 463
		public static IntVector3 One = new IntVector3(1, 1, 1);

		// Token: 0x040001D0 RID: 464
		public static IntVector3 MinusOne = new IntVector3(-1, -1, -1);
	}
}
