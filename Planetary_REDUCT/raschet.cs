//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Planetary_REDUCT
//{
//   class Planetarka
//   {
//       private int Za, Zg, Zf, Zb, N; // Z1 - число зубьев солнечного, Z2 - число зубьев сателлита
//                                       //Z3 - число зубьев короны, N - число сателлитов, m - модуль ступени
//                                      // U - передаточное отношение
//       private double U, M1, M2, Aw1, Aw2, DU, A; // U - передаточное отношение редуктора, M1 - модуль первой ступени,  M2 - модуль второй ступени 
//       public Planetarka(int zAmin,int zAmax, int zGmin, int zGmax, int zFmin, int zFmax, int Nmin, int Nmax,
//           double UT, double m1,
//           double m2, double du, double ag, int LTR1, int LTR2)  //конструктор по диапазонам чисел зубьев и количеств сателлитов
//       {
//           int IP, KA, KG, KF, KB, NW, JPR, MPR; //BSS
//           double DA, DG, AW1, DR1, DB, DR2, AW2, DR, AW, Uvr, DUR, Y, YD, D1, D2, Df, ALF, HAZ, CZ;
            
//           void SVZ1(double SM, int LH)
//           {
//               ALF = Math.PI / 9;
//               if (LH < 0) { HAZ = 1; } else HAZ = 1.1;
//               if (SM - 0.5 <= 0) { CZ = 0.5; }
//               else
//               {
//                   if (SM - 1 <= 0) { CZ = 0.35; } else CZ = 0.25;
//               }

//           }
//           void SV1(int K, int N) { if ((K % N) == 0) JPR = 0; else JPR = 1; }
//           void SV2(int K1, int K2)
//           {
//               for (int i = 2; i < K1; i++)
//               {
//                   SV1(K1, i);
//                   if (JPR == 0)
//                   {
//                       SV1(K2, i);
//                       if (JPR == 0) { MPR = 0; return; }
//                   }
//               }
//               MPR = 1;
//           }




//           void SV15(int KPZ, int K1, int K2, double m)
//           {
//               double Ulok, C;
//               ALF = 20;
//               HAZ = 1;
//               CZ = 1;

//                JPR = 0;
//              /*procedure SV15(KPZ, K1, K2:integer; SM: real; NW: integer; AG: real;
//               LTR1,LTR2: integer; var D1:real; var D2:real;
//              var AW:real; var DR:real; var IP:byte);
//               var U, C:real;
//              MPR,JPR: byte;*/
//               Ulok = K2 / K1;
//              //--------------------------------------------------------------------------
//              //----------Замена закомментированного ниже блока---------------------------
//              //--------------------------------------------------------------------------
//               if (KPZ == 1) { if (!(((Ulok - 6) <= 0) || ((Ulok - 8) <= 0))) IP = 1; };//goto 4
//               if (KPZ == 3)
//               {
//                   C = ((K1 * K1) - 34) / (2 * K1 - 34);
//                   if ((K2 - C) < 0) { IP = 3; return; }
//                   else
//                   { if (!((Ulok - 8) <= 0)) { IP = 1; return; } };
//                  //goto 25;
//               }//goto 3
//               if ((KPZ == 2) || (KPZ == 4))
//               {
//                   if ((1 - Ulok) <= 0) if (!(((Ulok - 6) <= 0) || ((Ulok - 8) <= 0))) IP = 1;
//                       else IP = 3;//then goto 4 else goto 40;
//                   C = ((K1 * K1) - 34) / (2 * K1 - 34);
//                   if ((K2 - C) < 0) { IP = 3; return; }
//                   else // then goto 40 else goto 5;
//                   if (!((Ulok - 8) <= 0)) { IP = 1; return; } //5://goto 25;
//               }
//              //------------------------------------------------------------------
//             /* if ((1 - Ulok)<= 0) then goto 4 else goto 40;
//              3:C:= (sqr(K1) - 34) / (2 * K1 - 34);
//              if ((K2 - C)< 0) then goto 40 else goto 5;
//              4:if (((U - 6)<= 0)|| ((U - 8) <= 0)) then goto 6 else IP= 1;// goto 25;
//              5:if ((U - 8)<= 0) then goto 6 else IP = 1; //goto 25;*/
//              //----------------------------------------------------------------------

//               if (!(LTR1 <= 0))
//               {
//                   if ((KPZ - 3) >= 0)
//                   {
//                       SV1(K2, NW);
//                       if (JPR <= 0) { IP = 3; return; }
//                   }

