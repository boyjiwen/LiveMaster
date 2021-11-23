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


	//	ȡ�������
	String^ GetConfigString(String^ section, String^ name);




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

