project "obs_opengl"
	kind "SharedLib"
	language "C"
	defines {
		"GLAD_GLAPI_EXPORT_BUILD"
	}
	files{
		"*.h",
		"*.c",
		"glad/include/**.h",
		"glad/src/*.c",
	}

	includedirs{
		"glad/include",
		"../../"
	}
	links{
		"obs"
	}
	
if is_pfm("windows") then
	removefiles{
		"gl-x11.c",
		"glad/src/glad_glx.c",
	}
	links{
		"opengl32"
	}
end