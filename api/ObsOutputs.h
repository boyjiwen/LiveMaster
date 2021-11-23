#pragma once
#include <obs.hpp>
#include <util/config-file.h>
#include <string>

class ObsBasic;

#define DESKTOP_AUDIO_1 Str("DesktopAudioDevice1")
#define DESKTOP_AUDIO_2 Str("DesktopAudioDevice2")
#define AUX_AUDIO_1     Str("AuxAudioDevice1")
#define AUX_AUDIO_2     Str("AuxAudioDevice2")
#define AUX_AUDIO_3     Str("AuxAudioDevice3")

#define SIMPLE_ENCODER_X264                    "x264"
#define SIMPLE_ENCODER_X264_LOWCPU   "x264_lowcpu"
#define SIMPLE_ENCODER_QSV                     "qsv"
#define SIMPLE_ENCODER_NVENC                   "nvenc"
#define SIMPLE_ENCODER_AMD                     "amd"

struct BasicOutputHandler {
	OBSOutput              fileOutput;
	OBSOutput              streamOutput;
	OBSOutput              replayBuffer;
	bool                   streamingActive = false;
	bool                   recordingActive = false;
	bool                   delayActive = false;
	bool                   replayBufferActive = false;
    ObsBasic     *main;

	std::string            outputType;

	OBSSignal              startRecording;
	OBSSignal              stopRecording;
	OBSSignal              startReplayBuffer;
	OBSSignal              stopReplayBuffer;
	OBSSignal              startStreaming;
	OBSSignal              stopStreaming;
	OBSSignal              streamDelayStarting;
	OBSSignal              streamStopping;
	OBSSignal              recordStopping;
	OBSSignal              replayBufferStopping;

	inline BasicOutputHandler(ObsBasic *main_) : main(main_) {}

	virtual ~BasicOutputHandler() {};

	virtual bool StartStreaming(obs_service_t *service) = 0;
	virtual bool StartRecording() = 0;
	virtual bool StartReplayBuffer() {return false;}
	virtual void StopStreaming(bool force = false) = 0;
	virtual void StopRecording(bool force = false) = 0;
	virtual void StopReplayBuffer(bool force = false) {(void)force;}
	virtual bool StreamingActive() const = 0;
	virtual bool RecordingActive() const = 0;
	virtual bool ReplayBufferActive() const {return false;}

	virtual void Update() = 0;

	inline bool Active() const
	{
		return streamingActive || recordingActive || delayActive ||
			replayBufferActive;
	}

    config_t* config()const;
};

BasicOutputHandler *CreateSimpleOutputHandler(ObsBasic *main);
BasicOutputHandler *CreateAdvancedOutputHandler(ObsBasic *main);
