using System;
using System.Runtime.InteropServices;

namespace Kernel32Lib
{
    public static partial class Kernel32
    {

        /// <summary>
        /// Generates simple tones on the speaker. The function is synchronous; it performs an alertable wait and does not return control to its caller until the sound finishes.
        /// </summary>
        /// <param name="dwFreq">The frequency of the sound, in hertz. This parameter must be in the range 37 through 32,767 (0x25 through 0x7FFF).</param>
        /// <param name="dwDuration">The duration of the sound, in milliseconds.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Beep(
           UInt32 dwFreq,
           UInt32 dwDuration
        );

        /// <summary>
        /// Captures a stack back trace by walking up the stack and recording the information for each frame.
        /// </summary>
        /// <param name="FramesToSkip">The number of frames to skip from the start of the back trace.</param>
        /// <param name="FramesToCapture">The number of frames to be captured. You can capture up to MAXUSHORT frames.</param>
        /// <param name="BackTrace">An array of pointers captured from the current stack trace.</param>
        /// <param name="BackTraceHash">A value that can be used to organize hash tables. If this parameter is NULL, then no hash value is computed.
        /// This value is calculated based on the values of the pointers returned in the BackTrace array.Two identical stack traces will generate identical hash values.</param>
        /// <returns>The number of captured frames.</returns>
        [DllImport("kernel32.dll")]
        public static extern ushort CaptureStackBackTrace(
            ulong  FramesToSkip,
            ulong FramesToCapture,
            out object BackTrace,
            out ulong BackTraceHash
        );

        /// <summary>
        /// Displays a message box and terminates the application when the message box is closed. If the system is running with a debug version of Kernel32.dll, the message box gives the user the opportunity to terminate the application or to cancel the message box and return to the application that called FatalAppExit.
        /// </summary>
        /// <param name="uAction">This parameter must be zero.</param>
        /// <param name="lpMessageText">The null-terminated string that is displayed in the message box.</param>
        [DllImport("kernel32.dll")]
        public static extern void FatalAppExit(
            uint    uAction,
            out string lpMessageText
        );

        /// <summary>
        /// メッセージ文字列を書式化します（書式を割り当てます）。この関数は、入力としてメッセージ定義を受け取ります。メッセージ定義は、この関数に渡すバッファ経由で渡すことができます。代わりに、ロード済みのモジュール内のメッセージテーブルリソースを使うよう指示することもできます。または、システムのメッセージテーブルリソースからメッセージ定義を検索するよう指示することもできます。この関数は、メッセージ識別子と言語識別子に基づいて、メッセージテーブルリソース内のメッセージ定義を検索します。要求に応じて、埋め込まれた挿入シーケンスを処理し、書式化されたメッセージテキストを出力バッファへコピーします。
        /// </summary>
        /// <param name="dwFlags">書式化処理の方法や、lpSource パラメータの解釈方法を指定します。dwFlags の下位バイト（low-order byte）は、この関数がメッセージ定義テキスト内の改行記号を出力バッファへ反映する方法（2 番目の表）を指定します。また、下位バイトで、書式化後の出力行の最大幅を指定することもできます。</param>
        /// <param name="lpSource">メッセージ定義の位置を指定します。このパラメータの意味は、dwFlags パラメータで指定された値によって異なります。</param>
        /// <param name="dwMessageId">希望のメッセージのメッセージ識別子を指定します。dwFlags パラメータで FORMAT_MESSAGE_FROM_STRING フラグを指定した場合は、dwMessageId パラメータは無視されます。</param>
        /// <param name="dwLanguageId">希望のメッセージの言語識別子を指定します。dwFlags パラメータで FORMAT_MESSAGE_FROM_STRING フラグを指定した場合は、dwLanguageId パラメータは無視されます。このパラメータで LANGID を指定すると、FormatMessage は、LANGID に一致するメッセージだけを返します。LANGID に一致するメッセージが見つからなかった場合、ERROR_RESOURCE_LANG_NOT_FOUND が返ります。このパラメータで 0 を指定すると、FormatMessage は次の順序で、メッセージ指定に使う LANGID を参照します。</param>
        /// <param name="lpBuffer">1 個のバッファへのポインタを指定します。関数から制御が返ると、このバッファに、書式化が終わった、NULL で終わるメッセージが格納されます。dwFlags パラメータで FORMAT_MESSAGE_ALLOCATE_BUFFER フラグを指定した場合、この関数は 関数を使って 1 個のバッファを割り当て、lpBuffer で指定されたアドレスに、そのバッファへのポインタを書き込みます。</param>
        /// <param name="nSize">dwFlags パラメータで FORMAT_MESSAGE_ALLOCATE_BUFFER フラグを指定しなかった場合は、nSize パラメータは、出力バッファに格納できる最大の文字数を TCHAR 単位で指定します。dwFlags パラメータで FORMAT_MESSAGE_ALLOCATE_BUFFER フラグを指定した場合は、出力バッファに割り当てる最小の文字数を TCHAR 単位で指定します。</param>
        /// <param name="Arguments">書式化が終わったメッセージ内で挿入シーケンスとして使われる複数の値からなる配列へのポインタを指定します。書式文字列の %1 は Arguments 配列の最初の要素、、%2 は次の要素を意味します。以下同様です。
        /// 配列の各要素の解釈は、メッセージ定義内の挿入シーケンスに関連付けられている書式化情報に依存します。既定では、各値を、NULL で終わる文字列へのポインタとして扱います。</param>
        /// <returns>関数が成功すると、バッファに格納された TCHAR 単位の文字数が返ります。終端の NULL 文字は、この数に含まれません。関数が失敗すると、0 が返ります。拡張エラー情報を取得するには、GetLastError 関数を使います。</returns>
        [DllImport("kernel32.dll")]
        public static extern UInt32 FormatMessage(
            UInt32   dwFlags,
            object lpSource,
            UInt32   dwMessageId,
            UInt32   dwLanguageId,
            out string lpBuffer,
            UInt32   nSize,
            params string[] Arguments
        );

