Imports System.IO
Imports System
Imports System.Windows.Forms


Public Class Multiconverter

    Public Shared Sub Unicolor(ByRef stream As MemoryStream)
        Dim num_of_blocks As Integer = stream.Length / 85
        Dim br As New BinaryReader(stream)
        Dim Grabar As New BinaryWriter(stream)
        Dim stream_value As New MemoryStream

        Dim Position As UInt32 = 0
        br.BaseStream.Position = 0
        Dim i As UInt32 = 0
        Dim Aux_32 As UInt32
        For i = 0 To num_of_blocks - 1
            Aux_32 = swaps.swap32(br.ReadUInt32)
            br.BaseStream.Position -= 4
            Grabar.Write(Aux_32)
            br.BaseStream.Position = br.BaseStream.Position + 81
        Next
    End Sub

    Public Shared Sub KitConfig_toConsole(ByRef stream As MemoryStream)

        Dim br As New BinaryReader(stream)
        Dim Grabar As New BinaryWriter(stream)
        br.BaseStream.Position = 0
        Dim Aux_32 As UInt32
        Dim Aux_16 As UInt16
        Dim Aux_byte As Byte


        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Aux_32 = bitworking_Kitconfig_1_toConsole(Aux_32)
        Grabar.Write(Aux_32)
        br.BaseStream.Position += 18
        Aux_16 = br.ReadUInt16
        br.BaseStream.Position -= 2
        Aux_16 = bitworking_Kitconfig_2_toConsole(Aux_16)
        Grabar.Write(Aux_16)
        Aux_32 = br.ReadUInt32
        br.BaseStream.Position -= 4
        Aux_32 = bitworking_Kitconfig_3_toConsole(Aux_32)
        Grabar.Write(Aux_32)
        Aux_32 = br.ReadUInt32
        br.BaseStream.Position -= 4
        Aux_32 = bitworking_Kitconfig_4_toConsole(Aux_32)
        Grabar.Write(Aux_32)
        Aux_32 = br.ReadUInt32
        br.BaseStream.Position -= 4
        Aux_32 = bitworking_Kitconfig_5_toConsole(Aux_32)
        Grabar.Write(Aux_32)

        Aux_byte = br.ReadByte
        br.BaseStream.Position -= 1
        Aux_byte = converter.rotl(Aux_byte, 3)
        Grabar.Write(Aux_byte)

    End Sub
    Public Shared Function bitworking_Kitconfig_1_toConsole(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111111", 2))
        'aux1 = aux1 << 30
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11110000000000000000", 2))
        aux1 = aux1 << 12
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111111000000000000000000000000", 2))
        aux1 = aux1 >> 4
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111111100000000", 2))
        aux1 = aux1 << 4
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111100000000000000000000", 2))
        aux1 = aux1 >> 12
        valuecambiado = (aux1 Or valuecambiado)



        valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function
    Public Shared Function bitworking_Kitconfig_2_toConsole(ByVal value As UInt16) As UInt16

        Dim aux1 As UInt16 = 0
        Dim valuecambiado As UInt16 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1000000000000000", 2))
        aux1 = aux1 >> 15
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111110000000000", 2))
        aux1 = aux1 >> 9
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111100000", 2))
        aux1 = aux1 << 1
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111", 2))
        aux1 = aux1 << 11
        valuecambiado = (aux1 Or valuecambiado)


        valuecambiado = swaps.swap16(valuecambiado)
        Return valuecambiado


    End Function
    Public Shared Function bitworking_Kitconfig_3_toConsole(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111", 2))
        aux1 = aux1 << 27
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111100000", 2))
        aux1 = aux1 << 17
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11110000000000", 2))
        aux1 = aux1 << 8
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111100000000000000", 2))
        aux1 = aux1 >> 1
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111110000000000000000000", 2))
        aux1 = aux1 >> 11
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111000000000000000000000000", 2))
        aux1 = aux1 >> 21
        valuecambiado = (aux1 Or valuecambiado)


        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("100000000000000000000000000000", 2))
        aux1 = aux1 >> 28
        valuecambiado = (aux1 Or valuecambiado)


        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1000000000000000000000000000000", 2))
        aux1 = aux1 >> 28
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("10000000000000000000000000000000", 2))
        aux1 = aux1 >> 31
        valuecambiado = (aux1 Or valuecambiado)


        valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function
    Public Shared Function bitworking_Kitconfig_4_toConsole(ByVal value As UInt32) As UInt32


        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11", 2))
        aux1 = aux1 << 30
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("100", 2))
        aux1 = aux1 << 27
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111111000", 2))
        aux1 = aux1 << 20
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111000000000", 2))
        aux1 = aux1 << 9
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11100000000000000", 2))
        aux1 = aux1 << 1
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111100000000000000000", 2))
        aux1 = aux1 >> 7
        valuecambiado = (aux1 Or valuecambiado)


        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111110000000000000000000000", 2))
        aux1 = aux1 >> 17
        valuecambiado = (aux1 Or valuecambiado)


        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111000000000000000000000000000", 2))
        aux1 = aux1 >> 27
        valuecambiado = (aux1 Or valuecambiado)

        

        valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function
    Public Shared Function bitworking_Kitconfig_5_toConsole(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111", 2))
        aux1 = aux1 << 27
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111100000", 2))
        aux1 = aux1 << 17
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111110000000000", 2))
        aux1 = aux1 << 7
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111000000000000000", 2))
        aux1 = aux1 >> 3
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111100000000000000000000", 2))
        aux1 = aux1 >> 13
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111110000000000000000000000000", 2))
        aux1 = aux1 >> 23
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1000000000000000000000000000000", 2))
        aux1 = aux1 >> 29
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("10000000000000000000000000000000", 2))
        aux1 = aux1 >> 31
        valuecambiado = (aux1 Or valuecambiado)


        valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function

    Public Shared Sub KitConfig_toPc(ByRef stream As MemoryStream)

        Dim br As New BinaryReader(stream)
        Dim Grabar As New BinaryWriter(stream)
        br.BaseStream.Position = 0
        Dim Aux_32 As UInt32
        Dim Aux_16 As UInt16
        Dim Aux_byte As Byte


        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Aux_32 = bitworking_Kitconfig_1_toPc(Aux_32)
        Grabar.Write(Aux_32)
        br.BaseStream.Position += 18
        Aux_16 = swaps.swap16(br.ReadUInt16)
        br.BaseStream.Position -= 2
        Aux_16 = bitworking_Kitconfig_2_toPc(Aux_16)
        Grabar.Write(Aux_16)
        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Aux_32 = bitworking_Kitconfig_3_toPc(Aux_32)
        Grabar.Write(Aux_32)
        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Aux_32 = bitworking_Kitconfig_4_toPc(Aux_32)
        Grabar.Write(Aux_32)
        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Aux_32 = bitworking_Kitconfig_5_toPc(Aux_32)
        Grabar.Write(Aux_32)

        Aux_byte = br.ReadByte
        br.BaseStream.Position -= 1
        Aux_byte = converter.rotr(Aux_byte, 3)
        Grabar.Write(Aux_byte)

    End Sub
    Public Shared Function bitworking_Kitconfig_1_toPc(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111111", 2))
        'aux1 = aux1 << 30
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111100000000", 2))
        aux1 = aux1 << 12
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111111000000000000", 2))
        aux1 = aux1 >> 4
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111111100000000000000000000", 2))
        aux1 = aux1 << 4
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11110000000000000000000000000000", 2))
        aux1 = aux1 >> 12
        valuecambiado = (aux1 Or valuecambiado)



        valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function
    Public Shared Function bitworking_Kitconfig_2_toPc(ByVal value As UInt16) As UInt16

        Dim aux1 As UInt16 = 0
        Dim valuecambiado As UInt16 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1", 2))
        aux1 = aux1 << 15
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111110", 2))
        aux1 = aux1 << 9
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111000000", 2))
        aux1 = aux1 >> 1
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111100000000000", 2))
        aux1 = aux1 >> 11
        valuecambiado = (aux1 Or valuecambiado)


        'valuecambiado = swaps.swap16(valuecambiado)
        Return valuecambiado


    End Function
    Public Shared Function bitworking_Kitconfig_3_toPc(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111000000000000000000000000000", 2))
        aux1 = aux1 >> 27
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111110000000000000000000000", 2))
        aux1 = aux1 >> 17
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111000000000000000000", 2))
        aux1 = aux1 >> 8
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111110000000000000", 2))
        aux1 = aux1 << 1
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111100000000", 2))
        aux1 = aux1 << 11
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111000", 2))
        aux1 = aux1 << 21
        valuecambiado = (aux1 Or valuecambiado)


        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("110", 2))
        aux1 = aux1 << 28
        valuecambiado = (aux1 Or valuecambiado)


        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1", 2))
        aux1 = aux1 << 31
        valuecambiado = (aux1 Or valuecambiado)


        'valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function
    Public Shared Function bitworking_Kitconfig_4_toPc(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11000000000000000000000000000000", 2))
        aux1 = aux1 >> 30
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("100000000000000000000000000000", 2))
        aux1 = aux1 >> 27
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111100000000000000000000000", 2))
        aux1 = aux1 >> 20
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111000000000000000000", 2))
        aux1 = aux1 >> 9
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111000000000000000", 2))
        aux1 = aux1 >> 1
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111110000000000", 2))
        aux1 = aux1 << 7
        valuecambiado = (aux1 Or valuecambiado)


        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111100000", 2))
        aux1 = aux1 << 17
        valuecambiado = (aux1 Or valuecambiado)


        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111", 2))
        aux1 = aux1 << 27
        valuecambiado = (aux1 Or valuecambiado)



        'valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function
    Public Shared Function bitworking_Kitconfig_5_toPc(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111000000000000000000000000000", 2))
        aux1 = aux1 >> 27
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111110000000000000000000000", 2))
        aux1 = aux1 >> 17
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111100000000000000000", 2))
        aux1 = aux1 >> 7
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111000000000000", 2))
        aux1 = aux1 << 3
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111110000000", 2))
        aux1 = aux1 << 13
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111100", 2))
        aux1 = aux1 << 23
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("10", 2))
        aux1 = aux1 << 29
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1", 2))
        aux1 = aux1 << 31
        valuecambiado = (aux1 Or valuecambiado)


        'valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function

    Public Shared Sub BadgeData(ByRef stream As MemoryStream)
        Dim num_of_blocks As Integer = stream.Length / 8
        Dim br As New BinaryReader(stream)
        Dim Grabar As New BinaryWriter(stream)
        Dim stream_value As New MemoryStream

        Dim Position As UInt32 = 0
        br.BaseStream.Position = 0
        Dim i As UInt32 = 0
        Dim Aux_32 As UInt32
        For i = 0 To num_of_blocks - 1
            Aux_32 = swaps.swap32(br.ReadUInt32)
            br.BaseStream.Position -= 4
            Grabar.Write(Aux_32)
            br.BaseStream.Position = br.BaseStream.Position + 4
        Next
    End Sub

    Public Shared Sub Spike(ByRef stream As MemoryStream)
        Dim num_of_blocks As Integer = stream.Length / 8
        Dim br As New BinaryReader(stream)
        Dim Grabar As New BinaryWriter(stream)
        Dim stream_value As New MemoryStream

        Dim Position As UInt32 = 0
        br.BaseStream.Position = 0
        Dim i As UInt32 = 0
        Dim Aux_32 As UInt32
        Dim Aux_16 As UInt16
        For i = 0 To num_of_blocks - 1
            Aux_32 = swaps.swap32(br.ReadUInt32)
            br.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_16 = swaps.swap16(br.ReadUInt16)
            br.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            br.BaseStream.Position = br.BaseStream.Position + 2

        Next
    End Sub

    Public Shared Sub model_ToPc(ByRef stream As MemoryStream)
        Dim br As New BinaryReader(stream)
        Dim Grabar As New BinaryWriter(stream)

        Dim Posicion_cabecera As UInteger
        br.BaseStream.Position = 0
        Dim Aux_32 As UInt32
        Dim Aux_16 As UInt16
        Dim Aux_byte As Byte
        Dim Inicio As UInteger
        Dim Final As UInteger
        Dim i As UInteger



        br.BaseStream.Position += 8

        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Grabar.Write(Aux_32)
        Aux_16 = swaps.swap16(br.ReadUInt16)
        br.BaseStream.Position -= 2
        Grabar.Write(Aux_16)

        Aux_16 = swaps.swap16(br.ReadUInt16)
        br.BaseStream.Position -= 2
        Grabar.Write(Aux_16)




        Aux_byte = br.ReadByte
        If Aux_byte = &H20 Then
            br.BaseStream.Position -= 1
            Dim console_value As Byte = &H9
            Grabar.Write(console_value)
        End If

        br.BaseStream.Position += 7

        Dim Pos_inicial As UInteger = br.BaseStream.Position
        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Grabar.Write(Aux_32)

        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Grabar.Write(Aux_32)
        Dim Num_bloques As UInteger = Aux_32


        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Grabar.Write(Aux_32)

        'bloque 1, por ahora balones.
        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Grabar.Write(Aux_32)

        Inicio = Aux_32 + Pos_inicial

        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Grabar.Write(Aux_32)

        Final = Aux_32 + Pos_inicial

        Posicion_cabecera = br.BaseStream.Position
        br.BaseStream.Position = Inicio


        For i = Inicio To Final - 4
            Aux_32 = swaps.swap32(br.ReadUInt32)
            br.BaseStream.Position -= 4
            Grabar.Write(Aux_32)
            i += 3

        Next

        br.BaseStream.Position = Posicion_cabecera
        'acabado primer bloque

        'segundo bloque

        Inicio = Final
        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Grabar.Write(Aux_32)

        Final = Aux_32 + Pos_inicial

        Posicion_cabecera = br.BaseStream.Position
        br.BaseStream.Position = Inicio

        br.BaseStream.Position += 20

        Dim Inicio_para_posiciones_bloque_32swap As UInteger = swaps.swap32(br.ReadUInt32)

        br.BaseStream.Position = Inicio + Inicio_para_posiciones_bloque_32swap + 12

        Dim Final_bloque_32swap As UInteger = swaps.swap32(br.ReadUInt32) + Inicio

        br.BaseStream.Position = Inicio

        For i = Inicio To Final_bloque_32swap - 4
            Aux_32 = swaps.swap32(br.ReadUInt32)
            br.BaseStream.Position -= 4
            Grabar.Write(Aux_32)
            i += 3

        Next

        For i = br.BaseStream.Position To Final - 2
            Aux_16 = swaps.swap16(br.ReadUInt16)
            br.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            i += 1

        Next
        'copiar a partir de aqui , porque hasta aquí está terminado.

    End Sub

    Public Shared Sub model_ToConsole(ByRef stream As MemoryStream)

        Dim br As New BinaryReader(stream)
        Dim Grabar As New BinaryWriter(stream)

        Dim Posicion_cabecera As UInteger
        br.BaseStream.Position = 0
        Dim Aux_32 As UInt32
        Dim Aux_16 As UInt16
        Dim Aux_byte As Byte
        Dim Inicio As UInteger
        Dim Final As UInteger
        Dim i As UInteger



        br.BaseStream.Position += 8

        Aux_32 = swaps.swap32(br.ReadUInt32)
        br.BaseStream.Position -= 4
        Grabar.Write(Aux_32)
        Aux_16 = swaps.swap16(br.ReadUInt16)
        br.BaseStream.Position -= 2
        Grabar.Write(Aux_16)

        Aux_16 = swaps.swap16(br.ReadUInt16)
        br.BaseStream.Position -= 2
        Grabar.Write(Aux_16)




        Aux_byte = br.ReadByte
        If Aux_byte = &H9 Then
            br.BaseStream.Position -= 1
            Dim console_value As Byte = &H30
            Grabar.Write(console_value)
        End If

        br.BaseStream.Position += 7

        Dim Pos_inicial As UInteger = br.BaseStream.Position
        br.BaseStream.Position += 4

        Dim Num_bloques As UInteger = br.BaseStream.Position
        br.BaseStream.Position += 8

        'bloque 1, por ahora balones.

        Inicio = br.ReadUInt32 + Pos_inicial

        Final = br.ReadUInt32 + Pos_inicial

        Posicion_cabecera = br.BaseStream.Position
        br.BaseStream.Position = Inicio
        br.BaseStream.Position += 4
        Dim numero_de_bloques_bloq1 As UInteger = br.ReadUInt32

        If numero_de_bloques_bloq1 = 1 Then
            br.BaseStream.Position = Inicio
            For i = Inicio To Final - 4
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                i += 3

            Next
        Else
            Dim posicion_para_sumar_el_principio_bloque As UInteger = br.BaseStream.Position
            br.BaseStream.Position += 4
            Dim comienzo_bloque1_del_bloque1 = br.ReadUInt32 + posicion_para_sumar_el_principio_bloque
            br.BaseStream.Position = comienzo_bloque1_del_bloque1 - 4
            Dim Numero_de_bloques_del_ultimo_bloque_del_bloque1 = br.ReadUInt32
            br.BaseStream.Position -= 12

            Dim comienzo_ultimobloque_del_bloque1 = br.ReadUInt32 + posicion_para_sumar_el_principio_bloque
            Dim final_cabecera_ultimo_bloque_bloque1 As UInteger = comienzo_ultimobloque_del_bloque1 + 20
            While final_cabecera_ultimo_bloque_bloque1 Mod 16 <> 0
                final_cabecera_ultimo_bloque_bloque1 += 4
            End While

            Dim comienzo_bloque_16_bloque1 As UInteger = final_cabecera_ultimo_bloque_bloque1 + (Numero_de_bloques_del_ultimo_bloque_del_bloque1 * 48)

            br.BaseStream.Position = Inicio
            For i = Inicio To comienzo_bloque_16_bloque1 - 4
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                i += 3

            Next
            br.BaseStream.Position = comienzo_bloque_16_bloque1

            For i = comienzo_bloque_16_bloque1 To Final - 2
                Aux_16 = swaps.swap16(br.ReadUInt16)
                br.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                i += 1
            Next

        End If




        br.BaseStream.Position = Posicion_cabecera
        'acabado primer bloque

        'segundo bloque

        Inicio = Final
        Final = br.ReadUInt32 + Pos_inicial

        Posicion_cabecera = br.BaseStream.Position
        br.BaseStream.Position = Inicio

        br.BaseStream.Position += 4
        Dim numero_de_bloques_largos As UInteger = br.ReadUInt32
        br.BaseStream.Position += 8
        Dim posicion_para_leer_comienzo_bloque32_primero As UInteger = br.ReadUInt32 + Inicio + 36
        br.BaseStream.Position -= 12
        Dim posicion_cabecera_bloques_largos As UInteger = br.BaseStream.Position
        br.BaseStream.Position = posicion_para_leer_comienzo_bloque32_primero
        Dim comienzo_primer_bloque32 As UInteger = br.ReadUInt32 + Inicio
        br.BaseStream.Position = posicion_cabecera_bloques_largos

        If numero_de_bloques_largos = 0 Then
            br.BaseStream.Position = Inicio
            For i = Inicio To Final - 4
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                i += 3

            Next
        Else


            For i = 0 To numero_de_bloques_largos - 1
                br.BaseStream.Position += 8
                Dim posicion_para_leer_comienzo_bloque32 As UInteger = br.ReadUInt32 + Inicio + 36
                Dim posicion_para_leer_final_bloque32 As UInteger = br.ReadUInt32 + Inicio + 12
                posicion_cabecera_bloques_largos = br.BaseStream.Position

                br.BaseStream.Position = posicion_para_leer_comienzo_bloque32
                Dim comienzo_bloque32 As UInteger = br.ReadUInt32 + Inicio
                Dim pos_leer_BLOQUE_PS3 As UInt32

                ' If i = 0 Or i = 3 Or i = 1 Then
                Select Case numero_de_bloques_largos
                    Case 1
                        pos_leer_BLOQUE_PS3 = posicion_para_leer_comienzo_bloque32 + 60
                        If Form1.CheckBox_Oral.Checked = True Then
                            pos_leer_BLOQUE_PS3 = posicion_para_leer_comienzo_bloque32 + 100
                        End If
                    Case 2
                        Select Case i
                            Case 0
                                pos_leer_BLOQUE_PS3 = posicion_para_leer_comienzo_bloque32 + 60
                            Case 1
                                pos_leer_BLOQUE_PS3 = posicion_para_leer_comienzo_bloque32 + 100
                        End Select
                    Case 4
                        Select Case i
                            Case 0
                                pos_leer_BLOQUE_PS3 = posicion_para_leer_comienzo_bloque32 + 120
                            Case 1
                                pos_leer_BLOQUE_PS3 = posicion_para_leer_comienzo_bloque32 + 40
                            Case 2
                                pos_leer_BLOQUE_PS3 = posicion_para_leer_comienzo_bloque32 + 40
                            Case 3
                                pos_leer_BLOQUE_PS3 = posicion_para_leer_comienzo_bloque32 + 100
                        End Select

                End Select

                br.BaseStream.Position = pos_leer_BLOQUE_PS3
                Dim comienzo_bloque_ps3 As UInt32 = br.ReadUInt32 + Inicio
                br.BaseStream.Position += 16
                If Form1.CheckBox_Oral.Checked = True Then
                    br.BaseStream.Position += 12
                End If
                Dim final_bloque_ps3 As UInt32 = br.ReadUInt32 + Inicio

                br.BaseStream.Position = comienzo_bloque_ps3
                For j = comienzo_bloque_ps3 To final_bloque_ps3 - 4
                    Aux_32 = swaps.swap32(br.ReadUInt32)
                    br.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                    j += 3

                Next


                '   End If

                br.BaseStream.Position = posicion_para_leer_final_bloque32
                Dim final_bloque32 As UInteger = br.ReadUInt32 + Inicio

                br.BaseStream.Position = comienzo_bloque32

                For j = comienzo_bloque32 To final_bloque32 - 4
                    Aux_32 = swaps.swap32(br.ReadUInt32)
                    br.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                    j += 3

                Next

                Dim comienzo_bloque_16 As UInteger = final_bloque32

                br.BaseStream.Position = posicion_cabecera_bloques_largos
                br.BaseStream.Position += 12

                Dim posicion_para_leer_final_bloque16 As Integer = br.ReadUInt32 + Inicio + 36


                br.BaseStream.Position = posicion_para_leer_final_bloque16
                Dim final_bloque_16 As UInteger = br.ReadUInt32 + Inicio


                br.BaseStream.Position = comienzo_bloque_16

                If i = numero_de_bloques_largos - 1 Then
                    final_bloque_16 = Final
                End If

                For k = br.BaseStream.Position To final_bloque_16 - 2
                    Aux_16 = swaps.swap16(br.ReadUInt16)
                    br.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                    k += 1

                Next

                br.BaseStream.Position = posicion_cabecera_bloques_largos + 4


            Next
            br.BaseStream.Position = Inicio

            For i = Inicio To comienzo_primer_bloque32 - 4
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                i += 3

            Next

        End If


        'tercer bloque el de nombres

        br.BaseStream.Position = Posicion_cabecera
        Inicio = Final
        Final = br.ReadUInt32 + Pos_inicial
        Posicion_cabecera = br.BaseStream.Position

        br.BaseStream.Position = Inicio
        br.BaseStream.Position += 4
        Dim Num_Nombres As UInteger = br.ReadUInt32
        Dim espacio_a_bloquenombres As UInteger = br.ReadUInt32
        br.BaseStream.Position -= 4
        Dim Pos_ini_bloq_nom_desde_aqui = br.BaseStream.Position + espacio_a_bloquenombres
        br.BaseStream.Position = Pos_ini_bloq_nom_desde_aqui
        Dim comienzo_nombres As UInteger = (Num_Nombres * 8) + Inicio + 12
        br.BaseStream.Position = Inicio
        If Num_Nombres = 0 Then

            For i = 0 To 2
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
            Next

        Else
            For i = Inicio To comienzo_nombres - 4
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                i += 3
            Next
        End If

        'acabado bloque de nombres

        'bloque 4
        br.BaseStream.Position = Posicion_cabecera
        Inicio = Final
        Final = br.ReadUInt32 + Pos_inicial
        Posicion_cabecera = br.BaseStream.Position
        br.BaseStream.Position = Inicio
        For i = Inicio To Final - 4

            Aux_32 = swaps.swap32(br.ReadUInt32)
            br.BaseStream.Position -= 4
            Grabar.Write(Aux_32)
            i += 3
        Next
        'acabado bloque 4


        'bloque 5
        br.BaseStream.Position = Posicion_cabecera
        Inicio = Final
        Final = br.ReadUInt32 + Pos_inicial
        Posicion_cabecera = br.BaseStream.Position
        br.BaseStream.Position = Inicio + 4
        Dim Num_bloques_bloque5 As UInteger = br.ReadUInt32

        Dim Buscar_final_bloque5 As UInteger = br.ReadUInt32
        br.BaseStream.Position -= 4
        Dim Pos_Final_bloque1_debloque5 As UInteger = br.BaseStream.Position + Buscar_final_bloque5
        br.BaseStream.Position = Pos_Final_bloque1_debloque5
        Dim Final_bloque1_debloque5 As UInteger = br.ReadUInt32 + Inicio
        br.BaseStream.Position = Inicio

        If Final - Inicio = 12 Then
            Final_bloque1_debloque5 = Final
        End If


        For i = Inicio To Final_bloque1_debloque5 - 4

            Aux_32 = swaps.swap32(br.ReadUInt32)
            br.BaseStream.Position -= 4
            Grabar.Write(Aux_32)
            i += 3
        Next

        br.BaseStream.Position = Final_bloque1_debloque5

        If Final_bloque1_debloque5 < Final And Final - Inicio > 12 Then



            While br.BaseStream.Position <> Final
                Dim Inicio_del_minibloque As UInteger = br.BaseStream.Position
                br.BaseStream.Position += 4
                Dim num_veces_internas_minibloque As UInteger = br.ReadUInt32
                Dim final_minibloque As UInteger

                If num_veces_internas_minibloque = 0 Then
                    final_minibloque = Inicio_del_minibloque + 12
                    br.BaseStream.Position = Inicio_del_minibloque

                    For i = Inicio_del_minibloque To final_minibloque - 4
                        Aux_32 = swaps.swap32(br.ReadUInt32)
                        br.BaseStream.Position -= 4
                        Grabar.Write(Aux_32)
                        i += 3
                    Next
                ElseIf num_veces_internas_minibloque = 1 Then
                    br.BaseStream.Position += 4
                    Dim comienzo_bloque_del_primer_minibloque As UInteger = br.ReadUInt32
                    br.BaseStream.Position -= 4

                    br.BaseStream.Position = Inicio_del_minibloque
                    For i = Inicio_del_minibloque To comienzo_bloque_del_primer_minibloque - 4
                        Aux_32 = swaps.swap32(br.ReadUInt32)
                        br.BaseStream.Position -= 4
                        Grabar.Write(Aux_32)
                        i += 3
                    Next

                    Dim comprobacion_final_nombre As UInteger = 1

                    While comprobacion_final_nombre <> 0
                        comprobacion_final_nombre = br.ReadUInt32
                    End While

                    Do While comprobacion_final_nombre = &HC
                        Aux_32 = swaps.swap32(br.ReadUInt32)
                        br.BaseStream.Position -= 4
                        Grabar.Write(Aux_32)
                        comprobacion_final_nombre = br.ReadUInt32
                        br.BaseStream.Position -= 4
                    Loop


                Else
                    br.BaseStream.Position += 4
                    Dim comienzo_bloque_del_primer_minibloque As UInteger = br.ReadUInt32
                    br.BaseStream.Position -= 4
                    For i = 0 To num_veces_internas_minibloque - 1
                        Dim comienzo_bloque_del_minibloque As UInteger = br.ReadUInt32
                        Dim posicion_enMinicabecera As UInteger = br.BaseStream.Position
                        br.BaseStream.Position = comienzo_bloque_del_minibloque + Inicio_del_minibloque

                        Dim Check As UInteger = br.ReadUInt32
                        br.BaseStream.Position -= 4

                        If Check = 1 Then
                            Aux_32 = swaps.swap32(br.ReadUInt32)
                            br.BaseStream.Position -= 4
                            Grabar.Write(Aux_32)

                            Aux_32 = swaps.swap32(br.ReadUInt32)
                            br.BaseStream.Position -= 4
                            Grabar.Write(Aux_32)
                        Else
                            Aux_32 = swaps.swap32(br.ReadUInt32)
                            br.BaseStream.Position -= 4
                            Grabar.Write(Aux_32)
                            Aux_32 = swaps.swap32(br.ReadUInt32)
                            br.BaseStream.Position -= 4
                            Grabar.Write(Aux_32)
                            Aux_32 = swaps.swap32(br.ReadUInt32)
                            br.BaseStream.Position -= 4
                            Grabar.Write(Aux_32)


                        End If

                        If br.BaseStream.Position = Final Then
                            final_minibloque = Final
                        End If

                        If i <> num_veces_internas_minibloque - 1 Then
                            br.BaseStream.Position = posicion_enMinicabecera
                        End If

                    Next
                    Dim final_temporal As UInteger = br.BaseStream.Position
                    br.BaseStream.Position = Inicio_del_minibloque

                    For i = Inicio_del_minibloque To (Inicio_del_minibloque + comienzo_bloque_del_primer_minibloque) - 4
                        Aux_32 = swaps.swap32(br.ReadUInt32)
                        br.BaseStream.Position -= 4
                        Grabar.Write(Aux_32)
                        i += 3

                    Next
                    br.BaseStream.Position = final_temporal
                    While final_temporal <> &HC
                        final_temporal = br.ReadUInt32()
                    End While
                    If final_temporal = &HC Then
                        br.BaseStream.Position -= 4
                    End If

                End If

                If final_minibloque = Final Then
                    br.BaseStream.Position = Final
                End If

            End While



        End If



        'acabado bloque 5

        'bloque 6
        br.BaseStream.Position = Posicion_cabecera
        Inicio = Final
        Final = br.ReadUInt32 + Pos_inicial
        Posicion_cabecera = br.BaseStream.Position
        br.BaseStream.Position = Inicio
        br.BaseStream.Position += 4
        Num_Nombres = br.ReadUInt32
        espacio_a_bloquenombres = br.ReadUInt32
        br.BaseStream.Position -= 4
        Pos_ini_bloq_nom_desde_aqui = br.BaseStream.Position + espacio_a_bloquenombres
        br.BaseStream.Position = Pos_ini_bloq_nom_desde_aqui
        comienzo_nombres = br.ReadUInt32 + Inicio
        br.BaseStream.Position = Inicio

        If Num_Nombres = 0 Then

            For i = 0 To 2
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
            Next

        Else
            For i = Inicio To comienzo_nombres - 4
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                i += 3
            Next
        End If


        'acabado bloque 6


        '7 bloque el de nombres

        br.BaseStream.Position = Posicion_cabecera
        Inicio = Final
        Final = br.ReadUInt32 + Pos_inicial
        Posicion_cabecera = br.BaseStream.Position
        br.BaseStream.Position = Inicio
        br.BaseStream.Position += 4
        Num_Nombres = br.ReadUInt32
        br.BaseStream.Position += 4
        comienzo_nombres = br.ReadUInt32 + Inicio
        br.BaseStream.Position = Inicio

        If Num_Nombres = 0 Then

            For i = 0 To 2
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
            Next

        Else
            For i = Inicio To comienzo_nombres - 4
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                i += 3
            Next
        End If
        'acabado bloque de nombres 7



        'bloque 8
        br.BaseStream.Position = Posicion_cabecera
        Dim final_bloque_10 As UInteger = Final
        Inicio = Final
        Final = stream.Length

        Posicion_cabecera = br.BaseStream.Position
        br.BaseStream.Position = Inicio
        For i = Inicio To Final - 4

            Aux_32 = swaps.swap32(br.ReadUInt32)
            br.BaseStream.Position -= 4
            Grabar.Write(Aux_32)
            i += 3
        Next
        'acabado bloque 8






        'bloque 9

        br.BaseStream.Position = Posicion_cabecera
        Inicio = br.ReadUInt32 + Pos_inicial
        Final = br.ReadUInt32 + Pos_inicial
        Posicion_cabecera = br.BaseStream.Position
        br.BaseStream.Position = Inicio
        For i = Inicio To Final - 4

            Aux_32 = swaps.swap32(br.ReadUInt32)
            br.BaseStream.Position -= 4
            Grabar.Write(Aux_32)
            i += 3
        Next

        'acabado bloque 9

        'bloque 10

        br.BaseStream.Position = Posicion_cabecera
        Inicio = Final
        Final = br.ReadUInt32 + Pos_inicial
        Posicion_cabecera = br.BaseStream.Position
        br.BaseStream.Position = Inicio
        br.BaseStream.Position += 4
        Num_Nombres = br.ReadUInt32
        br.BaseStream.Position += 4
        comienzo_nombres = br.ReadUInt32 + Inicio
        br.BaseStream.Position = Inicio

        If Num_Nombres = 0 Then

            For i = 0 To 2
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
            Next

        Else
            For i = Inicio To comienzo_nombres - 4
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                i += 3
            Next
        End If

        'acabado bloque 10
        'bloque 11
        Dim Check_name As UInteger

        br.BaseStream.Position = Posicion_cabecera
        Inicio = Final
        Final = br.ReadUInt32 + Pos_inicial
        br.BaseStream.Position = Inicio
        Check_name = br.ReadUInt32
        br.BaseStream.Position -= 4

        If Check_name = 12 Then


            For i = Inicio To final_bloque_10 - 4

                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                i += 3
            Next

        Else
            br.BaseStream.Position = br.BaseStream.Position + Check_name
            Dim Check_exists_bloque As UInteger = br.BaseStream.Position
            Dim Inicio_nom As UInteger
            Dim Final_nom As UInteger
            Inicio_nom = br.ReadUInt32 + Inicio
            Final_nom = br.ReadUInt32 + Inicio

            br.BaseStream.Position = Inicio

            If Check_exists_bloque <> final_bloque_10 Then



                For i = Inicio To Inicio_nom - 4

                    Aux_32 = swaps.swap32(br.ReadUInt32)
                    br.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                    i += 3
                Next
                br.BaseStream.Position = Final_nom

                For i = Final_nom To final_bloque_10 - 4

                    Aux_32 = swaps.swap32(br.ReadUInt32)
                    br.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                    i += 3
                Next
            Else
                For i = Inicio To final_bloque_10 - 4

                    Aux_32 = swaps.swap32(br.ReadUInt32)
                    br.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                    i += 3
                Next

            End If



        End If

        'acabado bloque 11
        'cabecera
        br.BaseStream.Position = Pos_inicial
        For i = Pos_inicial To Posicion_cabecera - 4
            Aux_32 = swaps.swap32(br.ReadUInt32)
            br.BaseStream.Position -= 4
            Grabar.Write(Aux_32)
            i += 3
        Next


    End Sub

    Public Shared Sub Dat(ByRef stream As MemoryStream)

        Dim br As New BinaryReader(stream)
        Dim Grabar As New BinaryWriter(stream)
        Dim stream_value As New MemoryStream

        Dim Position As UInt32 = 0
        br.BaseStream.Position = 4
        Dim i As UInt32 = 0
        Dim Aux_32 As UInt32
        For i = 4 To stream.Length - 4
            Aux_32 = swaps.swap32(br.ReadUInt32)
            br.BaseStream.Position -= 4
            Grabar.Write(Aux_32)
            i += 3
        Next
    End Sub

    Public Shared Sub audiarea_to_console(ByRef stream As MemoryStream)

        Dim br As New BinaryReader(stream)
        Dim Grabar As New BinaryWriter(stream)
        Dim stream_value As New MemoryStream


        br.BaseStream.Position = 8
        Dim i As UInt32 = 0
        Dim Inicio As UInt32 = br.ReadUInt32
        Dim Final_Cabecera As UInt32 = Inicio
        Dim num_veces As UInt32 = ((Final_Cabecera - 8) / 4) - 2
        Dim Final As UInt32 = 0

        Do While Final = 0
            Final = br.ReadUInt32
            If Final = 0 Then
                num_veces -= 1
            End If

        Loop


        Dim Posicion_cabecera As UInt32 = br.BaseStream.Position

        Dim Aux_32 As UInt32
        For i = 0 To num_veces
            br.BaseStream.Position = Inicio + 4
            If br.BaseStream.Position <> Final And Final <> 0 Then

                For j = br.BaseStream.Position To Final - 4
                    Aux_32 = swaps.swap32(br.ReadUInt32)
                    br.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                    j += 3
                Next
            End If
            If Final <> 0 Then
                Inicio = Final
            End If

            br.BaseStream.Position = Posicion_cabecera
            Final = br.ReadUInt32
            Posicion_cabecera = br.BaseStream.Position

        Next
        
        br.BaseStream.Position = Posicion_cabecera
        If br.BaseStream.Position <> stream.Length Then
            br.BaseStream.Position = Inicio + 4
            For j = br.BaseStream.Position To stream.Length - 4
                Aux_32 = swaps.swap32(br.ReadUInt32)
                br.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                j += 3
            Next
        End If
        br.BaseStream.Position = 0
        For j = 0 To Final_Cabecera - 4
            Aux_32 = swaps.swap32(br.ReadUInt32)
            br.BaseStream.Position -= 4
            Grabar.Write(Aux_32)
            j += 3
        Next


    End Sub

    Public Shared Sub teamcolor(ByRef stream As MemoryStream)
        Dim num_of_blocks As Integer = stream.Length / 16
        Dim br As New BinaryReader(stream)
        Dim Grabar As New BinaryWriter(stream)
        Dim stream_value As New MemoryStream


        br.BaseStream.Position = 0
        Dim i As UInt32 = 0

        Dim Aux_16 As UInt16
        For i = 0 To num_of_blocks - 1

            Aux_16 = swaps.swap16(br.ReadUInt16)
            br.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            br.BaseStream.Position = br.BaseStream.Position + 14

        Next
    End Sub


End Class
