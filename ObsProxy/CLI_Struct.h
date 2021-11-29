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
	String^ Id;
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
	String^ Text;		// 文本内容
	String^ Font;		// 字体
	UInt32 Color = 0;		// 字体颜色
	int Size = 36;			// 字体大小
	bool Bold = false;		// 加粗
	bool Italic = false;	// 斜体
	bool Underline = false;	// 下划线
	bool Strikeout = false;	// 删除线
	int Opacity = 100;		// 透明度

	// 自定义文本区
	bool Extents = false;		// 开启自定义文本区
	int ExtentsCx = 0;			// 宽度
	int ExtentsCy = 0;			// 高度
	bool ExtentsWrap = false;	// 自动换行

	// 对齐
	String^ Align = "center";	// left,center,right
	String^ Valign = "center";	// top,center,bottom

	// 背景
	UInt32 BkColor = 0;	// 背景颜色
	int BkOpacity = 100;	// 背景透明度

	// 描边
	bool Outline = false;		// 开启描边
	int OutlineSize = 0;		// 描边边框宽度
	UInt32 OutlineColor = 0;	// 描边颜色
	int OutlineOpacity = 100;	// 描边透明度

	// 滚动速度25,50,100
	int ScrollSpeed = 0;

	// 垂直方向排列
	bool Vertical = false;

	// 渐变
	bool Gradient = false;			// 开启渐变
	UInt32 GradientColor = 0;	// 渐变颜色
	int GradientOpacity = 100;		// 渐变透明度
	int GradientDirection = 90;	// 渐变方向

	// 聊天模式
	bool Chatlog = true;	//是否开启聊天模式
	int ChatlogLines = 6;	//聊天模式行数
};
