﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using knatteligan.Helpers;

namespace knatteligan.Repositories
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected static Repository<T> Repo;

        public abstract IEnumerable<T> GetAll();

        public static string GetFilePath(string fileName)
        {
            var path = Directory.GetCurrentDirectory() + "\\Resources\\XMLData\\";
            Trace.WriteLine(path);
            var combinedPath = Path.Combine(path, fileName);
            return new Uri($"{path}{combinedPath}").LocalPath;
        }

        public void Save<TSubT>(string filePath, List<TSubT> list)
        {
            Serializer<TSubT>.SaveDataToFile(list, filePath);
        }

        public IEnumerable<TSubT> Load<TSubT>(string filePath)
        {
            return Serializer<TSubT>.GetDataFromFile(filePath);
        }
    }
}