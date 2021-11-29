#include "pch.h"
#include "api/ObsUtils.h"




#include "ObsWrapper.h"



//#include <vcclr.h>
//#include <list>
//#include "TSceneItem.h"






void ObsWrapper::Init()
{
    m_core = new ObsCore();
    m_core->InitObsCore(this);
}

void ObsWrapper::InitObs()
{
    m_core->MakeUserDirs();
    m_core->InitGlobalConfig();
    m_core->MakeUserProfileDirs();
    m_core->InitObs();
}

void ObsWrapper::SaveProject()
{
    m_core->SaveProject();
}

void ObsWrapper::SaveConfig()
{
    m_core->SaveConfig();
}

void ObsWrapper::InitWindow(long hwnd, int w, int h)
{
    m_core->InitObsWindow(hwnd, w, h);
}

void ObsWrapper::SetWindowPos(long x, long y, long w, long h)
{
    m_core->SetWindowPos(x, y, w, h);
}

void ObsWrapper::LoadScene()
{
    m_core->LoadScene();
}

void ObsWrapper::CreateScene(String^ name)
{
    pin_ptr<const wchar_t> convertName = PtrToStringChars(name);
    //std::wstring newName = convertName;

    std::string utf8Name = ToUtf8(convertName);

    bool bExistName = false;
    for (size_t i = 0; i < m_core->scenes().size(); i++)
    {
        if (strcmp(m_core->scenes()[i]->name(), utf8Name.c_str()) == 0)
        {
            bExistName = true;
            return;
        }
    }

    utf8Name = GenerateSourceName(utf8Name);   
    m_core->AddScene(utf8Name.c_str(), true);    
}

void ObsWrapper::GetScenes(List<SceneData^>^ li)
{
    for (size_t i = 0; i < m_core->scenes().size(); i++)
    {
        SceneData^ data = gcnew SceneData();
        data->index = i;

        OBSSource source = obs_scene_get_source(m_core->scenes()[i]->scene);
        data->name = gcnew String(FromUtf8(obs_source_get_name(source)).c_str());

        li->Add(data);
    }
}

void ObsWrapper::ChangeScene(int idx)
{
    if (idx >= 0 && idx < m_core->scenes().size())
    {
        OBSScene scene = m_core->scenes()[idx]->scene;
        m_core->SetCurrentScene(scene);

        OBSSource source = obs_scene_get_source(scene);
        std::wstring ss = FromUtf8(obs_source_get_name(source));

        OnEventCurrentScene(gcnew String(ss.c_str()));
    }   
}

void ObsWrapper::FitToScreen()
{
    if (m_core)
    {
        m_core->FitToScreen();
    }
}

void ObsWrapper::SetSceneItemVisibile(int idx, bool visible)
{
    ObsSceneItemList& itemList = m_core->sceneItemList();
    itemList.SetVisible(idx, visible);
}

void ObsWrapper::RemoveSceneItem(int idx)
{
    ObsSceneItemList& itemList = m_core->sceneItemList();
    itemList.Remove(idx);
}

void ObsWrapper::SelectSceneItem(int idx)
{
    ObsSceneItemList& itemList = m_core->sceneItemList();
    itemList.Select(idx);
}

void ObsWrapper::MoveUp(int idx)
{
    ObsSceneItemList& itemList = m_core->sceneItemList();
    itemList.MoveUp(idx);
}

void ObsWrapper::MoveDown(int idx)
{
    ObsSceneItemList& itemList = m_core->sceneItemList();
    itemList.MoveDown(idx);
}

void ObsWrapper::MoveToTop(int idx)
{
	ObsSceneItemList& itemList = m_core->sceneItemList();
	itemList.MoveToTop(idx);
}

void ObsWrapper::MoveToBottom(int idx)
{
	ObsSceneItemList& itemList = m_core->sceneItemList();
	itemList.MoveToBottom(idx);
}

