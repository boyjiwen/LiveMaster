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


//	屏幕数据
public ref class CLI_MinitorItem
{
public:
	int Id;
	String^ Name;
};

//	捕捉数据
public ref class CLI_WindowItem
{
public:
	String^ Name;
	String^ WinName;
};

//	文字数据
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

	bool Extents = false;//自定义文本区
	int ExtentsCx = 0;//宽度
	bool ExtentsWrap = false;//自动换行
	String^ Align = "center"; //left,center,right
	String^ Valign = "center";//top,center,bottom

	//描边
	int OutlineSize = 0;
	UInt32 OutlineColor = 0;

	//滚动速度25,50,100
	int ScrollSpeed = 0;

	//透明度
	int Opacity = 100;
	int OutlineOpacity = 100;
};