        /// <summary>
        /// Retrieves the error mode for the current process.
        /// </summary>
        /// <returns>The process error mode. </returns>
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern ProcessErrorMode GetErrorMode();


        /// <summary>
        /// 呼び出し側のスレッドが持つ最新のエラーコードを取得します。エラーコードは、スレッドごとに保持されるため、複数のスレッドが互いの最新のエラーコードを上書きすることはありません。
        /// </summary>
        /// <returns>呼び出し側のスレッドが持つ最新のエラーコードが返ります。他の関数は、SetLastError 関数を呼び出して、内部でこのエラーコードを設定します。</returns>
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern UInt32 GetLastError();

        /// <summary>
        /// Retrieves the error mode for the calling thread.
        /// </summary>
        /// <returns>The process error mode. </returns>
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern ProcessErrorMode GetThreadErrorMode();

        /// <summary>
        /// 指定された種類の重大なエラーが発生したときに、システムに処理を任せるか、呼び出し側のアプリケーションが処理するかを設定します。
        /// </summary>
        /// <param name="uMode">プロセスのエラーモードを指定します。次の値の任意の組み合わせを指定します。</param>
        /// <returns>以前のエラーモードビットフラグが返ります。</returns>
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern ProcessErrorMode SetErrorMode([MarshalAs(UnmanagedType.U4)] ProcessErrorMode uMode);


        /// <summary>
        /// 呼び出し側スレッドが持つ最新のエラーコードを設定します。
        /// </summary>
        /// <param name="dwErrCode">スレッドの最新のエラーコードを指定します。</param>
        [DllImport("kernel32.dll")]
        public static extern void SetLastError(uint dwErrCode);


    }

    [Flags]
    public enum FormatMessageFlag : uint
    {
        FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100,
        FORMAT_MESSAGE_ARGUMENT_ARRAY  = 0x00002000,
        FORMAT_MESSAGE_FROM_HMODULE    = 0x00000800,
        FORMAT_MESSAGE_FROM_STRING     = 0x00000400,
        FORMAT_MESSAGE_FROM_SYSTEM     = 0x00001000,
        FORMAT_MESSAGE_IGNORE_INSERTS  = 0x00000200,
        ZERO                           = 0x00000000,
        FORMAT_MESSAGE_MAX_WIDTH_MASK  = 0x000000FF,
    }

    [Flags]
    public enum ProcessErrorMode : uint
    {
        ZERO                       = 0x0000,
        SEM_FAILCRITICALERRORS     = 0x0001,
        SEM_NOALIGNMENTFAULTEXCEPT = 0x0004,
        SEM_NOGPFAULTERRORBOX      = 0x0002,
        SEM_NOOPENFILEERRORBOX     = 0x8000,
    }
}
