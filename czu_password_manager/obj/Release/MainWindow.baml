   M S B A M L   `   `   `  ���� O  Kczu_password_manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:czu_password_managerczu_password_manager  %    czu_password_manager.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:czu_password_manager       � Title$  
MainWindow��$	��500��$	��800�� � 
ResizeMode$ 	CanResize=�$	��600��$	��400��$	��600��$	��900�� � 	Resources    NoMouseOverButtonTemplate �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%�   &	        �� " ��.R��� #��z��#��z��#��z��._��� #��z��#��z�� P�  � {� .  . ���  � �� �#607080��  � �� �#aaaaaa���� 	 �� Color$	 #233040��.��]� $��0,0,0,0q�.+�o� $	��Top=����#303030$��18��.@�m� $��_Info��$��18��
�������.@�m� $��Author��
���   �.@�m� $��Richard Svoboda��
���   ��� m� $
��Exit��
���   ��� $��Center=�$��Center=�$��	0,100,0,0q�.+��� -   $��label1��$��Center=�� .+�G� -   $��masterButton��$��1q����#607080���#203040$	��323��$��5q�$��74��$��16(�$
��Left=�$��Center=����� " G�.R��� #��z��#��z��$��1q�
 �� CornerRadius$
 2��._���  ��Name$ PART_ContentHost��$��5q�#��z��#��z���� -   $��masterPassword�����#607080���#203040$	��323��$��5q�$��74��$��16(�$
��Left=�$��Center=�$y�	Collapsed=�.������ " ��.R��� #��z��#��z��$��1q�$
 2��._��� $ PART_ContentHost��$��5q�#��z��#��z��.���� -   $��	masterBtn�����#607080$
��👁��$��27��$��26��$��18(�$��Center=�$��Center=�$
��Left=�$��Center=����#D0E0F0$��0q�$��	286,0,0,0q�#��[  �� -   $��errorLbl��$��12��$��	10,5,10,0q����#506070
������$y�Hidden=����� " ��.R��� #��z��#��z��#��z��$
 5��._��� $��Center=�$��5,0,0,0q�$��Center=���  �� Orientation$ 
Horizontal=�$��Center=�$��Center=�$��0,0,0,0q�.+��� -   $��logBtn��$��Unlock 🔒�����#607080$��43��$	��151��$��	0,30,10,0q�$��10(�$��DemiBold"����#D0E0F0���� " ��.R��� #��z��#��z��#��z��$
 5��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$ 0q���  �@J�Opacity$	 0.8Q�P�  �@J�IsMouseOver .  . ���  � ��$ 1q���  �@J�Opacity$ 1Q���  � �� �#b0c0d0�� -   $��SetGet�����#607080$��43��$��Create | Set Password��$	��150��$��	10,30,0,0q�$��10(�$��DemiBold"����#D0E0F0���� " �� P�  �@J�IsMouseOver . . ���  � ��$ 0q���  �@J�Opacity$	 0.8Q�P�  �@J�IsMouseOver .  . ���  � ��$ 1q���  �@J�Opacity$ 1Q���  � �� �#b0c0d0.R��� #��z��#��z��#��z��$
 5��._��� $��Center=�$��Center=