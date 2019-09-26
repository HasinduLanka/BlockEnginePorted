using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
    // Token: 0x02000012 RID: 18
    public class ePAnimation
    {
        // Token: 0x0600004C RID: 76 RVA: 0x00005880 File Offset: 0x00003A80
        public void Animate()
        {

            foreach (ePart Child in this.eP.Children)
            {
                Child.eAChildRotationGradient = this.ChildRotationGradient;
            }

            bool Completed = false;
            bool flag = this.DAngle >= 0f;
            if (flag)
            {
                bool flag2 = (double)Math.Abs(this.DAngle - this.CAngle) < 0.001;
                if (flag2)
                {
                    Completed = true;
                }
                bool flag3 = this.Speed > 0f;
                if (flag3)
                {
                    bool flag4 = this.CAngle > this.DAngle;
                    if (flag4)
                    {
                        Completed = true;
                    }
                }
                else
                {
                    bool flag5 = this.CAngle < this.DAngle;
                    if (flag5)
                    {
                        Completed = true;
                    }
                }
            }
            else
            {
                bool flag6 = this.DAngle < 0f;
                if (flag6)
                {
                    bool flag7 = (double)Math.Abs(this.CAngle - this.DAngle) < 0.001;
                    if (flag7)
                    {
                        Completed = true;
                    }
                    bool flag8 = this.Speed < 0f;
                    if (flag8)
                    {
                        bool flag9 = this.CAngle < this.DAngle;
                        if (flag9)
                        {
                            Completed = true;
                        }
                    }
                    else
                    {
                        bool flag10 = this.CAngle > this.DAngle;
                        if (flag10)
                        {
                            Completed = true;
                        }
                    }
                }
            }
            bool flag11 = Completed;
            if (flag11)
            {
                this.eP.RotateAsAnimation(this.Axis, this.DAngle - this.CAngle);
                bool isRefiningEdges = this.IsRefiningEdges;
                if (isRefiningEdges)
                {
                    this.eP.Rotation = this.eP.Parent.ModelRotation * Matrix.CreateFromAxisAngle(this.Axis, this.DAngle);
                }
                this.CAngle = this.DAngle;
                bool flag12 = this.CDAngleI == checked(this.DAngleLst.Count - 1);
                if (flag12)
                {
                    bool looping = this.Looping;
                    if (looping)
                    {
                        this.CDAngleI = 0;
                        this.DAngle = this.DAngleLst[this.CDAngleI];
                        this.Time = this.TimeLst[this.CDAngleI];
                        this.Speed = (this.DAngle - this.CAngle) / this.Time;
                        bool flag13 = !Information.IsNothing(this.ePAnimationChain1);
                        if (flag13)
                        {
                            this.ePAnimationChain1.Next_eA();
                        }
                    }
                    else
                    {
                        bool flag14 = !Information.IsNothing(this.ePAnimationChain1);
                        if (flag14)
                        {
                            this.ePAnimationChain1.Next_eA();
                        }
                        this.Pause();
                    }
                }
                else
                {
                    bool flag15 = !this.GotTrack;
                    if (flag15)
                    {
                        this.GotTrack = true;
                    }
                    else
                    {
                        checked
                        {
                            this.CDAngleI++;
                            this.DAngle = this.DAngleLst[this.CDAngleI];
                            this.Time = this.TimeLst[this.CDAngleI];
                        }
                        this.Speed = (this.DAngle - this.CAngle) / this.Time;
                    }
                }
            }
            bool flag16 = this.DAngle > this.CAngle;
            if (flag16)
            {
                bool getingTargetLstDir = this.GetingTargetLstDir;
                if (getingTargetLstDir)
                {
                    this.GetingTargetLstDir = false;
                }
                else
                {
                    this.GetingTargetLstDir = true;
                }
            }
            bool flag17 = !this.GetingTargetLstDir;
            if (flag17)
            {
                this.eP.RotateAsAnimation(this.Axis, this.Speed);
                this.CAngle += this.Speed;
                this.TotalCAngleChange += this.Speed;
            }
            else
            {
                this.eP.RotateAsAnimation(this.Axis, this.Speed);
                this.CAngle += this.Speed;
                this.TotalCAngleChange += this.Speed;
            }
        }

        // Token: 0x0600004D RID: 77 RVA: 0x00005C88 File Offset: 0x00003E88
        public void Pause()
        {
            bool playing = this.Playing;
            if (playing)
            {
                ePAnimation.LstAnimation.Remove(this);
                this.Playing = false;
            }
        }

        // Token: 0x0600004E RID: 78 RVA: 0x00005CB8 File Offset: 0x00003EB8
        public void RResume()
        {
            bool flag = !this.Playing;
            if (flag)
            {
                ePAnimation.LstAnimation.Add(this);
                this.Playing = true;
            }
        }

        // Token: 0x0600004F RID: 79 RVA: 0x00005CE8 File Offset: 0x00003EE8
        public void Destroy()
        {
            ePAnimation.LstAnimation.Remove(this);
            this.eP.CurrentAnimations.Remove(this);
        }

        // Token: 0x06000050 RID: 80 RVA: 0x00005D0C File Offset: 0x00003F0C
        public ePAnimation()
        {
            this.CAngle = 0f;
            this.Time = 0.04f;
            this.GotTrack = false;
            this.DAngleLst = new List<float>();
            this.TimeLst = new List<float>();
            this.CDAngleI = 0;
            this.Looping = false;
            this.IsRefiningEdges = false;
            this.GetingTargetLstDir = false;
            this.Parellel_eAs = new List<ePAnimation>();
            this.IsFakeTimer = false;
            this.Playing = false;
        }

        // Token: 0x06000051 RID: 81 RVA: 0x00005D8C File Offset: 0x00003F8C
        public ePAnimation(ePart eeP, Vector3 DDPos, float[] DDAngles, bool LLooping, float[] TTimes, Axis AAxis)
        {
            this.CAngle = 0f;
            this.Time = 0.04f;
            this.GotTrack = false;
            this.DAngleLst = new List<float>();
            this.TimeLst = new List<float>();
            this.CDAngleI = 0;
            this.Looping = false;
            this.IsRefiningEdges = false;
            this.GetingTargetLstDir = false;
            this.Parellel_eAs = new List<ePAnimation>();
            this.IsFakeTimer = false;
            this.Playing = false;
            this.CAngle = 0f;
            this.CDAngleI = 0;
            this.eP = eeP;
            this.DPos = DDPos;
            foreach (float DDAngle in DDAngles)
            {
                this.DAngleLst.Add(DDAngle);
            }
            this.DAngle = this.DAngleLst[0];
            this.Looping = LLooping;
            foreach (float TTime in TTimes)
            {
                this.TimeLst.Add(TTime);
            }
            this.Time = this.TimeLst[0];
            this.Axis = AAxis;
            this.eP.CurrentAnimations.Add(this);
            this.Speed = this.DAngle / this.Time;
        }

        // Token: 0x06000052 RID: 82 RVA: 0x00005EE0 File Offset: 0x000040E0
        public ePAnimation(ePart eeP, Vector3 DDPos, float[] DDAngles, bool LLooping, float[] TTimes, Axis AAxis, float ChildRotationGradient, bool RefineAnimationEdges)
        {
            this.CAngle = 0f;
            this.Time = 0.04f;
            this.GotTrack = false;
            this.DAngleLst = new List<float>();
            this.TimeLst = new List<float>();
            this.CDAngleI = 0;
            this.Looping = false;
            this.IsRefiningEdges = false;
            this.GetingTargetLstDir = false;
            this.Parellel_eAs = new List<ePAnimation>();
            this.IsFakeTimer = false;
            this.Playing = false;
            this.CAngle = 0f;
            this.CDAngleI = 0;
            this.eP = eeP;
            this.DPos = DDPos;
            this.ChildRotationGradient = ChildRotationGradient;
            this.IsRefiningEdges = RefineAnimationEdges;
            foreach (float DDAngle in DDAngles)
            {
                this.DAngleLst.Add(DDAngle);
            }
            this.DAngle = this.DAngleLst[0];
            this.Looping = LLooping;
            foreach (float TTime in TTimes)
            {
                this.TimeLst.Add(TTime);
            }
            this.Time = this.TimeLst[0];
            this.Axis = AAxis;
            this.eP.CurrentAnimations.Add(this);
            this.Speed = this.DAngle / this.Time;
        }

        // Token: 0x06000053 RID: 83 RVA: 0x00006044 File Offset: 0x00004244
        public ePAnimation(ePart eeP, Vector3 DDPos, float[] DDAngles, bool LLooping, float[] TTimes, Axis AAxis, float ChildRotationGradient)
        {
            this.CAngle = 0f;
            this.Time = 0.04f;
            this.GotTrack = false;
            this.DAngleLst = new List<float>();
            this.TimeLst = new List<float>();
            this.CDAngleI = 0;
            this.Looping = false;
            this.IsRefiningEdges = false;
            this.GetingTargetLstDir = false;
            this.Parellel_eAs = new List<ePAnimation>();
            this.IsFakeTimer = false;
            this.Playing = false;
            this.CAngle = 0f;
            this.CDAngleI = 0;
            this.eP = eeP;
            this.DPos = DDPos;
            this.ChildRotationGradient = ChildRotationGradient;
            foreach (float DDAngle in DDAngles)
            {
                this.DAngleLst.Add(DDAngle);
            }
            this.DAngle = this.DAngleLst[0];
            this.Looping = LLooping;
            foreach (float TTime in TTimes)
            {
                this.TimeLst.Add(TTime);
            }
            this.Time = this.TimeLst[0];
            this.Axis = AAxis;
            this.eP.CurrentAnimations.Add(this);
            this.Speed = this.DAngle / this.Time;
        }

        // Token: 0x06000054 RID: 84 RVA: 0x000061A0 File Offset: 0x000043A0
        public void Reset()
        {
            this.Pause();
            this.CAngle = 0f;
            this.CDAngleI = 0;
            this.DAngle = this.DAngleLst[0];
            this.Time = this.TimeLst[0];
            this.Speed = this.DAngle / this.Time;
        }

        // Token: 0x06000055 RID: 85 RVA: 0x000061FE File Offset: 0x000043FE
        public void AddStep(float DDAngle, float TTime)
        {
            this.DAngleLst.Add(DDAngle);
            this.TimeLst.Add(TTime);
        }

        // Token: 0x06000056 RID: 86 RVA: 0x0000621C File Offset: 0x0000441C
        public ePAnimation Clone(ePart eeP)
        {
            bool isFakeTimer = this.IsFakeTimer;
            ePAnimation o;
            if (isFakeTimer)
            {
                o = new ePAnimation(new ePart(new Entity(EntityTypes.WayPoint)), this.DPos, this.DAngleLst.ToArray(), this.Looping, this.TimeLst.ToArray(), this.Axis);
            }
            else
            {
                o = new ePAnimation(eeP, this.DPos, this.DAngleLst.ToArray(), this.Looping, this.TimeLst.ToArray(), this.Axis);
            }
                foreach (ePAnimation Ani in this.Parellel_eAs)
                {
                    ePAnimation ClA = Ani.Clone(eeP);
                    ClA.ChildRotationGradient = Ani.ChildRotationGradient;
                    o.Parellel_eAs.Add(ClA);
                }
            return o;
        }

        // Token: 0x04000066 RID: 102
        public static List<ePAnimation> LstAnimation = new List<ePAnimation>();

        // Token: 0x04000067 RID: 103
        public ePart eP;

        // Token: 0x04000068 RID: 104
        public Vector3 DPos;

        // Token: 0x04000069 RID: 105
        public float DAngle;

        // Token: 0x0400006A RID: 106
        public float CAngle;

        // Token: 0x0400006B RID: 107
        public float Time;

        // Token: 0x0400006C RID: 108
        public float Speed;

        // Token: 0x0400006D RID: 109
        public Axis Axis;

        // Token: 0x0400006E RID: 110
        public string Name;

        // Token: 0x0400006F RID: 111
        public float ChildRotationGradient;

        // Token: 0x04000070 RID: 112
        public bool GotTrack;

        // Token: 0x04000071 RID: 113
        public List<float> DAngleLst;

        // Token: 0x04000072 RID: 114
        public List<float> TimeLst;

        // Token: 0x04000073 RID: 115
        public int CDAngleI;

        // Token: 0x04000074 RID: 116
        public bool Looping;

        // Token: 0x04000075 RID: 117
        public bool IsRefiningEdges;

        // Token: 0x04000076 RID: 118
        public bool GetingTargetLstDir;

        // Token: 0x04000077 RID: 119
        public ePAnimationChain ePAnimationChain1;

        // Token: 0x04000078 RID: 120
        public List<ePAnimation> Parellel_eAs;

        // Token: 0x04000079 RID: 121
        public bool IsFakeTimer;

        // Token: 0x0400007A RID: 122
        public bool Playing;

        // Token: 0x0400007B RID: 123
        public float TotalCAngleChange;
    }
}
