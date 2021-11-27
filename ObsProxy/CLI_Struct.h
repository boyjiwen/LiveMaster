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

//	��������
public ref class CLI_TextData
{
public:
	String^ Name;
	String^ Text;
	String^ Font;
	UInt32 Color = 0;
	int Size = 16;
	bool Bold = false;
	bool Italic = false;

	bool Extents = false;//�Զ����ı���
	int ExtentsCx = 0;//���
	bool ExtentsWrap = false;//�Զ�����
	String^ Align = "center"; //left,center,right
	String^ Valign = "center";//top,center,bottom

	//���
	int OutlineSize = 0;
	UInt32 OutlineColor = 0;

	//�����ٶ�25,50,100
	int ScrollSpeed = 0;

	//͸����
	int Opacity = 100;
	int OutlineOpacity = 100;
};
