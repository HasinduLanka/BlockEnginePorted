using System;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
	// Token: 0x02000026 RID: 38
	[StandardModule]
	public sealed class PhysicsFuncs
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00015FD4 File Offset: 0x000141D4
		public static void LookAtEntity(Entity H, Entity Target)
		{
			Matrix RotChange = Matrix.Invert(Matrix.CreateTranslation(H.Position) * Matrix.CreateLookAt(H.Position, Target.Position, H.ModelRotation.Up));
			Matrix RotChangeY = Matrix.Invert(Matrix.CreateTranslation(H.Position) * Matrix.CreateLookAt(new Vector3(H.Position.X, Target.Position.Y, H.Position.Z), Target.Position, H.ModelRotation.Up));
			H.HeadRotation = RotChange;
			H.NeededBodyRotation = RotChangeY;
			H.NeededBodyRotationChanged = true;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0001607C File Offset: 0x0001427C
		public static void LookAtPosition(Entity H, Vector3 Target)
		{
			Matrix RotChange = Matrix.Invert(Matrix.CreateTranslation(H.Position) * Matrix.CreateLookAt(H.Position, Target, H.ModelRotation.Up));
			Matrix RotChangeY = Matrix.Invert(Matrix.CreateTranslation(H.Position) * Matrix.CreateLookAt(new Vector3(H.Position.X, Target.Y, H.Position.Z), Target, H.ModelRotation.Up));
			H.HeadRotation = RotChange;
			H.NeededBodyRotation = RotChangeY;
			H.NeededBodyRotationChanged = true;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00016114 File Offset: 0x00014314
		public static void LookOutFromEntity(Entity H, Entity Target)
		{
			Matrix RotChange = Matrix.CreateTranslation(H.Position) * Matrix.CreateLookAt(H.Position, Target.Position, H.ModelRotation.Up);
			Matrix RotChangeY = Matrix.CreateTranslation(H.Position) * Matrix.CreateLookAt(new Vector3(H.Position.X, Target.Position.Y, H.Position.Z), Target.Position, H.ModelRotation.Up);
			H.HeadRotation = RotChange;
			H.NeededBodyRotation = RotChangeY;
			H.NeededBodyRotationChanged = true;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000161B4 File Offset: 0x000143B4
		public static Matrix CreateLookAtPosition(Vector3 Position, Vector3 Target, Vector3 Up)
		{
			return Matrix.Invert(Matrix.CreateTranslation(Position) * Matrix.CreateLookAt(Position, Target, Up));
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000161E0 File Offset: 0x000143E0
		public static Vector3 RNDVec3(float Max, float Min)
		{
			Vector3 RNDVec3 = new Vector3((float)checked(PhysicsFuncs.CRND.Next((int)Math.Round((double)Min), (int)Math.Round((double)Max))) + (float)PhysicsFuncs.CRND.NextDouble(), (float)checked(PhysicsFuncs.CRND.Next((int)Math.Round((double)Min), (int)Math.Round((double)Max))) + (float)PhysicsFuncs.CRND.NextDouble(), (float)checked(PhysicsFuncs.CRND.Next((int)Math.Round((double)Min), (int)Math.Round((double)Max))) + (float)PhysicsFuncs.CRND.NextDouble());
			return RNDVec3;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00016270 File Offset: 0x00014470
		public static Vector3 RNDVec3(float MaxX, float MinX, float MaxY, float MinY, float MaxZ, float MinZ)
		{
			Vector3 RNDVec3 = new Vector3((float)checked(PhysicsFuncs.CRND.Next((int)Math.Round((double)MinX), (int)Math.Round((double)MaxX))) + (float)PhysicsFuncs.CRND.NextDouble(), (float)checked(PhysicsFuncs.CRND.Next((int)Math.Round((double)MinY), (int)Math.Round((double)MaxY))) + (float)PhysicsFuncs.CRND.NextDouble(), (float)checked(PhysicsFuncs.CRND.Next((int)Math.Round((double)MinZ), (int)Math.Round((double)MaxZ))) + (float)PhysicsFuncs.CRND.NextDouble());
			return RNDVec3;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00016300 File Offset: 0x00014500
		public static Vector3 RNDIntVec3(float Max, float Min)
		{
			Vector3 RNDIntVec3 = checked(new Vector3((float)PhysicsFuncs.CRND.Next((int)Math.Round((double)Min), (int)Math.Round((double)(unchecked(Max + 1f)))), (float)PhysicsFuncs.CRND.Next((int)Math.Round((double)Min), (int)Math.Round((double)(unchecked(Max + 1f)))), (float)PhysicsFuncs.CRND.Next((int)Math.Round((double)Min), (int)Math.Round((double)(unchecked(Max + 1f))))));
			return RNDIntVec3;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0001637C File Offset: 0x0001457C
		public static T RandomOf<T>(T[] Choices)
		{
			return Choices[PhysicsFuncs.CRND.Next(0, Choices.Count<T>())];
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000163A8 File Offset: 0x000145A8
		public static IntVector3 DivideAndTruncVector3(IntVector3 V, float D)
		{
			IntVector3 DivideAndTruncVector3 = checked(new IntVector3((int)Conversion.Int((float)V.X / D), (int)Conversion.Int((float)V.Y / D), (int)Conversion.Int((float)V.Z / D)));
			return DivideAndTruncVector3;
		}

		// Token: 0x040001BC RID: 444
		public static Random CRND = new Random(Funcs.RND.Next());
	}
}
