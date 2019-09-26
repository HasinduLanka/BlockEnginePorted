using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockEngine
{
    // Token: 0x02000032 RID: 50
    public class Tool : ePart
    {
        // Token: 0x06000202 RID: 514 RVA: 0x0001C050 File Offset: 0x0001A250
        public static Tool Buy(int ToolI)
        {
            return new Tool
            {
                Model = Tool.Tools[ToolI].Model,
                Transforms = Tool.Tools[ToolI].Transforms,
                Name = Tool.Tools[ToolI].Name,
                Rotation = Tool.Tools[ToolI].Rotation,
                RelativePosition = Tool.Tools[ToolI].RelativePosition,
                OriginalRotation = Tool.Tools[ToolI].OriginalRotation,
                DefualtRotation = Tool.Tools[ToolI].DefualtRotation,
                Type1 = Tool.Tools[ToolI].Type1,
                Length = Tool.Tools[ToolI].Length,
                RewardsToVictim = Tool.Tools[ToolI].RewardsToVictim,
                MaxAttackingDistance = Tool.Tools[ToolI].MaxAttackingDistance,
                MinAttackingDistance = Tool.Tools[ToolI].MinAttackingDistance
            };
        }

        // Token: 0x06000203 RID: 515 RVA: 0x0001C174 File Offset: 0x0001A374
        public void Use(Tool.Action A)
        {
            bool flag = !this.Attacking;
            if (flag)
            {
                bool flag2 = !this.Owner.LockedeA;
                if (flag2)
                {
                    this.Owner.LockedeA = true;
                    this.Attacking = true;
                    this.CAnimation = Tool.AnimationLst[A].Clone(this.Owner);
                    this.CAnimation.Name = "PA1";
                    this.CAnimation.Reset_LockedeA_After_eA.Add(this.Owner);
                    this.CAnimation.Revert_After_eA.AddRange(new ePart[]
                    {
                        this.Owner,
                        this
                    });
                    this.CAnimation.Completed += this.AttackCompleted;

                    foreach (ePAnimation eA in this.CAnimation.AnimationList)
                    {
                        eA.ePAnimationChain1 = this.CAnimation;

                        foreach (ePAnimation PeA in eA.Parellel_eAs)
                        {
                            PeA.ePAnimationChain1 = this.CAnimation;
                        }

                    }

                    this.CAnimation.Start();
                }
            }
        }

        // Token: 0x06000204 RID: 516 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
        public void Update()
        {
            this.R = new Ray(this.RelativePosition + this.Parent.Position, this.Rotation.Up);
        }

        // Token: 0x06000205 RID: 517 RVA: 0x0001C320 File Offset: 0x0001A520
        public void AttackCompleted(ePAnimationChain Sender, EventArgs e)
        {
            this.eAChildRotationGradient = 0f;
            this.Owner.ChildRotationGradient = 0f;
            this.Attacking = false;
            this.Charter.StandStraitInstantly();
            this.Charter.PuaseWalking();
            this.Charter.ResumeWalking();
            this.CAnimation.Completed -= this.AttackCompleted;
        }

        // Token: 0x06000206 RID: 518 RVA: 0x0001C38C File Offset: 0x0001A58C
        public static void InitializeAnimations()
        {
            ePart eeP = new ePart(new Entity(EntityTypes.Human1));
            Vector3 ddpos = default;
            float[] array = new float[2];
            array[0] = -2f;
            ePAnimation FeA = new ePAnimation(eeP, ddpos, array, false, new float[]
            {
                15f,
                10f
            }, new Axis("L".ToCharArray().First<char>(), false, new ePart(new Entity(EntityTypes.Human1))), 1.05f)
            {
                IsFakeTimer = true
            };
            ePAnimation eA = new ePAnimation(new ePart(new Entity(EntityTypes.Human1)), default, new float[]
            {
                -1.5f,
                -0.5f
            }, false, new float[]
            {
                15f,
                10f
            }, new Axis("L".ToCharArray().First<char>(), false, new ePart(new Entity(EntityTypes.Human1))), 1.05f);
            ePAnimation eA2 = new ePAnimation(new ePart(new Entity(EntityTypes.Human1)), default, new float[]
            {
                -0.2f,
                0.5f
            }, false, new float[]
            {
                15f,
                10f
            }, new Axis("U".ToCharArray().First<char>()), 1f);
            ePAnimation eAS = new ePAnimation(new ePart(new Entity(EntityTypes.Human1)), default, new float[]
            {
                0.1f,
                1f
            }, false, new float[]
            {
                15f,
                10f
            }, new Axis("L".ToCharArray().First<char>(), false, new ePart(new Entity(EntityTypes.Human1))), 2f);
            FeA.Parellel_eAs.AddRange(new ePAnimation[]
            {
                eA,
                eA2,
                eAS
            });
            ePAnimationChain CAnimation = new ePAnimationChain(new List<ePAnimation>
            {
                FeA
            }, false);
            Tool.AnimationLst.Add(Tool.Action.PrimaryAttack1, CAnimation);
            ePart eeP2 = new ePart(new Entity(EntityTypes.Human1));
            Vector3 ddpos2 = default;
            float[] array2 = new float[2];
            array2[0] = -2f;
            ePAnimation FeA2 = new ePAnimation(eeP2, ddpos2, array2, false, new float[]
            {
                20f,
                10f
            }, new Axis("L".ToCharArray().First<char>(), false, new ePart(new Entity(EntityTypes.Human1))), 0.5f)
            {
                IsFakeTimer = true
            };
            ePAnimation eA3 = new ePAnimation(new ePart(new Entity(EntityTypes.Human1)), default, new float[]
            {
                -2f,
                1f
            }, false, new float[]
            {
                20f,
                10f
            }, new Axis("L".ToCharArray().First<char>(), false, new ePart(new Entity(EntityTypes.Human1))), 1f);
            ePAnimation eA4 = new ePAnimation(new ePart(new Entity(EntityTypes.Human1)), default, new float[]
            {
                -0.4f,
                0.5f
            }, false, new float[]
            {
                5f,
                25f
            }, new Axis("U".ToCharArray().First<char>()), 1f);
            FeA2.Parellel_eAs.AddRange(new ePAnimation[]
            {
                eA3,
                eA4
            });
            ePAnimationChain CAnimation2 = new ePAnimationChain(new List<ePAnimation>
            {
                FeA2
            }, false);
            Tool.AnimationLst.Add(Tool.Action.PrimaryAttack2, CAnimation2);
            ePart eeP3 = new ePart(new Entity(EntityTypes.Human1));
            Vector3 ddpos3 = default;
            float[] array3 = new float[3];
            array3[0] = 0.2f;
            array3[1] = -0.8f;
            ePAnimation FeA3 = new ePAnimation(eeP3, ddpos3, array3, false, new float[]
            {
                5f,
                10f,
                10f
            }, new Axis("L".ToCharArray().First<char>(), false, new ePart(new Entity(EntityTypes.Human1))), -1.8f)
            {
                IsFakeTimer = true
            };
            ePart eeP4 = new ePart(new Entity(EntityTypes.Human1));
            Vector3 ddpos4 = default;
            float[] array4 = new float[3];
            array4[0] = 0.2f;
            array4[1] = -0.5f;
            ePAnimation eA5 = new ePAnimation(eeP4, ddpos4, array4, false, new float[]
            {
                5f,
                10f,
                10f
            }, new Axis("L".ToCharArray().First<char>(), false, new ePart(new Entity(EntityTypes.Human1))), -1.5f);
            ePart eeP5 = new ePart(new Entity(EntityTypes.Human1));
            Vector3 ddpos5 = default;
            float[] array5 = new float[3];
            array5[0] = 0.2f;
            array5[1] = 0.4f;
            ePAnimation eA6 = new ePAnimation(eeP5, ddpos5, array5, false, new float[]
            {
                5f,
                10f,
                10f
            }, new Axis("U".ToCharArray().First<char>()), 1f);
            FeA3.Parellel_eAs.AddRange(new ePAnimation[]
            {
                eA5,
                eA6
            });
            ePAnimationChain CAnimation3 = new ePAnimationChain(new List<ePAnimation>
            {
                FeA3
            }, false);
            Tool.AnimationLst.Add(Tool.Action.ShortAttack, CAnimation3);
        }

        // Token: 0x06000207 RID: 519 RVA: 0x0001C8CB File Offset: 0x0001AACB
        public Tool(Entity PParent) : base(PParent)
        {
            this.Velocity = Vector3.Zero;
            this.Attacking = false;
            this.MaxAttackingDistance = 90f;
            this.MinAttackingDistance = 70f;
        }

        // Token: 0x06000208 RID: 520 RVA: 0x0001C8FE File Offset: 0x0001AAFE
        public Tool() : base(new Entity(EntityTypes.WayPoint))
        {
            this.Velocity = Vector3.Zero;
            this.Attacking = false;
            this.MaxAttackingDistance = 90f;
            this.MinAttackingDistance = 70f;
        }

        // Token: 0x04000218 RID: 536
        public static List<Tool> Tools = new List<Tool>(4);

        // Token: 0x04000219 RID: 537
        public static int iSword1;

        // Token: 0x0400021A RID: 538
        public static int iBow1;

        // Token: 0x0400021B RID: 539
        public static int iRSword;

        // Token: 0x0400021C RID: 540
        public static int iShovel;

        // Token: 0x0400021D RID: 541
        public Model Model;

        // Token: 0x0400021E RID: 542
        public Matrix[] Transforms;

        // Token: 0x0400021F RID: 543
        public Vector3 Velocity;

        // Token: 0x04000220 RID: 544
        public ePart Owner;

        // Token: 0x04000221 RID: 545
        public int Side;

        // Token: 0x04000222 RID: 546
        public bool Attacking;

        // Token: 0x04000223 RID: 547
        public Tool.Types Type1;

        // Token: 0x04000224 RID: 548
        public RewardSet RewardsToVictim;

        // Token: 0x04000225 RID: 549
        public Ray R;

        // Token: 0x04000226 RID: 550
        public float MaxAttackingDistance;

        // Token: 0x04000227 RID: 551
        public float MinAttackingDistance;

        // Token: 0x04000228 RID: 552
        public ePAnimationChain CAnimation;

        // Token: 0x04000229 RID: 553
        public static Dictionary<Tool.Action, ePAnimationChain> AnimationLst = new Dictionary<Tool.Action, ePAnimationChain>();

        // Token: 0x02000046 RID: 70
        public enum Action
        {
            // Token: 0x040002A4 RID: 676
            PrimaryAttack1,
            // Token: 0x040002A5 RID: 677
            PrimaryAttack2,
            // Token: 0x040002A6 RID: 678
            PrimaryAttack3,
            // Token: 0x040002A7 RID: 679
            ShortAttack,
            // Token: 0x040002A8 RID: 680
            SpecialAttak1,
            // Token: 0x040002A9 RID: 681
            SpecialAttak2
        }

        // Token: 0x02000047 RID: 71
        public enum Types
        {
            // Token: 0x040002AB RID: 683
            Sword,
            // Token: 0x040002AC RID: 684
            Bow,
            // Token: 0x040002AD RID: 685
            Shovel
        }
    }
}
