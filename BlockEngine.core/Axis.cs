using System;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
	// Token: 0x02000035 RID: 53
	public class Axis
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0001CAEB File Offset: 0x0001ACEB
		public Axis()
		{
			this.Offset = Vector3.Zero;
			this.Gradient = Vector3.One;
			this.IsFromWorldCords = false;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0001CB14 File Offset: 0x0001AD14
		public Axis(char AAxisChar, Vector3 OOffset, Vector3 GGradient, bool OOwnerIsEntity, object OOwner)
		{
			this.Offset = Vector3.Zero;
			this.Gradient = Vector3.One;
			this.IsFromWorldCords = false;
			this.AxisChar = AAxisChar;
			this.Offset = OOffset;
			this.Gradient = GGradient;
			this.OwnerIsEntity = OOwnerIsEntity;
			bool ownerIsEntity = this.OwnerIsEntity;
			if (ownerIsEntity)
			{
				this.OwnerEntity = (Entity)OOwner;
			}
			else
			{
				this.Owner_ePart = (ePart)OOwner;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0001CB8C File Offset: 0x0001AD8C
		public Axis(char AAxisChar, Vector3 OOffset, bool OOwnerIsEntity, object OOwner)
		{
			this.Offset = Vector3.Zero;
			this.Gradient = Vector3.One;
			this.IsFromWorldCords = false;
			this.AxisChar = AAxisChar;
			this.Offset = OOffset;
			this.OwnerIsEntity = OOwnerIsEntity;
			bool ownerIsEntity = this.OwnerIsEntity;
			if (ownerIsEntity)
			{
				this.OwnerEntity = (Entity)OOwner;
			}
			else
			{
				this.Owner_ePart = (ePart)OOwner;
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0001CBFC File Offset: 0x0001ADFC
		public Axis(string AAxisChar, Vector3 GGradient, bool OOwnerIsEntity, object OOwner)
		{
			this.Offset = Vector3.Zero;
			this.Gradient = Vector3.One;
			this.IsFromWorldCords = false;
			this.AxisChar = Conversions.ToChar(AAxisChar);
			this.Gradient = GGradient;
			this.OwnerIsEntity = OOwnerIsEntity;
			bool ownerIsEntity = this.OwnerIsEntity;
			if (ownerIsEntity)
			{
				this.OwnerEntity = (Entity)OOwner;
			}
			else
			{
				this.Owner_ePart = (ePart)OOwner;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0001CC74 File Offset: 0x0001AE74
		public Axis(char AAxisChar, bool OOwnerIsEntity, object OOwner)
		{
			this.Offset = Vector3.Zero;
			this.Gradient = Vector3.One;
			this.IsFromWorldCords = false;
			this.AxisChar = AAxisChar;
			this.OwnerIsEntity = OOwnerIsEntity;
			bool ownerIsEntity = this.OwnerIsEntity;
			if (ownerIsEntity)
			{
				this.OwnerEntity = (Entity)OOwner;
			}
			else
			{
				this.Owner_ePart = (ePart)OOwner;
			}
		}

		/// <summary>
		/// Gets from World Cords
		/// </summary>
		/// <param name="AAxisChar"></param>
		// Token: 0x06000212 RID: 530 RVA: 0x0001CCDB File Offset: 0x0001AEDB
		public Axis(char AAxisChar)
		{
			this.Offset = Vector3.Zero;
			this.Gradient = Vector3.One;
			this.IsFromWorldCords = false;
			this.AxisChar = AAxisChar;
			this.IsFromWorldCords = true;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0001CD10 File Offset: 0x0001AF10
		public static implicit operator Vector3(Axis v)
		{
			Vector3 Out = default;
			bool flag = !v.IsFromWorldCords;
			if (flag)
			{
				bool ownerIsEntity = v.OwnerIsEntity;
				if (ownerIsEntity)
				{
					Out = Physics.GetAxisByChar(v.OwnerEntity.ModelRotation, v.AxisChar);
				}
				else
				{
					Out = Physics.GetAxisByChar(v.Owner_ePart.Rotation, v.AxisChar);
				}
				Out += v.Offset;
				Out *= v.Gradient;
			}
			else
			{
				char C = v.AxisChar;
				bool flag2 = Operators.CompareString(Conversions.ToString(C), "D", false) == 0;
				if (flag2)
				{
					Out = Vector3.Down;
				}
				else
				{
					bool flag3 = Operators.CompareString(Conversions.ToString(C), "U", false) == 0;
					if (flag3)
					{
						Out = Vector3.Up;
					}
					else
					{
						bool flag4 = Operators.CompareString(Conversions.ToString(C), "F", false) == 0;
						if (flag4)
						{
							Out = Vector3.Forward;
						}
						else
						{
							bool flag5 = Operators.CompareString(Conversions.ToString(C), "B", false) == 0;
							if (flag5)
							{
								Out = Vector3.Backward;
							}
							else
							{
								bool flag6 = Operators.CompareString(Conversions.ToString(C), "L", false) == 0;
								if (flag6)
								{
									Out = Vector3.Left;
								}
								else
								{
									bool flag7 = Operators.CompareString(Conversions.ToString(C), "R", false) == 0;
									if (flag7)
									{
										Out = Vector3.Right;
									}
								}
							}
						}
					}
				}
				Out += v.Offset;
				Out *= v.Gradient;
			}
			return Out;
		}

		// Token: 0x04000232 RID: 562
		public char AxisChar;

		// Token: 0x04000233 RID: 563
		public Vector3 Offset;

		// Token: 0x04000234 RID: 564
		public Vector3 Gradient;

		// Token: 0x04000235 RID: 565
		public bool OwnerIsEntity;

		// Token: 0x04000236 RID: 566
		public Entity OwnerEntity;

		// Token: 0x04000237 RID: 567
		public ePart Owner_ePart;

		// Token: 0x04000238 RID: 568
		public bool IsFromWorldCords;
	}
}
