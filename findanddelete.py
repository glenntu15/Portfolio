import os
import sys

#for dirName, subdirList, fileList in os.walk(rootDir):
##
#    for i in subdirList:
#        print(" next level i",i)
#    if (dirName == ".git"):
#        continue
#    for fname in fileList:
#        print('\t%s' % fname)

def ListThisDir(path, conservative):

    dirlist = os.listdir(path)
    for f in dirlist:
       
        fullsubpath = path + "/" + f
        isdir = os.path.isdir(fullsubpath)
        if isdir:
            if (f == ".vs"):
                print(" Need to delete: ",fullsubpath)
                print(" **************")
                print("")
            else:
                #print(" calling with ",fullsubpath)
                ListThisDir(fullsubpath, conservative)
            #print("        dir: ", f)
        
        else:
            #print("        file ",f)
            if (f.find(".circ") > 1):
                if conservative == True:
                    print("  ----> Check this one:   ", fullsubpath)
                else:
                    print(" Deleting: ",fullsubpath)
                    os.remove(fullsubpath)
            if (f.find(".json") > 1):
                if conservative == True:
                    print("  ----> Check this one:   ", fullsubpath)
                else:
                    print(" Deleting: ",fullsubpath)
                    os.remove(fullsubpath)
            if (f.find(".dll") > 1):
                if conservative == True:
                    print("  ----> Check this one:   ", fullsubpath)
                else:
                    print(" Deleting: ",fullsubpath)
                    os.remove(fullsubpath)
            if (f.find(".log") > 1):
                print(" Deleting: ",fullsubpath)
                os.remove(fullsubpath)
            if (f.find(".obj") > 1):
                print(" Deleting: ",fullsubpath)
                os.remove(fullsubpath)
            if (f.find(".tlog") > 1):
                print(" Deleting: ",fullsubpath)
                os.remove(fullsubpath)
            if (f.find(".exe") > 1):
                print(" Deleting: ",fullsubpath)
                os.remove(fullsubpath)
            loc = f.find(".cache")
            if (loc > 1 ):
                diff = len(f)  - loc;
                if (diff == 6):
                    print(" Deleting: ",fullsubpath)
                    os.remove(fullsubpath)

                #print(" file is: ",fullsubpath," diff is ",diff)
            if (f.find(".pdb") > 1):
                print(" Deleting: ",fullsubpath)
                os.remove(fullsubpath)

def ParseArgs(argv):
    nargs = len(argv)
       
    rootdir = sys.argv[1]
    print(" set rootdir to",rootdir)
    conservative = True
    if (nargs > 2):
        for i in range(1,nargs):
            print(" arg: ",sys.argv[i])
            if (argv[i] == "-h"):
                print(" Usage: findtodelete [path] -f  -h")
                print(" path is fully qualified path to search, must be first argument")
                print(" -f means full, ie delete .json and .dll files")
                print(" -h prints this message and does not execute")
                print(" if no arguments are gived the current directory is searched")
                exit()
            if sys.argv[i] == "-f":
                conservative = False;
    
    return[rootdir,conservative]


#------------------------------------------------------------------------------
#  Main program starts here
#------------------------------------------------------------------------------
rootdir = os.getcwd()
print(" scaning: ",rootdir)
nargs = len(sys.argv)
conservative = True
#print(" nargs = ",nargs)
#print("argv ", sys.argv)


if (nargs > 1):
   [rootdir,conservative] =  ParseArgs(sys.argv)

print(" after parseargs dir is ",rootdir)
    
print(" conservative is set to:",conservative)
dirlevel = 0;

ListThisDir(rootdir, conservative)
    
