project "glad"
	kind "StaticLib"
	language "C"
	defines {
		"GLAD_GLAPI_EXPORT_BUILD"
	}
	files{
		"src/**.h",
		"include/*.c",
	}

	includedirs{
		"include",
	}
