using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Text;

namespace System.Performance.Extensions {
    //public static class StringExtension {
    //    static MethodInfo wstrcopyMethodInfo;

    //    public static StringExtension() {
    //        Type stringType = typeof(String);
    //        wstrcopyMethodInfo = stringType.GetMethod("wstrcpy", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
    //        //string.wstrcpy
    //    }

    //    public unsafe static string[] Split(this string splitString, char delimiter) {
    //        return Split(splitString, new String(&delimiter));
    //    }

    //    public static string[] Split(this string splitString, string delimiter) {
    //        return FastSplit()
    //    }

    //    private static char getFirstChar(this char splitString) {
    //        return '\n';
    //    }

    //    public unsafe static string[] FastSplit(
    //      int[] sepList,
    //      int[] lengthList,
    //      int numReplaces,
    //      int count) {
    //        int length1 = numReplaces < count ? numReplaces + 1 : count;
    //        string[] strArray1 = new string[length1];
    //        int startIndex = 0;
    //        int length2 = 0;
    //        for (int index = 0; index < numReplaces && startIndex < this.Length; ++index) {
    //            if (sepList[index] - startIndex > 0)
    //                strArray1[length2++] = this.Substring(startIndex, sepList[index] - startIndex);
    //            startIndex = sepList[index] + (lengthList == null ? 1 : lengthList[index]);
    //            if (length2 == count - 1) {
    //                while (index < numReplaces - 1 && startIndex == sepList[++index])
    //                    startIndex += lengthList == null ? 1 : lengthList[index];
    //                break;
    //            }
    //        }
    //        if (startIndex < this.Length)
    //            strArray1[length2++] = this.Substring(startIndex);
    //        string[] strArray2 = strArray1;
    //        if (length2 != length1) {
    //            strArray2 = new string[length2];
    //            for (int index = 0; index < length2; ++index)
    //                strArray2[index] = strArray1[index];
    //        }
    //        return strArray2;
    //    }

    //    [SecurityCritical]
    //    private unsafe string InternalSubString(int startIndex, int length) {
    //        string str = string.FastAllocateString(length);
    //        fixed (char* dmem = &str.m_firstChar)
    //        fixed (char* chPtr = &this.m_firstChar)
    //            wstrcpy(dmem, chPtr + startIndex, length);
    //        return str;
    //    }

    //    [SecurityCritical]
    //    internal static unsafe void wstrcpy(char* dmem, char* smem, int charCount) {
    //        wstrcopyMethodInfo.Invoke(dmem, chPtr + startIndex, length);
    //    }
    //}
}
