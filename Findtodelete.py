import os


#for dirName, subdirList, fileList in os.walk(rootDir):
##
#    for i in subdirList:
#        print(" next level i",i)
#    if (dirName == ".git"):
#        continue
#    for fname in fileList:
#        print('\t%s' % fname)

def ListThisDir(path):

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
                ListThisDir(fullsubpath)
            #print("        dir: ", f)
        
        else:
            #print("        file ",f)
            if (f.find(".circ") > 1):
                print("  ----> Check this one:   ", f)
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
            if (f.find(".pdb") > 1):
                print(" Deleting: ",fullsubpath)
                os.remove(fullsubpath)
    
#------------------------------------------------------------------------------
#  Main program starts here
#------------------------------------------------------------------------------

rootDir ="c:/Portfolio"

dirlevel = 0;

ListThisDir(rootDir)
    
