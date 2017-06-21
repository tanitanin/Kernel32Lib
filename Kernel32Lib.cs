using System;
using System.Runtime.InteropServices;

namespace Kernel32Lib
{
    public static partial class Kernel32
    {
        /// <summary>
        /// 指定された実行可能モジュールを、呼び出し側プロセスのアドレス空間内にマップします。
        /// </summary>
        /// <param name="lpFileName">実行可能モジュール（.DLL または .EXE ファイル）の名前を保持する null で終わる文字列へのポインタを指定します。ここで指定する名前は、モジュールのファイル名であり、モジュール定義（.DEF）ファイルの LIBRARY キーワードで指定されたような、ライブラリモジュールそのものに格納されている名前に関連付けられることはありません。</param>
        /// <returns>
        /// 関数が成功すると、モジュールのハンドルが返ります。
        /// 関数が失敗すると、NULL が返ります。拡張エラー情報を取得するには、 関数を使います。
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LoadLibrary(string lpFileName);

        /// <summary>
        /// 指定された実行可能モジュールを、呼び出し側プロセスのアドレス空間にマップします。実行可能モジュールは、.DLL ファイルまたは .EXE ファイルです。この指定されたモジュールにより、ほかのモジュールがアドレス空間にマップされることがあります。
        /// </summary>
        /// <param name="lpFileName">Windows の実行可能モジュール（.DLL ファイルまたは .EXE ファイル）を示す NULL で終わる文字列へのポインタを指定します。指定する名前は、実行可能モジュールのファイル名です。</param>
        /// <param name="hReservedNull">このパラメータは将来使うために予約されています。NULL を指定してください。</param>
        /// <param name="dwFlags">モジュールをロードするときのアクションを指定します。LoadLibraryFlagsのいずれかの値を指定します。</param>
        /// <returns>
        /// 関数が成功すると、モジュールのハンドルが返ります。
        /// 関数が失敗すると、NULL が返ります。拡張エラー情報を取得するには、 関数を使います。
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, [MarshalAs(UnmanagedType.U4)] LoadLibraryFlags dwFlags);

        /// <summary>
        /// ロード済みのダイナミックリンクライブラリ（DLL）モジュールの参照カウントを 1 つ減らします。参照カウントが 0 になると、モジュールは呼び出し側プロセスのアドレス空間からマップ解除され、そのモジュールのハンドルは無効になります。
        /// </summary>
        /// <param name="hModule">ロード済みの DLL モジュールのハンドルを指定します。LoadLibrary 関数または GetModuleHandle 関数が、このハンドルを返します。</param>
        /// <returns>関数が成功すると、0 以外の値が返ります。関数が失敗すると、0 が返ります。拡張エラー情報を取得するには、 関数を使います。</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// ロード済みのダイナミックリンクライブラリ（DLL）の参照カウントを 1 減らし、ExitThread 関数を呼び出して呼び出し側スレッドを終了します。この関数は制御を返しません。
        /// この関数は、DLL 内で生成され実行されているスレッドに対して、安全なアンロードおよび自分自身の終了の機会を提供します。
        /// </summary>
        /// <remarks>戻り値はありません。この関数は、制御を返しません。無効な hLibModule ハンドルは無視されます。</remarks>
        /// <param name="hModule">この関数を使って参照カウントを 1 減らしたい DLL モジュールのハンドルを指定します。LoadLibrary 関数または GetModuleHandle 関数が、このハンドルを返します。</param>
        /// <param name="dwExitCode">呼び出し側スレッドの終了コードを指定します。</param>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern void FreeLibraryAndExitThread(IntPtr hModule, UInt32 dwExitCode);

        /// <summary>
        /// 呼び出し側プロセスのアドレス空間に該当ファイルがマップされている場合、指定されたモジュール名のモジュールハンドルを返します。
        /// </summary>
        /// <param name="lpFileName">モジュール（.DLL または .EXE ファイル）の名前を保持する、NULL で終わる文字列へのポインタを指定します。拡張子を記述しなかった場合は、既定で「.DLL」が追加されます。文字列の最後に「.」を記述すると、拡張子なしのモジュール名になります。</param>
        /// <returns>
        /// 関数が成功すると、モジュールのハンドルが返ります。
        /// 関数が失敗すると、NULL が返ります。拡張エラー情報を取得するには、 関数を使います。
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpFileName);

        /// <summary>
        /// ダイナミックリンクライブラリ（DLL）が持つ、指定されたエクスポート済み関数のアドレスを取得します。
        /// </summary>
        /// <param name="hModule">希望の関数を保持する DLL モジュールのハンドルを指定します。LoadLibrary 関数または GetModuleHandle 関数が、このハンドルを返します。</param>
        /// <param name="lpProcName">関数名を保持する null で終わる文字列へのポインタを指定します。代わりに、下位ワードに関数の順序値を、上位ワードに 0 を入れた値を指定することもできます。</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.FunctionPtr)]
        static extern Delegate GetProcAddress(IntPtr hModule, string lpProcName);

    }

    [Flags]
    enum LoadLibraryFlags : uint
    {

        DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
        LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
        LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
        LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
        LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
        LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
        LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
        LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
        LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
        LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
    }

}
