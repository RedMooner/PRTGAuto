   M S B A M L   `   `   `  ����C  ?PRTGAuto, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:PRTGAuto.ViewsPRTGAuto.Views  5      9!clr-namespace:PRTGAuto.ViewModelsPRTGAuto.ViewModels  5	          PRTGAuto.Views.MainView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:PRTGAuto.Views     5      _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      *vm!clr-namespace:PRTGAuto.ViewModels  5	        N� Title$  MainView��5       � DataContext 5      '   !PRTGAuto.ViewModels.MainViewModel  5   
   6   .��5      � 
�������.+�5   
   #  MaterialDesignThemes.Wpf.Card     UniformCornerRadius$ 15Q�6   $��-2,0,2,0q�68   .��5      �  � RowDefinitions 5      �� 5      $	��32* �6$   �� 5      $
��383* �6$    � ColumnDefinitions 5      �� 5      $��2* �6'   �� 5      $	��15* �6'    � 	Resources 5      '  !MaterialDesignThemes.Wpf.PackIcon �� 
TargetType �� Property �� Value��%�   '        �� 5      "  6   .�5      ��  � ��$ 30��62   6?   �� 5       � ��$ 30��63   6@   5      5      .+�5       � $��0��6   $��2��6)   .+�5!      �� $
��0 15q�6   .@�5"      �� $��Center=�6'   $
��0 10q�6D   .��5#      �� $
��Left=�6H   
���   �$��50��5$   ^   $	��100��6j   ��5#   %   �� *ShowObjectView+��5$      Z� ��65   Z� .��5%   "   �� $��-5q�6-   .+�5&   &     $��Center=�6>   	   Kind$	 Server=�6[   $��63��6i   �� 5'   &   $��Устройства��60   $��Center=�6B   $��76��6_   5(   #   5)      5*      �� 5+      $��Center=�6'   $
��0 10q�6D   .��5,      �� $
��Left=�6J   
���   �$��50��5-   ^   $	��100��6j   ��5,   %   �� *ShowSensorsView+��5-      Z� ��65   Z� .��5.   "   �� $��-5q�6-   .+�5/   &     $��Center=�6>   $	 ProximitySensor=�6[   $��63��6r   �� 50   &   $��Сенсоры��60   $��Center=�6?   $��62��6\   51   #   52      53      �� 55      $��Center=�6'   $
��0 10q�6D   .��56      �� $
��Left=�6&   
���   �$��50��57   ^   $	��100��6j   ��6   Z� ��65   Z� .��58   "   �� $��-5q�6-   .+�59   &     $��Center=�6>   $	 	ExitToApp=�6[   $��63��6l   �� 5:   &   $��
Выйти��60   $��43��6=   5;   #   5<      5=      5>      5?      � 5@      $��1��6   ���#F6F5F8$��2��6<    5A      �� 5B      $	��29* �6(   �� 5C      $
��386* �6(   .+�5E      � $y�Hidden=�6   $��0��6/   .+�5F      �� $��20(�6    $��SemiBold"�6.   $��	888,0,0,0q�6D   $��Center=�6q   $
��Left=�6�   ��6W   �� *Title+6�   5G       � 5H      -   5        
  �Name$
 frame��5H        � NavigationUIVisibility$ Hidden=�6+   $��2��6K   5I      5J      5K      5L      5M      