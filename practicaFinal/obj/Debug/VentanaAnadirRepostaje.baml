   M S B A M L   `   `   `  ����H  DpracticaFinal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:PracticaFinalPracticaFinal  5   	   *    $PracticaFinal.VentanaAnadirRepostaje   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:PracticaFinal     5   	   -   5          � Title$  VentanaAnadirRepostaje��5   	   $	��450��6(   $	��800��65   .��5	      �  � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��2* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��
Matrícula��6   $
��10 0q�6$   $��0��62   $��Bottom=�6?   �� 5   
   $��
 Vehículo��6   $
��10 0q�6$   $��1��62   $	��Top=�6?   �� 5   
   -   5        $��
CBvehiculo��5      $��0��6%   $��1��62   $��2��6B   $��5��6S   $��Center=�6g   $��10q�6�    �� DisplayMemberPath$ 	matricula��6�   �� 5   
   $��Fecha��6   $��10q�6    $��2��6,   $��Center=�69   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��DPfecha��5      $��10q�6$   $��2��60   $��1��6=   $��Center=�6M   �� 5    
   $
��Hora��6   $��10q�6   $��2��6+   $��2��68   $��Center=�6H   �� 5!   
   -   5        $��CBhora��5!      
�������$��Center=�64   $��2��6V   $��10q�6c   $��3��6o   �� 5"   
   $��:��6   $��10q�6   $��2��6(   $��4��65   $��Center=�6E   �� 5#   
   -   5        $��	CBminutos��5#      
�������$��2��67   $��Center=�6D   $��10q�6f   $��5��6r   �� 5$   
   $��Kilometraje��6   $��3��6&   $��10q�63   $��Center=�6?   �� 5%   
   -   5        $��TBkilometraje��5%      $��3��6'   $��1��64   $��5��6D   $��10q�6X   $��Center=�6d   6�   �� 5&   
   $��Litros��6   $��4��6!   $��10q�6.   $��Center=�6:   �� 5'   
   -   5        $��TBlitros��5'      $��4��6"   $��1��6/   $��5��6?   $��10q�6S   $��Center=�6_   6�   �� 5(   
   $��Coste��6   $��5��6    $��10q�6-   $��Center=�69   �� 5)   
   -   5        $��TBcoste��5)      $��5��6!   $��1��6.   $��5��6>   $��10q�6R   $��Center=�6^   6�   �� 5*   
   -	   5        $��botonAnadir��5*      $��AÑADIR��6$   $��6��65   $��0��6B   $��6��6R   $��10q�6f   $��Center=�6r   6�   5+      5,      