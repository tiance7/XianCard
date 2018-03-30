using FairyGUI;
using System.Collections;
using System.IO;
using UnityEngine;

public class Core : MonoBehaviour
{
    public static Core Inst { get; private set; }

    public static bool IsDebug = true;
    public static bool Editor;
    //public bool UseNet = true;

    private static int _mainThreadID;

    public void Awake()
    {
#if UNITY_EDITOR
        Editor = true;
#endif

        if(Inst == null)
        {
            Inst = this;
        }
        else if (Inst != null)
        {
            Destroy(this.gameObject);   //防止重复创建
            return;
        }

        _mainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
        DontDestroyOnLoad(gameObject);

        //Application.targetFrameRate = GlobalData.FRAME_RATE;
        UIConfig.defaultFont = "Microsoft YaHei";

        //GamePath.Init(Editor);
        //AssetManager.Init(Editor);
        //UIObjectFactory.SetLoaderExtension(typeof(MyGLoader));

        SoundTool soundTool = SoundTool.inst;   //提前初始化

        TemplateManager templateMgr = new TemplateManager(new TemplateList());
        templateMgr.LoadAllXml();

        WinTool.RegisteWindows();                      // 注册窗口

        EnemyActionReg.InitAction();

        CoreDebug();
    }

    public void Update()
    {
        //NetMessage.Update();
        TimeManager.Update(Time.deltaTime);

        //if (Input.GetKeyDown(KeyCode.KeypadMinus))
        //    WindowManager.Open(WindowId.GM_CONSOLE);
    }

    public static bool IsMainThread
    {
        get
        {
            return _mainThreadID == System.Threading.Thread.CurrentThread.ManagedThreadId;
        }
    }

    #region Debug
    public void OnGUI()
    {
#if !UNITY_EDITOR
        GUILayout.Label(_logInfo);
#endif
    }

    private string _logInfo;
    private readonly Queue _logQueue = new Queue(50);
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (_logQueue.Count > 20)
            _logQueue.Dequeue();
        _logInfo = logString;
        string newString = "\n [" + type + "] : " + _logInfo;
        _logQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            _logQueue.Enqueue(newString);
        }
        _logInfo = string.Empty;
        foreach (string mylog in _logQueue)
        {
            _logInfo += mylog;
        }
    }

    void CoreDebug()
    {
//#if UNITY_EDITOR
//        LoadDebugXml();
//#endif

        Application.logMessageReceived += HandleLog;

        //if (Debug.isDebugBuild)
        //{
        //    Debug.Log("====================================================================================");
        //    Debug.Log(string.Format("Application.platform = {0}", Application.platform));
        //    Debug.Log(string.Format("Application.dataPath = {0} , WritePermission: {1}",
        //        Application.dataPath, HasWriteAccessToFolder(Application.dataPath)));
        //    Debug.Log(string.Format("Application.streamingAssetsPath = {0} , WritePermission: {1}",
        //        Application.streamingAssetsPath, HasWriteAccessToFolder(Application.streamingAssetsPath)));
        //    Debug.Log(string.Format("Application.persistentDataPath = {0} , WritePermission: {1}",
        //        Application.persistentDataPath, HasWriteAccessToFolder(Application.persistentDataPath)));
        //    Debug.Log(string.Format("Application.temporaryCachePath = {0} , WritePermission: {1}",
        //        Application.temporaryCachePath, HasWriteAccessToFolder(Application.temporaryCachePath)));
        //    Debug.Log(string.Format("SystemInfo.deviceModel = {0}", SystemInfo.deviceModel));
        //    Debug.Log(string.Format("SystemInfo.graphicsDeviceVersion = {0}", SystemInfo.graphicsDeviceVersion));
        //    Debug.Log("====================================================================================");
        //}
    }

    // 测试有无写权限
    static bool HasWriteAccessToFolder(string folderPath)
    {
        try
        {
            string tmpFilePath = Path.Combine(folderPath, Path.GetRandomFileName());
            using (
                FileStream fs = new FileStream(tmpFilePath, FileMode.CreateNew, FileAccess.ReadWrite,
                    FileShare.ReadWrite))
            {
                StreamWriter writer = new StreamWriter(fs);
                writer.Write("1");
            }
            File.Delete(tmpFilePath);
            return true;
        }
        catch
        {
            return false;
        }
    }

    //#if UNITY_EDITOR
    //    // 编辑器模式下，读取Debug配置文件
    //    void LoadDebugXml()
    //    {
    //        TemplateManager templateMgr = new TemplateManager(new TemplateList());
    //        templateMgr.LoadSingleXml(GamePath.Xml, "Debug_table.xml", OnLoadDebugXml);
    //    }

    //    void OnLoadDebugXml(XmlNode xmlNode)
    //    {
    //        DebugDefineTemplateData.Init(xmlNode);
    //        DebugDefineTemplate _template;
    //        DebugDefineTemplateData.Data.TryGetValue(0, out _template);

    //        LoginModel _loginModel = LoginModel.inst;
    //        _loginModel.defaultserverListIndex = (_template == null) ? 0 : _template.nServerLine;
    //        _loginModel.defaultAccount = (_template == null) ? "" : _template.szAccount;
    //        _loginModel.defaultPassword = (_template == null) ? "" : _template.szPassword;
    //    }
    //#endif
    #endregion
}
