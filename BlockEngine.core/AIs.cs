using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
    // Token: 0x02000008 RID: 8
    public class AIs
    {
        // Token: 0x06000013 RID: 19 RVA: 0x0000228C File Offset: 0x0000048C
        private static void SubYoungManAI(Human E)
        {
            E.BodyParts[1].OriginalRotation = E.ModelRotationY;
            E.BodyParts[6].OriginalRotation = E.ModelRotationY;
            bool flag = !E.NoTarget;
            checked
            {
                if (flag)
                {
                    bool flag2 = E.Target != null && !E.Target.IsDead;
                    if (flag2)
                    {
                        AIs.DoCurrentAITartgetMode(E);
                    }
                    else
                    {
                        E.ReleaseTartget(true);
                    }
                }
                else
                {
                    bool flag3 = !E.InRandomMovement;
                    if (flag3)
                    {
                        E.PickNewRandomMovement();
                    }
                    else
                    {
                        E.CurrActionLst.Add(E.CRandomMovement);
                        E.CRandomMovementTime++;
                        bool flag4 = E.CRandomMovementTime > E.InRandomMovementTime;
                        if (flag4)
                        {
                            E.InRandomMovement = false;
                            E.PickNewRandomMovement();
                        }
                    }
                }
                E.DoCurrentActions();
            }
        }

        // Token: 0x06000014 RID: 20 RVA: 0x00002378 File Offset: 0x00000578
        private static void SubCivilianAI(Human E)
        {
            E.BodyParts[1].OriginalRotation = E.ModelRotationY;
            E.BodyParts[6].OriginalRotation = E.ModelRotationY;
            bool flag = !E.NoTarget;
            checked
            {
                if (flag)
                {
                    bool flag2 = E.Target != null && !E.Target.IsDead;
                    if (flag2)
                    {
                        AIs.DoCurrentAITartgetMode(E);
                    }
                    else
                    {
                        E.ReleaseTartget(true);
                    }
                }
                else
                {
                    bool flag3 = !E.InRandomMovement;
                    if (flag3)
                    {
                        E.PickNewRandomMovement();
                    }
                    else
                    {
                        E.CurrActionLst.Add(E.CRandomMovement);
                        E.CRandomMovementTime++;
                        bool flag4 = E.CRandomMovementTime > E.InRandomMovementTime;
                        if (flag4)
                        {
                            E.InRandomMovement = false;
                            E.PickNewRandomMovement();
                        }
                    }
                }
                E.DoCurrentActions();
            }
        }

        // Token: 0x06000015 RID: 21 RVA: 0x00002464 File Offset: 0x00000664
        private static void SubMurdurerAI(Human E)
        {
            E.BodyParts[1].OriginalRotation = E.ModelRotationY;
            E.BodyParts[6].OriginalRotation = E.ModelRotationY;
            bool flag = !E.NoTarget;
            checked
            {
                if (flag)
                {
                    bool flag2 = E.Target != null && !E.Target.IsDead;
                    if (flag2)
                    {
                        AIs.DoCurrentAITartgetMode(E);
                    }
                    else
                    {
                        E.ReleaseTartget(true);
                    }
                }
                else
                {
                    bool flag3 = !E.InRandomMovement;
                    if (flag3)
                    {
                        E.PickNewRandomMovement();
                    }
                    else
                    {
                        E.CurrActionLst.Add(E.CRandomMovement);
                        E.CRandomMovementTime++;
                        bool flag4 = E.CRandomMovementTime > E.InRandomMovementTime;
                        if (flag4)
                        {
                            E.InRandomMovement = false;
                            E.PickNewRandomMovement();
                        }
                    }
                }
                E.DoCurrentActions();
            }
        }

        // Token: 0x06000016 RID: 22 RVA: 0x00002550 File Offset: 0x00000750
        private static void SubGuardAI(Human E)
        {
            E.BodyParts[1].OriginalRotation = E.ModelRotationY;
            E.BodyParts[6].OriginalRotation = E.ModelRotationY;
            bool flag = !E.NoTarget;
            checked
            {
                if (flag)
                {
                    bool flag2 = E.Target != null && !E.Target.IsDead;
                    if (flag2)
                    {
                        AIs.DoCurrentAITartgetMode(E);
                    }
                    else
                    {
                        E.ReleaseTartget(true);
                    }
                }
                else
                {
                    bool flag3 = !E.InRandomMovement;
                    if (flag3)
                    {
                        E.PickNewRandomMovement();
                    }
                    else
                    {
                        E.CurrActionLst.Add(E.CRandomMovement);
                        E.CRandomMovementTime++;
                        bool flag4 = E.CRandomMovementTime > E.InRandomMovementTime;
                        if (flag4)
                        {
                            E.InRandomMovement = false;
                            E.PickNewRandomMovement();
                        }
                    }
                }
                E.DoCurrentActions();
            }
        }

        // Token: 0x06000017 RID: 23 RVA: 0x0000263C File Offset: 0x0000083C
        private static void SubWorkersAI(Human E)
        {
            E.BodyParts[1].OriginalRotation = E.ModelRotationY;
            E.BodyParts[6].OriginalRotation = E.ModelRotationY;
            bool flag = !E.NoTarget;
            if (flag)
            {
                bool flag2 = E.Target != null && !E.Target.IsDead;
                if (flag2)
                {
                    AIs.DoCurrentAITartgetMode_DoNotPickNewRndMotion(E);
                }
                else
                {
                    E.ReleaseTartget(true);
                }
            }
            E.DoCurrentActions();
        }

        // Token: 0x06000018 RID: 24 RVA: 0x000026C8 File Offset: 0x000008C8
        private static void SubAttackOnSight(Human E)
        {
            bool noTarget = E.NoTarget;
            if (noTarget)
            {
                bool flag = !E.CheckOutOfStackRange();
                if (flag)
                {
                    BoundingFrustum ViewFrustum = new BoundingFrustum(Matrix.CreateLookAt(E.Position, E.Position + E.HeadRotation.Forward, E.HeadRotation.Up) * Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(70f), 1f, 1f, E.CloseByRange));

                    foreach (Entity Ent in Ground.CStack.eList)
                    {
                        bool flag2 = Ent.ICode != E.ICode;
                        if (flag2)
                        {
                            bool flag3 = ViewFrustum.Intersects(new BoundingSphere(Ent.Position, 100f));
                            if (flag3)
                            {
                                bool flag4 = E.CheckAndLockTargetForSee(Ent) > AITargetMode.None;
                                if (flag4)
                                {
                                    break;
                                }
                            }
                        }
                    }

                }
            }
        }

        // Token: 0x06000019 RID: 25 RVA: 0x000027F4 File Offset: 0x000009F4
        private static void SubAttackWhenCloseBy(Human E)
        {
            bool noTarget = E.NoTarget;
            if (noTarget)
            {
                bool flag = !E.CheckOutOfStackRange();
                if (flag)
                {
                    BoundingSphere BS = new BoundingSphere(E.Position, E.CloseByRange);

                    foreach (Entity Ent in Ground.CStack.eList)
                    {
                        bool flag2 = BS.Intersects(new BoundingSphere(Ent.Position, 100f));
                        if (flag2)
                        {
                            bool flag3 = E.CheckAndLockTargetForSee(Ent) > AITargetMode.None;
                            if (flag3)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        // Token: 0x0600001A RID: 26 RVA: 0x000028C0 File Offset: 0x00000AC0
        private static void DoCurrentAITartgetMode(Human E)
        {
            checked
            {
                switch (E.TargetMode)
                {
                    case AITargetMode.None:
                        {
                            E.CRandomMovementTime++;
                            bool flag = E.CRandomMovementTime > E.InRandomMovementTime;
                            if (flag)
                            {
                                E.PickNewRandomMovement();
                            }
                            break;
                        }
                    case AITargetMode.FollowAndKill:
                        {
                            PhysicsFuncs.LookAtEntity(E, E.Target);
                            float TargetDistace = Vector3.Distance(E.Target.Position, E.Position);
                            bool flag2 = E.CTool != null;
                            if (flag2)
                            {
                                bool flag3 = TargetDistace > E.CTool.MaxAttackingDistance;
                                if (flag3)
                                {
                                    PhysicsFuncs.LookAtEntity(E, E.Target);
                                    E.CurrActionLst.Add(Actions.Forward);
                                }
                                else
                                {
                                    bool flag4 = TargetDistace < E.CTool.MinAttackingDistance;
                                    if (flag4)
                                    {
                                        PhysicsFuncs.LookAtEntity(E, E.Target);
                                        E.CurrActionLst.Add(Actions.Backward);
                                    }
                                    else
                                    {
                                        E.CurrActionLst.Add(Actions.Attack);
                                    }
                                }
                            }
                            break;
                        }
                    case AITargetMode.FollowAndAttackOnce:
                        {
                            PhysicsFuncs.LookAtEntity(E, E.Target);
                            float TargetDistace2 = Vector3.Distance(E.Target.Position, E.Position);
                            bool flag5 = E.CTool != null;
                            if (flag5)
                            {
                                bool flag6 = TargetDistace2 > E.CTool.MaxAttackingDistance;
                                if (flag6)
                                {
                                    PhysicsFuncs.LookAtEntity(E, E.Target);
                                    E.CurrActionLst.Add(Actions.Forward);
                                }
                                else
                                {
                                    bool flag7 = TargetDistace2 < E.CTool.MinAttackingDistance;
                                    if (flag7)
                                    {
                                        PhysicsFuncs.LookAtEntity(E, E.Target);
                                        E.CurrActionLst.Add(Actions.Backward);
                                    }
                                    else
                                    {
                                        E.CurrActionLst.Add(Actions.Attack);
                                        E.ReleaseTartget(true);
                                    }
                                }
                            }
                            break;
                        }
                    case AITargetMode.Follow:
                        {
                            PhysicsFuncs.LookAtEntity(E, E.Target);
                            float TargetDistace3 = Vector3.Distance(E.Target.Position, E.Position);
                            bool flag8 = TargetDistace3 > unchecked(E.eType.Width + 100f);
                            if (flag8)
                            {
                                PhysicsFuncs.LookAtEntity(E, E.Target);
                                E.CurrActionLst.Add(Actions.Forward);
                            }
                            else
                            {
                                bool flag9 = TargetDistace3 < E.eType.Width;
                                if (flag9)
                                {
                                    PhysicsFuncs.LookAtEntity(E, E.Target);
                                    E.CurrActionLst.Add(Actions.Backward);
                                }
                            }
                            break;
                        }
                    case AITargetMode.Look:
                        {
                            PhysicsFuncs.LookAtEntity(E, E.Target);
                            E.CRandomMovementTime++;
                            bool flag10 = E.CRandomMovementTime > E.eType.LookingAtTimeout;
                            if (flag10)
                            {
                                E.PickNewRandomMovement();
                            }
                            break;
                        }
                    case AITargetMode.RunFromIt:
                        {
                            PhysicsFuncs.LookOutFromEntity(E, E.Target);
                            E.CurrActionLst.Add(Actions.Forward);
                            E.CRandomMovementTime++;
                            bool flag11 = E.CRandomMovementTime > E.InRandomMovementTime;
                            if (flag11)
                            {
                                E.PickNewRandomMovement();
                            }
                            break;
                        }
                }
            }
        }

        // Token: 0x0600001B RID: 27 RVA: 0x00002BB0 File Offset: 0x00000DB0
        private static void DoCurrentAITartgetMode_DoNotPickNewRndMotion(Human E)
        {
            switch (E.TargetMode)
            {
                case AITargetMode.FollowAndKill:
                    {
                        PhysicsFuncs.LookAtEntity(E, E.Target);
                        float TargetDistace = Vector3.Distance(E.Target.Position, E.Position);
                        bool flag = E.CTool != null;
                        if (flag)
                        {
                            bool flag2 = TargetDistace > E.CTool.MaxAttackingDistance;
                            if (flag2)
                            {
                                PhysicsFuncs.LookAtEntity(E, E.Target);
                                E.CurrActionLst.Add(Actions.Forward);
                            }
                            else
                            {
                                bool flag3 = TargetDistace < E.CTool.MinAttackingDistance;
                                if (flag3)
                                {
                                    PhysicsFuncs.LookAtEntity(E, E.Target);
                                    E.CurrActionLst.Add(Actions.Backward);
                                }
                                else
                                {
                                    E.CurrActionLst.Add(Actions.Attack);
                                }
                            }
                        }
                        break;
                    }
                case AITargetMode.FollowAndAttackOnce:
                    {
                        PhysicsFuncs.LookAtEntity(E, E.Target);
                        float TargetDistace2 = Vector3.Distance(E.Target.Position, E.Position);
                        bool flag4 = E.CTool != null;
                        if (flag4)
                        {
                            bool flag5 = TargetDistace2 > E.CTool.MaxAttackingDistance;
                            if (flag5)
                            {
                                PhysicsFuncs.LookAtEntity(E, E.Target);
                                E.CurrActionLst.Add(Actions.Forward);
                            }
                            else
                            {
                                bool flag6 = TargetDistace2 < E.CTool.MinAttackingDistance;
                                if (flag6)
                                {
                                    PhysicsFuncs.LookAtEntity(E, E.Target);
                                    E.CurrActionLst.Add(Actions.Backward);
                                }
                                else
                                {
                                    E.CurrActionLst.Add(Actions.Attack);
                                    E.ReleaseTartget(false);
                                }
                            }
                        }
                        break;
                    }
                case AITargetMode.Follow:
                    {
                        PhysicsFuncs.LookAtEntity(E, E.Target);
                        float TargetDistace3 = Vector3.Distance(E.Target.Position, E.Position);
                        bool flag7 = TargetDistace3 > E.eType.Width + 100f;
                        if (flag7)
                        {
                            PhysicsFuncs.LookAtEntity(E, E.Target);
                            E.CurrActionLst.Add(Actions.Forward);
                        }
                        else
                        {
                            bool flag8 = TargetDistace3 < E.eType.Width;
                            if (flag8)
                            {
                                PhysicsFuncs.LookAtEntity(E, E.Target);
                                E.CurrActionLst.Add(Actions.Backward);
                            }
                        }
                        break;
                    }
                case AITargetMode.Look:
                    PhysicsFuncs.LookAtEntity(E, E.Target);
                    break;
                case AITargetMode.RunFromIt:
                    PhysicsFuncs.LookOutFromEntity(E, E.Target);
                    E.CurrActionLst.Add(Actions.Forward);
                    break;
            }
        }

        // Token: 0x04000008 RID: 8
        public static Action<Entity> YoungMan = delegate (Entity a0)
        {
            AIs.SubYoungManAI((Human)a0);
        };


        // Token: 0x04000009 RID: 9
        public static Action<Entity> Civilian = delegate (Entity a0)
        {
            AIs.SubCivilianAI((Human)a0);
        };

        // Token: 0x0400000A RID: 10
        public static Action<Entity> Guard = delegate (Entity a0)
        {
            AIs.SubGuardAI((Human)a0);
        };

        // Token: 0x0400000B RID: 11
        public static Action<Entity> Worker = delegate (Entity a0)
        {
            AIs.SubWorkersAI((Human)a0);
        };

        // Token: 0x0400000C RID: 12
        public static Action<Entity> Murdurer = delegate (Entity a0)
        {
            AIs.SubMurdurerAI((Human)a0);
        };

        // Token: 0x0400000D RID: 13
        public static Action<Entity> AttackOnSight = delegate (Entity a0)
        {
            AIs.SubAttackOnSight((Human)a0);
        };

        // Token: 0x0400000E RID: 14
        public static Action<Entity> AttackWhenCloseBy = delegate (Entity a0)
        {
            AIs.SubAttackWhenCloseBy((Human)a0);
        };
    }
}
