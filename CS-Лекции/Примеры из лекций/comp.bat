@rem --- Compile in MSVS 2015x86 console application
@call "%VS140COMNTOOLS%vsvars32.bat"
@csc /target:exe /codepage:1251 /platform:x86 /o %1

