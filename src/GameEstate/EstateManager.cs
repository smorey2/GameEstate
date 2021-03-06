﻿using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using static GameEstate.Estate;

namespace GameEstate
{
    public class EstateManager
    {
        public static string DefaultEstateKey = "AC";
        static string[] AllEstateKeys = new[] { "AC", "Arkane", "Aurora", "Cry", "Cyanide", "Origin", "Red", "Rsi", "Tes", "Valve" };

        static EstateManager()
        {
            Bootstrap();
            var assembly = Assembly.GetExecutingAssembly();
            Estate estate;
            foreach (var key in AllEstateKeys)
                using (var r = new StreamReader(assembly.GetManifestResourceStream($"GameEstate.Estates.{key}Estate.json")))
                    Estates.Add((estate = ParseEstate(r.ReadToEnd())).Id, estate);
        }

        /// <summary>
        /// Gets the estates.
        /// </summary>
        /// <value>
        /// The estates.
        /// </value>
        public static IDictionary<string, Estate> Estates { get; } = new Dictionary<string, Estate>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Parses the estate.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">pakFileType</exception>
        /// <exception cref="ArgumentNullException">games</exception>
        public static Estate ParseEstate(string json)
        {
            //var assembly = Assembly.GetExecutingAssembly();
            var options = new JsonDocumentOptions
            {
                CommentHandling = JsonCommentHandling.Skip
            };
            using (var doc = JsonDocument.Parse(json, options))
            {
                var elem = doc.RootElement;
                var fileManager = elem.TryGetProperty("fileManager", out var z) ? FileManager.ParseFileManager(z) : new FileManager();
                var locations = fileManager.Locations;
                return new Estate
                {
                    Id = (elem.TryGetProperty("id", out z) ? z.GetString() : null) ?? throw new ArgumentNullException("id"),
                    Name = (elem.TryGetProperty("name", out z) ? z.GetString() : null) ?? throw new ArgumentNullException("name"),
                    Description = elem.TryGetProperty("description", out z) ? z.GetString() : null,
                    PakFileType = elem.TryGetProperty("pakFileType", out z) ? Type.GetType(z.GetString(), false) ?? throw new ArgumentOutOfRangeException("pakFileType", z.GetString()) : null,
                    PakMulti = elem.TryGetProperty("pakMulti", out z) ? Enum.TryParse<PakMultiType>(z.GetString(), true, out var z1) ? z1 : throw new ArgumentOutOfRangeException("pakMulti", z.GetString()) : PakMultiType.SingleBinary,
                    Pak2FileType = elem.TryGetProperty("pak2FileType", out z) ? Type.GetType(z.GetString(), false) ?? throw new ArgumentOutOfRangeException("pak2FileType", z.GetString()) : null,
                    Pak2Multi = elem.TryGetProperty("pak2Multi", out z) ? Enum.TryParse<PakMultiType>(z.GetString(), true, out var z2) ? z2 : throw new ArgumentOutOfRangeException("pak2Multi", z.GetString()) : PakMultiType.SingleBinary,
                    Games = elem.TryGetProperty("games", out z) ? z.EnumerateObject().ToDictionary(x => x.Name, x => ParseGame(locations, x.Name, x.Value), StringComparer.OrdinalIgnoreCase) : throw new ArgumentNullException("games"),
                    Xforms = elem.TryGetProperty("xforms", out z) ? z.EnumerateObject().ToDictionary(x => x.Name, x => (object)x.Value.GetString(), StringComparer.OrdinalIgnoreCase) : new Dictionary<string, object>(),
                    FileManager = fileManager,
                };
            }
        }

        static EstateGame ParseGame(IDictionary<string, string> locations, string game, JsonElement elem)
        {
            var estate = new EstateGame
            {
                Game = game,
                Name = (elem.TryGetProperty("name", out var z) ? z.GetString() : null) ?? throw new ArgumentNullException("name"),
                Found = locations.ContainsKey(game),
            };
            if (elem.TryGetProperty("pak", out z))
                switch (z.ValueKind)
                {
                    case JsonValueKind.String: estate.DefaultPaks = new[] { new Uri(z.GetString()) }; break;
                    case JsonValueKind.Array: estate.DefaultPaks = z.EnumerateArray().Select(y => new Uri(z.GetString())).ToArray(); break;
                    default: throw new ArgumentOutOfRangeException();
                }
            return estate;
        }

        /// <summary>
        /// Gets the specified estate.
        /// </summary>
        /// <param name="estateName">Name of the estate.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">estateName</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">name</exception>
        public static Estate GetEstate(string estateName) => Estates.TryGetValue(estateName, out var estate) ? estate : throw new ArgumentOutOfRangeException(nameof(estateName), estateName);
    }
}