using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BlockEngine
{
	// Token: 0x0200000E RID: 14
	public class Controls
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00004194 File Offset: 0x00002394
		public static List<Controls.Control> NewControlList(Keys Forward, Keys Backward, Keys Left, Keys Right, Keys Up, Keys Down)
		{
			return new List<Controls.Control>
			{
				new Controls.Control(Forward, Actions.Forward),
				new Controls.Control(Backward, Actions.Backward),
				new Controls.Control(Left, Actions.Left),
				new Controls.Control(Right, Actions.Right),
				new Controls.Control(Up, Actions.Up)
			};
		}

		/// <summary>
		/// Returns IsPathBlocked
		/// </summary>
		// Token: 0x06000036 RID: 54 RVA: 0x000041F4 File Offset: 0x000023F4
		public static bool Go(Entity E, Actions A)
		{
			bool IsPathBlocked = false;
			switch (A)
			{
			case Actions.Forward:
				return Controls.Go(E, E.ModelRotationY.Forward, E.Accelaration.Z);
			case Actions.Backward:
				return Controls.Go(E, E.ModelRotationY.Backward, E.Accelaration.X);
			case Actions.Left:
				return Controls.Go(E, E.ModelRotationY.Left, E.Accelaration.X);
			case Actions.Right:
				return Controls.Go(E, E.ModelRotationY.Right, E.Accelaration.X);
			case Actions.Up:
			{
				bool onGround = E.OnGround;
				if (onGround)
				{
					E.Velocity += 50f * E.ModelRotation.Up;
					E.FallingSpeed = 0f;
				}
				break;
			}
			case Actions.Up2:
			{
				bool onGround2 = E.OnGround;
				if (onGround2)
				{
					E.Velocity += 28f * E.ModelRotation.Up;
					E.FallingSpeed = 0f;
				}
				break;
			}
			case Actions.RotateClockwiseY:
			{
				Matrix Rot = Matrix.CreateFromAxisAngle(Vector3.Up, -0.1f);
				E.HeadRotation *= Rot;
				E.NeededBodyRotation *= Rot;
				E.NeededBodyRotationChanged = true;
				break;
			}
			case Actions.RotateAntiClockwiseY:
			{
				Matrix Rot2 = Matrix.CreateFromAxisAngle(Vector3.Up, 0.1f);
				E.HeadRotation *= Rot2;
				E.NeededBodyRotation *= Rot2;
				E.NeededBodyRotationChanged = true;
				break;
			}
			}
			return IsPathBlocked;
		}

		/// <summary>
		/// Returns IsPathBlocked
		/// </summary>
		// Token: 0x06000037 RID: 55 RVA: 0x00004408 File Offset: 0x00002608
		public static bool Go(Entity E, Vector3 Dir, float Accelaration)
		{
			bool IsPathBlocked = false;
			bool[] FBlocks = Ground.GetFacingIsAir(E.Position, Physics.Find2DDualDirectionsOfDirection(Dir), Dir);
			Direction MonoFacingDirection = Physics.Find2DUnitDirectionOfDirection(Dir);
			bool flag = Information.IsNothing(FBlocks);
			bool Go;
			if (flag)
			{
				Go = true;
			}
			else
			{
				bool flag2 = FBlocks[2];
				if (flag2)
				{
					bool flag3 = FBlocks[3];
					if (flag3)
					{
						bool flag4 = FBlocks[0];
						if (flag4)
						{
							bool flag5 = FBlocks[1];
							if (flag5)
							{
								bool flag6 = FBlocks[4];
								if (flag6)
								{
									E.Velocity += Accelaration * Dir;
									E.MovedFB = true;
								}
								else
								{
									bool flag7 = MonoFacingDirection == Direction.Left || MonoFacingDirection == Direction.Right;
									if (flag7)
									{
										E.Velocity += Accelaration * Dir * Vector3.Right;
										E.MovedFB = true;
									}
									else
									{
										E.Velocity += Accelaration * Dir * Vector3.Backward;
										E.MovedFB = true;
									}
								}
							}
							else
							{
								bool flag8 = MonoFacingDirection == Direction.Left || MonoFacingDirection == Direction.Right;
								if (flag8)
								{
									E.Velocity += Accelaration * Dir * Vector3.Right;
									E.MovedFB = true;
								}
								else
								{
									bool flag9 = MonoFacingDirection == Direction.Forward || MonoFacingDirection == Direction.Backward;
									if (flag9)
									{
										Controls.Go(E, Actions.Up2);
										E.MovedFB = true;
									}
								}
							}
						}
						else
						{
							bool flag10 = FBlocks[1];
							if (flag10)
							{
								bool flag11 = MonoFacingDirection == Direction.Left || MonoFacingDirection == Direction.Right;
								if (flag11)
								{
									Controls.Go(E, Actions.Up2);
									E.MovedFB = true;
								}
								else
								{
									bool flag12 = MonoFacingDirection == Direction.Forward || MonoFacingDirection == Direction.Backward;
									if (flag12)
									{
										E.Velocity += Accelaration * Dir * Vector3.Backward;
										E.MovedFB = true;
									}
								}
							}
							else
							{
								Controls.Go(E, Actions.Up2);
								E.MovedFB = true;
							}
						}
					}
					else
					{
						bool flag13 = FBlocks[2];
						if (flag13)
						{
							bool flag14 = FBlocks[0];
							if (flag14)
							{
								E.Velocity += Accelaration * Dir * Vector3.Right;
								E.MovedFB = true;
								IsPathBlocked = true;
							}
							else
							{
								Controls.Go(E, Actions.Up2);
								E.MovedFB = true;
							}
						}
					}
				}
				else
				{
					bool flag15 = FBlocks[3];
					if (flag15)
					{
						bool flag16 = FBlocks[3];
						if (flag16)
						{
							bool flag17 = FBlocks[1];
							if (flag17)
							{
								E.Velocity += Accelaration * Dir * Vector3.Backward;
								E.MovedFB = true;
								IsPathBlocked = true;
							}
							else
							{
								Controls.Go(E, Actions.Up2);
								E.MovedFB = true;
							}
						}
					}
				}
				Go = IsPathBlocked;
			}
			return Go;
		}

		// Token: 0x0400002C RID: 44
		public const Keys KeyUnAssigned = Keys.Pa1;

		// Token: 0x0400002D RID: 45
		public static Vector3 V3_0X = new Vector3(0f, 1f, 1f);

		// Token: 0x0400002E RID: 46
		public static Vector3 V3_0Y = new Vector3(1f, 0f, 1f);

		// Token: 0x0400002F RID: 47
		public static Vector3 V3_0Z = new Vector3(1f, 1f, 0f);

		// Token: 0x04000030 RID: 48
		public static Vector3 V3_1X = new Vector3(1f, 0f, 0f);

		// Token: 0x04000031 RID: 49
		public static Vector3 V3_1Y = new Vector3(0f, 1f, 0f);

		// Token: 0x04000032 RID: 50
		public static Vector3 V3_1Z = new Vector3(0f, 0f, 1f);

		// Token: 0x0200003C RID: 60
		public class Control
		{
			// Token: 0x17000043 RID: 67
			// (get) Token: 0x0600022E RID: 558 RVA: 0x0001DA65 File Offset: 0x0001BC65
			// (set) Token: 0x0600022F RID: 559 RVA: 0x0001DA6F File Offset: 0x0001BC6F
			public Actions Action { get; set; }

			// Token: 0x06000230 RID: 560 RVA: 0x0001DA78 File Offset: 0x0001BC78
			public Control(Keys KeyOfAction, Actions RelatedAction)
			{
				this.IsKeyControl = true;
				this.Key = KeyOfAction;
				this.Action = RelatedAction;
			}

			// Token: 0x06000231 RID: 561 RVA: 0x0001DA98 File Offset: 0x0001BC98
			public Control(Controls.Control.MouseKeys MouseKey, Actions RelatedAction)
			{
				this.IsKeyControl = true;
				this.MouseControl = MouseKey;
				this.IsKeyControl = false;
				this.Action = RelatedAction;
			}

			// Token: 0x0400027B RID: 635
			public Keys Key;

			// Token: 0x0400027C RID: 636
			public Controls.Control.MouseKeys MouseControl;

			// Token: 0x0400027D RID: 637
			public bool IsKeyControl;

			// Token: 0x02000052 RID: 82
			public enum MouseKeys
			{
				// Token: 0x040002AF RID: 687
				LeftClick,
				// Token: 0x040002B0 RID: 688
				RightClick,
				// Token: 0x040002B1 RID: 689
				WheelUp,
				// Token: 0x040002B2 RID: 690
				WheelDown,
				// Token: 0x040002B3 RID: 691
				WheelPress
			}
		}
	}
}
