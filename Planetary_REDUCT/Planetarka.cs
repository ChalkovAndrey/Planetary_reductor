using System;
using System.Collections.Generic;
public class Planet
{   //-------------------------------РАССЧИТЫВАЕТСЯ МЕТОДАМИ---------------------------------------------------
    public int Za, Zb, Zg, Zf, N; // Z1 - число зубьев солнечного, Z2 - число зубьев сателлита
                                   //Z3 - число зубьев короны, N - число сателлитов, m - модуль ступени
                                   // U - передаточное отношение
    private double U, Aw1, Aw2, A; // U - передаточное отношение редуктора, 
                                               //M1 - модуль первой ступени,  M2 - модуль второй ступени 


    //-----------------------------------------ВВОДИТСЯ ИЗ ИНТЕРФЕЙСА-----------------------------------------
    public int ZaMin { get; set; }
  public static  List<string> Result = new List<string>(13);
    
    public int ZaMax { get; set; }
    public int ZgMin { get; set; }
    public int ZgMax { get; set; }
    public int ZfMin { get; set; }
    public int ZfMax { get; set; }
    public int NMin { get; set; }
    public int NMax { get; set; }

    public double M1 { get; set; }
    public double M2 { get; set; }
    //=======
    //public float M1 { get; set; }
    //public float M2 { get; set; }
    ////>>>>>>> raschet
    public double du { get; set; }
    public double UT { get; set; }
    public double ag { get; set; }
    private double DU; //UT - требуемое передаточное отношение, du - требуемая погрешность, ag - требуемый габарит
    private int LTR1, LTR2; // Маркеры для ответвления в расчетах

    private bool LTR;

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

  private void SetResult()
    {
        Result.Add( Za.ToString());
        Result.Add(Zb.ToString());
        Result.Add(Zg.ToString());
        Result.Add(Zf.ToString());
        Result.Add(N.ToString());
        Result.Add(string.Format("{0:0.000}", Da));
        Result.Add(string.Format("{0:0.000}", Db));
        Result.Add(string.Format("{0:0.000}", Dg));
        Result.Add(string.Format("{0:0.000}", Df));
        Result.Add(string.Format("{0:0.000}", A));
        Result.Add(string.Format("{0:0.000}", Y));
        Result.Add(string.Format("{0:0.000}",U));
        Result.Add(string.Format("{0:0.000}", DU));
    }





    //------------------------------------------------------------АРГУМЕНТЫ ОПИСАННЫХ НИЖЕ ФУНКЦИЙ--------------------------------------------------
    //Zsol - число зубьев солнечного колеса, Zs1 - число зубьев сателлита, сцепленного с солнцем, Zs2 - число зубьев сателлита, сцепленного с короной, Zk - число зубьев короны
    //Nw - число сателлитов, m1,m2 - модули первой и второй ступеней соответственно, haz - коэффициент ножки зуба.
    //----------------------------------------------------------------------------------------------------------------------------------------------

    private bool CheckLTR(int K, int P)//проверка наличия общих множителей у целых чисел при требовании износостойкости. Общие есть - 1. нет - 0.
    {
        if (LTR)
        {
            if (K % P == 0 || P % K == 0) return true;
            else return false;
        }
        else return false;
    }

    private bool Sosedstvo(int Zsol, int Zs1, int Zs2, int Zk, double m1, double m2, int Nw)//проверка соседства. Соседство невозможно - 1, возможно - 0.
    {
        if ((Zsol + Zs1) * Math.Sin(3.14 / Nw) - (Zs2 + 2) * m2 / m1 < 5 && Nw >= 3) return true;
        else return false;
    }

    private bool Sborka(int Zsol, int Zs1, int Zs2, int Zk, int Nw)//проверка условия сборки. собрать нельзя - 1, можно - 0.
    {
        if ((Zk * Zs1 + Zsol * Zs2) / (Zs1 * Nw) - Math.Round((double)(Zk * Zs1 + Zsol * Zs2) / (Zs1 * Nw), 0) != 0 && Nw >= 2) return true;
        else return false;
    }

    private bool Interfer(int Zs2, int Zk, double haz)//проверка возможности наличия интерференции. Интерференция есть - 1, нет -0.
    {
        if (((Zs2 * Zs2 - 34) / (2 * Zs2 - 34) > Zk && Zs2 <= 27 && haz == 1) || (Zk - Zs2 < 7 && Zs2 > 27)) return true;
        else return false;
        
    }

    private void GeomParamSolnSAT()//вычисление геометрических параметров ступени солнце-сателлит1
    {
        Da = Math.Round(M1 * Za,2);
        Dg = Math.Round(M1 * Zg,2);
        Aw1 = Math.Round((Da + Dg) / 2,2);
        DR1 = Math.Round(Aw1 + Dg,2);
    }

