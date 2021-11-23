#include "pch.h"
#include "api/ObsWindow.h"
#include "api/ObsUtils.h"
#include <vcclr.h>

#include "ObsProxy.h"
//	#include <wtypes.h>







void ObsProxy::MOBS::InitObsMain()
{
	g_obsMain = new ObsMain();
	//g_mainObserver = new MainObserver();

	//m_obsServer = new MsgObsServer();
	//->SetCallAddScene();

}

void ObsProxy::MOBS::MakeUserDirs()
{
	g_obsMain->MakeUserDirs();
}

void ObsProxy::MOBS::InitGlobalConfig()
{
	g_obsMain->InitGlobalConfig();
}

void ObsProxy::MOBS::MakeUserProfileDirs()
{
	g_obsMain->MakeUserProfileDirs();
}

void ObsProxy::MOBS::InitObs()
{
	g_obsMain->InitObs();
}

void ObsProxy::MOBS::InitObsWindow(long hwnd, int w, int h)
{
	g_obsWnd = ObsWindow::Create();
	ObsRect rc = { 0, 0, w, h };
	g_obsWnd->CreateWnd((window_handle_t)hwnd, rc);
	g_obsWnd->CreateDisplay();

	config_t* config = GetBasicConfig();
	if (config)
	{
		if (config_get_bool(config, "Output", "AutoRecord"))
		{

		}
	}

	g_obsMain->LoadScene();

	//if (config_get_bool(config, "BasicWindow", "AddTray"))
	//{
	//	//::PostMessage(m_hWnd, MSG_UPDATE_TRAY, 1, 0);
	//}
}

void ObsProxy::MOBS::SetWindowPos(long x, long y, long w, long h)
{
	ObsRect rc{ x, y ,w, h };
	g_obsWnd->SetWindowPos(rc);
}

void ObsProxy::MOBS::LoadScene()
{
	//g_obsMain->LoadScene();
}

///////////////////////////////////////////
void OnEnumMonitor(void* ptr, const char* name, int id) {	
	String^ aaa = gcnew String(name);

	
}

void ObsProxy::MOBS::GetMonitor(System::Collections::IList^ vMonitors)
{
	//EnumMonitor(OnEnumMonitor, NULL);
	std::vector<std::wstring> vArrays;
	EnumMonitor(vArrays);

	for (size_t i = 0; i < vArrays.size(); i++)
	{
		vMonitors->Add(gcnew String(vArrays[i].c_str()));
	}
}

//	È¡ÏÔÊ¾Æ÷Êý
void ObsProxy::MOBS::GetMonitors(List<CLI_MinitorItem^>^ vItems)
{
	obs_properties_t* prop = obs_properties_by_id("monitor_capture");
	if (prop)
	{
		obs_property_t* monitors = obs_properties_get(prop, "monitor");

		size_t  count = obs_property_list_item_count(monitors);
		for (size_t i = 0; i < count; i++)
		{
			const char* name = obs_property_list_item_name(monitors, i);
			int id = obs_property_list_item_int(monitors, i);

			CLI_MinitorItem^ it = gcnew CLI_MinitorItem();
			it->Id = id;
			it->Name = gcnew String(FromUtf8(name).c_str());
			vItems->Add(it);

			blog(LOG_INFO, "EnumMonitor:%s  %d", name, id);
		}
		obs_properties_destroy(prop);
	}
}

void ObsProxy::MOBS::AddCaptureScreen(String^ name, int index, bool isSelect)
{
	pin_ptr<const wchar_t> convertedValue = PtrToStringChars(name);  
	const wchar_t* constValue = convertedValue;
	
	g_obsMain->AddCaptureScreen(ToUtf8(constValue).c_str(), index, isSelect);
	
}

void ObsProxy::MOBS::OnCallAddScene(OBSScene source)
{
	throw gcnew System::NotImplementedException();
}
	