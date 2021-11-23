#include "ObsAudioControl.h"

#ifndef NSEC_PER_MSEC
#define NSEC_PER_MSEC 1000000
#endif


ObsAudioControl::ObsAudioControl(obs_source_t *source, ObsAudioControlObserver* observer)
    :m_source(source), m_observer(observer)
{
    signal_handler_t *handler = obs_source_get_signal_handler(source);
    m_volChangedSignal.Connect(handler, "volume", OBSSourceVolumeChanged,
        this);
    m_syncOffsetSignal.Connect(handler, "audio_sync", OBSSourceSyncChanged,
        this);
    m_flagsSignal.Connect(handler, "update_flags", OBSSourceFlagsChanged,
        this);
    m_mixersSignal.Connect(handler, "audio_mixers", OBSSourceMixersChanged,
        this);
}

ObsAudioControl::~ObsAudioControl()
{


}

void ObsAudioControl::OBSSourceFlagsChanged(void *param, calldata_t *calldata)
{
    ObsAudioControl* pThis = (ObsAudioControl*)param;
    uint32_t flags = (uint32_t)calldata_int(calldata, "flags");

    if(pThis->m_observer)
        pThis->m_observer->OnSourceFlagsChanged(pThis,flags);
}
void ObsAudioControl::OBSSourceVolumeChanged(void *param, calldata_t *calldata)
{
    ObsAudioControl* pThis = (ObsAudioControl*)param;
    float volume = (float)calldata_float(calldata, "volume");
    if (pThis->m_observer)
        pThis->m_observer->OnSourceVolumeChanged(pThis,volume);
}


void ObsAudioControl::OBSSourceSyncChanged(void *param, calldata_t *calldata)
{
    ObsAudioControl* pThis = (ObsAudioControl*)param;
    int64_t offset = calldata_int(calldata, "offset");
    if (pThis->m_observer)
        pThis->m_observer->OnSourceSyncChanged(pThis,offset);
}

void ObsAudioControl::OBSSourceMixersChanged(void *param, calldata_t *calldata)
{
    ObsAudioControl* pThis = (ObsAudioControl*)param;
    uint32_t mixers = (uint32_t)calldata_int(calldata, "mixers");
    if (pThis->m_observer)
        pThis->m_observer->OnSourceMixersChanged(pThis,mixers);
}


void ObsAudioControl::SetVolume(int percentage)
{
    float val = float(percentage) / 100.0f;
    obs_source_set_volume(m_source, val);
}
void ObsAudioControl::SetForceMono(bool forceMeno)
{
    uint32_t flags = obs_source_get_flags(m_source);
    bool forceMonoActive = (flags & OBS_SOURCE_FLAG_FORCE_MONO) != 0;

    if (forceMonoActive != forceMeno) {
        if (forceMeno)
            flags |= OBS_SOURCE_FLAG_FORCE_MONO;
        else
            flags &= ~OBS_SOURCE_FLAG_FORCE_MONO;

        obs_source_set_flags(m_source, flags);
    }
}
void ObsAudioControl::SetSyncOffset(int milliseconds)
{
    int64_t cur_val = obs_source_get_sync_offset(m_source);

    if (cur_val / NSEC_PER_MSEC != milliseconds)
        obs_source_set_sync_offset(m_source,
            int64_t(milliseconds) * NSEC_PER_MSEC);
}
void ObsAudioControl::SetMonitoringType(obs_monitoring_type mt)
{
    obs_source_set_monitoring_type(m_source, mt);
    const char *type = nullptr;
    switch (mt) {
    case OBS_MONITORING_TYPE_NONE:
        type = "none";
        break;
    case OBS_MONITORING_TYPE_MONITOR_ONLY:
        type = "monitor only";
        break;
    case OBS_MONITORING_TYPE_MONITOR_AND_OUTPUT:
        type = "monitor and output";
        break;
    }

    blog(LOG_INFO, "User changed audio monitoring for source '%s' to: %s",
        obs_source_get_name(m_source), type);
}


void ObsAudioControl::SetMixer(const int mixerIdx,const bool checked)
{
    uint32_t mixers = obs_source_get_audio_mixers(m_source);
    uint32_t new_mixers = mixers;

    if (checked) new_mixers |= (1 << mixerIdx);
    else         new_mixers &= ~(1 << mixerIdx);

    obs_source_set_audio_mixers(m_source, new_mixers);
}



ObsAudioManager::ObsAudioManager(ObsAudioControlObserver* obs)
    :m_observer(obs)
{
    obs_enum_sources(EnumSources, this);
}

ObsAudioManager::~ObsAudioManager()
{

}

bool ObsAudioManager::EnumSources(void *param, obs_source_t *source)
{
    ObsAudioManager *mgr = reinterpret_cast<ObsAudioManager*>(param);
    uint32_t flags = obs_source_get_output_flags(source);

    if ((flags & OBS_SOURCE_AUDIO) != 0 && obs_source_active(source))
    {
        mgr->m_controls.push_back(
            std::make_unique<ObsAudioControl>(source, mgr->m_observer));
    }
    return true;
}