    private void GeomParamSolnKOR()//вычисление геометрических параметров ступени сателлит2-корона
    {
        Df = Math.Round(M2 * Zf,2);
        Db = Math.Round(M2 * Zb,2);
        Aw2 = Math.Round((Db - Df) / 2,2);
        DR2 = Math.Round(M2 * (Zb + 2 * (HAZ + CZ)),2);
    }

    public void ZTMM46()
    {
        LTR1 = 1;
        LTR2 = 1;
       // YD = 0;

        //ZaMin = 20;ZaMax = 30; ZgMin = 25;ZgMax =50; ZfMin = 22; ZfMax = 40; M1 = 0.5;M2 = 0.5; NMin = 2; NMax = 3; UT = 9; du = 10; ag = 60;
        //ZaMin = 18; ZaMax = 30; ZgMin = 25; ZgMax = 65; ZfMin = 22; ZfMax = 40; M1 = 0.4; M2 = 0.5; NMin = 2; NMax = 4; UT = 15; du = 7; ag = 60;
        HAZ = 1;
        CZ = 0.25;
        LTR = true;

        for (NW = NMin; NW < NMax; NW++)
        {
            Za = ZaMin;
            while (Za != ZaMax)
            {
                Zg = ZgMin;
                while (Zg != ZgMax)
                {
                    
                    if (CheckLTR(Za, Zg)) goto M80; //проверка наличия общих множителей колес ступени при требовании износостойкости
                    if (CheckLTR(Za, NW)) goto M100;//проверка общих множителей числа зубьев солнца и числа сателлитов при требовании износостойкости

                    if (Zg / Za < 1) goto M80;// проверка допустимости передаточного отношения ступени a-g
                    if (Zg / Za > 6) goto M100;// проверка допустимости передаточного отношения ступени a-g

                  
                    GeomParamSolnSAT();//вычисление геометрических параметров ступени солнце-сателлит1

                    if (DR1 > ag) goto M80;// проверка влезания механизма в габарит

                    Zf = ZfMin;

                    while (Zf != ZfMax)
                    {

                        Zb = (int)(Zf + ((Za + Zg) * M1 / M2));
                        //if (CheckLTR(Zb, NW)) goto M200vix;
                       // if (CheckLTR(Zb, Zf)) goto M50;

                        if (Zb / Zf < 1.4) goto M50;// проверка допустимости передаточного отношения ступени f-b
                        if (Zb / Zf > 8) goto M200vix;// проверка допустимости передаточного отношения ступени f-b                    

                        if (Interfer(Zf, Zb, HAZ) || Sosedstvo(Za, Zg, Zf, Zb, M1, M2, NW) || Sborka(Za, Zg, Zf, Zb, NW)) goto M50;
                        //проверка невыполнения какого-либо из условий: отсутствия интерференции, соседства, сборки
                        
                        GeomParamSolnKOR();//вычисление геометрических параметров ступени сателлит2-корона

                        Y = Math.Round((Aw2 - Aw1) / M1,2);
                        if (YD<Y&&Y!=0) goto M50;//проверка вычисленноого коэффициента суммарного смещения на вхождение в предел

                        U = Math.Round((double)(1 + Zg * Zb / (Za * Zf)),2);
                        DU = (UT - U) * 100 / UT;
                        if (Math.Abs(DU) > du) goto M80;
                        if (DR2 > ag) goto M50; //проверка влезания механизма в габарит

                        if (DR1 > DR2) A = DR1; else A = DR2; //принятие габарита механизма как наибольшего из габаритов ступеней
                        goto M200;//окончание расчета


                    M50: Zf++;
                    }

                M80: Zg++;
                }

            M100: Za++;
            }
        M200vix:;
        }
    M200: N = NW;
        SetResult();

    }


