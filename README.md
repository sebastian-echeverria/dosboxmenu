# dosboxmenu
A Windows right-menu extension to quickly launch programs in DosBox from the File Explorer

To register:
1. Build DLL
2. Move DLL to whatever personal folder you want it installed to
3. Copy the batch files in the /DosBoxMenu/dist folder to the same folder
4. Start a command prompt as an Administrator
5. Change to the folder
6. Run "register.bat"

To unregister:
1. Start a command prompt as an Administrator
2. Change to the folder where the extension was registered from
3. Run "unregister.bat"

The extension currently assumes that the path of the DosBox executable is:

"C:\\Program Files (x86)\\DOSBox-0.74\\dosbox.exe"

If the executable is in a different path, write the full path to the file in an otherwise empty text file,
and store it as C:\dosboxpath.txt. The extension will automatically use the path found in that file instead.
Please use \\ instead of \ when writing the path to that file.