int ObsWrapper::GetVolume(int channel)
{
    int volume = 50;
    if (m_core)
        volume = m_core->GetVolume(channel);
    return volume;
}

void ObsWrapper::SetMuted(int channel, bool mute)
{
	if (m_core)
		m_core->SetMuted(channel, mute);
}

bool  ObsWrapper::GetMuted(int channel)
{
    bool muted = true;
	if (m_core)
        muted = m_core->GetMuted(channel);
	return muted;
}

void ObsWrapper::StartStream()
{
    if (!m_core->StreamActive())
    {
        m_core->StartStreaming();
    }
}

void ObsWrapper::StopStream() {
    m_core->StopStreaming();
    //  m_obs->StopRecording();
}

void ObsWrapper::StartRecording()
{
   /* System::Threading::Tasks::Task::Factory->StartNew(() -> void {

    });*/
    //System::Threading::Tasks::Task::Factory->StartNew((Action<String^>^)( gcnew System::Action<String^>(this, &ObsWrapper::TestCall)), L"3333");

  
   // System::Threading::Tasks::Task::Factory->StartNew(gcnew System::Action<Object^>(this, &ObsWrapper::TestCall), (Object^)L"ddd");
    
}

void ObsWrapper::StopRecording()
{
    m_core->StopRecording();
}


//  ÆÁÄ»²¶×½
void ObsWrapper::GetMonitor(System::Collections::IList^ vMonitors)
{
    std::vector<std::wstring> vArrays;
    EnumMonitor(vArrays);    

    for (size_t i = 0; i < vArrays.size(); i++)
    {
        vMonitors->Add(gcnew String(vArrays[i].c_str()));
    }
}

void ObsWrapper::GetMonitors(List<CLI_MinitorItem^>^ vItems)
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


void ObsWrapper::AddCaptureScreen(String^ name, int index, bool isSelect)
{
    pin_ptr<const wchar_t> convertedValue = PtrToStringChars(name);
    const wchar_t* constValue = convertedValue;

    m_core->AddCaptureScreen(ToUtf8(constValue).c_str(), index, isSelect);
}

//  ´°¿Ú
void ObsWrapper::GetWindows(List<CLI_WindowItem^>^ vItems)
{
    obs_properties_t* prop = obs_properties_by_id("window_capture");
    if (prop)
    {
        obs_property_t* property = obs_properties_get(prop, "window");

        if (property) {
            const char* name = obs_property_name(property);
            obs_property_type type = obs_property_get_type(property);
            const char* desc = obs_property_description(property);

            blog(LOG_INFO, "[%s]:%s", name, desc);

            if (strcmp(name, "window") == 0 && type == OBS_PROPERTY_LIST)
            {
                size_t  count = obs_property_list_item_count(property);
                for (size_t i = 0; i < count; i++)
                {
                    const char* name = obs_property_list_item_name(property, i);
                    const char* window = obs_property_list_item_string(property, i);

                    CLI_WindowItem^ it = gcnew CLI_WindowItem();
                    it->Name = gcnew String(FromUtf8(name).c_str());
                    it->WinName = gcnew String(FromUtf8(window).c_str());

                    vItems->Add(it);

                    blog(LOG_INFO, "GameProcess:%s  %s", name, window);
                }
            }
        }
        obs_properties_destroy(prop);
    }
}

void ObsWrapper::AddCaptureWindow(String^ name, String^ winName)
{
    pin_ptr<const wchar_t> convertName = PtrToStringChars(name);
    const wchar_t* constName = convertName;

    pin_ptr<const wchar_t> convertWinName = PtrToStringChars(winName);
    const wchar_t* constWinName = convertWinName;

    m_core->AddWindowCapture(ToUtf8(constName).c_str(), ToUtf8(constWinName).c_str());        
}

