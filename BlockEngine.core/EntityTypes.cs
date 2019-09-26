using System;
using System.Collections.Generic;

namespace BlockEngine
{
	// Token: 0x02000017 RID: 23
	public class EntityTypes
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00008C04 File Offset: 0x00006E04
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00008C0D File Offset: 0x00006E0D
		public static EntityType Human1 { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00008C15 File Offset: 0x00006E15
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00008C1E File Offset: 0x00006E1E
		public static EntityType Civilian { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00008C26 File Offset: 0x00006E26
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00008C2F File Offset: 0x00006E2F
		public static EntityType Guard { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00008C37 File Offset: 0x00006E37
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00008C40 File Offset: 0x00006E40
		public static EntityType Murderer { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00008C48 File Offset: 0x00006E48
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00008C51 File Offset: 0x00006E51
		public static EntityType Player1 { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00008C59 File Offset: 0x00006E59
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00008C62 File Offset: 0x00006E62
		public static EntityType Arrow1 { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00008C6A File Offset: 0x00006E6A
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00008C73 File Offset: 0x00006E73
		public static EntityType WayPoint { get; set; }

		// Token: 0x040000E6 RID: 230
		public static List<EntityType> Lst = new List<EntityType>();
	}
}
