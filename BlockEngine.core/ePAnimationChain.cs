using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.VisualBasic;

namespace BlockEngine
{
    // Token: 0x02000011 RID: 17
    public class ePAnimationChain
    {
        // Token: 0x06000041 RID: 65 RVA: 0x00004D64 File Offset: 0x00002F64
        public void Next_eA()
        {
            this.CurrentAnimation.Pause();
            bool flag = !Information.IsNothing(this.CurrentAnimation.Parellel_eAs);
            if (flag)
            {


                foreach (ePAnimation eA in this.CurrentAnimation.Parellel_eAs)
                {
                    eA.Pause();
                    bool flag2 = eA.CAngle != eA.DAngle;
                    if (flag2)
                    {
                        eA.eP.RotateAsAnimation(eA.Axis, eA.DAngle - eA.CAngle);
                        eA.CAngle = eA.DAngle;
                    }
                }


            }
            bool flag3;
            checked
            {
                this.iCurrentAnimation++;
                flag3 = (this.iCurrentAnimation == this.AnimationList.Count);
            }
            if (flag3)
            {
                bool loopable = this.Loopable;
                if (loopable)
                {
                    this.iCurrentAnimation = 0;
                    this.CurrentAnimation = this.AnimationList[this.iCurrentAnimation];
                    this.CurrentAnimation.RResume();
                    bool flag4 = !Information.IsNothing(this.CurrentAnimation.Parellel_eAs);
                    if (flag4)
                    {


                        foreach (ePAnimation eA2 in this.CurrentAnimation.Parellel_eAs)
                        {
                            eA2.RResume();
                        }


                    }
                }
                else
                {


                    foreach (ePAnimation eA3 in this.AnimationList)
                    {
                        bool flag5 = !Information.IsNothing(eA3.Parellel_eAs);
                        if (flag5)
                        {


                            foreach (ePAnimation eA4 in eA3.Parellel_eAs)
                            {
                                bool flag6 = eA4.CAngle != eA4.DAngle;
                                if (flag6)
                                {
                                    eA4.eP.RotateAsAnimation(eA4.Axis, eA4.DAngle - eA4.CAngle);
                                    eA4.CAngle = eA4.DAngle;
                                }
                                eA4.Destroy();
                            }


                        }
                        bool flag7 = eA3.CAngle != eA3.DAngle;
                        if (flag7)
                        {
                            eA3.eP.RotateAsAnimation(eA3.Axis, eA3.DAngle - eA3.CAngle);
                            eA3.CAngle = eA3.DAngle;
                        }
                        eA3.Destroy();
                    }


                    this.iCurrentAnimation = 0;
                    bool flag8 = !Information.IsNothing(this.Reset_eAC_After_eA);
                    if (flag8)
                    {


                        foreach (ePAnimation eAA in this.Reset_eAC_After_eA.AnimationList)
                        {
                            bool flag9 = !Information.IsNothing(eAA.Parellel_eAs);
                            if (flag9)
                            {


                                foreach (ePAnimation eAP in eAA.Parellel_eAs)
                                {
                                    eAP.Reset();
                                }



                            }


                            this.Reset_eAC_After_eA.Reset();
                        }
                        bool flag10 = !Information.IsNothing(this.Reset_LockedeA_After_eA);
                        if (flag10)
                        {


                            foreach (ePart eA5 in this.Reset_LockedeA_After_eA)
                            {
                                eA5.LockedeA = false;
                            }


                        }
                        bool flag11 = !Information.IsNothing(this.Revert_After_eA);
                        if (flag11)
                        {


                            foreach (ePart eA6 in this.Revert_After_eA)
                            {
                                eA6.Revert();
                            }


                        }
                        bool flag12 = !Information.IsNothing(this.Reset_N_Start_eAC_After_eA);
                        if (flag12)
                        {
                            this.Reset_N_Start_eAC_After_eA.Reset();
                            this.Reset_N_Start_eAC_After_eA.Start();
                        }
                        ePAnimationChain.CompletedEventHandler completedEvent = this.CompletedEvent;
                        if (completedEvent != null)
                        {
                            completedEvent(this, new EventArgs());
                        }
                        this.Done = true;
                    }
                }
            }
            else
            {
                this.CurrentAnimation = this.AnimationList[this.iCurrentAnimation];
                this.CurrentAnimation.RResume();
                bool flag13 = !Information.IsNothing(this.CurrentAnimation.Parellel_eAs);
                if (flag13)
                {

                    foreach (ePAnimation eA7 in this.CurrentAnimation.Parellel_eAs)
                    {
                        eA7.RResume();
                    }
                }
            }

        }

