// Компиляция в MSVC 2010x86 :
//> "%VS100COMNTOOLS%vsvars32.bat"
//> csc /target:exe /platform:x86 /o cstr.cs
using System;
namespace cstr {
  class Program {
    static void Main(string[] args) {
      string str = "string";
      string strsave = str;
      str += " contains constant values!";
      // str[0] = 'S'; //error 
      Console.WriteLine( str == strsave );
}}}

