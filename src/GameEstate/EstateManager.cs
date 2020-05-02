using GameEstate.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static GameEstate.Estate;

namespace GameEstate
{
    public class EstateManager
    {
        static EstateManager()
        {
            _ = UnsafeUtils.Platform;
            var assembly = Assembly.GetExecutingAssembly();
            Estate estate;
            foreach (var key in new[] { "Cry", "Red", "Rsi", "Tes", "U9", "UO" })
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
            var assembly = Assembly.GetExecutingAssembly();
            var p = JObject.Parse(json);
            var fileManager = p["fileManager"] != null ? FileManager.ParseFileManager((JObject)p["fileManager"]) : new FileManager();
            var locations = fileManager.Locations;
            return new Estate
            {
                Id = (string)p["id"] ?? throw new ArgumentNullException("id"),
                Name = (string)p["name"] ?? throw new ArgumentNullException("name"),
                Description = (string)p["description"] ?? null,
                PakFileType = p["pakFileType"] != null ? assembly.GetType((string)p["pakFileType"], false) ?? throw new ArgumentOutOfRangeException("pakFileType", (string)p["pakFileType"]) : null,
                PakMulti = p["pakMulti"] != null ? Enum.TryParse<PakMultiType>((string)p["pakMulti"], true, out var z1) ? z1 : throw new ArgumentOutOfRangeException("pakMulti", (string)p["pakMulti"]) : PakMultiType.SingleBinary,
                DatFileType = p["datFileType"] != null ? assembly.GetType((string)p["datFileType"], false) ?? throw new ArgumentOutOfRangeException("datFileType", (string)p["datFileType"]) : null,
                DatMulti = p["datMulti"] != null ? Enum.TryParse<DatMultiType>((string)p["datMulti"], true, out var z2) ? z2 : throw new ArgumentOutOfRangeException("datMulti", (string)p["datMulti"]) : DatMultiType.SingleBinary,
                Games = p["games"] != null ? p["games"].Cast<JProperty>().ToDictionary(x => x.Name, x => ParseGame(locations, x.Name, x.Value), StringComparer.OrdinalIgnoreCase) : throw new ArgumentNullException("games"),
                Xforms = p["xforms"] != null ? p["xforms"].Cast<JProperty>().ToDictionary(x => x.Name, x => (object)(string)x.Value, StringComparer.OrdinalIgnoreCase) : new Dictionary<string, object>(),
                FileManager = fileManager,
            };
        }

        static EstateGame ParseGame(IDictionary<string, string> locations, string game, JToken p) =>
            new EstateGame
            {
                Game = game,
                Name = (string)p["name"] ?? throw new ArgumentNullException("name"),
                DefaultPak = p["pak"] != null ? new Uri((string)p["pak"]) : null,
                DefaultDat = p["dat"] != null ? new Uri((string)p["dat"]) : null,
                Found = locations.ContainsKey(game),
            };

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