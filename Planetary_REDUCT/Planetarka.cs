using System;

public class Planet
{   //-------------------------------РАССЧИТЫВАЕТСЯ МЕТОДАМИ---------------------------------------------------
    public int Za, Zb, Zg, Zf, N; // Z1 - число зубьев солнечного, Z2 - число зубьев сателлита
                                   //Z3 - число зубьев короны, N - число сателлитов, m - модуль ступени
                                   // U - передаточное отношение
    private double U, Aw1, Aw2, A; // U - передаточное отношение редуктора, 
                                               //M1 - модуль первой ступени,  M2 - модуль второй ступени 


    //-----------------------------------------ВВОДИТСЯ ИЗ ИНТЕРФЕЙСА-----------------------------------------
    public int ZaMin { get; set; }
    
    public int ZaMax { get; set; }
    public int ZgMin { get; set; }
    public int ZgMax { get; set; }
    public int ZfMin { get; set; }
    public int ZfMax { get; set; }
    public int NMin { get; set; }
    public int NMax { get; set; }
    public double M1 { get; set; }
    public double M2{ get; set; }
    public double du { get; set; }
    public double UT { get; set; }
    public double ag { get; set; }
    private double DU; //UT - требуемое передаточное отношение, du - требуемая погрешность, ag - требуемый габарит
    private int LTR1, LTR2; // Маркеры для ответвления в расчетах
 //-----------------------------------------------------------------------------------------------------------


//------------------------------------СЕРВИСНЫЕ ПЕРЕМЕННЫЕ (ИСПОЛЬЗУЮТСЯ ТОЛЬКО В РАСЧЕТЕ------------------------
    private int IP, KA, KG, KF, KB, NW, JPR, MPR; // JPR, MPR - индикаторы
    private double Da,Db, Dg, Df, DR1, DR2, DR, AW, Uvr, Y, YD=1, D1, D2, ALF, HAZ, CZ;

    private int KPZ; // Управляющий признак, вводится(определяется по картинке в интерфейсе

    //---------------------------------------------------------------------------------------------------------------

 //------------------------------------ВЫВОДЯТСЯ НА ЭКРАН ПОСЛЕ РАСЧЕТА-------------------------------------------------
 //Za - число зубьев солнца, Zb - число зубьев короны, Zg - число зубьев сателлита 1, Zf - число зубьев сателлита 2
 //N - число сателлитов, Da - Db - Dg - Df - диаметры делительных окружностей колес, А - габарит механизма
 //Y - суммарный коэффициент смещения, U - передаточное отношение, DU - погрешность реализации передаточного 
 //---------------------------------------------------------------------------------------------------------------------


//YD=1 - допущение (в большинстве случаев)
    private void SVZ1(double SM, int LH)
    {

 // Определение угла зацепления и осевого зазора

        ALF = Math.PI / 9;
        if (LH < 0) { HAZ = 1; } else HAZ = 1.1;
        if (SM - 0.5 <= 0) { CZ = 0.5; }
        else
        {
            if (SM - 1 <= 0) { CZ = 0.35; } else CZ = 0.25;
        }
    }

    // Условие сборки

    private void SV1(int K, int N) { if ((K % N) == 0) JPR = 0; else JPR = 1; }

    // Проверка отсутствия интерференции

    private void SV2(int K1, int K2)
    {
        for (int i = 2; i < K1; i++)
        {
            SV1(K1, i);
            if (JPR == 0)
            {
                SV1(K2, i);
                if (JPR == 0) { MPR = 0; return; } else { MPR = 1;return; }
            }
        }
        //MPR = 1;
    }


    private void SV6(double AW1, double AW2, double SM1)
    {

       // Вычисление суммарного смещения

        Y = (AW2 - AW1) / SM1;
        if (Y < 0) { IP = 4; return; } ;
        if (Y == 0) { IP = 2; return;};
        if (Y > 0)
        {
            if ((Y - YD) > 0) { IP = 4; return; }
            else
            {
                IP = 2; return;
            }
        }                        
    }



    //private void SV15(int KPZ, int K1, int K2, double SM)
    //{

    //    // Проверка возможности реализации механизма при заданных числах зубов, модулях
    //    // и расчет геометрических параметров передачи

    //    double Ulok, C;