    public void ZTMM46pervayanormalnaya()
    {
        LTR1 = 1;
        LTR2 = 1;
        YD = 0.5;

        //ZaMin = 20;ZaMax = 30; ZgMin = 25;ZgMax =50; ZfMin = 22; ZfMax = 40; M1 = 0.5;M2 = 0.5; NMin = 2; NMax = 3; UT = 9; du = 10; ag = 60;
        ZaMin = 18; ZaMax = 30; ZgMin = 25; ZgMax = 65; ZfMin = 22; ZfMax = 40; M1 = 0.4; M2 = 0.5; NMin = 2; NMax = 3; UT = 15; du = 10; ag = 60;
        HAZ = 1;
        CZ = 0.25;
        
        for (NW = NMin; NW < NMax; NW++)
        {
            Za = ZaMin;
            while (Za != ZaMax)
            {
                Zg = ZgMin;
                while (Zg != ZgMax)
                {

                    if (LTR2 == 1){ if (Za % Zg == 0 || Zg % Za == 0) goto M80; }
                    if (LTR1 == 1) { if (Za % NW == 0) goto M100; }

                    if (Zg / Za < 1) goto M80;
                    if (Zg / Za > 6) goto M100;

                    Da = M1 * Za;
                    Dg = M1 * Zg;
                    Aw1 = (Da + Dg) / 2;
                    DR1 = Aw1 + Dg;

                    if (DR1 > ag) goto M80;//vlezla li 1 stupen

                    Zf = ZfMin;

                    while (Zf != ZfMax)
                    {
                        
                        Zb = (int)(Zf + ((Za + Zg) * M1 / M2));
                                                
                        
                        //if (LTR1 == 1) { if (Zb % NW == 0) goto M50; }
                        
                        //if (LTR2 == 1) { if (Zb % Zf == 0 || Zf % Zb == 0) goto M50; }//iznosostoikost

                        if (Zb / Zf < 1.4) goto M50;
                        if (Zb / Zf > 8) goto M200vix;//realizatcia

                        if ((Zf * Zf - 34) / (2 * Zf - 34)>Zb && Zf<=27 && HAZ==1) goto M50;//otsytstvie interfer
                        if (Zb - Zf < 7 && Zf > 27) goto M50; //otsutstvie interfer

                        if ((Za + Zg) * Math.Sin(3.14 / NW) - (Zf + 2) * M2 / M1 < 5&&NW>=3) goto M50;//sosedstvo
                        if ((Zb * Zg + Za * Zf) / (Zg * NW) - Math.Round((double)(Zb * Zg + Za * Zf) / (Zg * NW), 0) != 0&&NW>=2) goto M50;//sborka



                        Df = M2 * Zf;
                        Db = M2 * Zb;
                        Aw2 = (Db - Df) / 2;
                        DR2 = M2 * (Zb + 2 * (HAZ + CZ));

                        Y = (Aw2 - Aw1) / M1;
                        if (YD < Y&&Y!=0) goto M50;

                        U = 1 + Zg * Zb / (Za * Zf);
                        DU = (UT - U) * 100 / UT;
                        if (Math.Abs(DU) > du) goto M80;
                        if (DR2 > ag) goto M50;
                        
                        if (DR1 > DR2) A = DR1; else A = DR2;
                        goto M200;





                    M50: Zf++;
                    }

                M80: Zg++;
                }

            M100: Za++;
            }
        M200vix:;
        }M200:N=NW;
    }
//=======
//                    M50: Zf++;
//                    }

//                M80: Zg++;
//                }
//>>>>>>> raschet

//            M100: Za++;
//            }
//        M200vix:;
//        }
//    M200: N = NW;
        //SetResult();



            //-------------------------------Планетарка утро 28.02.20---------------------------------------------
            //public void ZTMM46()
            //{
            //    // Заполнение всех полей
            //    LTR1 = 1;
            //    LTR2 = 1;
            //    YD = 0.5;

            //    //ZaMin = 20;ZaMax = 30; ZgMin = 25;ZgMax =50; ZfMin = 22; ZfMax = 40; M1 = 0.5;M2 = 0.5; NMin = 2; NMax = 3; UT = 9; du = 10; ag = 60;
            //    ZaMin = 18; ZaMax = 30; ZgMin = 25; ZgMax = 65; ZfMin = 22; ZfMax = 40; M1 = 0.4; M2 = 0.5; NMin = 2; NMax = 4; UT = 15; du = 10; ag = 60;
            //    for (NW = NMin; NW < NMax; NW++)//200
            //    {
            //        double X = Math.Sin(Math.PI / NW);

            //        for (Za = ZaMin; Za < ZaMax; Za++)//100
            //        {   //if (IP == 2||IP==4||IP==3) break;
            //            for (Zg = ZgMin; Zg < ZgMax; Zg++)//80
            //            {
            //                //if ((Za + Zg) * X - Zg - 7 < 0) break; // then goto 80
            //                SV15(2, Za, Zg, M1);
            //                //if ((IP == 1)||(IP==3))break;// then goto 100;
            //                //if (IP == 1&&NW!=NMin) goto M100;// goto 80 раскомментировано   для LTR2=0 25.02.20
            //                //if (IP == 3) { break; } //Временно коммент 23.02.20
            //                Da = D1;
            //                Dg = D2;
            //                Aw1 = AW;
            //                DR1 = DR;

            //                for (Zf = ZfMin; Zf < ZfMax; Zf++)
            //                {
            //                    if (((Za + Zg) * X - (M2 / M1) * (Zf + 2) - 5) < 0) goto M80; //goto 80

