import os
import sys


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
                if conservative == True or nodelete == True:
                    print("  ----> Check this one:   ", fullsubpath)
                else:
                    print(" Deleting: ",fullsubpath)
                    os.remove(fullsubpath)
            if (f.find(".json") > 1):
                if conservative == True or nodelete == True:
                    print("  ----> Check this one:   ", fullsubpath)
                else:
                    print(" Deleting: ",fullsubpath)
                    os.remove(fullsubpath)
            if (f.find(".dll") > 1):
                if conservative == True or nodelete:
                    print("  ----> Check this one:   ", fullsubpath)
                else:
                    print(" Deleting: ",fullsubpath)
                    os.remove(fullsubpath)
            if (f.find(".log") > 1):
                if (nodelete == False):
                    print(" Deleting: ",fullsubpath)
                    os.remove(fullsubpath)
                else:
                    print(" not deleting: ",fullsubpath)
            if (f.find(".obj") > 1):
                if (not nodelete):
                    print(" Deleting: ",fullsubpath)
                    os.remove(fullsubpath)
                else:
                    print(" not deleting: ",fullsubpath)
            if (f.find(".tlog") > 1):
                if (not nodelete):
                    print(" Deleting: ",fullsubpath)
                    os.remove(fullsubpath)
                else:
                    print(" not deleting: ",fullsubpath)
            if (f.find(".exe") > 1):
                print(" Deleting: ",fullsubpath)
                os.remove(fullsubpath)
            loc = f.find(".cache")
            if (loc > 1 ):
                diff = len(f)  - loc
                if (diff == 6):
                    if (not nodelete):
                        print(" Deleting: ",fullsubpath)
                        os.remove(fullsubpath)
                    else:
                     print(" not deleting: ",fullsubpath)

                #print(" file is: ",fullsubpath," diff is ",diff)
            if (f.find(".pdb") > 1):
                if (not nodelete):
                    print(" Deleting: ",fullsubpath)
                    os.remove(fullsubpath)
                else:
                    print(" not deleting: ",fullsubpath)

def ParseArgs(argv):
    nargs = len(argv)
       
    rootdir = os.getcwd()

    print(" nargs = ",nargs)
    conservative =True
    helponly = False
    nodelete = False
    
    for i in range(1,nargs):
        print(" arg: ",sys.argv[i])
        if argv[i][0] != '-':
            rootdir = sys.argv[1]
            print(" set rootdir to",rootdir)
            continue
        elif (argv[i] == "-h"):
            helponly = True
        elif (argv[i] == "-nd" or argv[i] == "-n"):
            nodelete = True
        elif sys.argv[i] == "-f":
            conservative = False
            print(" -f processed, conservative = ",conservative)
        else:
            print("unnown argument ",argv[i])
            
    
    return[rootdir,conservative,nodelete,helponly]


#------------------------------------------------------------------------------
#  Main program starts here
#------------------------------------------------------------------------------
nargs = len(sys.argv)
conservative = True
print(" nargs = ",nargs)
print("argv ", sys.argv)
#set defaults incase no command args need to be processed
helponly = False
rootdir = os.getcwd()
nodelete = False

if (nargs > 1):
   [rootdir,conservative,nodelete,helponly] =  ParseArgs(sys.argv)
   
if (helponly):
    print(" Usage: FindandDelete [path] -f -n -h")
    print(" path is fully qualified path to search, must be first argument")
    print(" -f means full, ie delete .json and .dll files")
    print(" -n no delete is done - just file listing")
    print(" -h prints this message and does not execute")
    print(" if no arguments are gived the current directory is searched")
    exit()

##debug
print(" after parseargs dir is ",rootdir)
print(" scaning: ",rootdir)

print(" conservative is set to:",conservative)
print(" nodelete is set to ",nodelete)
dirlevel = 0;

ListThisDir(rootdir, conservative)
    
