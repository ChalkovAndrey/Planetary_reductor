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
    public float M1 { get; set; }
    public float M2{ get; set; }
    public double du { get; set; }
    public double UT { get; set; }
    public double ag { get; set; }
    private double DU; //UT - требуемое передаточное отношение, du - требуемая погрешность, ag - требуемый габарит
    private int LTR1, LTR2; // Маркеры для ответвления в расчетах
    private bool LTR = false;
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
        Da = M1 * Za;
        Dg = M1 * Zg;
        Aw1 = (Da + Dg) / 2;
        DR1 = Aw1 + Dg;
    }

    private void GeomParamSolnKOR()//вычисление геометрических параметров ступени сателлит2-корона
    {
        Df = M2 * Zf;
        Db = M2 * Zb;
        Aw2 = (Db - Df) / 2;
        DR2 = M2 * (Zb + 2 * (HAZ + CZ));
    }

    public void ZTMM46()
    {
        LTR1 = 1;
        LTR2 = 1;
        YD = 0.5;

        //ZaMin = 20;ZaMax = 30; ZgMin = 25;ZgMax =50; ZfMin = 22; ZfMax = 40; M1 = 0.5;M2 = 0.5; NMin = 2; NMax = 3; UT = 9; du = 10; ag = 60;
        //ZaMin = 18; ZaMax = 30; ZgMin = 25; ZgMax = 65; ZfMin = 22; ZfMax = 40; M1 = 0.4f; M2 = 0.5f; NMin = 2; NMax = 3; UT = 15; du = 10; ag = 60;
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
                        //if (CheckLTR(Zb, Zf)) goto M50;

                        if (Zb / Zf < 1.4) goto M50;// проверка допустимости передаточного отношения ступени f-b
                        if (Zb / Zf > 8) goto M200vix;// проверка допустимости передаточного отношения ступени f-b                    

                        if (Interfer(Zf, Zb, HAZ) || Sosedstvo(Za, Zg, Zf, Zb, M1, M2, NW) || Sborka(Za, Zg, Zf, Zb, NW)) goto M50;
                        //проверка невыполнения какого-либо из условий: отсутствия интерференции, соседства, сборки

                        GeomParamSolnKOR();//вычисление геометрических параметров ступени сателлит2-корона

                        Y = (Aw2 - Aw1) / M1;
                        if (YD < Y && Y != 0) goto M50;//проверка вычисленноого коэффициента суммарного смещения на вхождение в предел

                        U = 1 + Zg * Zb / (Za * Zf);
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

  
}
