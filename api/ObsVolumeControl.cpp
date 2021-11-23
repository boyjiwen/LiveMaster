#include "ObsVolumeControl.h"

ObsVolumeControl::ObsVolumeControl(OBSSource source, ObsVolumeControlObserver* obs)
    :m_source(source), m_observer(obs)
{

    m_obs_fader = obs_fader_create(OBS_FADER_CUBIC);
    m_obs_volmeter = obs_volmeter_create(OBS_FADER_LOG);

    obs_fader_add_callback(m_obs_fader, OBSVolumeChanged, this);
    obs_volmeter_add_callback(m_obs_volmeter, OBSVolumeLevel, this);

}

ObsVolumeControl::~ObsVolumeControl()
{
    obs_fader_remove_callback(m_obs_fader, OBSVolumeChanged, this);
    obs_volmeter_remove_callback(m_obs_volmeter, OBSVolumeLevel, this);

    signal_handler_disconnect(obs_source_get_signal_handler(m_source),
        "mute", OBSVolumeMuted, this);

    obs_fader_destroy(m_obs_fader);
    obs_volmeter_destroy(m_obs_volmeter);
}

void ObsVolumeControl::SetMuted(bool checked)
{
    obs_source_set_muted(m_source, checked);
}

void ObsVolumeControl::SetVolume(int vol)
{
    obs_fader_set_deflection(m_obs_fader, float(vol) * 0.01f);
}

void ObsVolumeControl::OBSVolumeChanged(void *data, float db)
{
    ObsVolumeControl *volControl = static_cast<ObsVolumeControl*>(data);

    if(volControl->m_observer)
        volControl->m_observer->OnVolumeChanged(volControl,db);
}

void ObsVolumeControl::OBSVolumeLevel(void *data,
    const float magnitude[MAX_AUDIO_CHANNELS],
    const float peak[MAX_AUDIO_CHANNELS],
    const float inputPeak[MAX_AUDIO_CHANNELS])
{
    ObsVolumeControl *volControl = static_cast<ObsVolumeControl*>(data);
    if (volControl->m_observer)
        volControl->m_observer->OnVolumeLevel(volControl,magnitude,peak,inputPeak);
}

void ObsVolumeControl::OBSVolumeMuted(void *data, calldata_t *calldata)
{
    ObsVolumeControl *volControl = static_cast<ObsVolumeControl*>(data);
    bool muted = calldata_bool(calldata, "muted");
    if (volControl->m_observer)
        volControl->m_observer->OnVolumeMuted(volControl,muted);
}
