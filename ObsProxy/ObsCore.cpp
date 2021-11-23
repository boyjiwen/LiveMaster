#include "pch.h"
#include "ObsCore.h"
#include "api/ObsBasic.h"
#include "api/ObsUtils.h"
#include "CLI_Struct.h"
#include "ObsWrapper.h"

void ObsCore::InitObsCore(ObsWrapper^ wrapper)
{	
	m_wrapper = wrapper;
}

void ObsCore::InitObsWindow(long hwnd, int w, int h)
{
	m_obsWindows = ObsWindow::Create();
	ObsRect rc = { 0, 0, w, h };
	m_obsWindows->CreateWnd((window_handle_t)hwnd, rc);
	m_obsWindows->CreateDisplay();

	SetObserver(this);
}

void ObsCore::SetWindowPos(long x, long y, long w, long h)
{ 
	ObsRect rc{ x, y ,w, h };
	m_obsWindows->SetWindowPos(rc);
}

void ObsCore::OnSceneItemAdded(void* data, calldata_t* params)
{
	int d = 0;
}

//	Load Scene init finish
void ObsCore::OnLoadSceneFinish()
{
	/*OBSScene scene = GetCurrentScene();
	OBSSource source = obs_scene_get_source(scene);

	std::wstring dd = FromUtf8(obs_source_get_name(source));*/
	m_wrapper->CallLoadSceneFinish();
}

void ObsCore::OnAddScene(OBSScene scene)
{
	OBSSource source = obs_scene_get_source(scene);
	std::wstring dd = FromUtf8(obs_source_get_name(source));

	/*if (this->GetCurrentScene() == scene)
	{
		int d = 0;
	}*/

	m_wrapper->CallAddScene(&dd);

}

void ObsCore::OnRemoveScene(OBSScene source)
{
}

//	Scene Item
void ObsCore::OnReloadSceneItemList()
{
	ObsSceneItemList& itemList = sceneItemList();
	m_wrapper->CallReloadSceneItemList(&itemList);

	OBSScene dd = GetCurrentScene();

}

