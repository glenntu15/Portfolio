// VoterDatabase.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#include "GlobalVars.h"
#include "BuildVoterDatabase.h"
#include "ReadVoterDatabase.h"
#include <stdio.h>

int main(int argc, char* argv[])
{
 
    std::ios::sync_with_stdio(false);

    if (argc > 1) {
        std::string arg1 = argv[1];
        if (arg1 == "-r") {
            glbl::mode = 1;
        }
    }


    if (glbl::mode ==0) {
        std::cout << " Running to build database with block size " << BLKSIZE << std::endl;

        BuildVoterDatabase* builder = new BuildVoterDatabase();
        builder->StartBuild();
    }
    else {
        std::cout << " Running to read database" << std::endl;
        ReadVoterDatabase* reader =  new ReadVoterDatabase();
        reader->BeganRead();
    }
    
}

