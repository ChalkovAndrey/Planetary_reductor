   M S B A M L   `   `   `  ����K  GPlanetary_REDUCT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Planetary_REDUCTPlanetary_REDUCT  5      $    Planetary_REDUCT.OutScreenPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Planetary_REDUCT     5        � 	Resources  5
   
   &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value:  4System.Windows.Controls.Primitives.DataGridRowHeader*  $System.Windows.Controls.DataGridCell��%�  '        '  �     '      �� 5      "  6   .�5      ��  �@��EdgeMode$ Aliased=�67   6I   �� 5       �@�UseLayoutRounding . 6A   �� 5       �@ GridLinesVisibility$
 None=�64   6C   5      �� 5   
   "  6   .�5      ��  � ��$ 0��6&   62   5      �� 5   
   "  6   .�5      ��  � ��$ 1,1,1,1q�60   6B   �� 5       � ��
 �   �6<   5      5      .��5!   
   �  � ColumnDefinitions 5"   
   �� 5#      $
��Auto �6    �� 5$       � RowDefinitions 5&   
   �� 5'      $��* �6   .+�5*        -   5          Name$ 
ResultGrid��5*      $��0��6+      MinRowHeight$ 20Q�6=      AutoGenerateColumns . 	   
IsReadOnly	 . 5/      �� 51      $��1��6   $��Bottom=�6.   .+�52      �� -   5        $��Сохранить��52      $	��100��6.   $��25��6:   $��25q�6F   6g   �� 53      -   5        $.��(Вернуться к рассчетам��53      $��25��69   $	��150��6E   $��0,0,0,40q�6Q   6y   55      58      59      