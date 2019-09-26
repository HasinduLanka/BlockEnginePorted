using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;
using System;

namespace BlockEngine
{
    // Token: 0x0200002C RID: 44
    public class Physics
    {
        // Token: 0x060001C0 RID: 448 RVA: 0x00017328 File Offset: 0x00015528
        public static Vector3 FindStandedVec3OfDirection(Vector3 D)
        {
            float X = D.X;
            float Y = D.Y;
            float Z = D.Z;
            float aX = Math.Abs(D.X);
            float aY = Math.Abs(D.Y);
            float aZ = Math.Abs(D.Z);
            Vector3 O = default;
            bool flag = aX > aY;
            if (flag)
            {
                bool flag2 = aZ > aX;
                if (flag2)
                {
                    bool flag3 = Z > 0f;
                    if (flag3)
                    {
                        O = Vector3.Backward;
                    }
                    else
                    {
                        O = Vector3.Forward;
                    }
                }
                else
                {
                    bool flag4 = aX > aZ;
                    if (flag4)
                    {
                        bool flag5 = X > 0f;
                        if (flag5)
                        {
                            O = Vector3.Right;
                        }
                        else
                        {
                            O = Vector3.Left;
                        }
                    }
                }
            }
            else
            {
                bool flag6 = aZ > aY;
                if (flag6)
                {
                    bool flag7 = Z > 0f;
                    if (flag7)
                    {
                        O = Vector3.Backward;
                    }
                    else
                    {
                        O = Vector3.Forward;
                    }
                }
                else
                {
                    bool flag8 = aY > aZ;
                    if (flag8)
                    {
                        bool flag9 = Y > 0f;
                        if (flag9)
                        {
                            O = Vector3.Up;
                        }
                        else
                        {
                            O = Vector3.Down;
                        }
                    }
                }
            }
            return O;
        }

        // Token: 0x060001C1 RID: 449 RVA: 0x00017450 File Offset: 0x00015650
        public static Direction FindUnitDirectionOfDirection(Vector3 D)
        {
            float X = D.X;
            float Y = D.Y;
            float Z = D.Z;
            float aX = Math.Abs(D.X);
            float aY = Math.Abs(D.Y);
            float aZ = Math.Abs(D.Z);
            Direction O = Direction.Forward;
            bool flag = aX > aY;
            if (flag)
            {
                bool flag2 = aZ > aX;
                if (flag2)
                {
                    bool flag3 = Z > 0f;
                    if (flag3)
                    {
                        O = Direction.Backward;
                    }
                    else
                    {
                        O = Direction.Forward;
                    }
                }
                else
                {
                    bool flag4 = aX > aZ;
                    if (flag4)
                    {
                        bool flag5 = X > 0f;
                        if (flag5)
                        {
                            O = Direction.Right;
                        }
                        else
                        {
                            O = Direction.Left;
                        }
                    }
                }
            }
            else
            {
                bool flag6 = aZ > aY;
                if (flag6)
                {
                    bool flag7 = Z > 0f;
                    if (flag7)
                    {
                        O = Direction.Backward;
                    }
                    else
                    {
                        O = Direction.Forward;
                    }
                }
                else
                {
                    bool flag8 = aY > aZ;
                    if (flag8)
                    {
                        bool flag9 = Y > 0f;
                        if (flag9)
                        {
                            O = Direction.Up;
                        }
                        else
                        {
                            O = Direction.Down;
                        }
                    }
                }
            }
            return O;
        }

        // Token: 0x060001C2 RID: 450 RVA: 0x00017558 File Offset: 0x00015758
        public static Direction Find2DUnitDirectionOfDirection(Vector3 D)
        {
            float X = D.X;
            float Z = D.Z;
            float aX = Math.Abs(D.X);
            float aZ = Math.Abs(D.Z);
            Direction O = Direction.Forward;
            bool flag = aZ > aX;
            if (flag)
            {
                bool flag2 = Z > 0f;
                if (flag2)
                {
                    O = Direction.Backward;
                }
                else
                {
                    O = Direction.Forward;
                }
            }
            else
            {
                bool flag3 = aX > aZ;
                if (flag3)
                {
                    bool flag4 = X > 0f;
                    if (flag4)
                    {
                        O = Direction.Right;
                    }
                    else
                    {
                        O = Direction.Left;
                    }
                }
            }
            return O;
        }

        /// <summary>
        /// 0 = Backward or Forward, 1 = Right or Left
        /// </summary>
        /// <returns></returns>
        // Token: 0x060001C3 RID: 451 RVA: 0x000175E8 File Offset: 0x000157E8
        public static Direction[] Find2DDualDirectionsOfDirection(Vector3 D)
        {
            float X = D.X;
            float Z = D.Z;
            Direction[] O = new Direction[2];
            bool flag = Z > 0f;
            if (flag)
            {
                O[0] = Direction.Backward;
            }
            else
            {
                O[0] = Direction.Forward;
            }
            bool flag2 = X > 0f;
            if (flag2)
            {
                O[1] = Direction.Right;
            }
            else
            {
                O[1] = Direction.Left;
            }
            return O;
        }

