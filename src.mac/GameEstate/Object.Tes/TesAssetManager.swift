//
//  TesAssetManager.swift
//  GameEstate
//
//  Created by Sky Morey on 5/28/18.
//  Copyright © 2018 Sky Morey. All rights reserved.
//

import Foundation
import SceneKit

public enum GameId: Int, CustomStringConvertible {
    // tes
    case morrowind = 0
    case oblivion
    case skyrim, skyrimSE, skyrimVR
    // fallout
    case fallout3
    case falloutNV
    case fallout4, fallout4VR

    public var description: String {
        switch self {
        case .morrowind: return "morrowind"
        case .oblivion: return "oblivion"
        case .skyrim: return "skyrim"
        case .skyrimSE: return "skyrimSE"
        case .skyrimVR: return "skyrimVR"
        case .fallout3: return "fallout3"
        case .falloutNV: return "falloutNV"
        case .fallout4: return "fallout4"
        case .fallout4VR: return "fallout4VR"
        }
    }
    
    init?(_ value: String) {
        switch value.lowercased() {
        case "morrowind": self = .morrowind
        case "oblivion": self = .oblivion
        case "skyrim": self = .skyrim
        case "skyrimse": self = .skyrimSE
        case "skyrimvr": self = .skyrimVR
        case "fallout3": self = .fallout3
        case "falloutnv": self = .falloutNV
        case "fallout4": self = .fallout4
        case "fallout4vr": self = .fallout4VR
        default: return nil
        }
    }
}

public class TesAssetManager: IAssetManager {    
    public func getAssetPack(_ url: URL?) -> IAssetPack? {
        guard let url = url, let scheme = url.scheme else {
            fatalError("url should not be nil")
        }
        debugPrint("\(url)")
        switch scheme {
        case "game":
            let localPath = String(url.path[url.path.index(after: url.path.startIndex)..<url.path.endIndex])
            let game = TesAssetManager.stringToGameId(url.host)
            let filePaths = FileManager.default.getFilePaths(searchPattern: localPath, for: game)
            guard filePaths != nil else {
                fatalError("\(game) not available")
            }
            let pack = TesAssetPack(filePaths!) as IAssetPack
            return pack
        default: return nil
        }
    }
    
    public func getDataPack(_ url: URL?) -> IDataPack? {
        guard let url = url, let scheme = url.scheme else {
            fatalError("url should not be nil")
        }
        debugPrint("\(url)")
        switch scheme {
        case "game":
            let localPath = String(url.path[url.path.index(after: url.path.startIndex)..<url.path.endIndex])
            let game = TesAssetManager.stringToGameId(url.host)
            let filePath = FileManager.default.getFilePath(localPath, for: game)
            let pack = TesDataPack(filePath, for: game) as IDataPack
            return pack
        default: return nil
        }
    }

    public func getCellManager(rootNode: SCNNode, asset: IAssetPack, data: IDataPack, loadBalancer: TemporalLoadBalancer) -> ICellManager? {
        return TesCellManager(rootNode: rootNode, asset: asset as! TesAssetPack, data: data as! TesDataPack, loadBalancer: loadBalancer)
    }

    static func stringToGameId(_ key: String?) -> GameId {
        guard let key = key else {
            return GameId.morrowind
        }
        var newKey = key
        if newKey.starts(with: "#") {
            newKey.remove(at: newKey.startIndex)
        }
        return GameId(newKey)!
    }
}
