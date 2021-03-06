
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Planetary_REDUCT
{

  
  
  public  class Wave : INotifyPropertyChanged
    {//---------------------------------------РАССЧИТЫВАЮТСЯ В МЕТОДАХ----------------------------------------------------
        public List<string> NameParams { get; set; } = new List<string> { "����� ������ ������ f", "����� ������ ������ �",
            "����� ������� ����������", "������ �������", "������������ ���������","����������� �������� ������ F",
            "����������� �������� ������ C","������� ����������� ���������� ������ F","������� ���������� ������ ������ F","������� ���������� ������ ������ F"
        ,"������� ����������� ���������� ������ �","������� ���������� ������ ������ �","������� ���������� ������ ������ �",
            "������� ������ ������� ������� ��� �������","T������ ������ ������� ������","������ ����� ������� ������","������ ����� �������� ������","������ ������ ��� �������� �������� ������",
        "����� ������� ������� ������","������� ����� �������� ������ ��� �������"};



       
        private double U, Dgp; //sigmacz, sigmac, bwf, bwc, l, bk, hob, mfc;
        //U - передаточное отношение волнового редуктора
        //Dgp - диаметр гибкого подшипника
        private double[] az = new double[8];//Массив геметрических параметров зацепления колес F и С
                                            //-----------------Разметка массива геометрических параметров-----------------------------------------
                                            //az[0] - Xf - коэф-т смещения колеса F; az[1] - Xc - коэф-т смещения колеса С
                                            //az[2] - df - диаметр делительной окружности колеса F, 
                                            //az[3] - daf - диаметр окружности вершин колесаF,
                                            //az[4] - dff - диаметр окружности впадин колеса F
                                            //az[5], az[6], az[7] - dc,dac,dfc - аналогичные параметры для колеса С

        private double[] ak = new double[7];// массив конструкционных параметров зацепления
                                            //---------Разметка массива конструкционных параметров-------------------------------------
                                            //ak[0] - sigmacz - толщина стенки гибкого стакана под зубьями; 
                                            //ak[1] - sigmac - толщина стенки стакана колеса;
                                            //ak[2] - bwf -ширина венца гибкого колеса ; 
                                            //ak[3] - bwc - ширина венца жесткого колеса; 
                                            //ak[4] - bk - ширина пояска для снижения перекоса зубьев; 
                                            //ak[5] - l - длина стакана гибкого колеса;
                                            //ak[6] - hob - толщина обода жесткого колеса под зубьями;
    

        public int Zf, Zc, Ngp;
        //����� ������ ����� F, C
        //Ngp - ����� (�����������) ������� ����������


        public double modulfc;//modulfc - модуль ступени FC, определяемый на этапе конструирования объекта
       public string[,] Result;


        //az[0] - Xf - ����-� �������� ������ F; az[1] - Xc - ����-� �������� ������ �
        //az[2] - df - ������� ����������� ���������� ������ F, 
        //az[3] - daf - ������� ���������� ������ ������F,
        //az[4] - dff - ������� ���������� ������ ������ F
        //az[5], az[6], az[7] - dc,dac,dfc - ����������� ��������� ��� ������ �

        //ak[0] - sigmacz - ������� ������ ������� ������� ��� �������; 
        //ak[1] - sigmac - ������� ������ ������� ������;
        //ak[2] - bwf -������ ����� ������� ������ ; 
        //ak[3] - bwc - ������ ����� �������� ������; 
        //ak[4] - bk - ������ ������ ��� �������� �������� ������; 
        //ak[5] - l - ����� ������� ������� ������;
        //ak[6] - hob - ������� ����� �������� ������ ��� �������;

        //U - передаточное отношение волнового редуктора
        //Dgp - диаметр гибкого подшипника


        //--------------------------------------------------�������� � ����������-------------------------------------------------------


        public double INo    { get { return mo;   } set { mo    = (double)value; OnPropertyChanged("INo"); } }
        public double INk    { get { return mk;   } set { mk    = (double)value; OnPropertyChanged("INk"); } }
        public double Itout  { get { return Tout; } set { Tout  = (double)value; OnPropertyChanged("Itout"); } }
        public double Inout  { get { return Nout; } set { Nout  = (double)value; OnPropertyChanged("Inout"); } }
        public double Idr    { get { return Dr;   } set { Dr    = (double)value; OnPropertyChanged("Idr"); } }
        public double Ihaz   { get { return Haz;  } set { Haz   = (double)value; OnPropertyChanged("Ihaz"); } }
        public double Icz    { get { return Cz;   } set { Cz    = (double)value; OnPropertyChanged("Icz"); } }

        //PR - ����� ����� ���������� ��� ������ ������� ������� ������������� (����� ���������� �� �������� � ������)
        //Tout - ������������ ������ ��������
        //nout - ������� �������� ��������� ����
        //Dr - ����������� ���������� ������ ���������� ������� ������� ������
        //No - ��������� ����� ������ � ������� �������; Nk - �������� ����� ��������� ������ ������ � ������� �������
        //--------------------------------------------------ВВОДЯТСЯ В ИНТЕРФЕЙСЕ-------------------------------------------------------
        public int PR { get; set; } = 1;//����� �����, �������� ���
        //public int No { get; set; }
        //public int Nk { get; set; }
        public double mo    { get; set; }
        public double mk    { get; set; }
        public double Tout  { get; set; }
        public double Nout  { get; set; }
        public double Dr    { get; set; }
        public double Haz   { get; set; }
        public double Cz    { get; set; }

      
        
       
        //--------------------------------------------------���������------------------------------------------


        private double  haz, cz;// �������� ������������ ������
        private int     IP;//�������� ��� ��������
        private double  dfpp;//��������� ������� ������� ����������. ������������ � ����� ��������������� �����
        private double  Dp;//��������� �������� ������� ������� ����������. ������������ � ����� ��������������� �����
                          //double dp;//������� ���������� 19.02.20 ��������� � ����������������
        private double  ngp;//���������� ������� �������� ����������. ������������ ��� �������� ������������ ����������
        private double  ngpp;//���������� ���������� ������� �������� ���������� ����������. ������������ ��� �������� ������������ ����������.

        private double  sm;//��������� ������. ����� ����������� � modulfc



        // SVOL2 - ����� ������� ���������� �� ����

        public void SetExample()
        {
            INo     = 0.3;
            INk     = 1;
            Itout   = 125;
            Inout   = 35;
            Idr     = 70;
            Ihaz    = 1;
            Icz     = 0.25;
        }

        public void ClearInput()
        {
            INo     = 0;
            INk     = 0;
            Itout   = 0;
            Inout   = 0;
            Idr     = 0;
            Ihaz    = 0;
            Icz     = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void ResetData()
        {
            for (int i = 0; i < az.Length; i++)
            {
                az[i] = 0;
            }
            for (int i = 0; i < ak.Length; i++)
            {
                ak[i] = 0;
            }
            Zf = 0;Zc = 0;modulfc = 0;Ngp = 0;U = 0;
            
        }
        public string[,] GenericResultMassive(ref string[,] result)
        {
            string[] MainPrams = new string[] { Zf.ToString(), Zc.ToString(), Ngp.ToString(), modulfc.ToString(), U.ToString() };
            string[] Ed = new string[] { "��", "��", "", "", "", "", "", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��" };
            string[] Appelation = new string[] { "Zf", "Zc", "NGP", "Mf", "U", "Xf", "Xc", "Df", "Daf", "Dff", "Dc", "Dac", "Dfc", "Sigmacz", "Sigmac", "Bwf", "Bwc", "Bk", "L", "Hob" };
        result = new string[20,4];
            for (int i = 0; i < NameParams.Count; i++)
            {
                result[i,0] = NameParams[i];
                result[i, 2] = Ed[i];
                result[i, 1] = Appelation[i];
                if (i < 5)
                    result[i, 3] = MainPrams[i];
                else if (i < 13)
                    result[i, 3] = az[i - 5].ToString();
                else
                    result[i, 3] = ak[i - 13].ToString();

            }
            return result;
        }

        private void Svol2()
        {
            
            int[,] a = new int[11, 5]; 

            a[0, 0] = 806;
            a[1, 0] = 808;
            a[2, 0] = 809;
            a[3, 0] = 811;
            a[4, 0] = 812;
            a[5, 0] = 815;
            a[6, 0] = 818;
            a[7, 0] = 822;
            a[8, 0] = 824;
            a[9, 0] = 830;
            a[10, 0] = 836;

            a[0, 1] = 42;
            a[1, 1] = 52;
            a[2, 1] = 62;
            a[3, 1] = 72;
            a[4, 1] = 80;
            a[5, 1] = 100;
            a[6, 1] = 120;
            a[7, 1] = 150;
            a[8, 1] = 160;
            a[9, 1] = 200;
            a[10, 1] = 240;

            a[0, 2] = 30;
            a[1, 2] = 40;
            a[2, 2] = 45;
            a[3, 2] = 55;
            a[4, 2] = 60;
            a[5, 2] = 75;
            a[6, 2] = 90;
            a[7, 2] = 110;
            a[8, 2] = 120;
            a[9, 2] = 150;
            a[10, 2] = 180;

            a[0, 3] = 7;
            a[1, 3] = 8;
            a[2, 3] = 9;
            a[3, 3] = 11;
            a[4, 3] = 13;
            a[5, 3] = 15;
            a[6, 3] = 18;
            a[7, 3] = 24;
            a[8, 3] = 24;
            a[9, 3] = 30;
            a[10, 3] = 35;

            a[0, 4] = 4000;
            a[1, 2] = 4000;
            a[2, 4] = 3500;
            a[3, 4] = 3500;
            a[4, 4] = 3500;
            a[5, 4] = 3000;
            a[6, 4] = 3000;
            a[7, 4] = 2500;
            a[8, 4] = 2000;
            a[9, 4] = 1600;
            a[10, 4] = 1600;

            for (int i = 0; i <= 10; i++)
            {

                
                if ((Dp <= a[i, 1]) && (Nout <= a[i, 4]))

                {
                    Dgp = a[i, 1];
                    Ngp = a[i, 0];
                    ngpp = a[i, 4];
                    break;
                }

            }
            return;

        }


        // SVOL3 - ������ ����� ������ ������� ������

        private void Svol3()
        {
            Zf = (int)Math.Round((Dgp / sm - 5.88 + (1.96 * (haz + cz))), 0);
            Zc = Zf + 2;// ��������� 19.02.20 ������ ������������
            return;
        }

        // ������ SVOL4 - u ip


        private void Svol4()
        {
            if (PR <= 1)
            {

                U = 0.5 * Zf;//������ kf ���������� Zf 19.02.20

            }
            else U = 0.5 * Zf;
            if (U <= 70)
            {
                IP = 1;
            }
            else
            {
                if ((300 - U) < 0)
                {
                    IP = 3;
                }
                else IP = 2;
            }
            return;
        }


        // svol5 - ������. az

        private void Svol5()
        {
            double qw = 1.1; double qf = 0.4;

            az[0] = Math.Round((3 + 0.01 * Zf),2);                          // Xf
            az[1] = Math.Round(az[1] - 1 + qw * (1 + 0.00005 * qf * Zf),2); // Xc
            az[2] = Math.Round(sm * Zf,2);                                  // df
            az[3] = Math.Round(az[2] + 2 * (az[1] + qf) * sm,2);            //daf
            az[4] = Math.Round(az[2] + 2 * (az[1] - haz - cz) * sm,2);      //dff
            az[5] = Math.Round(sm * Zc,2);                                  //dc
            az[6] = Math.Round(az[5] + 2 * (az[1] - haz) * sm,2);           //dac
            az[7] = Math.Round(az[6] + 2 * (haz + cz + qf) * sm,2);         //dfc
            return;
        }


        // svol6 - ������� ak

        private void Svol6()
        {
            ak[0] = Math.Round(sm * (0.5 * Zf + az[0] - (haz + cz)) - 0.5 * Dgp,2);//sigmacz
            ak[1] = Math.Round(0.7 * ak[0],2);  //sigmac
            ak[2] = Math.Round(0.2 * az[2],2);  //
            ak[3] = Math.Round(ak[2] + 3,2);    //
            ak[4] = Math.Round(0.06 * az[3],2); //
            ak[5] = Math.Round(1.0 * az[2],2);  //
            ak[6] = Math.Round(0.18 * az[5],2); //
            return;
        }


        //-----------------------���� ��������������� ������� ������ "Wave"-----------------------------------------

        //----------------------------------------------------------------------------------------------------------
        public void Construction()
        {
            int No, Nk;
            No = 0; Nk = 5;
            haz = Haz;
            cz = Cz;

            double[] am = new double[6];//���������� ������� ������� ��� ����������� ��������

            am[0] = 0.3;
            am[1] = 0.4;
            am[2] = 0.5;
            am[3] = 0.6;
            am[4] = 0.8;
            am[5] = 1.0;//������������� ������� �������

            double dfp = 15.5 * (Math.Pow(Tout, 1 / 3));//��������� ���������� �������� ���������� �� ����������� �����������
            double dfr = 1.1 * Dr;//��������� ���������� �������� ���������� �� ����������� ���������
            if (dfp >= dfr) dfpp = dfp; else dfpp = dfr;//����������� ���������� �������� �������� ������� ����������

            //double sm;
            // ngpp = 0;

            //--------����� �������� �������, � ������������ � ��������� �������, ��� ������������ ������ �� ������� �������
            for (int i = 0; i <= 5; i++)
            {
                if (am[i] == mo) { No = i; }
                if (am[i] == mk) { Nk = i; }

            }
            if (mo == 0.7) { No = 3; }
            if (mk == 0.7) { Nk = 4; }
            //----------------------------------------------------------------------------------------------------------------
            //--------------------------����� �� ������� ������� ������������ �������� ���������� ���������-------------------
            for (int n = No; n <= Nk; n++)
            {
                //double sm;
                sm = am[n];
                Dp = dfpp + 0.99 * sm * (6 - 2 * (haz + cz));
                if (Dp <= 240)
                {
                    Svol2();
                    Svol3();
                    Svol4();
                    if (IP == 1)
                    {
                        Dp = Dp + 5;
                    }
                    if (IP == 2)
                    {


                        ngp = Nout * U;

                        if ((ngpp - ngp) >= 0)
                        {
                            Svol5();
                            Svol6();
                            modulfc = sm;
                            break;

                        }

                    }
                    if (IP == 3)
                    {
                        Svol5();
                        Svol6();

                        modulfc = 0;//��������� ������ - ��������� ����, ��� ���� �������� ��������� � ������������� ������� ���������

                        break;

                    }
                }
            }
          Result = GenericResultMassive(ref Result);


        


        }
    }
}

