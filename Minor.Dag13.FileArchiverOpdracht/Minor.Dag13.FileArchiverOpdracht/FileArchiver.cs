using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Minor.Dag13.FileArchiverOpdracht {
    public class FileArchiver
    {
        private string _sourcePath;
        private string _archivePath;
        private FileSystemWatcher watcher;


        public FileArchiver(string sourcePath, string archivePath)
        {
            _sourcePath = sourcePath;
            _archivePath = archivePath;
            watcher = new FileSystemWatcher();

            watcher.Path = sourcePath;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime;

            watcher.Created += new FileSystemEventHandler(OnCreated);

            watcher.EnableRaisingEvents = true;
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            var filePath = Path.Combine(_archivePath, e.Name.Split('.')[0] + ".zip");
            File.Move(e.FullPath, filePath);
        }
    }
}