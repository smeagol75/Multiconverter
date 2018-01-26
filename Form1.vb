Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports System.ComponentModel


Public Class Form1

    'Dim Stream_file As FileStream
    Dim OpenPes As New OpenFileDialog
    Dim archivo As String
    Dim correctos As Integer = 0
    Dim incorrectos As Integer = 0
    Dim Stream_para_version As FileStream
    Dim Check_FF As Byte = 3



    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        OpenPes = Me.OpenFileDialog1

        OpenPes.Title = "Open A Pes 2016 compressed file"
        OpenPes.Filter = "*.* (*.*)|*.*"
        OpenPes.Multiselect = True


        If OpenPes.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Dim number_of_files As Integer = OpenPes.FileNames.Count

            ProgressBar1.Minimum = 1
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = number_of_files


            For Each Me.archivo In OpenPes.FileNames





                Try
                    Dim Stream_file As FileStream = File.Open(archivo, FileMode.Open, FileAccess.Read)
                    If (Stream_file IsNot Nothing) Then
                        Dim Leer As New BinaryReader(Stream_file)
                        Leer.ReadBytes(Stream_file.Length)
                        Leer.BaseStream.Position = 3
                        Dim CheckPesFile As Integer = &H59534557

                        If Leer.ReadUInt32 = CheckPesFile Then
                            Leer.BaseStream.Position = 0
                            Dim Check_pc_console As Byte = Leer.ReadByte

                            Dim Check_Console As Byte = &H1
                            If Check_pc_console = Check_Console Then
                                zlib1.unzlibconsole(Stream_file, archivo)
                                Stream_file.Close()
                                'MsgBox("Console File Unzlibed Succesfully")
                                correctos = correctos + 1

                            ElseIf Check_pc_console = &H0 Or Check_pc_console = &HFF Then

                                zlib1.unzlibpc(Stream_file, archivo)
                                Stream_file.Close()
                                'MsgBox("Pc File Unzlibed Succesfully")
                                correctos = correctos + 1

                            ElseIf Check_pc_console = &H2 Then
                                zlib1.unzlibconsole(Stream_file, archivo)
                                Stream_file.Close()
                                'MsgBox("Console File Unzlibed Succesfully")
                                correctos = correctos + 1


                            Else
                                MsgBox("UnkNown Compression")
                                incorrectos = incorrectos + 1
                            End If

                        Else
                            If OpenPes.FileNames.Count = 1 Then
                                MsgBox(archivo + " isn´t a Pes2014 compressed file")
                            End If
                            Stream_file.Close()
                            incorrectos = incorrectos + 1
                            'Return
                        End If



                    End If
                Catch Ex As Exception
                    'MessageBox.Show("Cannot read file from disk or File is Sized 0. Original error: " & Ex.Message)
                    incorrectos = incorrectos + 1

                End Try
                ProgressBar1.Increment(1)
            Next archivo

        End If
        MsgBox(correctos.ToString + " unzlib Succesfully  " + vbCrLf + incorrectos.ToString + " Failed to unzlib (Maybe size 0 or unknown compression)")
        correctos = 0
        incorrectos = 0
    End Sub

    Private Sub Button2_Click_1(sender As System.Object, e As System.EventArgs) Handles Button2.Click

        OpenPes = Me.OpenFileDialog1
        OpenPes.Title = "Open A Pes 2014 uncompressed file"
        OpenPes.Filter = "*.unzlib (*.unzlib)|*.unzlib"
        OpenPes.Multiselect = True

        If OpenPes.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Dim number_of_files As Integer = OpenPes.FileNames.Count

            ProgressBar1.Minimum = 1
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = number_of_files

            For Each Me.archivo In OpenPes.FileNames

                


                Try
                    Dim Stream_file As FileStream = File.Open(archivo, FileMode.Open, FileAccess.Read)
                    If (Stream_file IsNot Nothing) Then

                        Dim src As Byte() = New Byte(Stream_file.Length) {}
                        Stream_file.Read(src, 0, Stream_file.Length)

                        Dim Destlen As UInt32 = Stream_file.Length * 2
                        Dim Dest(0 To Destlen) As Byte
                        Dim Retval As UInt32 = zlib1.compress2(Dest, Destlen, src, Stream_file.Length, 9)
                        If (Retval = 0) Then
                            Dim Filename As String = Path.GetFileNameWithoutExtension(archivo)
                            If File.Exists(Path.GetDirectoryName(archivo) + "\" + Filename) Then
                                System.IO.File.Delete(Path.GetDirectoryName(archivo) + "\" + Filename)

                            End If
                            Dim file_zlib = New FileStream(Path.GetDirectoryName(archivo) + "\" + Filename, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                            Dim Grabar As New BinaryWriter(file_zlib)
                            file_zlib.WriteByte(&H1)
                            file_zlib.WriteByte(&H10)
                            file_zlib.WriteByte(&H81)
                            Grabar.Write("WESYS", 0, 5)
                            Destlen = swaps.swap32(Destlen)
                            Dim uncomp_length As UInt32 = swaps.swap32(Stream_file.Length)
                            Grabar.Write(Destlen)
                            Destlen = swaps.swap32(Destlen)
                            Grabar.Write(uncomp_length)
                            Grabar.Write(Dest, 0, Destlen)
                            file_zlib.Close()
                            Stream_file.Close()

                            'MsgBox("Succesfully Compressed to Console Format")
                            correctos = correctos + 1
                        End If
                    Else
                        'MsgBox("UnKnown Error")
                        incorrectos = incorrectos + 1
                        Stream_file.Close()

                    End If

                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try
            Next archivo
            ProgressBar1.Increment(1)

        End If

        MsgBox(correctos.ToString + " zlib Succesfully to Xbox360 format " + vbCrLf + incorrectos.ToString + " Failed to zlib (Maybe size 0?)")
        correctos = 0
        incorrectos = 0
    End Sub

    Private Sub Button3_Click_1(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        OpenPes = Me.OpenFileDialog1
        OpenPes.Title = "Open A Pes 2014 uncompressed file"
        OpenPes.Filter = "*.unzlib (*.unzlib)|*.unzlib"
        OpenPes.Multiselect = True

        If OpenPes.ShowDialog() = System.Windows.Forms.DialogResult.OK Then


            Dim number_of_files As Integer = OpenPes.FileNames.Count 
            ProgressBar1.Minimum = 1
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = number_of_files


            For Each Me.archivo In OpenPes.FileNames



                Try
                    Dim Stream_file As FileStream = File.Open(archivo, FileMode.Open, FileAccess.Read)
                    If (Stream_file IsNot Nothing) Then



                        Dim src As Byte() = New Byte(Stream_file.Length) {}
                        Stream_file.Read(src, 0, Stream_file.Length)

                        Dim Destlen As UInt32 = Stream_file.Length * 2
                        Dim Dest(0 To Destlen) As Byte
                        Dim Retval As UInt32 = zlib1.compress2(Dest, Destlen, src, Stream_file.Length, 9)
                        If (Retval = 0) Then
                            Dim Filename As String = Path.GetFileNameWithoutExtension(archivo)
                            If File.Exists(Path.GetDirectoryName(archivo) + "\" + Filename) Then
                                System.IO.File.Delete(Path.GetDirectoryName(archivo) + "\" + Filename)

                            End If
                            Dim file_zlib = New FileStream(Path.GetDirectoryName(archivo) + "\" + Filename, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                            Dim Grabar As New BinaryWriter(file_zlib)
                            file_zlib.WriteByte(&H0)
                            file_zlib.WriteByte(&H10)
                            file_zlib.WriteByte(&H81)
                            Grabar.Write("WESYS", 0, 5)
                            Dim uncomp_length As UInt32 = Stream_file.Length
                            Grabar.Write(Destlen)
                            Grabar.Write(uncomp_length)
                            Grabar.Write(Dest, 0, Destlen)
                            file_zlib.Close()
                            Stream_file.Close()
                            'MsgBox("Succesfully Compressed to PC Format")
                            correctos = correctos + 1
                        End If
                    Else
                        'MsgBox("UnKnown Error")
                        Stream_file.Close()
                        incorrectos = incorrectos + 1


                    End If

                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try

            Next archivo
            ProgressBar1.Increment(1)
        End If

        MsgBox(correctos.ToString + " zlib Succesfully to Pc format " + vbCrLf + incorrectos.ToString + " Failed to zlib (Maybe size 0?)")
        correctos = 0
        incorrectos = 0
    End Sub

    Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabControl1.DrawItem

        'Firstly we'll define some parameters.
        'Me.BackColor = Color.White
        Dim CurrentTab As TabPage = TabControl1.TabPages(e.Index)
        Dim ItemRect As Rectangle = TabControl1.GetTabRect(e.Index)
        Dim FillBrush As New SolidBrush(Color.White)
        Dim TextBrush As New SolidBrush(Color.Crimson)
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        'If we are currently painting the Selected TabItem we'll 
        'change the brush colors and inflate the rectangle.
        If CBool(e.State And DrawItemState.Selected) Then
            FillBrush.Color = Color.Crimson
            TextBrush.Color = Color.White
            ItemRect.Inflate(2, 2)
        End If

        'Set up rotation for left and right aligned tabs
        If TabControl1.Alignment = TabAlignment.Left Or TabControl1.Alignment = TabAlignment.Right Then
            Dim RotateAngle As Single = 90
            If TabControl1.Alignment = TabAlignment.Left Then RotateAngle = 270
            Dim cp As New PointF(ItemRect.Left + (ItemRect.Width \ 2), ItemRect.Top + (ItemRect.Height \ 2))
            e.Graphics.TranslateTransform(cp.X, cp.Y)
            e.Graphics.RotateTransform(RotateAngle)
            ItemRect = New Rectangle(-(ItemRect.Height \ 2), -(ItemRect.Width \ 2), ItemRect.Height, ItemRect.Width)
        End If

        'Next we'll paint the TabItem with our Fill Brush
        e.Graphics.FillRectangle(FillBrush, ItemRect)

        'Now draw the text.
        e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, RectangleF.op_Implicit(ItemRect), sf)

        'Reset any Graphics rotation
        e.Graphics.ResetTransform()

        'Finally, we should Dispose of our brushes.
        FillBrush.Dispose()
        TextBrush.Dispose()

    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        OpenPes = Me.OpenFileDialog1
        OpenPes.Title = "Open A Pes 2016 Database file"
        OpenPes.Filter = "*.bin (*.bin)|*.bin"
        OpenPes.Multiselect = True

        Dim Contador_errores As Integer = 0
        Dim Pc_or_console As String = "UnkNown Format"

        If OpenPes.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Dim number_of_files As Integer = OpenPes.FileNames.Count

            ProgressBar1.Minimum = 1
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = number_of_files



            For Each Me.archivo In OpenPes.FileNames
                


                Try

                    Dim Stream_file As FileStream = File.Open(archivo, FileMode.Open, FileAccess.Read)
                    If (Stream_file IsNot Nothing And Stream_file.Length <> 0) Then
                        Dim Leer As New BinaryReader(Stream_file)
                        Leer.ReadBytes(Stream_file.Length)
                        Leer.BaseStream.Position = 2
                        Dim CHECK_HALFZLIB As Byte = Leer.ReadByte

                        Leer.BaseStream.Position = 3
                        Dim CheckPesFile As Integer = &H59534557
                        Dim unzlib_memstream As New MemoryStream
                        If Leer.ReadUInt32 = CheckPesFile Then
                            Leer.BaseStream.Position = 0
                            Dim Check_pc_console As Byte = Leer.ReadByte

                            Dim Check_Console As Byte = &H1
                            Dim Check_ps3 As Byte = &H2
                            If Check_pc_console = Check_Console Or Check_pc_console = Check_ps3 Then
                                If CHECK_HALFZLIB <> 0 Then
                                    unzlib_memstream = zlib1.unzlibconsole_to_MemStream(Stream_file)
                                Else
                                    Leer.BaseStream.Position = 16
                                    Dim buffer As Byte() = Leer.ReadBytes(Stream_file.Length - 16)
                                    unzlib_memstream.Write(buffer, 0, buffer.Length)
                                End If


                                ' unzlib_memstream = zlib1.unzlibconsole_to_MemStream(Stream_file)
                                Stream_file.Close()
                                Leer.Close()
                                Pc_or_console = "To Pc "
                                Dim Nombre As String = Path.GetFileName(archivo)

                                Select Case Nombre

                                    Case "Ball.bin", "Ball1.bin", "Ball2.bin", "Ball3.bin", "Ball4.bin", "Ball5.bin", "Ball6.bin"
                                        converter.ball(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "BallCondition.bin", "BallCondition1.bin", "BallCondition2.bin", "BallCondition3.bin", "BallCondition4.bin", "BallCondition5.bin", "BallCondition6.bin"
                                        converter.BallCondition_topc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Boots.bin", "Boots1.bin", "Boots2.bin", "Boots3.bin", "Boots4.bin", "Boots5.bin", "Boots6.bin"
                                        converter.Boots(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Coach.bin", "Coach1.bin", "Coach2.bin", "Coach3.bin", "Coach4.bin", "Coach5.bin", "Coach6.bin"
                                        converter.Coach_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Competition.bin", "Competition1.bin", "Competition2.bin", "Competition3.bin", "Competition4.bin", "Competition5.bin", "Competition6.bin"
                                        converter.Competition_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "CompetitionEntry1.bin", "CompetitionEntry2.bin", "CompetitionEntry.bin", "CompetitionEntry3.bin", "CompetitionEntry4.bin", "CompetitionEntry5.bin", "CompetitionEntry6.bin"
                                        converter.CompetitionEntry_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "CompetitionKind1.bin", "CompetitionKind2.bin", "CompetitionKind.bin", "CompetitionKind3.bin", "CompetitionKind4.bin", "CompetitionKind5.bin", "CompetitionKind6.bin"
                                        converter.CompetitionKind_to_pc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "CompetitionRegulation.bin", "CompetitionRegulation1.bin", "CompetitionRegulation2.bin", "CompetitionRegulation3.bin", "CompetitionRegulation4.bin", "CompetitionRegulation5.bin", "CompetitionRegulation6.bin"
                                        converter.CompetitionRegulation_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Country.bin", "Country1.bin", "Country2.bin", "Country3.bin", "Country4.bin", "Country5.bin", "Country6.bin"
                                        converter.Country_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Derby.bin", "Derby1.bin", "Derby2.bin", "Derby3.bin", "Derby4.bin", "Derby5.bin", "Derby6.bin"
                                        converter.Derby_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "InstallVersionBallDlcAs.bin", "InstallVersionBallDlcEu.bin", "InstallVersionBallDlcJp.bin", "InstallVersionBallDlcUs.bin", "InstallVersionBallDp.bin"
                                        converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "InstallVersionBootsDlcAs.bin", "InstallVersionBootsDlcEu.bin", "InstallVersionBootsDlcJp.bin", "InstallVersionBootsDlcUs.bin", "InstallVersionBootsDp.bin"
                                        converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "InstallVersionStadiumDlcAs.bin", "InstallVersionStadiumDlcEu.bin", "InstallVersionStadiumDlcJp.bin", "InstallVersionStadiumDlcUs.bin", "InstallVersionStadiumDp.bin"
                                        converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "InstallVersionGloveDlcAs.bin", "InstallVersionGloveDlcEu.bin", "InstallVersionGloveDlcJp.bin", "InstallVersionGloveDlcUs.bin", "InstallVersionGloveDp.bin"
                                        converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Player.bin", "Player1.bin", "Player2.bin", "Player3.bin", "Player4.bin", "Player5.bin", "Player6.bin"
                                        converter.Player_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "PlayerAssignment.bin", "PlayerAssignment1.bin", "PlayerAssignment2.bin", "PlayerAssignment3.bin", "PlayerAssignment4.bin", "PlayerAssignment5.bin", "PlayerAssignment6.bin"
                                        converter.PlayerAssignment_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "SpecialPlayerAssignment.bin", "SpecialPlayerAssignment1.bin", "SpecialPlayerAssignment2.bin", "SpecialPlayerAssignment3.bin", "SpecialPlayerAssignment4.bin", "SpecialPlayerAssignment5.bin", "SpecialPlayerAssignment6.bin"
                                        converter.SpecialPlayerAssignment(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "SpecialPlayerAssignmentKind.bin", "SpecialPlayerAssignmentKind1.bin", "SpecialPlayerAssignmentKind2.bin", "SpecialPlayerAssignmentKind3.bin", "SpecialPlayerAssignmentKind4.bin", "SpecialPlayerAssignmentKind5.bin", "SpecialPlayerAssignmentKind6.bin"
                                        converter.SpecialPlayerAssignmentKind(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Stadium.bin", "Stadium1.bin", "Stadium2.bin", "Stadium3.bin", "Stadium4.bin", "Stadium5.bin", "Stadium6.bin"
                                        converter.Stadium_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "StadiumOrder.bin", "StadiumOrder1.bin", "StadiumOrder2.bin", "StadiumOrder3.bin", "StadiumOrder4.bin", "StadiumOrder5.bin", "StadiumOrder6.bin"
                                        converter.StadiumOrder_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "StadiumOrderInConfederation.bin", "StadiumOrderInConfederation1.bin", "StadiumOrderInConfederation2.bin", "StadiumOrderInConfederation3.bin", "StadiumOrderInConfederation4.bin", "StadiumOrderInConfederation5.bin", "StadiumOrderInConfederation6.bin"
                                        converter.StadiumOrderInConfederation_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Team.bin", "Team1.bin", "Team2.bin", "Team3.bin", "Team4.bin", "Team5.bin", "Team6.bin"
                                        converter.Team_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Glove.bin", "Glove1.bin", "Glove2.bin", "Glove3.bin", "Glove4.bin", "Glove5.bin", "Glove6.bin"
                                        converter.Glove(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Tactics.bin", "Tactics1.bin", "Tactics2.bin", "Tactics3.bin", "Tactics4.bin", "Tactics5.bin", "Tactics6.bin"
                                        converter.Tactics_toPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "TacticsFormation.bin", "TacticsFormation1.bin", "TacticsFormation2.bin", "TacticsFormation3.bin", "TacticsFormation4.bin", "TacticsFormation5.bin", "TacticsFormation6.bin"
                                        converter.Tactics_FormationToPc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case Else

                                        MsgBox("File Not Recognized as Pes 2014 Database file or not supported sorry")
                                End Select

                            ElseIf Check_pc_console = &H0 Then
                                If CHECK_HALFZLIB <> 0 Then
                                    unzlib_memstream = zlib1.unzlibPc_to_Memstream(Stream_file)
                                Else
                                    Leer.BaseStream.Position = 16
                                    Dim buffer As Byte() = Leer.ReadBytes(Stream_file.Length - 16)
                                    unzlib_memstream.Write(buffer, 0, buffer.Length)
                                End If
                                'unzlib_memstream = zlib1.unzlibPc_to_Memstream(Stream_file)
                                Stream_file.Close()
                                Leer.Close()
                                Pc_or_console = " to Console "
                                Dim Nombre As String = Path.GetFileName(archivo)

                                Select Case Nombre

                                    Case "Ball.bin", "Ball1.bin", "Ball2.bin", "Ball3.bin", "Ball4.bin", "Ball5.bin", "Ball6.bin"
                                        converter.ball(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "BallCondition.bin", "BallCondition1.bin", "BallCondition2.bin", "BallCondition3.bin", "BallCondition4.bin", "BallCondition5.bin", "BallCondition6.bin"
                                        converter.BallCondition_toconsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Boots.bin", "Boots1.bin", "Boots2.bin", "Boots3.bin", "Boots4.bin", "Boots5.bin", "Boots6.bin"
                                        converter.Boots(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Coach.bin", "Coach1.bin", "Coach2.bin", "Coach3.bin", "Coach4.bin", "Coach5.bin", "Coach6.bin"
                                        converter.Coach_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Competition.bin", "Competition1.bin", "Competition2.bin", "Competition3.bin", "Competition4.bin", "Competition5.bin", "Competition6.bin"
                                        converter.Competition_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "CompetitionEntry1.bin", "CompetitionEntry2.bin", "CompetitionEntry.bin", "CompetitionEntry3.bin", "CompetitionEntry4.bin", "CompetitionEntry5.bin", "CompetitionEntry6.bin"
                                        converter.CompetitionEntry_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "CompetitionKind1.bin", "CompetitionKind2.bin", "CompetitionKind.bin", "CompetitionKind3.bin", "CompetitionKind4.bin", "CompetitionKind5.bin", "CompetitionKind6.bin"
                                        converter.CompetitionKind_to_console(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "CompetitionRegulation.bin", "CompetitionRegulation1.bin", "CompetitionRegulation2.bin", "CompetitionRegulation3.bin", "CompetitionRegulation4.bin", "CompetitionRegulation5.bin", "CompetitionRegulation6.bin"
                                        converter.CompetitionRegulation_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Country.bin", "Country1.bin", "Country2.bin", "Country3.bin", "Country4.bin", "Country5.bin", "Country6.bin"
                                        converter.Country_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Derby.bin", "Derby1.bin", "Derby2.bin", "Derby3.bin", "Derby4.bin", "Derby5.bin", "Derby6.bin"
                                        converter.Derby_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "InstallVersionBallDlcAs.bin", "InstallVersionBallDlcEu.bin", "InstallVersionBallDlcJp.bin", "InstallVersionBallDlcUs.bin", "InstallVersionBallDp.bin"
                                        converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "InstallVersionBootsDlcAs.bin", "InstallVersionBootsDlcEu.bin", "InstallVersionBootsDlcJp.bin", "InstallVersionBootsDlcUs.bin", "InstallVersionBootsDp.bin"
                                        converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "InstallVersionStadiumDlcAs.bin", "InstallVersionStadiumDlcEu.bin", "InstallVersionStadiumDlcJp.bin", "InstallVersionStadiumDlcUs.bin", "InstallVersionStadiumDp.bin"
                                        converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "InstallVersionGloveDlcAs.bin", "InstallVersionGloveDlcEu.bin", "InstallVersionGloveDlcJp.bin", "InstallVersionGloveDlcUs.bin", "InstallVersionGloveDp.bin"
                                        converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Player.bin", "Player1.bin", "Player2.bin", "Player3.bin", "Player4.bin", "Player5.bin", "Player6.bin"
                                        converter.Player_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "PlayerAssignment.bin", "PlayerAssignment1.bin", "PlayerAssignment2.bin", "PlayerAssignment3.bin", "PlayerAssignment4.bin", "PlayerAssignment5.bin", "PlayerAssignment6.bin"
                                        converter.PlayerAssignment_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "SpecialPlayerAssignment.bin", "SpecialPlayerAssignment1.bin", "SpecialPlayerAssignment2.bin", "SpecialPlayerAssignment3.bin", "SpecialPlayerAssignment4.bin", "SpecialPlayerAssignment5.bin", "SpecialPlayerAssignment6.bin"
                                        converter.SpecialPlayerAssignment(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "SpecialPlayerAssignmentKind.bin", "SpecialPlayerAssignmentKind1.bin", "SpecialPlayerAssignmentKind2.bin", "SpecialPlayerAssignmentKind3.bin", "SpecialPlayerAssignmentKind4.bin", "SpecialPlayerAssignmentKind5.bin", "SpecialPlayerAssignmentKind6.bin"
                                        converter.SpecialPlayerAssignmentKind(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Stadium.bin", "Stadium1.bin", "Stadium2.bin", "Stadium3.bin", "Stadium4.bin", "Stadium5.bin", "Stadium6.bin"
                                        converter.Stadium_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "StadiumOrder.bin", "StadiumOrder1.bin", "StadiumOrder2.bin", "StadiumOrder3.bin", "StadiumOrder4.bin", "StadiumOrder5.bin", "StadiumOrder6.bin"
                                        converter.StadiumOrder_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "StadiumOrderInConfederation.bin", "StadiumOrderInConfederation1.bin", "StadiumOrderInConfederation2.bin", "StadiumOrderInConfederation3.bin", "StadiumOrderInConfederation4.bin", "StadiumOrderInConfederation5.bin", "StadiumOrderInConfederation6.bin"
                                        converter.StadiumOrderInConfederation_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Team.bin", "Team1.bin", "Team2.bin", "Team3.bin", "Team4.bin", "Team5.bin", "Team6.bin"
                                        converter.Team_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()


                                    Case "Glove.bin", "Glove1.bin", "Glove2.bin", "Glove3.bin", "Glove4.bin", "Glove5.bin", "Glove6.bin"
                                        converter.Glove(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Tactics.bin", "Tactics1.bin", "Tactics2.bin", "Tactics3.bin", "Tactics4.bin", "Tactics5.bin", "Tactics6.bin"
                                        converter.Tactics_toConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "TacticsFormation.bin", "TacticsFormation1.bin", "TacticsFormation2.bin", "TacticsFormation3.bin", "TacticsFormation4.bin", "TacticsFormation5.bin", "TacticsFormation6.bin"
                                        converter.Tactics_FormationToConsole(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case Else

                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox("File Not Recognized as Pes 2014 Database file or not supported sorry")
                                        End If
                                        Contador_errores += 1
                                End Select


                            Else
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox("File Must be size 0 or corrupt")
                                End If

                                Contador_errores += 1
                            End If


                    Else
                            If OpenPes.FileNames.Count = 1 Then
                                MsgBox("File Must be size 0 or corrupt Or not a Pes 2014 database file")
                            End If
                            Contador_errores += 1

                            End If
                    Else
                        If OpenPes.FileNames.Count = 1 Then
                            MsgBox("File Must be size 0 or corrupt")
                        End If

                        Contador_errores += 1

                        End If

                Catch Ex As Exception
                    If OpenPes.FileNames.Count = 1 Then
                        MsgBox("Unknown error: " + Ex.ToString)
                    End If
                    Contador_errores += 1

                End Try
                ProgressBar1.Increment(1)
            Next archivo


        Else
            MsgBox("No File Selected!!!!!")
        End If

        If OpenPes.FileNames.Count <> 1 And OpenPes.FileNames.Count <> 0 And Pc_or_console <> "UnkNown Format" Then

            MsgBox((OpenPes.FileNames.Count - Contador_errores).ToString & " Files Succefully Converted " & Pc_or_console & " Format." & vbCrLf & Contador_errores.ToString & " Files Was Sized 0 or corrupt from a total of " & OpenPes.FileNames.Count.ToString & " Files.")

        End If



    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        OpenPes = Me.OpenFileDialog1
        OpenPes.Title = "Open A Pes 2014 Edit.bin file (pc or xbox360)"
        OpenPes.Filter = "*.bin (*.bin)|*.bin"
        OpenPes.Multiselect = False
        If OpenPes.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            ProgressBar1.Minimum = 1
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = 10
            Dim Stream_file As FileStream = File.Open(OpenPes.FileName, FileMode.Open, FileAccess.Read)
            Of_converter.X360_toPc(Stream_file)



        End If

    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        OpenPes = Me.OpenFileDialog1
        OpenPes.Title = "Open A Pes 2014  file"
        OpenPes.Filter = "*.* (*.*)|*.*"
        OpenPes.Multiselect = True

        Dim Contador_errores As Integer = 0
        Dim Pc_or_console As String = "UnkNown Format"

        If OpenPes.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Dim number_of_files As Integer = OpenPes.FileNames.Count

            ProgressBar1.Minimum = 1
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = number_of_files



            For Each Me.archivo In OpenPes.FileNames



                Try

                    Dim Stream_file As FileStream = File.Open(archivo, FileMode.Open, FileAccess.Read)
                    If (Stream_file IsNot Nothing And Stream_file.Length <> 0) Then
                        Dim Leer As New BinaryReader(Stream_file)
                        Leer.ReadBytes(Stream_file.Length)
                        Leer.BaseStream.Position = 3
                        Dim CheckPesFile As Integer = &H59534557
                        Dim unzlib_memstream As New MemoryStream

                        If Leer.ReadUInt32 = CheckPesFile Then
                            Leer.BaseStream.Position = 0
                            Dim Check_pc_console As Byte = Leer.ReadByte
                            Dim extension As String = Path.GetExtension(archivo)
                            Dim Check_Console As Byte = &H1
                            If Check_pc_console = Check_Console Or Check_pc_console = &H2 Then
                                unzlib_memstream = zlib1.unzlibconsole_to_MemStream(Stream_file)
                                Stream_file.Close()
                                Leer.Close()
                                Pc_or_console = "To Pc "
                                Dim Nombre As String = Path.GetFileName(archivo)

                                Select Case Nombre

                                    Case "UniColor.bin"
                                        Multiconverter.Unicolor(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Spike.bin"
                                        Multiconverter.Spike(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Pc Format")
                                        End If
                                        unzlib_memstream.Close()


                                    Case Else
                                        Select Case extension

                                            Case ".dds", ".mtl"
                                                unzlib_memstream.Position = 0
                                                zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                                Stream_file.Close()
                                                If OpenPes.FileNames.Count = 1 Then
                                                    MsgBox(Nombre & " Succefully Converted to Pc Format")
                                                End If
                                                unzlib_memstream.Close()

                                            Case ".model"
                                                'Multiconverter.model_ToPc(unzlib_memstream)
                                                MsgBox("Doesn´t work to pc yet, sorry")
                                                unzlib_memstream.Position = 0
                                                zlib1.zlib_memstream_to_pc(unzlib_memstream, archivo)
                                                Stream_file.Close()
                                                If OpenPes.FileNames.Count = 1 Then
                                                    MsgBox(Nombre & " Succefully Converted to Pc Format")
                                                End If
                                                unzlib_memstream.Close()



                                            Case Else
                                                If OpenPes.FileNames.Count = 1 Then
                                                    MsgBox("File Not Recognized as Pes 2014 file or not supported sorry")
                                                End If
                                                Contador_errores += 1
                                        End Select



                                End Select

                            ElseIf Check_pc_console = &H0 Then

                                unzlib_memstream = zlib1.unzlibPc_to_Memstream(Stream_file)
                                Stream_file.Close()
                                Leer.Close()
                                Pc_or_console = " to Console "
                                Dim Nombre As String = Path.GetFileName(archivo)

                                Select Case Nombre

                                    Case "UniColor.bin"
                                        Multiconverter.Unicolor(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "Spike.bin"
                                        Multiconverter.Spike(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()

                                    Case "audiarea.bin"
                                        Multiconverter.audiarea_to_console(unzlib_memstream)
                                        unzlib_memstream.Position = 0
                                        zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                        Stream_file.Close()
                                        If OpenPes.FileNames.Count = 1 Then
                                            MsgBox(Nombre & " Succefully Converted to Console Format")
                                        End If
                                        unzlib_memstream.Close()


                                    Case Else

                                        Select Case extension

                                            Case ".dds", ".mtl"
                                                unzlib_memstream.Position = 0
                                                zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                                Stream_file.Close()
                                                If OpenPes.FileNames.Count = 1 Then
                                                    MsgBox(Nombre & " Succefully Converted to Pc Format")
                                                End If
                                                unzlib_memstream.Close()

                                            Case ".model"
                                                Multiconverter.model_ToConsole(unzlib_memstream)
                                                unzlib_memstream.Position = 0
                                                zlib1.zlib_memstream_to_console(unzlib_memstream, archivo)
                                                Stream_file.Close()
                                                If OpenPes.FileNames.Count = 1 Then
                                                    MsgBox(Nombre & " Succefully Converted to Console Format")
                                                End If
                                                unzlib_memstream.Close()


                                            Case Else
                                                If OpenPes.FileNames.Count = 1 Then
                                                    MsgBox("File Not Recognized as Pes 2014 file or not supported sorry")
                                                End If
                                                Contador_errores += 1
                                        End Select


                                End Select


                            Else
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox("File Must be size 0 or corrupt")
                                End If

                                Contador_errores += 1
                            End If


                        ElseIf Stream_file.Length = 120 Then

                            Stream_file.Position = 0
                            Dim Check_console As Byte = Stream_file.ReadByte
                            'MessageBox.Show("Is A Kit Config File?", "Pes 2014 Multiconverter", MessageBoxButtons.YesNo) Then
                            If Check_console <> 0 Then
                                Pc_or_console = " to Console "
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                Multiconverter.KitConfig_toConsole(Memory_stream)
                                Grabar_to_console(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Console Format")
                                End If

                            Else
                                Pc_or_console = " to Pc "
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                Multiconverter.KitConfig_toPc(Memory_stream)
                                Grabar_to_Pc(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Pc Format")
                                End If


                            End If

                        ElseIf Path.GetFileName(archivo) = "RefereeColor.bin" Then
                            Stream_file.Position = 85
                            Pc_or_console = " to Console "
                            Dim Check_console As Byte = Stream_file.ReadByte
                            If Check_console <> 0 Then
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                Multiconverter.Unicolor(Memory_stream)
                                 Grabar_to_console(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Console Format")
                                End If
                            Else
                                Dim Memory_stream As New MemoryStream
                                Pc_or_console = " to Pc "
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                Multiconverter.Unicolor(Memory_stream)
                                Grabar_to_Pc(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Pc Format")
                                End If


                            End If

                        ElseIf Path.GetFileName(archivo) = "BadgeData.bin" Then
                            Pc_or_console = " to Console "
                            Stream_file.Position = 0
                            Dim Check_console As Byte = Stream_file.ReadByte
                            If Check_console <> 0 Then
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                Multiconverter.BadgeData(Memory_stream)
                                Grabar_to_console(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Console Format")
                                End If
                            Else
                                Pc_or_console = " to Pc "
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                Multiconverter.BadgeData(Memory_stream)
                                Grabar_to_Pc(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Pc Format")
                                End If


                            End If
                            'Or Path.GetExtension(archivo) = ".unzlib"
                        ElseIf Path.GetExtension(archivo) = ".model" Then
                            Stream_file.Position = 8
                            Dim Check_console As Byte = Stream_file.ReadByte
                            If Check_console <> 0 Then
                                Pc_or_console = " to Console "
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                Multiconverter.model_ToConsole(Memory_stream)
                                Grabar_to_console(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Console Format")
                                End If

                            Else
                                Pc_or_console = " to Pc "
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                'Multiconverter.model_ToPc(Memory_stream)
                                MsgBox("Doesn´t work to pc yet, sorry")
                                Grabar_to_Pc(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Pc Format")
                                End If


                            End If

                        ElseIf Path.GetExtension(archivo) = ".mtl" Then
                            Pc_or_console = " to Console "
                            Stream_file.Position = 8
                            Dim Check_console As Byte = Stream_file.ReadByte
                            If Check_console <> 0 Then
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)

                                Grabar_to_console(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Console Format")
                                End If

                            Else
                                Pc_or_console = " to Pc "
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)

                                Grabar_to_Pc(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Pc Format")
                                End If


                            End If

                        ElseIf Path.GetExtension(archivo) = ".dds" Then
                            Pc_or_console = " to Console "
                            Stream_file.Position = 8
                            Dim Check_console As Byte = Stream_file.ReadByte
                            If Check_console <> 0 Then
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)

                                zlib1.zlib_memstream_to_console(Memory_stream, archivo)

                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Console Format")
                                End If

                            Else
                                Pc_or_console = " to Pc "
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                'Multiconverter.model_ToPc(Memory_stream)
                                MsgBox("Doesn´t work to pc yet, sorry")
                                ' Grabar_to_Pc(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Pc Format")
                                End If
                            End If


                        ElseIf Path.GetExtension(archivo) = ".dat" Then
                            Pc_or_console = " to Console "
                            Stream_file.Position = 4
                            Dim Check_console As Byte = Stream_file.ReadByte
                            If Check_console <> 0 Then
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                Multiconverter.Dat(Memory_stream)
                                Grabar_to_console(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Console Format")
                                End If

                            Else
                                Pc_or_console = " to Pc "
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                Multiconverter.Dat(Memory_stream)
                                Grabar_to_Pc(Memory_stream, Stream_file.Name)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Pc Format")
                                End If


                            End If

                        ElseIf Path.GetFileName(archivo) = "audiarea.bin" Then
                            Pc_or_console = " to Console "
                            Stream_file.Position = 0
                            Dim Check_console As Byte = Stream_file.ReadByte
                            If Check_console <> 0 Then
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                Multiconverter.audiarea_to_console(Memory_stream)
                                zlib1.zlib_memstream_to_console(Memory_stream, archivo)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Console Format")
                                End If
                            Else
                                Pc_or_console = " to Pc "
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                MsgBox("Doesn´t work to pc yet, sorry")
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Pc Format")
                                End If


                            End If


                        ElseIf Path.GetFileName(archivo) = "TeamColor.bin" Then
                            Pc_or_console = " to Console "
                            Stream_file.Position = 0
                            Dim Check_console As Byte = Stream_file.ReadByte
                            If Check_console <> 0 Then
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                Multiconverter.teamcolor(Memory_stream)
                                Grabar_to_console(Memory_stream, archivo)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Console Format")
                                End If
                            Else
                                Pc_or_console = " to Pc "
                                Dim Memory_stream As New MemoryStream
                                Stream_file.Position = 0
                                Stream_file.CopyTo(Memory_stream)
                                Multiconverter.teamcolor(Memory_stream)
                                Grabar_to_Pc(Memory_stream, archivo)
                                Stream_file.Close()
                                Memory_stream.Close()
                                If OpenPes.FileNames.Count = 1 Then
                                    MsgBox(Path.GetFileName(archivo).ToString & " Succefully Converted to Pc Format")
                                End If


                            End If

                        Else
                            If OpenPes.FileNames.Count = 1 Then
                                MsgBox("File Must be size 0 or corrupt Or not a Pes 2014 database file")
                            End If
                            Contador_errores += 1

                        End If
                    Else
                        If OpenPes.FileNames.Count = 1 Then
                            MsgBox("File Must be size 0 or corrupt")
                        End If

                        Contador_errores += 1

                    End If

                Catch Ex As Exception
                    If OpenPes.FileNames.Count = 1 Then
                        MsgBox("Unknown error: " + Ex.ToString)
                    End If
                    Contador_errores += 1

                End Try
                ProgressBar1.Increment(1)
            Next archivo


        Else
            MsgBox("No File Selected!!!!!")
        End If

        If OpenPes.FileNames.Count <> 1 And OpenPes.FileNames.Count <> 0 And Pc_or_console <> "UnkNown Format" Then

            MsgBox((OpenPes.FileNames.Count - Contador_errores).ToString & " Files Succefully Converted " & Pc_or_console & " Format." & vbCrLf & Contador_errores.ToString & " Files Was Sized 0 or corrupt from a total of " & OpenPes.FileNames.Count.ToString & " Files.")

        End If
    End Sub

    Public Shared Sub Grabar_to_Pc(ByRef Memory_stream As MemoryStream, ByRef archivo As String)
        Dim archivo_out As String
        archivo_out = Path.GetDirectoryName(archivo) + "\Converted to Pc\" + Path.GetFileName(archivo)
        If (Not System.IO.Directory.Exists(Path.GetDirectoryName(archivo_out))) Then
            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(archivo_out))
        End If
        If File.Exists(archivo_out) Then
            System.IO.File.Delete(archivo_out)
        End If
        Dim file_converted As FileStream = New FileStream(archivo_out, FileMode.OpenOrCreate, FileAccess.Write)
        Memory_stream.Position = 0
        Memory_stream.CopyTo(file_converted)
        file_converted.Close()


    End Sub

    Public Shared Sub Grabar_to_console(ByRef Memory_stream As MemoryStream, ByRef archivo As String)
        Dim archivo_out As String
        archivo_out = Path.GetDirectoryName(archivo) + "\Converted to Console\" + Path.GetFileName(archivo)
        If (Not System.IO.Directory.Exists(Path.GetDirectoryName(archivo_out))) Then
            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(archivo_out))
        End If
        If File.Exists(archivo_out) Then
            System.IO.File.Delete(archivo_out)
        End If
        Dim file_converted As FileStream = New FileStream(archivo_out, FileMode.OpenOrCreate, FileAccess.Write)
        Memory_stream.Position = 0
        Memory_stream.CopyTo(file_converted)

        file_converted.Close()
        Memory_stream.Close()

    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        OpenPes = Me.OpenFileDialog1
        OpenPes.Title = "Open A Pes 2014 Edit.bin file (pc or PS3)"
        OpenPes.Filter = "*.bin (*.bin)|*.bin"
        OpenPes.Multiselect = False
        If OpenPes.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            ProgressBar1.Minimum = 1
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = 10
            Dim Stream_file As FileStream = File.Open(OpenPes.FileName, FileMode.Open, FileAccess.Read)
            Of_converter.PS3_toPc(Stream_file)

        Else
            MsgBox("Select a File!")

        End If
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        OpenPes = Me.OpenFileDialog1
        OpenPes.Title = "Open A Pes 2014 uncompressed file"
        OpenPes.Filter = "*.unzlib (*.unzlib)|*.unzlib"
        OpenPes.Multiselect = True

        If OpenPes.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Dim number_of_files As Integer = OpenPes.FileNames.Count

            ProgressBar1.Minimum = 1
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = number_of_files

            For Each Me.archivo In OpenPes.FileNames




                Try
                    Dim Stream_file As FileStream = File.Open(archivo, FileMode.Open, FileAccess.Read)
                    If (Stream_file IsNot Nothing) Then

                        Dim src As Byte() = New Byte(Stream_file.Length) {}
                        Stream_file.Read(src, 0, Stream_file.Length)

                        Dim Destlen As UInt32 = Stream_file.Length * 2
                        Dim Dest(0 To Destlen) As Byte
                        Dim Retval As UInt32 = zlib1.compress2(Dest, Destlen, src, Stream_file.Length, 9)
                        If (Retval = 0) Then
                            Dim Filename As String = Path.GetFileNameWithoutExtension(archivo)
                            If File.Exists(Path.GetDirectoryName(archivo) + "\" + Filename) Then
                                System.IO.File.Delete(Path.GetDirectoryName(archivo) + "\" + Filename)

                            End If
                            Dim file_zlib = New FileStream(Path.GetDirectoryName(archivo) + "\" + Filename, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                            Dim Grabar As New BinaryWriter(file_zlib)
                            file_zlib.WriteByte(&H2)
                            file_zlib.WriteByte(&H10)
                            file_zlib.WriteByte(&H81)
                            Grabar.Write("WESYS", 0, 5)
                            Destlen = swaps.swap32(Destlen)
                            Dim uncomp_length As UInt32 = swaps.swap32(Stream_file.Length)
                            Grabar.Write(Destlen)
                            Destlen = swaps.swap32(Destlen)
                            Grabar.Write(uncomp_length)
                            Grabar.Write(Dest, 0, Destlen)
                            file_zlib.Close()
                            Stream_file.Close()

                            'MsgBox("Succesfully Compressed to Console Format")
                            correctos = correctos + 1
                        End If
                    Else
                        'MsgBox("UnKnown Error")
                        incorrectos = incorrectos + 1
                        Stream_file.Close()

                    End If

                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try
            Next archivo
            ProgressBar1.Increment(1)

        End If

        MsgBox(correctos.ToString + " zlib Succesfully to Ps3 format " + vbCrLf + incorrectos.ToString + " Failed to zlib (Maybe size 0?)")
        correctos = 0
        incorrectos = 0
    End Sub

    Public Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        OpenPes = Me.OpenFileDialog1
        OpenPes.Title = "Open A Pes 2014 Edit.bin file (pc or PS3)"
        OpenPes.Filter = "*.bin (*.bin)|*.bin"
        OpenPes.Multiselect = False
        If OpenPes.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Try
                Stream_para_version = File.Open(OpenPes.FileName, FileMode.Open, FileAccess.ReadWrite)
                Dim leer As New BinaryReader(Stream_para_version)

                Dim Select_type As Integer = Stream_para_version.Length
                NumericUpDown1.Visible = True
                NumericUpDown2.Visible = True
                NumericUpDown3.Visible = True
                Label2.Visible = True
                Label3.Visible = True
                Label4.Visible = True
                Label5.Visible = True
                Button10.Visible = True
                Button9.Visible = False
                Button11.Visible = True



                Select Case Select_type
                    Case 4726040
                        MsgBox("Ps3 Option file detected")
                        leer.BaseStream.Position = 12
                        NumericUpDown1.Value = swaps.swap32(leer.ReadUInt32)
                        NumericUpDown2.Value = swaps.swap32(leer.ReadUInt32)
                        leer.BaseStream.Position = 52
                        NumericUpDown3.Value = swaps.swap16(leer.ReadUInt16)

                    Case 4799602
                        leer.BaseStream.Position = 384

                        Dim Pc_or_console As Byte = leer.ReadByte
                        If Pc_or_console = &H0 Then
                            MsgBox("Xbox360 Option file detected")
                            leer.BaseStream.Position = 73174
                            NumericUpDown1.Value = swaps.swap32(leer.ReadUInt32)
                            NumericUpDown2.Value = swaps.swap32(leer.ReadUInt32)
                            leer.BaseStream.Position = 73214
                            NumericUpDown3.Value = swaps.swap16(leer.ReadUInt16)

                        Else
                            MsgBox("Pc Option file detected")
                            leer.BaseStream.Position = 73174
                            NumericUpDown1.Value = leer.ReadUInt32
                            NumericUpDown2.Value = leer.ReadUInt32
                            leer.BaseStream.Position = 73214
                            NumericUpDown3.Value = leer.ReadUInt16
                        End If



                    Case Else
                        NumericUpDown1.Visible = False
                        NumericUpDown2.Visible = False
                        NumericUpDown3.Visible = False
                        Label2.Visible = False
                        Label3.Visible = False
                        Label4.Visible = False
                        Label5.Visible = False
                        Button10.Visible = False
                        Button9.Visible = True
                        Button11.Visible = False

                        Stream_para_version.Close()
                        MsgBox("UnkNown Edit.bin")



                End Select



            Catch ex As Exception
                MsgBox(ex.ToString)

            End Try



        Else
            MsgBox("Select a File!")

        End If


    End Sub

    Public Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        Dim leer As New BinaryReader(Stream_para_version)
        Dim Grabar As New BinaryWriter(Stream_para_version)
        Dim Select_type As Integer = Stream_para_version.Length


        Select Case Select_type
            Case 4726040
                leer.BaseStream.Position = 12
                Dim Game_version As UInt32 = NumericUpDown1.Value
                Dim Dlc_version As UInt32 = NumericUpDown2.Value
                Dim Number_of_players As UInt16 = NumericUpDown3.Value
                Grabar.Write(swaps.swap32(Game_version))
                leer.BaseStream.Position = 44
                Grabar.Write(swaps.swap32(Game_version))

                leer.BaseStream.Position = 16
                Grabar.Write(swaps.swap32(Dlc_version))
                leer.BaseStream.Position = 20
                Grabar.Write(swaps.swap32(Dlc_version))
                leer.BaseStream.Position = 48
                Grabar.Write(swaps.swap32(Dlc_version))


                leer.BaseStream.Position = 52
                Grabar.Write(swaps.swap16(Number_of_players))

            Case 4799602
                leer.BaseStream.Position = 384
                Dim Game_version As UInt32 = NumericUpDown1.Value
                Dim Dlc_version As UInt32 = NumericUpDown2.Value
                Dim Number_of_players As UInt16 = NumericUpDown3.Value
                Dim Pc_or_console As Byte = leer.ReadByte
                If Pc_or_console = &H0 Then
                    leer.BaseStream.Position = 73174

                    Grabar.Write(swaps.swap32(Game_version))
                    leer.BaseStream.Position = 73206
                    Grabar.Write(swaps.swap32(Game_version))

                    leer.BaseStream.Position = 73178
                    Grabar.Write(swaps.swap32(Dlc_version))
                    leer.BaseStream.Position = 73182
                    Grabar.Write(swaps.swap32(Dlc_version))
                    leer.BaseStream.Position = 73210
                    Grabar.Write(swaps.swap32(Dlc_version))


                    leer.BaseStream.Position = 73214
                    Grabar.Write(swaps.swap16(Number_of_players))


                Else
                    
                    leer.BaseStream.Position = 73174

                    Grabar.Write(Game_version)
                    leer.BaseStream.Position = 73206
                    Grabar.Write(Game_version)

                    leer.BaseStream.Position = 73178
                    Grabar.Write(Dlc_version)
                    leer.BaseStream.Position = 73182
                    Grabar.Write(Dlc_version)
                    leer.BaseStream.Position = 73210
                    Grabar.Write(Dlc_version)


                    leer.BaseStream.Position = 73214
                    Grabar.Write(Number_of_players)

                End If


        End Select

        NumericUpDown1.Visible = False
        NumericUpDown2.Visible = False
        NumericUpDown3.Visible = False
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False
        Button10.Visible = False
        Button9.Visible = True
        Button11.Visible = False
        MsgBox("Version succesfully changed")
        Stream_para_version.Close()

    End Sub

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        Dim leer As New BinaryReader(Stream_para_version)
        Dim Select_type As Integer = Stream_para_version.Length


        Select Case Select_type
            Case 4726040
                leer.BaseStream.Position = 76
                Dim contador As UInteger = 0
                While leer.ReadUInt32 <> 0
                    contador += 1
                    leer.BaseStream.Position += 104
                End While
                NumericUpDown3.Value = contador
                MsgBox(contador.ToString + " Players found")

            Case 4799602
                leer.BaseStream.Position = 384

                Dim Pc_or_console As Byte = leer.ReadByte
                If Pc_or_console = &H0 Then
                    leer.BaseStream.Position = 73238
                    Dim contador As UInteger = 0
                    While leer.ReadUInt32 <> 0
                        contador += 1
                        leer.BaseStream.Position += 104
                    End While
                    NumericUpDown3.Value = contador
                    MsgBox(contador.ToString + " Players found")
                Else
                    leer.BaseStream.Position = 73238
                    Dim contador As UInteger = 0
                    While leer.ReadUInt32 <> 0
                        contador += 1
                        leer.BaseStream.Position += 104
                    End While
                    NumericUpDown3.Value = contador
                    MsgBox(contador.ToString + " Players found")
                End If


        End Select

        

    End Sub

   
    Private Sub Button12_Click(sender As System.Object, e As System.EventArgs) Handles Button12.Click
        Dim openfolder As New FolderBrowserDialog
        Dim Files As New ArrayList, File As Object, Raiz As String
        If openfolder.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Raiz = openfolder.SelectedPath


            Try
                For Each f In Directory.GetFiles(Raiz)
                    Files.Add(f)
                Next

            Catch ex As System.Exception
                MsgBox(ex)
            End Try

            

            Try

                DirSearch(Raiz, Files)

            Catch ex As System.Exception
                MsgBox(ex)
            End Try

            If System.IO.File.Exists(Raiz + "\Log.txt") Then
                System.IO.File.Delete(Raiz + "\Log.txt")
            End If

            Dim Log_file As New FileStream(Raiz + "\Log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite)
            Dim Log_file_writer As New StreamWriter(Log_file)





            If Files.Count > 0 Then
                Dim Contador_errores As Integer = 0
                Dim Pc_or_console As String = "UnkNown Format"

                ProgressBar1.Minimum = 1
                ProgressBar1.Value = 1
                ProgressBar1.Maximum = Files.Count



                For Each File In Files

                    archivo = File

                    Try

                        Dim Stream_file As FileStream = System.IO.File.Open(archivo, FileMode.Open, FileAccess.ReadWrite)
                        If (Stream_file IsNot Nothing And Stream_file.Length <> 0) Then
                            Dim Leer As New BinaryReader(Stream_file)
                            Leer.ReadBytes(Stream_file.Length)
                            Leer.BaseStream.Position = 2
                            Dim CHECK_HALFZLIB As Byte = Leer.ReadByte

                            Leer.BaseStream.Position = 3
                            Dim CheckPesFile As Integer = &H59534557
                            Dim unzlib_memstream As New MemoryStream

                            If Leer.ReadUInt32 = CheckPesFile Then
                                Leer.BaseStream.Position = 0
                                Dim Check_pc_console As Byte = Leer.ReadByte
                                Dim Check_Console As Byte = &H1


                                If Check_pc_console = &HFF Then



                                    If Check_pc_console = &HFF And Check_FF = 3 Then

                                        Dim Result As DialogResult = MsgBox("If It´s a Xbox360 Folder hit Yes, In Pc Case hit No.", MsgBoxStyle.YesNoCancel, "Select type")


                                        If Result = DialogResult.Yes Then
                                            Check_FF = &H1
                                            Check_pc_console = Check_FF
                                        ElseIf Result = MsgBoxResult.No Then
                                            Check_FF = &H0
                                            Check_pc_console = Check_FF
                                        Else

                                            Check_FF = &HFF
                                            Check_pc_console = Check_FF

                                        End If
                                    ElseIf Check_pc_console = &HFF And Check_FF = 1 Then
                                        Check_pc_console = Check_Console

                                    ElseIf Check_pc_console = &HFF And Check_FF = 0 Then
                                        Check_pc_console = 0

                                    Else
                                        MsgBox("Error, Unknown compresion")

                                    End If

                                End If


                                Dim extension As String = Path.GetExtension(archivo)




                                If Check_pc_console = Check_Console Then

                                   
                                        If CHECK_HALFZLIB <> 0 Then
                                        unzlib_memstream = zlib1.unzlibconsole_to_MemStream(Stream_file)
                                    Else
                                        Leer.BaseStream.Position = 16
                                        Dim buffer As Byte() = Leer.ReadBytes(Stream_file.Length - 16)
                                        unzlib_memstream.Write(buffer, 0, buffer.Length)
                                    End If
                                    Stream_file.Close()
                                    Leer.Close()
                                    Pc_or_console = "To Pc "
                                    Dim Nombre As String = Path.GetFileName(archivo)


                                    Select Case Nombre

                                        Case "Ball.bin", "Ball1.bin", "Ball2.bin", "Ball3.bin", "Ball4.bin", "Ball5.bin"
                                            converter.ball(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "BallCondition.bin", "BallCondition1.bin", "BallCondition2.bin", "BallCondition3.bin", "BallCondition4.bin", "BallCondition5.bin"
                                            converter.BallCondition_topc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Boots.bin", "Boots1.bin", "Boots2.bin", "Boots3.bin", "Boots4.bin", "Boots5.bin"
                                            converter.Boots(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Coach.bin", "Coach1.bin", "Coach2.bin", "Coach3.bin", "Coach4.bin", "Coach5.bin"
                                            converter.Coach_toPc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()
                                        Case "Competition.bin", "Competition1.bin", "Competition2.bin", "Competition3.bin", "Competition4.bin", "Competition5.bin"
                                            converter.Competition_toPc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "CompetitionEntry1.bin", "CompetitionEntry2.bin", "CompetitionEntry.bin", "CompetitionEntry3.bin", "CompetitionEntry4.bin", "CompetitionEntry5.bin"
                                            converter.CompetitionEntry_toPc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "CompetitionKind1.bin", "CompetitionKind2.bin", "CompetitionKind.bin", "CompetitionKind3.bin", "CompetitionKind4.bin", "CompetitionKind5.bin"
                                            converter.CompetitionKind_to_pc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "CompetitionRegulation.bin", "CompetitionRegulation1.bin", "CompetitionRegulation2.bin", "CompetitionRegulation3.bin", "CompetitionRegulation4.bin", "CompetitionRegulation5.bin"
                                            converter.CompetitionRegulation_toPc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Country.bin", "Country1.bin", "Country2.bin", "Country3.bin", "Country4.bin", "Country5.bin"
                                            converter.Country_toPc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Derby.bin", "Derby1.bin", "Derby2.bin", "Derby3.bin", "Derby4.bin", "Derby5.bin"
                                            converter.Derby_toPc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "InstallVersionBallDlcAs.bin", "InstallVersionBallDlcEu.bin", "InstallVersionBallDlcJp.bin", "InstallVersionBallDlcUs.bin", "InstallVersionBallDp.bin"
                                            converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "InstallVersionBootsDlcAs.bin", "InstallVersionBootsDlcEu.bin", "InstallVersionBootsDlcJp.bin", "InstallVersionBootsDlcUs.bin", "InstallVersionBootsDp.bin"
                                            converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "InstallVersionStadiumDlcAs.bin", "InstallVersionStadiumDlcEu.bin", "InstallVersionStadiumDlcJp.bin", "InstallVersionStadiumDlcUs.bin", "InstallVersionStadiumDp.bin"
                                            converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Player.bin", "Player1.bin", "Player2.bin", "Player3.bin", "Player4.bin", "Player5.bin"
                                            converter.Player_toPc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "PlayerAssignment.bin", "PlayerAssignment1.bin", "PlayerAssignment2.bin", "PlayerAssignment3.bin", "PlayerAssignment4.bin", "PlayerAssignment5.bin"
                                            converter.PlayerAssignment_toPc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "SpecialPlayerAssignment.bin", "SpecialPlayerAssignment1.bin", "SpecialPlayerAssignment2.bin", "SpecialPlayerAssignment3.bin", "SpecialPlayerAssignment4.bin", "SpecialPlayerAssignment5.bin"
                                            converter.SpecialPlayerAssignment(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "SpecialPlayerAssignmentKind.bin", "SpecialPlayerAssignmentKind1.bin", "SpecialPlayerAssignmentKind2.bin", "SpecialPlayerAssignmentKind3.bin", "SpecialPlayerAssignmentKind4.bin", "SpecialPlayerAssignmentKind5.bin"
                                            converter.SpecialPlayerAssignmentKind(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Stadium.bin", "Stadium1.bin", "Stadium2.bin", "Stadium3.bin", "Stadium4.bin", "Stadium5.bin"
                                            converter.Stadium_toPc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "StadiumOrder.bin", "StadiumOrder1.bin", "StadiumOrder2.bin", "StadiumOrder3.bin", "StadiumOrder4.bin", "StadiumOrder5.bin"
                                            converter.StadiumOrder_toPc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "StadiumOrderInConfederation.bin", "StadiumOrderInConfederation1.bin", "StadiumOrderInConfederation2.bin", "StadiumOrderInConfederation3.bin", "StadiumOrderInConfederation4.bin", "StadiumOrderInConfederation5.bin"
                                            converter.StadiumOrderInConfederation_toPc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Team.bin", "Team1.bin", "Team2.bin", "Team3.bin", "Team4.bin", "Team5.bin"
                                            converter.Team_toPc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "UniColor.bin"
                                            Multiconverter.Unicolor(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Spike.bin"
                                            Multiconverter.Spike(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()


                                        Case Else
                                            Select Case extension

                                                Case ".dds", ".mtl", ".DDS", ".xml", ".json", ".irv", ".lmn", ".usm"
                                                    unzlib_memstream.Position = 0
                                                    zlib1.zlib_memstream_to_pc_overwriting(unzlib_memstream, archivo)
                                                    unzlib_memstream.Close()

                                                Case ".model"

                                                    unzlib_memstream.Close()
                                                    Contador_errores += 1
                                                    Log_file_writer.WriteLine(archivo)

                                                Case Else


                                                    Contador_errores += 1
                                                    Log_file_writer.WriteLine(archivo)
                                            End Select

                                    End Select



                                ElseIf Check_pc_console = &H0 Then

                                    If CHECK_HALFZLIB <> 0 Then
                                        unzlib_memstream = zlib1.unzlibPc_to_Memstream(Stream_file)
                                    Else
                                        Leer.BaseStream.Position = 16
                                        Dim buffer As Byte() = Leer.ReadBytes(Stream_file.Length - 16)
                                        unzlib_memstream.Write(buffer, 0, buffer.Length)
                                    End If

                                    ' unzlib_memstream = zlib1.unzlibPc_to_Memstream(Stream_file)
                                    Stream_file.Close()
                                    Leer.Close()
                                    Pc_or_console = " to Console "
                                    Dim Nombre As String = Path.GetFileName(archivo)

                                    Select Case Nombre

                                        Case "Ball.bin", "Ball1.bin", "Ball2.bin", "Ball3.bin", "Ball4.bin", "Ball5.bin"
                                            converter.ball(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "BallCondition.bin", "BallCondition1.bin", "BallCondition2.bin", "BallCondition3.bin", "BallCondition4.bin", "BallCondition5.bin"
                                            converter.BallCondition_toconsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Boots.bin", "Boots1.bin", "Boots2.bin", "Boots3.bin", "Boots4.bin", "Boots5.bin"
                                            converter.Boots(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Coach.bin", "Coach1.bin", "Coach2.bin", "Coach3.bin", "Coach4.bin", "Coach5.bin"
                                            converter.Coach_toConsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Competition.bin", "Competition1.bin", "Competition2.bin", "Competition3.bin", "Competition4.bin", "Competition5.bin"
                                            converter.Competition_toConsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "CompetitionEntry1.bin", "CompetitionEntry2.bin", "CompetitionEntry.bin", "CompetitionEntry3.bin", "CompetitionEntry4.bin", "CompetitionEntry5.bin"
                                            converter.CompetitionEntry_toConsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "CompetitionKind1.bin", "CompetitionKind2.bin", "CompetitionKind.bin", "CompetitionKind3.bin", "CompetitionKind4.bin", "CompetitionKind5.bin"
                                            converter.CompetitionKind_to_console(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "CompetitionRegulation.bin", "CompetitionRegulation1.bin", "CompetitionRegulation2.bin", "CompetitionRegulation3.bin", "CompetitionRegulation4.bin", "CompetitionRegulation5.bin"
                                            converter.CompetitionRegulation_toConsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Country.bin", "Country1.bin", "Country2.bin", "Country3.bin", "Country4.bin", "Country5.bin"
                                            converter.Country_toConsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Derby.bin", "Derby1.bin", "Derby2.bin", "Derby3.bin", "Derby4.bin", "Derby5.bin"
                                            converter.Derby_toConsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "InstallVersionBallDlcAs.bin", "InstallVersionBallDlcEu.bin", "InstallVersionBallDlcJp.bin", "InstallVersionBallDlcUs.bin", "InstallVersionBallDp.bin"
                                            converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "InstallVersionBootsDlcAs.bin", "InstallVersionBootsDlcEu.bin", "InstallVersionBootsDlcJp.bin", "InstallVersionBootsDlcUs.bin", "InstallVersionBootsDp.bin"
                                            converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "InstallVersionStadiumDlcAs.bin", "InstallVersionStadiumDlcEu.bin", "InstallVersionStadiumDlcJp.bin", "InstallVersionStadiumDlcUs.bin", "InstallVersionStadiumDp.bin"
                                            converter.InstallVersion_Ball_Boots_Stadium_Dlc(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Player.bin", "Player1.bin", "Player2.bin", "Player3.bin", "Player4.bin", "Player5.bin"
                                            converter.Player_toConsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "PlayerAssignment.bin", "PlayerAssignment1.bin", "PlayerAssignment2.bin", "PlayerAssignment3.bin", "PlayerAssignment4.bin", "PlayerAssignment5.bin"
                                            converter.PlayerAssignment_toConsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "SpecialPlayerAssignment.bin", "SpecialPlayerAssignment1.bin", "SpecialPlayerAssignment2.bin", "SpecialPlayerAssignment3.bin", "SpecialPlayerAssignment4.bin", "SpecialPlayerAssignment5.bin"
                                            converter.SpecialPlayerAssignment(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "SpecialPlayerAssignmentKind.bin", "SpecialPlayerAssignmentKind1.bin", "SpecialPlayerAssignmentKind2.bin", "SpecialPlayerAssignmentKind3.bin", "SpecialPlayerAssignmentKind4.bin", "SpecialPlayerAssignmentKind5.bin"
                                            converter.SpecialPlayerAssignmentKind(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Stadium.bin", "Stadium1.bin", "Stadium2.bin", "Stadium3.bin", "Stadium4.bin", "Stadium5.bin"
                                            converter.Stadium_toConsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "StadiumOrder.bin", "StadiumOrder1.bin", "StadiumOrder2.bin", "StadiumOrder3.bin", "StadiumOrder4.bin", "StadiumOrder5.bin"
                                            converter.StadiumOrder_toConsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "StadiumOrderInConfederation.bin", "StadiumOrderInConfederation1.bin", "StadiumOrderInConfederation2.bin", "StadiumOrderInConfederation3.bin", "StadiumOrderInConfederation4.bin", "StadiumOrderInConfederation5.bin"
                                            converter.StadiumOrderInConfederation_toConsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Team.bin", "Team1.bin", "Team2.bin", "Team3.bin", "Team4.bin", "Team5.bin"
                                            converter.Team_toConsole(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "UniColor.bin"
                                            Multiconverter.Unicolor(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "Spike.bin"
                                            Multiconverter.Spike(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()

                                        Case "audiarea.bin"
                                            Multiconverter.audiarea_to_console(unzlib_memstream)
                                            unzlib_memstream.Position = 0
                                            zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                            unzlib_memstream.Close()


                                        Case Else

                                            Select Case extension

                                                Case ".dds", ".mtl", ".DDS", ".xml", ".json", ".irv", ".lmn", ".usm"
                                                    unzlib_memstream.Position = 0
                                                    zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                                    unzlib_memstream.Close()

                                                Case ".model"
                                                    Multiconverter.model_ToConsole(unzlib_memstream)
                                                    unzlib_memstream.Position = 0
                                                    zlib1.zlib_memstream_to_console_overwriting(unzlib_memstream, archivo)
                                                    unzlib_memstream.Close()
                                                    Dim Stream_81 As FileStream = System.IO.File.Open(archivo, FileMode.Open, FileAccess.ReadWrite)
                                                    Stream_81.Position = 2
                                                    Stream_81.WriteByte(&H1)
                                                    Stream_81.Close()


                                                Case Else

                                                    Contador_errores += 1
                                                    Log_file_writer.WriteLine(archivo)
                                            End Select


                                    End Select

                                End If

                                ElseIf Stream_file.Length = 120 Then

                                        Stream_file.Position = 0
                                        Dim Check_console_unzlib As Byte = Stream_file.ReadByte
                                        'MessageBox.Show("Is A Kit Config File?", "Pes 2014 Multiconverter", MessageBoxButtons.YesNo) Then
                                        If Check_console_unzlib <> 0 Then
                                            Pc_or_console = " to Console "
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Multiconverter.KitConfig_toConsole(Memory_stream)
                                            Grabar_to_console_overwriting(Memory_stream, Stream_file.Name)
                                            Memory_stream.Close()

                                        Else
                                            Pc_or_console = " to Pc "
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Multiconverter.KitConfig_toPc(Memory_stream)
                                            Grabar_to_Pc_overwriting(Memory_stream, Stream_file.Name)
                                            Memory_stream.Close()

                                        End If

                                ElseIf Path.GetFileName(archivo) = "RefereeColor.bin" Then
                                        Stream_file.Position = 85
                                        Pc_or_console = " to Console "
                                        Dim Check_console_unzlib As Byte = Stream_file.ReadByte
                                        If Check_console_unzlib <> 0 Then
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Multiconverter.Unicolor(Memory_stream)
                                            Grabar_to_console_overwriting(Memory_stream, Stream_file.Name)
                                            Memory_stream.Close()

                                        Else
                                            Dim Memory_stream As New MemoryStream
                                            Pc_or_console = " to Pc "
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Multiconverter.Unicolor(Memory_stream)
                                            Grabar_to_Pc_overwriting(Memory_stream, Stream_file.Name)

                                            Memory_stream.Close()

                                        End If

                                ElseIf Path.GetFileName(archivo) = "BadgeData.bin" Then
                                        Pc_or_console = " to Console "
                                        Stream_file.Position = 0
                                        Dim Check_console_unzlib As Byte = Stream_file.ReadByte
                                        If Check_console_unzlib <> 0 Then
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Multiconverter.BadgeData(Memory_stream)
                                            Grabar_to_console_overwriting(Memory_stream, Stream_file.Name)

                                            Memory_stream.Close()
                                        Else
                                            Pc_or_console = " to Pc "
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Multiconverter.BadgeData(Memory_stream)
                                            Grabar_to_Pc_overwriting(Memory_stream, Stream_file.Name)

                                            Memory_stream.Close()

                                        End If
                                        'Or Path.GetExtension(archivo) = ".unzlib"
                                ElseIf Path.GetExtension(archivo) = ".model" Then
                                        Stream_file.Position = 8
                                        Dim Check_console_unzlib As Byte = Stream_file.ReadByte
                                        If Check_console_unzlib <> 0 Then
                                            Pc_or_console = " to Console "
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Multiconverter.model_ToConsole(Memory_stream)
                                            Grabar_to_console_overwriting(Memory_stream, Stream_file.Name)

                                            Memory_stream.Close()
                                        Else
                                            Pc_or_console = " to Pc "
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            'Multiconverter.model_ToPc(Memory_stream)
                                            'MsgBox("Doesn´t work to pc yet, sorry")
                                            'Grabar_to_Pc_overwriting(Memory_stream, Stream_file.Name)
                                            Stream_file.Close()
                                            Memory_stream.Close()
                                            Contador_errores += 1
                                            Log_file_writer.WriteLine(archivo)

                                        End If

                                ElseIf Path.GetExtension(archivo) = ".mtl" Or Path.GetExtension(archivo) = ".mtl" Or Path.GetExtension(archivo) = ".xml" Or Path.GetExtension(archivo) = ".json" Or Path.GetExtension(archivo) = ".irv" Or Path.GetExtension(archivo) = ".lmn" Or Path.GetExtension(archivo) = ".usm" Then
                                        Pc_or_console = " to Console "
                                        Stream_file.Position = 8
                                        Dim Check_console As Byte = Stream_file.ReadByte
                                        If Check_console <> 0 Then
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Grabar_to_console_overwriting(Memory_stream, Stream_file.Name)

                                            Memory_stream.Close()

                                        Else
                                            Pc_or_console = " to Pc "
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Grabar_to_Pc_overwriting(Memory_stream, Stream_file.Name)

                                            Memory_stream.Close()


                                        End If

                                ElseIf Path.GetExtension(archivo) = ".dds" Or Path.GetExtension(archivo) = ".DDS" Then
                                        Pc_or_console = " to Console "
                                        Stream_file.Position = 8
                                        Dim Check_console As Byte = Stream_file.ReadByte
                                        If Check_console <> 0 Then
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            zlib1.zlib_memstream_to_console_overwriting(Memory_stream, archivo)


                                            Memory_stream.Close()

                                        Else
                                            Pc_or_console = " to Pc "
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            'Multiconverter.model_ToPc(Memory_stream)
                                            'MsgBox("Doesn´t work to pc yet, sorry")
                                            ' Grabar_to_Pc_overwriting(Memory_stream, Stream_file.Name)
                                            Stream_file.Close()
                                            Memory_stream.Close()

                                            Contador_errores += 1
                                            Log_file_writer.WriteLine(archivo)
                                        End If


                                ElseIf Path.GetExtension(archivo) = ".dat" Then
                                        Pc_or_console = " to Console "
                                        Stream_file.Position = 4
                                        Dim Check_console As Byte = Stream_file.ReadByte
                                        If Check_console <> 0 Then
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Multiconverter.Dat(Memory_stream)
                                            Grabar_to_console_overwriting(Memory_stream, Stream_file.Name)

                                            Memory_stream.Close()


                                        Else
                                            Pc_or_console = " to Pc "
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Multiconverter.Dat(Memory_stream)
                                            Grabar_to_Pc_overwriting(Memory_stream, Stream_file.Name)

                                            Memory_stream.Close()


                                        End If

                                ElseIf Path.GetFileName(archivo) = "audiarea.bin" Then
                                        Pc_or_console = " to Console "
                                        Stream_file.Position = 0
                                        Dim Check_console As Byte = Stream_file.ReadByte
                                        If Check_console <> 0 Then
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Multiconverter.audiarea_to_console(Memory_stream)
                                            zlib1.zlib_memstream_to_console_overwriting(Memory_stream, archivo)

                                            Memory_stream.Close()

                                        Else
                                            Pc_or_console = " to Pc "
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            'MsgBox("Doesn´t work to pc yet, sorry")
                                            Stream_file.Close()
                                            Memory_stream.Close()

                                            Contador_errores += 1
                                            Log_file_writer.WriteLine(archivo)

                                        End If


                                ElseIf Path.GetFileName(archivo) = "TeamColor.bin" Then
                                        Pc_or_console = " to Console "
                                        Stream_file.Position = 0
                                        Dim Check_console As Byte = Stream_file.ReadByte
                                        If Check_console <> 0 Then
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Multiconverter.teamcolor(Memory_stream)
                                            Grabar_to_console_overwriting(Memory_stream, archivo)

                                            Memory_stream.Close()

                                        Else
                                            Pc_or_console = " to Pc "
                                            Dim Memory_stream As New MemoryStream
                                            Stream_file.Position = 0
                                            Stream_file.CopyTo(Memory_stream)
                                            Stream_file.Close()
                                            Multiconverter.teamcolor(Memory_stream)
                                            Grabar_to_Pc_overwriting(Memory_stream, archivo)

                                            Memory_stream.Close()


                                        End If

                                Else

                                        Dim nombre_por_si_unzlib = Path.GetFileName(archivo)

                                        Select Case nombre_por_si_unzlib

                                            Case "Ball.bin", "Ball1.bin", "Ball2.bin", "Ball3.bin", "Ball4.bin", "Ball5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 140
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.ball(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.ball(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If

                                            Case "BallCondition.bin", "BallCondition1.bin", "BallCondition2.bin", "BallCondition3.bin", "BallCondition4.bin", "BallCondition5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 2
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.BallCondition_toconsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.BallCondition_topc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If

                                            Case "Boots.bin", "Boots1.bin", "Boots2.bin", "Boots3.bin", "Boots4.bin", "Boots5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 0
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Boots(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Boots(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If

                                            Case "Coach.bin", "Coach1.bin", "Coach2.bin", "Coach3.bin", "Coach4.bin", "Coach5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 0
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Coach_toConsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Coach_toPc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If

                                            Case "Competition.bin", "Competition1.bin", "Competition2.bin", "Competition3.bin", "Competition4.bin", "Competition5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 1
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Competition_toConsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Competition_toPc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If
                                            Case "CompetitionEntry1.bin", "CompetitionEntry2.bin", "CompetitionEntry.bin", "CompetitionEntry3.bin", "CompetitionEntry4.bin", "CompetitionEntry5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 3
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.CompetitionEntry_toConsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.CompetitionEntry_toPc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If


                                            Case "CompetitionRegulation.bin", "CompetitionRegulation1.bin", "CompetitionRegulation2.bin", "CompetitionRegulation3.bin", "CompetitionRegulation4.bin", "CompetitionRegulation5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 4
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.CompetitionRegulation_toConsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.CompetitionRegulation_toPc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If

                                            Case "Country.bin", "Country1.bin", "Country2.bin", "Country3.bin", "Country4.bin", "Country5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 1284
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Country_toConsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Country_toPc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If

                                            Case "Derby.bin", "Derby1.bin", "Derby2.bin", "Derby3.bin", "Derby4.bin", "Derby5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 0
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Derby_toConsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Derby_toPc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If

                                            Case "InstallVersionBallDlcAs.bin", "InstallVersionBallDlcEu.bin", "InstallVersionBallDlcJp.bin", "InstallVersionBallDlcUs.bin", "InstallVersionBallDp.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 4
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.InstallVersion_Ball_Boots_Stadium_Dlc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.InstallVersion_Ball_Boots_Stadium_Dlc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If

                                            Case "InstallVersionBootsDlcAs.bin", "InstallVersionBootsDlcEu.bin", "InstallVersionBootsDlcJp.bin", "InstallVersionBootsDlcUs.bin", "InstallVersionBootsDp.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 4
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.InstallVersion_Ball_Boots_Stadium_Dlc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.InstallVersion_Ball_Boots_Stadium_Dlc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If
                                            Case "InstallVersionStadiumDlcAs.bin", "InstallVersionStadiumDlcEu.bin", "InstallVersionStadiumDlcJp.bin", "InstallVersionStadiumDlcUs.bin", "InstallVersionStadiumDp.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 4
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.InstallVersion_Ball_Boots_Stadium_Dlc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.InstallVersion_Ball_Boots_Stadium_Dlc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If

                                            Case "Player.bin", "Player1.bin", "Player2.bin", "Player3.bin", "Player4.bin", "Player5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 0
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Player_toConsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Player_toPc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If

                                            Case "PlayerAssignment.bin", "PlayerAssignment1.bin", "PlayerAssignment2.bin", "PlayerAssignment3.bin", "PlayerAssignment4.bin", "PlayerAssignment5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 0
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.PlayerAssignment_toConsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.PlayerAssignment_toPc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If

                                            Case "SpecialPlayerAssignment.bin", "SpecialPlayerAssignment1.bin", "SpecialPlayerAssignment2.bin", "SpecialPlayerAssignment3.bin", "SpecialPlayerAssignment4.bin", "SpecialPlayerAssignment5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 0
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.SpecialPlayerAssignment(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.SpecialPlayerAssignment(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If

                                            Case "SpecialPlayerAssignmentKind.bin", "SpecialPlayerAssignmentKind1.bin", "SpecialPlayerAssignmentKind2.bin", "SpecialPlayerAssignmentKind3.bin", "SpecialPlayerAssignmentKind4.bin", "SpecialPlayerAssignmentKind5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 1
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 128 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.SpecialPlayerAssignmentKind(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.SpecialPlayerAssignmentKind(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If
                                            Case "Stadium.bin", "Stadium1.bin", "Stadium2.bin", "Stadium3.bin", "Stadium4.bin", "Stadium5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 6
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Stadium_toConsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Stadium_toPc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If
                                            Case "StadiumOrder.bin", "StadiumOrder1.bin", "StadiumOrder2.bin", "StadiumOrder3.bin", "StadiumOrder4.bin", "StadiumOrder5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 0
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.StadiumOrder_toConsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.StadiumOrder_toPc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If
                                            Case "StadiumOrderInConfederation.bin", "StadiumOrderInConfederation1.bin", "StadiumOrderInConfederation2.bin", "StadiumOrderInConfederation3.bin", "StadiumOrderInConfederation4.bin", "StadiumOrderInConfederation5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 0
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.StadiumOrderInConfederation_toConsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.StadiumOrderInConfederation_toPc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If
                                            Case "Team.bin", "Team1.bin", "Team2.bin", "Team3.bin", "Team4.bin", "Team5.bin"
                                                Pc_or_console = " to Console "
                                                Stream_file.Position = 0
                                                Dim Check_console As Byte = Stream_file.ReadByte
                                                If Check_console <> 0 Then

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Team_toConsole(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_console_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()
                                                Else
                                                    Pc_or_console = " to Pc "

                                                    Dim Memory_stream As New MemoryStream
                                                    Stream_file.Position = 0
                                                    Stream_file.CopyTo(Memory_stream)
                                                    Stream_file.Close()
                                                    converter.Team_toPc(Memory_stream)
                                                    Memory_stream.Position = 0
                                                    Grabar_to_Pc_overwriting(Memory_stream, archivo)
                                                    Memory_stream.Close()

                                                End If


                                            Case Else
                                                Contador_errores += 1
                                                Log_file_writer.WriteLine(archivo)
                                                Stream_file.Close()
                                        End Select
                                End If


                        End If





                    Catch Ex As Exception

                        Contador_errores += 1
                        Log_file_writer.WriteLine(archivo)

                    End Try
                    ProgressBar1.Increment(1)
                    
                Next
                correctos = Files.Count - Contador_errores
                incorrectos = Files.Count - correctos
                MsgBox(correctos.ToString + " converted Succesfully  " + vbCrLf + incorrectos.ToString + " Failed to Convert " + vbCrLf + "Look At: " + Log_file.Name + " to see the failed files!")
                correctos = 0
                incorrectos = 0
                Log_file_writer.Close()
                Log_file.Close()

            Else
                MsgBox("Folder is empty")

            End If




        Else
            MsgBox("No Folder selected")
        End If

        Check_FF = 3
    End Sub

    Public Shared Sub DirSearch(ByVal sDir As String, ByRef files As ArrayList)
        Dim d As String
        Dim f As String

        Try

            For Each d In Directory.GetDirectories(sDir)
                For Each f In Directory.GetFiles(d)
                    files.Add(f)
                Next
                DirSearch(d, files)
            Next
        Catch excpt As System.Exception
            Debug.WriteLine(excpt.Message)
        End Try
    End Sub

    Public Shared Sub Grabar_to_Pc_overwriting(ByRef Memory_stream As MemoryStream, ByRef archivo As String)
       
        If File.Exists(archivo) Then
            System.IO.File.Delete(archivo)
        End If
        Dim file_converted As FileStream = New FileStream(archivo, FileMode.OpenOrCreate, FileAccess.Write)
        Memory_stream.Position = 0
        Memory_stream.CopyTo(file_converted)
        file_converted.Close()


    End Sub

    Public Shared Sub Grabar_to_console_overwriting(ByRef Memory_stream As MemoryStream, ByRef archivo As String)
       
        If File.Exists(archivo) Then
            System.IO.File.Delete(archivo)
        End If
        Dim file_converted As FileStream = New FileStream(archivo, FileMode.OpenOrCreate, FileAccess.Write)
        Memory_stream.Position = 0
        Memory_stream.CopyTo(file_converted)

        file_converted.Close()
        Memory_stream.Close()

    End Sub

    Private Sub Button13_Click(sender As System.Object, e As System.EventArgs) Handles Button13.Click
        Dim openfolder As New FolderBrowserDialog
        Dim Files As New ArrayList, Raiz As String
        If openfolder.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Raiz = openfolder.SelectedPath

            Try
                For Each f In Directory.GetFiles(Raiz)
                    Files.Add(f)
                Next

            Catch ex As System.Exception
                MsgBox(ex)
            End Try



            Try

                DirSearch(Raiz, Files)

            Catch ex As System.Exception
                MsgBox(ex)
            End Try

            Dim number_of_files As Integer = Files.Count

            ProgressBar1.Minimum = 1
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = number_of_files


            For Each Me.archivo In Files





                Try
                    Dim Stream_file As FileStream = System.IO.File.Open(archivo, FileMode.Open, FileAccess.Read)
                    If (Stream_file IsNot Nothing) Then
                        Dim Leer As New BinaryReader(Stream_file)
                        Leer.ReadBytes(Stream_file.Length)
                        Leer.BaseStream.Position = 3
                        Dim CheckPesFile As Integer = &H59534557

                        If Leer.ReadUInt32 = CheckPesFile Then
                            Leer.BaseStream.Position = 0
                            Dim Check_pc_console As Byte = Leer.ReadByte

                            Dim Check_Console As Byte = &H1
                            If Check_pc_console = Check_Console Then
                                zlib1.unzlibconsole(Stream_file, archivo)
                                Stream_file.Close()
                                'MsgBox("Console File Unzlibed Succesfully")
                                correctos = correctos + 1

                            ElseIf Check_pc_console = &H0 Or Check_pc_console = &HFF Then

                                zlib1.unzlibpc(Stream_file, archivo)
                                Stream_file.Close()
                                'MsgBox("Pc File Unzlibed Succesfully")
                                correctos = correctos + 1

                            ElseIf Check_pc_console = &H2 Then
                                zlib1.unzlibconsole(Stream_file, archivo)
                                Stream_file.Close()
                                'MsgBox("Console File Unzlibed Succesfully")
                                correctos = correctos + 1


                            Else
                                MsgBox("UnkNown Compression")
                                incorrectos = incorrectos + 1
                            End If

                        Else
                            If Files.Count = 1 Then
                                MsgBox(archivo + " isn´t a Pes2014 compressed file")
                            End If
                            Stream_file.Close()
                            incorrectos = incorrectos + 1
                            'Return
                        End If



                    End If
                Catch Ex As Exception
                    'MessageBox.Show("Cannot read file from disk or File is Sized 0. Original error: " & Ex.Message)
                    incorrectos = incorrectos + 1

                End Try
                ProgressBar1.Increment(1)
            Next archivo

        End If
        MsgBox(correctos.ToString + " unzlib Succesfully  " + vbCrLf + incorrectos.ToString + " Failed to unzlib (Maybe size 0 or unknown compression)")
        correctos = 0
        incorrectos = 0

    End Sub

    Private Sub Button14_Click(sender As System.Object, e As System.EventArgs) Handles Button14.Click
        OpenPes = Me.OpenFileDialog1
        OpenPes.Title = "Open A Pes 2014  file"
        OpenPes.Filter = "*.* (*.*)|*.*"
        OpenPes.Multiselect = True

        Dim Contador_errores As Integer = 0
        Dim Pc_or_console As String = "UnkNown Format"

        If OpenPes.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Dim number_of_files As Integer = OpenPes.FileNames.Count

            ProgressBar1.Minimum = 1
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = number_of_files



            For Each Me.archivo In OpenPes.FileNames



                Try

                    Dim Stream_file As FileStream = File.Open(archivo, FileMode.Open, FileAccess.ReadWrite)
                    If (Stream_file IsNot Nothing And Stream_file.Length <> 0) Then
                        Dim Leer As New BinaryReader(Stream_file)

                        Leer.ReadBytes(Stream_file.Length)
                        Leer.BaseStream.Position = 3
                        Dim CheckPesFile As Integer = &H59534557
                       
                        If Leer.ReadUInt32 = CheckPesFile Then
                            Leer.BaseStream.Position = 0
                            Dim Check_pc_console As Byte = Leer.ReadByte
                            Dim Check_Console As Byte = &H1
                            If Check_pc_console = 0 Then
                                Leer.BaseStream.Position = 0


                                Stream_file.WriteByte(Check_Console)

                            Else
                                MsgBox("Marico Este Archivo no es para convertrlo!!!!")
                            End If

                        Else
                            MsgBox("Maricoooooo no es un archivo comprimido Pes... seguro que es un puto dds sin comprimir ;-)")

                        End If

                    End If

                Catch Ex As Exception
                    If OpenPes.FileNames.Count = 1 Then
                        MsgBox("Unknown error: " + Ex.ToString)
                    End If
                    Contador_errores += 1

                End Try
                ProgressBar1.Increment(1)
            Next archivo


        Else
            MsgBox("No File Selected!!!!!")
        End If

        If OpenPes.FileNames.Count <> 1 And OpenPes.FileNames.Count <> 0 Then

            MsgBox((OpenPes.FileNames.Count - Contador_errores).ToString & " MARICOOOOOOOOO KITS CONVERTIDOS DE LOS CUALES " & vbCrLf & Contador_errores.ToString & "  no se porqué carajo los pones jajajaja de un total de:" & OpenPes.FileNames.Count.ToString & " Files.")

        ElseIf OpenPes.FileNames.Count = 1 Then
            MsgBox((OpenPes.FileNames.Count - Contador_errores).ToString & " MARICOOOOOOOOO KITS CONVERTIDOS DE LOS CUALES " & vbCrLf & Contador_errores.ToString & "  no se porqué carajo los pones jajajaja de un total de:" & OpenPes.FileNames.Count.ToString & " Files.")

        End If
    End Sub

    Private Sub Button15_Click(sender As System.Object, e As System.EventArgs) Handles Button15.Click
        OpenPes = Me.OpenFileDialog1
        OpenPes.Title = "Open A Pes 2014  file"
        OpenPes.Filter = "*.* (*.*)|*.*"
        OpenPes.Multiselect = True

        Dim Contador_errores As Integer = 0
        Dim Pc_or_console As String = "UnkNown Format"

        If OpenPes.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Dim number_of_files As Integer = OpenPes.FileNames.Count

            ProgressBar1.Minimum = 1
            ProgressBar1.Value = 1
            ProgressBar1.Maximum = number_of_files



            For Each Me.archivo In OpenPes.FileNames



                Try

                    Dim Stream_file As FileStream = File.Open(archivo, FileMode.Open, FileAccess.ReadWrite)
                    If (Stream_file IsNot Nothing And Stream_file.Length <> 0) Then
                        Dim Leer As New BinaryReader(Stream_file)

                        Leer.ReadBytes(Stream_file.Length)
                        Leer.BaseStream.Position = 3
                        Dim CheckPesFile As Integer = &H59534557

                        If Leer.ReadUInt32 = CheckPesFile Then
                            Leer.BaseStream.Position = 28

                            Dim Check_Console As Byte = &HF0
                            


                                Stream_file.WriteByte(Check_Console)

                            Stream_file.Close()
                            Stream_file.Dispose()

                        Else
                            MsgBox("Maricoooooo no es un archivo comprimido Pes... seguro que es un puto dds sin comprimir ;-)")

                        End If

                    End If

                Catch Ex As Exception
                    If OpenPes.FileNames.Count = 1 Then
                        MsgBox("Unknown error: " + Ex.ToString)
                    End If
                    Contador_errores += 1

                End Try
                ProgressBar1.Increment(1)

            Next archivo


        Else
            MsgBox("No File Selected!!!!!")
        End If

        If OpenPes.FileNames.Count <> 1 And OpenPes.FileNames.Count <> 0 Then

            MsgBox((OpenPes.FileNames.Count - Contador_errores).ToString & " MARICOOOOOOOOO POSICIONES ARREGLADAS DE LAS CUALES " & vbCrLf & Contador_errores.ToString & "  no se porqué carajo los pones jajajaja de un total de:" & OpenPes.FileNames.Count.ToString & " Files.")

        ElseIf OpenPes.FileNames.Count = 1 Then
            MsgBox((OpenPes.FileNames.Count - Contador_errores).ToString & " MARICOOOOOOOOO POSICIONES ARREGLADAS DE LAS CUALES " & vbCrLf & Contador_errores.ToString & "  no se porqué carajo los pones jajajaja de un total de:" & OpenPes.FileNames.Count.ToString & " Files.")

        End If
    End Sub

    Private Sub Button16_Click(sender As System.Object, e As System.EventArgs) Handles Button16.Click

        Dim Stream_file As FileStream = File.Open("C:\Users\NUEVO2009\Desktop\10\pc\Player.bin", FileMode.Open, FileAccess.Read)
        Dim unzlib_memstream As New MemoryStream
        unzlib_memstream = zlib1.unzlibPc_to_Memstream(Stream_file)
               
        converter.Player_toConsole(unzlib_memstream)
        unzlib_memstream.Position = 0
        zlib1.zlib_memstream_to_console(unzlib_memstream, Stream_file.Name)
        Stream_file.Close()

        Dim Stream_file_2 As FileStream = File.Open("C:\Users\NUEVO2009\Desktop\10\pc\Converted to console\Player.bin", FileMode.Open, FileAccess.Read)
        zlib1.unzlibconsole(Stream_file_2, Stream_file_2.Name)
        Stream_file_2.Close()


                MsgBox("Mira que paso")


    End Sub
End Class