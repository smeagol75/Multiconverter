Imports System.IO
Imports System
Imports System.Windows.Forms



Public Class Of_converter



    Public Shared Sub X360_toPc(ByRef Stream_file As FileStream)


        Dim Memory_stream As New MemoryStream
        Stream_file.Position = 0
        Stream_file.CopyTo(Memory_stream)


        Dim Leer As New BinaryReader(Memory_stream)
        Leer.ReadBytes(Memory_stream.Length)
        Stream_file.Close()

        Dim Grabar As New BinaryWriter(Memory_stream)
        Dim Aux_byte As Byte
        Dim Aux_16 As UInt16
        Dim Aux_32 As UInt32

        'Cabecera
        Leer.BaseStream.Position = 384
        Dim Check_type As Integer = Leer.ReadByte
        Leer.BaseStream.Position -= 1


        'de consola a pc

        If Check_type = 0 Then

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Leer.BaseStream.Position = Leer.BaseStream.Position + Aux_32

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Leer.BaseStream.Position += 4

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

           Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

           Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


          Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Leer.BaseStream.Position += 4

           Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


           Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


           Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Dim Number_of_players As Integer = Aux_16
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 2
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 4
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 2
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 2
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Aux_16 = converter.Reverse_int16(Aux_16)
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)

            'Empieza Player database


            For i = 0 To 24999
                Dim Check_04 As UInt16
                Dim Check_04_2 As UInt16

                Check_04 = Leer.ReadUInt16()
                Leer.BaseStream.Position += 2
                Check_04_2 = Leer.ReadUInt16()
                Leer.BaseStream.Position -= 6

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)

                ' If Check_04 = &H400 And Check_04_2 = &H80 Then
                'Leer.BaseStream.Position -= 2
                'Dim Corrected_04 As UInt16 = &H8004
                ' Grabar.Write(Corrected_04)
                'End If


                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 4

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_2_toPc(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_2_toPc(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_2_toPc(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_3_toPc(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Aux_32 = bitworking_Player32_OptionFile_1_toPc(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Aux_32 = bitworking_Player32_OptionFile_2_toPc(Aux_32)
                Grabar.Write(Aux_32)

                'Cambio players editados
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)


                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)


                Leer.BaseStream.Position += 62


            Next

            'Acabados jugadores 
            'Player Assignament

            For j = 0 To 9999

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)

                For i = 0 To 4 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.Reverse_byte(Aux_byte)
                    Grabar.Write(Aux_byte)
                Next
                Aux_16 = swaps.swap16(Leer.ReadUInt16)
                Leer.BaseStream.Position -= 2
                Aux_16 = converter.rotl_16(Aux_16, 6)
                Grabar.Write(swaps.swap16(Aux_16))

                For i = 0 To 2 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.Reverse_byte(Aux_byte)
                    Grabar.Write(Aux_byte)
                Next
                For i = 0 To 3 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotr(Aux_byte, 1)
                    Grabar.Write(Aux_byte)
                Next


                Aux_32 = swaps.swap32(Leer.ReadUInt32)
                Leer.BaseStream.Position -= 4
                Aux_32 = bitworking_PlayerAssig_toPc(Aux_32)
                Grabar.Write(Aux_32)

                For i = 0 To 4 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotr(Aux_byte, 2)
                    Grabar.Write(Aux_byte)
                Next
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.rotr(Aux_byte, 2)
                Grabar.Write(Aux_byte)

                For i = 0 To 54 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotr(Aux_byte, 4)
                    Grabar.Write(Aux_byte)
                Next
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.rotl(Aux_byte, 1)
                Grabar.Write(Aux_byte)
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.rotl(Aux_byte, 1)
                Grabar.Write(Aux_byte)

                Aux_16 = swaps.swap16(Leer.ReadUInt16)
                Leer.BaseStream.Position -= 2
                Aux_16 = converter.rotr_16(Aux_16, 4)
                Grabar.Write(Aux_16)


                For i = 0 To 3 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotr(Aux_byte, 4)
                    Grabar.Write(Aux_byte)
                Next
                Aux_16 = swaps.swap16(Leer.ReadUInt16)
                Leer.BaseStream.Position -= 2
                Aux_16 = converter.rotl_16(Aux_16, 1)
                Grabar.Write(swaps.swap16(Aux_16))
                Leer.BaseStream.Position += 1


            Next

            'fin player assignament
            'Equipos



            For i = 0 To 849

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 2
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)


                For j = 0 To 26
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)

                Next

                Leer.BaseStream.Position += 196


            Next

            'Fin Equipos 

            'Ligas
            For j = 0 To 59


                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 4

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Aux_16 = bitworking_competition_01_OptionFile_toPc(Aux_16)
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Aux_16 = bitworking_competition_02_OptionFile_toPc(Aux_16)
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)





                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)
                Leer.BaseStream.Position += 1

                Leer.BaseStream.Position += 76
            Next


            'Fin(Ligas)
            'Estadios
            For j = 0 To 99
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Leer.BaseStream.Position += 126

            Next


            'Fin(Estadios)


            'No se muy bien que es

            For j = 0 To 2499
                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                Leer.BaseStream.Position += 84


            Next



            'Fin No se muy bien que es

            'Traspasos

            For j = 0 To 849
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Leer.BaseStream.Position += 2

                For k = 0 To 31
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)

                Next
                Leer.BaseStream.Position += 32



            Next




            'Fin Traspasos

            'Composicion competiciones
            For j = 0 To 545

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

            Next
            'Fin Composicion competiciones
            'Bloques 502

            For j = 0 To 849
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 4

                For k = 0 To 9
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                For k = 0 To 21
                    Aux_16 = swaps.swap16(Leer.ReadUInt16())
                    Leer.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                Next

                Leer.BaseStream.Position += 24

                For k = 0 To 9
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                For k = 0 To 21
                    Aux_16 = swaps.swap16(Leer.ReadUInt16())
                    Leer.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                Next

                Leer.BaseStream.Position += 24

                For k = 0 To 9
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                For k = 0 To 21
                    Aux_16 = swaps.swap16(Leer.ReadUInt16())
                    Leer.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                Next

                Leer.BaseStream.Position += 24

                For k = 0 To 30
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                Leer.BaseStream.Position += 48


            Next
            'Fin Bloques 502

        ElseIf Check_type <> 0 Then



            Aux_32 = Leer.ReadUInt32()
            Leer.BaseStream.Position -= 4
            Grabar.Write(swaps.swap32(Aux_32))

            Leer.BaseStream.Position = Leer.BaseStream.Position + Aux_32

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Leer.BaseStream.Position += 4

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Leer.BaseStream.Position += 4


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_16 = Leer.ReadUInt16()
            Dim Number_of_players As Integer = Aux_16
            Leer.BaseStream.Position -= 2
            Grabar.Write(swaps.swap16(Aux_16))
            Leer.BaseStream.Position += 2
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 4
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 2
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 2
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Aux_16 = converter.Reverse_int16(Aux_16)
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)

            'Empieza Player database


            For i = 0 To 24999
                Dim Check_04 As UInt16
                Dim Check_04_2 As UInt16
                Leer.BaseStream.Position += 2
                Check_04 = Leer.ReadUInt16()
                Leer.BaseStream.Position += 2
                Check_04_2 = Leer.ReadUInt16()
                Leer.BaseStream.Position -= 8

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)

                'If Check_04 = &H4 And Check_04_2 = &H8004 Then
                'Leer.BaseStream.Position -= 4
                'Dim Corrected_04 As UInt16 = &H8000
                'Grabar.Write(swaps.swap16(Corrected_04))
                ' Leer.BaseStream.Position += 2
                'End If

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 4

                Aux_32 = Leer.ReadUInt32()
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_2_toConsole(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = Leer.ReadUInt32()
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_2_toConsole(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = Leer.ReadUInt32()
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_2_toConsole(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = Leer.ReadUInt32()
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_3_toConsole(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = Leer.ReadUInt32()
                Leer.BaseStream.Position -= 4
                Aux_32 = bitworking_Player32_OptionFile_1_toConsole(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = Leer.ReadUInt32()
                Leer.BaseStream.Position -= 4
                Aux_32 = bitworking_Player32_OptionFile_2_toConsole(Aux_32)
                Grabar.Write(Aux_32)

                'Cambio players editados
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)


                Leer.BaseStream.Position += 62


            Next

            'Acabados jugadores 
            'Player assignament
            For j = 0 To 9999

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)

                For i = 0 To 4 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.Reverse_byte(Aux_byte)
                    Grabar.Write(Aux_byte)
                Next
                Aux_16 = swaps.swap16(Leer.ReadUInt16)
                Leer.BaseStream.Position -= 2
                Aux_16 = converter.rotr_16(Aux_16, 6)
                Grabar.Write(swaps.swap16(Aux_16))

                For i = 0 To 2 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.Reverse_byte(Aux_byte)
                    Grabar.Write(Aux_byte)
                Next
                For i = 0 To 3 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotl(Aux_byte, 1)
                    Grabar.Write(Aux_byte)
                Next


                Aux_32 = Leer.ReadUInt32
                Leer.BaseStream.Position -= 4
                Aux_32 = bitworking_PlayerAssig_toConsole(Aux_32)
                Grabar.Write(swaps.swap32(Aux_32))

                For i = 0 To 4 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotl(Aux_byte, 2)
                    Grabar.Write(Aux_byte)
                Next
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.rotl(Aux_byte, 2)
                Grabar.Write(Aux_byte)

                For i = 0 To 54 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotl(Aux_byte, 4)
                    Grabar.Write(Aux_byte)
                Next
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.rotr(Aux_byte, 1)
                Grabar.Write(Aux_byte)
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.rotr(Aux_byte, 1)
                Grabar.Write(Aux_byte)

                Aux_16 = swaps.swap16(Leer.ReadUInt16)
                Leer.BaseStream.Position -= 2
                Aux_16 = converter.rotl_16(Aux_16, 4)
                Grabar.Write(Aux_16)


                For i = 0 To 3 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotl(Aux_byte, 4)
                    Grabar.Write(Aux_byte)
                Next
                Aux_16 = swaps.swap16(Leer.ReadUInt16)
                Leer.BaseStream.Position -= 2
                Aux_16 = converter.rotr_16(Aux_16, 1)
                Grabar.Write(swaps.swap16(Aux_16))
                Leer.BaseStream.Position += 1


            Next

            'fin player assignament
            'Equipos

            For i = 0 To 849

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 2
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                For j = 0 To 26
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)

                Next

                Leer.BaseStream.Position += 196


            Next

            'Fin Equipos 

            'Ligas
            For j = 0 To 59


                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 4

                Aux_16 = Leer.ReadUInt16()
                Leer.BaseStream.Position -= 2
                Aux_16 = bitworking_competition_01_OptionFile_toConsole(Aux_16)
                Grabar.Write(Aux_16)
                Aux_16 = Leer.ReadUInt16()
                Aux_16 = bitworking_competition_02_OptionFile_toConsole(Aux_16)
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)



                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)
                Leer.BaseStream.Position += 1

                Leer.BaseStream.Position += 76
            Next


            'Fin(Ligas)
            'Estadios
            For j = 0 To 99
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Leer.BaseStream.Position += 126

            Next


            'Fin(Estadios)


            'No se muy bien que es

            For j = 0 To 2499
                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                Leer.BaseStream.Position += 84


            Next



            'Fin No se muy bien que es

            'Traspasos

            For j = 0 To 849
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Leer.BaseStream.Position += 2

                For k = 0 To 31
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)

                Next
                Leer.BaseStream.Position += 32



            Next




            'Fin Traspasos

            'Composicion competiciones
            For j = 0 To 545

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

            Next
            'Fin Composicion competiciones
            'Bloques 502

            For j = 0 To 849
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 4

                For k = 0 To 9
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                For k = 0 To 21
                    Aux_16 = swaps.swap16(Leer.ReadUInt16())
                    Leer.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                Next

                Leer.BaseStream.Position += 24

                For k = 0 To 9
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                For k = 0 To 21
                    Aux_16 = swaps.swap16(Leer.ReadUInt16())
                    Leer.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                Next

                Leer.BaseStream.Position += 24

                For k = 0 To 9
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                For k = 0 To 21
                    Aux_16 = swaps.swap16(Leer.ReadUInt16())
                    Leer.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                Next

                Leer.BaseStream.Position += 24

                For k = 0 To 30
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                Leer.BaseStream.Position += 48


            Next
            'Fin Bloques 502


        End If





        'ÇGrabar el temario

        Dim archivo As String

        If Check_type = 0 Then
            archivo = Path.GetDirectoryName(Stream_file.Name) + "\Converted to Pc\" + Path.GetFileName(Stream_file.Name)
        Else
            archivo = Path.GetDirectoryName(Stream_file.Name) + "\Converted to Console\" + Path.GetFileName(Stream_file.Name)
        End If

        If (Not System.IO.Directory.Exists(Path.GetDirectoryName(archivo))) Then
            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(archivo))
        End If
        If File.Exists(archivo) Then
            System.IO.File.Delete(archivo)
        End If
        Dim file_converted As FileStream = New FileStream(archivo, FileMode.OpenOrCreate, FileAccess.Write)
        Memory_stream.Position = 0
        Memory_stream.CopyTo(file_converted)
        Memory_stream.Close()
        file_converted.Close()


        If Check_type <> 0 Then
            MsgBox("Converted to Xbox360 Format at: " + vbCrLf + archivo.ToString)
        Else

            MsgBox("Converted to Pc Format at " + vbCrLf + archivo.ToString)
        End If




    End Sub

    Public Shared Function bitworking_Player32_OptionFile_1_toPc(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11", 2))
        aux1 = aux1 << 30
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111100", 2))
        aux1 = aux1 << 22
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11100000000", 2))
        aux1 = aux1 << 13
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11100000000000", 2))
        aux1 = aux1 << 7
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11100000000000000", 2))
        aux1 = aux1 << 1
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11100000000000000000", 2))
        aux1 = aux1 >> 5
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11100000000000000000000", 2))
        aux1 = aux1 >> 11
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111100000000000000000000000", 2))
        aux1 = aux1 >> 19
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11110000000000000000000000000000", 2))
        aux1 = aux1 >> 28
        valuecambiado = (aux1 Or valuecambiado)



        'valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function

    Public Shared Function bitworking_Player32_OptionFile_2_toPc(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11", 2))
        aux1 = aux1 << 30
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1100", 2))
        aux1 = aux1 << 26
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("110000", 2))
        aux1 = aux1 << 22
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11000000", 2))
        aux1 = aux1 << 18
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1100000000", 2))
        aux1 = aux1 << 14
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("110000000000", 2))
        aux1 = aux1 << 10
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11000000000000", 2))
        aux1 = aux1 << 6
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1100000000000000", 2))
        aux1 = aux1 << 2
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("110000000000000000", 2))
        aux1 = aux1 >> 2
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11000000000000000000", 2))
        aux1 = aux1 >> 6
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111100000000000000000000", 2))
        aux1 = aux1 >> 14
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11111100000000000000000000000000", 2))
        aux1 = aux1 >> 26
        valuecambiado = (aux1 Or valuecambiado)



        'valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function

    Public Shared Function bitworking_competition_01_OptionFile_toPc(ByVal value As UInt16) As UInt16

        Dim aux1 As UInt16 = 0
        Dim valuecambiado As UInt16 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt16("1111", 2))
        aux1 = aux1 << 12
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt16("1111110000", 2))
        aux1 = aux1 << 2
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt16("1111110000000000", 2))
        aux1 = aux1 >> 10
        valuecambiado = (aux1 Or valuecambiado)

       


        'valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function

    Public Shared Function bitworking_competition_02_OptionFile_toPc(ByVal value As UInt16) As UInt16

        Dim aux1 As UInt16 = 0
        Dim valuecambiado As UInt16 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt16("11111", 2))
        'If aux1 = 24 Then
        'aux1 = 1
        'End If
        aux1 = aux1 << 11
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt16("11111100000", 2))
        'aux1 = aux1 << 2
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt16("1111100000000000", 2))
        aux1 = aux1 >> 11
        valuecambiado = (aux1 Or valuecambiado)




        'valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function

    Public Shared Function bitworking_Player32_OptionFile_1_toConsole(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11000000000000000000000000000000", 2))
        aux1 = aux1 >> 30
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111111000000000000000000000000", 2))
        aux1 = aux1 >> 22
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111000000000000000000000", 2))
        aux1 = aux1 >> 13
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111000000000000000000", 2))
        aux1 = aux1 >> 7
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111000000000000000", 2))
        aux1 = aux1 >> 1
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111000000000000", 2))
        aux1 = aux1 << 5
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111000000000", 2))
        aux1 = aux1 << 11
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111110000", 2))
        aux1 = aux1 << 19
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1111", 2))
        aux1 = aux1 << 28
        valuecambiado = (aux1 Or valuecambiado)



        valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function

    Public Shared Function bitworking_Player32_OptionFile_2_toConsole(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11000000000000000000000000000000", 2))
        aux1 = aux1 >> 30
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("110000000000000000000000000000", 2))
        aux1 = aux1 >> 26
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1100000000000000000000000000", 2))
        aux1 = aux1 >> 22
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11000000000000000000000000", 2))
        aux1 = aux1 >> 18
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("110000000000000000000000", 2))
        aux1 = aux1 >> 14
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1100000000000000000000", 2))
        aux1 = aux1 >> 10
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11000000000000000000", 2))
        aux1 = aux1 >> 6
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("110000000000000000", 2))
        aux1 = aux1 >> 2
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("1100000000000000", 2))
        aux1 = aux1 << 2
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("11000000000000", 2))
        aux1 = aux1 << 6
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111111000000", 2))
        aux1 = aux1 << 14
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt32("111111", 2))
        aux1 = aux1 << 26
        valuecambiado = (aux1 Or valuecambiado)



        valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function

    Public Shared Function bitworking_competition_01_OptionFile_toConsole(ByVal value As UInt16) As UInt16

        Dim aux1 As UInt16 = 0
        Dim valuecambiado As UInt16 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt16("1111000000000000", 2))
        aux1 = aux1 >> 12
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt16("111111000000", 2))
        aux1 = aux1 >> 2
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt16("111111", 2))
        aux1 = aux1 << 10
        valuecambiado = (aux1 Or valuecambiado)




        valuecambiado = swaps.swap16(valuecambiado)
        Return valuecambiado


    End Function

    Public Shared Function bitworking_competition_02_OptionFile_toConsole(ByVal value As UInt16) As UInt16

        Dim aux1 As UInt16 = 0
        Dim valuecambiado As UInt16 = 0

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt16("1111100000000000", 2))
        'If aux1 = 1 Then
        'aux1 = 24
        'End If
        aux1 = aux1 >> 11
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt16("11111100000", 2))
        'aux1 = aux1 << 2
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And Convert.ToUInt16("11111", 2))
        aux1 = aux1 << 11
        valuecambiado = (aux1 Or valuecambiado)




        valuecambiado = swaps.swap16(valuecambiado)
        Return valuecambiado


    End Function

    Public Shared Sub PS3_toPc(ByRef Stream_file As FileStream)


        Dim Memory_stream As New MemoryStream
        Stream_file.Position = 0
        Stream_file.CopyTo(Memory_stream)


        Dim Leer As New BinaryReader(Memory_stream)
        Leer.ReadBytes(Memory_stream.Length)
        Stream_file.Close()

        Dim Grabar As New BinaryWriter(Memory_stream)
        Dim Aux_byte As Byte
        Dim Aux_16 As UInt16
        Dim Aux_32 As UInt32

        'Cabecera
        Leer.BaseStream.Position = 4
        Dim Check_type As Integer = Memory_stream.Length




        If Check_type = 4726040 Then



            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Leer.BaseStream.Position += 4


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Dim Number_of_players As Integer = Aux_16
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 2
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 4
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 2
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 2
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Aux_16 = converter.Reverse_int16(Aux_16)
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)

            'Empieza Player database


            For i = 0 To 24999
                Dim Check_04 As UInt16
                Dim Check_04_2 As UInt16

                Check_04 = Leer.ReadUInt16()
                Leer.BaseStream.Position += 2
                Check_04_2 = Leer.ReadUInt16()
                Leer.BaseStream.Position -= 6

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)

                If Check_04 = &H400 And Check_04_2 = &H80 Then
                    Leer.BaseStream.Position -= 2
                    Dim Corrected_04 As UInt16 = &H8004
                    Grabar.Write(Corrected_04)
                End If


                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 4

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_2_toPc(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_2_toPc(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_2_toPc(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_3_toPc(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Aux_32 = bitworking_Player32_OptionFile_1_toPc(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Aux_32 = bitworking_Player32_OptionFile_2_toPc(Aux_32)
                Grabar.Write(Aux_32)

                'Cambio players editados
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)


                Leer.BaseStream.Position += 62


            Next

            'Acabados jugadores 
            'Player Assignament

            For j = 0 To 9999

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)

                For i = 0 To 4 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.Reverse_byte(Aux_byte)
                    Grabar.Write(Aux_byte)
                Next
                Aux_16 = swaps.swap16(Leer.ReadUInt16)
                Leer.BaseStream.Position -= 2
                Aux_16 = converter.rotl_16(Aux_16, 6)
                Grabar.Write(swaps.swap16(Aux_16))

                For i = 0 To 2 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.Reverse_byte(Aux_byte)
                    Grabar.Write(Aux_byte)
                Next
                For i = 0 To 3 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotr(Aux_byte, 1)
                    Grabar.Write(Aux_byte)
                Next


                Aux_32 = swaps.swap32(Leer.ReadUInt32)
                Leer.BaseStream.Position -= 4
                Aux_32 = bitworking_PlayerAssig_toPc(Aux_32)
                Grabar.Write(Aux_32)

                For i = 0 To 4 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotr(Aux_byte, 2)
                    Grabar.Write(Aux_byte)
                Next
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.rotr(Aux_byte, 2)
                Grabar.Write(Aux_byte)

                For i = 0 To 54 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotr(Aux_byte, 4)
                    Grabar.Write(Aux_byte)
                Next
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.rotl(Aux_byte, 1)
                Grabar.Write(Aux_byte)
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.rotl(Aux_byte, 1)
                Grabar.Write(Aux_byte)

                Aux_16 = swaps.swap16(Leer.ReadUInt16)
                Leer.BaseStream.Position -= 2
                Aux_16 = converter.rotr_16(Aux_16, 4)
                Grabar.Write(Aux_16)


                For i = 0 To 3 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotr(Aux_byte, 4)
                    Grabar.Write(Aux_byte)
                Next
                Aux_16 = swaps.swap16(Leer.ReadUInt16)
                Leer.BaseStream.Position -= 2
                Aux_16 = converter.rotl_16(Aux_16, 1)
                Grabar.Write(swaps.swap16(Aux_16))
                Leer.BaseStream.Position += 1


            Next
            'Fin Player Assignamet
            'Equipos



            For i = 0 To 849

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 2
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)


                For j = 0 To 26
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)

                Next

                Leer.BaseStream.Position += 196


            Next

            'Fin Equipos 

            'Ligas
            For j = 0 To 59


                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 4

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Aux_16 = bitworking_competition_01_OptionFile_toPc(Aux_16)
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Aux_16 = bitworking_competition_02_OptionFile_toPc(Aux_16)
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)



                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)
                Leer.BaseStream.Position += 1

                Leer.BaseStream.Position += 76
            Next


            'Fin(Ligas)
            'Estadios
            For j = 0 To 99
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Leer.BaseStream.Position += 122

            Next


            'Fin(Estadios)


            'No se muy bien que es

            For j = 0 To 2499
                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                Leer.BaseStream.Position += 84


            Next



            'Fin No se muy bien que es

            'Traspasos

            For j = 0 To 849
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Leer.BaseStream.Position += 2

                For k = 0 To 31
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)

                Next
                Leer.BaseStream.Position += 32



            Next




            'Fin Traspasos

            'Composicion competiciones
            For j = 0 To 545

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

            Next
            'Fin Composicion competiciones
            'Bloques 502

            For j = 0 To 849
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 4

                For k = 0 To 9
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                For k = 0 To 21
                    Aux_16 = swaps.swap16(Leer.ReadUInt16())
                    Leer.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                Next

                Leer.BaseStream.Position += 24

                For k = 0 To 9
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                For k = 0 To 21
                    Aux_16 = swaps.swap16(Leer.ReadUInt16())
                    Leer.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                Next

                Leer.BaseStream.Position += 24

                For k = 0 To 9
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                For k = 0 To 21
                    Aux_16 = swaps.swap16(Leer.ReadUInt16())
                    Leer.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                Next

                Leer.BaseStream.Position += 24

                For k = 0 To 30
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                Leer.BaseStream.Position += 48


            Next
            'Fin Bloques 502

        ElseIf Check_type = 4799602 Then
            Leer.BaseStream.Position = 384


            Aux_32 = Leer.ReadUInt32()
            Leer.BaseStream.Position -= 4
            Grabar.Write(swaps.swap32(Aux_32))

            Leer.BaseStream.Position = Leer.BaseStream.Position + Aux_32

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Leer.BaseStream.Position += 4

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Leer.BaseStream.Position += 4


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)


            Aux_32 = swaps.swap32(Leer.ReadUInt32())
            Leer.BaseStream.Position -= 4
            Grabar.Write(Aux_32)

            Aux_16 = Leer.ReadUInt16()
            Dim Number_of_players As Integer = Aux_16
            Leer.BaseStream.Position -= 2
            Grabar.Write(swaps.swap16(Aux_16))
            Leer.BaseStream.Position += 2
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 4
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 2
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)
            Leer.BaseStream.Position += 2
            Aux_16 = swaps.swap16(Leer.ReadUInt16())
            Aux_16 = converter.Reverse_int16(Aux_16)
            Leer.BaseStream.Position -= 2
            Grabar.Write(Aux_16)

            'Empieza Player database


            For i = 0 To 24999
                Dim Check_04 As UInt16
                Dim Check_04_2 As UInt16
                Leer.BaseStream.Position += 2
                Check_04 = Leer.ReadUInt16()
                Leer.BaseStream.Position += 2
                Check_04_2 = Leer.ReadUInt16()
                Leer.BaseStream.Position -= 8

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)

                '  If Check_04 = &H4 And Check_04_2 = &H8004 Then
                'Leer.BaseStream.Position -= 4
                ' Dim Corrected_04 As UInt16 = &H8000
                'Grabar.Write(swaps.swap16(Corrected_04))
                'Leer.BaseStream.Position += 2
                'End If

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 4

                Aux_32 = Leer.ReadUInt32()
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_2_toConsole(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = Leer.ReadUInt32()
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_2_toConsole(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = Leer.ReadUInt32()
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_2_toConsole(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = Leer.ReadUInt32()
                Leer.BaseStream.Position -= 4
                Aux_32 = converter.bitworking_Player32_3_toConsole(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = Leer.ReadUInt32()
                Leer.BaseStream.Position -= 4
                Aux_32 = bitworking_Player32_OptionFile_1_toConsole(Aux_32)
                Grabar.Write(Aux_32)

                Aux_32 = Leer.ReadUInt32()
                Leer.BaseStream.Position -= 4
                Aux_32 = bitworking_Player32_OptionFile_2_toConsole(Aux_32)
                Grabar.Write(Aux_32)

               'Cambio players editados
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)


                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)

                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)


                Leer.BaseStream.Position += 62


            Next

            'Acabados jugadores 
            'Player assignament
            For j = 0 To 9999

                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)

                For i = 0 To 4 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.Reverse_byte(Aux_byte)
                    Grabar.Write(Aux_byte)
                Next
                Aux_16 = swaps.swap16(Leer.ReadUInt16)
                Leer.BaseStream.Position -= 2
                Aux_16 = converter.rotr_16(Aux_16, 6)
                Grabar.Write(swaps.swap16(Aux_16))

                For i = 0 To 2 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.Reverse_byte(Aux_byte)
                    Grabar.Write(Aux_byte)
                Next
                For i = 0 To 3 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotl(Aux_byte, 1)
                    Grabar.Write(Aux_byte)
                Next


                Aux_32 = Leer.ReadUInt32
                Leer.BaseStream.Position -= 4
                Aux_32 = bitworking_PlayerAssig_toConsole(Aux_32)
                Grabar.Write(swaps.swap32(Aux_32))

                For i = 0 To 4 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotl(Aux_byte, 2)
                    Grabar.Write(Aux_byte)
                Next
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.rotl(Aux_byte, 2)
                Grabar.Write(Aux_byte)

                For i = 0 To 54 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotl(Aux_byte, 4)
                    Grabar.Write(Aux_byte)
                Next
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.rotr(Aux_byte, 1)
                Grabar.Write(Aux_byte)
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.rotr(Aux_byte, 1)
                Grabar.Write(Aux_byte)

                Aux_16 = swaps.swap16(Leer.ReadUInt16)
                Leer.BaseStream.Position -= 2
                Aux_16 = converter.rotl_16(Aux_16, 4)
                Grabar.Write(Aux_16)


                For i = 0 To 3 - 1
                    Aux_byte = Leer.ReadByte
                    Leer.BaseStream.Position -= 1
                    Aux_byte = converter.rotl(Aux_byte, 4)
                    Grabar.Write(Aux_byte)
                Next
                Aux_16 = swaps.swap16(Leer.ReadUInt16)
                Leer.BaseStream.Position -= 2
                Aux_16 = converter.rotr_16(Aux_16, 1)
                Grabar.Write(swaps.swap16(Aux_16))
                Leer.BaseStream.Position += 1


            Next

            'fin player assignament
            'Equipos



            For i = 0 To 849

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 2
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)
                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)


                For j = 0 To 26
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)

                Next

                Leer.BaseStream.Position += 196


            Next

            'Fin Equipos 

            'Ligas
            For j = 0 To 59


                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 4

                Aux_16 = Leer.ReadUInt16()
                Leer.BaseStream.Position -= 2
                Aux_16 = bitworking_competition_01_OptionFile_toConsole(Aux_16)
                Grabar.Write(Aux_16)
                Aux_16 = Leer.ReadUInt16()
                Aux_16 = bitworking_competition_02_OptionFile_toConsole(Aux_16)
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)



                Aux_byte = Leer.ReadByte
                Leer.BaseStream.Position -= 1
                Aux_byte = converter.Reverse_byte(Aux_byte)
                Grabar.Write(Aux_byte)
                Leer.BaseStream.Position += 1

                Leer.BaseStream.Position += 76
            Next


            'Fin(Ligas)
            'Estadios
            For j = 0 To 99
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Leer.BaseStream.Position += 126

            Next


            'Fin(Estadios)


            'No se muy bien que es

            For j = 0 To 2499
                Aux_32 = swaps.swap32(Leer.ReadUInt32())
                Leer.BaseStream.Position -= 4
                Grabar.Write(Aux_32)
                Leer.BaseStream.Position += 84


            Next



            'Fin No se muy bien que es

            'Traspasos

            For j = 0 To 849
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)
                Leer.BaseStream.Position += 2

                For k = 0 To 31
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)

                Next
                Leer.BaseStream.Position += 32



            Next




            'Fin Traspasos

            'Composicion competiciones
            For j = 0 To 545

                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

            Next
            'Fin Composicion competiciones
            'Bloques 502

            For j = 0 To 849
                Aux_16 = swaps.swap16(Leer.ReadUInt16())
                Leer.BaseStream.Position -= 2
                Grabar.Write(Aux_16)

                Leer.BaseStream.Position += 4

                For k = 0 To 9
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                For k = 0 To 21
                    Aux_16 = swaps.swap16(Leer.ReadUInt16())
                    Leer.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                Next

                Leer.BaseStream.Position += 24

                For k = 0 To 9
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                For k = 0 To 21
                    Aux_16 = swaps.swap16(Leer.ReadUInt16())
                    Leer.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                Next

                Leer.BaseStream.Position += 24

                For k = 0 To 9
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                For k = 0 To 21
                    Aux_16 = swaps.swap16(Leer.ReadUInt16())
                    Leer.BaseStream.Position -= 2
                    Grabar.Write(Aux_16)
                Next

                Leer.BaseStream.Position += 24

                For k = 0 To 30
                    Aux_32 = swaps.swap32(Leer.ReadUInt32())
                    Leer.BaseStream.Position -= 4
                    Grabar.Write(Aux_32)
                Next

                Leer.BaseStream.Position += 48


            Next
            'Fin Bloques 502
        Else
            MsgBox("File Not Recognized")

        End If





        'ÇGrabar el temario

        Dim archivo As String

        If Check_type = 4726040 Then
            archivo = Path.GetDirectoryName(Stream_file.Name) + "\Converted to Pc\" + "Edit.bin"
        Else
            archivo = Path.GetDirectoryName(Stream_file.Name) + "\Converted to Console\" + "DATA.BIN"
        End If

        If (Not System.IO.Directory.Exists(Path.GetDirectoryName(archivo))) Then
            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(archivo))
        End If
        If File.Exists(archivo) Then
            System.IO.File.Delete(archivo)
        End If
        Dim file_converted As FileStream = New FileStream(archivo, FileMode.OpenOrCreate, FileAccess.Write)



        If Check_type = 4799602 Then

            Memory_stream.Position = 73162


            Dim Primer_bloque_convertido(3848676) As Byte
            Leer.BaseStream.Position = 73162
            Primer_bloque_convertido = Leer.ReadBytes(3848676)

            file_converted.Write(Primer_bloque_convertido, 0, Primer_bloque_convertido.Length)


            Dim Primer_bloque_nombres(15) As Byte
            Dim Segundo_bloque_nombres(76) As Byte
            For j = 0 To 59
                Primer_bloque_nombres = Leer.ReadBytes(15)
                file_converted.Write(Primer_bloque_nombres, 0, Primer_bloque_nombres.Length)
                Leer.BaseStream.Position += 1
                Segundo_bloque_nombres = Leer.ReadBytes(76)
                file_converted.Write(Segundo_bloque_nombres, 0, Segundo_bloque_nombres.Length)
                file_converted.WriteByte(0)
            Next




            Dim Id_estadio(2) As Byte
            Dim bloque_estadios_to_pc(122) As Byte

            For j = 0 To 99
                Id_estadio = Leer.ReadBytes(2)
                Leer.BaseStream.Position += 1
                file_converted.Write(Id_estadio, 0, Id_estadio.Length)
                bloque_estadios_to_pc = Leer.ReadBytes(122)
                file_converted.Write(bloque_estadios_to_pc, 0, bloque_estadios_to_pc.Length)
                Leer.BaseStream.Position += 3
            Next


            Dim Bloque_delFinal_convertido(859444) As Byte

            Bloque_delFinal_convertido = Leer.ReadBytes(859444)

            file_converted.Write(Bloque_delFinal_convertido, 0, Bloque_delFinal_convertido.Length)



            'Memory_stream.CopyTo(file_converted)
            Memory_stream.Close()
            file_converted.Close()


            MsgBox("Converted to Ps3 Format at: " + vbCrLf + archivo.ToString)
        Else

            Dim cabeceraPc() As Byte = My.Resources.cabecera_pc_.ToArray
            file_converted.Write(cabeceraPc, 0, cabeceraPc.Length)

            Leer.BaseStream.Position = 0
            Dim Primer_bloque_convertido(3848676) As Byte

            Primer_bloque_convertido = Leer.ReadBytes(3848676)

            file_converted.Write(Primer_bloque_convertido, 0, Primer_bloque_convertido.Length)


            Dim Primer_bloque_nombres(15) As Byte
            Dim Segundo_bloque_nombres(76) As Byte
            For j = 0 To 59
                Primer_bloque_nombres = Leer.ReadBytes(15)
                file_converted.Write(Primer_bloque_nombres, 0, Primer_bloque_nombres.Length)
                file_converted.WriteByte(0)
                Segundo_bloque_nombres = Leer.ReadBytes(76)
                file_converted.Write(Segundo_bloque_nombres, 0, Segundo_bloque_nombres.Length)
                Leer.BaseStream.Position += 1

            Next




            Dim Id_estadio(2) As Byte
            Dim bloque_estadios_to_pc(122) As Byte

            For j = 0 To 99
                Id_estadio = Leer.ReadBytes(2)
                file_converted.Write(Id_estadio, 0, Id_estadio.Length)
                file_converted.WriteByte(0)
                bloque_estadios_to_pc = Leer.ReadBytes(122)
                file_converted.Write(bloque_estadios_to_pc, 0, bloque_estadios_to_pc.Length)
                file_converted.WriteByte(0)
                file_converted.WriteByte(0)
                file_converted.WriteByte(0)
            Next


            Dim Bloque_delFinal_convertido(859092) As Byte

            Bloque_delFinal_convertido = Leer.ReadBytes(859092)

            file_converted.Write(Bloque_delFinal_convertido, 0, Bloque_delFinal_convertido.Length)

            For j = 0 To 351
                file_converted.WriteByte(0)
            Next



            'Memory_stream.CopyTo(file_converted)
            Memory_stream.Close()
            file_converted.Close()
            MsgBox("Converted to Pc Format at " + vbCrLf + archivo.ToString)
        End If




    End Sub

    Public Shared Function bitworking_PlayerAssig_toConsole(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0
        Dim Reversed As UInt32 = converter.Reverse_int32(value)


        aux1 = Reversed
        aux1 = (aux1 And 4278190080)
        'aux1 = aux1 << 24
        valuecambiado = (aux1 Or valuecambiado)



        aux1 = value
        aux1 = (aux1 And 4227858432)
        aux1 = aux1 >> 26
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And 66060288)
        aux1 = aux1 >> 14
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And 1032192)
        aux1 = aux1 >> 2
        valuecambiado = (aux1 Or valuecambiado)


        aux1 = value
        aux1 = (aux1 And 16128)
        aux1 = aux1 << 10
        valuecambiado = (aux1 Or valuecambiado)



        'valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function

    Public Shared Function bitworking_PlayerAssig_toPc(ByVal value As UInt32) As UInt32

        Dim aux1 As UInt32 = 0
        Dim valuecambiado As UInt32 = 0
        Dim Reversed As UInt32 = converter.Reverse_int32(value)


        aux1 = Reversed
        aux1 = (aux1 And 255)
        'aux1 = aux1 << 24
        valuecambiado = (aux1 Or valuecambiado)



        aux1 = value
        aux1 = (aux1 And 63)
        aux1 = aux1 << 26
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And 4032)
        aux1 = aux1 << 14
        valuecambiado = (aux1 Or valuecambiado)

        aux1 = value
        aux1 = (aux1 And 258048)
        aux1 = aux1 << 2
        valuecambiado = (aux1 Or valuecambiado)


        aux1 = value
        aux1 = (aux1 And 16515072)
        aux1 = aux1 >> 10
        valuecambiado = (aux1 Or valuecambiado)



        'valuecambiado = swaps.swap32(valuecambiado)
        Return valuecambiado


    End Function

  
End Class


