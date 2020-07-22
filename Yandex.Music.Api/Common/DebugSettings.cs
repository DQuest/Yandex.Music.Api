﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

namespace Yandex.Music.Api.Common
{
    public class DebugSettings
    {
        #region Поля

        private readonly string debugDir;
        private readonly string logFileName;

        #endregion

        #region Свойства

        public string LogFileName { get; }

        public string OutputDir { get; }

        #endregion

        #region Основные функции

        public T Deserialize<T>(string url, string json)
        {
            var errors = new Dictionary<string, List<string>>();

            var settings = new JsonSerializerSettings {
                Error = (sender, args) =>
                {
                    var pos = args.ErrorContext.Error.Message.IndexOf("Path");
                    var error = args.ErrorContext.Error.Message.Substring(0, pos);
                    var path = args.ErrorContext.Error.Message.Substring(pos);

                    if (!errors.ContainsKey(error))
                        errors[error] = new List<string>();

                    errors[error].Add(path);
                    args.ErrorContext.Handled = true;
                },
                MissingMemberHandling = MissingMemberHandling.Error
            };

            var obj = JsonConvert.DeserializeObject<T>(json, settings);

            // Запись ответа от API с ошибкой
            if (errors.Count > 0) {
                if (!Directory.Exists(OutputDir))
                    Directory.CreateDirectory(OutputDir);

                var fileName = Path.Combine(debugDir, $"{url.Trim('/').Replace("/", "-").Replace(":", "-")}.json");

                using (var fs = new FileStream(fileName, FileMode.Create)) {
                    using (var sr = new StreamWriter(fs)) {
                        sr.Write(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.Indented));
                    }
                }

                using (var fs = new FileStream(logFileName, FileMode.Append)) {
                    using (var sr = new StreamWriter(fs)) {
                        sr.WriteLine($"{fileName}:");
                        sr.WriteLine(string.Join("\r\n", errors.Select(p =>
                            $"\t{p.Key}\r\n: {string.Join("\r\n", p.Value.Select(s => $"\t\t{s}"))}")));
                    }
                }
            }

            return obj;
        }

        public DebugSettings(string outputDir, string logFile)
        {
            OutputDir = outputDir;
            LogFileName = logFile;

            debugDir = Path.Combine(AppContext.BaseDirectory, OutputDir);
            logFileName = Path.Combine(debugDir, LogFileName);

            if (File.Exists(logFileName))
                File.Delete(logFileName);
        }

        #endregion Основные функции
    }
}