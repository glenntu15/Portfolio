#pragma once

namespace OPenGLDLL {

	extern "C" {
#define CLASS_DECLSPEC    __declspec(dllexport)
		__declspec(dllexport) int _stdcall PlotWindow();
	}
}