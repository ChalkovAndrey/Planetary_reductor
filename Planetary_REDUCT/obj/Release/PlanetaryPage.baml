   M S B A M L   `   `   `  ���� K  GPlanetary_REDUCT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Planetary_REDUCTPlanetary_REDUCT  $    Planetary_REDUCT.PlanetaryPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:Planetary_REDUCT       � 	Resources   �� 
TargetType �� Property �� Value��%�   '��       '�� 5     �� " ��.���  � ��$ Microsoft Sans Serif)��� " ��.���  � ��$ Microsoft Sans Serif)���  � ��$
 Bold"�.��� .+�� -    �Name$ 	InputGrid��$��-10,0,9.8,0q� � RowDefinitions �� $
��0.2* ��� �� �� �� �� �� �� �� �� �� �� �� $
��0.4* � � ColumnDefinitions �� $��50 ��� $	��325 ��� $
��Auto ��� $��20 ��� $
��Auto ��� $��50 ��� $	��240 ��� $��50 �.+��� $��1��$��1��$��3��$��Center=�.��QOПовышенная износостойкость и выносливость�� -    ��Name$ 
ltr_choice��$��1��$��2�� d� IsThreeState .  	 d� 	IsChecked$	 FalseY�$��Center=�$��Center=��� $��2��$��1��$M��GЧисло зубьев солнечного колеса - Za Min, Max��$��Center=��� $��3��$��1��$M��GЧисло зубьев первого сателлита - Zg Min, Max��$��Center=��� $��4��$��1��$M��GЧисло зубьев второго сателлита - Zf Min, Max��$��Center=��� $��5��$��1��$2��,Число сателлитов - N Min, Max��$��Center=��� $��6��$��1��$=��7Модуль первой ступени a-g - M1, мм��$��Center=��� $��7��$��1��$=��7Модуль второй ступени f-b - M2, мм��$��Center=��� $��8��$��1��$5��/Передаточное отношение - U��$��Center=��� $��9��$��1��$9��3Допустимая погрешность - DU, %��$��Center=��� $��10��$��1��$2��,Габаритный размер - AG, мм��$��Center=��� $��11��$��1��$W��QCуммарный коэффициент смещения ступени a-g - Yd��$��Center=��� $��3��$��6��$0��*Коэффициент ножки зуба��$
��Left=�$��Center=��� $��4��$��6��$��Осевой зазор��$
��Left=�$��Center=��� $��2��$��6��$;��5Параметры исходного контура:��$��Center=�$��Center=��� -   
 ��Name$
 Zamin��$��2��$��2��$��50��$��30������  �� Path$ ZaMin� �� Mode$ TwoWay=��� -   $
 Zamax��$��2��$��4��$��50��$��30������ $ ZaMax�$ TwoWay=��� -   $
 Zgmin��$��3��$��2��$��50��$��30������ $ ZgMin�$ TwoWay=��� -   $
 Zymax��$��3��$��4��$��50��$��30������ $ ZgMax�$ TwoWay=��� -   $
 Zfmin��$��4��$��2��$��50��$��30������ $ ZfMin�$ TwoWay=��� -   $
 Zfmax��$��4��$��4��$��50��$��30������ $ ZfMax�$ TwoWay=��� -	   $

 Nmin��$��5��$��2��$��50��$��30������ $
 NMin�$ TwoWay=��� -
   $

 Nmax��$��5��$��4��$��50��$��30������ $
 NMax�$ TwoWay=��� -   $
 m1��$��6��$��2��$��50��$��30������ $ M1�$ TwoWay=��� -   $
 m2��$��7��$��2��$��50��$��30������ $ M2�$ TwoWay=��� -   $
 u��$��8��$��2��$��50��$��30������ $ UT�$ TwoWay=��� -   $
 DU��$��9��$��2��$��50��$��30������ $ du�$ TwoWay=��� -   $
 ag��$��10��$��2��$��50��$��30������ $ ag�$ TwoWay=��� -   $
 Yd��$��11��$��2��$��50��$��30������ $ YD�$ TwoWay=��� -   $	
 haz��$��3��$��6��$��50��$��30��$��Right=����� $	 HAZ�$ TwoWay=��� -   $
 cz��$��4��$��6��$��50��$��30��$��Right=����� $ CZ�$ TwoWay=��� -   $��9��$��6��$
��Left=�$	��100��$��30��$��Очистить���� -   $��9��$��6��$��Right=�$	��100��$��30��$��Пример данных���� -   $��10��$��6��$��Center=�$	��100��$��30��$��Рассчитать���� -   $��11��$��6��$��Center=�$
��auto��$��30��.��75Вернуться к выбору редуктора� -   $��
OutputGrid��$y�	Collapsed=�.+�$   Planetary_REDUCT.OutScreenPage  