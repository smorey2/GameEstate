using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEstate.Core
{
    public class CorePakMultiFile : IDisposable
    {
        public readonly List<CorePakFile> Packs = new List<CorePakFile>();

        public CorePakMultiFile(string[] filePaths)
        {
            //var files = (filePaths ?? throw new ArgumentNullException(nameof(filePaths)))
            //    .Where(x => Path.GetExtension(x) == ".bsa" || Path.GetExtension(x) == ".ba2");
            //Packs.AddRange(files.Select(x => new CorePakFile(x)));
        }

        public void Dispose() => Close();

        public void Close()
        {
            if (Packs != null)
                foreach (var pack in Packs)
                    pack.Close();
        }
    }
}