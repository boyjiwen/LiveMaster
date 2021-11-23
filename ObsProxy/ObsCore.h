#pragma once
#include "api/ObsMain.h"
#include "api/ObsBasic.h"
#include "api/ObsWindow.h"
//#include "ObsWrapper.h"

#include <msclr/gcroot.h>

using namespace msclr;

ref class ObsWrapper;

class ObsCore : public ObsMain, public ObsObserver
{
public:
	//void InitObsMain();
    ObsCore()
    {
        int d = 0;
    }

    void InitObsCore(ObsWrapper^  wrapper);

    void InitObsWindow(long hwnd, int w, int h);
    void SetWindowPos(long x, long y, long w, long h);
    

public:
    virtual void OnSceneItemAdded(void* data, calldata_t* params);

public:
    virtual void OnLoadSceneFinish();
    virtual void OnAddScene(OBSScene source);
    virtual void OnRemoveScene(OBSScene source);
    virtual void OnReorderScene() {}

    virtual void OnActivateAudioSource(OBSSource source) {}

    virtual void OnDeactivateAudioSource(OBSSource source) {}

    virtual void OnRenameSources(OBSSource source,
        const char* oldName, const char* newName) {}

    //���¼���sceneitem
    virtual void OnReloadSceneItemList();
    virtual void OnSceneItemSelectChanged(int index) {}

    //�����ص��ӿ�
    virtual void OnStreamStopping() {}
    virtual void OnStreamingStart() {}
    virtual void OnStreamingStop(int code, const char* error) {}

    //¼�ƻص��ӿ�
    virtual void OnRecordingStart() {}
    virtual void OnRecordStopping() {}
    virtual void OnRecordingStop(int code) {}

    //��ʾ�˵�
    virtual void OnMenu(window_handle_t handle, const ObsPoint& point) {}

    //����������video��С
    virtual void OnVideoReset() {}






private:
    ObsWindow* m_obsWindows;
    gcroot<ObsWrapper^> m_wrapper;
    

};

