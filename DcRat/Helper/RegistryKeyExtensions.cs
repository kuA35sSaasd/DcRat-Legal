﻿using System;
using Microsoft.Win32;

namespace DcRat.Helper
{
    /// <summary>
    /// Provides extensions for registry key and value operations.
    /// </summary>
    public static class RegistryKeyExtensions
    {
        public static string RegistryTypeToString(this RegistryValueKind valueKind, object valueData)
        {
            if (valueData == null)
            {
                return "(value not set)";
            }

            switch (valueKind)
            {
                case RegistryValueKind.Binary:
                    return ((byte[])valueData).Length > 0 ? BitConverter.ToString((byte[])valueData).Replace("-", " ").ToLower() : "(zero-length binary value)";
                case RegistryValueKind.MultiString:
                    return string.Join(" ", (string[])valueData);
                case RegistryValueKind.DWord:   //Convert with hexadecimal before int
                    return $"0x{((uint) ((int) valueData)).ToString("x8")} ({((uint) ((int) valueData)).ToString()})";
                case RegistryValueKind.QWord:
                    return
                        $"0x{((ulong) ((long) valueData)).ToString("x8")} ({((ulong) ((long) valueData)).ToString()})";
                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    return valueData.ToString();
                case RegistryValueKind.Unknown:
                default:
                    return string.Empty;
            }
        }

        public static RegistryKey OpenReadonlySubKeySafe(this RegistryKey key, string name)
        {
            try
            {
                return key.OpenSubKey(name, false);
            }
            catch
            {
                return null;
            }
        }

        public static RegistryKey OpenWritableSubKeySafe(this RegistryKey key, string name)
        {
            try
            {
                return key.OpenSubKey(name, true);
            }
            catch
            {
                return null;
            }
        }

        public static string RegistryTypeToString(this RegistryValueKind valueKind)
        {
            switch (valueKind)
            {
                case RegistryValueKind.Binary:
                    return "REG_BINARY";
                case RegistryValueKind.MultiString:
                    return "REG_MULTI_SZ";
                case RegistryValueKind.DWord:
                    return "REG_DWORD";
                case RegistryValueKind.QWord:
                    return "REG_QWORD";
                case RegistryValueKind.String:
                    return "REG_SZ";
                case RegistryValueKind.ExpandString:
                    return "REG_EXPAND_SZ";
                case RegistryValueKind.Unknown:
                    return "(Unknown)";
                default:
                    return "REG_NONE";
            }
        }
    }
}