    //    Ulok = K2 / K1;
    //    //--------------------------------------------------------------------------
    //    //----------Замена закомментированного ниже блока---------------------------
    //    //--------------------------------------------------------------------------
    //    if (KPZ == 1) { if (Ulok - 6> 0) { IP = 1; return; } };
    //    if (KPZ == 3)
    //    {
    //        C = ((K1 * K1) - 34) / (2 * K1 - 34);
    //        if ((K2 - C) < 0) { IP = 3; return; }
    //        else
    //        { if (Ulok - 8 > 0) { IP = 1; return; } };
    //        //goto 25;
    //    }//goto 3
    //    if ((KPZ == 2) || (KPZ == 4))
    //    {
    //       if (1 - Ulok > 0) { IP = 3; return; }
    //       if (Ulok - 6 > 0) { IP = 1; return; }
    //    }

    //    //----------------------------------------------------------------------------------------------------------
    //    //

    //    if (LTR1>0)
    //    {
    //        if (KPZ - 3 < 0)
    //        {
    //            SV1(K1, NW);//(K1,NW)
    //            if (JPR <= 0) { IP = 1; return; }
    //        }
    //        else
    //        {
    //            SV1(K2, NW);//(K2,NW)
    //            if (JPR <= 0) { IP = 3; return; }
    //        }


    //    }

    //    if (LTR2 > 0)
    //    {
    //        SV2(K1, K2);
    //        if (MPR <= 0) { IP = 3; return; }
    //        SVZ1(SM, 0);
    //        D1 = SM * (K1 + 2 * HAZ);
    //        if (KPZ - 3 < 0)
    //        {
    //            D2 = SM * (K2 + 2 * HAZ);
    //            AW = 0.5 * SM * (K1 + K2);
    //            DR = 2 * AW + D2;
    //            if (DR - ag > 0) { IP = 1; return; } else { IP = 2; return; }
    //        }
    //        else
    //        {
    //            D2 = SM * (K2 + 2 * (HAZ + CZ));
    //            AW = 0.5 * SM * (K2 - K1);
    //            DR = D2;
    //            if (DR - ag > 0) { IP = 1; return; } else { IP = 2; return; }
    //        }
    //    }
    //    else
    //    {
    //        SVZ1(SM, 0);
    //        D1 = SM * (K1 + 2 * HAZ);
    //        if (KPZ - 3 < 0)
    //        {
    //            D2 = SM * (K2 + 2 * HAZ);
    //            AW = 0.5 * SM * (K1 + K2);
    //            DR = 2 * AW + D2;
    //            if (DR - ag > 0) { IP = 1; return; } else { IP = 2; return; }
    //        }
    //        else
    //        {
    //            D2 = SM * (K2 + 2 * (HAZ + CZ));
    //            AW = 0.5 * SM * (K2 - K1);
    //            DR = D2;
    //            if (DR - ag > 0) { IP = 1; return; } else { IP = 2; return; }
    //        }
    //    }


    //}


    private void SV15(int KPZ, int K1, int K2, double SM) {

        double Ulok = K2 / K1;

        if (KPZ == 1) goto M4;
        if (KPZ == 2) goto M2;
        if (KPZ == 3) goto M3;

    M2: if (1 - Ulok <= 0) goto M4; else goto M40;
    M3: double C = (K1 * K1 - 34) / (2 * K1 - 34);
        if (K2 - C >= 0) goto M5; else goto M40;
    M4: if (Ulok - 6 <= 0) goto M6; else goto M25;
    M5: if (Ulok - 8 <= 0) goto M6; else goto M25;
    M6: if (LTR1 <= 0) goto M10; else goto M7;
    M7: if (KPZ - 3 >= 0) goto M9; else goto M8;
    M8: SV1(K1, NW);
        if(JPR <= 0) goto M25; else goto M10;
    M9: SV1(K2, NW);
        if (JPR <= 0) goto M40; else goto M10;
    M10: if (LTR2 <= 0) goto M17; else goto M15;
    M15: SV2(K1, K2);
        if (MPR <= 0) goto M40; else goto M17;
    M17: SVZ1(SM, 0);
        D1 = SM * (K1 + 2 * HAZ);
        if (KPZ - 3 >= 0) goto M20; else goto M18;
    M18: D2 = SM * (K2 + 2 * HAZ);
        AW = 0.5 * SM * (K1 + K2);
        DR = 2 * AW + D2;
        goto M21;
    M20: D2 = SM * (K2 + 2 * (HAZ + CZ));
        AW = 0.5 * SM * (K2 - K1);
        DR = D2;
    M21: if (DR - ag <= 0) goto M30; else goto M25;
        M25: IP = 1;
        return;
    M30: IP = 2;
        return;
    M40: IP = 3;
        return;

    }


    private void SV16(int NW)
    {
        // Определение числа зубьев второго сателлита и проверка
        //возможности реализации при такой конфигурации

        Zf = Za + Zb;
        SV1(Zf, NW);
    
    }

