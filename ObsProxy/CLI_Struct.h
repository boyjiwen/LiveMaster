#pragma once

#using <mscorlib.dll>


using namespace System::Collections;

using namespace System;

public ref class SceneData {
public:
	int index;
	String^ name;
};

public ref class SceneItem
{
public:
	String^ Name;
	bool bVisible;
	bool bSelected;
};


//	��Ļ����
public ref class CLI_MinitorItem
{
public:
	int Id;
	String^ Name;
};

//	��׽����
public ref class CLI_WindowItem 
{
public:
	String^ Name;
	String^ WinName;
};
