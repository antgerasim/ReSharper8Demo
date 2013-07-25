dir *.svn /s /b /AH > temp.txt
for /F %%f in (temp.txt) do rd %%f /Q /S
del temp.txt