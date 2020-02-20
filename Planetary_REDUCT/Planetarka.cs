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
    public int M1 { get; set; }
    public int M2{ get; set; }
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
                if (JPR == 0) { MPR = 0; return; }
            }
        }
        MPR = 1;
    }


    private void SV6(double AW1, double AW2, double SM1)
    {

       // Вычисление суммарного смещения

        Y = (AW2 - AW1) / SM1;
        if (Y < 0) { IP = 4; return; } ;    
        if (Y == 0) { IP = 2; return;};    
        if ((Y - YD) > 0) IP = 4;                            
    }



    private void SV15(int KPZ, int K1, int K2, double SM)
    {

        // Проверка возможности реализации механизма при заданных числах зубов, модулях
        // и расчет геометрических параметров передачи

        double Ulok, C;
       // ALF = 20;
        //HAZ = 1;
        //CZ = 1;

        //JPR = 0;
        /*procedure SV15(KPZ, K1, K2:integer; SM: real; NW: integer; AG: real;
         LTR1,LTR2: integer; var D1:real; var D2:real;
        var AW:real; var DR:real; var IP:byte);
         var U, C:real;
        MPR,JPR: byte;*/
        Ulok = K2 / K1;
        //--------------------------------------------------------------------------
        //----------Замена закомментированного ниже блока---------------------------
        //--------------------------------------------------------------------------
        if (KPZ == 1) { if (!(Ulok - 6 <= 0)) { IP = 1; return; } };
        if (KPZ == 3)
        {
            C = ((K1 * K1) - 34) / (2 * K1 - 34);
            if ((K2 - C) < 0) { IP = 3; return; }
            else
            { if (!((Ulok - 8) <= 0)) { IP = 1; return; } };
            //goto 25;
        }//goto 3
        if ((KPZ == 2) || (KPZ == 4))
        {
            if (1 - Ulok <= 0)
            {
                if (!(Ulok - 6 <= 0))
                {
                    IP = 1; return;
                }
            }
            else { IP = 3; return;}
        }

        //----------------------------------------------------------------------------------------------------------
        //

        if (!(LTR1 <= 0))
        {
            if (!((KPZ - 3) >= 0))
            {
                SV1(K1, NW);
                if (JPR <= 0) { IP = 1; return; }
            }

            else
            {
                SV1(K2, NW);
                if (JPR <= 0) { IP = 3; return; }

            }
        }


        if (!(LTR2 <= 0))//10:
        {// !then goto 17;
            SV2(K1, K2);
            if (MPR <= 0) { IP = 3; return; }// then goto 40;
        }
        else
        {
            SVZ1(SM, 0);//17:
            D1 = SM * (K1 + 2 * HAZ);
            if ((KPZ - 3) >= 0)
            {
                D2 = SM * (K2 + 2 * (HAZ + CZ));
                AW = SM * (K2 - K1) / 2;
                DR = D2;
                if ((DR - ag) <= 0) { IP = 2; return; };
            }// then goto 20;
            D2 = SM * (K2 + 2 * HAZ);
            AW = SM * (K1 + K2) / 2;
            DR = 2 * AW + D2;
            if ((DR - ag) <= 0) { IP = 2; return; };

        }

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
        if ((LC % LB) == 0) p = 0; else p = 1;// ВМЕСТО SV1(LC, LB, p);
        IP = p;
    }

    private void SV18()
    {

        // Расчет числа зубов короны

        double ZB; int ZBI;
        ZB = (M1 * (Za + Zg) + M2 * Zf) / M2;
        ZBI = (int)Math.Round(ZB, 0);
        if ((ZB - ZBI) == 0) Zb = ZBI; else Zb = ZBI + 1;
    }

    public void ZTMM46()
    {
        // Заполнение всех полей

        for (NW = NMin; NW < NMax; NW++)//200
        {
            double X = Math.Sin(Math.PI / NW);
            double a = (Za + Zg) * X;
            double b = Za * X;
            for (Za = ZaMin; Za < ZaMax; Za++)//100
            {
                for (Zg = ZgMin; Zg < ZgMax; Zg++)//80
                {
                    if ((((Za + Zg) * X) - Zg - 7) < 0) break; // then goto 80
                    else
                    {
                        SV15(2, Za, Zg, M1);
                        if (IP == 1) break;// then goto 100;
                        if (IP == 3) break;// goto 80
                        Da = D1;
                        Dg = D2;
                        Aw1 = AW;
                        DR1 = DR;

                        for (Zf = ZfMin; Zf < ZfMax; Zf++)
                        {
                            if (((Za + Zg) * X - (M2 / M1) * (Zf + 2) - 5) < 0) break;//!!!!!!!!? goto 80
                            //Zb = (int)Math.Round((Zf + (M1 / M2) * (Za + Zg)));
                            SV18();
                            SV15(3, Zf, Zb, M2);
                            Df = D1;
                            Db = D2;
                            Aw2 = AW;
                            DR2 = DR;

                            if (IP == 1) break;    // then goto 50;
                            if (IP == 3) break;    // then goto 80;
                            Uvr = 1 + Zg * Zb / (Za * Zf);
                            DU = (UT - Uvr) * 100 / UT;
                            if ((du - Math.Abs(DU)) < 0) break;
                            SV6(Aw1, Aw2, M1);             //    SV6 (YD, AW1, AW2, 1, Y, IP);
                            if ((IP - 2) > 0) break;// then goto 50;
                            if (Y == 0)
                            {
                                if ((DR1 - DR2) > 0) { A = DR1; }
                                else
                                {
                                    A = DR2;
                                    SV17();
                                }
                            };
                            DR1 = 2 * Aw2 + Dg;
                            if ((DR1 - ag) > 0) break;
                            if ((DR1 - DR2) > 0) A = DR1; else A = DR2;
                            SV17();


                        }//50   
                    }//end else

                    if (IP == 3) break;
                    if ((DR1 - ag) > 0) break;
                }//80
                if (IP == 1) break;
            }//100
            if (IP == 1) break;
        }//200
        N = NW;
    }


    public Planet()
	{

	}
}
