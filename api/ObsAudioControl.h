#pragma once
#include <obs.hpp>
#include <vector>
#include <memory>

class ObsAudioControl;

class ObsAudioControlObserver
{
public:
    virtual ~ObsAudioControlObserver() {}
    virtual void OnSourceFlagsChanged(ObsAudioControl* ctrl, uint32_t flags) =0;
    virtual void OnSourceVolumeChanged(ObsAudioControl* ctrl, float volume) =0;
    virtual void OnSourceSyncChanged(ObsAudioControl* ctrl, int64_t offset) =0;
    virtual void OnSourceMixersChanged(ObsAudioControl* ctrl, uint32_t mixers) =0;
};

//设置音频参数
class ObsAudioControl
{
public:
    ObsAudioControl(obs_source_t *source, ObsAudioControlObserver* observer);
    ~ObsAudioControl();

    void SetVolume(int percentage);
    void SetForceMono(bool forceMeno);
    void SetSyncOffset(int milliseconds);

    void SetMonitoringType(obs_monitoring_type type);
    void SetMixer(const int mixerIdx, const bool checked);

    //属性
    const char* name() {
        return obs_source_get_name(m_source);
    }

    float volume() {
        return obs_source_get_volume(m_source);
    }

    uint32_t flags() {
        return obs_source_get_flags(m_source);
    }

    uint32_t mixers() {
        return obs_source_get_audio_mixers(m_source);
    }

    obs_monitoring_type monitoring_type() {
        return obs_source_get_monitoring_type(m_source);
    }

protected:
    static void OBSSourceFlagsChanged(void *param, calldata_t *calldata);
    static void OBSSourceVolumeChanged(void *param, calldata_t *calldata);
    static void OBSSourceSyncChanged(void *param, calldata_t *calldata);
    static void OBSSourceMixersChanged(void *param, calldata_t *calldata);

    ObsAudioControlObserver* m_observer;
    OBSSource m_source;
    OBSSignal m_volChangedSignal;
    OBSSignal m_syncOffsetSignal;
    OBSSignal m_flagsSignal;
    OBSSignal m_mixersSignal;
};

class ObsAudioManager
{
public:
    ObsAudioManager(ObsAudioControlObserver* obs);
    ~ObsAudioManager();

    const std::vector<std::unique_ptr<ObsAudioControl>>& Controls() { return m_controls; }
private:
    static bool EnumSources(void *param, obs_source_t *source);

    ObsAudioControlObserver* m_observer;
    std::vector<std::unique_ptr<ObsAudioControl>> m_controls;
};

