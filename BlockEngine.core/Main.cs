using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace BlockEngine
{
    // Token: 0x02000025 RID: 37
    [StandardModule]
    public class Main
    {

        // Token: 0x0600016E RID: 366 RVA: 0x000158F7 File Offset: 0x00013AF7
        public static void StartGame()
        {
            Main.GameThrd = new Thread(new ThreadStart(Main.StartGamenewThrd));
            Main.GameThrd.Start();
        }

        // Token: 0x0600016F RID: 367 RVA: 0x0001591B File Offset: 0x00013B1B
        private static void StartGamenewThrd()
        {
            Main.Game = new Game1();
            Main.Started = true;
            Main.Game.Run();
        }

        // Token: 0x06000170 RID: 368 RVA: 0x0001593C File Offset: 0x00013B3C
        public static void Log(string S, bool NewLine = true)
        {
            checked
            {
                if (NewLine)
                {
                    Main.LogContent = string.Concat(new string[]
                    {
                        Conversions.ToString(Main.nLog),
                        " -  ",
                        S,
                        "\r\n",
                        Main.LogContent
                    });
                    Main.nLog += 5L;
                }
                else
                {
                    Main.LogContent = S + Main.LogContent;
                    Main.nLog += 1L;
                }
                bool flag = Main.IsLogerEnabled && Main.LastnLog < Main.nLog - 5L;
                if (flag)
                {
                    Main.Loger1.LogUpdate(Main.nLog);
                    Main.LastnLog = Main.nLog;
                }
            }
        }

        // Token: 0x06000171 RID: 369 RVA: 0x000159F0 File Offset: 0x00013BF0
        public static void Delay(int Millies)
        {
            Thread.Sleep(Millies);
        }

        // Token: 0x06000172 RID: 370 RVA: 0x000159FC File Offset: 0x00013BFC
        public static void RunInNewThread(Delegate Meth, object[] Paras, int Delay = 0)
        {
            Thread Thr = new Thread(new ParameterizedThreadStart((object o) => { Main.ThrRunInNewThread((Tuple<Delegate, object[]>)o); }));


            Thr.Start(new Tuple<Delegate, object[]>(Meth, Paras));
        }

        // Token: 0x06000173 RID: 371 RVA: 0x00015A42 File Offset: 0x00013C42
        private static void ThrRunInNewThread(Tuple<Delegate, object[]> Input)
        {
            Input.Item1.DynamicInvoke(Input.Item2);
        }

        // Token: 0x06000174 RID: 372 RVA: 0x00015A58 File Offset: 0x00013C58
        public static void ExitApp()
        {
            bool started = Main.Started;
            if (started)
            {
                Main.MouseLookEnadbled = false;
                bool flag = Ground.CStack.nSavingChunks > 0;
                if (flag)
                {
                    Loader.SaveChunks(Ground.CStack.SavingChunks, Ground.CStack.nSavingChunks, false);
                    Ground.CStack.nSavingChunks = 0;
                }
                List<XEntity> XeLst = new List<XEntity>();

                foreach (Entity e in Ground.CStack.eList)
                {
                    bool flag2 = !e.IsDead || e.eType.IsPlayer;
                    if (flag2)
                    {
                        XeLst.Add(new XEntity(e));
                    }
                }

                XEntity.Save(Loader.FileEntity, XeLst);
                Loader.MInfo.PlayerPosition = Main.Player1.Position;
                Loader.MInfo.Save(Loader.MapInfoFile);
                Ground.CStack.ChunkList = null;
                Ground.CStack.eList = null;
                Main.Game.ExitGame();
                GC.Collect();
            }
            Main.ExitProgram();
        }

        // Token: 0x06000175 RID: 373 RVA: 0x00015B90 File Offset: 0x00013D90
        public static void ExitProgram()
        {
            Main.LogContent = string.Concat(new string[]
            {
                "Log - ",
                DateTime.Now.ToShortDateString(),
                " ",
                DateTime.Now.ToLongTimeString(),
                "\r\n_______________________________\r\n",
                Main.LogContent
            });
            File.WriteAllText(Main.FileLog, Main.LogContent);
            Process.GetCurrentProcess().Close();
            Process.GetCurrentProcess().Kill();
        }

        // Token: 0x06000176 RID: 374 RVA: 0x00015C20 File Offset: 0x00013E20
        public static void ShowLoger()
        {
            bool flag = !Main.IsLogerEnabled;
            if (flag)
            {
                Main.Loger1 = new FrmLog();
                FrmLog frmLog = Main.Loger1;
                Thread LogThread = new Thread(() =>
                {
                    frmLog.ShowDialog();
                });
                LogThread.Start();
                Main.IsLogerEnabled = true;
            }
            else
            {
                Main.Loger1.BringToFront();
            }
        }

        // Token: 0x06000177 RID: 375 RVA: 0x00015C84 File Offset: 0x00013E84
        public static float GetSunlightIntencity(double Time)
        {
            double h = Time / 60.0;
            bool flag = h < 5.0;
            float GetSunlightIntencity;
            if (flag)
            {
                GetSunlightIntencity = 0.2f;
            }
            else
            {
                bool flag2 = h < 6.0;
                if (flag2)
                {
                    GetSunlightIntencity = (float)((h - 5.0) * 0.4 + 0.2);
                }
                else
                {
                    bool flag3 = h < 8.0;
                    if (flag3)
                    {
                        double i = 6.0;
                        double u = 8.0;
                        double a = 1.3;
                        double b = 0.6;
                        GetSunlightIntencity = (float)((h - i) / (u - i) * (a - b) + b);
                    }
                    else
                    {
                        bool flag4 = h < 10.0;
                        if (flag4)
                        {
                            double i = 8.0;
                            double u = 10.0;
                            double a = 1.4;
                            double b = 1.3;
                            GetSunlightIntencity = (float)((h - i) / (u - i) * (a - b) + b);
                        }
                        else
                        {
                            bool flag5 = h < 13.0;
                            if (flag5)
                            {
                                double i = 10.0;
                                double u = 13.0;
                                double a = 1.55;
                                double b = 1.4;
                                GetSunlightIntencity = (float)((h - i) / (u - i) * (a - b) + b);
                            }
                            else
                            {
                                bool flag6 = h < 15.0;
                                if (flag6)
                                {
                                    double i = 13.0;
                                    double u = 15.0;
                                    double a = 1.55;
                                    double b = 1.35;
                                    GetSunlightIntencity = (float)((u - h) / (u - i) * (a - b) + b);
                                }
                                else
                                {
                                    bool flag7 = h < 17.0;
                                    if (flag7)
                                    {
                                        double i = 15.0;
                                        double u = 17.0;
                                        double a = 1.3;
                                        double b = 1.2;
                                        GetSunlightIntencity = (float)((u - h) / (u - i) * (a - b) + b);
                                    }
                                    else
                                    {
                                        bool flag8 = h < 19.0;
                                        if (flag8)
                                        {
                                            double i = 17.0;
                                            double u = 19.0;
                                            double a = 1.2;
                                            double b = 0.5;
                                            GetSunlightIntencity = (float)((u - h) / (u - i) * (a - b) + b);
                                        }
                                        else
                                        {
                                            bool flag9 = h < 22.0;
                                            if (flag9)
                                            {
                                                double i = 19.0;
                                                double u = 22.0;
                                                double a = 0.5;
                                                double b = 0.3;
                                                GetSunlightIntencity = (float)((u - h) / (u - i) * (a - b) + b);
                                            }
                                            else
                                            {
                                                GetSunlightIntencity = 0.2f;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return GetSunlightIntencity;
        }

        // Token: 0x06000178 RID: 376 RVA: 0x00015F3C File Offset: 0x0001413C
        public static void CloneObject<T>(T originalobject, ref T clone)
        {
            foreach (PropertyInfo p in originalobject.GetType().GetProperties())
            {
                bool canRead = p.CanRead;
                if (canRead)
                {
                    clone.GetType().GetProperty(p.Name).SetValue(clone, RuntimeHelpers.GetObjectValue(p.GetValue(originalobject, null)));
                }
            }
        }
        public enum Platform
        {
            DirectX, DesktopGL
        }


        // Token: 0x04000193 RID: 403
        public static Game1 Game;

        public static Platform platform;

        // Token: 0x04000194 RID: 404
        public static Thread GameThrd;

        // Token: 0x04000195 RID: 405
        public static Viewport Viewport;

        // Token: 0x04000196 RID: 406
        public static long NowGameTime = 0L;

        // Token: 0x04000197 RID: 407
        public static int MinFPS = 50;

        // Token: 0x04000198 RID: 408
        public static int MaxFPS = 58;

        // Token: 0x04000199 RID: 409
        public static float MaxRenderDistance = 10000f;

        // Token: 0x0400019A RID: 410
        public static float RenderDistance = 6000f;

        // Token: 0x0400019B RID: 411
        public static float MinRenderDistance = 5000f;

        // Token: 0x0400019C RID: 412
        public static float FOV;

        // Token: 0x0400019D RID: 413
        public static Player Player1;

        // Token: 0x0400019E RID: 414
        public static List<Entity> DeadEntities = new List<Entity>();

        // Token: 0x0400019F RID: 415
        public static Point MouseDefPosition = new Point(300, 300);

        // Token: 0x040001A0 RID: 416
        public static float MouseSensivity = 0.4f;

        // Token: 0x040001A1 RID: 417
        public static int MouseWheelValue = 0;

        // Token: 0x040001A2 RID: 418
        public static Vector3 cameraPosition = new Vector3(0f, 0f, -2000f);

        // Token: 0x040001A3 RID: 419
        public static Vector3 SunlightDirection = Vector3.Normalize(new Vector3(0f, 1f, -1f));

        // Token: 0x040001A4 RID: 420
        public static Vector3 CameraRelativeYPos = new Vector3(0f, 146.5f, 0f);

        // Token: 0x040001A5 RID: 421
        public static Matrix projectionMatrix;

        // Token: 0x040001A6 RID: 422
        public static Matrix viewMatrix;

        // Token: 0x040001A7 RID: 423
        public static int FPS;

        // Token: 0x040001A8 RID: 424
        public static bool MouseLookEnadbled = true;

        /// <summary>
        /// 1 = 1 min
        /// </summary>
        // Token: 0x040001A9 RID: 425
        public static double TimeOfTheDay;

        // Token: 0x040001AA RID: 426
        public static Color BackColor = Color.LightBlue;

        // Token: 0x040001AB RID: 427
        public static float SunLightIntencity = 1f;

        // Token: 0x040001AC RID: 428
        public static Stopwatch STW = new Stopwatch();

        // Token: 0x040001AD RID: 429
        public static long STWLast;

        // Token: 0x040001AE RID: 430
        public static FrmHUI FHUI;

        // Token: 0x040001AF RID: 431
        public static Form FGame;

        // Token: 0x040001B0 RID: 432
        public static bool RunningSlow = false;

        // Token: 0x040001B1 RID: 433
        public static bool IsGroundChanged = false;

        // Token: 0x040001B2 RID: 434
        public static long CurrUpdateCount = 0L;

        // Token: 0x040001B3 RID: 435
        public static SoundEffect TstSoundEff;

        // Token: 0x040001B4 RID: 436
        public static string CurrentMapName;

        // Token: 0x040001B5 RID: 437
        public static bool Started = false;

        // Token: 0x040001B6 RID: 438
        private static readonly string FileLog = "Log.txt";

        // Token: 0x040001B7 RID: 439
        public static string LogContent = "";

        // Token: 0x040001B8 RID: 440
        public static long nLog = 0L;

        // Token: 0x040001B9 RID: 441
        public static long LastnLog = 0L;

        // Token: 0x040001BA RID: 442
        public static bool IsLogerEnabled = false;

        // Token: 0x040001BB RID: 443
        public static FrmLog Loger1;

        // Token: 0x02000042 RID: 66
        public class MapVariablePipeline
        {
            // Token: 0x04000291 RID: 657
            public static bool NewMap = false;

            // Token: 0x04000292 RID: 658
            public static int NewMapSize;

            // Token: 0x04000293 RID: 659
            public static int NewMapBiome;

            // Token: 0x04000294 RID: 660
            public static bool NewMapSpeedSave;

            // Token: 0x04000295 RID: 661
            public static double GraphicQuality = 1.0;

            // Token: 0x04000296 RID: 662
            public static double LODBias = -0.8;
        }
    }
}
