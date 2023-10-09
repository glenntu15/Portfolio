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
    
#------------------------------------------------------------------------------
#  Main program starts here
#------------------------------------------------------------------------------

rootDir = os.getcwd()
print(" scaning: ",rootDir)
nargs = len(sys.argv)
conservative = True
#print(" nargs = ",nargs)
#print("argv ", sys.argv)


if (nargs > 1):
    rootDir = sys.argv[1]
if (nargs > 2):
    if sys.argv[2] == "-f":
        conservative = True;
        
print(" conservative is set to:",conservative)
dirlevel = 0;

ListThisDir(rootDir, conservative)
    
