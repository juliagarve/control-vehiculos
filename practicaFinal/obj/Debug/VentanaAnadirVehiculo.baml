   M S B A M L   `   `   `  ����H  DpracticaFinal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:PracticaFinalPracticaFinal  5   	   )    #PracticaFinal.VentanaAnadirVehiculo   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:PracticaFinal     5   	   -   5          � Title$  VentanaAnadirVehiculo��5   	   $	��500��6'   $	��800��64    � 	Resources 5	         PracticaFinal.Vehiculo   origendatos��%   &	          5
   
   6   5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5    
   �� $��Tipo de vehículo��6   $��1��6+   �� 5!   
   $��icono coche.png��6   $��Uniform=�6)   $��1��6<   $��0��6I   �� 5"   
   -   5        $��CBtipoVehiculo��5"      $��Center=�6)   $
��10 0q�6K   $��1��6Y   $��1��6f   ��6v   ��  �� Source# [   �� Path$ GetTiposVehiculos��� 5#   
   $��Marca��6   $��Center=�6    $��2��6B   $��1��6O   �� 5$   
   $��icono marca.png��6   $��Uniform=�6)   $��3��6<   $��0��6I   �� 5%   
   -   5        $��CBmarca��5%      $��Center=�6"   $
��10 0q�6D   $��10q�6R   $��3��6_   $��1��6l   ��6|   �� # [  $ GetMarcasVehiculos��� 5&   
   $��Modelo��6   $��Center=�6!   $��4��6C   $��1��6P   �� 5'   
   $��icono modelo.jpg��6   $��Uniform=�6*   $��5��6=   $��0��6J   �� 5(   
   -   5        $��TBmodelo��5(      $��Center=�6"   $
��10 0q�6D   $��5��6R   $��1��6_   �� 5)   
   $��
Matrícula��6   $��Center=�6$   $��6��6F   $��1��6S   �� 5*   
   $��icono matricula.png��6   $��Uniform=�6-   $��7��6@   $��0��6M   �� 5+   
   -   5        $��TBmatricula��5+      $��Center=�6%   $
��10 0q�6G   $��7��6U   $��1��6b   �� 5,   
   $��Tipo de combustible��6   $��Center=�6.   $��8��6P   $��1��6]   �� 5-   
   $��icono combustible.png��6   $��Uniform=�6/   $��9��6B   $��0��6O   �� 5.   
   -   5        $��CBtipoCombustible��5.      $��Center=�6,   $
��10 0q�6N   $��10q�6\   $��9��6i   $��1��6v   ��6�   �� # [  $ GetTiposCombustibles��� 5/   
   $��Kilometraje��6   $��Center=�6&   $��10��6H   $��1��6V   �� 50   
   $��icono kilometraje.png��6   $��Uniform=�6/   $��11��6B   $��0��6P   �� 51   
   -   5        $��TBkilometraje��51      $��Center=�6'   $
��10 0q�6I   $��11��6W   $��1��6e   6u   �� 52   
   -   5        $��botonAnadir��52      $��AÑADIR��6$   $��100 10 100 10q�65   $��12��6L   $��0��6Z   $��2��6j   6~   53      54      