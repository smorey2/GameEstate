﻿namespace GameEstate.Dev
{
    public static class DevTest
    {
        public static void Test()
        {
            var bms =
@"# The Witcher 3 (script 0.1.3)
# script for QuickBMS http://quickbms.aluigi.org

get EXT extension
if EXT == ""bundle""

    idstring ""POTATO70""
    get BUNDLE_SIZE long
    get DUMMY_SIZE long
    get DATA_OFF long
    math INFO_OFF = 0x20
    math DATA_OFF + INFO_OFF
    goto INFO_OFF
    for INFO_OFF = INFO_OFF < DATA_OFF
        getdstring NAME 0x100
        getdstring HASH 16
        get ZERO long
        get SIZE long
        get ZSIZE long
        get OFFSET long
        get TSTAMP longlong
        getdstring ZERO 16
        get DUMMY long
        get ZIP long
        savepos INFO_OFF

        if ZIP == 0
            log NAME OFFSET SIZE
        elif ZIP == 1
            comtype zlib
            clog NAME OFFSET ZSIZE SIZE
        elif ZIP == 2
            comtype snappy
            clog NAME OFFSET ZSIZE SIZE
        elif ZIP == 3
            comtype doboz
            clog NAME OFFSET ZSIZE SIZE
        else    # 4 and 5
            comtype lz4
            clog NAME OFFSET ZSIZE SIZE
        endif
    next

elif EXT == ""cache""

    getdstring SIGN 4
    goto 0
    if SIGN == ""CS3W""

        idstring ""CS3W""
        get VER long
        get DUMMY longlong
        if VER >= 2
            get INFO_OFF longlong
            get FILES longlong
            get NAMES_OFF longlong
            get NAMES_SIZE longlong
        else
            get INFO_OFF long
            get FILES long
            get NAMES_OFF long
            get NAMES_SIZE long
        endif

        log MEMORY_FILE NAMES_OFF NAMES_SIZE

        goto INFO_OFF
        for i = 0 < FILES
            if VER >= 2
                get NAME_OFF longlong
                get OFFSET longlong
                get SIZE longlong
            else
                get NAME_OFF long
                get OFFSET long
                get SIZE long
            endif
            goto NAME_OFF MEMORY_FILE
            get NAME string MEMORY_FILE
            log NAME OFFSET SIZE
        next i

    else

        for
            get ZSIZE long
            if ZSIZE == 0
                padding 0x1000
                get ZSIZE long
            endif
            get SIZE long
            get TYPE byte
            savepos OFFSET
            clog """" OFFSET ZSIZE SIZE
            math OFFSET + ZSIZE
            goto OFFSET
        next

    endif

else

    print ""Error: unsupported extension %EXT%""
    cleanexit

endif";
            Quickbms.Load(bms, @"D:\Program Files (x86)\GOG Galaxy\Games\The Witcher 3 Wild Hunt GOTY\content\content12", @"C:\T_\Asset");
        }
    }
}