   M S B A M L   `   `   `  ���� O  Kczu_password_manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:czu_password_managerczu_password_manager  %    czu_password_manager.AfterLogin   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:czu_password_manager       � Title$  
AfterLogin��$	��400��$	��700�� � 
ResizeMode$ 	CanResize=�$	��600��$	��400��$	��400��$	��800�� � 	Resources    NoMouseOverButtonTemplate �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%�   &	        �� " ��.R��� #��z��#��z��#��z��._��� #��z��#��z�� P�  � {� .  . ���  � �� �#607080��  � �� �#aaaaaa���� 	 �� Color$	 #233040��.��� 
 � RowDefinitions
 �� $��* ��� $
��Auto � � ColumnDefinitions �� $	��280 ��� $��* ��� $	��333 �.+��� $��0��$��0�����#7F8A94$��Stretch=�.+��� $
��Auto=�.���� -   $��credentialsListBox�����#7F8A94
����������#304050$	��300������ *Credentials+���� .R��� .+��� $
��Bold"����� *name+�� -   $��addNew��$��1��$	��330��$	��221��$y�	Collapsed=�$
��Left=�$��Center=�$��2��$��44,0,0,0q�.+��� ���#405060���#304050$��1q� �� CornerRadius$ 10��$	��221��._��� $	��300��$
��Auto��$��Center=�$��Center=�.+��� $
��Name�����#D0E0F0$��Center=��� -    ��Name$ 	nameInput��$	��200��$��0,0,0,5q�$��Center=����#90A0B0.������ " ��.R��� #��z�����#304050$��1q�$ 5��._���  ��Name$ PART_ContentHost��$��5q�#��z��#��z��.���� $��Username�����#D0E0F0$��Center=�� .+��� -   $ usernameInput��$	��200��$��0,5,0,5q�$��Center=����#90A0B0.������ " ��.R��� #��z�����#304050$��1q�$ 5��._��� $ PART_ContentHost��$��5q�#��z��#��z��.���� -    ��Name$ usrClip��$��27��$
��📋��$��26��$��Center=�$��Center=�$��Right=�$��Center=�$��0,0,15,0q�$��2q�
���   ����� " ��.R��� #��z��#��z��#��z��$ 2��._��� $��Center=�$��Center=��� $��Password�����#D0E0F0$��Center=�� .+�G� -    G�Name$ passwordInput��$	��200��$��0,5,0,5q�$��Center=����#90A0B0���� " G�.R��� #��z�����#304050$��1q�$ 5��._��� $ PART_ContentHost��$��5q�#��z��#��z���� -   $��password�����#90A0B0���#203040$	��200��$��0,5,0,5q�$
��Left=�$��Center=�$y�	Collapsed=�.������ " ��.R��� #��z��#��z��$��1q�$ 5��._��� $ PART_ContentHost��$��5q�#��z��#��z��.���� -   $��	masterBtn�����#90A0B0$
��👁��$��27��$��26��$��18(�$��Center=�$��Center=�$
��Left=�$��Center=����#D0E0F0$��0q�$��	220,0,0,0q�#��[  �� -	   $ 
passwdClip��$��27��$
��📋��$��26��$��Center=�$��Center=�$��Right=�$��Center=�$��0,0,15,0q�$��2q�
���   ����� " ��.R��� #��z��#��z��#��z��$ 2��._��� $��Center=�$��Center=���  �� Orientation$ 
Horizontal=�$��5q�$��Center=�$��Center=�.+��� -
   $��
saveButton��$
��Save�����#D0E0F0���#42526E$��Center=�$��69��$��20,5,5,5q����� " ��.R��� #��z��#��z��#��z��$ 2��._��� $��Center=�$��Center=��� -   $��
editButton��$
��Edit�����#D0E0F0���#42526E$��Center=�$��69��$��20,5,5,5q����� " ��.R��� #��z��#��z��#��z��$ 2��._��� $��Center=�$��Center=��� -   $��deleteButton��$��Delete�����#D0E0F0���#42526E$��Center=�$��69��$��20,5,5,5q����� " ��.R��� #��z��#��z��#��z��$ 2��._��� $��Center=�$��Center=��� $��1�����#6B7B8C$��0q�$��1q�._��� -   $��+��$��20(����#D0E0F0���#42526E$��40��$��Center=�$��Stretch=�$��10,10q����� " ��.R��� #��z��#��z��#��z��$ 2��._��� $��Center=�$��Center=