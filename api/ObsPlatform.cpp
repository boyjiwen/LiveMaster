#include "ObsPlatform.h"
#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <shellapi.h>
#include <shlobj.h>
#include <Dwmapi.h>
#include <mmdeviceapi.h>
#include <audiopolicy.h>
#include <util/platform.h>
#include <obs.h>

std::string GetDefaultVideoSavePath()
{
    wchar_t path_utf16[MAX_PATH];
    char    path_utf8[MAX_PATH] = {};

    SHGetFolderPathW(NULL, CSIDL_MYVIDEO, NULL, SHGFP_TYPE_CURRENT,
        path_utf16);

    os_wcs_to_utf8(path_utf16, wcslen(path_utf16), path_utf8, MAX_PATH);
    return std::string(path_utf8);
}


std::string GetFileName(const  std::string &file)
{
#ifdef WIN32
    auto find = file.rfind('\\');
#else
    auto find = file.rfind('/');
#endif
    if (find != std::string::npos)
    {
        return file.substr(find + 1);
    }
    return file;
}


std::string GetFilePostfix(const  std::string &file)
{
    auto find = file.rfind('.');

    if (find != std::string::npos)
    {
        return file.substr(find + 1);
    }
    return file;
}



bool GetScreenSize(int *cx, int *cy)
{
    *  cx = GetSystemMetrics(SM_CXSCREEN);
    *  cy = GetSystemMetrics(SM_CYSCREEN);
    return true;
}

void obs_log(int log_level, const char *msg, va_list args, void *param)
{
    char bla[4096];
    int len = vsnprintf(bla, 4095, msg, args);
    wchar_t* str;
    os_utf8_to_wcs_ptr(bla, len, &str);

    OutputDebugStringW(str);
    OutputDebugStringW(L"\n");
    bfree(str);

    if (log_level < LOG_WARNING)
        __debugbreak();

    UNUSED_PARAMETER(param);
}
