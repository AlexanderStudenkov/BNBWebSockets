   M S B A M L   `   `   `  ����H  DBNBWebSockets, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:BNBWebSocketsBNBWebSockets  5   	       BNBWebSockets.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:BNBWebSockets     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��137* �6   �� 5      $	��23* �6   .+�5   
   �� -   5         ��Name$ BidsList��5      $��10,31,469,0q�6&   $��Stretch=�6;   $
��Auto��6Y   $
��Auto��6f    �� View 5      �� 5      .P�5      ��  �� DisplayMemberBinding 6%   ��  �� Path$ Price�.��6Q   Price6X   �� 5       6%   �� $ Quantity�.��6T   
Quantity6^   5      .@�5      �� $	��115��6   $��	TextBlock��6%   $
��Wrap=�66   $	��179��6J   5      �� 5   
   $
��Left=�6   $��	10,10,0,0q�6/   $��Bids:��6B   $
��Wrap=�6O   $	��Top=�6c   �� 5   
   -   5        $ AsksList��5      $��221,31,258,0q�6&   $��Stretch=�6<    5      �� 5      .P�5      �� $��Price��6L    6%   �� *Price+6L   �� 5      $��Quantity��6O    6%   �� *
Quantity+6O   5      .@�5       �� $	��115��6   $��	TextBlock��6%   $
��Wrap=�66   $	��179��6J   5"      �� 5#   
   $
��Left=�6   $��
221,10,0,0q�6/   $��Asks:��6C   $
��Wrap=�6P   $	��Top=�6d   5%      5&      