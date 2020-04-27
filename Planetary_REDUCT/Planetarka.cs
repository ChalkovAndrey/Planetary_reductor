using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Planet:INotifyPropertyChanged
{   //-------------------------------РАССЧИТЫВАЕТСЯ МЕТОДАМИ---------------------------------------------------
    public int Za, Zb, Zg, Zf, N; // Z1 - число зубьев солнечного, Z2 - число зубьев сателлита
                                   //Z3 - число зубьев короны, N - число сателлитов, m - модуль ступени
                                   // U - передаточное отношение
    private double U, Aw1, Aw2, A; // U - передаточное отношение редуктора, 
                                               //M1 - модуль первой ступени,  M2 - модуль второй ступени 


    //-----------------------------------------ВВОДИТСЯ ИЗ ИНТЕРФЕЙСА-----------------------------------------
  
    private int zamin;
    public int ZaMin { get { return zamin; } set { zamin = (int)value; OnPropertyChanged(); } }
    public static List<string> Result = new List<string>(13);
    private int zamax;
    public int ZaMax { get { return zamax; } set { zamax = value; OnPropertyChanged(); } }
    private int zgmin;
    public int ZgMin { get { return zgmin; } set { zgmin = value; OnPropertyChanged(); } }
    private int zgmax;
    public int ZgMax { get { return zgmax; } set { zgmax = value; OnPropertyChanged(); } }
    private int zfmin;
    public int ZfMin { get { return zfmin; } set { zfmin = value; OnPropertyChanged(); } }
    private int zfmax;
    public int ZfMax { get { return zfmax; } set { zfmax = value; OnPropertyChanged(); } }
    private int nMin;
    public int NMin { get { return nMin; } set { nMin = value; OnPropertyChanged(); } }
    private int nMax;
    public int NMax { get { return nMax; } set { nMax = value; OnPropertyChanged(); } }
    private double m1;
    public double M1 { get { return m1; } set {  m1 = value; OnPropertyChanged(); } }
    private double m2;
    public double M2 { get { return m2; } set { m2 = value; OnPropertyChanged(); } }
    private double _du;
    public double du { get { return _du; } set { _du = value; OnPropertyChanged(); } }
    private double ut;
    public double UT { get { return ut; } set { ut = value; OnPropertyChanged(); } }
    private double _ag;
    public double ag { get { return _ag; } set { _ag = value; OnPropertyChanged(); } }
    private double haz;
    public double HAZ { get { return haz; } set { haz = value; OnPropertyChanged(); } }
    private double cz;
    public double CZ { get { return cz; } set { cz = value; OnPropertyChanged(); } }
    private double yd;
    public double YD { get { return yd; } set { yd = value; OnPropertyChanged(); } }
    public bool LTR;

    private double DU; //UT - требуемое передаточное отношение, du - требуемая погрешность, ag - требуемый габарит
    private int LTR1, LTR2; // Маркеры для ответвления в расчетах

 //-----------------------------------------------------------------------------------------------------------


//------------------------------------СЕРВИСНЫЕ ПЕРЕМЕННЫЕ (ИСПОЛЬЗУЮТСЯ ТОЛЬКО В РАСЧЕТЕ------------------------
    private int IP, KA, KG, KF, KB, NW, JPR, MPR; // JPR, MPR - индикаторы
    private double Da,Db, Dg, Df, DR1, DR2, DR, AW, Uvr, Y, D1, D2, ALF;

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


    public void SetExample()
    {
        ZaMin = 18;
        ZaMax = 30;
        ZgMin = 25;
        ZgMax = 65;
        ZfMin = 22;
        ZfMax = 40;
        M1 = 0.4;
        M2 = 0.5;
        NMin = 2;
        NMax = 4;
        UT = 15; du = 7; ag = 60; YD = 0.5; HAZ = 1; CZ = 0.25;
    }

    public void ClearInput()
    {
        ZaMin   = 0;
        ZaMax   = 0;
        ZgMin   = 0;
        ZgMax   = 0;
        ZfMin   = 0;
        ZfMax   = 0;
        M1      = 0;
        M2      = 0;
        NMin    = 0;
        NMax    = 0;
        UT = 0; du = 0; ag = 0; YD = 0; HAZ = 0; CZ = 0;
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
        DR1 = Math.Round(Aw1 + Dg/2,2);//!!!!!!!!
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



        //ZaMin = 20;ZaMax = 30; ZgMin = 25;ZgMax =50; ZfMin = 22; ZfMax = 40; M1 = 0.5;M2 = 0.5; NMin = 2; NMax = 3; UT = 9; du = 10; ag = 60;
        //ZaMin = 18; ZaMax = 30; ZgMin = 25; ZgMax = 65; ZfMin = 22; ZfMax = 40; M1 = 0.4; M2 = 0.5; NMin = 2; NMax = 4; UT = 15; du = 7; ag = 60; YD = 0.5; HAZ = 1; CZ = 0.25;
       // LTR = false;

        for (NW = NMin; NW <= NMax; NW++)
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

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName]string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
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
        
        for (NW = NMin; NW <= NMax; NW++)
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
}

