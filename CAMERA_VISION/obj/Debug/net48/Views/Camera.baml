   M S B A M L   `   `   `  ����H  DCAMERA_VISION, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:CAMERA_VISION.ViewsCAMERA_VISION.Views  5           CAMERA_VISION.Views.Camera   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:CAMERA_VISION.Views     5      Y UPrism.Unity.Wpf, Version=7.2.0.1422, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59S OPrism.Wpf, Version=7.2.0.1422, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5	      -   5        !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel$
  True��5
       &  "MahApps.Brushes.Control.Background#���   5       � 	Resources 5      )  #MahApps.Metro.Controls.MetroTabItem �� 
TargetType �� Property �� Value �� 
TargetType �Name J� SnapsToDevicePixels ��Name	 ��Name  TabItemSelectorBrush
 �� Triggers P� Property P� Value $  TabItemBackgroundIsSelectedBrush �� 
TargetName P� 
SourceName % !TabItemBackgroundIsMouseOverBrush��%�  '        7  7  7  �� 5   
   "  6   .�5      ��  � �� 6'   �� *DataContext.Name+�� 5       � ��$ 14(�6)   �� 5       � �� 5      �� 5      "  6&   .R�5      � $ 	PART_Grid��6   #��z��5      $
 True��5      $��0q�5      .+�5      �� $ PART_HeaderContent��6/   #��z��5   +   $��Header��5   +   $��Center=�5   +   # z 5   +   �� 5       $	 PART_Selector��6(   $��Bottom=�5!   $   $��1��5"   $   $y�	Collapsed=�5#   $   8��  5$   $   5%      
 5&      P� 5'       �@��
IsSelected$
 True��6<    �5(   "   �� 5)   &   8   � ��6C   �� 5+   &   $ PART_Selector��6-    � y�$ Visible=�6^   5-      P� 5.      $ 	PART_Grid��6&    �@J�IsMouseOver$
 True��6T    �5/   "   �� 50   &   8   � ��6C   �� 51   &    �@�Cursor$
 Hand��6?   53      55      57      58      59      .��5;      � .+�5<   
   ,  &MahApps.Metro.Controls.MetroTabControl  !  Prism.Regions.RegionManager   
RegionName$ CameraTabRegion��6#   5=      5?      