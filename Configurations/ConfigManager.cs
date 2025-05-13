using Plate.Domain.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Plate.Configurations
{
    public static class ConfigManager
    {
        // config.json 파일은 실행파일과 같은 폴더에 위치
        private static readonly string ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

        /// <summary>
        /// config.json 파일에서 Settings 객체를 불러옵니다.
        /// 파일이 없으면 기본 Settings 객체를 반환합니다.
        /// </summary>
        public static SettingsConfig LoadConfig()
        {
            if (!File.Exists(ConfigFilePath))
            {
                return new SettingsConfig();
            }

            try
            {
                string json = File.ReadAllText(ConfigFilePath, Encoding.UTF8);
                var settings = JsonConvert.DeserializeObject<SettingsConfig>(json);
                return settings ?? new SettingsConfig();
            }
            catch (Exception ex)
            {
                throw new Exception("설정 파일을 불러오는 도중 오류가 발생했습니다.", ex);
            }
        }

        /// <summary>
        /// Settings 객체를 config.json 파일로 저장합니다.
        /// </summary>
        public static void SaveConfig(SettingsConfig settings)
        {
            try
            {
                // 가독성을 위해 들여쓰기가 적용된 JSON 문자열 생성
                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(ConfigFilePath, json, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new Exception("설정 파일을 저장하는 도중 오류가 발생했습니다.", ex);
            }
        }
    }
    
}