            //                    SV18();
            //                    SV15(3, Zf, Zb, M2);
            //                    Df = D1;
            //                    Db = D2;
            //                    Aw2 = AW;
            //                    DR2 = DR;

            //                    if (IP == 1) break;
            //                    if (IP == 3) goto M80;// then goto 50;
            //                                          //if (IP == 1) { break; }    // then goto 80; временно коммент 23.02.20
            //                                          //if (IP == 3) { Zg = ZgMax-1; break; }//добавлено 23.02.20
            //                    //if (IP == 2) goto M200;
            //                    Uvr = 1 + Zg * Zb / (Za * Zf);
            //                    DU = (UT - Uvr) * 100 / UT;
            //                   if ((du - Math.Abs(DU)) < 0) break;
            //                    SV6(Aw1, Aw2, M1);             //    SV6 (YD, AW1, AW2, 1, Y, IP);
            //                    if (IP != 2) break;// then goto 50;


            //                    if (Y < 0 || Y > 0)
            //                    {

            //                        DR1 = 2 * Aw2 + Dg;
            //                        if (DR1 - ag > 0) goto M100;

            //                    }

            //                    if (DR1 - DR2 <= 0) A = DR2; else A = DR1;


            //                    SV17();
            //                    N = NW;
            //                    if (IP == 2) goto M200;
            //                    // if (IP==3) break;// раскомментить при необх закоменчено 23.02.20
            //                    //if (JPR == 0) break;



            //                }//50   
            //                 //if (IP == 3) break;
            //                 ////if ((DR1 - ag >0)||IP==1||IP==3) break;//bilo >=
            //                 //if ( DR2 - ag < 0) break;

            //            }//80
            //        M80:;
            //            if (IP == 2) break;
            //            //if (IP == 3) break;

            //        // if (IP == 3 || IP == 2) break;//временно закомментированно 23.02.20
            //        //if((IP == 3)||(IP==1)) break;
            //        /////if ((IP == 3)|| (DR1 - ag > 0)||(IP==1)) break;
            //        //if (DR1 - ag < 0 || DR2 - ag < 0) break;


            //        }//100
            //    M100:;
            //        //if (IP == 2) break;
            //        //if (IP == 3) break;
            //        //if (IP == 3||IP==1) break;
            //        N = NW;
            //        //if (IP == 2||IP==0) break;
            //        // if ((IP == 0) || (IP == 2) || (IP == 4)) break;//добавлено 23.02.20

            //    }//200
            //M200:;
            //     // N = NW; временно закомменчено
            //     //// Zg = Zg - 1;
            //     // Zb = Zb + 1;

            //}


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


            //private void SV15(int KPZ, int K1, int K2, double SM) {

            //    double Ulok =(double) K2 /(double) K1;

            //                    if (KPZ == 1) goto M4;
            //                    if (KPZ == 2) goto M2;
            //                    if (KPZ == 3) goto M3;
            //                    M2: if (1 - Ulok <= 0) goto M4; else goto M40;
            // M3: double C = (K1 * K1 - 34) / (2 * K1 - 34);
            // if (K2 - C >= 0) goto M5; else goto M40;
            //M4: if (Ulok - 6 <= 0) goto M6; else goto M25;
            //M5: if (Ulok - 8 <= 0) goto M6; else goto M25;
            //M6: if (LTR1 <= 0) goto M10; else goto M7;
            //M7: if (KPZ - 3 >= 0) goto M9; else goto M8;
            //M8: SV1(K1, NW);
            //    if(JPR <= 0) goto M25; else goto M10;
            //M9: SV1(K2, NW);
            //    if (JPR <= 0) goto M40; else goto M10;
            //M10: if (LTR2 <= 0) goto M17; else goto M15;
            //M15: SV2(K1, K2);
            //    if (MPR <= 0) goto M40; else goto M17;
            //M17: SVZ1(SM, 0);
            //    D1 = SM * (K1 + 2 * HAZ);
            //    if (KPZ - 3 >= 0) goto M20; else goto M18;
            //M18: D2 = SM * (K2 + 2 * HAZ);
            //    AW = 0.5 * SM * (K1 + K2);
            //    DR = 2 * AW + D2;
            //    goto M21;
            //M20: D2 = SM * (K2 + 2 * (HAZ + CZ));
            //    AW = 0.5 * SM * (K2 - K1);
            //    DR = D2;
            //M21: if (DR - ag <= 0) goto M30; else goto M25;
            //    M25: IP = 1;
            //    return;
            //M30: IP = 2;
            //    return;
            //M40: IP = 3;
            //    return;     

  

    }

