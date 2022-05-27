// dllmain.cpp : Defines the entry point for the DLL application.
#include "dllmain.h"
#include <stdio.h>     // - Just for some ASCII messages
#include "gl/glut.h"   // - An interface and windows 

/*BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}*/
namespace OPenGLDLL {

    extern "C" {
        //#define CLASS_DECLSPEC    __declspec(dllexport)
        __declspec(dllexport) int _stdcall PlotWindow()
        {
            int argc = 1;
            char* argv[1];
            int error = 0;
            argv[0] = "SpectragramDLL";
            glutInit(&argc, argv);

            return error;
        }
    }
}