    private void SV17()
    {

        // Проверка по условию...

        int LC; int LB; int p;
        LC = Zg * Zb + Za * Zf;
        LB = Zg * NW;
        SV1(LC, LB);
    }

    private void SV18()
    {

        // Расчет числа зубов короны

        double ZB; int ZBI;
        ZB = (int)((M1 * (Za + Zg) + M2 * Zf) / M2);
        //ZBI = (int)Math.Round(ZB);
        ZBI = (int)ZB;
        if ((ZB - ZBI) == 0) Zb = ZBI; else Zb = (int)(ZB + 1);
    }



    public void ZTMM46()
    {
        // Заполнение всех полей
        LTR1 = 1;
        LTR2 = 1;
        YD = 0.5;

        //ZaMin = 20;ZaMax = 30; ZgMin = 25;ZgMax =50; ZfMin = 22; ZfMax = 40; M1 = 0.5;M2 = 0.5; NMin = 2; NMax = 3; UT = 9; du = 10; ag = 60;
        ZaMin = 18; ZaMax = 30; ZgMin = 25; ZgMax = 65; ZfMin = 22; ZfMax = 40; M1 = 0.4; M2 = 0.5; NMin = 2; NMax = 4; UT = 15; du = 3; ag = 60;
        for (NW = NMin; NW < NMax; NW++)//200
        {
            double X = Math.Sin(Math.PI / NW);
            //double a = (Za + Zg) * X;
            // double b = Za * X;
            for (Za = ZaMin; Za < ZaMax; Za++)//100
            {   //if (IP == 2||IP==4||IP==3) break;
                for (Zg = ZgMin; Zg < ZgMax; Zg++)//80
                {
                    if ((Za + Zg) * X - Zg - 7 < 0) break; // then goto 80
                    SV15(2, Za, Zg, M1);
                    //if ((IP == 1)||(IP==3))break;// then goto 100;
                    if (IP == 1&&NW!=NMin) goto M100;// goto 80 раскомментировано   для LTR2=0 25.02.20
                    if (IP == 3) { break; } //Временно коммент 23.02.20
                    Da = D1;
                    Dg = D2;
                    Aw1 = AW;
                    DR1 = DR;

                    for (Zf = ZfMin; Zf < ZfMax; Zf++)
                    {
                        if (((Za + Zg) * X - (M2 / M1) * (Zf + 2) - 5) < 0) goto M80; //goto 80
                                                                                       //Zb = (int)Math.Round((Zf + (M1 / M2) * (Za + Zg)));
                        SV18();
                        SV15(3, Zf, Zb, M2);
                        Df = D1;
                        Db = D2;
                        Aw2 = AW;
                        DR2 = DR;

                        if (IP == 1) break;
                        if (IP == 3) goto M80;// then goto 50;
                                              //if (IP == 1) { break; }    // then goto 80; временно коммент 23.02.20
                                              //if (IP == 3) { Zg = ZgMax-1; break; }//добавлено 23.02.20
                        Uvr = 1 + Zg * Zb / (Za * Zf);
                        DU = (UT - Uvr) * 100 / UT;
                        if ((du - Math.Abs(DU)) < 0) break;
                        SV6(Aw1, Aw2, M1);             //    SV6 (YD, AW1, AW2, 1, Y, IP);
                        if (IP != 2) break;// then goto 50;

         
                        if (Y < 0 || Y > 0)
                        {

                            DR1 = 2 * Aw2 + Zg;
                            if (DR1 - ag > 0) goto M100;

                        }

                        if (DR1 - DR2 <= 0) A = DR2; else A = DR1;
                   

                        SV17();
                        if (IP == 2) goto M200;
                        // if (IP==3) break;// раскомментить при необх закоменчено 23.02.20
                        //if (JPR == 0) break;



                    }//50   
                     //if (IP == 3) break;
                     ////if ((DR1 - ag >0)||IP==1||IP==3) break;//bilo >=
                     //if ( DR2 - ag < 0) break;
                
                }//80
            M80:;
                if (IP == 2) break;
                //if (IP == 3) break;

            // if (IP == 3 || IP == 2) break;//временно закомментированно 23.02.20
            //if((IP == 3)||(IP==1)) break;
            /////if ((IP == 3)|| (DR1 - ag > 0)||(IP==1)) break;
            //if (DR1 - ag < 0 || DR2 - ag < 0) break;

            
            }//100
        M100:;
            //if (IP == 2) break;
            //if (IP == 3) break;
            //if (IP == 3||IP==1) break;
            N = NW;
            //if (IP == 2||IP==0) break;
            // if ((IP == 0) || (IP == 2) || (IP == 4)) break;//добавлено 23.02.20

        }//200
    M200:;
         // N = NW; временно закомменчено
         //// Zg = Zg - 1;
         // Zb = Zb + 1;

    }


