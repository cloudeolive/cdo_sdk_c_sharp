rmdir $(ProjectDir)\cloudeo_sdk_C# /s /q
mkdir $(ProjectDir)\cloudeo_sdk_C#
copy $(TargetDir)\$(TargetFileName) $(ProjectDir)\cloudeo_sdk_C#
del $(ProjectDir)\Doxygen
copy $(ProjectDir)\Doxygen.in $(ProjectDir)\Doxygen
echo PROJECT_NUMBER = 1.17.0.0 >> $(ProjectDir)\Doxygen
echo OUTPUT_DIRECTORY = $(ProjectDir)\cloudeo_sdk_C#\apidocs >> $(ProjectDir)\Doxygen
echo INPUT = $(ProjectDir) >> $(ProjectDir)\Doxygen

