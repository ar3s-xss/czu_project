   M S B A M L   `   `   `  ���� O  Kczu_password_manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:czu_password_managerczu_password_manager  (    "czu_password_manager.UnleashMaster   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:czu_password_manager       � Title$  UnleashMaster��$	��400��$	��600��$	��450��$	��350��$	��500��$	��700�� � 	Resources    NoMouseOverButtonTemplate �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%�   &	        �� " ��.R��� #��z��#��z��#��z��._��� #��z��#��z�� P�  � {� .  . ���  � �� �#607080��  � �� �#D0E0F0����  �� Color$ #233040��.���� $��Center=�$��Center=�$��	0,100,0,0q�.+��� -   $��
oldPassLbl��$��0,0,0,0q�$��0q�$��Old master password�����#b0c0d0$y�	Collapsed=�� -   $��oldPassGrid��$y�	Collapsed=�.+�G� -   $��oldPassword�����#607080���#203040$	��260��$��30��$��12(�$
��Left=�$��Center=�$y�Visible=����� " G�.R��� #��z��#��z��$��1q�	 �� CornerRadius$	 5��._��� 
 ��Name$
 PART_ContentHost��$��5q�#��z��#��z���� -   $��oldPasswordTextBox�����#607080���#203040$	��260��$��30��$��12(�$
��Left=�$��Center=�$y�	Collapsed=�.������ " ��.R��� #��z��#��z��$��1q�$	 5��._��� $
 PART_ContentHost��$��5q�#��z��#��z��.���� -   $��oldBtn��$
��👁��$��14(�$��27��$��26��$��Center=�$��Center=�$
��Left=�$��Center=����#607080���#D0E0F0$��0q�$��	231,0,0,0q�#��[  �� $��0,15,0,0q�$��0q�$��New master password�����#b0c0d0� .+�G� -   $��newPassword�����#607080���#203040$	��260��$��30��$��12(�$
��Left=�$��Center=�$y�Visible=����� " G�.R��� #��z��#��z��$��1q�$	 5��._��� $
 PART_ContentHost��$��5q�#��z��#��z���� -   $��newPasswordTextBox�����#607080���#203040$	��260��$��30��$��12(�$
��Left=�$��Center=�$y�	Collapsed=�.������ " ��.R��� #��z��#��z��$��1q�$	 5��._��� $
 PART_ContentHost��$��5q�#��z��#��z��.���� -   $��newBtn��$
��👁��$��14(�$��27��$��26��$��Center=�$��Center=�$
��Left=�$��Center=����#607080���#D0E0F0$��0q�$��	231,0,0,0q�#��[  �� $��0,15,0,0q�$��0q�$��Again new master password�����#b0c0d0� .+�G� -	   $��againNewPassword�����#607080���#203040$	��260��$��30��$��12(�$
��Left=�$��Center=�$y�Visible=����� " G�.R��� #��z��#��z��$��1q�$	 5��._��� $
 PART_ContentHost��$��5q�#��z��#��z���� -
   $��againNewPasswordTextBox�����#607080���#203040$	��260��$��30��$��12(�$
��Left=�$��Center=�$y�	Collapsed=�.������ " ��.R��� #��z��#��z��$��1q�$	 5��._��� $
 PART_ContentHost��$��5q�#��z��#��z��.���� -   $��aNewBtn��$
��👁��$��14(�$��27��$��26��$��Center=�$��Center=�$
��Left=�$��Center=����#607080���#D0E0F0$��0q�$��	231,0,0,0q�#��[  �� -   $��errorLbl��$��5,5,0,5q����#506070
���������� " ��.R��� #��z��#��z��#��z��$	 5��._��� $��Center=�$��5,0,0,0q�$��Center=���  �� Orientation$ 
Horizontal=�$��Center=�$��Center=�.+��� -   $	��100��$��20��$��0,0,20,0q�$��Confirm�����#607080���#D0E0F0���� " ��.R��� #��z��#��z��#��z��$	 5��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$ 0q���  �@J�Opacity$	 0.8Q�P�  �@J�IsMouseOver .  . ���  � ��$ 1q���  �@J�Opacity$ 1Q���  � �� �#b0c0d0�� -   $��Go back��$	��100��$��20��$��10,0,0,0q����#607080���#D0E0F0���� " ��.R��� #��z��#��z��#��z��$	 5��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��$ 0q���  �@J�Opacity$	 0.8Q�P�  �@J�IsMouseOver .  . ���  � ��$ 1q���  �@J�Opacity$ 1Q���  � �� �#b0c0d0