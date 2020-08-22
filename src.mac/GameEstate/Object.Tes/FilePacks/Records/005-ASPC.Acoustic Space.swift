//
//  ASPCRecord.swift
//  GameEstate
//
//  Created by Sky Morey on 5/28/18.
//  Copyright © 2018 Sky Morey. All rights reserved.
//

public class ASPCRecord: Record {
    public override var description: String { return "ASPC: \(EDID)" }
    public var EDID: STRVField = STRVField_empty // Editor ID
    public var CNAME: CREFField! // RGB color
    
    override func createField(_ r: BinaryReader, for format: GameFormatId, type: String, dataSize: Int) -> Bool {
        switch type {
        case "EDID": EDID = r.readSTRV(dataSize)
        case "CNAME": CNAME = r.readT(dataSize)
        default: return false
        }
        return true
    }
}
