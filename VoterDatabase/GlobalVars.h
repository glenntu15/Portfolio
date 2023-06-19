#pragma once
#ifndef _Global_defined
#define _Global_defined
#include <iostream>
//#include <map>
#include <unordered_map>

#define BLKSIZE 4096
#define LRECL 128
#define DRECL 16

typedef struct
{
    unsigned int sosid;
    int blockno;
    int offset;
} dictentry;

struct dictrecord
{
    unsigned int vid;
    unsigned int sosid;
    int blockno;
    int offset;
    dictrecord(int id, dictentry ent)
    {
        vid = id;
        sosid = ent.sosid;
        blockno = ent.blockno;
        offset = ent.offset;
    }
    dictrecord() 
    {
        vid = 0;
        sosid = 0;
        blockno = 0;
        offset = 0;
    }
};
namespace glbl
{
    static int mode = 0;  // 0 is build database 1 is read database

    static char database_name[] = "C:\\tmp\\database.bin";
    static char data_path[] = "C:\\tmp\\";

    static int Block_size = BLKSIZE;
    static int Lrecl = LRECL;

    static std::unordered_map<unsigned int, dictentry> dictionary; 
    //static std::map<unsigned int, dictentry> dictionary;

}
#endif



