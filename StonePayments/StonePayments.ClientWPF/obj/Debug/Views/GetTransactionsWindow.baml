   M S B A M L   `   `   `  ����R  NStonePayments.ClientWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:StonePayments.ClientWPF.ViewsStonePayments.ClientWPF.Views  5   	   9    3StonePayments.ClientWPF.Views.GetTransactionsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:StonePayments.ClientWPF.Views     5   	     � Title$'  !Listagem de todas as transações��5   	   $	��450��61   $	��800��6>    � WindowStartupLocation$ CenterOwner=�6J   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��95* �6   �� 5      $
��699* �6   .+�5   
   �� $
��Auto=�6C   $��2��6s   $��0,0,0.334,-0.333q�6�   ��6   �� *AllTransactionModelList+ �� View 5      �� 5       �� AllowsColumnReorder . $
��Auto=�66   .P�5      �� $��Customer��6%    �� Width$	 140��69    �� DisplayMemberBinding 6E   ��  �� Path$ CustomerName��� 5      $��
CardNumber��6%   $	 100��69    6E   �� $
 Card��� 5      $��Amount��6%   $ 60��69    6E   �� $ Amount��� 5      $
��Type��6%   $ 60��69    6E   �� $
 Type��� 5      $��Number��6%   $ 50��69    6D   �� $ Number�5      5      5      5      