        // Token: 0x060001C4 RID: 452 RVA: 0x00017648 File Offset: 0x00015848
        public static Vector3 GetAxisByChar(Matrix Rotaion, char C)
        {
            Vector3 Direction;
            if (Operators.CompareString(Conversions.ToString(C), "D", false) == 0)
            {
                Direction = Rotaion.Down;
            }
            else
            {
                if (Operators.CompareString(Conversions.ToString(C), "U", false) == 0)
                {
                    Direction = Rotaion.Up;
                }
                else
                {
                    bool flag3 = Operators.CompareString(Conversions.ToString(C), "F", false) == 0;
                    if (flag3)
                    {
                        Direction = Rotaion.Forward;
                    }
                    else
                    {
                        bool flag4 = Operators.CompareString(Conversions.ToString(C), "B", false) == 0;
                        if (flag4)
                        {
                            Direction = Rotaion.Backward;
                        }
                        else
                        {
                            bool flag5 = Operators.CompareString(Conversions.ToString(C), "L", false) == 0;
                            if (flag5)
                            {
                                Direction = Rotaion.Left;
                            }
                            else
                            {
                                bool flag6 = Operators.CompareString(Conversions.ToString(C), "R", false) == 0;
                                if (flag6)
                                {
                                    Direction = Rotaion.Right;
                                }
                                else
                                {
                                    Direction = Vector3.Forward;
                                }
                            }
                        }
                    }
                }
            }
            return Direction;
        }

        // Token: 0x060001C5 RID: 453 RVA: 0x0001773C File Offset: 0x0001593C
        public static Vector3 GetAxisByChar(Matrix Rotaion, char C, Vector3 RelativePos)
        {
            bool flag = Operators.CompareString(Conversions.ToString(C), "D", false) == 0;
            Vector3 Direction;
            if (flag)
            {
                Direction = Rotaion.Down;
            }
            else
            {
                bool flag2 = Operators.CompareString(Conversions.ToString(C), "U", false) == 0;
                if (flag2)
                {
                    Direction = Rotaion.Up;
                }
                else
                {
                    bool flag3 = Operators.CompareString(Conversions.ToString(C), "F", false) == 0;
                    if (flag3)
                    {
                        Direction = Rotaion.Forward;
                    }
                    else
                    {
                        bool flag4 = Operators.CompareString(Conversions.ToString(C), "B", false) == 0;
                        if (flag4)
                        {
                            Direction = Rotaion.Backward;
                        }
                        else
                        {
                            bool flag5 = Operators.CompareString(Conversions.ToString(C), "L", false) == 0;
                            if (flag5)
                            {
                                Direction = Rotaion.Left;
                            }
                            else
                            {
                                bool flag6 = Operators.CompareString(Conversions.ToString(C), "R", false) == 0;
                                if (flag6)
                                {
                                    Direction = Rotaion.Right;
                                }
                                else
                                {
                                    Direction = Vector3.Zero;
                                }
                            }
                        }
                    }
                }
            }
            Direction += Vector3.Transform(RelativePos, Rotaion);
            return Direction;
        }

        // Token: 0x060001C6 RID: 454 RVA: 0x0001783C File Offset: 0x00015A3C
        public static Vector3 GetBigerToOne(Vector3 V)
        {
            V.Normalize();
            float X = V.X;
            float Y = V.Y;
            float Z = V.Z;
            float aX = Math.Abs(V.X);
            float aY = Math.Abs(V.Y);
            float aZ = Math.Abs(V.Z);
            Vector3 O = default;
            bool flag = aX > aY;
            if (flag)
            {
                bool flag2 = aZ > aX;
                if (flag2)
                {
                    bool flag3 = Z > 0f;
                    if (flag3)
                    {
                        O.X = 1f / Z * X;
                        O.Y = 1f / Z * Y;
                        O.Z = 1f;
                    }
                    else
                    {
                        O.X = -1f / Z * X;
                        O.Y = -1f / Z * Y;
                        O.Z = -1f;
                    }
                }
                else
                {
                    bool flag4 = aX > aZ;
                    if (flag4)
                    {
                        bool flag5 = X > 0f;
                        if (flag5)
                        {
                            O.Z = 1f / X * Z;
                            O.Y = 1f / X * Y;
                            O.X = 1f;
                        }
                        else
                        {
                            O.Z = -1f / X * Z;
                            O.Y = -1f / X * Y;
                            O.X = -1f;
                        }
                    }
                }
            }
            else
            {
                bool flag6 = aZ > aY;
                if (flag6)
                {
                    bool flag7 = Z > 0f;
                    if (flag7)
                    {
                        O.X = 1f / Z * X;
                        O.Y = 1f / Z * Y;
                        O.Z = 1f;
                    }
                    else
                    {
                        O.X = -1f / Z * X;
                        O.Y = -1f / Z * Y;
                        O.Z = -1f;
                    }
                }
                else
                {
                    bool flag8 = aY > aZ;
                    if (flag8)
                    {
                        bool flag9 = Y > 0f;
                        if (flag9)
                        {
                            O.X = 1f / Y * X;
                            O.Z = 1f / Y * Z;
                            O.Y = 1f;
                        }
                        else
                        {
                            O.X = -1f / Y * X;
                            O.Z = -1f / Y * Z;
                            O.Y = -1f;
                        }
                    }
                }
            }
            return O;
        }

        // Token: 0x040001D8 RID: 472
        public static Vector3 YZero = new Vector3(1f, 0f, 1f);

        // Token: 0x040001D9 RID: 473
        public static Vector3 HalfXYAndYZero = new Vector3(0.5f, 0f, 0.5f);
    }
}