void ObsWrapper::AddImageSource(String^ url, int alpha)
{
    pin_ptr<const wchar_t> cUrl = PtrToStringChars(url);
    ObsMain::Instance()->AddImage(ToUtf8(cUrl).c_str(), alpha);    
}

void ObsWrapper::AddTextSource(CLI_TextData^ data)
{
    ObsMain::TextData* textData = new ObsMain::TextData();
    textData->name = ConvertString(data->Name);
    textData->text = ConvertString(data->Text);
    textData->font = ConvertString(data->Font);
    textData->color = data->Color;
    textData->size = data->Size;
    textData->bold = data->Bold;
    textData->italic = data->Italic;
    textData->underline = data->Underline;
    textData->strikeout = data->Strikeout;
    textData->opacity = data->Opacity;

    textData->extents = data->Extents;
    textData->extents_cx = data->ExtentsCx;
    textData->extents_cy = data->ExtentsCy;
    textData->extents_wrap = data->ExtentsWrap;

    textData->align = ConvertString(data->Align);
    textData->valign = ConvertString(data->Valign);

    textData->bk_color = data->BkColor;
    textData->bk_opacity = data->BkOpacity;

    textData->outline = data->Outline;
    textData->outline_size = data->OutlineSize;
    textData->outline_color = data->OutlineColor;
    textData->outline_opacity = data->OutlineOpacity;

    textData->scroll_speed = data->ScrollSpeed;

    textData->vertical = data->Vertical;

    textData->gradient = data->Gradient;
    textData->gradient_color = data->GradientColor;
    textData->gradient_opacity = data->GradientOpacity;
    textData->gradient_direction = data->GradientDirection;

    textData->chatlog = data->Chatlog;
    textData->chatlog_lines = data->ChatlogLines;

    ObsMain::Instance()->AddText(textData);
}

String^ ObsWrapper::GetConfigString(String^ section, String^ name)
{
    pin_ptr<const wchar_t> convertSection = PtrToStringChars(section);
    pin_ptr<const wchar_t> convertName = PtrToStringChars(name);

    const char* value = config_get_string(ObsMain::Instance()->basicConfig(), ToUtf8(convertSection).c_str(), ToUtf8(convertName).c_str());

    if (!value)
        return "";
    else
        return gcnew String(FromUtf8(value).c_str());
}

bool ObsWrapper::GetConfigBool(String^ section, String^ name)
{
    pin_ptr<const wchar_t> convertSection = PtrToStringChars(section);
    pin_ptr<const wchar_t> convertName = PtrToStringChars(name);

    bool bRet = config_get_bool(ObsMain::Instance()->basicConfig(), ToUtf8(convertSection).c_str(), ToUtf8(convertName).c_str());
    return bRet;
}

LONG64 ObsWrapper::GetConfigInt(String^ section, String^ name)
{
    pin_ptr<const wchar_t> convertSection = PtrToStringChars(section);
    pin_ptr<const wchar_t> convertName = PtrToStringChars(name);

    LONG64 iRet = config_get_int(ObsMain::Instance()->basicConfig(), ToUtf8(convertSection).c_str(), ToUtf8(convertName).c_str());
    return iRet;
}

ULONG64 ObsWrapper::GetConfigUInt(String^ section, String^ name)
{
    pin_ptr<const wchar_t> convertSection = PtrToStringChars(section);
    pin_ptr<const wchar_t> convertName = PtrToStringChars(name);

    LONG64 iRet = config_get_uint(ObsMain::Instance()->basicConfig(), ToUtf8(convertSection).c_str(), ToUtf8(convertName).c_str());
    return iRet;
}

double ObsWrapper::GetConfigDouble(String^ section, String^ name)
{
    pin_ptr<const wchar_t> convertSection = PtrToStringChars(section);
    pin_ptr<const wchar_t> convertName = PtrToStringChars(name);

    double dRet = config_get_double(ObsMain::Instance()->basicConfig(), ToUtf8(convertSection).c_str(), ToUtf8(convertName).c_str());
    return dRet;
}

