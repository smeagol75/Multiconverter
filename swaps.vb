Imports System.IO
Imports Pes2017MultiConverter





Public Class swaps

    Public Shared Function swap32(ByVal value As UInt32) As UInt32
        Dim bytes As Byte() = BitConverter.GetBytes(value)
        Array.Reverse(bytes)
        value = BitConverter.ToUInt32(bytes, 0)
        Return value

    End Function
    Public Shared Function swap16(ByVal value As UInt16) As UInt16
        Dim bytes As Byte() = BitConverter.GetBytes(value)
        Array.Reverse(bytes)
        value = BitConverter.ToUInt16(bytes, 0)
        Return value

    End Function

    Public Shared Function swap32_SignedInt(ByVal value As Integer) As Integer
        Dim bytes As Byte() = BitConverter.GetBytes(value)
        Array.Reverse(bytes)
        value = BitConverter.ToInt32(bytes, 0)
        Return value

    End Function



End Class
