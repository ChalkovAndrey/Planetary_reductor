   M S B A M L   `   `   `  ����K  GPlanetary_REDUCT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Planetary_REDUCTPlanetary_REDUCT  5      '    !Planetary_REDUCT.StartPageControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Planetary_REDUCT     5        � 	Resources  5       �� 
TargetType �� Triggers �� Binding �� ElementName �� Path �� Value �� Property �� Value��%M  '��       '�� �    �� 5   
   " ��6    5      �� 5       6   �� $ PlanetaryBut��$ IsMouseOver� True5      .a�5      ��  � ��$ Images/Planet.jpg��6/   5      �� 5       6   �� $ PlanetaryBut��$ IsMouseOver�	 False5      .a�5      ��  � ��$ Images/Planet.jpg��6/   5      �� 5       6   �� $ WavesBut��$ IsMouseOver� True5      .a�5      ��  � ��$ Images/Wave.jpg��6/   5      5      �� 5   
   " ��6   .�5      ��  � ��$ Microsoft Sans Serif)�6+   5      5      .��5!      � 	 � RowDefinitions	 5#   
   �� 5%      $��* �6   �� 5'      $
��1.6* �6   
 � ColumnDefinitions
 5*   
   �� 5+      $��0.25* �6   �� 5,      $��0.25* �6   �� 5-      $��0.25* �6   �� 5.      $��0.25* �6   .+�52   
   �� $��25(�6   $��0��6"   $��1��6/   $��Center=�6?   $��Center=�6[   $��2��6x    �� TextAlignment$ Center=�6�   $��Times New Roman)�6�   $
��Bold"�6�   #!Программа расчета�� 53      6   A?планетарных и волновых редукторов�� 54   
   6   64при габаритных ограничениях6=   �� 55   
   -   5        $��
ImReductor��55      $��1��6"   $��0��6/   $��Bottom=�6?   $
��Fill=�6Z   $��2��6i   56      �� 57   
   -   5        $��robot��57      $��0��6   $��0��6*   $��Bottom=�6:   $
��Fill=�6U   $��Center=�6d   $��images/robot.png��6�   58      �� 59   
   -   5        $��	shesterni��59      $��0��6!   $��3��6.   $��Bottom=�6>   $
��Fill=�6Y   $��Center=�6h   $��images/shesterni.png��6�   5:      � 5=   
   $��1��6   $��2��6   $��2��6,   	 5>      �� 5?      $��* �6    �� 5@      $��20 �6    �� 5A      $��30 �6    �� 5B      $��20 �6    �� 5C      $��30 �6    �� 5D      $
��1.5* �6    
 5F      �� 5G      $��* �6#   �� 5H      $
��auto �6$   �� 5I      $��* �6$   .+�5K      �� $0��*Выберите тип редуктора��6   $��14(�66   $��0��6D   $��0��6R   $��3��6b   $��Bottom=�6v   $��Center=�6�   $
��Bold"�6�   $��Microsoft Sans Serif)�6�   6�   �� 5L      -   5         ��Name$ PlanetaryBut��5L      $��2��6+   $
��auto��68   $��30��6E   $��1��6Q   $-��'Планетарный редуктор��6a   $��Center=�6�   6�   �� 5M      -   5        $ WavesBut��5M      $��4��6'   $
��auto��64   $��30��6A   $��1��6M   $'��!Волновой редуктор��6]   6�   5N      5Q      5R      