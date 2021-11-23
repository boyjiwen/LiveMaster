#pragma once
#include <obs.hpp>
#include <vector>
#include <memory>

class ObsVolumeControl;

class ObsVolumeControlObserver
{
public:
    virtual ~ObsVolumeControlObserver() {}
    virtual void OnVolumeChanged(ObsVolumeControl* ctrl,float db) = 0;
    virtual void OnVolumeLevel(ObsVolumeControl* ctrl, 
        const float magnitude[MAX_AUDIO_CHANNELS],
        const float peak[MAX_AUDIO_CHANNELS],
        const float inputPeak[MAX_AUDIO_CHANNELS]) = 0;
    virtual void OnVolumeMuted(ObsVolumeControl* ctrl, bool muted)=0;
};

//obs“Ù∆µøÿ÷∆
class ObsVolumeControl
{
public:
    ObsVolumeControl(OBSSource source, ObsVolumeControlObserver* obs);
    ~ObsVolumeControl();

    void SetMuted(bool checked);
    void SetVolume(int vol);//0~100

    inline obs_source_t *GetSource() const { return m_source; }

    const char* name() {
        return obs_source_get_name(m_source);
    }

    float deflection() {
        return obs_fader_get_deflection(m_obs_fader) * 100.0f;
    }

    float db() {
        return obs_fader_get_db(m_obs_fader);
    }

private:
    static void OBSVolumeChanged(void *param, float db);
    static void OBSVolumeLevel(void *data,
        const float magnitude[MAX_AUDIO_CHANNELS],
        const float peak[MAX_AUDIO_CHANNELS],
        const float inputPeak[MAX_AUDIO_CHANNELS]);
    static void OBSVolumeMuted(void *data, calldata_t *calldata);

    OBSSource m_source;
    obs_fader_t     *m_obs_fader;
    obs_volmeter_t  *m_obs_volmeter;
    ObsVolumeControlObserver* m_observer;
};
