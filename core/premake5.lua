project "obs"
	kind "SharedLib"
	language "C"
	
	files{
		"*.h",
		"*.c",
		"graphics/*.h",
		"graphics/*.c",
		"graphics/libnsgif/*.h",
		"graphics/libnsgif/*.c",
	}
	defines {
		"NOMINMAX",
		"PTW32_STATIC_LIB",
	}
	includedirs{
		"./",
		"../3dparty/ffmpeg/include",
		"../3dparty/zlib",
		"../3dparty/win_pthreads",
		"../3dparty/jansson",
	}
	
	links{
		"zlib",
		"jansson",
		"w32-pthreads",
		"avcodec",
		"avformat",
		"avutil",
		"swresample",
		"swscale",
	}
	
if is_pfm("windows") then

	files{
		"audio-monitoring/win32/*.h",
		"audio-monitoring/win32/*.c",
		"callback/*.h",
		"callback/*.c",
		"media-io/*.h",
		"media-io/*.c",
		"util/*.h",
		"util/*.c",
		"util/windows/*.h",
		"util/windows/*.c",
		"util/vc/*.h",
	}
	
	removefiles{
		"obs-nix.c",
		"obs-cocoa.c",
		"util/pipe-posix.c",
		"util/platform-nix.c",
		"util/platform-nix-dbus.c",
		"util/threading-posix.*",
	}
	links{
		"Winmm",
		"Psapi",
	}
	
	linkoptions{
		"/SAFESEH:NO",
	}
end