    //public void ZTMM46()
    //{
    //    // Заполнение всех полей
    //    LTR1 = 1;
    //    LTR2 = 1;
    //    YD = 0.5;

    //    //ZaMin = 20;ZaMax = 30; ZgMin = 25;ZgMax =50; ZfMin = 22; ZfMax = 40; M1 = 0.5;M2 = 0.5; NMin = 2; NMax = 3; UT = 9; du = 10; ag = 60;
    //    ZaMin = 18; ZaMax = 30; ZgMin = 25; ZgMax = 65; ZfMin = 22; ZfMax = 40; M1 = 0.4; M2 = 0.5; NMin = 2; NMax = 3; UT = 15; du = 3; ag = 60;
    //    for (NW = NMin; NW < NMax; NW++)//200
    //    {
    //        double X = Math.Sin(Math.PI / NW);
    //        double a = (Za + Zg) * X;
    //        double b = Za * X;
    //        Za = ZaMin;
    //        while (Za != ZaMax)//100
    //        {
    //            Za++;
    //            Zg = ZgMin;
    //            while (Zg!=ZgMax)//80
    //            {
    //                Zg++;
    //                if ((((Za + Zg) * X) - Zg - 7) < 0) {  break; } // then goto 80
    //                else
    //                {
    //                    SV15(2, Za, Zg, M1);
    //                    switch (IP)
    //                    {
    //                        case 1: goto M100; // goto 100;
    //                        case 3: goto M80;
    //                    }


    //                    Da = D1;
    //                    Dg = D2;
    //                    Aw1 = AW;
    //                    DR1 = DR;

    //                    for (Zf = ZfMin; Zf < ZfMax; Zf++)
    //                    {
    //                        if (((Za + Zg) * X - (M2 / M1) * (Zf + 2) - 5) < 0) goto M80;//goto 80
    //                        //Zb = (int)Math.Round((Zf + (M1 / M2) * (Za + Zg)));
    //                        SV18();
    //                        SV15(3, Zf, Zb, M2);
    //                        Df = D1;
    //                        Db = D2;
    //                        Aw2 = AW;
    //                        DR2 = DR;

    //                        switch (IP)
    //                        {
    //                            case 1: goto M50;
    //                            case 3: goto M80;
    //                        }
    //                        Uvr = 1 + Zg * Zb / (Za * Zf);
    //                        DU = (UT - Uvr) * 100 / UT;
    //                        if ((du - Math.Abs(DU)) < 0) goto M50;
    //                        SV6(Aw1, Aw2, M1);             //    SV6 (YD, AW1, AW2, 1, Y, IP);
    //                        if ((IP - 2) > 0) goto M50;// then goto 50;


    //                        if (!(Y == 0))
    //                        {
    //                            DR1 = 2 * Aw2 + Dg;
    //                            if ((DR1 - ag) > 0) goto M100; //раскомментить при необходимости
    //                                                           //if ((DR1 - ag) > 0) { Zg = ZgMax-1;Za = ZaMax-1;break; }//добавлено 23.02.20
    //                            if ((DR1 - DR2) > 0) { A = DR1; } else { A = DR2; }
    //                        }
    //                        else
    //                        {
    //                            if ((DR1 - DR2) > 0) { A = DR1; } else { A = DR2; }
    //                            //break;
    //                        }
    //                        SV17();
    //                    M50:;



    //                    }//50   

    //                }//end else
    //            M80: Zg++;
    //            }//80
    //            if (IP == 2) break;
    //        M100: Za++;
    //        }//100

    //        //N = NW;
    //        //if ((IP == 2) || (IP == 0)||(IP==4)) break;

    //        if (IP == 2) break;


    //    }//200


    //}
    //РАБОЧАЯ КОПИЯ ИСПРАВНОГО ВАРИАНТА

    //public void ZTMM46()
    //{
    //    // Заполнение всех полей
    //    LTR1 = 1;
    //    LTR2 = 1;
    //    YD = 0.5;

