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
	String^ Text;		// �ı�����
	String^ Font;		// ����
	UInt32 Color = 0;		// ������ɫ
	int Size = 36;			// �����С
	bool Bold = false;		// �Ӵ�
	bool Italic = false;	// б��
	bool Underline = false;	// �»���
	bool Strikeout = false;	// ɾ����
	int Opacity = 100;		// ͸����

	// �Զ����ı���
	bool Extents = false;		// �����Զ����ı���
	int ExtentsCx = 0;			// ���
	int ExtentsCy = 0;			// �߶�
	bool ExtentsWrap = false;	// �Զ�����

	// ����
	String^ Align = "center";	// left,center,right
	String^ Valign = "center";	// top,center,bottom

	// ����
	UInt32 BkColor = 0;	// ������ɫ
	int BkOpacity = 100;	// ����͸����

	// ���
	bool Outline = false;		// �������
	int OutlineSize = 0;		// ��߱߿���
	UInt32 OutlineColor = 0;	// �����ɫ
	int OutlineOpacity = 100;	// ���͸����

	// �����ٶ�25,50,100
	int ScrollSpeed = 0;

	// ��ֱ��������
	bool Vertical = false;

	// ����
	bool Gradient = false;			// ��������
	UInt32 GradientColor = 0;	// ������ɫ
	int GradientOpacity = 100;		// ����͸����
	int GradientDirection = 90;	// ���䷽��

	// ����ģʽ
	bool Chatlog = true;	//�Ƿ�������ģʽ
	int ChatlogLines = 6;	//����ģʽ����
};
