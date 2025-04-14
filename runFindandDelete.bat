echo on
if "%~1"=="" (
    echo Error: No arguments provided.
    echo Usage: run_findanddelete.bat [arguments...]
    pause
    exit /b 1
)

python c:\portfolio\FindandDelete.py %*