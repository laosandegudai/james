﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using WinFred.HelperClasses;
using WinFred.Search;

namespace WinFred
{
    public class SearchResult : IComparable<SearchResult>
    {
        public string Id { get; set; }
        public int Priority { get; set; }
        public string Path { get; set; }
        public string Filename => Path.Substring(Path.LastIndexOf('\\') + 1);
        public ImageSource Icon => (Config.GetInstance().DisplayFileIcons)? GeneralHelper.GetIcon(Path): null;
        public int CompareTo(SearchResult other) => Priority - other.Priority;

        public void Open()
        {
            Process.Start(Path);
            SearchEngine.GetInstance().IncrementPriority(this);
        }

        public void OpenFolder()
        {
            if (Directory.Exists(Path))
            {
                Open();
            }
            else
            {
                Process.Start(Path.Substring(0, Path.LastIndexOf('\\')));
                SearchEngine.GetInstance().IncrementPriority(this);
            }
        }
    }
}