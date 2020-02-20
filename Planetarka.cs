using System;

public class Class1
{   //-------------------------------РАССЧИТЫВАЕТСЯ МЕТОДАМИ---------------------------------------------------
    private int Za, Zb, Zg, Zf, N; // Z1 - число зубьев солнечного, Z2 - число зубьев сателлита
                                   //Z3 - число зубьев короны, N - число сателлитов, m - модуль ступени
                                   // U - передаточное отношение
    private double U, M1, M2, Aw1, Aw2, DU, A; // U - передаточное отношение редуктора, 
                                               //M1 - модуль первой ступени,  M2 - модуль второй ступени 

        
 //-----------------------------------------ВВОДИТСЯ ИЗ ИНТЕРФЕЙСА-----------------------------------------
    private int zAmin, zAmax, zGmin, zGmax, zFmin, zFmax, Nmin, Nmax; // Nmin, Nmax - диапазон чисел сателлитов
    private double UT, du, ag; //UT - требуемое передаточное отношение, du - требуемая погрешность, ag - требуемый габарит
    private int LTR1, LTR2; // Маркеры для ответвления в расчетах
                            //-----------------------------------------------------------------------------------------------------------


//------------------------------------СЕРВИСНЫЕ ПЕРЕМЕННЫЕ (ИСПОЛЬЗУЮТСЯ ТОЛЬКО В РАСЧЕТЕ------------------------
    private int IP, KA, KG, KF, KB, NW, JPR, MPR; // JPR, MPR - индикаторы
    private double DA, DG, AW1, DR1, DB, DR2, AW2, DR, AW, Uvr, DUR, Y, YD=1, D1, D2, Df, ALF, HAZ, CZ;

    private int KPZ; // Управляющий признак, вводится(определяется по картинке в интерфейсе

    //---------------------------------------------------------------------------------------------------------------
//YD=1 - допущение (в большинстве случаев)
    private void SVZ1(double SM, int LH)
    {
        ALF = Math.PI / 9;
        if (LH < 0) { HAZ = 1; } else HAZ = 1.1;
        if (SM - 0.5 <= 0) { CZ = 0.5; }
        else
        {
            if (SM - 1 <= 0) { CZ = 0.35; } else CZ = 0.25;
        }
    }


    private void SV1(int K, int N) { if ((K % N) == 0) JPR = 0; else JPR = 1; }

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

        Y = (AW2 - AW1) / SM1;
        if (Y < 0) { IP = 4; return; } ;    
        if (Y == 0) { IP = 2; return;};    
        if ((Y - YD) > 0) IP = 4;                            
    }



    private void SV15(int KPZ, int K1, int K2, double SM)
    {
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

    private void

    public void construction()
    {

    }


    public Class1()
	{
	}
}