//                   else
//                   {
//                       SV1(K1, NW);
//                       if (JPR <= 0) { IP = 1; return; }
//                       else
//                       {// then goto 25 else goto 10;
//                           SV1(K2, NW);//9:
//                           if (JPR <= 0) { IP = 3; return; } // then goto 40;
//                       }
//                   }
//               }
//               /*then goto 10 else goto 7;  6:
//              7:if (KPZ - 3)>= 0 then goto 9;
//              SV1(K1, NW, JPR);
//              if (JPR <= 0) IP = 1; else // then goto 25 else goto 10;
//              9:SV1(K2, NW, JPR);*/
//              if (JPR <= 0) IP = 3; // then goto 40;
//               if (!(LTR2 <= 0))//10:
//               {// !then goto 17;
//                   SV2(K1, K2);
//                   if (MPR <= 0) { IP = 3; return; }// then goto 40;
//               }
//               else
//               {
//                   SVZ1(m, 0);//17:
//                   D1 = m * (K1 + 2 * HAZ);
//                   if ((KPZ - 3) >= 0)
//                   {
//                       D2 = m * (K2 + 2 * (HAZ + CZ));
//                       AW = m * (K2 - K1) / 2;
//                       DR = D2;
//                       if ((DR - ag) <= 0) { IP = 2; return; };
//                   }// then goto 20;
//                   D2 = m * (K2 + 2 * HAZ);
//                   AW = m * (K1 + K2) / 2;
//                   DR = 2 * AW + D2;

//                   if ((DR - ag) <= 0) { IP = 2; return; };

//               } 
//           }


//           void SV6()
//           {
              
//               Y = (AW2 - AW1) / m1;
//               if (Y < 0) IP = 4;     // then goto 7;
//               if (Y == 0) IP = 2;     //then goto 5;
//               if ((Y - YD) > 0) IP = 4;// then goto 7;
//                                       //5:IP:= 2; goto 9;
//                                       //7:IP:= 4;
//                                       //9:end;
//           }

//           void SV18()
//           {
//               double ZB; int ZBI;
//               ZB = (m1 * (KA + KG) + m2 * KF) / m2;
//               ZBI =(int) Math.Round(ZB, 0);
//               if ((ZB - ZBI) == 0) KB = ZBI; else KB = ZBI + 1;
//           }

//           void SV17()
//           {
//               int LC; int LB; int p;
//               LC = KG * KB + KA * KF;
//               LB = KG * NW;
//               if ((LC % LB) == 0) p = 0; else p = 1;// ВМЕСТО SV1(LC, LB, p);
//               IP = p;
//           }

//           for (NW = Nmin; NW < Nmax; NW++)//200
//           {
//               double X = Math.Sin(Math.PI / NW);
//               for (KA = zAmin; KA < zAmax; KA++)//100
//               {
//                   for (KG = zGmin; KG < zGmax; KG++)//80
//                   {
//                       if (((KA + KG) * X - KG - 7) < 0) break;
//                       else
//                       {
//                           SV15(2, KA, KG, m1);
//                           if (IP == 1) break;// then goto 100;
//                           if (IP == 3) break;// goto 80
//                           DA = D1;
//                           DG = D2;
//                           AW1 = AW;
//                           DR1 = DR;

//                           for (KF = zFmin; KF < zFmax; KF++)
//                           {
//                               if (((KA + KG) * X - (m2 / m1) * (KF + 2) - 5) < 0) break;
//                                KB = (int)Math.Round((KF + (M1 / M2) * (KA + KG)));
//                               SV18();
//                               SV15(3, KF, KB, m2);
//                               Df = D1;
//                               DB = D2;
//                               AW2 = AW;
//                               DR2 = DR;

//                               if (IP == 1) break;    // then goto 50;
//                               if (IP == 3) break;    // then goto 80;
//                               Uvr = 1 + KG * KB / (KA * KF);
//                               DUR = (UT - Uvr) * 100 / UT;
//                               if ((du - Math.Abs(DUR)) < 0) break;
//                               SV6();             //    SV6 (YD, AW1, AW2, 1, Y, IP);
//                               if ((IP - 2) > 0) break;// then goto 50;
//                               if (Y == 0)
//                               {
//                                   if ((DR1 - DR2) > 0) { A = DR1; }
//                                   else
//                                   {
//                                       A = DR2;
//                                       SV17(); break;
//                                   }
//                               };
//                               DR1 = 2 * AW2 + DG;
//                               if ((DR1 - ag) > 0) break;
//                               if ((DR1 - DR2) > 0) A = DR1; else A = DR2;
//                               SV17();


//                           }//50
//                       }//end else

//                       if (IP == 3) break;
//                       if ((DR1 - ag) > 0) break;
//                   }//80
//                   if (IP == 1) break;
//               }//100
//               if (IP == 1) break;
//           }//200

//          //Заполнение полей в соответствии с расчетами выше.
//           Za = KA;
//           Zb = KB;
//           Zg = KG;
//           Zf = KF;
//           Aw1 = AW1;
//           Aw2 = AW2;
//           N = NW;
//           M1 = m1;
//           M2 = m2;
//           U = Uvr;
//           DU = DUR;



//       }// end konstructor


//   }//end class definition
    
//    class Raschet
//    {
//    }
//}
