   M S B A M L   `   `   `  ����I  EAnimalRegister, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AnimalRegister.Pig.WindsAnimalRegister.Pig.Winds  5   	   /    )AnimalRegister.Pig.Winds.VeterinaryWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:AnimalRegister.Pig.Winds     5   	     � Title$  Veterinární záznamy��5   	   $	��720��6%   $	��400��62    � WindowStartupLocation$ CenterScreen=�6>    � Icon$ ..\Img\pigIcon.ico��5	   	      DefaultWindow#���   6#   .��5
      �  	 Light#���  6   $��10q�6+    � RowDefinitions 5   
   �� 5      $��10 �6   �� 5      $��* �6   �� 5      $��25 �6   �� 5      $��35 �6   �� 5      $��25 �6   �� 5      $��35 �6   �� 5      $��25 �6   �� 5      $��35 �6   �� 5      $��25 �6   �� 5      $��35 �6   �� 5      $��25 �6   �� 5      $��35 �6   �� 5      $��25 �6   �� 5       $	��100 �6   �� 5"      $��50 �6   �� 5#      $��10 �6    � ColumnDefinitions 5&   
   �� 5(      $��15 �6   �� 5)      $��* �6   �� 5*      $��15 �6   .+�5.   
   �� $��14��6    �� Orientation$ 
Horizontal=�6#   $��Center=�6<   $��1��5/      $��Bottom=�6%   .+�50      �� -   5        $��
saveButton��50      $��10,5,5,5q�6'     
SaveButton#���  51      $��Ulož��66    �� 	IsDefault . 52   *   �� 55      -   5        $��removeButton��55      $��10,5,5,5q�6)   #���  56      $��Odeber��66   57      59      �� 5;   
   $��..\Img\pigAdd.png��6   $��1��6+   $��0��68   $��45,10,10,10q�5<      $	��150��6%   $	��140��64   $��2��6D   �� 5@   
   $��2��6   $��1��6!   $��	10,0,10,0q�61   $%��Výběr veteriny pro zobrazení��6D     NadpisTextBoxu#���  5A      �� 5B   
   -   5        $��veterinarySelectComboBox��5D   %   $��3��5B      $��1��6    $��	10,4,10,4q�60   $��13(�6C   $
��Bold"�5D       �� SelectedIndex$ -1��6E   
������� � ToolTip$f `Vyber pokud chceš zobrazit / upravit existující záznam. Pro nový záznam ponech nevybráno!��5F      ��5C      �� 	 �� Mode$	 OneWay=�5H      �� 5K   
   $��4��6   $��1��6!   $��Datum záznamu *��62   #���  5L      �� 5M   
   -   5        $��dateTextBox��5M   /   $��5��6   $��1��6     BasicInputTextBox#���  6B   $$ Datum veterinárního záznamu��5P      
 � DataContext
 5N      ��  �� ElementName$ veterinarySelectComboBox�� �� Path$ SelectedItem���5O      �� *Date+$	 OneWay=� �� StringFormat$ 
dd.MM.yyyy��5P      �� 5R   
   $��6��6   $��1��6!   $��Náklady v CZK *��62   #���  5S      �� 5T   
   -   5        $��priceTextBox��5T   /   $��7��6   $��1��6   #���  6C   $8 2Vyplacená částka veterináři za léky a úkony��5W      
 5U      �� $ veterinarySelectComboBox��$ SelectedItem���5V      �� *Price+$	 OneTime=�5W      �� 5Y   
   $��8��6   $��1��6!   $��Účel návštěvy *��62   #���  5Z      �� 5[   
   -   5        $��purposeTextBox��5[   /   $��9��6   $��1��6   #���  6E   $U OÚčel návštěvy - důvod proč byl veterinář zavolán * např. Očkování��5^      
 5\      �� $ veterinarySelectComboBox��$ SelectedItem���5]      �� *	Purpose+$	 OneTime=�5^      �� 5`   
   $��10��6   $��1��6"   $��Podané léky��63   #���  5a      �� 5b   
   -   5        $��drugsTextBox��5b   0   $��11��6   $��1��6    #���  6D   $  Podané léčivo zvířeti��5e      
 5c      �� $ veterinarySelectComboBox��$ SelectedItem���5d      �� *Drugs+$	 OneTime=�5e      �� 5g   
   $��12��6   $��1��6"   $��Prováděné úkony��63   #���  5h      �� 5i   
   -   5        $��tasksTextBox��5i   0   $��13��6   $��1��6    #���  6D   $_ YDetailní popis k prováděným úkonům a další informace, které chce chovatel zapsat��5l       �� AcceptsReturn . 
 5j      �� $ veterinarySelectComboBox��$ SelectedItem���5k      �� *Tasks+$	 OneTime=�5m      5n      5o      