void ObsWrapper::SetConfigString(String^ section, String^ name, String^ value)
{
    pin_ptr<const wchar_t> cSection = PtrToStringChars(section);
    pin_ptr<const wchar_t> cName = PtrToStringChars(name);
    pin_ptr<const wchar_t> cValue = PtrToStringChars(value);
    
    config_set_string(ObsMain::Instance()->basicConfig(), ToUtf8(cSection).c_str(), ToUtf8(cName).c_str(), ToUtf8(cValue).c_str());
}

void ObsWrapper::SetConfigBool(String^ section, String^ name, bool value)
{
    pin_ptr<const wchar_t> cSection = PtrToStringChars(section);
    pin_ptr<const wchar_t> cName = PtrToStringChars(name);    

    config_set_bool(ObsMain::Instance()->basicConfig(), ToUtf8(cSection).c_str(), ToUtf8(cName).c_str(), value);
}

void ObsWrapper::SetConfigInt(String^ section, String^ name, LONG64 value)
{
    pin_ptr<const wchar_t> cSection = PtrToStringChars(section);
    pin_ptr<const wchar_t> cName = PtrToStringChars(name);

    config_set_int(ObsMain::Instance()->basicConfig(), ToUtf8(cSection).c_str(), ToUtf8(cName).c_str(), value);
}

void ObsWrapper::SetConfigUInt(String^ section, String^ name, ULONG64 value)
{
    pin_ptr<const wchar_t> cSection = PtrToStringChars(section);
    pin_ptr<const wchar_t> cName = PtrToStringChars(name);

    config_set_uint(ObsMain::Instance()->basicConfig(), ToUtf8(cSection).c_str(), ToUtf8(cName).c_str(), value);
}

void ObsWrapper::SetConfigDouble(String^ section, String^ name, double value)
{
    pin_ptr<const wchar_t> cSection = PtrToStringChars(section);
    pin_ptr<const wchar_t> cName = PtrToStringChars(name);

    config_set_double(ObsMain::Instance()->basicConfig(), ToUtf8(cSection).c_str(), ToUtf8(cName).c_str(), value);
}


void ObsWrapper::CallLoadSceneFinish()
{
    OBSScene scene = m_core->GetCurrentScene();
    OBSSource source = obs_scene_get_source(scene);
    std::wstring ss = FromUtf8(obs_source_get_name(source));

    OnEventCurrentScene(gcnew String(ss.c_str()));
}

void ObsWrapper::CallReloadSceneItemList(void* data)
{
    ObsSceneItemList* itemList = (ObsSceneItemList*)data;
    if (itemList == nullptr) return;

    List<SceneItem^>^ li = gcnew List<SceneItem^>();
    for (size_t i = 0; i < itemList->Count(); i++)
    {
        SceneItem^ it = gcnew SceneItem();
        it->Name = marshal_as<String^>(FromUtf8(ObsSceneItemList::itemName(itemList->Get(i))));
        
        obs_source_t* source = obs_sceneitem_get_source(itemList->Get(i));
        it->Id = marshal_as<String^>(FromUtf8(obs_source_get_id(source)));

        it->bVisible = ObsSceneItemList::itemVisible(itemList->Get(i));
        it->bSelected = ObsSceneItemList::itemSelected(itemList->Get(i));
        li->Add(it);
    }
    this->OnEventReloadSceneItemList(li);
}

//System::Threading::Tasks::Task::Factory->StartNew(gcnew System::Action<Object^>(this, &ObsWrapper::TestCall), (Object^)L"ddd");

void ObsWrapper::CallAddScene(void* data)
{
    std::wstring* name = (std::wstring*)(data);
    
}

std::string ObsWrapper::ConvertString(String^ str)
{
    pin_ptr<const wchar_t> temp = PtrToStringChars(str);
    std::string convert = ToUtf8(temp).c_str();
    return convert;
}
