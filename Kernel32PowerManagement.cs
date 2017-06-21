using System;
using System.Runtime.InteropServices;

namespace Kernel32Lib
{
    public static partial class Kernel32
    {

        /// <summary>
        /// 指定したデバイスの現在の電源状態を取得します。
        /// </summary>
        /// <param name="hDevice">ファイルやソケットのような、デバイスにあるオブジェクトのハンドル、またはデバイス自体のハンドルを指定します。</param>
        /// <param name="pfOn">電源状態を受け取る変数へのポインタを指定します。デバイスの電源が完全に ON になっていると、変数に TRUE が返ります。デバイスの電源が完全に ON になっていないと、変数に FALSE が返ります。</param>
        /// <returns>関数が成功すると、0 以外の値が返ります。関数が失敗すると、0 が返ります。</returns>
        [DllImport("kernel32.dll")]
        static extern bool GetDevicePowerState(IntPtr hDevice, out bool pfOn);

        /// <summary>
        /// システムの電源状態を取得します。システムが AC 電源と DC 電源のどちらで稼動しているか、現在バッテリが充電されているか、バッテリの残量はどのくらいかなどが確認できます。
        /// </summary>
        /// <param name="lpSystemPowerStatus">電源状態の情報を受け取る、 構造体へのポインタを指定します。</param>
        /// <returns>関数が成功すると、0 以外の値が返ります。関数が失敗すると、0 が返ります。拡張エラー情報を取得するには、 関数を使います｡</returns>
        [DllImport("kernel32.dll")]
        static extern bool GetSystemPowerStatus(out SystemPowerStatus lpSystemPowerStatus);

        /// <summary>
        /// 現在のコンピュータの状態を返します。
        /// </summary>
        /// <returns>システムが自動的に実行状態に戻っている場合にユーザーがアクティブでなければ、TRUE が返ります。その他の場合は、FALSE が返ります。</returns>
        [DllImport("kernel32.dll")]
        static extern bool IsSystemResumeAutomatic();

        /// <summary>
        /// アプリケーションが実行されていることをシステムに通知し、アプリケーションの実行中にシステムが電源のスリープ状態に入るのを防ぎます。
        /// </summary>
        /// <param name="esFlags">スレッドの実行要求を指定します。次のいずれかの値を指定します。</param>
        /// <returns>関数が成功すると、前回のスレッド実行状態が返ります。関数が失敗すると、NULL が返ります。</returns>
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.U4)]
        static extern ExecutionState SetThreadExecutionState([MarshalAs(UnmanagedType.U4)] ExecutionState esFlags);

    }

    /// <summary>
    /// Contains information about the power status of the system.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SystemPowerStatus
    {
        [MarshalAs(UnmanagedType.U1)] ACLineStatus ACLineStatus;
        [MarshalAs(UnmanagedType.U1)] BatteryFlag BatteryFlag;
        byte BatteryLifePercent;
        [MarshalAs(UnmanagedType.U1)] SystemStatusFlag SystemStatusFlag;
        UInt32 BatteryLifeTime;
        UInt32 BatteryFullLifeTime;
    }

    /// <summary>
    /// The AC power status. This member can be one of the following values.
    /// </summary>
    [Flags]
    public enum ACLineStatus : byte
    {
        Offline = 0,
        Online  = 1,
        Unknown = 255,
    }

    /// <summary>
    /// The battery charge status. This member can contain one or more of the following flags.
    /// </summary>
    [Flags]
    public enum BatteryFlag : byte
    {
        High            = 1, // more than 66 percent
        Low             = 2, // less than 33 percent
        Critical        = 4, // less than five percent
        Charging        = 8,
        NoSystemBattery = 128,
        UnknownStatus   = 255, // unable to read the battery flag information
    }

    /// <summary>
    /// The status of battery saver. To participate in energy conservation, avoid resource intensive tasks when battery saver is on. To be notified when this value changes, call the RegisterPowerSettingNotification function with the power setting GUID, GUID_POWER_SAVING_STATUS.
    /// </summary>
    [Flags]
    public enum SystemStatusFlag : byte
    {
        BatterySavorIsOff = 0,
        BatterySavorIsOn = 1,
    }

    [Flags]
    public enum ExecutionState : UInt32
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,

        /// <summary>
        /// ほかの状態フラグがクリアされており、ES_CONTINUOUS を使った次の呼び出しまで設定済みの状態を維持する必要があることをシステムに通知します。
        /// </summary>
        ES_CONTINUOUS = 0x80000000,

        /// <summary>
        /// システムでディスプレイアクティビティとして通常認識されない操作をスレッドが実行していることをシステムに通知します。
        /// </summary>
        ES_DISPLAY_REQUIRED = 0x00000002,

        /// <summary>
        /// システムでアクティビティとして通常認識されない操作をスレッドが実行していることをシステムに通知します。
        /// </summary>
        ES_SYSTEM_REQUIRED = 0x00000001,

        ES_USER_PRESENT      = 0x00000004,
    }

}
