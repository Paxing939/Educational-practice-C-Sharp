@rem --- Compile in MSVS 2015x86 win-gui application
@call "%VS140COMNTOOLS%vsvars32.bat"
@csc /target:winexe /codepage:1251 /platform:x86 /o %1

