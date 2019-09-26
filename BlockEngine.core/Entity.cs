using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
    // Token: 0x02000015 RID: 21
    public class Entity : IEquatable<Entity>
    {
        // Token: 0x17000012 RID: 18
        // (get) Token: 0x06000070 RID: 112 RVA: 0x000073ED File Offset: 0x000055ED
        // (set) Token: 0x06000071 RID: 113 RVA: 0x000073F7 File Offset: 0x000055F7
        public float RotationY { get; set; }

        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000072 RID: 114 RVA: 0x00007400 File Offset: 0x00005600
        // (set) Token: 0x06000073 RID: 115 RVA: 0x0000740A File Offset: 0x0000560A
        public float RotationX { get; set; }

        // Token: 0x17000014 RID: 20
        // (get) Token: 0x06000074 RID: 116 RVA: 0x00007413 File Offset: 0x00005613
        // (set) Token: 0x06000075 RID: 117 RVA: 0x0000741D File Offset: 0x0000561D
        public float RotationZ { get; set; }

        // Token: 0x06000076 RID: 118 RVA: 0x00007426 File Offset: 0x00005626
        public virtual void Update()
        {
        }

        // Token: 0x06000077 RID: 119 RVA: 0x00007429 File Offset: 0x00005629
        public virtual void Draw()
        {
        }

        // Token: 0x06000078 RID: 120 RVA: 0x0000742C File Offset: 0x0000562C
        public void ExpensiveUpdate()
        {
            bool flag = this.DelTargetScan != null;
            if (flag)
            {
                this.DelTargetScan(this);
            }
        }

        // Token: 0x06000079 RID: 121 RVA: 0x00007454 File Offset: 0x00005654
        public virtual void Load(EntityType eType)
        {
            this.eType = eType;
        }

        // Token: 0x0600007A RID: 122 RVA: 0x00007460 File Offset: 0x00005660
        public Entity(EntityType eT)
        {
            this.Name = "Model0";
            this.Health = 200;
            this.MaxHealth = 500;
            this.Strength = 1f;
            this.Weight = 0.3f;
            this.XP = 0;
            this.Position = Vector3.Zero;
            this.Accelaration = new Vector3(3f, 3f, 3.4f);
            this.RotationAccelaration = new Vector3(0.02f, 0.03f, 0.02f);
            this.RotationVelocityReducingFactor = new Vector3(0.01f, 0.01f, 0.01f);
            this.ModelVelocityReducingFactor = new Vector3(0.7f, 0.6f, 0.7f);
            this.ModelRotation = Matrix.Identity;
            this.ModelRotationY = Matrix.Identity;
            this.Velocity = new Vector3(0f);
            this.MaxVelocity = new Vector3(0.5f, 0.5f, 2f);
            this.Jumping = false;
            this.MovedFB = false;
            this.WalkingFw = false;
            this.HeadRotation = Matrix.Identity;
            this.NeededBodyRotation = Matrix.Identity;
            this.NeededBodyRotationChanged = true;
            this.NeededBodyRotationGainingSpeed = 0.4f;
            this.FacingDirection = new Vector3(0f, 0f, 1f);
            this.FallingSpeed = 0f;
            this.RotationVelocity = Vector3.Zero;
            this.BodyParts = new List<ePart>(8);
            this.Height = new Vector3(0f, 120f, 0f);
            this.CurrentChunk = new Chunk();
            this.TrappedCount = 0;
            this.OnGround = false;
            this.IsDead = false;
            this.TargetLockedAIs = new List<Entity>();
            this.Enemies = new List<Entity>();
            this.Friendlies = new List<Entity>();
            this.HighestTatget = false;
            this.NoTarget = true;
            this.InRandomMovement = false;
            this.CurrActionLst = new List<Actions>();
            this.LastActionTime = 0.0;
            this.Tools = new List<Tool>();
            this.NoAI = false;
            this.CloseByRange = 1000f;
            this.eType = eT;
        }

        // Token: 0x0600007B RID: 123 RVA: 0x00007690 File Offset: 0x00005890
        public Entity()
        {
            this.Name = "Model0";
            this.Health = 200;
            this.MaxHealth = 500;
            this.Strength = 1f;
            this.Weight = 0.3f;
            this.XP = 0;
            this.Position = Vector3.Zero;
            this.Accelaration = new Vector3(3f, 3f, 3.4f);
            this.RotationAccelaration = new Vector3(0.02f, 0.03f, 0.02f);
            this.RotationVelocityReducingFactor = new Vector3(0.01f, 0.01f, 0.01f);
            this.ModelVelocityReducingFactor = new Vector3(0.7f, 0.6f, 0.7f);
            this.ModelRotation = Matrix.Identity;
            this.ModelRotationY = Matrix.Identity;
            this.Velocity = new Vector3(0f);
            this.MaxVelocity = new Vector3(0.5f, 0.5f, 2f);
            this.Jumping = false;
            this.MovedFB = false;
            this.WalkingFw = false;
            this.HeadRotation = Matrix.Identity;
            this.NeededBodyRotation = Matrix.Identity;
            this.NeededBodyRotationChanged = true;
            this.NeededBodyRotationGainingSpeed = 0.4f;
            this.FacingDirection = new Vector3(0f, 0f, 1f);
            this.FallingSpeed = 0f;
            this.RotationVelocity = Vector3.Zero;
            this.BodyParts = new List<ePart>(8);
            this.Height = new Vector3(0f, 120f, 0f);
            this.CurrentChunk = new Chunk();
            this.TrappedCount = 0;
            this.OnGround = false;
            this.IsDead = false;
            this.TargetLockedAIs = new List<Entity>();
            this.Enemies = new List<Entity>();
            this.Friendlies = new List<Entity>();
            this.HighestTatget = false;
            this.NoTarget = true;
            this.InRandomMovement = false;
            this.CurrActionLst = new List<Actions>();
            this.LastActionTime = 0.0;
            this.Tools = new List<Tool>();
            this.NoAI = false;
            this.CloseByRange = 1000f;
        }

        // Token: 0x0600007C RID: 124 RVA: 0x000078B6 File Offset: 0x00005AB6
        public virtual void GiveTool(Tool T, int Place)
        {
        }

        // Token: 0x0600007D RID: 125 RVA: 0x000078BC File Offset: 0x00005ABC
        public virtual void DoCurrentActions()
        {
            foreach (Actions Act in this.CurrActionLst)
            {
                bool flag = Controls.Go(this, Act);
                if (flag)
                {
                    bool inRandomMovement = this.InRandomMovement;
                    if (inRandomMovement)
                    {
                        this.PickNewRandomMovement();
                    }
                }
            }

            bool flag2 = this.CurrActionLst.Contains(Actions.Attack);
            if (flag2)
            {
                bool flag3 = this.CTool != null;
                if (flag3)
                {
                    bool flag4 = this.LastActionTime + 500.0 < (double)Main.NowGameTime & !this.CTool.Owner.LockedeA;
                    if (flag4)
                    {
                        foreach (ePAnimation eA in this.CTool.Owner.CurrentAnimations)
                        {
                            eA.Reset();
                        }
                        this.CTool.Owner.Revert();
                        this.CTool.Use(PhysicsFuncs.RandomOf<Tool.Action>(new Tool.Action[]
                        {
                            Tool.Action.PrimaryAttack1,
                            Tool.Action.PrimaryAttack2,
                            Tool.Action.ShortAttack
                        }));
                        this.LastActionTime = (double)Main.NowGameTime;
                    }
                }
            }
            this.CurrActionLst.Clear();
        }

        // Token: 0x0600007E RID: 126 RVA: 0x00007A2C File Offset: 0x00005C2C
        public void ShotHit(Entity Hater)
        {
            this.CheckAndLockTargetForAttack(Hater);
            Entity[] Helpers = this.NearEntities(500f, EntityRelationMode.Friends);
            foreach (Entity e in Helpers)
            {
                e.CheckAndLockTargetForAttack(Hater);
            }
            bool flag = Helpers.Count<Entity>() == 0;
            if (flag)
            {
            }
        }

        // Token: 0x0600007F RID: 127 RVA: 0x00007A84 File Offset: 0x00005C84
        public bool LockTarget(Entity at, AITargetMode Mode, int ForHowLong = 600)
        {
            bool flag = !(at == this);
            if (flag)
            {
                bool flag2 = !at.IsDead;
                if (flag2)
                {
                    this.NoTarget = false;
                    this.Target = at;
                    this.TargetMode = Mode;
                    bool flag3 = !at.TargetLockedAIs.Contains(this);
                    if (flag3)
                    {
                        at.TargetLockedAIs.Add(this);
                    }
                    this.InRandomMovement = false;
                    this.InRandomMovementTime = ForHowLong;
                    this.CRandomMovementTime = 0;
                    return true;
                }
            }
            return false;
        }

        // Token: 0x06000080 RID: 128 RVA: 0x00007B04 File Offset: 0x00005D04
        public AITargetMode CheckAndLockTargetForAttack(Entity at)
        {
            bool flag = !(at == this);
            if (flag)
            {
                bool flag2 = !at.IsDead;
                if (flag2)
                {
                    EntityRelationMode Rel = this.ChekRelation(at);
                    bool flag3 = this.eType.TargetSelectingRulesForDamage.ContainsKey(Rel);
                    if (flag3)
                    {
                        AITargetMode TTargetMode = this.eType.TargetSelectingRulesForDamage[Rel];
                        bool flag4 = TTargetMode > AITargetMode.None;
                        if (flag4)
                        {
                            this.LockTarget(at, TTargetMode, 600);
                        }
                        return TTargetMode;
                    }
                }
            }
            return AITargetMode.None;
        }

        // Token: 0x06000081 RID: 129 RVA: 0x00007B8C File Offset: 0x00005D8C
        public AITargetMode CheckAndLockTargetForSee(Entity at)
        {
            bool flag = !(at == this);
            if (flag)
            {
                bool flag2 = !at.IsDead;
                if (flag2)
                {
                    EntityRelationMode Rel = this.ChekRelation(at);
                    bool flag3 = this.eType.TargetSelectingRulesForDetection.ContainsKey(Rel);
                    if (flag3)
                    {
                        AITargetMode TTargetMode = this.eType.TargetSelectingRulesForDetection[Rel];
                        bool flag4 = TTargetMode > AITargetMode.None;
                        if (flag4)
                        {
                            this.LockTarget(at, TTargetMode, 600);
                        }
                        return TTargetMode;
                    }
                }
            }
            return AITargetMode.None;
        }

        // Token: 0x06000082 RID: 130 RVA: 0x00007C14 File Offset: 0x00005E14
        public void ReleaseTartget(bool PickNewRndMovement = true)
        {
            this.NoTarget = true;
            bool flag = this.Target != null;
            if (flag)
            {
                this.Target.TargetLockedAIs.Remove(this);
                this.TargetLockedAIs.Remove(this.Target);
            }
            this.Target = null;
            bool flag2 = this.HighestTatget && !this.HighestTatgetE.IsDead;
            if (flag2)
            {
                this.LockTarget(this.HighestTatgetE, this.HighestTargetMode, 600);
                PickNewRndMovement = false;
            }
            bool flag3 = PickNewRndMovement;
            if (flag3)
            {
                this.PickNewRandomMovement();
            }
        }

        // Token: 0x06000083 RID: 131 RVA: 0x00007CA9 File Offset: 0x00005EA9
        public void DefendHere()
        {
            this.PickNewRandomMovement(Actions.Null, int.MaxValue);
            this.NoTarget = false;
            this.DelTargetScan = (Action<Entity>)AIs.AttackWhenCloseBy;
        }

        // Token: 0x06000084 RID: 132 RVA: 0x00007CD0 File Offset: 0x00005ED0
        public EntityRelationMode ChekRelation(Entity e)
        {

            foreach (Entity eni in this.Enemies)
            {
                bool flag = e == eni;
                if (flag)
                {
                    return EntityRelationMode.Enemy;
                }
            }

            foreach (Entity eni2 in this.Friendlies)
            {
                bool flag2 = e == eni2;
                if (flag2)
                {
                    return EntityRelationMode.Friends;
                }
            }


            foreach (EntityType eni3 in this.eType.EtFriendlies)
            {
                bool flag3 = e.eType.Equals(eni3);
                if (flag3)
                {
                    return EntityRelationMode.TypeFriends;
                }
            }

            foreach (EntityType eni4 in this.eType.EtEnemies)
            {
                bool flag4 = e.eType.Equals(eni4);
                if (flag4)
                {
                    return EntityRelationMode.TypeEnemies;
                }
            }

            return EntityRelationMode.Unknowen;
        }

        // Token: 0x06000085 RID: 133 RVA: 0x00007E58 File Offset: 0x00006058
        public void BeFriends(Entity e)
        {
            this.BeUnknown(e);
            bool flag = !this.Friendlies.Contains(e);
            if (flag)
            {
                this.Friendlies.Add(e);
            }
            bool flag2 = !e.Friendlies.Contains(this);
            if (flag2)
            {
                e.Friendlies.Add(this);
            }
        }

        // Token: 0x06000086 RID: 134 RVA: 0x00007EB4 File Offset: 0x000060B4
        public void BeEnemies(Entity e)
        {
            this.BeUnknown(e);
            bool flag = !this.Enemies.Contains(e);
            if (flag)
            {
                this.Enemies.Add(e);
            }
            bool flag2 = !e.Enemies.Contains(this);
            if (flag2)
            {
                e.Enemies.Add(this);
            }
        }

        // Token: 0x06000087 RID: 135 RVA: 0x00007F10 File Offset: 0x00006110
        public void BeUnknown(Entity e)
        {
            bool flag = this.Enemies.Contains(e);
            if (flag)
            {
                this.Enemies.Remove(e);
            }
            bool flag2 = this.Friendlies.Contains(e);
            if (flag2)
            {
                this.Friendlies.Remove(e);
            }
            bool flag3 = e.Enemies.Contains(e);
            if (flag3)
            {
                e.Enemies.Remove(e);
            }
            bool flag4 = e.Friendlies.Contains(e);
            if (flag4)
            {
                e.Friendlies.Remove(e);
            }
        }

        // Token: 0x06000088 RID: 136 RVA: 0x00007F94 File Offset: 0x00006194
        public Entity NearestEntity()
        {
            Entity Nearest = null;
            float NearestDistance = float.PositiveInfinity;


            foreach (Entity e in Ground.CStack.eList)
            {
                bool flag = !(e == this);
                if (flag)
                {
                    float CDistance = Vector3.DistanceSquared(this.Position, e.Position);
                    bool flag2 = CDistance < NearestDistance;
                    if (flag2)
                    {
                        NearestDistance = CDistance;
                        Nearest = e;
                    }
                }
            }


            return Nearest;
        }

        // Token: 0x06000089 RID: 137 RVA: 0x00008034 File Offset: 0x00006234
        public Entity[] NearEntities(float radius)
        {
            List<Entity> Nearests = new List<Entity>();
            float r = radius * radius;

            foreach (Entity e in Ground.CStack.eList)
            {
                bool flag = e != this;
                if (flag)
                {
                    bool flag2 = Vector3.DistanceSquared(this.Position, e.Position) < r;
                    if (flag2)
                    {
                        Nearests.Add(e);
                    }
                }
            }
            return Nearests.ToArray();
        }

        // Token: 0x0600008A RID: 138 RVA: 0x000080D8 File Offset: 0x000062D8
        public Entity[] NearEntities(float radius, EntityRelationMode Relation)
        {
            List<Entity> Nearests = new List<Entity>();
            float r = radius * radius;


            foreach (Entity e in Ground.CStack.eList)
            {
                bool flag = e != this;
                if (flag)
                {
                    bool flag2 = Vector3.DistanceSquared(this.Position, e.Position) < r && e.ChekRelation(this) == Relation;
                    if (flag2)
                    {
                        Nearests.Add(e);
                    }
                }
            }


            return Nearests.ToArray();
        }

        // Token: 0x0600008B RID: 139 RVA: 0x00008188 File Offset: 0x00006388
        public Entity[] FacingEntities(float Distance, float FieldOfViewDegrees)
        {
            List<Entity> FacingEs = new List<Entity>();
            BoundingFrustum ViewFrust = new BoundingFrustum(Matrix.CreateLookAt(this.Position, this.Position + this.HeadRotation.Forward, this.HeadRotation.Up) * Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(FieldOfViewDegrees), 2f, 1f, Distance));


            foreach (Entity e in Ground.CStack.eList)
            {
                bool flag = e != this;
                if (flag)
                {
                    bool flag2 = ViewFrust.Intersects(new BoundingSphere(e.Position, 100f));
                    if (flag2)
                    {
                        FacingEs.Add(e);
                    }
                }
            }


            return FacingEs.ToArray();
        }

        // Token: 0x0600008C RID: 140 RVA: 0x00008278 File Offset: 0x00006478
        public Entity FacingEntity(float Length)
        {
            Ray R = new Ray(this.Position + this.Height, this.HeadRotation.Forward);


            foreach (Entity e in Ground.CStack.eList)
            {
                bool flag = e != this;
                if (flag)
                {
                    bool flag2 = e.CollitionHierarchy.GetCollided(R, Length) != null;
                    if (flag2)
                    {
                        return e;
                    }
                }
            }

            return null;
        }

        // Token: 0x0600008D RID: 141 RVA: 0x00008324 File Offset: 0x00006524
        public void Hire(Entity Owner)
        {
            this.BeFriends(Owner);
            this.LockTarget(Owner, AITargetMode.Follow, 600);
            this.HighestTatget = true;
            this.HighestTatgetE = Owner;
            this.HighestTargetMode = AITargetMode.Follow;

            foreach (Entity e in Owner.Friendlies)
            {
                e.BeFriends(this);
            }
        }

        // Token: 0x0600008E RID: 142 RVA: 0x000083A8 File Offset: 0x000065A8
        public void Go(Actions Act)
        {
            bool flag = !this.CurrActionLst.Contains(Act);
            if (flag)
            {
                this.CurrActionLst.Add(Act);
            }
        }

        // Token: 0x0600008F RID: 143 RVA: 0x000083D8 File Offset: 0x000065D8
        public void Go(Vector3 Destination)
        {
            this.NoTarget = false;
            Action<Entity> OriginalAI = this.DelUpdateAI;
            this.DelUpdateAI = (Action<Entity>)AIs.Worker;
            PhysicsFuncs.LookAtPosition(this, Destination);
            while (this.NeededBodyRotationChanged)
            {
                Main.Delay(10);
            }
            double LastDistance = double.MaxValue;
            double CurrDist = double.MaxValue;
            int AccelerationSq = 2500;
            for (; ; )
            {
                bool flag = CurrDist < (double)AccelerationSq;
                if (flag)
                {
                    break;
                }
                long LastUpdateCount = Main.CurrUpdateCount;
                this.Go(Actions.Forward);
                while (LastUpdateCount == Main.CurrUpdateCount)
                {
                    Main.Delay(10);
                }
                CurrDist = (double)Vector3.DistanceSquared(this.Position, Destination);
                bool flag2 = CurrDist > LastDistance;
                if (flag2)
                {
                    PhysicsFuncs.LookAtPosition(this, Destination);
                    while (this.NeededBodyRotationChanged)
                    {
                        Main.Delay(10);
                    }
                }
                LastDistance = CurrDist;
            }
            this.NoTarget = true;
            this.DelUpdateAI = OriginalAI;
        }

        // Token: 0x06000090 RID: 144 RVA: 0x000084C4 File Offset: 0x000066C4
        public void PickNewRandomMovement()
        {
            bool flag = !this.NoAI;
            if (flag)
            {
                int RNDOut = PhysicsFuncs.CRND.Next(0, 4);
                bool flag2 = RNDOut == 0;
                if (flag2)
                {
                    this.CRandomMovement = Actions.Forward;
                    this.InRandomMovementTime = PhysicsFuncs.CRND.Next(200);
                }
                else
                {
                    bool flag3 = RNDOut == 1;
                    if (flag3)
                    {
                        this.CRandomMovement = Actions.RotateClockwiseY;
                        this.InRandomMovementTime = PhysicsFuncs.CRND.Next(30);
                    }
                    else
                    {
                        bool flag4 = RNDOut == 2;
                        if (flag4)
                        {
                            this.CRandomMovement = Actions.RotateAntiClockwiseY;
                            this.InRandomMovementTime = PhysicsFuncs.CRND.Next(30);
                        }
                        else
                        {
                            this.CRandomMovement = Actions.Null;
                            this.InRandomMovementTime = PhysicsFuncs.CRND.Next(60);
                        }
                    }
                }
                this.InRandomMovement = true;
                this.CRandomMovementTime = 0;
                this.NoTarget = true;
            }
        }

        // Token: 0x06000091 RID: 145 RVA: 0x00008592 File Offset: 0x00006792
        public void PickNewRandomMovement(Actions Act, int MaxTime, int MinTime)
        {
            this.CRandomMovement = Act;
            this.InRandomMovementTime = PhysicsFuncs.CRND.Next(MinTime, MaxTime);
            this.InRandomMovement = true;
            this.CRandomMovementTime = 0;
            this.NoTarget = true;
        }

        // Token: 0x06000092 RID: 146 RVA: 0x000085C3 File Offset: 0x000067C3
        public void PickNewRandomMovement(Actions Act, int Time)
        {
            this.CRandomMovement = Act;
            this.InRandomMovementTime = Time;
            this.InRandomMovement = true;
            this.CRandomMovementTime = 0;
            this.NoTarget = true;
        }

        // Token: 0x06000093 RID: 147 RVA: 0x000085EC File Offset: 0x000067EC
        public virtual void Kill()
        {
            this.Health = -1;
            this.IsDead = true;
            checked
            {
                int num = this.Friendlies.Count - 1;
                for (int i = 0; i <= num; i++)
                {
                    this.Friendlies[i].Friendlies.Remove(this);
                }
                int num2 = this.Enemies.Count - 1;
                for (int j = 0; j <= num2; j++)
                {
                    this.Enemies[j].Enemies.Remove(this);
                }
            }
        }

        /// <summary>
        /// Player Only
        /// </summary>
        // Token: 0x06000094 RID: 148 RVA: 0x0000866C File Offset: 0x0000686C
        public void ChunkOutOfStackRange()
        {
            IntVector3 RealChunkIndex = Ground.ChunkIndexOfPosition(this.Position);
            bool BNotOutOfRange = true;
            bool flag = Ground.CStack.ChunkRangeMax.Z <= RealChunkIndex.Z;
            if (flag)
            {
                this.Position.Z = (float)(checked(Ground.CStack.ChunkRangeMax.Z * 400)) - 50f;
                BNotOutOfRange = false;
            }
            else
            {
                bool flag2 = RealChunkIndex.Z <= Ground.CStack.ChunkRangeMin.Z;
                if (flag2)
                {
                    this.Position.Z = (float)(checked(Ground.CStack.ChunkRangeMin.Z * 400)) + 50f;
                    BNotOutOfRange = false;
                }
            }
            bool flag3 = Ground.CStack.ChunkRangeMax.X <= RealChunkIndex.X;
            if (flag3)
            {
                this.Position.X = (float)(checked(Ground.CStack.ChunkRangeMax.X * 400)) - 50f;
                BNotOutOfRange = false;
            }
            else
            {
                bool flag4 = RealChunkIndex.X <= Ground.CStack.ChunkRangeMin.X;
                if (flag4)
                {
                    this.Position.X = (float)(checked(Ground.CStack.ChunkRangeMin.X * 400)) + 50f;
                    BNotOutOfRange = false;
                }
            }
            bool flag5 = BNotOutOfRange;
            if (flag5)
            {
                Loader.LoadAndReplaceChunkTempory(RealChunkIndex);
                this.Velocity *= -5f;
            }
            this.InRandomMovement = false;
        }

        // Token: 0x06000095 RID: 149 RVA: 0x000087E8 File Offset: 0x000069E8
        public bool CheckOutOfStackRange()
        {
            IntVector3 RealChunkIndex = Ground.ChunkIndexOfPosition(this.Position);
            bool flag = Ground.CStack.ChunkRangeMax.Z <= RealChunkIndex.Z;
            bool CheckOutOfStackRange;
            if (flag)
            {
                CheckOutOfStackRange = true;
            }
            else
            {
                bool flag2 = RealChunkIndex.Z <= Ground.CStack.ChunkRangeMin.Z;
                if (flag2)
                {
                    CheckOutOfStackRange = true;
                }
                else
                {
                    bool flag3 = Ground.CStack.ChunkRangeMax.X <= RealChunkIndex.X;
                    if (flag3)
                    {
                        CheckOutOfStackRange = true;
                    }
                    else
                    {
                        bool flag4 = RealChunkIndex.X <= Ground.CStack.ChunkRangeMin.X;
                        CheckOutOfStackRange = flag4;
                    }
                }
            }
            return CheckOutOfStackRange;
        }

        // Token: 0x06000096 RID: 150 RVA: 0x00008894 File Offset: 0x00006A94
        public bool CheckOutOfWorld()
        {
            IntVector3 RealChunkIndex = Ground.ChunkIndexOfPosition(this.Position);
            bool flag = Loader.MaxWorldBorders.Z <= RealChunkIndex.Z;
            bool CheckOutOfWorld;
            if (flag)
            {
                CheckOutOfWorld = true;
            }
            else
            {
                bool flag2 = RealChunkIndex.Z <= 0;
                if (flag2)
                {
                    CheckOutOfWorld = true;
                }
                else
                {
                    bool flag3 = Loader.MaxWorldBorders.X <= RealChunkIndex.X;
                    if (flag3)
                    {
                        CheckOutOfWorld = true;
                    }
                    else
                    {
                        bool flag4 = RealChunkIndex.X <= 0;
                        CheckOutOfWorld = flag4;
                    }
                }
            }
            return CheckOutOfWorld;
        }

        // Token: 0x06000097 RID: 151 RVA: 0x0000891C File Offset: 0x00006B1C
        public void PutInsideOfTheWorld()
        {
            IntVector3 RealChunkIndex = Ground.ChunkIndexOfPosition(this.Position);
            bool flag = Loader.MaxWorldBorders.X <= RealChunkIndex.X;
            if (flag)
            {
                this.Position.X = (float)(checked(Loader.MaxWorldBorders.X * 8 * 50)) - 100f;
            }
            else
            {
                bool flag2 = RealChunkIndex.X <= Ground.CStack.ChunkRangeMin.X;
                if (flag2)
                {
                    this.Position.X = 500f;
                }
            }
            bool flag3 = Loader.MaxWorldBorders.Z <= RealChunkIndex.Z;
            if (flag3)
            {
                this.Position.Z = (float)(checked(Loader.MaxWorldBorders.Z * 8 * 50)) - 100f;
            }
            else
            {
                bool flag4 = RealChunkIndex.Z <= Ground.CStack.ChunkRangeMin.Z;
                if (flag4)
                {
                    this.Position.Z = 500f;
                }
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }

        public bool Equals(Entity other)
        {
            return other != null &&
                   ICode == other.ICode;
        }

        public override int GetHashCode()
        {
            return 2052254649 + ICode.GetHashCode();
        }



        //// Token: 0x06000098 RID: 152 RVA: 0x00008A10 File Offset: 0x00006C10
        //public static bool operator ==(Entity Left, Entity Right)
        //{
        //    return Left.ICode == Right.ICode;
        //}

        //// Token: 0x06000099 RID: 153 RVA: 0x00008A30 File Offset: 0x00006C30
        //public static bool operator !=(Entity Left, Entity Right)
        //{
        //    return Left.ICode != Right.ICode;
        //}

        //// Token: 0x0600009A RID: 154 RVA: 0x00008A54 File Offset: 0x00006C54
        //public override bool Equals(object obj)
        //{
        //    return this.ICode == ((Entity)obj).ICode;
        //}

        // Token: 0x04000092 RID: 146
        public string Name;

        // Token: 0x04000093 RID: 147
        public int ICode;

        // Token: 0x04000094 RID: 148
        public EntityType eType;

        // Token: 0x04000095 RID: 149
        public int Health;

        // Token: 0x04000096 RID: 150
        public int MaxHealth;

        // Token: 0x04000097 RID: 151
        public float Strength;

        // Token: 0x04000098 RID: 152
        public float Weight;

        // Token: 0x04000099 RID: 153
        public int XP;

        // Token: 0x0400009A RID: 154
        public Vector3 Position;

        // Token: 0x0400009B RID: 155
        public Vector3 Accelaration;

        // Token: 0x0400009C RID: 156
        public Vector3 RotationAccelaration;

        // Token: 0x0400009D RID: 157
        public Vector3 RotationVelocityReducingFactor;

        // Token: 0x0400009E RID: 158
        public Vector3 ModelVelocityReducingFactor;

        // Token: 0x0400009F RID: 159
        public Matrix ModelRotation;

        // Token: 0x040000A0 RID: 160
        public Matrix ModelRotationY;

        // Token: 0x040000A1 RID: 161
        public Vector3 Velocity;

        // Token: 0x040000A2 RID: 162
        public Vector3 MaxVelocity;

        // Token: 0x040000A3 RID: 163
        public bool Jumping;

        // Token: 0x040000A4 RID: 164
        public bool MovedFB;

        // Token: 0x040000A5 RID: 165
        public bool WalkingFw;

        // Token: 0x040000A6 RID: 166
        public Matrix HeadRotation;

        // Token: 0x040000A7 RID: 167
        public Matrix NeededBodyRotation;

        // Token: 0x040000A8 RID: 168
        public bool NeededBodyRotationChanged;

        // Token: 0x040000A9 RID: 169
        public float NeededBodyRotationGainingSpeed;

        // Token: 0x040000AA RID: 170
        public Vector3 FacingDirection;

        // Token: 0x040000AB RID: 171
        public float FallingSpeed;

        // Token: 0x040000AC RID: 172
        public Vector3 RotationVelocity;

        // Token: 0x040000AD RID: 173
        public List<ePart> BodyParts;

        // Token: 0x040000AE RID: 174
        public Vector3 Height;

        // Token: 0x040000AF RID: 175
        public Ground.BlockEnvironment BlockEnv;

        // Token: 0x040000B0 RID: 176
        public Chunk CurrentChunk;

        // Token: 0x040000B1 RID: 177
        public DBlock CurrentBlock;

        // Token: 0x040000B2 RID: 178
        public Direction DualFacingDirectionsFB;

        // Token: 0x040000B3 RID: 179
        public Direction DualFacingDirectionsLR;

        // Token: 0x040000B4 RID: 180
        public int TrappedCount;

        // Token: 0x040000B5 RID: 181
        public bool OnGround;

        // Token: 0x040000B6 RID: 182
        public bool IsDead;

        // Token: 0x040000B7 RID: 183
        public List<Entity> TargetLockedAIs;

        // Token: 0x040000B8 RID: 184
        public List<Entity> Enemies;

        // Token: 0x040000B9 RID: 185
        public List<Entity> Friendlies;

        // Token: 0x040000BA RID: 186
        public eCollitionHierarchy CollitionHierarchy;

        // Token: 0x040000BB RID: 187
        public bool HighestTatget;

        // Token: 0x040000BC RID: 188
        public Entity HighestTatgetE;

        // Token: 0x040000BD RID: 189
        public AITargetMode HighestTargetMode;

        // Token: 0x040000BE RID: 190
        public Entity Target;

        // Token: 0x040000BF RID: 191
        public AITargetMode TargetMode;

        // Token: 0x040000C0 RID: 192
        public bool NoTarget;

        // Token: 0x040000C1 RID: 193
        public Actions CRandomMovement;

        // Token: 0x040000C2 RID: 194
        public bool InRandomMovement;

        // Token: 0x040000C3 RID: 195
        public int CRandomMovementTime;

        // Token: 0x040000C4 RID: 196
        public int InRandomMovementTime;

        // Token: 0x040000C5 RID: 197
        public List<Actions> CurrActionLst;

        // Token: 0x040000C6 RID: 198
        public double LastActionTime;

        // Token: 0x040000C7 RID: 199
        public List<Tool> Tools;

        // Token: 0x040000C8 RID: 200
        public Tool CTool;

        // Token: 0x040000CC RID: 204
        public bool NoAI;

        // Token: 0x040000CD RID: 205
        public Action<Entity> DelTargetScan;

        // Token: 0x040000CE RID: 206
        public Action<Entity> DelUpdateAI;

        // Token: 0x040000CF RID: 207
        public float CloseByRange;

        /// <summary>
        /// Last ICode
        /// </summary>
        // Token: 0x040000D0 RID: 208
        public static short LICode = 0;

        public static bool operator ==(Entity left, Entity right)
        {
            return EqualityComparer<Entity>.Default.Equals(left, right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
