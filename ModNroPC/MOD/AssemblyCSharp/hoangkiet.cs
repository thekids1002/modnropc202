using AssemblyCSharp.Mod.Xmap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AssemblyCSharp
{
    class hoangkiet
    {
        private const int ID_ITEM_CAPSULE_VIP = 194;
        private const int ID_ITEM_CAPSULE = 193;
        private const int ID_ITEM_BONGTAI = 454;
        public static MyVector myChatVip = new MyVector();
        public static bool autodanh;
        public static bool paint;
        public static bool paintfriend;
        public static bool loadedskill = true;
        public static bool rkhu = true;
        public static int ID_MAP_GOBACK;
        public static int ID_ZONE_GOBACK;
        public static bool goBack = false;
        public static int npcid;
        public static int menuid;
        public static int optionid;
        public static void tudanhbanthan()
        {
            try
            {
                if (GameScr.hsme)
                {
                    sbyte idSkill = Char.myCharz().myskill.template.id;
                    SkillTemplate skillTemplate = new SkillTemplate();
                    skillTemplate.id = 7;
                    MyVector vMe = new MyVector();
                    vMe.addElement(Char.myCharz());
                    Skill skill = global::Char.myCharz().getSkill(skillTemplate);
                    long num = mSystem.currentTimeMillis();
                    long num2 = num - skill.lastTimeUseThisSkill;
                    if (num2 > skill.coolDown)
                    {
                        Service.gI().selectSkill((int)skillTemplate.id);
                        Service.gI().sendPlayerAttack(new MyVector(), vMe, -1);
                        skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
                        Service.gI().selectSkill((int)idSkill);
                    }
                }
            }

            catch (Exception)
            {

            }
        }

        public static int x_goback;
        public static int y_goback;
        public static void myChat(string s)
        {
            if (s.Equals("hsme"))
            {
                GameScr.hsme ^= true;
                GameScr.info1.addInfo("Auto hồi sinh bản thân" + (GameScr.hsme ? " Bật" : "Tắt"), 0);

            }
            if (s.Equals("alogin"))
            {
                GameScr.alogin ^= true;
                GameScr.info1.addInfo("Autologin " + (GameScr.alogin ? " Bật" : "Tắt"), 0);
            }
            if (s.Equals("akhu"))
            {
                GameScr.akhu = TileMap.zoneID;
                GameScr.info1.addInfo("Auto vào khu " + GameScr.akhu, 0);
            }
            if (s.Equals("xindau"))
            {
                isxindau = !isxindau;
                GameScr.info1.addInfo("Auto xin đậu : " + trangthai(isxindau), 0);
            }
            if (s.Equals("chodau"))
            {
                ischodau = !ischodau;
                GameScr.info1.addInfo("Auto cho đậu : " + trangthai(ischodau), 0);
            }
            if (s.Equals("thudau"))
            {
                isthudau = !isthudau;
                GameScr.info1.addInfo("Auto cho đậu : " + trangthai(isthudau), 0);
            }
            if (s.Equals("ak"))
            {
                autodanh = !autodanh;
                GameScr.info1.addInfo("Tự đánh : " + (autodanh ? " Bật" : "Tắt"), 0);
            }
            if (s.Equals("goback"))
            {
                ID_MAP_GOBACK = TileMap.mapID;
                ID_ZONE_GOBACK = TileMap.zoneID;
                x_goback = Char.myCharz().cx;
                y_goback = Char.myCharz().cy;
                goBack = !goBack;
                GameScr.info1.addInfo("GoBack Map : " + TileMap.mapName + " Khu :" + ID_ZONE_GOBACK + trangthai(goBack), 0);
            }
            if (s.StartsWith("npc")){
                    int num = int.Parse(s.Substring(3));
                      Service.gI().openMenu(num);
            }
            if (s.StartsWith("autonc"))
            {
                autonc = !autonc;
                GameScr.info1.addInfo("Tự nâng cấp : " + (autonc ? " Bật" : "Tắt"), 0);
            }
            return;
        }

        public static void update()
        {

            if (GameCanvas.gameTick % 100 == 0)
            {
                tudanhbanthan();
                xindau();
                thudau();
                chodau();
                goback();
                if (!(TileMap.mapID == 22 || TileMap.mapID == 21 || TileMap.mapID == 23))
                {
                    Service.gI().openUIZone();
                }
            }


            if (GameCanvas.gameTick % 5 == 0)
            {
                tudanh();

            }
            if (Char.myCharz().havePet && mSystem.currentTimeMillis() - Char.myCharz().timeupdate > 2000)
            {
                Service.gI().petInfo();
                Char.myCharz().timeupdate = mSystem.currentTimeMillis();
            }
            if (loadedskill)
            {
                loadSkill();
                loadedskill = false;
            }
        }
        public static long timenc = 0;
        public static bool autonc;
        public static Item item;
        public static Item[] items;
        public static bool isGold;
        public static MyVector myVector = new MyVector();
        public static sbyte action;
        public static int index1;
        public static int index2;
        public static int index3;
        public static void autonangcap()
        {
            try
            {
                if(mSystem.currentTimeMillis() - timenc > 3000 && autonc)
                {
                    Service.gI().crystalCollectLock(items);
                    timenc = mSystem.currentTimeMillis();
                }
            }
            catch (Exception e)
            {

            }
        }
        public static void paintf(mGraphics g)
        {
            try
            {
                if (paint)
                {
                    int paintX = GameCanvas.w - 150;
                    int paintY = 120;
                    mFont.tahoma_7b_green.drawString(g, "" + TileMap.mapName + " - " + TileMap.zoneID + "- MapID :" + TileMap.mapID, 10, 80, 0);
                    if (Char.myCharz().havePet)
                    {

                        mFont.tahoma_7b_white.drawString(g, "Đệ tử", 10, 180, 0);
                        mFont.tahoma_7b_white.drawString(g, "HP :" + NinjaUtil.numberTostring(Char.myPetz().cHP + ""), 10, 190, 0);
                        mFont.tahoma_7b_white.drawString(g, "MP :" + NinjaUtil.numberTostring(Char.myPetz().cMP + ""), 10, 200, 0);
                        mFont.tahoma_7b_white.drawString(g, "SM :" + NinjaUtil.numberTostring(Char.myPetz().cPower + ""), 10, 210, 0);
                        mFont.tahoma_7b_white.drawString(g, "TN :" + NinjaUtil.numberTostring(Char.myPetz().cTiemNang + ""), 10, 220, 0);
                        mFont.tahoma_7b_white.drawString(g, "TN :" + (Char.myPetz().cStamina / Char.myPetz().cMaxStamina * 100) + "%", 10, 230, 0);
                    }
                    if (GameScr.vCharInMap.size() > 0 && GameScr.vCharInMap != null)
                    {
                        mFont.tahoma_7b_white.drawString(g, "Nhân vật trong khu ", paintX, paintY - 10, 0);

                        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
                        {
                            Char @char = (Char)GameScr.vCharInMap.elementAt(i);
                            if (@char != null)
                            {
                                if (Char.myCharz().charFocus != null && Char.myCharz().charFocus.charID == @char.charID)
                                {
                                    mFont.tahoma_7b_white.drawString(g, @char.cName + " - " + NinjaUtil.getMoneys(@char.cHP), paintX, paintY, 0);
                                    g.setColor(UnityEngine.Color.yellow);
                                    if (Math.abs(Char.myCharz().cx - @char.cx) > 20)
                                    {
                                        g.drawLine(Char.myCharz().cx - GameScr.cmx, Char.myCharz().cy - GameScr.cmy, @char.cx - GameScr.cmx, @char.cy - GameScr.cmy);
                                        g.drawLine(Char.myCharz().cx + 1 - GameScr.cmx, Char.myCharz().cy + 1 - GameScr.cmy, @char.cx + 1 - GameScr.cmx, @char.cy + 1 - GameScr.cmy);
                                    }
                                }
                                else if (System.Char.IsUpper(@char.cName[0]) && !@char.cName.ToLower().Trim().Contains("đệ") && @char.cTypePk == 5)
                                {
                                    mFont.tahoma_7b_red.drawString(g, @char.cName + " - " + NinjaUtil.getMoneys(@char.cHP), paintX, paintY, 0);
                                    g.setColor(UnityEngine.Color.red);
                                    if (Math.abs(Char.myCharz().cx - @char.cx) > 20)
                                    {
                                        g.drawLine(Char.myCharz().cx - GameScr.cmx, Char.myCharz().cy - GameScr.cmy, @char.cx - GameScr.cmx, @char.cy - GameScr.cmy);
                                        g.drawLine(Char.myCharz().cx + 1 - GameScr.cmx, Char.myCharz().cy + 1 - GameScr.cmy, @char.cx + 1 - GameScr.cmx, @char.cy + 1 - GameScr.cmy);
                                    }
                                }
                                else
                                {
                                    mFont.tahoma_7b_yellow.drawString(g, @char.cName + " - " + NinjaUtil.getMoneys(@char.cHP), paintX, paintY, 0);
                                }
                            }
                            paintY += 10;
                        }
                    }
                    paintChatVip(g);
                }
            }
            catch (Exception e)
            {
              
            }
        }

        #region

        public static void paintChatVip(mGraphics g)
        {
            try
            {
                int x = GameCanvas.w - 150;
                int y = 30;
                if (myChatVip.size() > 0)
                {
                    for (int i = myChatVip.size(); i > myChatVip.size() - 6; i--)
                    {
                        string chatvip = (string)myChatVip.elementAt(i);

                        if (chatvip != null)
                        {
                            int index = chatvip.IndexOf('|');
                            string chatvip2 = chatvip.Substring(0, index);
                            string[] arr = chatvip.Split('|');
                            long time = long.Parse(arr[1]);
                            int time1 = ((int)mSystem.currentTimeMillis() - (int)time);
                            int time_ago = (int)((time1 / 1000));

                            if (chatvip2.ToLower().Contains(TileMap.mapName.ToLower()))
                            {
                                mFont.tahoma_7b_red.drawString(g, chatvip2 + " " + NinjaUtil.getTimeAgo(time_ago), x, y + 10, 0);
                            }
                            else
                            {
                                mFont.tahoma_7_yellow.drawString(g, chatvip2 + " " + NinjaUtil.getTimeAgo(time_ago), x, y + 10, 0);
                            }
                            y += 10;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                GameCanvas.startOKDlg(e.ToString());
            }
        }

        #endregion

        public static void chim(string s)
        {
            GameScr.info1.addInfo(s, 0);
        }

        public static void tudanh()
        {
            /*sbyte idSkill = Char.myCharz().myskill.template.id;
            SkillTemplate skillTemplate = new SkillTemplate();
            skillTemplate.id = idSkill;*/

            if (autodanh)
            {
                Skill skill = global::Char.myCharz().myskill;
                long num2 = mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill;
                if (Char.myCharz().mobFocus != null && num2 > skill.coolDown + 100
                    && !Char.myCharz().mobFocus.isMobMe
                    && Math.abs(Char.myCharz().mobFocus.x - Char.myCharz().cx) < 150
                    && GameScr.gI().isMeCanAttackMob(Char.myCharz().mobFocus))
                {
                    MyVector vec = new MyVector();
                    vec.addElement(Char.myCharz().mobFocus);
                    Service.gI().sendPlayerAttack(vec, new MyVector(), 1);
                    skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
                }
                if (Char.myCharz().charFocus != null && num2 > skill.coolDown + 50
                    && Char.myCharz().isMeCanAttackOtherPlayer(Char.myCharz().charFocus))
                {
                    MyVector vec = new MyVector();
                    vec.addElement(Char.myCharz().charFocus);
                    Service.gI().sendPlayerAttack(new MyVector(), vec, 2);
                    skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
                }
            }

        }

        public static bool HotKeys()
        {
            switch (GameCanvas.keyAsciiPress)
            {
                case 'g':
                    Char @char = Char.myCharz().charFocus;

                    Service.gI().giaodich(0, @char.charID, -1, -1);
                    break;
                case 'a':
                    myChat("ak");
                    break;
                case 'z':
                    paint = !paint;
                    chim("Hiện thông tin :" + trangthai(paint));
                    break;
                case 'c':
                    if (ExistItem(ID_ITEM_CAPSULE_VIP))
                    {
                        doUseItem(ID_ITEM_CAPSULE_VIP);

                    }
                    else if (ExistItem(ID_ITEM_CAPSULE))
                    {
                        doUseItem(ID_ITEM_CAPSULE);
                    }
                    else
                    {
                        chim("Không có capsule");
                    }
                    break;
                /*case 'a':
                    Chat("add");
                    break;
                case 'b':
                    Chat("abf");
                    break;*/
                case 'm':
                    Service.gI().openUIZone();
                    GameCanvas.panel.setTypeZone();
                    GameCanvas.panel.show();
                    break;
                case 'b':
                    Service.gI().friend(0, -1);
                    break;
                case 's':
                    focusBoss();
                    break;
                case 'j':
                    LoadMap(0);
                    break;
                case 'k':
                    LoadMap(1);
                    break;
                case 'l':
                    LoadMap(2);
                    break;
                case 'f':
                    if (ExistItem(ID_ITEM_BONGTAI))
                    {
                        doUseItem(ID_ITEM_BONGTAI);
                        if (!Char.myCharz().isNhapThe)
                        {
                            Service.gI().petStatus(3); // ĐỆ VỀ NHÀ
                        }

                    }
                    else
                    {
                        chim("Không có bông tai!!!");
                    }
                    break;
                case 'v':
                    if (Char.myCharz().mobFocus == null && Char.myCharz().charFocus == null && Char.myCharz().itemFocus == null)
                    {
                        chim("Không có mục tiêu");
                        return true;
                    }
                    else
                    {
                        if (Char.myCharz().mobFocus != null)
                        {
                            XmapController.MoveMyChar(Char.myCharz().mobFocus.x, Char.myCharz().mobFocus.y);
                        }
                        else if (Char.myCharz().charFocus != null)
                        {
                            XmapController.MoveMyChar(Char.myCharz().charFocus.cx, Char.myCharz().charFocus.cy);
                        }
                        else if (Char.myCharz().itemFocus != null)
                        {
                            XmapController.MoveMyChar(Char.myCharz().itemFocus.x, Char.myCharz().itemFocus.y);
                        }

                    }
                    break;
                default:
                    return false;
            }
            return true;
        }

        public static bool ExistItem(short iditem)
        {
            if (Char.myCharz().arrItemBag == null)
            {
                return false;
            }
            for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
            {
                if (Char.myCharz().arrItemBag[i] != null && Char.myCharz().arrItemBag[i].template.id == iditem)
                {
                    //  Service.gI().useItem(0, 1, -1, Char.myCharz().arrItemBag[i].template.id);
                    return true;
                }
            }
            return false;
        }
        public static void doUseItem(short iditem)
        {
            Service.gI().useItem(0, 1, -1, iditem);
        }
        public static string trangthai(bool x)
        {
            return x ? "Bật" : "Tắt";
        }
        public static void loadSkill()
        {
            GameScr.gI().loadDefaultonScreenSkill();
            GameScr.gI().loadDefaultKeySkill();

        }
        public static long timexindau = 0;
        public static bool isxindau = false;
        public static bool ischodau = false;
        public static bool isthudau = false;
        public static void xindau()
        {
            if (mSystem.currentTimeMillis() - timexindau > 300000L && isxindau)
            {
                Service.gI().clanMessage(1, "", -1);
                timexindau = mSystem.currentTimeMillis();
            }
        }

        public static void chodau()
        {
            for (int i = 0; i < ClanMessage.vMessage.size() && ischodau; i++)
            {
                ClanMessage clan = (ClanMessage)ClanMessage.vMessage.elementAt(i);
                if (clan.maxCap != 0 && clan.playerName != Char.myCharz().cName && clan.recieve != clan.maxCap)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Service.gI().clanDonate(clan.id);
                        Thread.Sleep(100);
                    }
                }
            }
        }
        public static long timethudau = 0;
        public static void thudau()
        {
            if ((TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23) && mSystem.currentTimeMillis() - timethudau > 180000 && isthudau)
            {
                Service.gI().openMenu(4);
                Service.gI().menu(4, 0, 0);
            }
        }
        public static long time_chuyen_khu;
        public static void goback()
        {

            if (goBack)
            {
               
                if (XmapData.IsMyCharDie())
                {
                    Service.gI().returnTownFromDead();
                }
                else if((TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23) )
                {

                    Service.gI().openMenu(4);
                    Service.gI().menu(4, 0, 0);
                    for(int i = 0; i < GameScr.vItemMap.size(); i++)
                    {
                        ItemMap item = (ItemMap)GameScr.vItemMap.elementAt(i);
                        XmapController.MoveMyChar(item.x, item.y);
                        Char.myCharz().itemFocus = item;
                        Service.gI().pickItem(item.itemMapID);
                        GameCanvas.gI().keyPressedz(-5);
                      
                    }
                    XmapController.StartRunToMapId(ID_MAP_GOBACK);
                }
                else if(TileMap.mapID != ID_MAP_GOBACK )
                {
                    XmapController.StartRunToMapId(ID_MAP_GOBACK);
                }
                else if(TileMap.mapID == ID_MAP_GOBACK && TileMap.zoneID != ID_ZONE_GOBACK)
                {
                   if(mSystem.currentTimeMillis() - time_chuyen_khu > 5000)
                    {
                        Service.gI().requestChangeZone(ID_ZONE_GOBACK, 0);
                        time_chuyen_khu = mSystem.currentTimeMillis();
                        
                    }
                    
                }
                XmapController.MoveMyChar(x_goback, y_goback);


            }


        }

        public static void focusBoss()
        {
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = (Char)GameScr.vCharInMap.elementAt(i);
                if (System.Char.IsUpper(@char.cName[0]) && !@char.cName.ToLower().Trim().Contains("đệ") && @char.cTypePk == 5)
                {
                    Char.myCharz().focusManualTo(@char);
                    return;
                }
            }
            chim("Không có boss");
        }

        public static string GetTextPopup(PopUp popUp)

        {

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < popUp.says.Length; i++)

            {

                stringBuilder.Append(popUp.says[i]);

                stringBuilder.Append(" ");

            }

            return stringBuilder.ToString().Trim();

        }

        public static void RequestChangeMap(Waypoint waypoint)

        {

            if (waypoint.isOffline)

            {

                Service.gI().getMapOffline();

                return;

            }

            Service.gI().requestChangeMap();

        }

        public static void MoveMyChar(int x, int y)

        {

            global::Char.myCharz().cx = x;

            global::Char.myCharz().cy = y;

            Service.gI().charMove();

            if (!ItemTime.isExistItem(4387))

            {

                global::Char.myCharz().cx = x;

                global::Char.myCharz().cy = y + 1;

                Service.gI().charMove();

                global::Char.myCharz().cx = x;

                global::Char.myCharz().cy = y;

                Service.gI().charMove();

            }

        }

        public static Waypoint FindWaypoint(int type)

        {

            if (TileMap.vGo.size() == 1)

            {

                return (Waypoint)TileMap.vGo.elementAt(0);

            }

            for (int i = 0; i < TileMap.vGo.size(); i++)

            {

                Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);

                if (type == 0)

                {

                    if ((TileMap.mapID == 70 && GetTextPopup(waypoint.popup) == "Vực cấm") || (TileMap.mapID == 73 && GetTextPopup(waypoint.popup) == "Vực chết") || (TileMap.mapID == 110 && GetTextPopup(waypoint.popup) == "Rừng tuyết"))

                    {

                        return waypoint;

                    }

                    if (waypoint.maxX < 60)

                    {

                        return waypoint;

                    }

                }

                if (type == 1)

                {

                    if (((TileMap.mapID == 106 || TileMap.mapID == 107) && GetTextPopup(waypoint.popup) == "Hang băng") || ((TileMap.mapID == 105 || TileMap.mapID == 108) && GetTextPopup(waypoint.popup) == "Rừng băng") || (TileMap.mapID == 109 && GetTextPopup(waypoint.popup) == "Cánh đồng tuyết"))

                    {

                        return waypoint;

                    }

                    if (TileMap.mapID == 27)

                    {

                        return null;

                    }

                    if ((int)waypoint.minX < TileMap.pxw - 60 && waypoint.maxX >= 60)

                    {

                        return waypoint;

                    }

                }

                if (type == 2)

                {

                    if (TileMap.mapID == 70 && GetTextPopup(waypoint.popup) == "Căn cứ Raspberry")

                    {

                        return waypoint;

                    }

                    if ((int)waypoint.minX > TileMap.pxw - 60)

                    {

                        return waypoint;

                    }

                }

            }

            return null;

        }

        public static void LoadMap(int type)

        {

            Waypoint waypoint = FindWaypoint(type);

            if (waypoint != null)

            {

                int maxY = (int)waypoint.maxY;

                MoveMyChar((waypoint.maxX < 60) ? 15 : (((int)waypoint.minX <= TileMap.pxw - 60) ? ((int)(waypoint.minX + 30)) : (TileMap.pxw - 15)), maxY);

                if (type == 1 || TileMap.vGo.size() == 1)

                {

                    RequestChangeMap(waypoint);

                }

            }

        }
    }
}
