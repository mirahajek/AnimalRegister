   M S B A M L   `   `   `  ����I  EAnimalRegister, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AnimalRegister.Pig.WindsAnimalRegister.Pig.Winds  5   	   /    )AnimalRegister.Pig.Winds.AddFinanceWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:AnimalRegister.Pig.Winds     5   	   -   5          � Title$%  Přidání / úprava transakcí��5   	   $	��780��6,   $	��420��69    � WindowStartupLocation$ CenterScreen=�6E    � Icon$ ..\Img\pigIcon.ico��5	   	      DefaultWindow#���   6#   .��5
      �  	 Light#���  6   $��10q�6+    � RowDefinitions 5   
   �� 5      $��5 �6   �� 5      $��* �6   �� 5      $��25 �6   �� 5      $��35 �6   �� 5      $��25 �6   �� 5      $��35 �6   �� 5      $��25 �6   �� 5      $��35 �6   �� 5      $��25 �6   �� 5      $��80 �6   �� 5      $��25 �6   �� 5      $��35 �6   �� 5      $��25 �6   �� 5       $��35 �6   �� 5"      $��25 �6   �� 5#      $��35 �6   �� 5%      $��50 �6   �� 5&      $��5 �6    � ColumnDefinitions 5)   
   �� 5+      $��10 �6   �� 5,      $��* �6   �� 5-      $��10 �6   .+�51   
   �� $��16��6    �� Orientation$ 
Horizontal=�6#   $��Center=�6<   $��1��52      $��Bottom=�6%   .+�53      �� -   5        $��
saveButton��53      $��10,5,5,5q�6'     
SaveButton#���  54      $��Ulož��66    �� 	IsDefault . 55   *   �� 58      -   5        $��removeButton��58      $��10,5,5,5q�6)   #���  59      $��Odeber��66   5:      5<      �� 5>   
   $��..\Img\money.png��6   $��1��6*   $��0��67   $��45,10,10,10q�5?      $	��150��6%   $	��140��64   $��2��6D   �� 5D   
   $��2��6   $��1��6!   $��Datum *��62     NadpisTextBoxu#���  5E      �� 5F   
   -   5        $��dateTextBox��5F   /   $��3��6   $��1��6     BasicInputTextBox#���  6B    � ToolTip$" Datum ve formátu 12.10.2020��5H      ��5G      �� *Date+ �� Mode$ OneWay=�	 �� StringFormat$	 
dd.MM.yyyy��5H      �� 5J   
   $��4��6   $��1��6!   $��Název *��62   #���  5K      �� 5L   
   -   5        $��nameTextBox��5L   /   $��5��6   $��1��6   #���  6B   $2 ,Krátký název charakterizující transakci��5N      ��5M      �� *Name+$ OneTime=�5N      �� 5P   
   $��6��6   $��1��6!   $��
Částka *��62   #���  5Q      �� 5R   
   -   5        $��priceTextBox��5R   /   $��7��6   $��1��6   #���  6C   $' !Hodnota transakce - celé číslo��5T      ��5S      �� *Price+$ OneTime=�5T      �� 5V   
   $��8��6   $��1��6!   $��Popis��62   #���  5W      �� 5X   
   -   5        $��descriptionTextBox��5X   /   $��9��6   $��1��6   #���  6I   
 �� AcceptsReturn
 .  �� TextWrapping$
 Wrap=�5Y   R   $&  Detailnější popis k transakci��5Z      ��5Y      �� *Description+$ OneTime=�5Z      �� 5]   
   $��10��6   $��1��6"   $��	10,0,10,0q�62   $��Příjmy / výdaje *��6E   #���  5^      �� 5_   
   -   5        $��typeComboBox��5`   %   $��11��5_      $��1��6!   $��	10,4,10,4q�61   $��13(�6D   $
��Bold"�5`       �� SelectedIndex$ -1��69   
�������$S MTyp transakce, zda se jedná o příjem nebo výdaj. Vyber jednu z možností��5a      .@�5c      �� $��Příjem��6   �� 5d      $��Výdaj��6   5e      �� 5g   
   $��12��6   $��1��6"   $��	10,0,10,0q�62   $��Kategorie *��6E   #���  5h      �� 5i   
   -	   5        $��categoryComboBox��5k   %   $��13��5i      $��1��6!   $��	10,4,10,4q�61   $��13(�6D   $
��Bold"�5k      $ -1��6=   
�������$l fKategorie, do které příjem/ výdaj spadá. Dále dle této kategorie je možné získat statistiku.��5m      ��5j      �� $ OneWay=�5o      �� 5q   
   -
   5        $��animalTitleTextBlock��5r   =   $��14��5q      $��1��6"   $��	10,0,10,0q�62   $��Zvíře��6E   #���  5r      6=   �� 5s   
   -   5        $��animalComboBox��5u   %   $��15��5s      $��1��6!   $��	10,4,10,4q�61   $��13(�6D   $
��Bold"�5u      $ -1��6;   
�������$n hVztažné zvíře. Vyplňuje se pouze v případě, že chcete transakci evidovat k nějakému zvířeti��5w      ��5t      �� $ OneWay=�5y      5z      5{      