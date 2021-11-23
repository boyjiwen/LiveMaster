#pragma once
#include <memory>
#include <util/util.hpp>

#include "ObsOutputs.h"
#include "ObsWindowBase.h"
#define Str(s) s

struct RtmpServer
{
    std::string server;
    std::string key;
    bool use_auth = false;
    std::string username;
    std::string password;
};

class ObsObserver
{
public:
    virtual ~ObsObserver() {}

    virtual void OnLoadSceneFinish() = 0;
    virtual void OnAddScene(OBSScene source) =0;
    virtual void OnRemoveScene(OBSScene source) = 0;
    virtual void OnReorderScene() = 0;

    virtual void OnActivateAudioSource(OBSSource source) =0;

    virtual void OnDeactivateAudioSource(OBSSource source) = 0;

    virtual void OnRenameSources(OBSSource source,
        const char* oldName,const char* newName) = 0;

    //重新加载sceneitem
    virtual void OnReloadSceneItemList() = 0;
    virtual void OnSceneItemSelectChanged(int index) = 0;

    //推流回调接口
    virtual void OnStreamStopping() = 0;
    virtual void OnStreamingStart() = 0;
    virtual void OnStreamingStop(int code, const char* error) = 0;

    //录制回调接口
    virtual void OnRecordingStart() = 0;
    virtual void OnRecordStopping() = 0;
    virtual void OnRecordingStop(int code) = 0;

    //显示菜单
    virtual void OnMenu(window_handle_t handle,const ObsPoint& point) = 0;

    //设置里重置video大小
    virtual void OnVideoReset() = 0;
};


class ObsBasic
{
public:
    ObsBasic();
    ~ObsBasic();

    //初始化obs
    virtual bool InitObs();

    OBSService GetService();
    void  SetService(obs_service_t *service);
    void SetObserver(ObsObserver* obs) { m_observer = obs; }
    bool SetRtmpServer(const RtmpServer& server);

    bool StreamingActive() const;
    bool Active() const;

    bool StartStreaming();
    void StopStreaming();
    void ForceStopStreaming();
    bool StartRecording();
    void StopRecording();
    bool StartReplayBuffer();
    void StopReplayBuffer();

    //由obsoutput调用接口
    void StreamDelayStarting(int delay);
    void StreamStopping();
    void StreamDelayStopping(int delay);
    void StreamingStart();
    void StreamingStop(int code, const char* error);
    void RecordingStart();
    void RecordStopping();
    void RecordingStop(int code);
    void ReplayBufferStart();
    void ReplayBufferStop(int code);
    void ReplayBufferStopping();

    //basic config
    config_t* basicConfig() { return m_basicConfig; }
    config_t* globalConfig()const { return m_globalConfig; }

    void SetTransition(OBSSource transition);

    //0~100
    void SetVolume(int channel,int vol);
    int GetVolume(int channel);
    void SetMuted(int channel,bool mute);
    bool GetMuted(int channel);
    bool CheckChannel(int channel);

    int GetProfilePath(char *path, size_t size, const char *file);
    static int GetConfigPath(char *path, size_t size, const char *name);

    //创建用户目录
    bool MakeUserDirs();
    bool MakeUserProfileDirs();

    ObsObserver* observer() { return m_observer; }

    //视频设置改变
    bool VideoSettingChange();
    bool ServiceSettingChange();
    bool AudioSettingChange();
    //是否正在录制
    bool StreamActive() {
        return (m_outputHandler && m_outputHandler->Active());
    }

    void SaveConfig() {
        m_basicConfig.SaveSafe("tmp");
        //m_globalConfig.SaveSafe("tmp");
    }

    //音频接口
    const char *InputAudioSource();
    const char *OutputAudioSource();
    void ResetAudioDevice(const char *sourceId, const char *deviceId,
        const char *deviceDesc, int channel);

protected:
    int  ResetVideo();
    bool ResetAudio();
    void ResetOutputs();
    bool  ResetService();

    void EnableDesktopAudio(bool enable);
    void EnableInputAudio(bool enable);
    void InitAudioSources();

    bool InitBasicConfig();

    //初始化配置
    bool InitBasicConfigDefaults();
    //初始化默认转换
    void InitDefaultTransitions();

    void GetConfigFPS(uint32_t &num, uint32_t &den) const;
    void GetFPSCommon(uint32_t &num, uint32_t &den) const;
    void GetFPSInteger(uint32_t &num, uint32_t &den) const;
    void GetFPSFraction(uint32_t &num, uint32_t &den) const;
    void GetFPSNanoseconds(uint32_t &num, uint32_t &den) const;




    void CheckForSimpleModeX264Fallback();

    OBSService m_service;
    ConfigFile  m_basicConfig;
    ConfigFile  m_globalConfig;

    std::unique_ptr<BasicOutputHandler> m_outputHandler;

    OBSSource m_curTransition; //当前场景转换

    bool m_streamingStopping = false;
    bool m_recordingStopping = false;
    bool m_replayBufferStopping = false;

    ObsObserver* m_observer = nullptr;
};


