﻿using System;
using System.Text;
using System.Runtime.InteropServices;

namespace saga.file
{
	// やっつけのINIファイルリーダー
	class ReadIniFile
	{
		// UDPポート
		private int port;
		// メインウィンドウ名 ゆかりorまきorEtc.
		private string mainWindowName;
		// 保存ウィンドウ名 対応用
		private string saveWindowName;
		// UDP通信+保存オプション 保存Path
		private string savePath;
		// 強制上書きフラグ
		private bool forceOverWriteFlag;
		// デバッグ表示フラグ
		private bool debugFlag;

		[DllImport("KERNEL32.DLL")]
		public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

		public ReadIniFile(string fileName, string sectionStr)
		{
			if (fileName == null)
			{
				throw new NullReferenceException("");
			}

			string iniFilePath = AppDomain.CurrentDomain.BaseDirectory + fileName;

			if (!System.IO.File.Exists(iniFilePath))
			{
				throw new System.IO.FileLoadException("IniFileが見つかりません");
			}

			StringBuilder sb = new StringBuilder(1024);

			// キー名"PORT"の読み込み
			GetPrivateProfileString(
				sectionStr, // セクション名
				"PORT", // キー名
				"NONE", // 値が取得できなかった場合
				sb, // 格納先
				Convert.ToUInt32(sb.Capacity), // 格納先のキャパ
				iniFilePath); // iniファイルパス
			if (sb.ToString().Equals("NONE"))
			{
				throw new NullReferenceException("IniFileにキー名「PORT」が設定されていません。");
			}
			else
			{
				this.port = Int32.Parse(sb.ToString());
			}

			// キー名"MAIN_WINDOW_NAME"の読み込み
			GetPrivateProfileString(
				sectionStr, // セクション名
				"MAIN_WINDOW_NAME", // キー名
				"NONE", // 値が取得できなかった場合
				sb, // 格納先
				Convert.ToUInt32(sb.Capacity), // 格納先のキャパ
				iniFilePath); // iniファイルパス
			if (sb.ToString().Equals("NONE"))
			{
				throw new NullReferenceException("IniFileにキー名「MAIN_WINDOW_NAME」が設定されていません。");
			}
			else
			{
				this.mainWindowName = sb.ToString();
			}

			// キー名"SAVE_WINDOW_NAME"の読み込み
			GetPrivateProfileString(
				sectionStr, // セクション名
				"SAVE_WINDOW_NAME", // キー名
				"NONE", // 値が取得できなかった場合
				sb, // 格納先
				Convert.ToUInt32(sb.Capacity), // 格納先のキャパ
				iniFilePath); // iniファイルパス
			if (sb.ToString().Equals("NONE"))
			{
				//throw new NullReferenceException("IniFileにキー名「SAVE_WINDOW_NAME」が設定されていません。");
			}
			else
			{
				this.saveWindowName = sb.ToString();
			}

			// キー名"SAVE_PATH"の読み込み
			GetPrivateProfileString(
				sectionStr, // セクション名
				"SAVE_PATH", // キー名
				"NONE", // 値が取得できなかった場合
				sb, // 格納先
				Convert.ToUInt32(sb.Capacity), // 格納先のキャパ
				iniFilePath); // iniファイルパス
			if (sb.ToString().Equals("NONE"))
			{
				//throw new NullReferenceException("IniFileにキー名「SAVE_PATH」が設定されていません。");
			}
			else
			{
				this.savePath = sb.ToString();
			}

			// キー名"FORCE_OVERWRITE"の読み込み
			GetPrivateProfileString(
				sectionStr, // セクション名
				"FORCE_OVERWRITE", // キー名
				"FALSE", // 値が取得できなかった場合
				sb, // 格納先
				Convert.ToUInt32(sb.Capacity), // 格納先のキャパ
				iniFilePath); // iniファイルパス
			if (sb.ToString().Equals("TRUE"))
			{
				//throw new NullReferenceException("IniFileにキー名「FORCE_OVERWRITE」が設定されていません。");
				this.forceOverWriteFlag = true;
			}
			else
			{
				this.forceOverWriteFlag = false;
			}

			// キー名"DEBUG"の読み込み
			GetPrivateProfileString(
				sectionStr, // セクション名
				"DEBUG", // キー名
				"FALSE", // 値が取得できなかった場合
				sb, // 格納先
				Convert.ToUInt32(sb.Capacity), // 格納先のキャパ
				iniFilePath); // iniファイルパス
			if (sb.ToString().Equals("TRUE"))
			{
				//throw new NullReferenceException("IniFileにキー名「DEBUG」が設定されていません。");
				this.debugFlag = true;
			}
			else
			{
				this.debugFlag = false;
			}
		}

		public int GetPort()
		{
			return this.port;
		}
		public string GetMainWindowName()
		{
			return this.mainWindowName;
		}
		public string GetSaveWindowName()
		{
			return this.saveWindowName;
		}
		public string GetSavePath()
		{
			return this.savePath;
		}
		public bool GetForceOverWriteFlag()
		{
			return this.forceOverWriteFlag;
		}
		public bool GetDebugFlag()
		{
			return this.debugFlag;
		}
	}
}
