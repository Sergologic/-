   M S B A M L   `   `   `  ����E  ASergeiTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:SergeiTest.ViewModelsSergeiTest.ViewModels  5   	       SergeiTest.TestSergey   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :
viewModels#clr-namespace:SergeiTest.ViewModels     5   	     � Title$)  #made especially for SOFTWARE CENTER��5	   	   $	��500��65   $	��850��6B    � WindowStartupLocation$ CenterScreen=�6N    � DataContext 5      )   #SergeiTest.ViewModels.MainViewModel  5   
   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $	��150 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��0��6I   $��0,0,0,10q�6V   .+�5      �� $��Выбрать XML��6   $	��120��6+   $��35��67   $��5q�6C   ���#2D2D30���#00BFFF��5      �� *LoadXmlCommand+�� 5      $+��%Сохранить выбранное��6   $	��150��63   $��35��6?   $��5q�6K   ���#2D2D30���#00BFFF��5      �� *ExportSelectedCommand+�� 5      $&�� Снять все галочки��6   $	��140��61   $��35��6=   $��5q�6I   ���#2D2D30���#00BFFF��5       �� *UncheckAllCommand+�� 5"      $��Справка��6   $	��100��6'   $��35��63   $��5q�6?   ���#2D2D30���#00BFFF��5#      �� *HelpCommand+5$      R� 5'   
   -   5        $��1��5'      ���#2A2A2A
�������$��0,0,0,10q�5+      ��5(      �� *	TreeItems+��5-      �� 5.       �� ItemsSource 6+   �� *
Children+.R�5/      �� $ 
Horizontal=�6!   .+�50      �� $��0,0,5,0q�6C    d� 	IsChecked 6#   �� *	IsChecked+6C   �� 51      ��6$   �� *DisplayName+52      53      55      �� 59   
   $��2��6   ���#2A2A2A���#00BFFF$��Consolas)�5:   
   $��13(�6     �� TextWrapping$
 Wrap=�6.   " �� VerticalScrollBarVisibility$
 Auto=�6B   	 �� 
IsReadOnly	 . $��1q�5;      ���#444$��8q�6C   ��5<   
   �� *SelectedXmlText+
 �� Mode$
 OneWay=�5?      5A      