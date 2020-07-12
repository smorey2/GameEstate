mkdir il
copy ..\src\GameEstate\bin\Debug\netstandard2.0\GameEstate*.dll il\.
..\lib\i2lcpp\il2cpp\build\deploy\net471\il2cpp.exe ^
  --assembly=il\GameEstate.Abstract.dll ^
  --libil2cpp-static ^
  --convert-to-cpp ^
  --generatedcppdir=GameEstate.Abstract.cpp ^
  --verbose