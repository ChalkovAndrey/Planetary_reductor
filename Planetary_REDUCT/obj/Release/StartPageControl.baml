   M S B A M L   `   `   `  ���� K  GPlanetary_REDUCT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Planetary_REDUCTPlanetary_REDUCT  '    !Planetary_REDUCT.StartPageControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:Planetary_REDUCT       � 	Resources   �� 
TargetType �� Triggers �� Binding �� ElementName �� Path �� Value �� Property �� Value��%~  '��       '�� 5    �� " �� ��  �� $ PlanetaryBut��$ IsMouseOver� True.a���  � ��$ Images/Planet.jpg����  �� $ PlanetaryBut��$ IsMouseOver�	 False.a���  � ��$ Images/Planet.jpg����  �� $ WavesBut��$ IsMouseOver� True.a���  � ��$ Images/Wave.jpg���� " ��.���  � ��$ Microsoft Sans Serif)�.��� 	 � RowDefinitions	 �� $��* ��� $
��1.6* �
 � ColumnDefinitions
 �� $��0.25* ��� $��0.25* ��� $��0.25* ��� $��0.25* �.+��� $��25(�$��0��$��1��$��Center=�$��Center=�$��2�� �� TextAlignment$ Center=�$��Times New Roman)�$
��Bold"�#!Программа расчета�� A?планетарных и волновых редукторов�� 64при габаритных ограничениях�� -   $��
ImReductor��$��1��$��0��$��Bottom=�$
��Fill=�$��2���� -   $��robot��$��0��$��0��$��Bottom=�$
��Fill=�$��Center=�$��images/robot.png���� -   $��	shesterni��$��0��$��3��$��Bottom=�$
��Fill=�$��Center=�$��images/shesterni.png��� $��1��$��2��$��2��	 �� $��* ��� $��20 ��� $��30 ��� $��20 ��� $��30 ��� $
��1.5* �
 �� $��* ��� $
��auto ��� $��* �.+��� $0��*Выберите тип редуктора��$��14(�$��0��$��0��$��3��$��Bottom=�$��Center=�$
��Bold"�$��Microsoft Sans Serif)��� -    ��Name$ PlanetaryBut��$��2��$
��auto��$��30��$��1��$-��'Планетарный редуктор��$��Center=��� -   $ WavesBut��$��4��$
��auto��$��30��$��1��$'��!Волновой редуктор��