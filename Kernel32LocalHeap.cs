using System;
using System.Runtime.InteropServices;

namespace Kernel32Lib
{
    public static partial class Kernel32
    {
        /// <summary>
        /// ヒープから、指定されたバイト数を確保します。Win32 のメモリ管理には、ローカルヒープとグローバルヒープを個別に提供する機能がありません。
        /// </summary>
        /// <remarks>この関数は、16 ビット版 Windows との互換性のために提供されています。</remarks>
        /// <param name="uFlags">割り当ての属性</param>
        /// <param name="uBytes">割り当てたいバイト数</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr LocalAlloc([MarshalAs(UnmanagedType.U4)] LocalMemoryFlags uFlags, uint uBytes);

        /// <summary>
        /// 指定されたローカルメモリオブジェクトのサイズまたは属性を変更します。サイズは増減することができます。
        /// </summary>
        /// <remarks>この関数は、16 ビット版 Windows との互換性のために提供されています。</remarks>
        /// <param name="hMem">再確保するローカルメモリオブジェクトのハンドルを指定します。LocalAlloc 関数または LocalReAlloc 関数が返したハンドルを使います。</param>
        /// <param name="uBytes">メモリブロックの新しいサイズをバイト数で指定します。uFlags パラメータに LMEM_MODIFY フラグがセットされている場合、このパラメータは無視されます。</param>
        /// <param name="uFlags">
        /// 再確保するローカルメモリオブジェクトのタイプを表すフラグを指定します。LMEM_MODIFY フラグがセットされている場合、メモリオブジェクトの属性が変更され、uBytes パラメータは無視されます。それ以外の場合、このパラメータはメモリオブジェクトの再確保の方法を制御します。
        /// LMEM_MODIFY フラグは、次のフラグの一方または両方と組み合わせて指定できます。
        /// </param>
        /// <returns>
        /// 関数が成功すると、再確保されたメモリオブジェクトのハンドルが返ります。
        /// 関数が失敗すると、NULL が返ります。拡張エラー情報を取得するには、GetLastError 関数を使います。
        /// </returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr LocalReAlloc(IntPtr hMem, uint uBytes, [MarshalAs(UnmanagedType.U4)] LocalMemoryFlags uFlags);

        /// <summary>
        /// 指定されたローカルメモリオブジェクトを解放し、そのハンドルを無効にします。
        /// <remarks>この関数は、16 ビット版 Windows との互換性のために提供されています。</remarks>
        /// </summary>
        /// <param name="hMem">ローカルメモリオブジェクトのハンドルを指定します。LocalAlloc 関数または LocalReAlloc 関数が返したハンドルを使います。</param>
        /// <returns>
        /// ロックカウントをデクリメントしてもメモリオブジェクトがロックされている場合、0 以外の値が返ります。
        /// 関数が失敗すると、0 が返ります。拡張エラー情報を取得するには、GetLastError 関数を使います。GetLastError 関数が NO_ERROR を返した場合、メモリオブジェクトはロック解除されています。
        /// </returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr LocalFree(IntPtr hMem);

        /// <summary>
        /// ローカルメモリオブジェクトをロックし、オブジェクトのメモリブロックの最初のバイトへのポインタを返します。
        /// </summary>
        /// <param name="hMem">ローカルメモリオブジェクトのハンドルを指定します。LocalAlloc 関数または LocalReAlloc 関数が返したハンドルを使います。</param>
        /// <returns>オブジェクトのメモリブロックの最初のバイトへのポインタ</returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr LocalLock(IntPtr hMem);

        /// <summary>
        /// LMEM_MOVEABLE フラグをセットして確保されたメモリオブジェクトに対応したロックカウントをデクリメント（ 値を 1 減らす）します。この関数は、LMEM_FIXED フラグをセットして確保されたメモリオブジェクトには影響しません。
        /// </summary>
        /// <remarks>この関数は、16 ビット版 Windows との互換性のために提供されています。</remarks>
        /// <param name="hMem">ローカルメモリオブジェクトのハンドルを指定します。LocalAlloc 関数または LocalReAlloc 関数が返したハンドルを使います。</param>
        /// <returns>
        /// ロックカウントをデクリメントしてもメモリオブジェクトがロックされている場合、0 以外の値が返ります。
        /// 関数が失敗すると、0 が返ります。拡張エラー情報を取得するには、GetLastError 関数を使います。GetLastError 関数が NO_ERROR を返した場合、メモリオブジェクトはロック解除されています。
        /// </returns>
        [DllImport("kernel32.dll")]
        [return : MarshalAs(UnmanagedType.Bool)]
        public static extern bool LocalUnlock(IntPtr hMem);

    }

    [Flags]
    public enum LocalMemoryFlags : uint
    {
        /// <summary>
        /// 固定メモリを確保します。メモリオブジェクトへのポインタが戻り値として返ります。
        /// </summary>
        LMEM_FIXED = 0x0000,

        /// <summary>
        /// 移動可能メモリを確保します。Win32 では、物理メモリ内のメモリブロックは移動することはありませんが、既定のヒープ内では移動が可能です。
        /// メモリオブジェクトのハンドルが戻り値として返ります。このハンドルをポインタに変換するには、LocalLock 関数を使います。
        /// このフラグを LMEM_FIXED フラグと組み合わせることはできません。
        /// </summary>
        LMEM_MOVEABLE = 0x0002,

        /// <summary>
        /// 無視されます。このフラグは、16 ビット版 Windows との互換性のために提供されています。
        /// </summary>
        LMEM_NOCOMPACT = 0x0010,

        /// <summary>
        /// 無視されます。このフラグは、16 ビット版 Windows との互換性の目的のために提供されています。
        /// </summary>
        LMEM_NODISCARD = 0x0020,

        /// <summary>
        /// メモリの内容を 0 に初期化します。
        /// </summary>
        LMEM_ZEROINIT = 0x0040,

        LMEM_MODIFY = 0x0080,

        /// <summary>
        /// 無視されます。このフラグは、16 ビット版 Windows との互換性のために提供されています。
        /// Win32 で廃棄可能ブロックを確保するには、LocalDiscard 関数を明示的に呼び出さなければなりません。
        /// このフラグを LMEM_FIXED フラグと組み合わせることはできません。
        /// </summary>
        LMEM_DISCARDABLE = 0x0F00,

        LMEM_VALID_FLAGS = 0x0F72,
        LMEM_INVALID_HANDLE = 0x8000,

        /// <summary>
        /// LMEM_MOVEABLE フラグと LMEM_ZEROINIT フラグを組み合わせたものと同じです。
        /// </summary>
        LHND = (LMEM_MOVEABLE | LMEM_ZEROINIT),

        /// <summary>
        /// LMEM_FIXED フラグと LMEM_ZEROINIT フラグを組み合わせたものと同じです。
        /// </summary>
        LPTR = (LMEM_FIXED | LMEM_ZEROINIT),

        /// <summary>
        /// LMEM_MOVEABLE フラグと同じです。
        /// </summary>
        NONZEROLHND = (LMEM_MOVEABLE),

        /// <summary>
        /// LMEM_FIXED フラグと同じです。
        /// </summary>
        NONZEROLPTR = (LMEM_FIXED)
    }
}