    //    //ZaMin = 20;ZaMax = 30; ZgMin = 25;ZgMax =50; ZfMin = 22; ZfMax = 40; M1 = 0.5;M2 = 0.5; NMin = 2; NMax = 3; UT = 9; du = 10; ag = 60;
    //    ZaMin = 18; ZaMax = 30; ZgMin = 25; ZgMax = 65; ZfMin = 22; ZfMax = 40; M1 = 0.4; M2 = 0.5; NMin = 2; NMax = 3; UT = 15; du = 3; ag = 60;
    //    for (NW = NMin; NW < NMax; NW++)//200
    //    {
    //        double X = Math.Sin(Math.PI / NW);
    //        //double a = (Za + Zg) * X;
    //        // double b = Za * X;
    //        for (Za = ZaMin; Za < ZaMax; Za++)//100
    //        {   //if (IP == 2||IP==4||IP==3) break;
    //            for (Zg = ZgMin; Zg < ZgMax; Zg++)//80
    //            {
    //                if ((((Za + Zg) * X) - Zg - 7) < 0) break; // then goto 80
    //                else
    //                {
    //                    SV15(2, Za, Zg, M1);
    //                    //if ((IP == 1)||(IP==3))break;// then goto 100;
    //                    //if (IP == 3) { break; } Временно коммент 23.02.20
    //                    if (IP == 1) break;// goto 80 раскомментировано   для LTR2=0 25.02.20
    //                    Da = D1;
    //                    Dg = D2;
    //                    Aw1 = AW;
    //                    DR1 = DR;

    //                    for (Zf = ZfMin; Zf < ZfMax; Zf++)
    //                    {
    //                        if (((Za + Zg) * X - (M2 / M1) * (Zf + 2) - 5) < 0) break;//goto 80
    //                        //Zb = (int)Math.Round((Zf + (M1 / M2) * (Za + Zg)));
    //                        SV18();
    //                        SV15(3, Zf, Zb, M2);
    //                        Df = D1;
    //                        Db = D2;
    //                        Aw2 = AW;
    //                        DR2 = DR;

    //                        if ((IP == 1) || (IP == 3)) break;    // then goto 50;
    //                        //if (IP == 1) { break; }    // then goto 80; временно коммент 23.02.20
    //                        //if (IP == 3) { Zg = ZgMax-1; break; }//добавлено 23.02.20
    //                        Uvr = 1 + Zg * Zb / (Za * Zf);
    //                        DU = (UT - Uvr) * 100 / UT;
    //                        if ((du - Math.Abs(DU)) < 0) break;
    //                        SV6(Aw1, Aw2, M1);             //    SV6 (YD, AW1, AW2, 1, Y, IP);
    //                        if ((IP - 2) > 0) break;// then goto 50;

    //                        /*if (Y == 0)//(Y==0 было
    //                        {
    //                            if ((DR1 - DR2) > 0) { A = DR1; }
    //                            else
    //                            {
    //                                A = DR2;
    //                                SV17();
    //                            }
    //                        };
    //                        DR1 = 2 * Aw2 + Dg;
    //                        if ((DR1 - ag) > 0) break;
    //                        if ((DR1 - DR2) > 0) A = DR1; else A = DR2;
    //                        SV17();*/
    //                        if (!(Y == 0))
    //                        {
    //                            DR1 = 2 * Aw2 + Dg;
    //                            if ((DR1 - ag) > 0) break; //раскомментить при необходимости
    //                                                       //if ((DR1 - ag) > 0) { Zg = ZgMax-1;Za = ZaMax-1;break; }//добавлено 23.02.20
    //                            if ((DR1 - DR2) > 0) A = DR1; else A = DR2;
    //                        }
    //                        else
    //                        {
    //                            if ((DR1 - DR2) > 0) A = DR1; else A = DR2;
    //                            //break;
    //                        }
    //                        SV17();
    //                        if (!(IP == 3)) break;// раскомментить при необх закоменчено 23.02.20



    //                    }//50   
    //                }//end else

    //                //if (IP == 3) break;
    //                ////if ((DR1 - ag >0)||IP==1||IP==3) break;//bilo >=
    //                //if ( DR2 - ag < 0) break;
    //            }//80
    //            if (IP == 3 || IP == 2) break;//временно закомментированно 23.02.20
    //            //if((IP == 3)||(IP==1)) break;
    //            /////if ((IP == 3)|| (DR1 - ag > 0)||(IP==1)) break;
    //            //if (DR1 - ag < 0 || DR2 - ag < 0) break;
    //        }//100
    //        //if (IP == 3) break;
    //        //if (IP == 3||IP==1) break;
    //        N = NW;
    //        if ((IP == 0) || (IP == 2) || (IP == 4)) break;//добавлено 23.02.20

    //    }//200
    //     // N = NW; временно закомменчено
    //    Zg = Zg - 1;
    //    Zb = Zb + 1;

    //}

    public Planet()
	{

	}
}
