using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
    // Token: 0x02000010 RID: 16
    public class ePart
    {
        // Token: 0x06000038 RID: 56 RVA: 0x000046F0 File Offset: 0x000028F0
        public ePart(string NName, Matrix RRotaion, Vector3 RRelativePosition, Entity PParent, int Index)
        {
            this.Name = "";
            this.Rotation = Matrix.Identity;
            this.Hurt = 0;
            this.Children = new List<ePart>();
            this.RelativePositionFromParent = Vector3.Zero;
            this.OriginalRotation = Matrix.Identity;
            this.DefualtRotation = Matrix.Identity;
            this.OriginalRelativePosition = Vector3.Zero;
            this.LockedeA = false;
            this.CurrentAnimations = new List<ePAnimation>();
            this.Name = NName;
            this.Rotation = RRotaion;
            this.RelativePosition = RRelativePosition;
            this.Parent = PParent;
            this.OriginalRotation = RRotaion;
            this.DefualtRotation = RRotaion;
            this.OriginalRelativePosition = RRelativePosition;
            this.Index = Index;
        }

        // Token: 0x06000039 RID: 57 RVA: 0x000047A8 File Offset: 0x000029A8
        public ePart(Entity PParent)
        {
            this.Name = "";
            this.Rotation = Matrix.Identity;
            this.Hurt = 0;
            this.Children = new List<ePart>();
            this.RelativePositionFromParent = Vector3.Zero;
            this.OriginalRotation = Matrix.Identity;
            this.DefualtRotation = Matrix.Identity;
            this.OriginalRelativePosition = Vector3.Zero;
            this.LockedeA = false;
            this.CurrentAnimations = new List<ePAnimation>();
            this.Parent = PParent;
        }

        // Token: 0x0600003A RID: 58 RVA: 0x0000482C File Offset: 0x00002A2C
        public void Hurten(float Damage)
        {
            bool flag = Damage < 0f;
            if (flag)
            {
                bool flag2 = this.Hurt > 0;
                if (flag2)
                {
                    bool flag3 = (float)this.Hurt + Damage > 0f;
                    if (flag3)
                    {
                        this.Hurt -= checked((byte)Math.Round((double)(unchecked(Damage * -1f))));
                    }
                    else
                    {
                        this.Hurt = 0;
                    }
                }
            }
            else
            {
                bool flag4 = this.Hurt < checked(byte.MaxValue - (byte)Math.Round((double)Damage));
                if (flag4)
                {
                    this.Hurt += checked((byte)Math.Round((double)Damage));
                }
                else
                {
                    this.Hurt = byte.MaxValue;
                }
            }
        }

        // Token: 0x0600003B RID: 59 RVA: 0x000048D8 File Offset: 0x00002AD8
        public void Rotate(Vector3 Axis, float Angle)
        {
            this.Rotation *= Matrix.CreateFromAxisAngle(Axis, Angle);
            bool flag = this.Children.Count > 0;
            if (flag)
            {

                foreach (ePart Ch in this.Children)
                {
                    Ch.Rotation *= Matrix.CreateFromAxisAngle(Axis, Angle * Ch.ChildRotationGradient);
                    Ch.RelativePosition = this.RelativePosition + Axis * Ch.RelativePositionFromParent * this.Length;
                }

            }
        }

        // Token: 0x0600003C RID: 60 RVA: 0x000049B0 File Offset: 0x00002BB0
        public void RotateAsAnimation(Vector3 Axis, float Angle)
        {
            this.Rotation *= Matrix.CreateFromAxisAngle(Axis, Angle);
            bool flag = this.Children.Count > 0;
            if (flag)
            {
                foreach (ePart Ch in this.Children)
                {
                    Ch.Rotation *= Matrix.CreateFromAxisAngle(Axis, Angle * Ch.eAChildRotationGradient);
                    Ch.RelativePosition = this.RelativePosition + Physics.GetAxisByChar(this.Rotation, this.ChildDirection, Ch.RelativePositionFromParent) * this.Length;
                }
            }
        }

        // Token: 0x0600003D RID: 61 RVA: 0x00004A94 File Offset: 0x00002C94
        public void RotateAsChild(Matrix RotationChange)
        {
            this.Rotation *= RotationChange;
            this.RelativePosition = Vector3.Transform(this.RelativePosition, RotationChange);
            bool flag = this.Children.Count > 0;
            if (flag)
            {

                foreach (ePart Ch in this.Children)
                {
                    Ch.Rotation *= RotationChange;
                    Ch.RelativePosition = this.RelativePosition + Physics.GetAxisByChar(this.Rotation, this.ChildDirection, Ch.RelativePositionFromParent) * this.Length;
                }

            }
        }

        // Token: 0x0600003E RID: 62 RVA: 0x00004B78 File Offset: 0x00002D78
        public void RotateAsChild(Vector3 Axis, float Angle)
        {
            Matrix RotationCHange = Matrix.CreateFromAxisAngle(Axis, Angle);
            this.Rotation *= RotationCHange;
            this.RelativePosition = Vector3.Transform(this.RelativePosition, RotationCHange);
            bool flag = this.Children.Count > 0;
            if (flag)
            {
                foreach (ePart Ch in this.Children)
                {
                    Ch.Rotation *= Matrix.CreateFromAxisAngle(Axis, Angle * Ch.ChildRotationGradient);
                    Ch.RelativePosition = this.RelativePosition + Physics.GetAxisByChar(this.Rotation, this.ChildDirection, Ch.RelativePositionFromParent) * this.Length;
                }
            }
        }

        // Token: 0x0600003F RID: 63 RVA: 0x00004C74 File Offset: 0x00002E74
        public void Rotate(Matrix RotationChange)
        {
            this.Rotation *= RotationChange;
            bool flag = this.Children.Count > 0;
            if (flag)
            {
                    foreach (ePart Ch in this.Children)
                    {
                        Ch.Rotation *= RotationChange;
                        Vector3 ChildDirectionM = Physics.GetAxisByChar(this.Rotation, this.ChildDirection, Ch.RelativePositionFromParent) * Ch.RelativePositionFromParent;
                        Ch.RelativePosition = this.RelativePosition + ChildDirectionM * this.Length;
                    }
            }
        }

        // Token: 0x06000040 RID: 64 RVA: 0x00004D54 File Offset: 0x00002F54
        public void Revert()
        {
            this.Rotation = this.OriginalRotation;
        }

        // Token: 0x04000048 RID: 72
        public string Name;

        // Token: 0x04000049 RID: 73
        public Matrix Rotation;

        // Token: 0x0400004A RID: 74
        public Vector3 RelativePosition;

        // Token: 0x0400004B RID: 75
        public Entity Parent;

        // Token: 0x0400004C RID: 76
        public Human Charter;

        // Token: 0x0400004D RID: 77
        public byte Hurt;

        // Token: 0x0400004E RID: 78
        public int Index;

        // Token: 0x0400004F RID: 79
        public List<ePart> Children;

        // Token: 0x04000050 RID: 80
        public float Length;

        // Token: 0x04000051 RID: 81
        public char ChildDirection;

        // Token: 0x04000052 RID: 82
        public float eAChildRotationGradient;

        // Token: 0x04000053 RID: 83
        public float ChildRotationGradient;

        // Token: 0x04000054 RID: 84
        public Vector3 RelativePositionFromParent;

        // Token: 0x04000055 RID: 85
        public Matrix OriginalRotation;

        // Token: 0x04000056 RID: 86
        public Matrix DefualtRotation;

        // Token: 0x04000057 RID: 87
        public Vector3 OriginalRelativePosition;

        // Token: 0x04000058 RID: 88
        public bool LockedeA;

        // Token: 0x04000059 RID: 89
        public List<ePAnimation> CurrentAnimations;
    }
}
