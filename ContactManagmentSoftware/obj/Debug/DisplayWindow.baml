   M S B A M L   `   `   `  ����S  OContactManagmentSoftware, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ContactManagmentSoftwareContactManagmentSoftware  5   	   ,    &ContactManagmentSoftware.DisplayWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:ContactManagmentSoftware     5   	     � Title$  DisplayWindow��5   	   $	��450��6   $	��750��6,   .��5	      � .+�5
   
   �� -   5         ��Name$ gridSplitter��5
      $
��Left=�6-   $	��420��6H   $��	306,0,0,0q�6U   $	��Top=�6h   $��12��6�   �� 5   
   -   5         ��Name$ StoredContactTextBlock��5      $
��Left=�64   $��
482,40,0,0q�6O   $
��Wrap=�6c   $��Stored Contact��6w   $	��Top=�6�   �� 5   
   -   5        $ NameTxtBlock��5      $
��Left=�6*   $��
337,85,0,0q�6E   $
��Wrap=�6Y   $
��Name��6m   $	��Top=�6y   �� 5   
   -   5        $ CompanyTxtBlock��5      $
��Left=�6-   $��
549,87,0,0q�6H   $
��Wrap=�6\   $��Company��6p   $	��Top=�6   �� 5   
   -   5        $ PhoneTxtBlock��5      $
��Left=�6+   $��337,121,0,0q�6F   $
��Wrap=�6[   $��Phone��6o   $	��Top=�6|    J� RenderTransformOrigin$ -0.518,-1.504&�6�   �� 5   
   -   5        $ EmailTxtBlock��5      $
��Left=�6+   $��570,124,0,0q�6F   $
��Wrap=�6[   $��Email��6o   $	��Top=�6|   �� 5   
   -   5         ��Name$ PhoneComboBox��5      $
��Left=�6*   $��422,151,0,0q�6E   $	��Top=�6Z   $��93��6r   6�   �� 5   
   -   5        $ AddressTxtBlock��5      $
��Left=�6-   $��337,158,0,0q�6H   $
��Wrap=�6]   $��Address��6q   $	��Top=�6�   �� 5   
   -	   5        $ BirthdayTxtBlock��5      $
��Left=�6.   $��555,161,0,0q�6I   $
��Wrap=�6^   $��Birthday��6r   $	��Top=�6�   �� 5   
   -
   5        $ GenderTxtBlock��5      $
��Left=�6,   $��561,202,0,0q�6G   $
��Wrap=�6\   $��Gender��6p   $	��Top=�6~   �� 5   
   -   5        $ PositionTxtBlock��5      $
��Left=�6.   $��558,237,0,0q�6I   $
��Wrap=�6^   $��Position��6r   $	��Top=�6�   �� 5   
   -   5        $ NotesTxtBlock��5      $
��Left=�6+   $��569,273,0,0q�6F   $
��Wrap=�6[   $��Notes��6o   $	��Top=�6|   �� 5   
   -   5        $ YearsTxtBlock��5      $
��Left=�6+   $��337,339,0,0q�6F   $
��Wrap=�6[   $��Years in the company��6o   $	��Top=�6�   �� 5   
   -   5         ��Name$ 
NameTxtBox��5      $
��Left=�6&   $��23��6A   $��
387,87,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $��TextBox��6u   $	��Top=�6�   $	��128��6�   �� 5   
   -   5        $ CompanyTxtBox��5      $
��Left=�6)   $��23��6D   $��
618,87,0,0q�6P   $
 Wrap=�6d   $��TextBox��6x   $	��Top=�6�   $��88��6�   �� 5   
   -   5        $ PhoneTxtBox��5      $
��Left=�6'   $��23��6B   $��387,120,0,0q�6N   $
 Wrap=�6c   $��TextBox��6w   $	��Top=�6�   $	��173��6�   �� 5   
   -   5        $ AddressTxtBox��5      $
��Left=�6)   $	��130��6D   $��337,185,0,0q�6Q   $
 Wrap=�6f   $��TextBox��6z   $	��Top=�6�   $	��198��6�   �� 5   
   -   5        $ YearsTxtBox��5      $
��Left=�6'   $��23��6B   $��337,360,0,0q�6N   $
 Wrap=�6c   $��TextBox��6w   $	��Top=�6�   $��63��6�   �� 5   
   -   5        $ EmailTxtBox��5      $
��Left=�6'   $��23��6B   $��618,121,0,0q�6N   $
 Wrap=�6c   $��TextBox��6w   $	��Top=�6�   $��88��6�   �� 5   
   -   5        $ BirthDayTxtBox��5      $
��Left=�6*   $��23��6E   $��618,161,0,0q�6Q   $
 Wrap=�6f   $��TextBox��6z   $	��Top=�6�   $��88��6�   �� 5   
   -   5        $ GenderTxtBox��5      $
��Left=�6(   $��23��6C   $��618,201,0,0q�6O   $
 Wrap=�6d   $��TextBox��6x   $	��Top=�6�   $��88��6�   �� 5   
   -   5        $ PositionTxtBox��5      $
��Left=�6*   $��23��6E   $��618,236,0,0q�6Q   $
 Wrap=�6f   $��TextBox��6z   $	��Top=�6�   $��88��6�   �� 5    
   -   5        $ NotesTxtBox��5       $
��Left=�6'   $��89��6B   $��555,294,0,0q�6N   $
 Wrap=�6c   $��TextBox��6w   $	��Top=�6�   $	��151��6�   �� 5!   
   -   5         ��Name$ button��5!      $��New Contact��6!   $
��Left=�67   $��
20,124,0,0q�6R   $	��Top=�6f   $��75��6~   6�   �� 5"   
   -   5        $ button1��5"      $��Delete Contact��6"   $
��Left=�6;   $��181,124,0,0q�6V   $	��Top=�6k   $��83��6�   6�   �� 5#   
   -   5        $ button2��5#      $
��Edit��6"   $
��Left=�61   $��
20,239,0,0q�6L   $	��Top=�6`   $��75��6x   6�   �� 5$   
   -   5        $ button3��5$      $��Search��6"   $
��Left=�63   $��181,238,0,0q�6N   $	��Top=�6c   $��83��6{   6�   5&      5'      