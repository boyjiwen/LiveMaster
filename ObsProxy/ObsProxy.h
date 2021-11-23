#pragma once
#include "api/ObsMain.h"
//#include "MsgObserver.h"

using namespace System;

namespace ObsProxy {
	public ref class MOBS 
	{	
	public:		
		void InitObsMain();
		void MakeUserDirs();
		void InitGlobalConfig();
		void MakeUserProfileDirs();
		void InitObs();

		//
		void InitObsWindow(long hwnd, int w, int h);
		void SetWindowPos(long x, long y, long w, long h);
		void LoadScene();


		//	屏幕
		void GetMonitor(System::Collections::IList^ vMonitors);
		void GetMonitors(List<CLI_MinitorItem^>^ vItems);

		void AddCaptureScreen(String^ name, int index, bool isSelect);

	private:
		void OnCallAddScene(OBSScene source);


	private:
		ObsMain* g_obsMain;
		ObsWindow* g_obsWnd;
		//MsgObsServer* m_obsServer;		

	};
}
