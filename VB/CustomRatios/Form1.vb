﻿Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraMap

Namespace CustomRatios
    Partial Public Class Form1
        Inherits Form

        Private Const filepath As String = "..\..\Data\Countries.shp"
        Private Const defaultSideSize As Integer = 512

        Private Class WidthHeightRatio
            Public Property Name() As String
            Public Property Value() As Double
        End Class
        Private ratios As New List(Of WidthHeightRatio)() From { _
            New WidthHeightRatio() With {.Name = "Default", .Value = 1}, _
            New WidthHeightRatio() With {.Name = "Lambert", .Value = 3.14}, _
            New WidthHeightRatio() With {.Name = "Behrmann", .Value = 2.36}, _
            New WidthHeightRatio() With {.Name = "Trystan Edwards", .Value = 2}, _
            New WidthHeightRatio() With {.Name = "Gall-Peters", .Value = 1.57}, _
            New WidthHeightRatio() With {.Name = "Balthasart", .Value = 1.3} _
        }

        Public Sub New()
            InitializeComponent()

            lbRatio.DataSource = ratios
            lbRatio.DisplayMember = "Name"
            AddHandler lbRatio.SelectedIndexChanged, AddressOf lbRatio_SelectedIndexChanged
            lbRatio.SetSelected(0, True)

            Dim baseUri As New Uri(System.Reflection.Assembly.GetEntryAssembly().Location)
            Dim uri As New Uri(baseUri, filepath)
            mapControl1.Layers.Add(New VectorItemsLayer() With { _
                .Data = New ShapefileDataAdapter() With {.FileUri = uri} _
            })
        End Sub

        Private Sub lbRatio_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            mapControl1.InitialMapSize = New Size() With {.Width = defaultSideSize, .Height = CInt((defaultSideSize / DirectCast(lbRatio.SelectedValue, WidthHeightRatio).Value))}
        End Sub
    End Class
End Namespace
