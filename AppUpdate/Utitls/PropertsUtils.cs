using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AppUpdate.Utitls
{
    public class PropertsUtils
    {
        private Color backColor = Color.FromArgb(255, 92, 138);
        private string verNo = "当前版本：1.2.0（201903011615）";
        private string updateContent = "";
        private string downloadUrl = "";
        private string backImg = "";
        private string mainApplicationName = "";
        public PropertsUtils()
        {
            this.backColor = Color.FromArgb(int.Parse(GetAppConfig("backColor")));
            this.backImg = GetAppConfig("backImg");
            this.verNo = GetAppConfig("verNo");
            this.updateContent = GetAppConfig("updateContent");
            this.downloadUrl = GetAppConfig("downloadUrl");
            this.mainApplicationName = GetAppConfig("mainApplicationName");
        }
        /// <summary>
        /// 系统背景色
        /// </summary>
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string VerNo
        {
            get
            {
                return verNo;
            }

            set
            {
                verNo = value;
            }
        }
        /// <summary>
        /// 背景图
        /// </summary>
        public string BackImg
        {
            get { return backImg; }
            set { backImg = value; }
        }
        /// <summary>
        /// 更新内容
        /// </summary>
        public string UpdateContent
        {
            get { return updateContent; }
            set { updateContent = value; }
        }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl
        {
            get { return downloadUrl; }
            set { downloadUrl = value; }
        }

        /// <summary>
        /// 根据Key值获取value值
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        private static string GetAppConfig(string strKey)
        {
            string file = System.Windows.Forms.Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == strKey)
                {
                    return config.AppSettings.Settings[strKey].Value.ToString();
                }
            }
            return null;
        }
        /// <summary>
        /// 更新key,values
        /// </summary>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        private static void UpdateAppConfig(string newKey, string newValue)
        {
            string file = System.Windows.Forms.Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            bool exist = false;
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == newKey)
                {
                    exist = true;
                }
            }
            if (exist)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 更新完毕后启动的程序名称
        /// </summary>
        public string MainApplicationName
        {
            get { return mainApplicationName; }
            set { mainApplicationName = value; }
        }
        /// <summary>
        /// 保存配置文件
        /// </summary>
        public void saveConfig()
        {
            UpdateAppConfig("backColor", backColor.ToArgb().ToString());
            UpdateAppConfig("backImg", backImg.ToString());
            UpdateAppConfig("verNo", verNo.ToString());
            UpdateAppConfig("updateContent", updateContent.ToString());
            UpdateAppConfig("downloadUrl", downloadUrl.ToString());
            UpdateAppConfig("mainApplicationName", mainApplicationName.ToString());
        }
    }
}
