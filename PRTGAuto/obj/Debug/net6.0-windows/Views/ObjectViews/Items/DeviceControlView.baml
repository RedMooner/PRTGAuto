   M S B A M L   `   `   `  ����C  ?PRTGAuto, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:PRTGAuto.Views.ObjectViews.Items PRTGAuto.Views.ObjectViews.Items  5      8    2PRTGAuto.Views.ObjectViews.Items.DeviceControlView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @local.clr-namespace:PRTGAuto.Views.ObjectViews.Items     5      _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5
      � .+�5   
   �� $	��500��6   $��Center=�6!   $
��Left=�6<   $��88��6W   $��20,0,0,0q�6c   
�������  �� RadiusX$  10��6�    �� RadiusY$ 15��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5       �� AngleX$
 0.45Q�6$   �� 5      T� 5       T� X$ 0.589Q�6)   5      5      � 5   
   $	��Top=�6   $
��Left=�6(   $��88��6C   $��20,20,20,20q�6O   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Devices=�6&   $��70��65   $
��Left=�6@   $��Center=�6[   $��88��6v   $��10,0,0,0q�6�   � 5      .+�5      � $��90,0,-420,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��25(�6i   $��DemiBold"�6w   $��Center=�6�   ��6&   �� *	ShortName+ � ToolTip 6A   �� *CurrentDevice.Name+6�   5      �� 5      $��5q�6!   $ 
Horizontal=�6,   $��Bottom=�6E   .+�5        $ 
Identifier=�62   $��15��6D   $��10,0,5,0q�6P   $
��Left=�6b   $��Center=�6}   $��15��6�   �� 5      ��6%   ��  �� StringFormat$ Id: {0}��	 �� Path$	 CurrentDevice.Id�6c     5      
���  ��$ Error=�6C   $��15��6P   $��10,0,5,0q�6[   $
��Left=�6n   $��Center=�6�   $��15��6�   �� 5       ��6&   �� $/ )Датчиков с ошбиками: {0}��$	 CurrentDevice.DownSensors�6~   5!      �� 5"      $��Bottom=�6C   $��Right=�6^   $��5q�6z   $��22��6�   $��48��6�   $@ :Экспортировать данные сенсоров��6�   ��6   �� *ExportDataToExcel+��6�   0  *MaterialDesignThemes.Wpf.PackIconExtension  
   Kind$
 MicrosoftExcel=�6�   5#      5%      5&      5'      5(      