#pragma once
#include "ObsCore.h"
//#include "TSceneItem.h"


public delegate void EventReloadSceneItemList(List<SceneItem^>^ li);
public delegate void EventCurrentScene(String^ name);


public ref class ObsWrapper
{
public:
	void Init();
	void InitObs();

	void SaveProject();
	void SaveConfig();

	//	Window
	void InitWindow(long hwnd, int w, int h);
	void SetWindowPos(long x, long y, long w, long h);

	//	Scene
	void LoadScene();
	void CreateScene(String^ name);
	void GetScenes(List<SceneData^>^ li);
	void ChangeScene(int idx);

	//	Scene item
	void SetSceneItemVisibile(int idx, bool visible);
	void RemoveSceneItem(int idx);
	void SelectSceneItem(int idx);
	

	//	�������
	void StartStream();
	void StopStream();
	void StartRecording();
	void StopRecording();
	

	//	��Ļ��׽
	void GetMonitor(System::Collections::IList^ vMonitors);
	void GetMonitors(List<CLI_MinitorItem^>^ vItems);
	void AddCaptureScreen(String^ name, int index, bool isSelect);

	//	���ڲ�׽
	void GetWindows(List<CLI_WindowItem^>^ vItems);
	void AddCaptureWindow(String^ name, String^ winName);

	//	���ͼƬԴ
	void AddImageSource(String^ url, int alpha);
	
	//	�������Դ
	void AddTextSource(CLI_TextData^ data);


	//	Config���
	String^ GetConfigString(String^ section, String^ name);
	bool GetConfigBool(String^ section, String^ name);
	LONG64 GetConfigInt(String^ section, String^ name);
	ULONG64 GetConfigUInt(String^ section, String^ name);
	double GetConfigDouble(String^ section, String^ name);

	void SetConfigString(String^ section, String^ name, String^ value);
	void SetConfigBool(String^ section, String^ name, bool value);
	void SetConfigInt(String^ section, String^ name, LONG64 value);
	void SetConfigUInt(String^ section, String^ name, ULONG64 value);
	void SetConfigDouble(String^ section, String^ name, double value);
	




public:
	event EventReloadSceneItemList^ OnEventReloadSceneItemList;
	event EventCurrentScene^ OnEventCurrentScene;

public:
	void CallLoadSceneFinish();
	void CallReloadSceneItemList(void* data);
	void CallAddScene(void* data);
	


private:
	ObsCore* m_core;

};