        // Token: 0x06000042 RID: 66 RVA: 0x00005350 File Offset: 0x00003550
        public ePAnimationChain()
        {
            this.iCurrentAnimation = 0;
            this.AnimationList = new List<ePAnimation>();
            this.Playing = false;
            this.Done = false;
            this.Reset_LockedeA_After_eA = new List<ePart>();
            this.Revert_After_eA = new List<ePart>();
        }

        // Token: 0x06000043 RID: 67 RVA: 0x00005390 File Offset: 0x00003590
        public ePAnimationChain(List<ePAnimation> AAnimationList, bool LLoopable)
        {
            this.iCurrentAnimation = 0;
            this.AnimationList = new List<ePAnimation>();
            this.Playing = false;
            this.Done = false;
            this.Reset_LockedeA_After_eA = new List<ePart>();
            this.Revert_After_eA = new List<ePart>();
            this.AnimationList.AddRange(AAnimationList);


            foreach (ePAnimation eA in this.AnimationList)
            {
                eA.ePAnimationChain1 = this;
            }


            this.Loopable = LLoopable;
            this.CurrentAnimation = this.AnimationList[0];
            this.Done = false;
        }

        // Token: 0x06000044 RID: 68 RVA: 0x00005454 File Offset: 0x00003654
        public void Reset()
        {
            this.CurrentAnimation = this.AnimationList[0];
            this.Done = false;


            foreach (ePAnimation eA in this.AnimationList)
            {
                eA.Reset();
                bool flag = !Information.IsNothing(eA.Parellel_eAs);
                if (flag)
                {

                    foreach (ePAnimation eA2 in eA.Parellel_eAs)
                    {
                        eA2.Reset();
                    }


                }
            }


        }

        // Token: 0x06000045 RID: 69 RVA: 0x00005528 File Offset: 0x00003728
        public void Start()
        {
            this.CurrentAnimation.RResume();
            this.Playing = true;
            bool flag = !Information.IsNothing(this.CurrentAnimation.Parellel_eAs);
            if (flag)
            {


                foreach (ePAnimation eA in this.CurrentAnimation.Parellel_eAs)
                {
                    eA.RResume();
                }


            }
        }

        // Token: 0x06000046 RID: 70 RVA: 0x000055B4 File Offset: 0x000037B4
        public void Pause()
        {
            bool playing = this.Playing;
            if (playing)
            {
                this.CurrentAnimation.Pause();
                this.Playing = false;
                bool flag = !Information.IsNothing(this.CurrentAnimation.Parellel_eAs);
                if (flag)
                {

                    foreach (ePAnimation eA in this.CurrentAnimation.Parellel_eAs)
                    {
                        eA.Pause();
                    }
                }
            }
        }

        // Token: 0x06000047 RID: 71 RVA: 0x0000564C File Offset: 0x0000384C
        public void RResume()
        {
            bool flag = !this.Playing;
            if (flag)
            {
                this.CurrentAnimation.RResume();
                this.Playing = true;
                bool flag2 = !Information.IsNothing(this.CurrentAnimation.Parellel_eAs);
                if (flag2)
                {

                    foreach (ePAnimation eA in this.CurrentAnimation.Parellel_eAs)
                    {
                        eA.RResume();
                    }
                }
            }
        }

