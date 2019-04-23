using System.Diagnostics;
public class WatchExecuteTime {
    public delegate void WatchEventHandler();

    public static void WatchExecute(WatchEventHandler watchEvent)
    {
        Stopwatch watcher = new Stopwatch();
        watcher.Start();
        watchEvent();
        watcher.Stop();
        
        UnityEngine.Debug.LogFormat("{0}执行时间:{1}毫秒",watchEvent.Method.Name ,watcher.ElapsedMilliseconds);
    }

}
