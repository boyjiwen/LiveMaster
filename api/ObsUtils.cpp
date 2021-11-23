#include "ObsUtils.h"
#include <obs.hpp>
#include <util/platform.h>

void EnumGameWindow(enum_window_callback callback, void * ptr)
{
    obs_properties_t* prop = obs_properties_by_id("game_capture");
    if (prop)
    {
        obs_property_t *property = obs_properties_get(prop, "window");
        if (property) {
            const char        *name = obs_property_name(property);
            obs_property_type type = obs_property_get_type(property);
            if (strcmp(name, "window") == 0 && type == OBS_PROPERTY_LIST)
            {
                size_t  count = obs_property_list_item_count(property);
                //game capture.c:1952 ����˿��ַ�����ǰ�棬�ʴ�1��ʼ
                for (size_t i = 1; i < count; i++)
                {
                    const char *name = obs_property_list_item_name(property, i);
                    const char *window = obs_property_list_item_string(property, i);

                    if(callback)
                        callback(ptr, name, window);
                    blog(LOG_INFO,"GameProcess:%s  %s",name,window);
                }
            }
        }
        obs_properties_destroy(prop);
    }
}


void _enum_window_callback(void* ptr, const char* name, const char* window)
{
    std::vector<WindowInfo>* info = (std::vector<WindowInfo>*)ptr;
    info->push_back(WindowInfo(name,window));
}

std::vector<WindowInfo> GetWindowInfo(bool isGame)
{
    std::vector<WindowInfo> infos;
    if (isGame)
        EnumGameWindow(_enum_window_callback, &infos);
    else
        EnumWindow(_enum_window_callback, &infos);
    return infos;
}

void EnumWindow(enum_window_callback callback, void * ptr)
{
    obs_properties_t* prop = obs_properties_by_id("window_capture");
    if (prop)
    {
        obs_property_t *property = obs_properties_get(prop,"window");

        if (property) {
            const char        *name = obs_property_name(property);
            obs_property_type type = obs_property_get_type(property);
            const char *desc = obs_property_description(property);

            blog(LOG_INFO, "[%s]:%s", name, desc);

            if (strcmp(name, "window") == 0 && type == OBS_PROPERTY_LIST)
            {
                size_t  count = obs_property_list_item_count(property);
                for (size_t i = 0; i < count; i++)
                {
                    const char *name = obs_property_list_item_name(property, i);
                    const char *window = obs_property_list_item_string(property, i);

                    if (callback)
                        callback(ptr, name, window);
                    blog(LOG_INFO, "GameProcess:%s  %s", name, window);
                }
            }
        }
        obs_properties_destroy(prop);
    }
}


void EnumDevice(enum_device_callback callback, void * ptr)
{
    obs_properties_t* prop = obs_properties_by_id("dshow_input");
    if (prop)
    {
        //��Ƶ�豸
        obs_property_t *property = obs_properties_get(prop, "video_device_id");
        if (property) 
        {
                size_t  count = obs_property_list_item_count(property);
                for (size_t i = 0; i < count; i++)
                {
                    const char *name = obs_property_list_item_name(property, i);
                    const char *id = obs_property_list_item_string(property, i);

                    if (callback)
                        callback(ptr, true,name, id);
                    blog(LOG_INFO, "EnumDevice:%s  %s", name, id);
                }
        }

        //��Ƶ�豸
        property = obs_properties_get(prop, "audio_device_id");
        if (property)
        {
            size_t  count = obs_property_list_item_count(property);
            for (size_t i = 0; i < count; i++)
            {
                const char *name = obs_property_list_item_name(property, i);
                const char *id = obs_property_list_item_string(property, i);

                if (callback)
                    callback(ptr, false, name, id);
                blog(LOG_INFO, "EnumDevice:%s  %s", name, id);
            }
        }

        obs_properties_destroy(prop);
    }
}

std::string ToUtf8(const wchar_t* unicode, int len)
{
    if (len <= 0)
        len = wcslen(unicode);

    char* utf8 = nullptr;
    size_t utf8len = os_wcs_to_utf8_ptr(unicode, len, &utf8);
    std::string data(utf8, utf8len);
    bfree(utf8);
    return data;
}

std::wstring FromUtf8(const char* str, int len)
{
    if (len <= 0)
        len = strlen(str);

    wchar_t* uft8 = NULL;
    os_utf8_to_wcs_ptr(str, len, &uft8);

    std::wstring data(uft8, len);

    bfree(uft8);
    return data;
}

std::string GenerateSourceName(const  std::string &base)
{
    std::string name;
    int inc = 0;

    for (;; inc++) {
        name = base;

        if (inc) {
            name += " (";
            name += std::to_string(inc + 1);
            name += ")";
        }

        obs_source_t *source = obs_get_source_by_name(name.c_str());
        if (!source)
            return name;
    }
}


void EnumMonitor(enum_monitor_callback callback, void * ptr)
{
    obs_properties_t* prop = obs_properties_by_id("monitor_capture");
    if (prop)
    {
        obs_property_t *monitors = obs_properties_get(prop, "monitor");

        size_t  count = obs_property_list_item_count(monitors);
        for (size_t i = 0; i < count; i++)
        {
            const char *name = obs_property_list_item_name(monitors, i);
            int id = obs_property_list_item_int(monitors, i);

            if (callback)
                callback(ptr, name, id);
            blog(LOG_INFO, "EnumMonitor:%s  %d", name, id);
        }
        obs_properties_destroy(prop);
    }
}

void EnumMonitor(std::vector<std::wstring>& vMonitors)
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

            vMonitors.push_back(FromUtf8(name));

           /* if (callback)
                callback(ptr, name, id);*/
            blog(LOG_INFO, "EnumMonitor:%s  %d", name, id);
        }
        obs_properties_destroy(prop);
    }
}


std::string ObsErrorToText(int code)
{
    std::string msg;
    switch (code) {
    case OBS_OUTPUT_BAD_PATH:
        msg = ToUtf8(L"��Ч��·����URL����������������ȷ����������Ч�ġ�");
        break;

    case OBS_OUTPUT_CONNECT_FAILED:
        msg = ToUtf8(L"�޷����ӵ�������");
        break;

    case OBS_OUTPUT_INVALID_STREAM:
        msg = ToUtf8(L"�޷�����ָ����Ƶ��������Կ, ����ϸ���������Կ��.  ���������ȷ��, �п��������ӵ�������ʱ��������.");
        break;

    case OBS_OUTPUT_ERROR:
        msg = ToUtf8(L"��ͼ���ӵ�������ʱ��������Ĵ�����ϸ��Ϣ��¼����־�ļ��С�");
        break;

    case OBS_OUTPUT_DISCONNECTED:
        /* doesn't happen if output is set to reconnect.  note that
        * reconnects are handled in the output, not in the UI */
        msg = ToUtf8(L"�Ѵӷ������Ͽ���");
        break;
    case OBS_OUTPUT_UNSUPPORTED:
        msg = ToUtf8(L"�����ʽ��֧�ֻ��߲�֧�ֳ���һ������.�����������, Ȼ�����ԡ�");
        break;
    case OBS_OUTPUT_NO_SPACE:
        msg = ToUtf8(L"���̿ռ䲻�㡣");
        break;
    default:
        msg = ToUtf8(L"����δ֪����");
        break;
    }
    return msg;
}


