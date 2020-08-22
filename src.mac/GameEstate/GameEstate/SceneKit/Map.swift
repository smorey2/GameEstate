//
//  Map.swift
//  GameEstate
//
//  Created by Sky Morey on 5/28/18.
//  Copyright © 2018 Sky Morey. All rights reserved.
//

import Foundation
import CoreData

public enum MapDir: Int {
    case left = 0
    case downLeft = 1
    case downRight = 2
    case right = 3
    case upRight = 4
    case upLeft = 5

    public var reverse: MapDir {
        return MapDir(rawValue: (self.rawValue + 3) % 6)!
    }
}

@objc(Map)
open class Map: NSObject {
    open var width: Int = 0
    open var height: Int = 0
    var nodes = [MapNode]()

    func indexOf(_ x: Int, _ y: Int) -> Int {
        return (y * Int(width)) + x
    }

    func translateIndex(_ index: Int) -> (x: Int, y: Int) {
        let y = index / Int(self.width)
        let x = index - (y * Int(height))
        return (x, y)
    }

    func negate(_ val: UInt64) -> Int {
        return val == 0 ? 1 : 0
    }

    func moveIndex(_ index: Int, dir: MapDir) -> (x: Int, y: Int) {
        let translated = translateIndex(index)
        return movePoint(x: translated.x, y: translated.y, dir: dir)
    }

    /*
    /*
          A_____ B____ X
         /\    /\    /
        /  \  /  \  /
     C /____\/_D__\/ E
       \    /\    /
        \  /  \  /
      Y  \/_F__\/ G

    D -> C is Left
    D -> F is Down Left
    D -> G is Down Right
    D -> E is Right
    D -> B is UpRight
    D -> A is Up Left

    */
*/
    func movePoint(x xIn: Int, y yIn: Int, dir: MapDir) -> (x: Int, y: Int) {
        var x = UInt64(xIn)
        var y = UInt64(yIn)
        switch dir {
        case .upLeft, .left:
            if x == 0 { x = UInt64(width - 1) }
            else { x -= 1 }
        case .downRight, .right:
            if xIn == (width - 1) { x = 0 }
            else { x += 1 }
        default: x = UInt64(xIn)
        }
        switch dir {
        case .downLeft, .downRight:
            if yIn == 0 { y = UInt64(height - 1) }
            else { y -= 1 }
        case .upRight, .upLeft:
            if yIn == (height - 1) { y = 0 }
            else { y += 1 }
        default: y = UInt64(yIn)
        }
//        if x > 65 { x = 65 }
//        if y > 65 { y = 65 }
        return (Int(x), Int(y))
    }
}