        // Token: 0x14000001 RID: 1
        // (add) Token: 0x06000048 RID: 72 RVA: 0x000056E8 File Offset: 0x000038E8
        // (remove) Token: 0x06000049 RID: 73 RVA: 0x00005720 File Offset: 0x00003920
        public event ePAnimationChain.CompletedEventHandler Completed
        {
            [CompilerGenerated]
            add
            {
                ePAnimationChain.CompletedEventHandler completedEventHandler = this.CompletedEvent;
                ePAnimationChain.CompletedEventHandler completedEventHandler2;
                do
                {
                    completedEventHandler2 = completedEventHandler;
                    ePAnimationChain.CompletedEventHandler value2 = (ePAnimationChain.CompletedEventHandler)Delegate.Combine(completedEventHandler2, value);
                    completedEventHandler = Interlocked.CompareExchange<ePAnimationChain.CompletedEventHandler>(ref this.CompletedEvent, value2, completedEventHandler2);
                }
                while (completedEventHandler != completedEventHandler2);
            }
            [CompilerGenerated]
            remove
            {
                ePAnimationChain.CompletedEventHandler completedEventHandler = this.CompletedEvent;
                ePAnimationChain.CompletedEventHandler completedEventHandler2;
                do
                {
                    completedEventHandler2 = completedEventHandler;
                    ePAnimationChain.CompletedEventHandler value2 = (ePAnimationChain.CompletedEventHandler)Delegate.Remove(completedEventHandler2, value);
                    completedEventHandler = Interlocked.CompareExchange<ePAnimationChain.CompletedEventHandler>(ref this.CompletedEvent, value2, completedEventHandler2);
                }
                while (completedEventHandler != completedEventHandler2);
            }
        }

        // Token: 0x0600004A RID: 74 RVA: 0x00005758 File Offset: 0x00003958
        public ePAnimationChain Clone(ePart eeP)
        {
            ePAnimationChain C = new ePAnimationChain();


            foreach (ePAnimation eA in this.AnimationList)
            {
                ePAnimation NeweA = eA.Clone(eeP);
                NeweA.ePAnimationChain1 = this;
                NeweA.Axis.Owner_ePart = eeP;
                NeweA.ChildRotationGradient = eA.ChildRotationGradient;


                foreach (ePAnimation PeA in NeweA.Parellel_eAs)
                {
                    PeA.ePAnimationChain1 = this;
                    PeA.Axis.Owner_ePart = eeP;
                }


                C.AnimationList.Add(NeweA);
            }


            C.Loopable = this.Loopable;
            C.iCurrentAnimation = 0;
            C.CurrentAnimation = C.AnimationList[0];
            return C;
        }

        // Token: 0x0400005A RID: 90
        public ePAnimation CurrentAnimation;

        // Token: 0x0400005B RID: 91
        public int iCurrentAnimation;

        // Token: 0x0400005C RID: 92
        public List<ePAnimation> AnimationList;

        // Token: 0x0400005D RID: 93
        public bool Loopable;

        // Token: 0x0400005E RID: 94
        public bool Playing;

        // Token: 0x0400005F RID: 95
        public bool Done;

        // Token: 0x04000060 RID: 96
        public string Name;

        // Token: 0x04000061 RID: 97
        public ePAnimationChain Reset_eAC_After_eA;

        // Token: 0x04000062 RID: 98
        public List<ePart> Reset_LockedeA_After_eA;

        // Token: 0x04000063 RID: 99
        public List<ePart> Revert_After_eA;

        // Token: 0x04000064 RID: 100
        public ePAnimationChain Reset_N_Start_eAC_After_eA;

        // Token: 0x04000065 RID: 101
        [CompilerGenerated]
        private ePAnimationChain.CompletedEventHandler CompletedEvent;

        // Token: 0x0200003D RID: 61
        // (Invoke) Token: 0x06000235 RID: 565
        public delegate void CompletedEventHandler(ePAnimationChain sender, EventArgs e);
    }
}
