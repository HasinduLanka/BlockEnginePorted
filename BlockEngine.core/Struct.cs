using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BlockEngine
{
    // Token: 0x02000031 RID: 49
    public class Struct
    {
        // Token: 0x060001F7 RID: 503 RVA: 0x0001A060 File Offset: 0x00018260
        public Struct()
        {
            this.HasEList = false;
            this.eList = new List<XEntity>();
        }

        // Token: 0x060001F8 RID: 504 RVA: 0x0001A07C File Offset: 0x0001827C
        public static void Initialize()
        {
            Struct.Lst = new Struct[101];
            Struct.Lst[10] = Struct.Tree1;
            Struct.Lst[11] = Struct.TreeJak;
            Struct.Lst[30] = Struct.HouseSmall1;
            Struct.Lst[31] = Struct.HouseSmall2;
            Struct.Lst[40] = Struct.WatchTower1;
            Struct.Lst[41] = Struct.Hut1;
            Struct.Choices = new Struct[]
            {
                Struct.Tree1,
                Struct.TreeJak,
                Struct.HouseSmall1,
                Struct.HouseSmall2,
                Struct.WatchTower1,
                Struct.Hut1
            };
        }

        // Token: 0x1700003C RID: 60
        // (get) Token: 0x060001F9 RID: 505 RVA: 0x0001A120 File Offset: 0x00018320
        private static Struct Tree1
        {
            get
            {
                Struct O = new Struct();
                Struct @struct = O;
                @struct.ID = 10;
                @struct.Size3D = new Vector3(6f, 11f, 6f);
                @struct.Length3D = new Vector3(@struct.Size3D.X - 1f, @struct.Size3D.Y - 1f, @struct.Size3D.Z - 1f);
                O.Content = new BlockType[checked((int)Math.Round((double)O.Length3D.X) + 1)][][];
                float x = O.Length3D.X;
                for (float X = 0f; X <= x; X += 1f)
                {
                    float z;
                    checked
                    {
                        O.Content[(int)Math.Round((double)X)] = new BlockType[(int)Math.Round((double)O.Length3D.Z) + 1][];
                        z = O.Length3D.Z;
                    }
                    for (float Z = 0f; Z <= z; Z += 1f)
                    {
                        float y;
                        checked
                        {
                            O.Content[(int)Math.Round((double)X)][(int)Math.Round((double)Z)] = new BlockType[(int)Math.Round((double)O.Length3D.Y) + 1];
                            y = O.Length3D.Y;
                        }
                        for (float Y = 0f; Y <= y; Y += 1f)
                        {
                            checked(O.Content[(int)Math.Round((double)X)][(int)Math.Round((double)Z)][(int)Math.Round((double)Y)]) = BlockType.Air;
                        }
                    }
                }
                O.IsStructArray = new bool[checked((int)Math.Round((double)O.Length3D.X) + 1)][];
                float x2 = O.Length3D.X;
                for (float X2 = 0f; X2 <= x2; X2 += 1f)
                {
                    checked
                    {
                        O.IsStructArray[(int)Math.Round((double)X2)] = new bool[(int)Math.Round((double)O.Length3D.Z) + 1];
                    }
                }
                int X3 = 2;
                checked
                {
                    do
                    {
                        int Z2 = 2;
                        do
                        {
                            int Y2 = 1;
                            do
                            {
                                O.Content[X3][Z2][Y2] = BlockType.PlaceHolder;
                                Y2++;
                            }
                            while (Y2 <= 10);
                            O.IsStructArray[X3][Z2] = true;
                            Z2++;
                        }
                        while (Z2 <= 4);
                        X3++;
                    }
                    while (X3 <= 4);
                    O.Content[3][3][0] = BlockType.Tree1;
                    O.IsStructArray[3][3] = true;
                    return O;
                }
            }
        }

        // Token: 0x1700003D RID: 61
        // (get) Token: 0x060001FA RID: 506 RVA: 0x0001A398 File Offset: 0x00018598
        private static Struct TreeJak
        {
            get
            {
                Struct O = new Struct();
                Struct @struct = O;
                @struct.ID = 11;
                @struct.Size3D = new Vector3(7f, 15f, 7f);
                @struct.Length3D = new Vector3(@struct.Size3D.X - 1f, @struct.Size3D.Y - 1f, @struct.Size3D.Z - 1f);
                O.Content = new BlockType[checked((int)Math.Round((double)O.Length3D.X) + 1)][][];
                float x = O.Length3D.X;
                for (float X = 0f; X <= x; X += 1f)
                {
                    float z;
                    checked
                    {
                        O.Content[(int)Math.Round((double)X)] = new BlockType[(int)Math.Round((double)O.Length3D.Z) + 1][];
                        z = O.Length3D.Z;
                    }
                    for (float Z = 0f; Z <= z; Z += 1f)
                    {
                        float y;
                        checked
                        {
                            O.Content[(int)Math.Round((double)X)][(int)Math.Round((double)Z)] = new BlockType[(int)Math.Round((double)O.Length3D.Y) + 1];
                            y = O.Length3D.Y;
                        }
                        for (float Y = 0f; Y <= y; Y += 1f)
                        {
                            checked(O.Content[(int)Math.Round((double)X)][(int)Math.Round((double)Z)][(int)Math.Round((double)Y)]) = BlockType.Air;
                        }
                    }
                }
                O.IsStructArray = new bool[checked((int)Math.Round((double)O.Length3D.X) + 1)][];
                float x2 = O.Length3D.X;
                for (float X2 = 0f; X2 <= x2; X2 += 1f)
                {
                    checked
                    {
                        O.IsStructArray[(int)Math.Round((double)X2)] = new bool[(int)Math.Round((double)O.Length3D.Z) + 1];
                    }
                }
                int X3 = 3;
                checked
                {
                    do
                    {
                        int Z2 = 3;
                        do
                        {
                            int Y2 = 1;
                            do
                            {
                                O.Content[X3][Z2][Y2] = BlockType.PlaceHolder;
                                Y2++;
                            }
                            while (Y2 <= 14);
                            O.IsStructArray[X3][Z2] = true;
                            Z2++;
                        }
                        while (Z2 <= 5);
                        X3++;
                    }
                    while (X3 <= 5);
                    O.Content[4][4][0] = BlockType.Jak;
                    O.IsStructArray[4][4] = true;
                    return O;
                }
            }
        }

        // Token: 0x1700003E RID: 62
        // (get) Token: 0x060001FB RID: 507 RVA: 0x0001A610 File Offset: 0x00018810
        private static Struct HouseSmall1
        {
            get
            {
                Struct O = new Struct();
                Struct @struct = O;
                @struct.ID = 30;
                @struct.Size3D = new Vector3(11f, 12f, 11f);
                @struct.Length3D = new Vector3(@struct.Size3D.X - 1f, @struct.Size3D.Y - 1f, @struct.Size3D.Z - 1f);
                O.Content = new BlockType[checked((int)Math.Round((double)O.Length3D.X) + 1)][][];
                float x = O.Length3D.X;
                for (float X = 0f; X <= x; X += 1f)
                {
                    float z;
                    checked
                    {
                        O.Content[(int)Math.Round((double)X)] = new BlockType[(int)Math.Round((double)O.Length3D.Z) + 1][];
                        z = O.Length3D.Z;
                    }
                    for (float Z = 0f; Z <= z; Z += 1f)
                    {
                        float y;
                        checked
                        {
                            O.Content[(int)Math.Round((double)X)][(int)Math.Round((double)Z)] = new BlockType[(int)Math.Round((double)O.Length3D.Y) + 1];
                            y = O.Length3D.Y;
                        }
                        for (float Y = 0f; Y <= y; Y += 1f)
                        {
                            checked(O.Content[(int)Math.Round((double)X)][(int)Math.Round((double)Z)][(int)Math.Round((double)Y)]) = BlockType.Air;
                        }
                    }
                }
                O.IsStructArray = new bool[checked((int)Math.Round((double)O.Length3D.X) + 1)][];
                float x2 = O.Length3D.X;
                for (float X2 = 0f; X2 <= x2; X2 += 1f)
                {
                    checked
                    {
                        O.IsStructArray[(int)Math.Round((double)X2)] = new bool[(int)Math.Round((double)O.Length3D.Z) + 1];
                    }
                }
                int X3 = 1;
                checked
                {
                    do
                    {
                        int Z2 = 1;
                        do
                        {
                            O.Content[X3][Z2][0] = BlockType.Stone;
                            O.IsStructArray[X3][Z2] = true;
                            Z2++;
                        }
                        while (Z2 <= 9);
                        X3++;
                    }
                    while (X3 <= 9);
                    int X4 = 1;
                    do
                    {
                        int Y2 = 0;
                        do
                        {
                            O.Content[X4][1][Y2] = BlockType.Brick;
                            Y2++;
                        }
                        while (Y2 <= 5);
                        O.IsStructArray[X4][1] = true;
                        X4++;
                    }
                    while (X4 <= 9);
                    int Y3 = 1;
                    do
                    {
                        O.Content[5][1][Y3] = BlockType.Air;
                        Y3++;
                    }
                    while (Y3 <= 3);
                    int X5 = 1;
                    do
                    {
                        int Y4 = 0;
                        do
                        {
                            O.Content[X5][9][Y4] = BlockType.Brick;
                            Y4++;
                        }
                        while (Y4 <= 5);
                        O.IsStructArray[X5][9] = true;
                        X5++;
                    }
                    while (X5 <= 9);
                    int Z3 = 1;
                    do
                    {
                        int Y5 = 0;
                        do
                        {
                            O.Content[1][Z3][Y5] = BlockType.Brick;
                            Y5++;
                        }
                        while (Y5 <= 5);
                        O.IsStructArray[1][Z3] = true;
                        Z3++;
                    }
                    while (Z3 <= 9);
                    int Z4 = 1;
                    do
                    {
                        int Y6 = 0;
                        do
                        {
                            O.Content[9][Z4][Y6] = BlockType.Brick;
                            Y6++;
                        }
                        while (Y6 <= 5);
                        O.IsStructArray[9][Z4] = true;
                        Z4++;
                    }
                    while (Z4 <= 9);
                    int X6 = 0;
                    do
                    {
                        int Z5 = 0;
                        do
                        {
                            O.Content[X6][Z5][6] = BlockType.WoodPlank;
                            O.IsStructArray[X6][Z5] = true;
                            Z5++;
                        }
                        while (Z5 <= 10);
                        X6++;
                    }
                    while (X6 <= 10);
                    int X7 = 1;
                    do
                    {
                        int Z6 = 1;
                        do
                        {
                            O.Content[X7][Z6][6] = BlockType.Air;
                            Z6++;
                        }
                        while (Z6 <= 9);
                        X7++;
                    }
                    while (X7 <= 9);
                    int X8 = 1;
                    do
                    {
                        int Z7 = 1;
                        do
                        {
                            O.Content[X8][Z7][7] = BlockType.WoodPlank;
                            Z7++;
                        }
                        while (Z7 <= 9);
                        X8++;
                    }
                    while (X8 <= 9);
                    int X9 = 2;
                    do
                    {
                        int Z8 = 2;
                        do
                        {
                            O.Content[X9][Z8][7] = BlockType.Air;
                            Z8++;
                        }
                        while (Z8 <= 8);
                        X9++;
                    }
                    while (X9 <= 8);
                    int X10 = 2;
                    do
                    {
                        int Z9 = 2;
                        do
                        {
                            O.Content[X10][Z9][8] = BlockType.WoodPlank;
                            Z9++;
                        }
                        while (Z9 <= 8);
                        X10++;
                    }
                    while (X10 <= 8);
                    int X11 = 3;
                    do
                    {
                        int Z10 = 3;
                        do
                        {
                            O.Content[X11][Z10][8] = BlockType.Air;
                            Z10++;
                        }
                        while (Z10 <= 7);
                        X11++;
                    }
                    while (X11 <= 7);
                    int X12 = 3;
                    do
                    {
                        int Z11 = 3;
                        do
                        {
                            O.Content[X12][Z11][9] = BlockType.WoodPlank;
                            Z11++;
                        }
                        while (Z11 <= 7);
                        X12++;
                    }
                    while (X12 <= 7);
                    int X13 = 4;
                    do
                    {
                        int Z12 = 4;
                        do
                        {
                            O.Content[X13][Z12][9] = BlockType.Air;
                            Z12++;
                        }
                        while (Z12 <= 6);
                        X13++;
                    }
                    while (X13 <= 6);
                    int X14 = 4;
                    do
                    {
                        int Z13 = 4;
                        do
                        {
                            O.Content[X14][Z13][10] = BlockType.WoodPlank;
                            Z13++;
                        }
                        while (Z13 <= 6);
                        X14++;
                    }
                    while (X14 <= 6);
                    O.HasEList = true;
                    Human H = Human.AddNewHuman(EntityTypes.Civilian, "CivilianGenHouseSmall1", new Vector3(200f, 150f, 200f), null, 1);
                    O.eList.Add(new XEntity(H));
                    return O;
                }
            }
        }

        // Token: 0x1700003F RID: 63
        // (get) Token: 0x060001FC RID: 508 RVA: 0x0001AB70 File Offset: 0x00018D70
        private static Struct HouseSmall2
        {
            get
            {
                Struct O = new Struct();
                Struct @struct = O;
                @struct.ID = 31;
                @struct.Size3D = new Vector3(22f, 12f, 22f);
                @struct.Length3D = new Vector3(@struct.Size3D.X - 1f, @struct.Size3D.Y - 1f, @struct.Size3D.Z - 1f);
                O.Content = new BlockType[checked((int)Math.Round((double)O.Length3D.X) + 1)][][];
                float x = O.Length3D.X;
                for (float X = 0f; X <= x; X += 1f)
                {
                    float z;
                    checked
                    {
                        O.Content[(int)Math.Round((double)X)] = new BlockType[(int)Math.Round((double)O.Length3D.Z) + 1][];
                        z = O.Length3D.Z;
                    }
                    for (float Z = 0f; Z <= z; Z += 1f)
                    {
                        float y;
                        checked
                        {
                            O.Content[(int)Math.Round((double)X)][(int)Math.Round((double)Z)] = new BlockType[(int)Math.Round((double)O.Length3D.Y) + 1];
                            y = O.Length3D.Y;
                        }
                        for (float Y = 0f; Y <= y; Y += 1f)
                        {
                            checked(O.Content[(int)Math.Round((double)X)][(int)Math.Round((double)Z)][(int)Math.Round((double)Y)]) = BlockType.Air;
                        }
                    }
                }
                O.IsStructArray = new bool[checked((int)Math.Round((double)O.Length3D.X) + 1)][];
                float x2 = O.Length3D.X;
                for (float X2 = 0f; X2 <= x2; X2 += 1f)
                {
                    checked
                    {
                        O.IsStructArray[(int)Math.Round((double)X2)] = new bool[(int)Math.Round((double)O.Length3D.Z) + 1];
                    }
                }
                int X3 = 1;
                checked
                {
                    do
                    {
                        int Z2 = 1;
                        do
                        {
                            O.Content[X3][Z2][0] = BlockType.Stone;
                            O.IsStructArray[X3][Z2] = true;
                            Z2++;
                        }
                        while (Z2 <= 20);
                        X3++;
                    }
                    while (X3 <= 20);
                    int X4 = 1;
                    do
                    {
                        int Y2 = 0;
                        do
                        {
                            O.Content[X4][1][Y2] = BlockType.Brick;
                            Y2++;
                        }
                        while (Y2 <= 5);
                        X4++;
                    }
                    while (X4 <= 20);
                    int Y3 = 1;
                    do
                    {
                        O.Content[5][1][Y3] = BlockType.Air;
                        Y3++;
                    }
                    while (Y3 <= 3);
                    int X5 = 1;
                    do
                    {
                        int Y4 = 0;
                        do
                        {
                            O.Content[X5][20][Y4] = BlockType.Brick;
                            Y4++;
                        }
                        while (Y4 <= 5);
                        X5++;
                    }
                    while (X5 <= 20);
                    int Z3 = 1;
                    do
                    {
                        int Y5 = 0;
                        do
                        {
                            O.Content[1][Z3][Y5] = BlockType.Brick;
                            Y5++;
                        }
                        while (Y5 <= 5);
                        Z3++;
                    }
                    while (Z3 <= 20);
                    int Z4 = 1;
                    do
                    {
                        int Y6 = 0;
                        do
                        {
                            O.Content[20][Z4][Y6] = BlockType.Brick;
                            Y6++;
                        }
                        while (Y6 <= 5);
                        Z4++;
                    }
                    while (Z4 <= 20);
                    int X6 = 1;
                    do
                    {
                        int Y7 = 0;
                        do
                        {
                            O.Content[X6][10][Y7] = BlockType.Brick;
                            Y7++;
                        }
                        while (Y7 <= 5);
                        X6++;
                    }
                    while (X6 <= 20);
                    int Z5 = 10;
                    do
                    {
                        int Y8 = 0;
                        do
                        {
                            O.Content[10][Z5][Y8] = BlockType.Brick;
                            Y8++;
                        }
                        while (Y8 <= 5);
                        Z5++;
                    }
                    while (Z5 <= 20);
                    int Y9 = 1;
                    do
                    {
                        O.Content[17][10][Y9] = BlockType.Air;
                        Y9++;
                    }
                    while (Y9 <= 3);
                    int Y10 = 1;
                    do
                    {
                        O.Content[4][10][Y10] = BlockType.Air;
                        Y10++;
                    }
                    while (Y10 <= 3);
                    int X7 = 0;
                    do
                    {
                        int Z6 = 0;
                        do
                        {
                            O.Content[X7][Z6][6] = BlockType.WoodPlank;
                            O.IsStructArray[X7][Z6] = true;
                            Z6++;
                        }
                        while (Z6 <= 21);
                        X7++;
                    }
                    while (X7 <= 21);
                    int X8 = 1;
                    do
                    {
                        int Z7 = 1;
                        do
                        {
                            O.Content[X8][Z7][6] = BlockType.Air;
                            Z7++;
                        }
                        while (Z7 <= 20);
                        X8++;
                    }
                    while (X8 <= 20);
                    int X9 = 1;
                    do
                    {
                        int Z8 = 1;
                        do
                        {
                            O.Content[X9][Z8][7] = BlockType.WoodPlank;
                            Z8++;
                        }
                        while (Z8 <= 20);
                        X9++;
                    }
                    while (X9 <= 20);
                    int X10 = 2;
                    do
                    {
                        int Z9 = 2;
                        do
                        {
                            O.Content[X10][Z9][7] = BlockType.Air;
                            Z9++;
                        }
                        while (Z9 <= 19);
                        X10++;
                    }
                    while (X10 <= 19);
                    int X11 = 2;
                    do
                    {
                        int Z10 = 2;
                        do
                        {
                            O.Content[X11][Z10][8] = BlockType.WoodPlank;
                            Z10++;
                        }
                        while (Z10 <= 19);
                        X11++;
                    }
                    while (X11 <= 19);
                    int X12 = 3;
                    do
                    {
                        int Z11 = 3;
                        do
                        {
                            O.Content[X12][Z11][8] = BlockType.Air;
                            Z11++;
                        }
                        while (Z11 <= 18);
                        X12++;
                    }
                    while (X12 <= 18);
                    int X13 = 3;
                    do
                    {
                        int Z12 = 3;
                        do
                        {
                            O.Content[X13][Z12][9] = BlockType.WoodPlank;
                            Z12++;
                        }
                        while (Z12 <= 18);
                        X13++;
                    }
                    while (X13 <= 18);
                    int X14 = 4;
                    do
                    {
                        int Z13 = 4;
                        do
                        {
                            O.Content[X14][Z13][9] = BlockType.Air;
                            Z13++;
                        }
                        while (Z13 <= 17);
                        X14++;
                    }
                    while (X14 <= 17);
                    int X15 = 4;
                    do
                    {
                        int Z14 = 4;
                        do
                        {
                            O.Content[X15][Z14][10] = BlockType.WoodPlank;
                            Z14++;
                        }
                        while (Z14 <= 17);
                        X15++;
                    }
                    while (X15 <= 17);
                    O.HasEList = true;
                    Human H = Human.AddNewHuman(EntityTypes.Civilian, "CivilianGenHouseSmall2", new Vector3(200f, 150f, 200f), null, 1);
                    O.eList.Add(new XEntity(H));
                    return O;
                }
            }
        }

        // Token: 0x17000040 RID: 64
        // (get) Token: 0x060001FD RID: 509 RVA: 0x0001B150 File Offset: 0x00019350
        private static Struct WatchTower1
        {
            get
            {
                Struct O = new Struct();
                Struct @struct = O;
                @struct.ID = 40;
                @struct.Size3D = new Vector3(11f, 20f, 11f);
                @struct.Length3D = new Vector3(@struct.Size3D.X - 1f, @struct.Size3D.Y - 1f, @struct.Size3D.Z - 1f);
                O.Content = new BlockType[checked((int)Math.Round((double)O.Length3D.X) + 1)][][];
                float x = O.Length3D.X;
                for (float X = 0f; X <= x; X += 1f)
                {
                    float z;
                    checked
                    {
                        O.Content[(int)Math.Round((double)X)] = new BlockType[(int)Math.Round((double)O.Length3D.Z) + 1][];
                        z = O.Length3D.Z;
                    }
                    for (float Z = 0f; Z <= z; Z += 1f)
                    {
                        float y;
                        checked
                        {
                            O.Content[(int)Math.Round((double)X)][(int)Math.Round((double)Z)] = new BlockType[(int)Math.Round((double)O.Length3D.Y) + 1];
                            y = O.Length3D.Y;
                        }
                        for (float Y = 0f; Y <= y; Y += 1f)
                        {
                            checked(O.Content[(int)Math.Round((double)X)][(int)Math.Round((double)Z)][(int)Math.Round((double)Y)]) = BlockType.Air;
                        }
                    }
                }
                O.IsStructArray = new bool[checked((int)Math.Round((double)O.Length3D.X) + 1)][];
                float x2 = O.Length3D.X;
                for (float X2 = 0f; X2 <= x2; X2 += 1f)
                {
                    checked
                    {
                        O.IsStructArray[(int)Math.Round((double)X2)] = new bool[(int)Math.Round((double)O.Length3D.Z) + 1];
                    }
                }
                int Y2 = 0;
                checked
                {
                    do
                    {
                        O.Content[0][0][Y2] = BlockType.Wood;
                        Y2++;
                    }
                    while (Y2 <= 16);
                    O.IsStructArray[0][0] = true;
                    O.Content[0][0][1] = BlockType.Brick;
                    int Y3 = 0;
                    do
                    {
                        O.Content[10][0][Y3] = BlockType.Wood;
                        Y3++;
                    }
                    while (Y3 <= 16);
                    O.IsStructArray[10][0] = true;
                    O.Content[10][0][1] = BlockType.Brick;
                    int Y4 = 0;
                    do
                    {
                        O.Content[10][10][Y4] = BlockType.Wood;
                        Y4++;
                    }
                    while (Y4 <= 16);
                    O.IsStructArray[10][10] = true;
                    O.Content[10][10][1] = BlockType.Brick;
                    int Y5 = 0;
                    do
                    {
                        O.Content[0][10][Y5] = BlockType.Wood;
                        Y5++;
                    }
                    while (Y5 <= 16);
                    O.IsStructArray[0][10] = true;
                    O.Content[0][10][1] = BlockType.Brick;
                    int X3 = 0;
                    do
                    {
                        int Z2 = 0;
                        do
                        {
                            O.Content[X3][Z2][11] = BlockType.Wood;
                            O.IsStructArray[X3][Z2] = true;
                            Z2++;
                        }
                        while (Z2 <= 10);
                        X3++;
                    }
                    while (X3 <= 10);
                    int X4 = 1;
                    do
                    {
                        int Z3 = 1;
                        do
                        {
                            O.Content[X4][Z3][11] = BlockType.WoodPlank;
                            Z3++;
                        }
                        while (Z3 <= 9);
                        X4++;
                    }
                    while (X4 <= 9);
                    int X5 = 0;
                    do
                    {
                        int Z4 = 0;
                        do
                        {
                            O.Content[X5][Z4][16] = BlockType.Wood;
                            Z4++;
                        }
                        while (Z4 <= 10);
                        X5++;
                    }
                    while (X5 <= 10);
                    int X6 = 1;
                    do
                    {
                        int Z5 = 1;
                        do
                        {
                            O.Content[X6][Z5][16] = BlockType.WoodPlank;
                            Z5++;
                        }
                        while (Z5 <= 9);
                        X6++;
                    }
                    while (X6 <= 9);
                    int X7 = 0;
                    do
                    {
                        int Z6 = 0;
                        do
                        {
                            O.Content[X7][Z6][12] = BlockType.WoodPlank;
                            Z6++;
                        }
                        while (Z6 <= 10);
                        X7++;
                    }
                    while (X7 <= 10);
                    int X8 = 1;
                    do
                    {
                        int Z7 = 1;
                        do
                        {
                            O.Content[X8][Z7][12] = BlockType.Air;
                            Z7++;
                        }
                        while (Z7 <= 9);
                        X8++;
                    }
                    while (X8 <= 9);
                    O.HasEList = true;
                    Human H = Human.AddNewHuman(EntityTypes.Guard, "WatchToerGuard", new Vector3(200f, 700f, 200f), Tool.Buy(PhysicsFuncs.RandomOf<int>(new int[]
                    {
                        Tool.iSword1,
                        Tool.iRSword
                    })), 1);
                    H.InRandomMovement = true;
                    H.InRandomMovementTime = 100000;
                    H.CRandomMovement = Actions.Null;
                    O.eList.Add(XEntity.NewXE(H, new Vector3(200f, 700f, 200f)));
                    O.eList.Add(XEntity.NewXE(H, new Vector3(300f, 700f, 300f)));
                    return O;
                }
            }
        }

        // Token: 0x17000041 RID: 65
        // (get) Token: 0x060001FE RID: 510 RVA: 0x0001B64C File Offset: 0x0001984C
        private static Struct Hut1
        {
            get
            {
                Struct O = new Struct();
                Struct @struct = O;
                @struct.ID = 41;
                @struct.Size3D = new Vector3(6f, 8f, 6f);
                @struct.Length3D = new Vector3(@struct.Size3D.X - 1f, @struct.Size3D.Y - 1f, @struct.Size3D.Z - 1f);
                O.Content = new BlockType[checked((int)Math.Round((double)O.Length3D.X) + 1)][][];
                float x = O.Length3D.X;
                for (float X = 0f; X <= x; X += 1f)
                {
                    float z;
                    checked
                    {
                        O.Content[(int)Math.Round((double)X)] = new BlockType[(int)Math.Round((double)O.Length3D.Z) + 1][];
                        z = O.Length3D.Z;
                    }
                    for (float Z = 0f; Z <= z; Z += 1f)
                    {
                        float y;
                        checked
                        {
                            O.Content[(int)Math.Round((double)X)][(int)Math.Round((double)Z)] = new BlockType[(int)Math.Round((double)O.Length3D.Y) + 1];
                            y = O.Length3D.Y;
                        }
                        for (float Y = 0f; Y <= y; Y += 1f)
                        {
                            checked(O.Content[(int)Math.Round((double)X)][(int)Math.Round((double)Z)][(int)Math.Round((double)Y)]) = BlockType.Air;
                        }
                    }
                }
                O.IsStructArray = new bool[checked((int)Math.Round((double)O.Length3D.X) + 1)][];
                float x2 = O.Length3D.X;
                for (float X2 = 0f; X2 <= x2; X2 += 1f)
                {
                    checked
                    {
                        O.IsStructArray[(int)Math.Round((double)X2)] = new bool[(int)Math.Round((double)O.Length3D.Z) + 1];
                    }
                }
                int X3 = 0;
                checked
                {
                    do
                    {
                        int Y2 = 0;
                        do
                        {
                            O.Content[X3][0][Y2] = BlockType.WoodPlank;
                            Y2++;
                        }
                        while (Y2 <= 7);
                        X3++;
                    }
                    while (X3 <= 5);
                    int X4 = 0;
                    do
                    {
                        int Y3 = 0;
                        do
                        {
                            O.Content[X4][5][Y3] = BlockType.WoodPlank;
                            Y3++;
                        }
                        while (Y3 <= 7);
                        X4++;
                    }
                    while (X4 <= 5);
                    int Z2 = 0;
                    do
                    {
                        int Y4 = 0;
                        do
                        {
                            O.Content[0][Z2][Y4] = BlockType.WoodPlank;
                            Y4++;
                        }
                        while (Y4 <= 7);
                        Z2++;
                    }
                    while (Z2 <= 5);
                    int Z3 = 0;
                    do
                    {
                        int Y5 = 0;
                        do
                        {
                            O.Content[5][Z3][Y5] = BlockType.WoodPlank;
                            Y5++;
                        }
                        while (Y5 <= 7);
                        Z3++;
                    }
                    while (Z3 <= 5);
                    int Y6 = 1;
                    do
                    {
                        O.Content[0][2][Y6] = BlockType.Air;
                        Y6++;
                    }
                    while (Y6 <= 3);
                    int X5 = 0;
                    do
                    {
                        int Z4 = 0;
                        do
                        {
                            O.Content[X5][Z4][0] = BlockType.Dirt;
                            O.IsStructArray[X5][Z4] = true;
                            Z4++;
                        }
                        while (Z4 <= 5);
                        X5++;
                    }
                    while (X5 <= 5);
                    int X6 = 0;
                    do
                    {
                        int Z5 = 0;
                        do
                        {
                            O.Content[X6][Z5][6] = BlockType.Wood;
                            Z5++;
                        }
                        while (Z5 <= 5);
                        X6++;
                    }
                    while (X6 <= 5);
                    O.HasEList = true;
                    Human H = Human.AddNewHuman(PhysicsFuncs.RandomOf<EntityType>(new EntityType[]
                    {
                        EntityTypes.Human1,
                        EntityTypes.Murderer
                    }), "HutGen", new Vector3(100f, 50f, 100f), Tool.Buy(PhysicsFuncs.RandomOf<int>(new int[]
                    {
                        Tool.iSword1,
                        Tool.iRSword
                    })), 1);
                    H.InRandomMovement = true;
                    H.InRandomMovementTime = 100000;
                    H.CRandomMovement = Actions.Null;
                    O.eList.Add(XEntity.NewXE(H, new Vector3(100f, 100f, 100f)));
                    return O;
                }
            }
        }

        // Token: 0x060001FF RID: 511 RVA: 0x0001BA44 File Offset: 0x00019C44
        public static BlockType[][][] GenerateStructMap(ref HeightMap HM, ref bool[][] IsStruct, ref byte[][] Heights, ref List<XEntity> eList)
        {
            checked
            {
                BlockType[][][] Bars = new BlockType[HeightMap.Size - 1 + 1][][];
                IsStruct = new bool[HeightMap.Size - 1 + 1][];
                Heights = new byte[HeightMap.Size - 1 + 1][];
                int num = HeightMap.Size - 1;
                for (int X = 0; X <= num; X++)
                {
                    IsStruct[X] = new bool[HeightMap.Size - 1 + 1];
                    Bars[X] = new BlockType[HeightMap.Size - 1 + 1][];
                    Heights[X] = new byte[HeightMap.Size - 1 + 1];
                }
                int BiomeStructCount = HM.Bm.Structs.Length;
                int num2 = HeightMap.Size - 10;
                for (int X2 = 10; X2 <= num2; X2++)
                {
                    int num3 = HeightMap.Size - 10;
                    for (int Z = 10; Z <= num3; Z++)
                    {
                        bool flag = !IsStruct[X2][Z];
                        unchecked
                        {
                            if (flag)
                            {
                                Struct S;
                                for (; ; )
                                {
                                    int R = Funcs.RND.Next(BiomeStructCount);
                                    bool flag2 = Funcs.RND.NextDouble() < HM.Bm.StructsRarity[R];
                                    if (!flag2)
                                    {
                                        break;
                                    }
                                    S = Struct.Lst[(int)HM.Bm.Structs[R]];
                                    bool flag3 = (float)HeightMap.Size <= (float)X2 + S.Length3D.X || (float)HeightMap.Size <= (float)Z + S.Length3D.Z;
                                    if (!flag3)
                                    {
                                        goto IL_153;
                                    }
                                }
                                goto IL_392;
                                IL_153:
                                bool IsIntersects = false;
                                int BaseHeightTotal = 0;
                                float x = S.Length3D.X;
                                for (float SX = 0f; SX <= x; SX += 1f)
                                {
                                    float z = S.Length3D.Z;
                                    for (float SZ = 0f; SZ <= z; SZ += 1f)
                                    {
                                        checked
                                        {
                                            BaseHeightTotal += (int)HM.B[(int)Math.Round((double)(unchecked((float)X2 + SX)))][(int)Math.Round((double)(unchecked((float)Z + SZ)))];
                                            bool flag4 = IsStruct[(int)Math.Round((double)(unchecked((float)X2 + SX)))][(int)Math.Round((double)(unchecked((float)Z + SZ)))];
                                            if (flag4)
                                            {
                                                IsIntersects = true;
                                                break;
                                            }
                                        }
                                    }
                                    bool flag5 = IsIntersects;
                                    if (flag5)
                                    {
                                        break;
                                    }
                                }
                                bool flag6 = !IsIntersects;
                                if (flag6)
                                {
                                    int StructureBaseHeight;
                                    float x2;
                                    checked
                                    {
                                        StructureBaseHeight = (int)((double)((float)BaseHeightTotal / unchecked(S.Size3D.X * S.Size3D.Z)));
                                        Struct.CloneXELise(S.eList, eList, new Vector3((float)(X2 * 50), (float)(StructureBaseHeight * 50), (float)(Z * 50)));
                                        x2 = S.Length3D.X;
                                    }
                                    for (float SX2 = 0f; SX2 <= x2; SX2 += 1f)
                                    {
                                        float z2 = S.Length3D.Z;
                                        for (float SZ2 = 0f; SZ2 <= z2; SZ2 += 1f)
                                        {
                                            checked
                                            {
                                                IsStruct[(int)Math.Round((double)(unchecked((float)X2 + SX2)))][(int)Math.Round((double)(unchecked((float)Z + SZ2)))] = true;
                                                bool flag7 = S.IsStructArray[(int)Math.Round((double)SX2)][(int)Math.Round((double)SZ2)];
                                                if (flag7)
                                                {
                                                    HM.B[(int)Math.Round((double)(unchecked((float)X2 + SX2)))][(int)Math.Round((double)(unchecked((float)Z + SZ2)))] = (byte)StructureBaseHeight;
                                                    Bars[(int)Math.Round((double)(unchecked((float)X2 + SX2)))][(int)Math.Round((double)(unchecked((float)Z + SZ2)))] = S.Content[(int)Math.Round((double)SX2)][(int)Math.Round((double)SZ2)];
                                                    Heights[(int)Math.Round((double)(unchecked((float)X2 + SX2)))][(int)Math.Round((double)(unchecked((float)Z + SZ2)))] = (byte)Math.Round((double)S.Length3D.Y);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            IL_392:;
                        }
                    }
                }
                return Bars;
            }
        }

        // Token: 0x06000200 RID: 512 RVA: 0x0001BE08 File Offset: 0x0001A008
        private static void CloneXELise(List<XEntity> XELst, List<XEntity> Dest, Vector3 Pos)
        {
            checked
            {
                foreach (XEntity XE in XELst)
                {
                    Entity.LICode += 1;
                    Dest.Add(new XEntity
                    {
                        Accelaration = XE.Accelaration,
                        CRandomMovement = XE.CRandomMovement,
                        CRandomMovementTime = XE.CRandomMovementTime,
                        CTool = XE.CTool,
                        Enemies = XE.Enemies,
                        FacingDirection = XE.FacingDirection,
                        FallingSpeed = XE.FallingSpeed,
                        Friendlies = XE.Friendlies,
                        Health = XE.Health,
                        MaxHealth = XE.MaxHealth,
                        HighestTargetMode = XE.HighestTargetMode,
                        HighestTatget = XE.HighestTatget,
                        HighestTatgetE = XE.HighestTatgetE,
                        ICode = (int)Entity.LICode,
                        InRandomMovement = XE.InRandomMovement,
                        InRandomMovementTime = XE.InRandomMovementTime,
                        IsDead = XE.IsDead,
                        Jumping = XE.Jumping,
                        MaxVelocity = XE.MaxVelocity,
                        ModelVelocity = XE.ModelVelocity,
                        MovedFB = XE.MovedFB,
                        Name = XE.Name,
                        NoTarget = XE.NoTarget,
                        OnGround = XE.OnGround,
                        Position = XE.Position + Pos,
                        RotationAccelaration = XE.RotationAccelaration,
                        RotationVelocity = XE.RotationVelocity,
                        Strength = XE.Strength,
                        Target = XE.Target,
                        TargetLockedAIs = XE.TargetLockedAIs,
                        TargetMode = XE.TargetMode,
                        Tools = XE.Tools,
                        TypeID = XE.TypeID,
                        WalkingFw = XE.WalkingFw,
                        Weight = XE.Weight,
                        XP = XE.XP
                    });
                }

            }
        }

        // Token: 0x0400020F RID: 527
        public byte ID;

        // Token: 0x04000210 RID: 528
        public Vector3 Size3D;

        /// <summary>
        /// Size3D - 1
        /// </summary>
        // Token: 0x04000211 RID: 529
        public Vector3 Length3D;

        // Token: 0x04000212 RID: 530
        public bool[][] IsStructArray;

        /// <summary>
        /// X Z Y
        /// </summary>
        // Token: 0x04000213 RID: 531
        public BlockType[][][] Content;

        // Token: 0x04000214 RID: 532
        public bool HasEList;

        // Token: 0x04000215 RID: 533
        public List<XEntity> eList;

        // Token: 0x04000216 RID: 534
        public static Struct[] Lst;

        // Token: 0x04000217 RID: 535
        public static Struct[] Choices;

        // Token: 0x02000045 RID: 69
        public enum EnumStructs : byte
        {
            // Token: 0x0400029B RID: 667
            None,
            // Token: 0x0400029C RID: 668
            PlaceHolder,
            // Token: 0x0400029D RID: 669
            Tree1 = 10,
            // Token: 0x0400029E RID: 670
            TreeJak,
            // Token: 0x0400029F RID: 671
            HouseSmall1 = 30,
            // Token: 0x040002A0 RID: 672
            HouseSmall2,
            // Token: 0x040002A1 RID: 673
            WatchTower1 = 40,
            // Token: 0x040002A2 RID: 674
            Hut1
        }
    }
}
