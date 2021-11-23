#pragma once
#include <util/c99defs.h>

#include <string>
#include <vector>


std::string GetDefaultVideoSavePath();

std::string GetFileName(const  std::string &file);
std::string GetFilePostfix(const  std::string &file);



//»ñÈ¡ÆÁÄ»³ß´ç
bool GetScreenSize(int *cx,int *cy);

void obs_log(int log_level, const char *msg, va_list args, void *param);
