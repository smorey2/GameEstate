﻿using GameEstate.Core;
using GameEstate.Explorer.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Explorer
{
    public static class StandardExplorerItem
    {
        /// <summary>
        /// Gets the pak files asynchronous.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="pakFile">The pak file.</param>
        /// <returns></returns>
        public static Task<List<ExplorerItemNode>> GetPakFilesAsync(ExplorerManager manager, BinaryPakFile pakFile)
        {
            var pakMultiFile = pakFile as BinaryPakMultiFile;
            var root = new List<ExplorerItemNode>();
            string currentPath = null;
            List<ExplorerItemNode> currentFolder = null;
            foreach (var file in pakMultiFile.Files)
            {
                // folder
                var fileFolder = Path.GetDirectoryName(file.Path);
                if (currentPath != fileFolder)
                {
                    currentPath = fileFolder;
                    currentFolder = root;
                    if (!string.IsNullOrEmpty(fileFolder))
                        foreach (var folder in fileFolder.Split('\\'))
                        {
                            var found = currentFolder.Find(x => x.Name == folder && x.PakFile == null && x.Pak2File == null);
                            if (found != null) currentFolder = found.Items;
                            else
                            {
                                found = new ExplorerItemNode(folder, manager.FolderIcon);
                                currentFolder.Add(found);
                                currentFolder = found.Items;
                            }
                        }
                }
                // file
                var fileName = Path.GetFileName(file.Path);
                var extention = Path.GetExtension(fileName);
                if (extention.Length > 0)
                    extention = extention.Substring(1);
                currentFolder.Add(new ExplorerItemNode(fileName, manager.GetIcon(extention), file) { PakFile = pakFile });
            }
            return Task.FromResult(root);
        }
    }
}